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
            int treeWidth = 1000;
            QuadTree testTree = new QuadTree(new Point(-treeWidth, treeWidth), new Point(treeWidth, -treeWidth), new Point(0, 0));
            
            List<Point> points = new List<Point>();
            
            Random random = new Random();
            for (int i = 0; i < 300; ++i)
            {
                Point p = new Point(random.Next(-300, 300), random.Next(-300, 300));
                points.Add(p);
                testTree.AddPoint(p, i);
            }

            
            


            Bitmap tree = testTree.DrawTree();
            Graphics g = Graphics.FromImage(tree);

            foreach (Point p in points)
            {
                int center_x = tree.Width / 2;
                int center_y = tree.Height / 2;
                g.FillEllipse(Brushes.Red, p.X + center_x, (-1 * p.Y) + center_y, 2, 2);
            }

            pictureBox1.Image = tree;
        }
    }
}