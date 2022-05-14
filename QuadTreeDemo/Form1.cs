namespace QuadTreeDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            QuadTree testTree = new QuadTree(new Point(-100, 100), new Point(100, -100), new Point(0, 0));

            Random random = new Random();
            //for(int i = 0; i < 100; ++i)
            //{
            //    Point p = new Point(random.Next(-100, 100), random.Next(-100, 100));
            //    testTree.AddPoint(p, i);
            //}

            List<Point> points = new List<Point>();
            points.Add(new Point(-50, 50));
            //points.Add(new Point(50, 50));
            //points.Add(new Point(50, -50));
            //points.Add(new Point(-50, -50));
            //points.Add(new Point(-70, 60));
            //points.Add(new Point(-80, 70));
            points.Add(new Point(-50, 30)); //this causes problems
            points.Add(new Point(-49, 10));

            testTree.AddPoint(new Point(-50, 50), 1);
            testTree.AddPoint(new Point(-50, 30), 2); //This point gets added to the wrong quadrant when subdividing!
            testTree.AddPoint(new Point(-49, 10), 3);


            //int idx = 1;
            //foreach(Point p in points)
            //{
            //    testTree.AddPoint(p, idx++);
            //}


            Bitmap tree = testTree.DrawTree();
            Graphics g = Graphics.FromImage(tree);

            foreach(Point p in points)
            {
                int center_x = tree.Width / 2;
                int center_y = tree.Height / 2;
                g.FillEllipse(Brushes.Blue, p.X + center_x, -1 * p.Y + center_y, 3, 3);
            }

            pictureBox1.Image = tree;
        }
    }
}