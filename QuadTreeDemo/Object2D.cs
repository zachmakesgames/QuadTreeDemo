using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeDemo
{
    //A test object for the demo
    internal class Object2D
    {
        public Point position = new Point();
        public bool ignited = false;
        public int ignitedTime = 0;
        public int id = 0;

        public Object2D() { }
    }
}
