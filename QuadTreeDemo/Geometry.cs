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
        public int X = 0;
        public int Y = 0;

        public Point() { }

        public Point(int x, int y)
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

    }
}
