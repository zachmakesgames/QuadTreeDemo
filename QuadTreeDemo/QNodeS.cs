//QNodeS.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeDemo
{
    //Base node has position and extents
    //Could be improved by simply using a Quad
    //to contain all three
    internal class QNodeBase
    {
        public Point Position = new Point();

        public Point ExtentTopLeft = new Point();
        public Point ExtentBottomRight = new Point();
    }

    //Spine is derived from base, but also has 4 children
    //and a depth (depth in the tree)
    internal class QNodeSpine : QNodeBase
    {
        public QNodeBase[] Children = new QNodeBase[4]; //0 = TopLeft, 1 = TopRight, 2 = BottomRight, 3 = BottomLeft

        public int depth = 0;

        public QNodeSpine(Point topLeft, Point bottomRight, Point center, int node_depth = 0)
        {
            ExtentTopLeft = topLeft;
            ExtentBottomRight = bottomRight;
            Position = center;
            depth = node_depth;
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

    //Leaf is also derived from base, but has a
    //list of objects contained within the leaf
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
