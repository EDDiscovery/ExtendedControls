using BaseUtils;
using ExtendedControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            DefineDefaults();
            
            // draw the axes widget
            extAstroPlotTest.DrawAxesWidget(extAstroPlotTest.AxesLength);

            // draw the boundaries cube frame
            //extAstroPlotTest.DrawBoundariesWidget(extAstroPlotTest.BoundariesRadius);            

            //AddDemoStars();           
            AddDemoOrrery();

        }

        private void DefineDefaults()
        {            
            extAstroPlotTest.Distance = 9;
            extAstroPlotTest.Focus = 1400;
            extAstroPlotTest.Elevation = -0.3;
            extAstroPlotTest.Azimuth = -0.3;
            extAstroPlotTest.AxesWidget = true;
            extAstroPlotTest.AxesLength = 2;

            extAstroPlotTest.BoundariesRadius = 0.9;

            extAstroPlotTest.MouseSensitivity_Wheel = 250;
            extAstroPlotTest.MouseSensitivity_Movement = 200;

        }

        private void AddDemoStars()
        {
            extAstroPlotTest.Clear();
                        
            Random rand = new Random();
            double R = 1;
            List<double[]> Stars = new List<double[]>();

            for (int j = 0; j < 8; j++)
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

        struct SystemBodies
        {
            public byte Level;
            public string Name;
            public string Class;
            public double Distance;
            public double Inclination;
        }

        private void AddDemoOrrery()
        {
            // Col 285 Sector JX-T d3-74 A 

            // White Main Sequence F star(F1VI)
            //Age: 2,264 my
            //Solar Masses: 1.33
            //Radius: 1.2SR
            //Surface Temp: 7,238K
            //Orbital Period: 36.9 days
            //Semi Major Axis: 0.09AU
            //Orbital Eccentricity: 0.041
            //Orbital Inclination: 8.624°
            //Arg Of Periapsis: 47.460°
            //Absolute Magnitude: 3.45
            //Axial tilt: 0.00°

            //Distance from Arrival Point 140.0ls
            //Surface Temp: 4,207K
            //Orbital Period: 36.9 days
            //Semi Major Axis: 0.18AU
            //Orbital Eccentricity: 0.041
            //Orbital Inclination: 8.624°
            //Arg Of Periapsis: 227.460°
            //Absolute Magnitude: 6.74
            //Axial tilt: 0.00°

            //Distance from Arrival Point 16,926.0ls
            //Surface Temp: 66K
            //Gravity: 1.59g
            //Surface Pressure: 1.11 Atmospheres
            //Volcanism: Water Geysers
            //Orbital Period: 28,090.7 days
            //Semi Major Axis: 22.83AU
            //Orbital Eccentricity: 0.548
            //Orbital Inclination: -74.060°
            //Arg Of Periapsis: 271.141°
            //Axial tilt: -0.26°

            SystemBodies _A1 = new SystemBodies
            {
                Level = 1,
                Distance = 16926,
                Inclination = -74.060
            };

            SystemBodies _B = new SystemBodies
            {
                Level = 0,
                Distance = 140,
                Inclination = 8.624
            };            

            List<double[]> Bodies = new List<double[]>();

            // in this situation, the array of values acts differently, as they aren't coordinates:
            // the first number will indicate the mean distance to the host star, or the orbital radius
            // the second indicate the orbital inclination, or it's elevation from the orbital plane
            //Bodies.Add(new double[] { -0.8, 0.2 });
            Bodies.Add(new double[] { _A1.Distance, _A1.Inclination });
            Bodies.Add(new double[] { _B.Distance, _B.Inclination });

            //Bodies.Add(new double[] { -1.2, 0 });

            extAstroPlotTest.AddBodiesToOrrery(Bodies);
        }

        private void extAstroPlotTest_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {                               
                contextMenuStrip1.Show(e.Location.X, e.Location.Y);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefineDefaults();
        }
    }
}


