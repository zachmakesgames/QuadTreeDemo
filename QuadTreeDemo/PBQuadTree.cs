using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeDemo
{
    //PBQuadTree or PreBuiltQuadTree preallocates nodes down to a specified max depth
    //This makes inserting faster as we know we never have to allocate another node,
    //we can use a while loop to insert nodes instead of using recursion.
    internal class PBQuadTree
    {
        QNodeBase root;

        public int maxDepth = 4;

        public PBQuadTree(Point topLeft, Point bottomRight, Point center)
        {
            root = new QNodeSpine(topLeft, bottomRight, center);
            BuildTree(ref root, 0);
        }

        //Pre-build the tree with empty leaf nodes
        private void BuildTree(ref QNodeBase node, int depth)
        {
            Quad q0 = SubdivideQuad(node.ExtentTopLeft, node.ExtentBottomRight, 0);
            Quad q1 = SubdivideQuad(node.ExtentTopLeft, node.ExtentBottomRight, 1);
            Quad q2 = SubdivideQuad(node.ExtentTopLeft, node.ExtentBottomRight, 2);
            Quad q3 = SubdivideQuad(node.ExtentTopLeft, node.ExtentBottomRight, 3);
            
            if (depth < maxDepth)
            {

                QNodeSpine s = node as QNodeSpine;

                s.Children[0] = new QNodeSpine(q0);
                s.Children[1] = new QNodeSpine(q1);
                s.Children[2] = new QNodeSpine(q2);
                s.Children[3] = new QNodeSpine(q3);

                BuildTree(ref s.Children[0], depth + 1);
                BuildTree(ref s.Children[1], depth + 1);
                BuildTree(ref s.Children[2], depth + 1);
                BuildTree(ref s.Children[3], depth + 1);
            }
            else
            {
                ((QNodeSpine)node).Children[0] = new QNodeLeaf(q0.center);
                ((QNodeSpine)node).Children[1] = new QNodeLeaf(q1.center);
                ((QNodeSpine)node).Children[2] = new QNodeLeaf(q2.center);
                ((QNodeSpine)node).Children[3] = new QNodeLeaf(q3.center);
            }
        }

        //Same subdivision function as QuadTree
        public static Quad SubdivideQuad(Point extentTopLeft, Point extentBottomRight, int quadrant)
        {
            float new_half_extent_x = Math.Abs(extentBottomRight.X - extentTopLeft.X) / 2;
            float new_half_extent_y = Math.Abs(extentBottomRight.Y - extentTopLeft.Y) / 2;

            Point topLeft = new Point();
            Point bottomRight = new Point();

            Point center = Point.Center(extentTopLeft, extentBottomRight);

            switch (quadrant)
            {
                case 0:
                    {
                        topLeft = new Point(extentTopLeft);
                        bottomRight = new Point(center);
                    }
                    break;
                case 1:
                    {
                        topLeft = new Point(center + new Point(0, new_half_extent_y));
                        bottomRight = new Point(center + new Point(new_half_extent_x, 0));
                    }
                    break;
                case 2:
                    {
                        topLeft = new Point(center);
                        bottomRight = new Point(center + new Point(new_half_extent_x, -1 * new_half_extent_y));
                    }
                    break;
                case 3:
                    {
                        topLeft = new Point(center - new Point(new_half_extent_x, 0));
                        bottomRight = new Point(center - new Point(0, new_half_extent_y));
                    }
                    break;
            }

            Point newCenter = Point.Center(topLeft, bottomRight);

            if (topLeft.Y - bottomRight.Y < 0 ||
                bottomRight.X - topLeft.X < 0)
            {
                throw new Exception("Invalid area!");
            }

            Quad subQuad = new Quad();
            subQuad.topLeft = topLeft;
            subQuad.bottomRight = bottomRight;
            subQuad.center = newCenter;

            return subQuad;
        }

        //Same quadrant function as QuadTree
        private static int GetQuadrant(Point p, Point topLeft, Point bottomRight)
        {
            int quadrant = -1;

            float low_x = topLeft.X;
            float high_x = bottomRight.X;
            float low_y = bottomRight.Y;
            float high_y = topLeft.Y;

            //Make sure the point lies in the quadrant.
            //May need to tweek this to include points that lie on the edge
            if ((p.X >= low_x) && (p.X <= high_x) &&
                (p.Y >= low_y) && (p.Y <= high_y))
            {
                Point center = Point.Center(topLeft, bottomRight);

                if (p.X >= center.X)
                {
                    if (p.Y >= center.Y)
                    {
                        quadrant = 1;
                    }
                    else
                    {
                        quadrant = 2;
                    }
                }
                else
                {
                    if (p.Y >= center.Y)
                    {
                        quadrant = 0;
                    }
                    else
                    {
                        quadrant = 3;
                    }
                }
            }


            return quadrant;
        }

        //A linear method to add a point/object to the current tree.
        //Since we should never have to re-allocate leaf nodes, we can
        //easily navigate the tree in a linear method instead of recursively.
        public void AddPoint(Point p, Object2D data)
        {
            ref QNodeBase n = ref root;
            while (true)
            {
                if (n.GetType() == typeof(QNodeSpine))
                {
                    int quad = GetQuadrant(p, n.ExtentTopLeft, n.ExtentBottomRight);
                    if (quad >= 0)
                    {
                        n = ref ((QNodeSpine)n).Children[quad];
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    //n is a leaf so add the data to the leaf
                    QNodeLeaf leaf = n as QNodeLeaf;
                    leaf.Items.Add(data);
                    return;
                }
                
            }
        }

        //A simple function to draw the current tree
        public Bitmap DrawTree()
        {
            int width = (int)(root.ExtentBottomRight.X - root.ExtentTopLeft.X);
            int height = (int)(root.ExtentTopLeft.Y - root.ExtentBottomRight.Y);
            Bitmap bitmap = new Bitmap(width + 10, height + 10);

            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.LightGreen);

            DrawNode(ref bitmap, ref root);


            return bitmap;
        }

        //A simple function to draw the given node on
        //the given bitmap
        public void DrawNode(ref Bitmap bitmap, ref QNodeBase node)
        {
            if (node != null)
            {
                Graphics g = Graphics.FromImage(bitmap);

                int center_x = bitmap.Width / 2;
                int center_y = bitmap.Height / 2;

                if (node.GetType() == typeof(QNodeSpine))
                {
                    int width = (int)(node.ExtentBottomRight.X - node.ExtentTopLeft.X);
                    int height = (int)(node.ExtentTopLeft.Y - node.ExtentBottomRight.Y);

                    g.DrawRectangle(Pens.Black, node.ExtentTopLeft.X + center_x, -1 * node.ExtentTopLeft.Y + center_y, width, height);

                    QNodeSpine spine = node as QNodeSpine;
                    DrawNode(ref bitmap, ref spine.Children[0]);
                    DrawNode(ref bitmap, ref spine.Children[1]);
                    DrawNode(ref bitmap, ref spine.Children[2]);
                    DrawNode(ref bitmap, ref spine.Children[3]);
                }

            }
        }


        //A recursive function to find all leaf nodes that overlap the circle
        //created by center point p and radius
        private void CheckNodesInRadius(ref List<QNodeLeaf> nodes, ref QNodeBase current_node, Point p, float radius)
        {
            if (current_node.GetType() == typeof(QNodeSpine))
            {
                QNodeSpine spine = current_node as QNodeSpine;

                Quad q = new Quad();
                q.topLeft = spine.ExtentTopLeft;
                q.bottomRight = spine.ExtentBottomRight;
                q.center = spine.Position;

                if (q.DoesCircleOverlap(p, radius))
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        QNodeBase child = spine.Children[i];
                        if (child != null)
                        {

                            if (child.GetType() == typeof(QNodeLeaf))
                            {
                                nodes.Add(child as QNodeLeaf);
                            }
                            else
                            {
                                CheckNodesInRadius(ref nodes, ref child, p, radius);
                            }
                        }
                    }
                }
            }

        }


        //A public function to initiate the recursive finding of overlapping
        //leaf nodes
        public List<QNodeLeaf> FindNodesInRadius(Point p, float radius)
        {
            List<QNodeLeaf> nodes = new List<QNodeLeaf>();
            CheckNodesInRadius(ref nodes, ref root, p, radius);

            return nodes;
        }
    }
}
