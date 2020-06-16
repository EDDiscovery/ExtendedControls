using BaseUtils;
using ExtendedControls;
using ExtendedControls.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            extAstroPlotTest.DrawFrameWidget(extAstroPlotTest.BoundariesRadius);

            //AddDemoStars();           
            //DemoOrrery();
            //DemoCluster();
            //DemoTravel();
        }

        private void DefineDefaults()
        {
            extAstroPlotTest.Distance = 50;
            extAstroPlotTest.Focus = 1000;

            extAstroPlotTest.Elevation = -0.3;
            extAstroPlotTest.Azimuth = -0.3;
            extAstroPlotTest.AxesWidget = true;
            extAstroPlotTest.AxesLength = 10;
            
            extAstroPlotTest.CoordsCenter[0] = 0.0;
            extAstroPlotTest.CoordsCenter[1] = 0.0;
            extAstroPlotTest.CoordsCenter[2] = 0.0;

            extAstroPlotTest.BoundariesWidget = false;
            extAstroPlotTest.BoundariesRadius = 0.9;

            extAstroPlotTest.MouseSensitivity_Wheel = 20;
            extAstroPlotTest.MouseSensitivity_Movement = 200;

            extAstroPlotTest.SmallDotSize = 4;

            this.Width = 600;
            this.Height = 600;
        }


        #region Stars

        private class LocalSystems
        {
            public string Name { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }            
        }

        private readonly List<LocalSystems> localSystemsList = new List<LocalSystems>();

        private class TravelSystems
        {
            public string Name { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        private readonly List<TravelSystems> travelSystemsList = new List<TravelSystems>();

        private void DemoCluster()
        {
            extAstroPlotTest.Clear();
                                    
            PopulateNearestSystems();
            CreateContextMenu(localSystemsList);
                        
            var centerTo = localSystemsList[0];

            SetCenterSystem(new double[] { centerTo.X, centerTo.Y, centerTo.Z });

            DrawSystems();
        }

        private void DemoTravel()
        {
            extAstroPlotTest.Clear();

            PopulateTravelList();
            CreateContextMenu(travelSystemsList);

            var centerTo = travelSystemsList[2];

            SetCenterSystem(new double[] { centerTo.X, centerTo.Y, centerTo.Z });

            DrawTravelMap();
        }

        public void PopulateNearestSystems()
        {
            // create a hardcoded list of systems from DB
            localSystemsList.Add(new LocalSystems { Name = "Synuefe VR-E c27-6", X = 874.59, Y = -475.22, Z = 119.25 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe VF-D d13-7", X = 887.44, Y = -477.19, Z = 112.34 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe VF-D d13-6", X = 875.38, Y = -467.38, Z = 127.72 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe OO-J b54-0", X = 872.53, Y = -475.47, Z = 108.81 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe RZ-H b55-0", X = 876.50, Y = -464.50, Z = 117.53 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe RZ-H b55-0", X = 869.28, Y = -474.44, Z = 111.72 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe OO-J b54-1", X = 869.28, Y = -474.44, Z = 111.72 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe QO-J b54-1", X = 897.25, Y = -472.09, Z = 110.47 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe ZX-C c28-8", X = 873.53, Y = -485.00, Z = 139.53 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe PO-J b54-0", X = 879.25, Y = -479.47, Z = 100.03 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe VF-D d13-31", X = 860.34, Y = -480.59, Z = 126.47 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe ZL-B d14-21", X = 873.78, Y = -483.63, Z = 143.13 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe VF-D d13-15", X = 873.97, Y = -497.22, Z = 108.09 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe PO-J b54-1", X = 875.34, Y = -476.88, Z = 99.06 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe SZ-H b55-0", X = 900.06, Y = -464.44, Z = 115.22 });
            localSystemsList.Add(new LocalSystems { Name = "Synuefe ZL-B d14-26", X = 866.31, Y = -476.22, Z = 139.31 });
        }

        public void PopulateTravelList()
        {
            travelSystemsList.Add(new TravelSystems { Name = "Fe 1 Sector NI-T d3 - 74", X = 3311.78125, Y = -29.625, Z = 1350.5625 });
            travelSystemsList.Add(new TravelSystems { Name = "Fe 1 Sector XV-M c7-3", X = 3301.03125, Y = -28.75, Z = 1359.53125 });
            travelSystemsList.Add(new TravelSystems { Name = "Fe 1 Sector MZ-X a30-3", X = 3299.21875, Y = -34.84375, Z = 1354.0625 });
            travelSystemsList.Add(new TravelSystems { Name = "Swoiphs AF-M a103-4", X = 3244.4375, Y = -33.21875, Z = 1323.25 });
            travelSystemsList.Add(new TravelSystems { Name = "Sifeae IC-D d12-99", X = 3186.71875, Y = -24.8125, Z = 1302 });
            travelSystemsList.Add(new TravelSystems { Name = "Swoiphs MS-P a101-0", X = 3177.8125, Y = -32.09375, Z = 1296.40625 });
        }

        public void DrawSystems()
        {
            extAstroPlotTest.Clear();

            List<double[]> Stars = new List<double[]>();

            for (int i = 0; i < localSystemsList.Count; i++)
            {
                Stars.Add(new double[] { localSystemsList[i].X, localSystemsList[i].Y, localSystemsList[i].Z });
            }
            extAstroPlotTest.AddPointsToMap(Stars);
        }

        public void DrawTravelMap()
        {
            extAstroPlotTest.Clear();

            List<double[]> Travel = new List<double[]>();

            for (int i = 0; i < travelSystemsList.Count; i++)
            {
                Travel.Add(new double[] { travelSystemsList[i].X, travelSystemsList[i].Y, travelSystemsList[i].Z });
            }
            extAstroPlotTest.DrawTravelToMap(Travel);
        }
        
        private void CreateContextMenu(List<LocalSystems> localSystemsList)
        {
            ToolStripMenuItem[] localItems = new ToolStripMenuItem[localSystemsList.Count];
            for (int i = 0; i < localSystemsList.Count; i++)
            {
                localItems[i] = new ToolStripMenuItem
                {
                    Text = localSystemsList[i].Name,
                    Name = localSystemsList[i].Name,
                    Tag = new double[] { localSystemsList[i].X, localSystemsList[i].Y, localSystemsList[i].Z }
                };
                localItems[i].Click += TestAstroPlot_Click;
            }
            contextMenuStrip.Items.AddRange(localItems);
        }

        private void CreateContextMenu(List<TravelSystems> travelSystemsList)
        {
            ToolStripMenuItem[] travelItems = new ToolStripMenuItem[travelSystemsList.Count];
            for (int i = 0; i < travelSystemsList.Count; i++)
            {
                travelItems[i] = new ToolStripMenuItem
                {
                    Text = travelSystemsList[i].Name,
                    Name = travelSystemsList[i].Name,
                    Tag = new double[] { travelSystemsList[i].X, travelSystemsList[i].Y, travelSystemsList[i].Z }
                };
                travelItems[i].Click += TestAstroPlot_Click;
            }
            contextMenuStrip.Items.AddRange(travelItems);
        }
        
        private void TestAstroPlot_Click(object sender, EventArgs e)
        {
            var selected = sender as ToolStripMenuItem;
            
            SetCenterSystem((double[])selected.Tag);
        }                        
        
        private void SetCenterSystem(double[] coords)
        {
            extAstroPlotTest.CoordsCenter = coords;
                        
            extAstroPlotTest.Clear();
            DrawTravelMap();
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
        
        private void DemoOrrery()
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
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip.Show();
                
            }
            if (e.Button == MouseButtons.Middle)
            {

            }
                
        }
                
        private void extButtonLocal_Click(object sender, EventArgs e)
        {
            extAstroPlotTest.Invalidate();
            extAstroPlotTest.Clear();
            DemoCluster();
        }

        private void extButtonTravel_Click(object sender, EventArgs e)
        {
            extAstroPlotTest.Invalidate();
            extAstroPlotTest.Clear();
            DemoTravel();
        }

        private void extButtonOrrery_Click(object sender, EventArgs e)
        {
            extAstroPlotTest.Invalidate();
            extAstroPlotTest.Clear();
            DemoOrrery();
        }

        private void extButtonClear_Click(object sender, EventArgs e)
        {
            extAstroPlotTest.Invalidate();
            extAstroPlotTest.Clear();
        }
    }

    internal class systems
    {
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}