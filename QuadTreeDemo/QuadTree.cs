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
    internal class QuadTree
    {
        QNodeBase root;

        public QuadTree(Point topLeft, Point bottomRight, Point center)
        {
            root = new QNodeSpine(topLeft, bottomRight, center);
        }

        public static Quad SubdivideQuad(Point extentTopLeft, Point extentBottomRight, Point center, int quadrant)
        {
            int new_half_extent_x = (extentBottomRight.X - extentTopLeft.X) / 4;
            int new_half_extent_y = (extentBottomRight.Y - extentTopLeft.Y) / 4;

            Point topLeft = new Point();
            Point bottomRight = new Point();

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
                        bottomRight = new Point(center + new Point(new_half_extent_x, new_half_extent_y));
                    }
                    break;
                case 3:
                    {
                        topLeft = new Point(center - new Point(new_half_extent_x, 0));
                        bottomRight = new Point(center + new Point(0, new_half_extent_y));
                    }
                    break;
            }

            Point newCenter = Point.Center(topLeft, bottomRight);

            Quad subQuad = new Quad();
            topLeft.X--;
            topLeft.Y++;
            bottomRight.X++;
            bottomRight.Y--;
            subQuad.topLeft = topLeft;
            subQuad.bottomRight = bottomRight;
            subQuad.center = newCenter;

            return subQuad;
        }

        private static int GetQuadrant(Point p, Point topLeft, Point bottomRight)
        {
            int quadrant = -1;

            //Make sure the point lies in the quadrant.
            //May need to tweek this to include points that lie on the edge
            if ((p.X <= bottomRight.X) && (p.X >= topLeft.X) &&
                (p.Y <= topLeft.Y) && (p.Y >= bottomRight.Y))
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

        //private void BreakLeaf(ref QNodeBase leafNode, ref QNodeBase parentNode, int quadrant)
        //{
        //    if (leafNode != null && parentNode != null)
        //    {
        //        QNodeLeaf leaf = leafNode as QNodeLeaf;

        //        //Copy the leaf's data so it doesnt go away when the node gets deleted
        //        Point p = new Point(leaf.Position);
        //        List<object> items = new List<object>(leaf.Items);

        //        QNodeSpine parent = parentNode as QNodeSpine;

        //        //Dividing by 4 gives us the half extent which is more usful than the full extent
        //        //so we can calculate the corners
        //        int new_half_extent_x = (parent.ExtentBottomRight.X - parent.ExtentTopLeft.X) / 4;
        //        int new_half_extent_y = (parent.ExtentBottomRight.Y - parent.ExtentTopLeft.Y) / 4;

        //        Point topLeft = new Point();
        //        Point bottomRight = new Point();

        //        switch (quadrant)
        //        {
        //            case 0:
        //                {
        //                    topLeft = new Point(parent.ExtentTopLeft);
        //                    bottomRight = new Point(parent.Position);
        //                }
        //                break;
        //            case 1:
        //                {
        //                    topLeft = new Point(parent.Position + new Point(0, new_half_extent_y));
        //                    bottomRight = new Point(parent.Position + new Point(new_half_extent_x, 0));
        //                }
        //                break;
        //            case 2:
        //                {
        //                    topLeft = new Point(parent.Position);
        //                    bottomRight = new Point(parent.Position + new Point(new_half_extent_x, new_half_extent_y));
        //                }
        //                break;
        //            case 3:
        //                {
        //                    topLeft = new Point(parent.Position - new Point(new_half_extent_x, 0));
        //                    bottomRight = new Point(parent.Position + new Point(0, new_half_extent_y));
        //                }
        //                break;
        //        }

        //        Point newCenter = Point.Center(topLeft, bottomRight);

        //        leafNode = new QNodeSpine(topLeft, bottomRight, newCenter);

        //        //Add a new leaf node to the now spine node
        //        int quad = GetQuadrant(p, topLeft, bottomRight);

        //        if (quad == -1)
        //        {
        //            throw new Exception("Node is outside of its own extent, what the fuck?!");
        //        }


        //        ((QNodeSpine)leafNode).Children[quad] = new QNodeLeaf(p, items);
        //    }
        //}

        private void AddPointToNode(ref QNodeBase node, Point p, object data)
        {
            if (node == null)
            {
                return;
            }

            //Shouldn't need this, it will be easier to do a forward search at a spine and create
            //a new spine from an old spine instead of a new spine from a leaf
            //if(node.GetType() == typeof(QNodeLeaf))
            //{
            //    //need to break the leaf into a spine and subdivide
            //    //BreakLeaf(node, parent, )
            //}
            //else{
            //Determine which quadrant it should go in


            QNodeSpine spine = node as QNodeSpine;

            int quadrant = GetQuadrant(p, spine.ExtentTopLeft, spine.ExtentBottomRight);

            if(quadrant < 0)
            {
                Trace.WriteLine("Point at X: " + p.X.ToString() + " Y: " + p.Y.ToString() + " falls out of its quadrant!");
                return;///TODO: Figure out how to handle nodes that lie on the edge of a quadrant
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
                    //Otherwise get the leaf that is currently in the way,
                    //then create a new spine and call AddPointToNode twice,
                    //once for the old point (do this one first!) and then
                    //again for the new point
                    //also need to break the leaf into a spine and subdivide
                    QNodeLeaf leaf = (QNodeLeaf)spine.Children[quadrant];

                    Quad subQuad = SubdivideQuad(spine.ExtentTopLeft, spine.ExtentBottomRight, spine.Position, quadrant);


                    //Calculate 
                    spine.Children[quadrant] = new QNodeSpine(subQuad.topLeft, subQuad.bottomRight, subQuad.center);

                    AddPointToNode(ref spine.Children[quadrant], leaf.Position, leaf.Items);
                    AddPointToNode(ref spine.Children[quadrant], p, data);
                }
            }
            //}
        }

        public void AddPoint(Point p, object data)
        {
            AddPointToNode(ref root, p, data);
        }

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
                else
                {
                    g.FillEllipse(Brushes.Red, node.Position.X+center_x, -1*node.Position.Y+center_y, 6, 6);
                }
            }
        }
    }
}
