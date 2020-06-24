using BaseUtils;
using ExtendedControls;
using ExtendedControls.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DialogTest
{
    public partial class TestAstroPlot : Form
    {
        private readonly ThemeStandard theme;

        public TestAstroPlot()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontSize = 8.25f;
            theme.WindowsFrame = true;

            InitializeComponent();
        }

        // Timer
        private readonly System.Timers.Timer _mouseIdleTimer = new System.Timers.Timer(); //add _mouseIdleTimer.Dispose(); to the Dispose method on another file.

        private void TestAstroPlot_Load(object sender, EventArgs e)
        {
            astroPlot.AxesWidget = true;
            astroPlot.FramesWidget = false;

            TestOrientation();
        }

        private class TestSystem
        {
            public string Name { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public bool IsVisited { get; set; } = false;
            public bool IsWaypoint { get; set; } = false;
            public bool IsCurrent { get; set; } = false;
        }

        private readonly List<TestSystem> testSystemsList = new List<TestSystem>();

        public void PopulateOrientationTest()
        {
            testSystemsList.Add(new TestSystem { Name = "Sol", X = 0, Y = 0, Z = 0 });
            testSystemsList.Add(new TestSystem { Name = "SagA*", X = 25.21875, Y = -20.90625, Z = 25899.96875 });
            testSystemsList.Add(new TestSystem { Name = "Beagle Point", X = -1111.5625, Y = -134.21875, Z = 65269.75 });
            testSystemsList.Add(new TestSystem { Name = "Colonia", X = -9530.5, Y = -910.28125, Z = 19808.125 });
            testSystemsList.Add(new TestSystem { Name = "Shackleton's Star", X = 11170.34375, Y = 6.03125, Z = -16520.875 });
            testSystemsList.Add(new TestSystem { Name = "Point Decision", X = -7684.75, Y = 13.46875, Z = -14373.46875 });
            testSystemsList.Add(new TestSystem { Name = "Magellan's Star", X = 40503.8125, Y = 25.96875, Z = 17678 });
            testSystemsList.Add(new TestSystem { Name = "Star One", X = -35413.03125, Y = -14.75, Z = 3821.46875 });
        }
        
        private void TestOrientation()
        {
            testSystemsList.Clear();
            astroPlot.Clear();

            astroPlot.Distance = 100000;
            astroPlot.AxesLength = 100000;
            astroPlot.MouseSensitivity_Wheel = 50;

            PopulateOrientationTest();

            var centerTo = testSystemsList[1];
                    
            astroPlot.SetCenterOfMap(new double[] { centerTo.X, centerTo.Y, centerTo.Z });

            PlotObjects(testSystemsList);
        }

        private void PlotObjects(List<TestSystem> list)
        {
            var List = new List<object[]>();

            for (int i = 0; i < list.Count; i++)
            {
                List.Add(new object[] { list[i].Name, list[i].X, list[i].Y, list[i].Z, list[i].IsVisited, list[i].IsWaypoint, list[i].IsCurrent });
            }
            astroPlot.AddSystemsToMap(List);
        }
                
        private void TestAstroPlot_MouseEnter(object sender, EventArgs e)
        {
            _mouseIdleTimer.Start();

#if DEBUG
            Debug.WriteLine("Form timer started");
#endif
        }

        private void TestAstroPlot_MouseLeave(object sender, EventArgs e)
        {
            _mouseIdleTimer.Stop();

#if DEBUG
            Debug.WriteLine("Form timer stopped");
#endif
        }
    }
}
