//Geometry.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeDemo
{
    //A very basic 2D Point implementation
    //with some useful operators and functions
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


    //A very basic 2D Quad implementation
    //with some very helpful basic collision
    //detection functions
    public class Quad
    {
        public Point topLeft = new Point();
        public Point bottomRight = new Point();
        public Point center = new Point();

        //A function to determine if a circle overlaps this quad
        public bool DoesCircleOverlap(Point circ_center, float radius)
        {
            float sqrDist = SqrDistance(circ_center);

            return sqrDist <= radius * radius;
        }

        //A functoin to determine the squared distance from any point
        //in this quad to the given point, needed for the above function
        public float SqrDistance(Point p)
        {

            float sqrDist = 0.0f;

            float min_x = topLeft.X;
            float max_x = bottomRight.X;
            float min_y = bottomRight.Y;
            float max_y = topLeft.Y;

            float vx = p.X;
            if (vx < min_x) sqrDist += (min_x-vx) * (min_x-vx);
            if (vx > max_x) sqrDist += (vx-max_x) * (vx-max_x);

            float vy = p.Y;
            if(vy < min_y) sqrDist += (min_y - vy) * (min_y - vy);
            if(vy > max_y) sqrDist += (vy - max_y) * (vy - max_y);

            return sqrDist;
        }

    }
}
