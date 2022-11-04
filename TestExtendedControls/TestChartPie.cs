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
    public partial class TestChartPie : Form
    {
        private ExtendedControls.ExtChart chart { get; set; }
        private ExtendedControls.ExtSafeChart chartdef2 { get; set; }
        ThemeList theme;
        public TestChartPie()
        {
            InitializeComponent();


            Font f = new Font("Ms sans serif", 10);

            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");


            {
                chart = new ExtChart();                                                  // create a chart, to see if it works, may not on all platforms
                chart.Font = f;
                chart.Name = "pie";
                chart.Bounds = new Rectangle(150, 10, 1200, 900);

                chart.AddTitle("Pie1", Docking.Top, Color.Red, new Font("Arial", 15), alignment: ContentAlignment.MiddleLeft, position: new ElementPosition(5, 0, 100, 5));
                chart.AddTitle("Pie2", Docking.Top, Color.Red, new Font("Arial", 15), alignment: ContentAlignment.MiddleLeft, position: new ElementPosition(50, 0, 100, 5));
                chart.SetBorder(5, ChartDashStyle.Solid, Color.Green);

                var l1 = chart.AddLegend("LPie1", Color.Red, position: new ElementPosition(5, 5, 10, 20));
                chart.SetLegendShadowOffset(20);
                chart.SetLegendColor(Color.Blue, Color.FromArgb(128, 0, 0, 0));
                var l2 = chart.AddLegend("LPie2", Color.Red, position: new ElementPosition(85, 5, 10, 20));
                chart.SetLegendShadowOffset(20);
                chart.SetLegendColor(Color.Blue, Color.FromArgb(128, 0, 0, 0));

                var ca1 = chart.AddChartArea("Pie1", new ElementPosition(5, 5, 40, 90));

                chart.SetChartAreaColors(Color.Gray, Color.Purple, 2, ChartDashStyle.Solid);

                var series1 = chart.AddSeries("C1", "Pie1", SeriesChartType.Pie, legend:"LPie1");
                chart.SetSeriesShadowOffset(10);
                chart.SetSeriesDataLabelsColorFont(Color.White, new Font("Algerian", 15), Color.DarkBlue);
                chart.SetSeriesDataLabelsBorder(Color.Black, 1, ChartDashStyle.Solid);

                var ca2 = chart.AddChartArea("Pie2", new ElementPosition(50, 5, 45, 90));
                //ca2.AlignWithChartArea = "Pie1";
                //ca2.AlignmentOrientation = AreaAlignmentOrientations.Horizontal;
                //ca2.AlignmentStyle = AreaAlignmentStyles.All;

                chart.SetChartAreaColors(Color.Gray, Color.Purple, 2, ChartDashStyle.Solid);
                chart.SetChartArea3DStyle(new ChartArea3DStyle() { Inclination = 10, Enable3D = true, Rotation = -90, LightStyle = LightStyle.Simplistic});

                var series2 = chart.AddSeries("C2", "Pie2", SeriesChartType.Pie, legend:"LPie2");
                chart.SetSeriesColor(Color.Black, Color.FromArgb(128, 0, 0, 0));
                chart.SetSeriesDataLabelsColorFont(Color.White, new Font("Arial", 15), Color.DarkBlue);
                chart.SetSeriesDataLabelsBorder(Color.Black, 1, ChartDashStyle.Solid);

             //    Theme.Current.Apply(chart, f);

                chart.ReportOnMouseDown(MouseDownEvent);
                    
                chart.CursorPositionChanged += Chart_CursorPositionChanged;

                chart.SetCurrentSeries("C1");
                Color[] colors = new Color[] { Color.Red, Color.Green, Color.Blue, Color.DarkCyan, Color.Magenta, Color.Brown };

                for (int i = 0; i < 5; i++)
                {
                    var dp = chart.AddPoint(i + 1, $"Pie1 {i + 1}");
                    //  dp.LabelForeColor = Color.White;
                    dp.Color = colors[i];
                }

                chart.SetCurrentSeries("C2");

                for (int i = 0; i < 6; i++)
                {
                    var dp = chart.AddPoint(i + 1, $"Pie2 {i + 1}");
                    //  dp.LabelForeColor = Color.White;
                    dp.Color = colors[i];
                }

                this.Controls.Add(chart);

            }
        }

        private void MouseDownEvent(HitTestResult hittest)
        {
            System.Diagnostics.Debug.WriteLine($"Mouse down on {hittest.ChartElementType}");
            if ( hittest.ChartElementType == ChartElementType.DataPoint)
            {
                System.Diagnostics.Debug.WriteLine($".. {hittest.ChartArea.Name} : DP {hittest.PointIndex}");

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
