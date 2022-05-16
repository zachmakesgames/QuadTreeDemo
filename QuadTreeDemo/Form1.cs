using System.Diagnostics;

namespace QuadTreeDemo
{
    public partial class Form1 : Form
    {
        List<Point> points = new List<Point>();
        int treeWidth = 600;
        QuadTree tree;
        Stopwatch watch = new Stopwatch();
        List<QNodeLeaf> nodes = new List<QNodeLeaf>();

        Point pCenter = new Point(10, 0);
        float radius = 50;

        List<Point> infectedPoints = new List<Point>();

        Point selectedPoint = new Point(0,0);


        public Form1()
        {
            InitializeComponent();
            tree = new QuadTree(new Point(-treeWidth, treeWidth), new Point(treeWidth, -treeWidth), new Point(0, 0));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //watch.Reset();
            //watch.Start();
            //nodes = tree.FindNodesInRadius(pCenter, radius);

            //watch.Stop();
            //Trace.WriteLine($"Took {watch.ElapsedMilliseconds} ms to find neighbors");

            //Trace.WriteLine("Found " + nodes.Count.ToString() + " nodes in radius");

            
        }

        private void buildTreeButton_Click(object sender, EventArgs e)
        {
            points.Clear();
            Random random = new Random();
            for (int i = 0; i < 300; ++i)
            {
                Point p = new Point(random.Next(-300, 300), random.Next(-300, 300));
                points.Add(p);
            }

            tree = new QuadTree(new Point(-treeWidth, treeWidth), new Point(treeWidth, -treeWidth), new Point(0, 0));

            watch.Start();
            int idx = 0;
            foreach (Point p in points)
            {
                tree.AddPoint(p, idx++);
            }

            watch.Stop();
            Trace.WriteLine($"Took {watch.ElapsedMilliseconds} ms to build tree");

            UpdateImage();
        }

        private void pickRandom_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int idx = random.Next(0, points.Count);

            infectedPoints.Add(points[idx]);
            selectedPoint = points[idx];

            UpdateImage();
        }

        private void infectNeighbors_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Infecting neighbors");
            List<Point> newInfectedPoints = new List<Point>();
            foreach (Point p in infectedPoints) {
                List<QNodeLeaf> neighbors = tree.FindNodesInRadius(p, radius);
                foreach (QNodeLeaf neighbor in neighbors)
                {
                    Point neighborP = neighbor.Position;
                    if(Point.Distance(p, neighborP) < radius)
                    {
                        if (!infectedPoints.Contains(neighborP))
                        {
                            newInfectedPoints.Add(neighborP);
                        }
                    }
                }
            }

            infectedPoints.AddRange(newInfectedPoints);
            UpdateImage();
            Trace.WriteLine("Done");
        }

        private void UpdateImage()
        {
            Bitmap treeMap = tree.DrawTree();
            Graphics g = Graphics.FromImage(treeMap);
            int center_x = treeMap.Width / 2;
            int center_y = treeMap.Height / 2;

            foreach (Point p in points)
            {

                g.FillEllipse(Brushes.White, p.X + center_x, (-1 * p.Y) + center_y, 4, 4);
            }


            g.DrawEllipse(Pens.Blue, selectedPoint.X + center_x - radius, (-1 * selectedPoint.Y) + center_y - radius, radius * 2, radius * 2);

            foreach (QNodeLeaf node in nodes)
            {
                Point p = node.Position;
                g.FillEllipse(Brushes.Red, p.X + center_x, (-1 * p.Y) + center_y, 4, 4);
            }

            foreach(Point p in infectedPoints)
            {
                g.FillEllipse(Brushes.Purple, p.X + center_x, (-1 * p.Y) + center_y, 4, 4);
            }


            pictureBox1.Image = treeMap;
        }
    }
}