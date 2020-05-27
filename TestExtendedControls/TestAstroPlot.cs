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
            
            AddDemoStars();           
            //AddDemoOrrery();

            // define default projection view
            extAstroPlotTest.Distance = 8;
            extAstroPlotTest.Focus = 1300;
            extAstroPlotTest.Elevation = -0.7;
            extAstroPlotTest.Azimuth = 0.4;

            extAstroPlotTest.AxesWidget = true;
            extAstroPlotTest.AxesLength = 3;

            extAstroPlotTest.MouseSensitivity_Wheel = 250;
            extAstroPlotTest.MouseSensitivity_Movement = 200;

            // draw the axes widget
            extAstroPlotTest.DrawAxesWidget(extAstroPlotTest.AxesLength);

            // draw the boundaries cube frame
            extAstroPlotTest.DrawBoundariesWidget(1);
        }

        private void AddDemoStars()
        {
            extAstroPlotTest.Clear();
                        
            Random rand = new Random();
            double R = 1;
            List<double[]> Stars = new List<double[]>();

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 25; i++)
                {
                    double theta = Math.PI * rand.NextDouble();
                    double phi = 2 * Math.PI * rand.NextDouble();

                    // Spherical distribution
                    //double x = R * Math.Sin(theta) * Math.Cos(phi);
                    //double y = R * Math.Sin(theta) * Math.Sin(phi);
                    //double z = R * Math.Cos(theta);

                    // Volume distribution
                    double x = R * Math.Sin(theta) * Math.Sin(phi);
                    double y = R * Math.Sin(theta) * Math.Cos(phi);
                    double z = R * Math.Cos(theta) * Math.Sin(phi);

                    Stars.Add(new double[] { x, y, z });
                }
                extAstroPlotTest.AddPointsToMap(Stars);

                Stars.Clear();
            }            
        }

        private void AddDemoOrrery()
        {
            List<double[]> Bodies = new List<double[]>();

            // in this situation, the arrya of values act differently, as they aren't coordinates:
            // the first number will indicate the mean distance to the host star, or the orbital radius
            // the second indicate the orbital inclination, or it's elevation from the orbital plane
            Bodies.Add(new double[] { -0.8, 0.2, -0.8 });

            Bodies.Add(new double[] { -1.2, 0, -1.2 });

            extAstroPlotTest.AddBodiesToOrrery(Bodies);
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


