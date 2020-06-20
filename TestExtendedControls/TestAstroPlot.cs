using BaseUtils;
using ExtendedControls;
using ExtendedControls.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DialogTest
{
    public partial class TestAstroPlot : Form
    {
        private ThemeStandard theme;
        
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
            extAstroPlotTest.SetAxesCoordinates(extAstroPlotTest.AxesLength);

            // draw the boundaries cube frame
            extAstroPlotTest.SetFrameCoordinates(extAstroPlotTest.FramesRadius);

            DemoCluster();
            //TestOrientation();
        }

        private void DefineDefaults()
        {
            extAstroPlotTest.Distance = 60;
            extAstroPlotTest.Focus = 1000;

            extAstroPlotTest.Elevation = -0.3;
            extAstroPlotTest.Azimuth = -0.3;
            extAstroPlotTest.AxesWidget = true;
            extAstroPlotTest.AxesLength = 10;
            
            extAstroPlotTest.CoordsCenter[0] = 0.0;
            extAstroPlotTest.CoordsCenter[1] = 0.0;
            extAstroPlotTest.CoordsCenter[2] = 0.0;

            extAstroPlotTest.FramesWidget = false;
            extAstroPlotTest.FramesRadius = 14;

            extAstroPlotTest.MouseSensitivity_Wheel = 20;
            extAstroPlotTest.MouseSensitivity_Movement = 200;

            extAstroPlotTest.SmallDotSize = 6;

            this.Width = 600;
            this.Height = 600;
        }

        private class System
        {
            public string Name { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public bool IsVisited { get; set; } = false;
            public bool IsWaypoint { get; set; } = false;
            public bool IsCurrent { get; set; } = false;
        }
                
        private readonly List<System> localSystemsList = new List<System>();
        private readonly List<System> travelSystemsList = new List<System>();
        private readonly List<System> orientationTestList = new List<System>();

        public void PopulateNearestSystems()
        {
            localSystemsList.Add(new System { Name = "Synuefe VR-E c27-6", X = 874.59, Y = -475.22, Z = 119.25, IsVisited = true, IsCurrent = true });
            localSystemsList.Add(new System { Name = "Synuefe VF-D d13-7", X = 887.44, Y = -477.19, Z = 112.34 });
            localSystemsList.Add(new System { Name = "Synuefe VF-D d13-6", X = 875.38, Y = -467.38, Z = 127.72 });
            localSystemsList.Add(new System { Name = "Synuefe OO-J b54-0", X = 872.53, Y = -475.47, Z = 108.81 });
            localSystemsList.Add(new System { Name = "Synuefe RZ-H b55-0", X = 876.50, Y = -464.50, Z = 117.53, IsVisited = true });
            localSystemsList.Add(new System { Name = "Synuefe RZ-H b55-0", X = 869.28, Y = -474.44, Z = 111.72, IsVisited = true });
            localSystemsList.Add(new System { Name = "Synuefe OO-J b54-1", X = 869.28, Y = -474.44, Z = 111.72 });
            localSystemsList.Add(new System { Name = "Synuefe QO-J b54-1", X = 897.25, Y = -472.09, Z = 110.47 });
            localSystemsList.Add(new System { Name = "Synuefe ZX-C c28-8", X = 873.53, Y = -485.00, Z = 139.53, IsVisited = true });
            localSystemsList.Add(new System { Name = "Synuefe PO-J b54-0", X = 879.25, Y = -479.47, Z = 100.03 });
            localSystemsList.Add(new System { Name = "Synuefe VF-D d13-31", X = 860.34, Y = -480.59, Z = 126.47 });
            localSystemsList.Add(new System { Name = "Synuefe ZL-B d14-21", X = 873.78, Y = -483.63, Z = 143.13, IsVisited = true });
            localSystemsList.Add(new System { Name = "Synuefe VF-D d13-15", X = 873.97, Y = -497.22, Z = 108.09 });
            localSystemsList.Add(new System { Name = "Synuefe PO-J b54-1", X = 875.34, Y = -476.88, Z = 99.06 });
            localSystemsList.Add(new System { Name = "Synuefe SZ-H b55-0", X = 900.06, Y = -464.44, Z = 115.22, IsVisited = true });
            localSystemsList.Add(new System { Name = "Synuefe ZL-B d14-26", X = 866.31, Y = -476.22, Z = 139.31 });
        }

        public void PopulateTravelList()
        {
            travelSystemsList.Add(new System { Name = "Fe 1 Sector NI-T d3 - 74", X = 3311.78125, Y = -29.625, Z = 1350.5625, IsCurrent = true });
            travelSystemsList.Add(new System { Name = "Fe 1 Sector XV-M c7-3", X = 3301.03125, Y = -28.75, Z = 1359.53125 });
            travelSystemsList.Add(new System { Name = "Fe 1 Sector MZ-X a30-3", X = 3299.21875, Y = -34.84375, Z = 1354.0625 });
            travelSystemsList.Add(new System { Name = "Swoiphs AF-M a103-4", X = 3244.4375, Y = -33.21875, Z = 1323.25 });
            travelSystemsList.Add(new System { Name = "Sifeae IC-D d12-99", X = 3186.71875, Y = -24.8125, Z = 1302 });
            travelSystemsList.Add(new System { Name = "Swoiphs MS-P a101-0", X = 3177.8125, Y = -32.09375, Z = 1296.40625 });
        }

        public void PopulateOrientationTest()
        {
            orientationTestList.Add(new System { Name = "Sol", X = 0, Y = 0, Z = 0 });
            orientationTestList.Add(new System { Name = "SagA*", X = 25.21875, Y = -20.90625, Z = 25899.96875 });
            orientationTestList.Add(new System { Name = "Beagle Point", X = -1111.5625, Y = -134.21875, Z = 65269.75 });
            orientationTestList.Add(new System { Name = "Colonia", X = -9530.5, Y = -910.28125, Z = 19808.125 });
            orientationTestList.Add(new System { Name = "Shackleton's Star", X = 11170.34375, Y = 6.03125, Z = -16520.875 });
            orientationTestList.Add(new System { Name = "Point Decision", X = -7684.75, Y = 13.46875, Z = -14373.46875 });
            orientationTestList.Add(new System { Name = "Magellan's Star", X = 40503.8125, Y = 25.96875, Z = 17678 });
            orientationTestList.Add(new System { Name = "Star One", X = -35413.03125, Y = -14.75, Z = 3821.46875 });
        }

        public class ContextMenuEntries
        {
            public string Name { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        private readonly List<object[]> contextMenuList = new List<object[]>();

        private void TestOrientation()
        {
            orientationTestList.Clear();
            contextMenuList.Clear();
            extAstroPlotTest.Clear();

            extAstroPlotTest.Distance = 100000;
            extAstroPlotTest.AxesLength = 100000;
            extAstroPlotTest.AxesWidget = true;
            extAstroPlotTest.MouseSensitivity_Wheel = 10;

            PopulateOrientationTest();

            foreach (var item in orientationTestList)
            {
                contextMenuList.Add(new object[] { item.Name, item.X, item.Y, item.Z });
            }

            CreateContextMenu(contextMenuList);

            var centerTo = orientationTestList[0];

            SetCenterSystem(new double[] { centerTo.X, centerTo.Y, centerTo.Z });

            PlotObjects(orientationTestList);
        }

        private void DemoCluster()
        {
            localSystemsList.Clear();
            contextMenuList.Clear();
            extAstroPlotTest.Clear();
                                    
            PopulateNearestSystems();

            foreach (var item in localSystemsList)
            {
                contextMenuList.Add(new object[] { item.Name, item.X, item.Y, item.Z });
            }

            CreateContextMenu(contextMenuList);

            var centerTo = localSystemsList[0];

            SetCenterSystem(new double[] { centerTo.X, centerTo.Y, centerTo.Z });

            PlotObjects(localSystemsList);
        }
        
        private void DemoTravel()
        {
            travelSystemsList.Clear();
            contextMenuList.Clear();
            extAstroPlotTest.Clear();

            PopulateTravelList();

            foreach (var item in travelSystemsList)
            {
                contextMenuList.Add(new object[] { item.Name, item.X, item.Y, item.Z });
            }

            CreateContextMenu(contextMenuList);

            var centerTo = travelSystemsList[0];

            SetCenterSystem(new double[] { centerTo.X, centerTo.Y, centerTo.Z });

            PlotObjects(travelSystemsList);
        }
                
        private void CreateContextMenu(List<object[]> contextMenuList)
        {
            contextMenuStrip.Items.Clear();

            var localItems = new ToolStripMenuItem[contextMenuList.Count];
            for (int i = 0; i < contextMenuList.Count; i++)
            {
                localItems[i] = new ToolStripMenuItem
                {
                    Text = contextMenuList[i][0].ToString(),
                    Name = contextMenuList[i][0].ToString(),
                    Tag = new double[] { (double)contextMenuList[i][1], (double)contextMenuList[i][2], (double)contextMenuList[i][3] }
                };
                localItems[i].Click += TestAstroPlot_Click;
            }
            contextMenuStrip.Items.AddRange(localItems);
        }

        private void PlotObjects(List<System> list)
        {
            extAstroPlotTest.Clear();

            var List = new List<object[]>();

            for (int i = 0; i < list.Count; i++)
            {
                List.Add(new object[] { list[i].Name, list[i].X, list[i].Y, list[i].Z, list[i].IsVisited, list[i].IsWaypoint, list[i].IsCurrent });
            }
            extAstroPlotTest.AddSystemsToMap(List);
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
        }

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

        private void extAstroPlotTest_MouseHover(object sender, EventArgs e)
        {
            toolTip1.ShowAlways = true;
        }
    }
}