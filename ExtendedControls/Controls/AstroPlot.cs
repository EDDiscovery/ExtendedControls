﻿/*
 * Copyright © 2016 - 2020 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 * 
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */

using ExtendedControls.Controls.AstroPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls.Controls
{
    public partial class ExtAstroPlot : UserControl
    {
        // Axes Widget
        public class Axis
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public PointF Coords { get; set; } = new PointF(0, 0);
        }
        private readonly List<Axis> Axes = new List<Axis>();

        // Frame Widget
        public class Corner
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public PointF Coords { get; set; } = new PointF(0, 0);
        }
        private readonly List<Corner> Frames = new List<Corner>();


        // Map Elements
        public class MapObjects
        {
            public string Name { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public bool IsVisited { get; set; }
            public PointF Coords { get; set; }
            public bool IsWaypoint { get; internal set; }
            public bool IsCurrent { get; internal set; }
        }
        private readonly List<MapObjects> MapSystems = new List<MapObjects>();
                
        private double focalLength = 900;
        private double distance = 6;
        private double[] cameraPosition = new double[3];
        private double[] centerCoordinates = new double[3];

        // Objects
        private int smallDotSize = 8;
        private int mediumDotSize = 12;
        private int largeDotSize = 16;

        // Mouse 
        private bool leftMousePressed = false, rightMousePressed = false, middleMousePressed = false;
        private PointF ptMouseClick;
        private int mouseMovementSens = 150;
        private double mouseWheelSens = 300;
        private Point mousePosition;
        private int hotspotSize = 10;

        // Axes Widget
        private bool drawAxesWidget = true;
        private int axesWidgetThickness = 3;
        private int axesWidgetLength = 50;

        // Boundaries Cube
        private bool drawFramesWidget = true;
        private double framesRadiusWidth = 0.8;
        private int framesWidgetThickness = 1;

        // Azymuth is the horizontal direction expressed as the angular distance between the direction of a fixed point (such as the observer's heading) and the direction of the object
        private double lastAzimuth, azimuth = 0.3;
        // Elevation is the angular distance of something (such as a celestial object) above the horizon
        private double lastElevation, elevation = 0.3;

        private readonly System.Timers.Timer _mouseIdleTimer = new System.Timers.Timer();

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        #region Properties

        [Description("Set the distance at which the camera stands from the plot")]
        public double Distance
        {
            get { return distance; }
            set { distance = (value >= 0.1) ? distance = value : distance; UpdateProjection(); }
        }

        [Description("Focal length of the camera")]
        public new double Focus
        {
            get { return focalLength; }
            set { focalLength = value; UpdateProjection(); }
        }

        [Description("Camera position")]
        public double[] Camera
        {
            get { return cameraPosition; }
            set { cameraPosition = value; UpdateProjection(); }
        }

        [Description("Set the coordinates of the center of the plot")]
        public double[] CoordsCenter
        {
            get { return centerCoordinates; }
            set { centerCoordinates = value; UpdateProjection(); }
        }

        [Description("Horizontal direction of the camera expressed as an angular distance")]
        public double Azimuth
        {
            get { return azimuth; }
            set { azimuth = value; UpdateProjection(); }
        }

        [Description("Vertical direction of the camera expressed as an angular distance")]
        public double Elevation
        {
            get { return elevation; }
            set { elevation = value; UpdateProjection(); }
        }

        [Description("Diameter of the smaller dots")]
        public int SmallDotSize
        {
            get { return smallDotSize; }
            set { smallDotSize = value; UpdateProjection(); }
        }

        [Description("Diameter of the medium dots")]
        public int MediumDotSize
        {
            get { return mediumDotSize; }
            set { mediumDotSize = value; UpdateProjection(); }
        }

        [Description("Diameter of the large dots")]
        public int LargeDotSize
        {
            get { return largeDotSize; }
            set { largeDotSize = value; UpdateProjection(); }
        }

        [Description("Toggle the axes widget display")]
        public bool AxesWidget
        {
            get { return drawAxesWidget; }
            set { drawAxesWidget = value; UpdateProjection(); }
        }

        [Description("Set the thickness of each axis in the axes widget")]
        public int AxesThickness
        {
            get { return axesWidgetThickness; }
            set { axesWidgetThickness = value; UpdateProjection(); }
        }

        [Description("Set the length of each axis in the axes widget")]
        public int AxesLength
        {
            get { return axesWidgetLength; }
            set { axesWidgetLength = value; UpdateProjection(); }
        }

        [Description("Toggle the boundaries frame")]
        public bool FramesWidget
        {
            get { return drawFramesWidget; }
            set { drawFramesWidget = value; UpdateProjection(); }
        }

        [Description("Set the boundaries frame radius")]
        public double FramesRadius
        {
            get { return framesRadiusWidth; }
            set { framesRadiusWidth = value; UpdateProjection(); }
        }

        [Description("Set the boundaries frame thickness")]
        public int FramesThickness
        {
            get { return framesWidgetThickness; }
            set { framesWidgetThickness = value; UpdateProjection(); }
        }

        [Description("Set the sensitivity of the mouse movement")]
        public int MouseSensitivity_Movement
        {
            get { return mouseMovementSens; }
            set { mouseMovementSens = value; UpdateProjection(); }
        }

        [Description("Set the sensisitivy of the mouse wheel")]
        public double MouseSensitivity_Wheel
        {
            get { return mouseWheelSens; }
            set { mouseWheelSens = value; UpdateProjection(); }
        }

        [Description("Define the size of the hotspot area for the map points")]
        public int HotSpotSize
        {
            get { return hotspotSize; }
            set { hotspotSize = value; UpdateProjection(); }
        }
        #endregion

        public ExtAstroPlot()
        {
            InitializeComponent();
            Handlers.MouseWheel.Add(this, OnMouseWheel);

            systemLabel.Text = "";
            systemLabel.Visible = false;

            _mouseIdleTimer.AutoReset = false;
            _mouseIdleTimer.Interval = 300;
            _mouseIdleTimer.Elapsed += MouseIdleTimer_Elapsed;
        }
        
        private void Plot_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            // Pick the foreground color defined in the designer            
            _ = new SolidBrush(ForeColor);

            Pen AxisPen = new Pen(new SolidBrush(ForeColor))
            {
                Width = 1
            };

            Pen FramePen = new Pen(new SolidBrush(ForeColor))
            {
                Width = 1,
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
            };
                        
            Pen TravelMapPen = new Pen(new SolidBrush(ForeColor))
            {
                Width = 1,
                DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
            };

            _ = Color.White;
                        
            // give some love to the renderint engine
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

            // axes
            if (drawAxesWidget && Axes != null)
            {
                for (int i = 0; i < Axes.Count; i++)
                {
                    if (i == 1)
                    {
                        AxisPen.Color = Color.Red;
                        e.Graphics.DrawLine(AxisPen, Axes[0].Coords, Axes[1].Coords);
                    }                    
                    if (i == 2)
                    {
                        AxisPen.Color = Color.Green;
                        e.Graphics.DrawLine(AxisPen, Axes[0].Coords, Axes[2].Coords);
                    }

                    if (i == 3)
                    {
                        AxisPen.Color = Color.Blue;
                        e.Graphics.DrawLine(AxisPen, Axes[0].Coords, Axes[3].Coords);
                    }
                }
            }

            // boundaries
            if (drawFramesWidget && Frames != null)
            {
                // bottom
                e.Graphics.DrawLine(FramePen, Frames[0].Coords, Frames[1].Coords);
                e.Graphics.DrawLine(FramePen, Frames[1].Coords, Frames[5].Coords);
                e.Graphics.DrawLine(FramePen, Frames[5].Coords, Frames[3].Coords);
                e.Graphics.DrawLine(FramePen, Frames[3].Coords, Frames[0].Coords);

                // left
                e.Graphics.DrawLine(FramePen, Frames[0].Coords, Frames[2].Coords);
                e.Graphics.DrawLine(FramePen, Frames[2].Coords, Frames[4].Coords);
                e.Graphics.DrawLine(FramePen, Frames[4].Coords, Frames[3].Coords);

                // right
                e.Graphics.DrawLine(FramePen, Frames[1].Coords, Frames[6].Coords);
                e.Graphics.DrawLine(FramePen, Frames[6].Coords, Frames[7].Coords);
                e.Graphics.DrawLine(FramePen, Frames[7].Coords, Frames[5].Coords);

                // top
                e.Graphics.DrawLine(FramePen, Frames[2].Coords, Frames[6].Coords);
                e.Graphics.DrawLine(FramePen, Frames[4].Coords, Frames[7].Coords);
            }

            if (MapSystems != null)
            {
                for (int i = 0; i < MapSystems.Count; i++)
                {
                    Color Paint;
                    if (MapSystems[i].IsVisited)
                    {
                        if (MapSystems[i].IsCurrent)
                        {
                            Paint = Color.Red;
                        }
                        else
                        {
                            Paint = Color.Aqua;
                        }
                    }
                    else
                    {
                        Paint = Color.Yellow;
                    }

                    e.Graphics.FillEllipse(new SolidBrush(Paint), new RectangleF(
                        MapSystems[i].Coords.X - (SmallDotSize / 2),
                        MapSystems[i].Coords.Y - (SmallDotSize / 2),
                        SmallDotSize,
                        SmallDotSize));

                    if (MapSystems[i].IsWaypoint && i != MapSystems.Count - 1)
                    {
                        e.Graphics.DrawLine(
                           TravelMapPen,
                           MapSystems[i].Coords,
                           MapSystems[i + 1].Coords);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Pick the background color defined in the designer
            var backColor = new SolidBrush(BackColor);

            Graphics g = this.CreateGraphics();
            g.FillRectangle(backColor, new Rectangle(0, 0, this.Width, this.Height));
        }

        #region Widgets
        public void DrawAxesWidget(int length)
        {
            if (drawAxesWidget)
            {
                Axes.Add(new Axis
                {
                    X = 0,
                    Y = 0,
                    Z = 0
                });
                Axes.Add(new Axis
                {
                    X = length,
                    Y = 0,
                    Z = 0
                });
                Axes.Add(new Axis
                {
                    X = 0,
                    Y = length * -1,
                    Z = 0
                });
                Axes.Add(new Axis
                {
                    X = 0,
                    Y = 0,
                    Z = length * -1
                });
                                
                UpdateProjection();
            }
        }

        public void DrawFrameWidget(double frameRadius)
        {
            if (drawFramesWidget)
            {

                Frames.Add(new Corner { X = frameRadius, Y = frameRadius, Z = frameRadius });
                Frames.Add(new Corner { X = frameRadius, Y = frameRadius, Z = -frameRadius });
                Frames.Add(new Corner { X = frameRadius, Y = -frameRadius, Z = frameRadius });
                Frames.Add(new Corner { X = -frameRadius, Y = frameRadius, Z = frameRadius });
                Frames.Add(new Corner { X = -frameRadius, Y = -frameRadius, Z = frameRadius });
                Frames.Add(new Corner { X = -frameRadius, Y = frameRadius, Z = -frameRadius });
                Frames.Add(new Corner { X = frameRadius, Y = -frameRadius, Z = -frameRadius });
                Frames.Add(new Corner { X = -frameRadius, Y = -frameRadius, Z = -frameRadius });
            }
        }
        #endregion

        #region Add objects to map
        public void AddSystemsToMap(List<object[]> mapSystems)
        {
            for (int i = 0; i < mapSystems.Count; i++)
            {
                MapSystems.Add(new MapObjects
                {
                    Name = mapSystems[i][0].ToString(),
                    X = (double)mapSystems[i][1] - centerCoordinates[0],
                    Y = (double)mapSystems[i][2] - centerCoordinates[1],
                    Z = (double)mapSystems[i][3] - centerCoordinates[2],
                    IsVisited = (bool)mapSystems[i][4],
                    IsWaypoint = (bool)mapSystems[i][5],
                    IsCurrent = (bool)mapSystems[i][6],
                    Coords = new PointF(0, 0)
                });
            }
            UpdateProjection();
        }
        #endregion

        #region Projection                
        private void UpdateProjection()
        {
            double x = (distance * Math.Cos(elevation) * Math.Cos(azimuth));
            double y = (distance * Math.Cos(elevation) * Math.Sin(azimuth));
            double z = (distance * Math.Sin(elevation));

            cameraPosition = new double[3] { -y, z, -x };

            if (MapSystems != null)
            {
                AstroPlot.Update.MapSystems(MapSystems, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }

            if (Axes != null)
            {
                AstroPlot.Update.MapAxes(Axes, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }

            if (Frames != null)
            {
                AstroPlot.Update.MapFrames(Frames, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }

            Invalidate();
        }

        public void Clear()
        {
            Invalidate();
            MapSystems.Clear();
        }
        #endregion

        #region Interaction
                
        private void ExtAstroPlot_SizeChanged(object sender, EventArgs e)
        {
            plot.Height = this.Height;
            plot.Width = this.Width;
            plot.Top = this.Top;
            plot.Left = this.Left;
        }

        private void Plot_SizeChanged(object sender, EventArgs e)
        {
                UpdateProjection();
        }
                
        private void Plot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // rotate
                leftMousePressed = true;
                ptMouseClick = new PointF(e.X, e.Y);
                systemLabel.Text = "";
                systemLabel.Visible = false;
                lastAzimuth = azimuth;
                lastElevation = elevation;
            }
        }

        private void Plot_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                leftMousePressed = false;
            if (e.Button == MouseButtons.Middle)
                middleMousePressed = false;
        }
                        
        private void Plot_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftMousePressed)
            {
                azimuth = lastAzimuth - ((ptMouseClick.X - e.X) / 150);
                elevation = lastElevation + ((ptMouseClick.Y - e.Y) / 150);
                UpdateProjection();
            }
            
            mousePosition = e.Location;

            _mouseIdleTimer.Stop();
            _mouseIdleTimer.Start();

            //Debug.WriteLine("Timer started");
        }
        
        private void MouseIdleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Debug.WriteLine("Timer code running...");

            var hs = HotSpotSize;

            var labelPosition = new Point();

            string text = null;
            
            if (MapSystems != null)
            {
                for (int i = 0; i < MapSystems.Count; i++)
                {
                    if (mousePosition.X > (MapSystems[i].Coords.X - hs) && mousePosition.X < (MapSystems[i].Coords.X + hs) &&
                        mousePosition.Y > (MapSystems[i].Coords.Y - hs) && mousePosition.Y > (MapSystems[i].Coords.Y - hs))
                    {
                        text = MapSystems[i].Name;
                        labelPosition = new Point((int)MapSystems[i].Coords.X - (smallDotSize * 4), (int)MapSystems[i].Coords.Y - (smallDotSize * 3));
                    }
                }
            }

            BeginInvoke(
                (Action)(
                    () =>
                    {
                        string currentText = systemLabel.Text;
                        systemLabel.Text = text;
                        systemLabel.Visible = true;
                        systemLabel.Location = labelPosition;                        
                    }
                )
            );
        }

        private void Plot_MouseLeave(object sender, EventArgs e)
        {
            _mouseIdleTimer.Stop();
            //Debug.WriteLine("Timer stopped");
        }
                        
        private new void OnMouseWheel(MouseEventArgs e)
        {
            if (!middleMousePressed)
            {
                systemLabel.Visible = false;

                // zoom
                Distance += -e.Delta / MouseSensitivity_Wheel;
            }
        }
        #endregion
    }
}
