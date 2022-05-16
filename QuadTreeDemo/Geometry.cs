//Geometry.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeDemo
{
    public class Point
    {
        public float X = 0;
        public float Y = 0;

        public Point() { }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Point(Point copy)
        {
            this.X = copy.X;
            this.Y = copy.Y;
        }

        public static Point Center(Point topLeft, Point bottomRight)
        {
            return new Point((bottomRight.X + topLeft.X) / 2, (topLeft.Y + bottomRight.Y) / 2);
        }

        public static float Distance(Point p1, Point p2)
        {
            float x_m = p2.X - p1.X;
            float y_m = p2.Y - p1.Y;
            
            return MathF.Sqrt(x_m * x_m + y_m * y_m);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static Point operator *(Point p, int i)
        {
            return new Point(p.X * i, p.Y * i);
        }

        public static Point operator *(int i, Point p)
        {
            return new Point(p.X * i, p.Y * i);
        }
    }

    public class Quad
    {
        public Point topLeft = new Point();
        public Point bottomRight = new Point();
        public Point center = new Point();

        public bool DoesCircleOverlap(Point circ_center, float radius)
        {
            float min_x = topLeft.X;
            float max_x = bottomRight.X;
            float min_y = bottomRight.Y;
            float max_y = topLeft.Y;


            Point bottomLeft = new Point(topLeft.X, bottomRight.Y);
            Point topRight = new Point(bottomRight.X, topLeft.Y);

            if(
                (circ_center.X > min_x && circ_center.X < max_x && circ_center.Y > min_y && circ_center.Y < max_y) ||
                Point.Distance(topLeft, circ_center) < radius ||
                Point.Distance(bottomRight, circ_center) < radius ||
                Point.Distance(bottomLeft, circ_center) < radius ||
                Point.Distance(topRight, circ_center) < radius ||
                Point.Distance(center, circ_center) < radius)
            {
                return true;
            }

            return false;
        }

    }
}
