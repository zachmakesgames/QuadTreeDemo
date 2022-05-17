using System.Diagnostics;

namespace QuadTreeDemo
{
    public partial class Form1 : Form
    {
        List<Object2D> points = new List<Object2D>();
        int treeWidth = 500;
        PBQuadTree tree;
        Stopwatch watch = new Stopwatch();
        List<QNodeLeaf> nodes = new List<QNodeLeaf>();

        float radius = 50;

        Dictionary<int, Object2D> ignitedPoints = new Dictionary<int, Object2D>();
        //List<Object2D> ignitedPoints = new List<Object2D>(); using a dictionary makes it easier to manage ignited points

        Point selectedPoint = new Point(0,0);
        Point lastClicked = new Point(0,0);

        Bitmap treeMap = new Bitmap(1,1);


        bool useTree = true;
        bool drawQuads = true;

        TimeAttack ta = new TimeAttack();

        public Form1()
        {
            InitializeComponent();
            tree = new PBQuadTree(new Point(-treeWidth, treeWidth), new Point(treeWidth, -treeWidth), new Point(0, 0));
            ta.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Value = (int)radius;
            radiusLabel.Text = radius.ToString();
        }

        private void buildTreeButton_Click(object sender, EventArgs e)
        {
            points.Clear();
            ignitedPoints.Clear();
            selectedPoint = new Point();
            nodes.Clear();

            Random random = new Random();
            for (int i = 0; i < 10000; ++i)
            {
                Point p = new Point(random.Next(-treeWidth, treeWidth), random.Next(-treeWidth, treeWidth));
                Object2D obj = new Object2D();
                obj.position = p;
                obj.id = i;
                points.Add(obj);
            }

            tree = new PBQuadTree(new Point(-treeWidth, treeWidth), new Point(treeWidth, -treeWidth), new Point(0, 0));

            watch.Start();
            foreach (Object2D p in points)
            {
                tree.AddPoint(p.position, p);
            }

            watch.Stop();

            long microseconds = watch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
            Trace.WriteLine($"Took {watch.ElapsedMilliseconds} ms ({microseconds} us) to build tree");

            UpdateImage();
        }

        private void pickRandom_Click(object sender, EventArgs e)
        {
            if (points.Count > 0)
            {
                Random random = new Random();
                int idx = random.Next(0, points.Count);
                Object2D obj = points[idx];

                ignitedPoints[obj.id] = obj;
                selectedPoint = obj.position;

                UpdateImage();
            }
            }

            private void igniteNeighbors_Click(object sender, EventArgs e)
        {
            int maxignitedTime = 1;
            
            List<Object2D> newignitedPoints = new List<Object2D>();

            watch.Reset();
            watch.Start();
            int testedObjects = 0;

            if (useTree)
            {
                watch.Reset();
                watch.Start();
                foreach (Object2D p in ignitedPoints.Values)
                {
                    ++testedObjects;
                    if (p.ignitedTime < maxignitedTime)
                    {

                        p.ignitedTime++;
                        List<QNodeLeaf> neighbors = tree.FindNodesInRadius(p.position, radius);
                        foreach (QNodeLeaf neighbor in neighbors)
                        {
                            ++testedObjects;
                            Point neighborP = neighbor.Position;

                            foreach (Object2D obj in neighbor.Items)
                            {
                                if (Point.Distance(p.position, obj.position) < radius)
                                {
                                    if (!ignitedPoints.ContainsKey(obj.id))
                                    {
                                        newignitedPoints.Add(obj);
                                    }
                                }
                            }

                        }
                    }
                }
                watch.Stop();
            }
            else
            {
                watch.Reset();
                watch.Start();
                foreach (Object2D obj in ignitedPoints.Values)
                {
                    ++testedObjects;
                    if (obj.ignitedTime < maxignitedTime)
                    {
                        obj.ignitedTime++;
                        foreach (Object2D obj2 in points)
                        {
                            ++testedObjects;
                            if (obj != obj2)
                            {
                                if (Point.Distance(obj.position, obj2.position) < radius)
                                {
                                    if (!ignitedPoints.ContainsKey(obj2.id))
                                    {
                                        newignitedPoints.Add(obj2);
                                    }
                                }
                            }
                        }
                    }
                }
                watch.Stop();
            }

            float ms = watch.ElapsedMilliseconds;
            long microseconds = watch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
            Trace.WriteLine($"Took {ms.ToString()}ms ({microseconds} us) to ignite all neighbors");

            timeLabel.Text = $"{ms.ToString()}ms ({microseconds} us)";
            Trace.WriteLine("Tested " + testedObjects + " objects");
            foreach(Object2D obj in newignitedPoints)
            {
                ignitedPoints[obj.id] = obj;
            }


            UpdateImage();
        }

        private void UpdateImage()
        {
            
            Graphics g;
            if (drawQuads)
            {
                treeMap = tree.DrawTree();
                g = Graphics.FromImage(treeMap);
            }
            else
            {
                g = Graphics.FromImage(treeMap);
                g.Clear(Color.LightGreen);
            }
            //g.Clear(Color.LightGreen);
            int center_x = treeMap.Width / 2;
            int center_y = treeMap.Height / 2;

            foreach (Object2D obj in points)
            {
                Point p = obj.position;
                g.FillEllipse(Brushes.Green, p.X + center_x, (-1 * p.Y) + center_y, 4, 4);
            }


            g.DrawEllipse(Pens.Blue, selectedPoint.X + center_x - radius, (-1 * selectedPoint.Y) + center_y - radius, radius * 2, radius * 2);

            //foreach (QNodeLeaf node in nodes)
            //{
            //    Point p = node.Position;
            //    g.FillEllipse(Brushes.Red, p.X + center_x, (-1 * p.Y) + center_y, 4, 4);
            //}

            foreach(Object2D obj in ignitedPoints.Values)
            {
                Point p = obj.position;
                g.FillEllipse(Brushes.Orange, p.X + center_x, (-1 * p.Y) + center_y, 4, 4);
                //g.DrawEllipse(Pens.Blue, p.X + center_x - radius, (-1 * p.Y) + center_y - radius, radius * 2, radius * 2);
            }


            g.DrawEllipse(Pens.Red, lastClicked.X - radius + center_x, (-1*lastClicked.Y) + center_y - radius, radius * 2, radius * 2);

            g.FillEllipse(Brushes.Pink, lastClicked.X + center_x, (-1 * lastClicked.Y) + center_y, 4, 4);

            pictureBox1.Image = treeMap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs MEA = (MouseEventArgs)e;
            int X = MEA.Location.X;
            int Y = MEA.Location.Y;


            int img_width = treeMap.Width;
            int img_height = treeMap.Height;

            float scaled_x = (float)X / (float)pictureBox1.Width;
            float scaled_y = (float)Y / (float)pictureBox1.Height;
            scaled_x = scaled_x * img_width;
            scaled_y = scaled_y * img_height;

            int center_x = img_width / 2;
            int center_y = img_height / 2;

            int fixed_x = (int)scaled_x -  center_x;
            int fixed_y = -1*((int)scaled_y - center_y);

            lastClicked.X = fixed_x;
            lastClicked.Y = fixed_y;

            

            Point fixedP = new Point(fixed_x, fixed_y);

            List<QNodeLeaf> hits = tree.FindNodesInRadius(fixedP, radius);
            Graphics g = Graphics.FromImage(treeMap);

            List<Object2D> borderPoints = new List<Object2D>();

            Dictionary<int, Object2D> objHits = new Dictionary<int, Object2D>();



            foreach (QNodeLeaf hit in hits)
            {
                foreach (Object2D obj in hit.Items)
                {
                    if (Point.Distance(obj.position, fixedP) < radius)
                    {
                        
                        Point p = obj.position;

                        objHits[obj.id] = obj;

                        obj.ignitedTime = 0;
                        obj.ignited = false;

                        if (!ignitedPoints.ContainsKey(obj.id))
                        {
                            //Trace.WriteLine("Object doesn't seem to be in the list!");
                        }
                        if (!ignitedPoints.Remove(obj.id))
                        {
                            //Trace.WriteLine("Couldn't remove item");
                        }
                    }
                }
            }


            //Need to traverse the list twice, the second time allows us to ensure that we
            //only reset the ignited points that are not in the list of points that we just
            //cleansed
            foreach (Object2D hit in objHits.Values)
            {
                //Reset the ignite timer on near by nodes so they can ignite again
                List<QNodeLeaf> neighbors = tree.FindNodesInRadius(hit.position, radius);
                foreach (QNodeLeaf neighbor in neighbors)
                {
                    foreach (Object2D n in neighbor.Items)
                    {
                        if (Point.Distance(n.position, hit.position) < radius)
                        {
                            if (!objHits.ContainsKey(n.id))
                            {
                                if (ignitedPoints.ContainsKey(n.id))
                                {
                                    ignitedPoints[n.id].ignitedTime = 0;
                                }
                            }
                        }
                    }
                }
            }


            UpdateImage();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            radius = trackBar1.Value;
            radiusLabel.Text = radius.ToString();
            foreach(Object2D obj in ignitedPoints.Values)
            {
                obj.ignitedTime = 0;
            }

            UpdateImage();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            useTree = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            drawQuads = checkBox2.Checked;
            UpdateImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!ta.IsDisposed)
            {
                ta.Show();
            }
            else
            {
                ta = new TimeAttack();
                ta.Show();
            }
            
        }
    }
}