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
using MathNet.Numerics.Distributions;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography;


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
            public bool isReverse;
        }

        struct Segment
        {
            public PointInfo Point1 { get; set; }
            public PointInfo Point2 { get; set; }
            public double Slope { get; set; }
        }

        private List<Segment> segments = new List<Segment>();
        private List<PointInfo> points = new List<PointInfo>();
        private bool[] isReverse = new bool[11]; // Tableau pour stocker les informations sur le sens des segments

        public void StartFunction()
        {
            // Ajout des points
            points.Add(new PointInfo() { Letter = 'C', X = 9, Y = 30, slope = 12.5, isReverse = false }); // 0
            points.Add(new PointInfo() { Letter = 'D', X = 30, Y = 35, slope = 20 / 6, isReverse = true }); // 1
            points.Add(new PointInfo() { Letter = 'E', X = 18, Y = 48, slope = 2.25, isReverse = false }); // 2
            points.Add(new PointInfo() { Letter = 'F', X = 43, Y = 42, slope = 10, isReverse = true }); // 3
            points.Add(new PointInfo() { Letter = 'H', X = 37, Y = 29, slope = 15, isReverse = false }); // 5
            points.Add(new PointInfo() { Letter = 'G', X = 47, Y = 32, slope = 0, isReverse = false }); // 4
            points.Add(new PointInfo() { Letter = 'I', X = 48, Y = 23, slope = 30, isReverse = true }); // 6
            points.Add(new PointInfo() { Letter = 'L', X = 29, Y = 13, slope = 2.5, isReverse = false }); // 7
            points.Add(new PointInfo() { Letter = 'M', X = 30, Y = 8, slope = 2 + (2 / 3), isReverse = true }); // 8
            points.Add(new PointInfo() { Letter = 'N', X = 13, Y = 10, slope = 10 / 13, isReverse = true }); // 9
            points.Add(new PointInfo() { Letter = 'C', X = 9, Y = 30, slope = 12.5, isReverse = false }); // Repeat the first point to close the loop

            // Initialisation du tableau isReverse
            for (int i = 0; i < 10; i++)
            {
                isReverse[i] = points[i].isReverse;
            }

            // Initialisation de la DataGridView
            dataGridViewPoints.ReadOnly = false;
            CalculateSlopes();

            dataGridViewPoints.ColumnCount = 4;
            dataGridViewPoints.Columns[0].Name = "Letter";
            dataGridViewPoints.Columns[1].Name = "X";
            dataGridViewPoints.Columns[2].Name = "Y";
            dataGridViewPoints.Columns[3].Name = "Slope";

            // Ajout des points à la DataGridView
            foreach (var point in points)
            {
                dataGridViewPoints.Rows.Add(point.Letter, point.X, point.Y, point.slope);
            }

            RenderDraw();
            AddGridLines();
        }

        public Form1()
        {
            InitializeComponent();
            InitializePlot();
            StartFunction();
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

        private void RenderDraw()
        {
            lineSeries.Points.Clear();

            List<DataPoint> allPoints = new List<DataPoint>();

            for (int i = 0; i < points.Count - 1; i++)
            {
                PointInfo point1 = points[i];
                PointInfo point2 = points[i + 1];

                double pt1X = point1.X;
                double pt2X = point2.X;
                double pt1Y = point1.Y;
                double pt2Y = point2.Y;
                double currentSlope = point1.slope;
                double nextSlope = point2.slope;
                int nbrPoints = 1000;

                List<PointF> interpolatedPoints;
                //if (isReverse[i])
                //{
                //}
                //else
                //{
                //}

                interpolatedPoints = CalculateHermiteCurve(pt1X, pt2X, pt1Y, pt2Y, currentSlope, nextSlope, nbrPoints);

                foreach (PointF point in interpolatedPoints)
                {
                    lineSeries.Points.Add(new DataPoint(point.X, point.Y));
                }
            }

            plotModel.InvalidatePlot(true);
        }


        private List<PointF> CalculateHermiteCurve(double pt1X, double pt2X, double pt1Y, double pt2Y, double currentSlope, double nextSlope, int nbrPoints)
        {
            List<PointF> points = new List<PointF>();

            double step = (pt2X - pt1X) / nbrPoints;

            for (int i = 0; i <= nbrPoints; i++)
            {
                double x = pt1X + step * i;
                double interpolatedY = CalculateInterpolatedY(x, pt1X, pt2X, pt1Y, pt2Y, currentSlope, nextSlope);
                points.Add(new PointF((float)x, (float)interpolatedY));
            }

            return points;
        }

        private double CalculateInterpolatedY(double x, double x0, double x1, double y0, double y1, double currentSlope, double nextSlope)
        {
            double t = (x - x0) / (x1 - x0);
            double l1 = (t - 1) * (t - 1) * (2 * t + 1);
            double l2 = t * t * (-2 * t + 3);
            double l3 = (t - 1) * (t - 1) * t;
            double l4 = t * t * (t - 1);
            return l1 * y0 + l2 * y1 + l3 * currentSlope * (x1 - x0) + l4 * nextSlope * (x1 - x0);
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
        }

        private void generate_courbe_Click(object sender, EventArgs e)
        {
            points.Clear(); 

            foreach (DataGridViewRow row in dataGridViewPoints.Rows)
            {
                if (!row.IsNewRow)
                {
                    char letter = Convert.ToChar(row.Cells["Letter"].Value);
                    double x = Convert.ToDouble(row.Cells["X"].Value);
                    double y = Convert.ToDouble(row.Cells["Y"].Value);
                    double slope = Convert.ToDouble(row.Cells["Slope"].Value);

                    points.Add(new PointInfo { Letter = letter, X = x, Y = y, slope = slope });
                }
            }
            RenderDraw();
        }
    }
}