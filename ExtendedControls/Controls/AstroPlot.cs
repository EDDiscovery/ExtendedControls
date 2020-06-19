/*
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
        // points used to draw and orient the axes widget
        private readonly List<double[]> AxesCoords = new List<double[]>();
        private readonly List<PointF[]> AxesAnchors = new List<PointF[]>();

        // points used for the boundaries cube
        private readonly List<double[]> FrameCorners = new List<double[]>();
        private readonly List<PointF[]> FrameLines = new List<PointF[]>();

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
        
        // Widgets
        public class MapWidgets
        {
            public bool Axes { get; set; }
            public bool Frame { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        private readonly List<MapWidgets> Widgets = new List<MapWidgets>();
                        
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
        private bool drawBoundariesWidget = true;
        private double boundariesRadiusWidth = 0.8;
        private int boundariesWidgetThickness = 1;

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
        public bool BoundariesWidget
        {
            get { return drawBoundariesWidget; }
            set { drawBoundariesWidget = value; UpdateProjection(); }
        }

        [Description("Set the boundaries frame radius")]
        public double BoundariesRadius
        {
            get { return boundariesRadiusWidth; }
            set { boundariesRadiusWidth = value; UpdateProjection(); }
        }

        [Description("Set the boundaries frame thickness")]
        public int BoundariesFrameThickness
        {
            get { return boundariesWidgetThickness; }
            set { boundariesWidgetThickness = value; UpdateProjection(); }
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
            AstroPlot.Handlers.MouseWheel.Add(this, OnMouseWheel);

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

            var hs = HotSpotSize;

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

            Color Paint = Color.White;

            // center point            
            var center = new PointF((int)(this.Width / 2), (int)(this.Height / 2));

            // give some love to the renderint engine
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

            // axes
            if (AxesAnchors != null)
            {
                for (int i = 0; i < AxesAnchors.Count; i++)
                {
                    for (int c = 0; c < AxesAnchors[i].Length; c++)
                    {
                        PointF p = AxesAnchors[i][c];
                        if (c == 0) // x axis
                        {
                            AxisPen.Color = Color.Red;
                        }
                        if (c == 1) // y axys
                        {
                            AxisPen.Color = Color.Green;
                        }
                        if (c == 2) // x axis
                        {
                            AxisPen.Color = Color.Blue;
                        }

                        var axisAnchorPoint = new PointF(p.X, p.Y);
                        e.Graphics.DrawLine(AxisPen, center, axisAnchorPoint);
                    }
                }
            }

            // boundaries
            if (FrameLines?.Count > 0)
            {
                // bottom
                e.Graphics.DrawLine(FramePen, FrameLines[0][0], FrameLines[0][1]);
                e.Graphics.DrawLine(FramePen, FrameLines[0][1], FrameLines[0][5]);
                e.Graphics.DrawLine(FramePen, FrameLines[0][5], FrameLines[0][3]);
                e.Graphics.DrawLine(FramePen, FrameLines[0][3], FrameLines[0][0]);

                // left
                e.Graphics.DrawLine(FramePen, FrameLines[0][0], FrameLines[0][2]);
                e.Graphics.DrawLine(FramePen, FrameLines[0][2], FrameLines[0][4]);
                e.Graphics.DrawLine(FramePen, FrameLines[0][4], FrameLines[0][3]);

                // right
                e.Graphics.DrawLine(FramePen, FrameLines[0][1], FrameLines[0][6]);
                e.Graphics.DrawLine(FramePen, FrameLines[0][6], FrameLines[0][7]);
                e.Graphics.DrawLine(FramePen, FrameLines[0][7], FrameLines[0][5]);

                // top
                e.Graphics.DrawLine(FramePen, FrameLines[0][2], FrameLines[0][6]);
                e.Graphics.DrawLine(FramePen, FrameLines[0][4], FrameLines[0][7]);
            }

            if (MapSystems != null)
            {
                for (int i = 0; i < MapSystems.Count; i++)
                {
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
        private void AddAxesAnchors(List<double[]> anchors)
        {
            List<double[]> _anchors = new List<double[]>(anchors);
            AxesCoords.AddRange(_anchors);
            AxesAnchors.Add(AstroPlot.Update.Widgets(AxesCoords, this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            UpdateProjection();
        }

        public void DrawAxesWidget(int length)
        {
            if (drawAxesWidget)
            {
                List<double[]> Coords = new List<double[]>
                {
                    new double[] { length * 0.5, 0.0, 0.0, 0 },
                    new double[] { 0.0, -(length * 0.5), 0.0, 1 },
                    new double[] { 0.0, 0.0, -(length * 0.5), 2 }
                };

                // draw the anchors points
                AddAxesAnchors(Coords);

                Coords.Clear();
            }
        }

        private void AddFrameCorners(List<double[]> corners)
        {
            List<double[]> _corners = new List<double[]>(corners);
            FrameCorners.AddRange(_corners);
            FrameLines.Add(AstroPlot.Update.Widgets(FrameCorners, this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            UpdateProjection();
        }

        public void DrawFrameWidget(double frameRadius)
        {
            if (drawBoundariesWidget)
            {
                List<double[]> Corners = new List<double[]>
                {
                    new double[] { frameRadius, frameRadius, frameRadius },
                    new double[] { frameRadius, frameRadius, -frameRadius },
                    new double[] { frameRadius, -frameRadius, frameRadius },
                    new double[] { -frameRadius, frameRadius, frameRadius },
                    new double[] { -frameRadius, -frameRadius, frameRadius },
                    new double[] { -frameRadius, frameRadius, -frameRadius },
                    new double[] { frameRadius, -frameRadius, -frameRadius },
                    new double[] { -frameRadius, -frameRadius, -frameRadius }
                };

                AddFrameCorners(Corners);

                Corners.Clear();
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
                                    
            if (AxesAnchors != null && drawAxesWidget)
            {
                for (int i = 0; i < AxesAnchors.Count; i++)
                    AxesAnchors[i] = AstroPlot.Update.Widgets(AxesCoords, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }

            if (FrameCorners != null && drawBoundariesWidget)
            {
                for (int i = 0; i < FrameLines.Count; i++)
                    FrameLines[i] = AstroPlot.Update.Widgets(FrameCorners, Width, Height, focalLength, cameraPosition, azimuth, elevation);
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
