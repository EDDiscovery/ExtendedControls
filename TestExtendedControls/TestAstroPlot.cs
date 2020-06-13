using BaseUtils;
using ExtendedControls;
using ExtendedControls.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
            extAstroPlotTest.DrawBoundariesWidget(extAstroPlotTest.BoundariesRadius);

            //AddDemoStars();           
            //AddDemoOrrery();
            DemoCluster();
        }

        private void DefineDefaults()
        {
            extAstroPlotTest.Distance = 9;
            extAstroPlotTest.Focus = 1400;
            extAstroPlotTest.Elevation = -0.3;
            extAstroPlotTest.Azimuth = -0.3;
            extAstroPlotTest.AxesWidget = false;
            extAstroPlotTest.AxesLength = 2;
            
            extAstroPlotTest.Center[0] = 0.0;
            extAstroPlotTest.Center[1] = 0.0;
            extAstroPlotTest.Center[2] = 0.0;

            extAstroPlotTest.BoundariesWidget = false;
            extAstroPlotTest.BoundariesRadius = 0.9;

            extAstroPlotTest.MouseSensitivity_Wheel = 120;
            extAstroPlotTest.MouseSensitivity_Movement = 200;

            extAstroPlotTest.SmallDotSize = 4;
            
        }

        public class systemsInCluster
        {
            public string Name { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        #region Stars
        private void DemoCluster()
        {
            extAstroPlotTest.Clear();
            PopulateNearestSystems();            
        }

        public void PopulateNearestSystems()
        {
            systemsInCluster sys00 = new systemsInCluster { Name = "Synuefe VR-E c27-6", X = 874.59, Y = -475.22, Z = 119.25 };
            systemsInCluster sys01 = new systemsInCluster { Name = "Synuefe VF-D d13-7", X = 887.44, Y = -477.19, Z = 112.34 };
            systemsInCluster sys02 = new systemsInCluster { Name = "Synuefe VF-D d13-30", X = 875.38, Y = -467.38, Z = 127.72 };
            systemsInCluster sys03 = new systemsInCluster { Name = "Synuefe VF-D d13-6", X = 872.53, Y = -475.47, Z = 108.81 };
            systemsInCluster sys04 = new systemsInCluster { Name = "Synuefe OO-J b54-0", X = 876.50, Y = -464.50, Z = 117.53 };
            systemsInCluster sys05 = new systemsInCluster { Name = "Synuefe RZ-H b55-0", X = 869.28, Y = -474.44, Z = 111.72 };            
            
            double[] centerSystem = new double[3];

            SetCenterSystem(sys00);

            List<double[]> Stars = new List<double[]>();

            //Stars.Add(new double[] { CalculateDistanceX(sys00, centerSystem[0]), CalculateDistanceY(sys00, centerSystem[1]), CalculateDistanceZ(sys00, centerSystem[2]) });
            //Stars.Add(new double[] { CalculateDistanceX(sys01, centerSystem[0]), CalculateDistanceY(sys01, centerSystem[1]), CalculateDistanceZ(sys01, centerSystem[2]) });
            //Stars.Add(new double[] { CalculateDistanceX(sys02, centerSystem[0]), CalculateDistanceY(sys02, centerSystem[1]), CalculateDistanceZ(sys02, centerSystem[2]) });
            //Stars.Add(new double[] { CalculateDistanceX(sys03, centerSystem[0]), CalculateDistanceY(sys03, centerSystem[1]), CalculateDistanceZ(sys03, centerSystem[2]) });
            //Stars.Add(new double[] { CalculateDistanceX(sys04, centerSystem[0]), CalculateDistanceY(sys04, centerSystem[1]), CalculateDistanceZ(sys04, centerSystem[2]) });
            //Stars.Add(new double[] { CalculateDistanceX(sys05, centerSystem[0]), CalculateDistanceY(sys05, centerSystem[1]), CalculateDistanceZ(sys05, centerSystem[2]) });
            Stars.Add(new double[] { sys00.X - extAstroPlotTest.Center[0], sys00.Y - extAstroPlotTest.Center[1], sys00.Z - extAstroPlotTest.Center[2] });
            Stars.Add(new double[] { sys01.X - extAstroPlotTest.Center[0], sys01.Y - extAstroPlotTest.Center[1], sys01.Z - extAstroPlotTest.Center[2] });
            Stars.Add(new double[] { sys02.X - extAstroPlotTest.Center[0], sys02.Y - extAstroPlotTest.Center[1], sys02.Z - extAstroPlotTest.Center[2] });
            Stars.Add(new double[] { sys03.X - extAstroPlotTest.Center[0], sys03.Y - extAstroPlotTest.Center[1], sys03.Z - extAstroPlotTest.Center[2] });

            extAstroPlotTest.AddPointsToMap(Stars); 
        }
                
        private void SetCenterSystem(systemsInCluster system)
        {
            extAstroPlotTest.Center[0] = system.X;
            extAstroPlotTest.Center[1] = system.Y;
            extAstroPlotTest.Center[2] = system.Z;
        }

        
        private double CalculateDistanceX(systemsInCluster sys, double c)
        {
            var dx = c - sys.X;            
            return dx / 20;
        }

        private double CalculateDistanceY(systemsInCluster sys, double c)
        {
            var dy = c - sys.Y;
            return dy / 20;
        }

        private double CalculateDistanceZ(systemsInCluster sys, double c)
        {
            var dz = c - sys.Z;
            return dz / 20;
        }

        #endregion

        #region Points
        private void AddDemoStars()
        {
            extAstroPlotTest.Clear();
                        
            Random rand = new Random();
            double R = 1;
            List<double[]> Stars = new List<double[]>();

            for (int j = 0; j < 5; j++)
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
        #endregion

        #region Orrery
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
            extAstroPlotTest.Clear();

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
                Distance = 22.83, // semi major axis
                Inclination = -74.060
            };

            SystemBodies _B = new SystemBodies
            {
                Level = 0,
                Distance = 0.18, // semi major axis
                Inclination = 8.624
            };            

            List<double[]> Bodies = new List<double[]>();

            // in this situation, the array of values acts differently, as they aren't coordinates:
            // the first number will indicate the mean distance to the host star, or the orbital radius
            // the second indicate the orbital inclination, or it's elevation from the orbital plane
            //Bodies.Add(new double[] { -0.8, 0.2 });
            Bodies.Add(new double[] { _A1.Distance, _A1.Inclination });

            List<double[]> Centers = new List<double[]>();
            Centers.Add(new double[] { _B.Distance, _B.Inclination });

            //Bodies.Add(new double[] { -1.2, 0 });

            extAstroPlotTest.AddBodiesToOrrery(Bodies);
            extAstroPlotTest.AddMassCentersToOrrery(Centers);
        }
        #endregion

        private void extAstroPlotTest_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //contextMenuStrip1.Show(e.Location.X, e.Location.Y);                
                
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefineDefaults();
        }
    }
}


