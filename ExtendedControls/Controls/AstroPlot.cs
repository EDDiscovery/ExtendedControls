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
                        
        // Map Systems
        private readonly List<object[]> MapVisited = new List<object[]>();
        private readonly List<object[]> MapUnVisited = new List<object[]>();
        private readonly List<PointF[]> MapSystemsVisited = new List<PointF[]>();
        private readonly List<PointF[]> MapSystemsUnVisited = new List<PointF[]>();

        // Travel Map
        private readonly List<object[]> TravelMap = new List<object[]>();
        private readonly List<PointF[]> TravelMapWaypoints = new List<PointF[]>();

        // Tooltips        
        private readonly List<PointF[]> MapTooltips = new List<PointF[]>();

        private double focalLength = 900;
        private double distance = 6;
        private double[] cameraPosition = new double[3];
        private double[] centerCoordinates = new double[3];
                
        // Objects
        private int smallDotSize = 4;
        private int mediumDotSize = 8;
        private int largeDotSize = 12;

        // Mouse 
        private bool leftMousePressed = false, rightMousePressed = false, middleMousePressed = false;
        private PointF ptMouseClick;
        private int mouseMovementSens = 150;
        private double mouseWheelSens = 300;
        private Point mousePosition;

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

        [Description("Diameter of the smaller dots")]
        public int MediumDotSize
        {
            get { return mediumDotSize; }
            set { mediumDotSize = value; UpdateProjection(); }
        }

        [Description("Diameter of the smaller dots")]
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
        #endregion

        public ExtAstroPlot()
        {
            InitializeComponent();
            AstroPlot.MouseWheel.Add(this, OnMouseWheel);

            toolTip.AutoPopDelay = 1000;

            _mouseIdleTimer.AutoReset = false;
            _mouseIdleTimer.Interval = 300;
            _mouseIdleTimer.Elapsed += _mouseIdleTimer_Elapsed;
        }
        
        private void plot_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            // Pick the background color defined in the designer            
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

            Pen OrreryOrbitsPen = new Pen(new SolidBrush(Color.White))
            {
                Width = 1,
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            };

            Pen TravelMapPen = new Pen(new SolidBrush(ForeColor))
            {
                Width = 1,
                DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
            };

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
            if (FrameLines != null)
            {
                if (FrameLines.Count > 0)
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
            }

            if (MapSystemsVisited != null)
            {
                for (int i = 0; i < MapSystemsVisited.Count; i++)
                {
                    foreach (PointF p in MapSystemsVisited[i])
                    {
                        e.Graphics.FillEllipse(new SolidBrush(Color.Blue), new RectangleF(p.X - SmallDotSize / 2, p.Y - SmallDotSize / 2, SmallDotSize, SmallDotSize));
                    }
                }
            }
            if (MapSystemsUnVisited != null)
            {
                for (int i = 0; i < MapSystemsUnVisited.Count; i++)
                {
                    foreach (PointF p in MapSystemsUnVisited[i])
                    {
                        e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), new RectangleF(p.X - SmallDotSize / 2, p.Y - SmallDotSize / 2, SmallDotSize, SmallDotSize));
                    }
                }
            }

            if (TravelMap != null)
            {
                for (int i = 0; i < TravelMapWaypoints.Count; i++)
                {
                    for (int ii = 0; ii < TravelMapWaypoints[i].Length; ii++)
                    {
                        e.Graphics.DrawRectangle(FramePen, new Rectangle((int)MapTooltips[i][ii].X - 5, (int)MapTooltips[i][ii].Y - 5, 10, 10));

                        if (ii == 0)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(Color.Red), new RectangleF(TravelMapWaypoints[i][ii].X - MediumDotSize / 2, TravelMapWaypoints[i][ii].Y - MediumDotSize / 2, MediumDotSize, MediumDotSize));
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(Color.BlueViolet), new RectangleF(TravelMapWaypoints[i][ii].X - MediumDotSize / 2, TravelMapWaypoints[i][ii].Y - MediumDotSize / 2, MediumDotSize, MediumDotSize));
                        }

                            if (ii != TravelMapWaypoints[i].Length - 1)
                                e.Graphics.DrawLine(TravelMapPen, TravelMapWaypoints[i][ii], TravelMapWaypoints[i][ii + 1]);
                            else
                            e.Graphics.DrawLine(TravelMapPen, TravelMapWaypoints[i][ii], TravelMapWaypoints[i][ii]);
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
            AxesAnchors.Add(AstroPlot.UpdateWidgets(AxesCoords, this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
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
            FrameLines.Add(AstroPlot.UpdateWidgets(FrameCorners, this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
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

        #region Map                
        public void AddPointsAndLabels(List<object[]> mapSystems)
        {
            List<object[]> _visited = new List<object[]>();
            List<object[]> _unvisited = new List<object[]>();

            for (int i = 0; i < mapSystems.Count; i++)
            {
                // 0 = Name, 1 = X, 2 = Y, 3 = Z, 4 = visited, 5 = current
                if ((bool)mapSystems[i][4])
                {
                    _visited.Add(new object[] { mapSystems[i][0], (double)mapSystems[i][1] - centerCoordinates[0], (double)mapSystems[i][2] - centerCoordinates[1], (double)mapSystems[i][3] - centerCoordinates[2], mapSystems[i][4] });
                }
                else
                {
                    _unvisited.Add(new object[] { mapSystems[i][0], (double)mapSystems[i][1] - centerCoordinates[0], (double)mapSystems[i][2] - centerCoordinates[1], (double)mapSystems[i][3] - centerCoordinates[2], mapSystems[i][4] });
                }
            }

            MapVisited.AddRange(_visited);
            MapUnVisited.AddRange(_unvisited);

            MapSystemsVisited.Add(AstroPlot.UpdateObjects(MapVisited, Width, Height, focalLength, cameraPosition, azimuth, elevation));
            MapSystemsUnVisited.Add(AstroPlot.UpdateObjects(MapUnVisited, Width, Height, focalLength, cameraPosition, azimuth, elevation));
            UpdateProjection();
        }
                
        public void AddTravelMapPoints(List<object[]> waypoints)
        {
            List<object[]> _travel = new List<object[]>();

            for (int i = 0; i < waypoints.Count; i++)
            {
                _travel.Add(new object[] { waypoints[i][0], (double)waypoints[i][1] - centerCoordinates[0], (double)waypoints[i][2] - centerCoordinates[1], (double)waypoints[i][3] - centerCoordinates[2] });
            }

            TravelMap.AddRange(_travel);

            TravelMapWaypoints.Add(AstroPlot.UpdateObjects(TravelMap, this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            MapTooltips.Add(AstroPlot.UpdateObjects(TravelMap, this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));

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
                        
            if (MapSystemsVisited != null)
            {
                for (int i = 0; i < MapSystemsVisited.Count; i++)
                    MapSystemsVisited[i] = AstroPlot.UpdateObjects(MapVisited, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }

            if (MapSystemsUnVisited != null)
            {
                for (int i = 0; i < MapSystemsUnVisited.Count; i++)
                    MapSystemsUnVisited[i] = AstroPlot.UpdateObjects(MapUnVisited, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }

            if (TravelMapWaypoints != null)
            {
                for (int i = 0; i < TravelMapWaypoints.Count; i++)
                {
                    TravelMapWaypoints[i] = AstroPlot.UpdateObjects(TravelMap, Width, Height, focalLength, cameraPosition, azimuth, elevation);
                    MapTooltips[i] = AstroPlot.UpdateObjects(TravelMap, Width, Height, focalLength, cameraPosition, azimuth, elevation);
                }
            }

            if (AxesAnchors != null && drawAxesWidget)
            {
                for (int i = 0; i < AxesAnchors.Count; i++)
                    AxesAnchors[i] = AstroPlot.UpdateWidgets(AxesCoords, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }

            if (FrameCorners != null && drawBoundariesWidget)
            {
                for (int i = 0; i < FrameLines.Count; i++)
                    FrameLines[i] = AstroPlot.UpdateWidgets(FrameCorners, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }


            Invalidate();
        }

        public void Clear()
        {
            Invalidate();

            MapVisited.Clear();
            MapSystemsVisited.Clear();
            MapUnVisited.Clear();
            MapSystemsUnVisited.Clear();

            TravelMap.Clear();
            TravelMapWaypoints.Clear();

            MapTooltips.Clear();
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

        private void plot_SizeChanged(object sender, EventArgs e)
        {
                UpdateProjection();
        }
                
        private void plot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // rotate
                leftMousePressed = true;
                ptMouseClick = new PointF(e.X, e.Y);
                lastAzimuth = azimuth;
                lastElevation = elevation;
            }
        }

        private void plot_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                leftMousePressed = false;
            if (e.Button == MouseButtons.Middle)
                middleMousePressed = false;
        }

        private string toolTipText;

        private void plot_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void plot_MouseMove(object sender, MouseEventArgs e)
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

            Debug.WriteLine("Timer started");
        }

        private void _mouseIdleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Debug.WriteLine("Timer code running...");

            string text = null;
            if (mousePosition.X < 100 && mousePosition.Y < 100)
                text = "Top Left";
            else if (mousePosition.X < 100 && mousePosition.Y > Height - 100)
                text = "Bottom Left";
            else if (mousePosition.X > Width - 100 && mousePosition.Y < 100)
                text = "Top Right";
            else if (mousePosition.X > Width - 100 && mousePosition.Y > Height - 100)
                text = "Bottom Right";

            foreach (var item in MapTooltips[0])
            {
                Debug.WriteLine(item);
                if (mousePosition.X == item.X && mousePosition.Y == item.Y)
                {
                    text = item.X.ToString();
                    Debug.Write(text);
                }
                    
            }

            BeginInvoke(
                (Action)(
                    () =>
                    {
                        string currentText = toolTip.GetToolTip(this);
                        if (currentText != text)
                        {
                            toolTipText = text;
                            toolTip.SetToolTip(this, text);
                            Debug.WriteLine(text);
                        }
                    }
                )
            );

            //toolTip.SetToolTip(senderObject, toolTipText);
        }

        private void plot_MouseLeave(object sender, EventArgs e)
        {
            _mouseIdleTimer.Stop();
            Debug.WriteLine("Timer stopped");
        }

        private void plot_MouseEnter(object sender, EventArgs e)
        {
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private new void OnMouseWheel(MouseEventArgs e)
        {
            if (!middleMousePressed)
            {
                // zoom
                Distance += -e.Delta / MouseSensitivity_Wheel;
            }
        }
                
        #endregion
    }
}

