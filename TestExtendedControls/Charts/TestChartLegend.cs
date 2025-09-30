using ExtendedControls;
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
    public partial class TestChartLegend : Form
    {
        private ExtendedControls.ExtChart chart { get; set; }
        private ExtendedControls.ExtSafeChart chartdef2 { get; set; }
        ThemeList theme;
        public TestChartLegend()
        {
            InitializeComponent();


            Font f = new Font("Ms sans serif", 10);

            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");


            {
                chart = new ExtendedControls.ExtChart();                                                  // create a chart, to see if it works, may not on all platforms
                chart.Font = f;
                chart.Name = "mostVisited";
                chart.Bounds = new Rectangle(150, 10, 900, 900);

                chart.AddTitle("MV1", "Most Visited");
                chart.SetBorder(5, ChartDashStyle.Dash, Color.Green);

                chart.AddChartArea("ChartArea1", new ElementPosition(0, 0, 80f, 100f));

                chart.SetChartAreaColors(Color.Gray, Color.Purple, 5, ChartDashStyle.Dot);

                chart.SetXAxisInterval(DateTimeIntervalType.Days, 0, IntervalAutoMode.VariableCount);
                chart.SetXAxisMajorGridInterval(2, DateTimeIntervalType.Days);
                chart.SetXAxisMajorGridWidthColor(1, ChartDashStyle.DashDotDot, Color.RoyalBlue);

                chart.SetXAxisMinorGridWidthColor(1, ChartDashStyle.DashDotDot, Color.Purple);
                chart.SetXAxisMinorGridInterval(6, DateTimeIntervalType.Hours);
                chart.SetXAxisLabelColorFont(Color.Red, new Font("Arial", 8));
                chart.SetXAxisFormat("dd/MM/yyyy:HH:mm");

                chart.XCursorShown();
                chart.XCursorSelection();
                chart.SetXCursorInterval(1, DateTimeIntervalType.Minutes);
                chart.SetXCursorColors(Color.IndianRed, Color.Yellow, 5);
                chart.SetXCursorScrollBarColors(Color.Red, Color.Yellow);

                chart.SetYAxisMajorGridWidthColor(1, ChartDashStyle.DashDotDot, Color.DarkBlue);
                //chartdef1.SetYAxisMinorGrid(3, ChartDashStyle.DashDotDot, Color.DarkBlue);
                chart.SetYAxisLabelColorFont(Color.Blue, new Font("Arial", 8));
                chart.SetYAxisFormat("N2");

                //chart.YCursor();
                //chart.YCursorSelection(true,false);

                chart.YAutoScale();
                chart.EnableZoomMouseWheelX();
                chart.ZoomMouseWheelXMinimumInterval = 12.0 / 24;          // date displays are in days, allow down to hours

                chart.SetYCursorColors(Color.IndianRed, Color.Yellow, 5);
                chart.SetYCursorScrollBarColors(Color.Red, Color.Yellow);

                chart.AddLegend("Line", Color.Green, Color.Yellow, new Font("Ms sans serif", 10));

                //chart.SetCustomLegendColumns(new LegendCellColumn() { ColumnType = LegendCellColumnType.SeriesSymbol },
                //    new LegendCellColumn() { Text = "#LEGENDTEXT" }, new LegendCellColumn() { Text = "#CUSTOMPROPERTY(CHECK)" });

                chart.AddSeries("Series1", "ChartArea1", SeriesChartType.Line, Color.Pink);//, legend: "Line");
                //chart.SetCustomSeriesProperty("CHECK", "\U2705");

                chart.AddSeries("Series2", "ChartArea1", SeriesChartType.Line, Color.Yellow);//, legend: "Line");
                //chart.SetCustomSeriesProperty("CHECK", "✔");

                chart.SetCustomLegendItems( new LegendItem("Series 1 \u2714", Color.Pink, ""), new LegendItem("Series 2 \u274c", Color.Yellow, ""));

                chart.SetSeriesDataLabelsColorFont(Color.Purple, new Font("Algerian", 15), Color.Yellow);
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
                                    new Action<ToolStripMenuItem[]>((list) =>
                                    {
                                        list[0].Enabled = list[1].Enabled = chart.IsZoomedX;
                                        list[2].Enabled = false;
                                    })
                                    );


                //Theme.Current.Apply(chart, f);


                chart.CursorPositionChanged += Chart_CursorPositionChanged;

                chart.ReportOnMouseDown((ht, fp, e) =>
                {
                    System.Diagnostics.Debug.WriteLine($"Mouse hit {ht.ChartArea} {fp} {e.Location} {chart.CurrentLegend.Position.X} {chart.CurrentLegend.Position.Y}");
                    PointF? lclick = chart.ReportLegendClickPosition(fp);
                    if (lclick != null)
                    {
                        System.Diagnostics.Debug.WriteLine($".. percentage down {lclick}");
                        int series = lclick.Value.Y < 50 ? 0 : 1;
                        chart.SetSeriesEnabled(!chart.IsSeriesEnabled(series), series);
                        chart.ChangeSetCustomLegendItemsText($"Series {series + 1} " + (chart.IsSeriesEnabled(series) ? "\u2714" : "\u274c"), series);
                    
                    }
                });

                if (true)
                {
                    chart.SetCurrentSeries(0);
                    for (int i = 0; i < 365; i++)
                    {
                        DateTime start = new DateTime(1970, 1, 1);
                        start = start.AddHours(i * 1);
                        chart.AddXY(start, i * 100);
                        if (i % 24 == 0)
                        {
                            chart.SetPointLabelCustomColorBorder(Color.Blue, Color.White, Color.Red, 1, ChartDashStyle.Solid);
                            chart.SetPointMarkerStyle(MarkerStyle.Triangle, Color.Red, 5, Color.Blue, 2);
                        }
                    }
                    chart.SetCurrentSeries(1);
                    for (int i = 0; i < 365; i++)
                    {
                        DateTime start = new DateTime(1970, 1, 1);
                        start = start.AddHours(i * 1);
                        chart.AddXY(start, 100-i * 100);
                        if (i % 24 == 0)
                        {
                            chart.SetPointLabelCustomColorBorder(Color.Blue, Color.White, Color.Red, 1, ChartDashStyle.Solid);
                            chart.SetPointMarkerStyle(MarkerStyle.Triangle, Color.Red, 5, Color.Blue, 2);
                        }
                    }
                }
                this.Controls.Add(chart);

            }
        }

        private void Chart_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            var xcursor = e.ChartArea.CursorX;
            var pos = xcursor.Position;
            if (double.IsNaN(pos))
            {

            }
            else
            {
                DateTime t = DateTime.FromOADate(pos);
                System.Diagnostics.Debug.WriteLine($"{e.Axis.Name} {e.Axis.Minimum}-{e.Axis.Maximum} {e.ChartArea.CursorX.Position} = {t.ToString()}");

                int dpindex = chart.FindIndexOfPoint(pos);

                System.Diagnostics.Debug.WriteLine($".. Associated with data point {dpindex}");
                int dpindex2 = chart.FindIndexOfNearestPoint(pos);
                System.Diagnostics.Debug.WriteLine($".. Associated with nearest data point {dpindex2}");

            }
        }


        private void extButton1_Click(object sender, EventArgs e)
        {
            DateTime t = new DateTime(1970, 1, 3);
            chart.SetXCursorPosition(t);
        }
        private void extButton2_Click(object sender, EventArgs e)
        {
            DateTime t = new DateTime(1970, 1, 4);
            chart.SetXCursorPosition(t);
        }




    }
}
