//QNodeS.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeDemo
{
    internal class QNodeBase
    {
        public Point Position = new Point();

        public Point ExtentTopLeft = new Point();
        public Point ExtentBottomRight = new Point();
    }

    internal class QNodeSpine : QNodeBase
    {
        public QNodeBase[] Children = new QNodeBase[4]; //0 = TopLeft, 1 = TopRight, 2 = BottomRight, 3 = BottomLeft

        public int depth = 0;

        public QNodeSpine(Point topLeft, Point bottomRight, Point center)
        {
            ExtentTopLeft = topLeft;
            ExtentBottomRight = bottomRight;
            Position = center;
        }

        public QNodeSpine(Quad q)
        {
            ExtentTopLeft = q.topLeft;
            ExtentBottomRight = q.bottomRight;
            Position = q.center;
        }

        public QNodeSpine()
        {
            ExtentTopLeft = new Point();
            ExtentBottomRight = new Point();

        }
    }

    internal class QNodeLeaf : QNodeBase
    {
        public List<Object2D> Items = new List<Object2D>();

        public QNodeLeaf(Point position)
        {
            Position = position;
        }

        public QNodeLeaf(Point position, Object2D item)
        {
            Position = position;
            Items.Add(item);
        }

        public QNodeLeaf(Point position, List<Object2D> items)
        {
            Position = position;
            Items.AddRange(items);
        }
    }
}
