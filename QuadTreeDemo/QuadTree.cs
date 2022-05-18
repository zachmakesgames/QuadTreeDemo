//QuadTree.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace QuadTreeDemo
{
    //A non pre-allocated quad tree that allocates new nodes as they are inserted.
    //This implementation works for small quantities of objects, but causes stack
    //overflows beyond roughly 200 objects (changes based on object distribution).
    
    //This implementation now has a depth limit which avoids a stack overflow by
    //creating a list of items at the deepest node instead of having only one
    //item per leaf
    internal class QuadTree
    {
        QNodeBase root;

        public int maxDepth = 8;

        public QuadTree(Point topLeft, Point bottomRight, Point center, int max_depth = 8)
        {
            root = new QNodeSpine(topLeft, bottomRight, center);
            maxDepth = max_depth;
        }

        //A basic function to subdivide a given quad, returns the quad in the quadrant specificed by quadrant
        public static Quad SubdivideQuad(Point extentTopLeft, Point extentBottomRight, Point center_point, int quadrant)
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
                        bottomRight = new Point(center + new Point(new_half_extent_x, -1*new_half_extent_y));
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

            if(topLeft.Y - bottomRight.Y < 0 ||
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

        //Returns the quadrant number that point p falls in given a quad defined by topLeft and bottomRight
        //Returns -1 if the point falls outside of the quad
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

        //A recursive function to a point/object to a given node
        private void AddPointToNode(ref QNodeBase node, Point p, List<Object2D> data)
        {

            //Trace.WriteLine("Current tree depth: " + maxDepth.ToString());

            if (node == null)
            {
                return;
            }

            
            QNodeSpine spine = node as QNodeSpine;

            int quadrant = GetQuadrant(p, spine.ExtentTopLeft, spine.ExtentBottomRight);

            if(quadrant < 0)
            {
                Trace.WriteLine("Point at X: " + p.X.ToString() + " Y: " + p.Y.ToString() + " falls out of its quadrant!");
                return;
            }

            if (spine.Children[quadrant] == null)
            {
                //If the spine has an empty spot, just add the leaf
                spine.Children[quadrant] = new QNodeLeaf(p, data);

            }
            else
            {
                if (spine.Children[quadrant] is QNodeSpine)
                {
                    AddPointToNode(ref spine.Children[quadrant], p, data);
                }
                else
                {
                    QNodeLeaf leaf = (QNodeLeaf)spine.Children[quadrant];

                    //Otherwise get the leaf that is currently in the way,
                    //then create a new spine and call AddPointToNode twice,
                    //once for the old point (do this one first!) and then
                    //again for the new point
                    //also need to break the leaf into a spine and subdivide
                    if (spine.depth >= maxDepth)
                    {
                        //If we're at max depth, just add the data to the leaf
                        leaf.Items.AddRange(data);
                    }
                    else
                    {
                        //Otherwise we can break the leaf into a new spine
                        Quad subQuad = SubdivideQuad(spine.ExtentTopLeft, spine.ExtentBottomRight, spine.Position, quadrant);

                        QNodeSpine newSpine = new QNodeSpine(subQuad.topLeft, subQuad.bottomRight, subQuad.center, spine.depth+1);
                        
                        spine.Children[quadrant] = newSpine;

                        AddPointToNode(ref spine.Children[quadrant], leaf.Position, leaf.Items);
                        AddPointToNode(ref spine.Children[quadrant], p, data);
                    }
                }
            }
            //}
        }

        //The public method to add a point to the tree
        //Recursively calls AddPointToNode on the root node
        public void AddPoint(Point p, Object2D data)
        {
            List<Object2D> dataList = new List<Object2D>();
            dataList.Add(data);
            AddPointToNode(ref root, p, dataList);
        }

        //A function to draw the current tree to a bitmap
        public Bitmap DrawTree()
        {
            int width = (int)(root.ExtentBottomRight.X - root.ExtentTopLeft.X);
            int height = (int)(root.ExtentTopLeft.Y - root.ExtentBottomRight.Y);
            Bitmap bitmap = new Bitmap(width+10, height+10);

            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.LightGreen);

            DrawNode(ref bitmap, ref root);


            return bitmap;
        }

        //A recursive function to draw a given node
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

                    g.DrawRectangle(Pens.Black, node.ExtentTopLeft.X+center_x, -1*node.ExtentTopLeft.Y+center_y, width, height);

                    QNodeSpine spine = node as QNodeSpine;
                    DrawNode(ref bitmap, ref spine.Children[0]);
                    DrawNode(ref bitmap, ref spine.Children[1]);
                    DrawNode(ref bitmap, ref spine.Children[2]);
                    DrawNode(ref bitmap, ref spine.Children[3]);
                }
                
            }
        }
    
        //Recursively searches the tree to find all nodes that overlap the circle created
        //by a center point p and radius.
        //Uses Quad.DoesCircleOverlap to determine if the current node overlaps the circle
        private void CheckNodesInRadius(ref List<QNodeLeaf> nodes, ref QNodeBase current_node, Point p, float radius)
        {
            if(current_node.GetType() == typeof(QNodeSpine))
            {
                QNodeSpine spine = current_node as QNodeSpine;

                Quad q = new Quad();
                q.topLeft = spine.ExtentTopLeft;
                q.bottomRight = spine.ExtentBottomRight;
                q.center = spine.Position;

                if(q.DoesCircleOverlap(p, radius))
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


        //A public starter function to find all nodes that overlap a circle
        //created by center point p and radius
        public List<QNodeLeaf> FindNodesInRadius(Point p, float radius)
        {
            List<QNodeLeaf> nodes = new List<QNodeLeaf>();
            CheckNodesInRadius(ref nodes, ref root, p, radius);

            return nodes;
        }
    }
}
