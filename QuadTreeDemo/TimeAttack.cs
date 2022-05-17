using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuadTreeDemo
{
    public partial class TimeAttack : Form
    {
        public TimeAttack()
        {
            InitializeComponent();
        }

        private void timeAttackButton_Click(object sender, EventArgs e)
        {

            //Test the difference between using a quadtree to process nodes and
            //a standard nested for loop running at O(n^2)
            List<Object2D> points = new List<Object2D>();
            Random random = new Random();

            Stopwatch watch = new Stopwatch();

            int treeWidth = (int)treeWidthBox.Value;

            for(int i =0; i < nodeBox.Value; ++i)
            {
                Object2D obj = new Object2D();
                obj.position = new Point(random.Next(-treeWidth, treeWidth), random.Next(-treeWidth, treeWidth));
                obj.id = i;
                points.Add(obj);
            }

            Point tl = new Point(-treeWidth, treeWidth);
            Point br = new Point(treeWidth, -treeWidth);

            PBQuadTree tree = new PBQuadTree(tl, br, new Point(0, 0));

            foreach(Object2D obj in points)
            {
                tree.AddPoint(obj.position, obj);
            }

            Object2D testObj = points[random.Next(0, points.Count)];

            int runCount = (int)runCountBox.Value;


            List<float> run_times_tree = new List<float>();
            List<float> run_times_no_tree = new List<float>();

            int treeHits = 0;
            for(int i = 0; i < runCount; ++i)
            {
                watch.Reset();
                watch.Start();
                List<QNodeLeaf> hits = tree.FindNodesInRadius(testObj.position, (float)searchRadiusBox.Value);
                foreach(QNodeLeaf hit in hits)
                {
                    foreach(Object2D hitObj in hit.Items)
                    {
                        if(Point.Distance(testObj.position, hitObj.position) < (float)searchRadiusBox.Value)
                        {
                            ++treeHits;
                        }
                    }
                }
                watch.Stop();
                long microseconds = watch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                float ms = (float)microseconds / 1000.0f;
                run_times_tree.Add(ms);
            }


            int noTreeHits = 0;
            for(int i =0; i < runCount; ++i)
            {
                watch.Reset();
                watch.Start();
                foreach (Object2D obj in points)
                {
                    if(obj != testObj)
                    {
                        if (Point.Distance(testObj.position, obj.position) < (float)searchRadiusBox.Value)
                        {
                            ++noTreeHits;
                        }
                    }
                }

                watch.Stop();
                long microseconds = watch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                float ms = (float)microseconds / 1000.0f;
                run_times_no_tree.Add(ms);
            }

            float avg_time_tree = 0;
            foreach(float f in run_times_tree)
            {
                avg_time_tree += f;
            }
            avg_time_tree /= runCount;

            float avg_time_no_tree = 0;
            foreach(float f in run_times_no_tree){
                avg_time_no_tree += f;
            }
            avg_time_no_tree /= runCount;

            treeTimeLabel.Text = avg_time_tree.ToString();
            noTreeTimeLabel.Text = avg_time_no_tree.ToString();


        }
    }
}
