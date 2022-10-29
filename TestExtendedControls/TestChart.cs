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
        private ExtendedControls.ExtChart chartdef1 { get; set; }
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
                chartdef1 = new ExtendedControls.ExtChart();                                                  // create a chart, to see if it works, may not on all platforms
                chartdef1.Font = f;
                chartdef1.Name = "mostVisited";
                chartdef1.Bounds = new Rectangle(10, 10, 900, 900);

                chartdef1.AddTitle("Most Visited", Color.Red, new Font("Arial", 15));
                chartdef1.SetBorder(Color.Green, 5, ChartDashStyle.Dash);

                var ca = chartdef1.AddChartArea("ChartArea1", Color.Gray, Color.Purple, 5, ChartDashStyle.Dot);

                chartdef1.SetXAxisMajorGrid(Color.RoyalBlue, 3, ChartDashStyle.DashDotDot);
                chartdef1.SetXAxisMinorGrid(Color.RoyalBlue, 3, ChartDashStyle.DashDotDot);
                chartdef1.SetXAxisLabelColorFontFormat(Color.Red, new Font("Brush Script MT", 12), "MM/yyyy");

                chartdef1.SetYAxisMajorGrid(Color.DarkBlue, 3, ChartDashStyle.DashDotDot);
                chartdef1.SetYAxisMinorGrid(Color.DarkBlue, 3, ChartDashStyle.DashDotDot);
                chartdef1.SetYAxisLabelColorFontFormat(Color.Blue, new Font("Brush Script MT", 12), "N2");

                chartdef1.EnableXCursor(Color.IndianRed, 5, Color.Yellow, Color.Red, Color.Yellow);

                chartdef1.AddLegend(Color.Green, Color.Yellow, new Font("Ms sans serif", 7));

                chartdef1.AddSeries("Series1", "ChartArea1", SeriesChartType.Line, Color.Pink);
                chartdef1.ShowSeriesDataLabels(Color.Yellow, new Font("Algerian", 15), Color.Purple);
                chartdef1.ShowSeriesDataLabelsBorder(Color.Green, 5, ChartDashStyle.Dot);
                chartdef1.ShowSeriesMarkers(MarkerStyle.Diamond, Color.Goldenrod, 16, Color.Yellow, 5);

                if (false)
                {
                    int[] datavalues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

                    int i = 0;
                    foreach (var data in datavalues)        // display 10
                    {
                        //   chartdef1.AddPoint(new DataPoint(i, data));
                        if (i == 2)
                        {
                            // chartdef1.SetPointDataLabelCustomColorBorder(Color.Blue, Color.White, Color.Red, 5, ChartDashStyle.Solid);
                        }
                        i++;
                    }
                }

                if (true)
                {
                    chartdef1.SetXAxisInterval(365);
                    int[] datavalues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                    DateTime[] datevalues = new DateTime[] {
                        new DateTime(1970,1,1),new DateTime(1981,1,1),new DateTime(1982,1,1),new DateTime(1983,1,1),
                        new DateTime(1984,1,1),new DateTime(1985,1,1),new DateTime(1986,1,1),new DateTime(1987,1,1),
                        new DateTime(1988,1,1),new DateTime(1989,1,1),new DateTime(1990,1,1),new DateTime(1991,1,1),
                    };

                    for (int i = 0; i < datavalues.Length; i++)
                    {
                        chartdef1.AddXY(datevalues[i], datavalues[i]);
                        if (i == 2)
                        {
                            chartdef1.SetPointDataLabelCustomColorBorder(Color.Blue, Color.White, Color.Red, 5, ChartDashStyle.Solid);
                            chartdef1.SetPointMarkerStyle(MarkerStyle.Triangle, Color.Red, 20, Color.Blue, 3);
                        }
                    }
                }


                this.Controls.Add(chartdef1);
            }

            {
                chartdef2 = new ExtendedControls.ExtSafeChart();                                                  // create a chart, to see if it works, may not on all platforms
                chartdef2.Font = f;
                chartdef2.Name = "mostVisited";
                chartdef2.Bounds = new Rectangle(920, 500, 400, 400);

                chartdef2.AddTitle("Most Visited", Color.Red, new Font("Arial", 8));
                chartdef2.SetBorder(Color.Green, 5, ChartDashStyle.Dash);

                chartdef2.AddChartArea("ChartArea1", Color.Gray, Color.Purple, 5, ChartDashStyle.Dot);

                chartdef2.SetXAxisMajorGrid(Color.RoyalBlue, 3, ChartDashStyle.DashDotDot);
                chartdef2.SetXAxisMinorGrid(Color.RoyalBlue, 3, ChartDashStyle.DashDotDot);
                chartdef2.SetXAxisLabelColorFontFormat(Color.Red, new Font("Brush Script MT", 8), "MM/yyyy");

                chartdef2.SetYAxisMajorGrid(Color.DarkBlue, 3, ChartDashStyle.DashDotDot);
                chartdef2.SetYAxisMinorGrid(Color.DarkBlue, 3, ChartDashStyle.DashDotDot);
                chartdef2.SetYAxisLabelColorFontFormat(Color.Blue, new Font("Brush Script MT", 8), "N2");

                chartdef2.EnableXCursor(Color.IndianRed, 5, Color.Yellow, Color.Red, Color.Yellow);

                chartdef2.AddLegend(Color.Green, Color.Yellow, new Font("Ms sans serif", 7));

                chartdef2.AddSeries("Series1", "ChartArea1", SeriesChartType.Line, Color.Pink);
                chartdef2.ShowSeriesDataLabels(Color.Yellow, new Font("Algerian",8), Color.Purple);
                chartdef2.ShowSeriesDataLabelsBorder(Color.Green, 5, ChartDashStyle.Dot);
                chartdef2.ShowSeriesMarkers(MarkerStyle.Diamond, Color.Goldenrod, 16, Color.Yellow, 5);

                if (true)
                {
                    chartdef2.SetXAxisInterval(365);
                    int[] datavalues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                    DateTime[] datevalues = new DateTime[] {
                        new DateTime(1970,1,1),new DateTime(1981,1,1),new DateTime(1982,1,1),new DateTime(1983,1,1),
                        new DateTime(1984,1,1),new DateTime(1985,1,1),new DateTime(1986,1,1),new DateTime(1987,1,1),
                        new DateTime(1988,1,1),new DateTime(1989,1,1),new DateTime(1990,1,1),new DateTime(1991,1,1),
                    };

                    for (int i = 0; i < datavalues.Length; i++)
                    {
                        chartdef2.AddXY(datevalues[i], datavalues[i]);
                        if (i == 2)
                        {
                            chartdef2.SetPointDataLabelCustomColorBorder(Color.Blue, Color.White, Color.Red, 5, ChartDashStyle.Solid);
                            chartdef2.SetPointMarkerStyle(MarkerStyle.Triangle, Color.Red, 20, Color.Blue, 3);
                        }
                    }
                }


                this.Controls.Add(chartdef2);
            }



        }

    }
}
