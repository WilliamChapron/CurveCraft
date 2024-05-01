using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.Annotations;
using System.Diagnostics;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using MathNet.Numerics.Interpolation;


using System.Runtime.InteropServices;
using System.IO;


namespace MathProjet
{

    public partial class Form1 : Form
    {
        private PlotView plotView;
        private PlotModel plotModel;
        private ScatterSeries scatterSeries;
        private LineSeries lineSeries;


        struct PointInfo
        {
            public char Letter;
            public double X;
            public double Y;
            public double slope;
        }

        struct Segment
        {
            public PointInfo Point1 { get; set; }
            public PointInfo Point2 { get; set; }
            public double Slope { get; set; }
        }

        private List<Segment> segments = new List<Segment>();
        private List<PointInfo> points = new List<PointInfo>();

        public Form1()
        {
            InitializeComponent();
            InitializePlot();




            // Organisé par ordre de x  -> 9 points
            //points.Add(new PointInfo() { Letter = 'C', X = 1.99, Y = 2.78 }); // 0
            //points.Add(new PointInfo() { Letter = 'D', X = 2, Y = 7.13 }); // 1
            //points.Add(new PointInfo() { Letter = 'F', X = 3.60, Y = 9.69 }); // 2
            //points.Add(new PointInfo() { Letter = 'M', X = 5.92, Y = 1.52 }); // 3
            //points.Add(new PointInfo() { Letter = 'E', X = 6, Y = 7 }); // 4
            //points.Add(new PointInfo() { Letter = 'G', X = 8, Y = 7.13 }); // 5
            //points.Add(new PointInfo() { Letter = 'H', X = 8.44, Y = 8.83 }); // 6
            //points.Add(new PointInfo() { Letter = 'L', X = 9.07, Y = 3.36 }); // 7
            //points.Add(new PointInfo() { Letter = 'I', X = 9.40, Y = 5.54 }); // 8

            points.Add(new PointInfo() { Letter = 'C', X = 1.99, Y = 2.78 }); // 0
            points.Add(new PointInfo() { Letter = 'D', X = 2, Y = 7.13 }); // 1
            points.Add(new PointInfo() { Letter = 'E', X = 6, Y = 7 }); // 2
            points.Add(new PointInfo() { Letter = 'F', X = 3.60, Y = 9.69 }); // 3
            points.Add(new PointInfo() { Letter = 'H', X = 8.44, Y = 8.83 }); // 5
            points.Add(new PointInfo() { Letter = 'G', X = 8, Y = 7.13 }); // 4
            points.Add(new PointInfo() { Letter = 'I', X = 9.40, Y = 5.54 }); // 6
            points.Add(new PointInfo() { Letter = 'L', X = 9.07, Y = 3.36 }); // 7
            points.Add(new PointInfo() { Letter = 'M', X = 5.92, Y = 1.52 }); // 8


            //// organisé  par rapport au tracé de la forme -> 9  segments
            //segments.Add(new Segment() { Point1 = points[0], Point2 = points[1] }); // CD -> 0
            //segments.Add(new Segment() { Point1 = points[1], Point2 = points[4] }); // DE -> 1
            //segments.Add(new Segment() { Point1 = points[4], Point2 = points[2] }); // EF -> 2
            //segments.Add(new Segment() { Point1 = points[2], Point2 = points[6] }); // FH -> 3
            //segments.Add(new Segment() { Point1 = points[6], Point2 = points[5] }); // HG -> 4
            //segments.Add(new Segment() { Point1 = points[5], Point2 = points[8] }); // GI -> 5
            //segments.Add(new Segment() { Point1 = points[8], Point2 = points[7] }); // IL -> 6
            //segments.Add(new Segment() { Point1 = points[7], Point2 = points[3] }); // LM -> 7
            //segments.Add(new Segment() { Point1 = points[3], Point2 = points[0] }); // MC -> 8


            CalculateSlopes();

            foreach (var point in points)
            {
               listBoxPoints.Items.Add($"Point : ({point.Letter}, {point.X}, {point.Y}), Slope : {point.slope}");
            }
            //foreach (var segment in segments)
            //{
            //    listBoxPentes.Items.Add($"Segment : {segment.Point1.Letter}{segment.Point2.Letter}, , Pente {segment.Slope}");
            //}


            //DrawPoints();
            //DrawLines();
            RenderDraw();

            AddGridLines();
        }

        private void InitializePlot()
        {
            plotModel = new PlotModel { Title = "Points et lignes" };

            scatterSeries = new ScatterSeries();
            plotModel.Series.Add(scatterSeries);

            lineSeries = new LineSeries();
            plotModel.Series.Add(lineSeries);

            plotView = new PlotView
            {
                Size = new Size(1200, 800),
                Model = plotModel
            };
            Controls.Add(plotView);
        }

        private void AddGridLines()
        {
            plotModel.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Minimum = 1,
                Maximum = 15,
                MajorStep = 0.5,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None
            });

            plotModel.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Minimum = 1,
                Maximum = 15,
                MajorStep = 0.5,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None
            });
        }

        private void DrawPoints()
        {
            scatterSeries.Points.Clear();
            foreach (var point in points)
            {
                scatterSeries.Points.Add(new ScatterPoint(point.X, point.Y));
                scatterSeries.TrackerFormatString = $"Point : {point.Letter} ({point.X}, {point.Y})";
            }
            plotModel.InvalidatePlot(true);
        }

        private void DrawLines()
        {
            lineSeries.Points.Clear();

            for (int i = 0; i < points.Count - 1; i++)
            {
                PointInfo point1 = points[i];
                PointInfo point2 = points[i + 1];

                lineSeries.Points.Add(new DataPoint(point1.X, point1.Y));
                lineSeries.Points.Add(new DataPoint(point2.X, point2.Y));

                lineSeries.Points.Add(new DataPoint(double.NaN, double.NaN));
            }

            plotModel.InvalidatePlot(true);
        }


        private void RenderDraw()
        {
            lineSeries.Points.Clear();

            for (int i = 0; i < points.Count - 1; i++)
            {
                PointInfo point1 = points[i];
                PointInfo point2 = points[i + 1];


                double nextSlope = points[i + 1].slope;

                //if (point2.X < point1.X)
                //{
                //    // Inverser l'ordre des points
                //    PointInfo temp = point1;
                //    point1 = point2;
                //    point2 = temp;
                //}

                InterpolateHermiteSegment(point1, point2, point1.slope, nextSlope);
            }

            plotModel.InvalidatePlot(true);
        }

        private void DrawInterpolationCurve()
        {

            var xValues = new[] { 1.0, 2.0, 3.0, 4.0 };
            var yValues = new[] { 2.0, 3.0, 5.0, 4.0 };
            var slopes = new[] { 0.5, -0.2, 0.7, 0.1 };


            var spline = CubicSpline.InterpolateHermite(xValues, yValues, slopes);


            var interpolatedPoints = new List<DataPoint>();
            for (double x = xValues[0]; x <= xValues[xValues.Length - 1]; x += 0.1)
            {
                double y = spline.Interpolate(x);
                interpolatedPoints.Add(new DataPoint(x, y));
            }

            lineSeries.Points.Clear();
            lineSeries.Points.AddRange(interpolatedPoints);
        }

        private void DebugMessage(string message)
        {
            textBoxOutput.Items.Add(message);
        }

        private void InterpolateHermiteSegment(PointInfo point1, PointInfo point2, double currentSlope, double nextSlope)
        {
            const int numberOfPoints = 100;

            double deltaX = point2.X - point1.X;
            double h = deltaX / numberOfPoints;


            List<DataPoint> interpolatedPoints = new List<DataPoint>();
            for (int i = 0; i <= numberOfPoints; i++)
            {
                double x = point1.X + h * i;
                double t = (x - point1.X) / deltaX; // Normalize


                double l1 = (t - 1) * (t - 1) * (2 * t + 1);
                double l2 = t * t * (-2 * t + 3);
                double l3 = (t - 1) * (t - 1) * t;
                double l4 = t * t * (t - 1);


                double interpolatedY = l1 * point1.Y + l2 * point2.Y + l3 * currentSlope * deltaX + l4 * nextSlope * deltaX;
                interpolatedPoints.Add(new DataPoint(x, interpolatedY));
            }


            for (int i = 0; i < interpolatedPoints.Count - 1; i++)
            {
                lineSeries.Points.Add(interpolatedPoints[i]);
                lineSeries.Points.Add(new DataPoint(double.NaN, double.NaN)); 
            }
        }


        private void CalculateSlopes()
        {

            int n = points.Count;
            for (int i = 0; i < n - 1; i++)
            {
                double deltaY = points[i + 1].Y - points[i].Y;
                double deltaX = points[i + 1].X - points[i].X;
                points[i] = new PointInfo
                {
                    Letter = points[i].Letter,
                    X = points[i].X,
                    Y = points[i].Y,
                    slope = deltaY / deltaX,
                };
            }
            //List<Segment> segmentsTempo = new List<Segment>(segments);
            //for (int i = 0; i < segmentsTempo.Count; i++)
            //{
            //    double deltaY;
            //    double deltaX;
            //    if (segmentsTempo[i].Point1.X < segmentsTempo[i].Point2.X)
            //    {
            //        // point2 in x in segment is before point 1, We inverse, because we want pent from left to right
            //        deltaY = segmentsTempo[i].Point2.Y - segmentsTempo[i].Point1.Y;
            //        deltaX = segmentsTempo[i].Point2.X - segmentsTempo[i].Point1.X;
            //    }
            //    else
            //    {
            //        // It's normal, point2 in x is after point 1
            //        deltaY = segmentsTempo[i].Point1.Y - segmentsTempo[i].Point2.Y;
            //        deltaX = segmentsTempo[i].Point1.X - segmentsTempo[i].Point2.X;
            //    }

            //    double slope = deltaY / deltaX;

            //    if (slope < 100)
            //    {
            //        segments[i] = new Segment
            //        {
            //            Point1 = segments[i].Point1,
            //            Point2 = segments[i].Point2,
            //            Slope = slope
            //        };
            //    }
            //    else
            //    {
            //        segments[i] = new Segment
            //        {
            //            Point1 = segments[i].Point1,
            //            Point2 = segments[i].Point2,
            //            Slope = double.NaN
            //        };
            //    }
            //}
        }

        



        private void start_App_btn_Click(object sender, EventArgs e)
        {
            Point form1Location = this.Location;

            this.Hide();

            Form2 form2 = new Form2();
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = form1Location;

            form2.Opacity = 1;
            form2.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
