using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestExtendedControls
{
    public partial class TestChart : Form
    {
        private ExtendedControls.ExtChart chart { get; set; }
        private ExtendedControls.ExtSafeChart chartdef2 { get; set; }
        public TestChart()
        {
            InitializeComponent();

            Font f = new Font("Ms sans serif", 10);

            {
                chart1.Titles.Add(new Title("Test", Docking.Top, f, Color.Red));        // Can add multiple
                chart1.Legends.Add(new Legend("L1name"));

                ChartArea chartArea2 = new ChartArea();
                chartArea2.Name = "ChartArea2";
                chartArea2.AxisX.LabelStyle.ForeColor = Color.RosyBrown;
                chartArea2.AxisY.LabelStyle.ForeColor = Color.RoyalBlue;
                chartArea2.AxisX.MajorGrid.LineColor = Color.Green;
                chartArea2.AxisY.MajorGrid.LineColor = Color.Green;
                chartArea2.AxisX.MinorGrid.LineColor = Color.Blue;
                chartArea2.AxisY.MinorGrid.LineColor = Color.Blue;
                chartArea2.BorderColor = Color.Purple;
                chartArea2.BorderDashStyle = ChartDashStyle.Dash;
                chartArea2.BorderWidth = 2;
                chartArea2.BackColor = Color.Gray;

                chartArea2.CursorX.IsUserEnabled = true;
                chartArea2.CursorX.IsUserSelectionEnabled = true;
                chartArea2.CursorX.AutoScroll = true;

                chartArea2.CursorX.AutoScroll = true;
                chartArea2.AxisX.ScaleView.Zoomable = true;
                //  chartArea2.AxisX.ScaleView.Size = 5;
                chartArea2.AxisX.LabelStyle.Format = "g"; // ?
                chartArea2.AxisX.LabelStyle.Enabled = true;
                //  chartArea2.AxisX.ScrollBar.Enabled = true;
                chartArea2.AxisX.ScaleView.SizeType = DateTimeIntervalType.Number;

                //     chartArea2.AxisX.ScaleView.Zoom(2,4);


                chart1.ChartAreas.Add(chartArea2);

                Series series2 = new Series();
                series2.Name = "Series2t";
                series2.ChartArea = "ChartArea2";
                series2.Color = Color.Red;

                int[] datavalues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int i = 0;
                foreach (var data in datavalues)        // display 10
                {
                    series2.Points.Add(new DataPoint(i, data));
                    series2.Points[i].AxisLabel = i.ToString();
                    series2.Points[i].LabelForeColor = Color.AliceBlue;
                    i++;
                }

                chart1.Series.Add(series2);
            }

            {
                chart = new ExtendedControls.ExtChart();                                                  // create a chart, to see if it works, may not on all platforms
                chart.Font = f;
                chart.Name = "mostVisited";
                chart.Bounds = new Rectangle(10, 10, 900, 900);

                chart.AddTitle("Most Visited", Docking.Top, Color.Red, new Font("Arial", 15));
                chart.SetBorder(5, ChartDashStyle.Dash, Color.Green);

                chart.AddChartArea("ChartArea1");
                chart.SetChartAreaColors(Color.Gray, Color.Purple, 5, ChartDashStyle.Dot);

                //chartdef1.SetXAxisInterval(28, IntervalAutoMode.VariableCount);
                chart.SetXAxisInterval(28, IntervalAutoMode.FixedCount);
                chart.SetXAxisMajorGrid(1, ChartDashStyle.DashDotDot, Color.RoyalBlue);
                //chartdef1.SetXAxisMinorGrid(1, ChartDashStyle.DashDotDot, Color.AliceBlue);
                chart.SetXAxisLabelColorFont(Color.Red, new Font("Arial", 8));
                chart.SetXAxisFormat("MM-yyyy");
                chart.XCursor();
                chart.XCursorSelection();
                chart.SetXCursorColors(Color.IndianRed, Color.Yellow, 5);
                chart.SetXCursorScrollBarColors(Color.Red, Color.Yellow);


                chart.SetYAxisMajorGrid(1, ChartDashStyle.DashDotDot, Color.DarkBlue);
                //chartdef1.SetYAxisMinorGrid(3, ChartDashStyle.DashDotDot, Color.DarkBlue);
                chart.SetYAxisLabelColorFont(Color.Blue, new Font("Arial", 8));
                chart.SetYAxisFormat("N2");

                //chart.YCursor();
                //chart.YCursorSelection(true,false);

                chart.YAutoScale();
                chart.EnableZoomMouseWheelX();

                chart.SetYCursorColors(Color.IndianRed, Color.Yellow, 5);
                chart.SetYCursorScrollBarColors(Color.Red, Color.Yellow);


                chart.AddLegend(Color.Green, Color.Yellow, new Font("Ms sans serif", 7));

                chart.AddSeries("Series1", "ChartArea1", SeriesChartType.Line, Color.Pink);
                chart.SetSeriesDataLabels(Color.Yellow, new Font("Algerian", 15), Color.Purple);
                chart.SetSeriesDataLabelsBorder(Color.Green, 5, ChartDashStyle.Dot);
                chart.ShowSeriesMarkers(MarkerStyle.Diamond);
                chart.SetSeriesMarkersColorSize(Color.Goldenrod, 2, Color.Yellow, 2);

                chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;


                
                chart.AddContextMenu(new string[] { "Zoom out by 1", "Reset Zoom", "Test disable" },
                                    new Action<ToolStripMenuItem>[]
                                        { new Action<ToolStripMenuItem>((s)=> { chart.ZoomOutX(); } ),
                                          new Action<ToolStripMenuItem>((s)=> { chart.ZoomResetX(); } ),
                                          new Action<ToolStripMenuItem>((s)=> { } ),
                                        },
                                    new Action<ToolStripMenuItem[]>((list) => {
                                        list[0].Enabled = list[1].Enabled = chart.IsZoomedX;
                                        list[2].Enabled = false; } )
                                    );

                if (false)
                {
                    int[] datavalues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                    DateTime[] datevalues = new DateTime[] {
                        new DateTime(1970,1,1),new DateTime(1981,1,1),new DateTime(1982,1,1),new DateTime(1983,1,1),
                        new DateTime(1984,1,1),new DateTime(1985,1,1),new DateTime(1986,1,1),new DateTime(1987,1,1),
                        new DateTime(1988,1,1),new DateTime(1989,1,1),new DateTime(1990,1,1),new DateTime(1991,1,1),
                    };

                    for (int i = 0; i < datavalues.Length; i++)
                    {
                        chart.AddXY(datevalues[i], datavalues[i]);
                        if (i == 2)
                        {
                            chart.SetPointLabelCustomColorBorder(Color.Blue, Color.White, Color.Red, 5, ChartDashStyle.Solid);
                            chart.SetPointMarkerStyle(MarkerStyle.Triangle, Color.Red, 20, Color.Blue, 3);
                        }
                    }
                }

                if (true)
                {
                    for (int i = 0; i < 365; i++)
                    {
                        DateTime start = new DateTime(1970, 1, 1);
                        start = start.AddDays(i * 2);
                        chart.AddXY(start, i *100);
                        if (i == 2)
                        {
                            chart.SetPointLabelCustomColorBorder(Color.Blue, Color.White, Color.Red, 1, ChartDashStyle.Solid);
                            chart.SetPointMarkerStyle(MarkerStyle.Triangle, Color.Red, 20, Color.Blue, 3);
                        }
                    }
                }
                this.Controls.Add(chart);

            }
        }

        //   make this installable
        //       is there a way of getting y values in display range

        //private void Chartdef1_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    ExtendedControls.ExtChart chart = sender as ExtendedControls.ExtChart;
        //    ChartArea c0 = chart.ChartAreas[0];
        //    Axis ax = chart.ChartAreas[0].AxisX;
        //    Axis ay = chart.ChartAreas[0].AxisY;


        //    //System.Diagnostics.Debug.WriteLine($"Mouse {e.Location} Chart0 {c0.Position.Size} {c0.Position.X} {c0.Position.Y}");
        //    //RectangleF c0pos = new RectangleF(c0.Position.X * chart.Width/100, c0.Position.Y * chart.Height/100, c0.Position.Width * chart.Width / 100, c0.Position.Height * chart.Height / 100);
        //    //PointF offset = new PointF((float)e.Location.X / chart.Width, (float)e.Location.Y / chart.Height);
        //    //PointF mrelpos = new PointF(e.Location.X - c0pos.X, e.Location.Y - c0pos.Y);
        //    //System.Diagnostics.Debug.WriteLine($"Chart is at {c0pos} offset into control {offset} offset in graph {mrelpos}");

        //    //System.Diagnostics.Debug.WriteLine($"{}");

        //    double xpos = c0.AxisX.PixelPositionToValue(e.Location.X);
        //    double ypos = c0.AxisY.PixelPositionToValue(e.Location.Y);

        //    // System.Diagnostics.Debug.WriteLine($"Axis x {ax.Maximum} {ax.Minimum} | {ax.ScaleView.Position} {ax.ScaleView.Size} | {ax.ScaleView.ViewMinimum} {ax.ScaleView.ViewMaximum}");
            
        //    double size = ax.ScaleView.ViewMaximum - ax.ScaleView.ViewMinimum;
        //    System.Diagnostics.Debug.WriteLine($"Zoom {ax.ScaleView.ViewMinimum} {ax.ScaleView.ViewMaximum} = {ax.ScaleView.ViewMaximum - ax.ScaleView.ViewMinimum}");
        //    System.Diagnostics.Debug.WriteLine($"  Axis y {ay.Maximum} {ay.Minimum} | {ax.ScaleView.Position} {ay.ScaleView.Size} |{ay.ScaleView.ViewMinimum} {ay.ScaleView.ViewMaximum}");

        //    if (e.Delta > 0)
        //    {
        //        size /= 1.2;

        //        //double size = double.IsNaN(ax.ScaleView.Size) ? (ax.Maximum - ax.Minimum) / 1.2 : ax.ScaleView.Size / 1.2;
        //        if (size > 2)
        //        {
        //            ax.ScaleView.Zoom(xpos - size / 2, xpos + size / 2);
        //        }
        //    }
        //    else
        //    {
        //        if ( ax.ScaleView.IsZoomed )
        //        {
        //            size *= 1.2;
        //            if (size > ax.Maximum - ax.Minimum)
        //                ax.ScaleView.ZoomReset(0);
        //            else
        //            {
        //                if (xpos + size / 2 > ax.Maximum)
        //                    ax.ScaleView.Zoom(ax.Maximum - size, ax.Maximum);
        //                else if (xpos - size / 2 < ax.Minimum)
        //                    ax.ScaleView.Zoom(ax.Minimum, ax.Minimum + size);
        //                else
        //                    ax.ScaleView.Zoom(xpos - size / 2, xpos + size / 2);
        //            }
        //        }

        //    }

        //  //  ay.ScaleView.Zoom(minmax.Item1, minmax.Item2);
        //}




    }
}
