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



        public QNodeSpine(Point topLeft, Point bottomRight, Point center)
        {
            ExtentTopLeft = topLeft;
            ExtentBottomRight = bottomRight;
            Position = center;
        }

        public QNodeSpine()
        {
            ExtentTopLeft = new Point();
            ExtentBottomRight = new Point();

        }
    }

    internal class QNodeLeaf : QNodeBase
    {
        public List<object> Items = new List<object>();

        public QNodeLeaf(Point position)
        {
            Position = position;
        }

        public QNodeLeaf(Point position, object item)
        {
            Position = position;
            Items.Add(item);
        }

        public QNodeLeaf(Point position, List<object> items)
        {
            Position = position;
            Items.AddRange(items);
        }
    }
}
