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

        private void AddPointToNode(ref QNodeBase node, Point p, object data)
        {
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
                
            }
        }
    }
}
