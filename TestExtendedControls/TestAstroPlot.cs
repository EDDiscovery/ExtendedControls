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

namespace DialogTest
{
    public partial class TestAstroPlot : Form
    {
        ThemeStandard theme;

        public TestAstroPlot()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontSize = 8.25f;
            theme.WindowsFrame = true;

            InitializeComponent();
            //AddDemoStars();           
            AddDemoOrrery();

            extAstroPlotTest.Distance = 8;
            extAstroPlotTest.Focus = 1300;
            extAstroPlotTest.Elevation = 0.7;
            extAstroPlotTest.Azimuth = 0.3;
        }

        private void AddDemoStars()
        {
            extAstroPlotTest.Clear();
                        
            Random rand = new Random();
            double R = 1;
            List<double[]> Stars = new List<double[]>();

            for (int j = 0; j < 7; j++)
            {
                for (int i = 0; i < 25; i++)
                {
                    double theta = Math.PI * rand.NextDouble();
                    double phi = 2 * Math.PI * rand.NextDouble();
                    double x = R * Math.Sin(theta) * Math.Cos(phi);
                    double y = R * Math.Sin(theta) * Math.Sin(phi);
                    double z = R * Math.Cos(theta);
                    Stars.Add(new double[] { x, y, z });
                }
                extAstroPlotTest.AddPoints(Stars);

                Stars.Clear();
            }            
        }

        private void AddDemoOrrery()
        {
            List<double[]> Bodies = new List<double[]>();

            Bodies.Add(new double[] { 0.8, 0, 0 });
            Bodies.Add(new double[] { 0.3, 0, 0 });
            Bodies.Add(new double[] { 1.2, 0.1, 0 });
            Bodies.Add(new double[] { 1.5, 0.2, 0 });

            extAstroPlotTest.AddEllipses(Bodies);
        }

        private void extAstroPlotTest_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                extAstroPlotTest.Distance = 8;
                extAstroPlotTest.Focus = 1300;
                extAstroPlotTest.Elevation = 0.7;
                extAstroPlotTest.Azimuth = 0.3;
            }
        }
    }
}


