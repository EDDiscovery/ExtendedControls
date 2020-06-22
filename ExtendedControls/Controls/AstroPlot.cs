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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtendedControls.Controls
{
    public partial class AstroPlot : Control
    {
        private readonly List<Axis> Axes = new List<Axis>();
        private readonly List<Corner> Frames = new List<Corner>();
        private readonly List<PlotObjects> MapObjects = new List<PlotObjects>();

        // Projection
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
        private double mouseWheelSens = 100;
        private Point mousePosition;
        private int hotspotSize = 10;

        // Axes Widget
        private bool drawAxesWidget = true;
        private int axesWidgetThickness = 3;
        private int axesWidgetLength = 10;

        // Frame Widget
        private bool drawFramesWidget = true;
        private double framesRadiusWidth = 20;
        private int framesWidgetThickness = 1;

        // Output
        private string selectedObjectName;
        private PointF selectedObjectCoords;
        
        // Azymuth is the horizontal direction expressed as the angular distance between the direction of a fixed point (such as the observer's heading) and the direction of the object
        private double lastAzimuth, azimuth = 0.3;

        // Elevation is the angular distance of something (such as a celestial object) above the horizon
        private double lastElevation, elevation = 0.3;

        // Timer
        private readonly System.Timers.Timer _mouseIdleTimer = new System.Timers.Timer(); //add _mouseIdleTimer.Dispose(); to the Dispose method on another file.

        // Avoid flickering during redraw
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        
        #region Public Interface
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

        public Color VisitedColor { get; set; } = Color.Aqua;

        public Color UnVisitedColor { get; set; } = Color.Yellow;

        public Color CurrentColor { get; set; } = Color.Red;

        public string SelectedObjectName
        {
            get { return selectedObjectName; }
            set { selectedObjectName = value; }
        }
        public PointF SelectedObjectCoords
        {
            get { return selectedObjectCoords; }
            set { selectedObjectCoords = value; }
        }

        #endregion

        public AstroPlot()
        {
            InitializeComponent();
            Handlers.MouseWheel.Add(this, OnMouseWheel);

            selectedObjectName = "";
            
            _mouseIdleTimer.AutoReset = false;
            _mouseIdleTimer.Interval = 300;
            _mouseIdleTimer.Elapsed += MouseIdleTimer_Elapsed;

            if (drawAxesWidget)
                SetAxesCoordinates(this.axesWidgetLength);

            if (drawFramesWidget)
                SetFrameCoordinates(this.framesRadiusWidth);
        }

        private void PlotCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // rotate
                leftMousePressed = true;
                ptMouseClick = new PointF(e.X, e.Y);
                selectedObjectName = "";
                lastAzimuth = azimuth;
                lastElevation = elevation;
            }
            if (e.Button == MouseButtons.Middle)
            {
                middleMousePressed = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                rightMousePressed = true;
            }
        }

        private void PlotCanvas_MouseLeave(object sender, EventArgs e)
        {
            _mouseIdleTimer.Stop();
#if DEBUG
            Debug.WriteLine("Timer stopped");
#endif
        }

        private void PlotCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftMousePressed)
            {
                azimuth = lastAzimuth - ((ptMouseClick.X - e.X) / mouseMovementSens);
                elevation = lastElevation + ((ptMouseClick.Y - e.Y) / mouseMovementSens);
                UpdateProjection();
            }

            mousePosition = e.Location;

            _mouseIdleTimer.Stop();
            _mouseIdleTimer.Start();

#if DEBUG
            Debug.WriteLine("Timer started");
#endif
        }

        private void PlotCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                leftMousePressed = false;
            if (e.Button == MouseButtons.Middle)
                middleMousePressed = false;
            if (e.Button == MouseButtons.Right)
                rightMousePressed = false;
        }

        public void AddSystemsToMap(List<object[]> mapObjects)
        {
            for (int i = 0; i < mapObjects.Count; i++)
            {
                MapObjects.Add(new PlotObjects
                {
                    Name = mapObjects[i][0].ToString(),
                    X = (double)mapObjects[i][1] - centerCoordinates[0],
                    Y = (double)mapObjects[i][2] - centerCoordinates[1],
                    Z = (double)mapObjects[i][3] - centerCoordinates[2],
                    IsVisited = (bool)mapObjects[i][4],
                    IsWaypoint = (bool)mapObjects[i][5],
                    IsCurrent = (bool)mapObjects[i][6],
                    Coords = new PointF(0, 0)
                });
            }
            UpdateProjection();
        }

        public void Clear()
        {
            Invalidate();
            MapObjects.Clear();
        }

        private void UpdateProjection()
        {
            var x = (distance * Math.Cos(elevation) * Math.Cos(azimuth));
            var y = (distance * Math.Cos(elevation) * Math.Sin(azimuth));
            var z = (distance * Math.Sin(elevation));

            cameraPosition = new double[3] { -y, z, -x };

            if (MapObjects != null)
            {
                Update.PlotObjects(MapObjects, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }

            if (drawAxesWidget && Axes != null)
            {
                Update.AxesWidget(Axes, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }

            if (drawFramesWidget && Frames != null)
            {
                Update.FrameWidget(Frames, Width, Height, focalLength, cameraPosition, azimuth, elevation);
            }

            Invalidate();
        }

        private void PlotCanvas_Resize(object sender, EventArgs e)
        {
            UpdateProjection();
        }

        private void PlotCanvas_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            // give some love to the renderint engine
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

            using (var AxisPen = new Pen(new SolidBrush(ForeColor))
            {
                Width = 1
            })
            {
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
            }

            using (var FramePen = new Pen(new SolidBrush(ForeColor))
            {
                Width = 1,
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
            })
            {
                // Frame
                if (drawFramesWidget && Frames.Count > 0)
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

                if (MapObjects != null)
                {
                    for (int i = MapObjects.Count - 1; i >= 0; i--)
                    {
                        var FillColor = MapObjects[i].IsVisited ? MapObjects[i].IsCurrent ? CurrentColor : VisitedColor : UnVisitedColor;

                        using (var objectBrush = new SolidBrush(FillColor))
                        {
                            e.Graphics.FillEllipse(objectBrush, new RectangleF(
                                MapObjects[i].Coords.X - (SmallDotSize / 2),
                                MapObjects[i].Coords.Y - (SmallDotSize / 2),
                                SmallDotSize,
                                SmallDotSize));
                        }

                        if (MapObjects[i].IsWaypoint && i != MapObjects.Count - 1)
                        {
                            using (var TravelMapPen = new Pen(new SolidBrush(ForeColor))
                            {
                                Width = 1,
                                DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
                            })
                            {
                                e.Graphics.DrawLine(TravelMapPen, MapObjects[i].Coords, MapObjects[i + 1].Coords);
                            }
                        }
                    }
                }
            }
        }
                
        #region Interaction               

        internal void Plot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // rotate
                leftMousePressed = true;
                ptMouseClick = new PointF(e.X, e.Y);
                selectedObjectName = "";
                lastAzimuth = azimuth;
                lastElevation = elevation;
            }

            if (e.Button == MouseButtons.Middle)
            {
                middleMousePressed = true;
            }

            if (e.Button == MouseButtons.Right)
            {
                rightMousePressed = true;
            }
        }

        private void MouseIdleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
#if DEBUG
            Debug.WriteLine("Timer code running...");
#endif
            var hs = HotSpotSize;
            var text = "";
            var coords = new PointF();

            if (MapObjects != null)
            {
                for (int i = 0; i < MapObjects.Count; i++)
                {
                    if (mousePosition.X > (MapObjects[i].Coords.X - hs) && mousePosition.X < (MapObjects[i].Coords.X + hs) &&
                        mousePosition.Y > (MapObjects[i].Coords.Y - hs) && mousePosition.Y > (MapObjects[i].Coords.Y - hs))
                    {
                        text = MapObjects[i].Name;
                        coords = MapObjects[i].Coords;
                    }
                }
            }

            BeginInvoke(
                (Action)(
                    () =>
                    {
                        var currentText = selectedObjectName;
                        selectedObjectName = text;
                        selectedObjectCoords = coords;
                    }
                )
            );
        }

        private new void OnMouseWheel(MouseEventArgs e)
        {
            if (!middleMousePressed)
            {
                // zoom
                if (MouseSensitivity_Wheel != 0)
                {
                    Distance += -e.Delta * MouseSensitivity_Wheel;
                }
                else
                {
                    Distance += -e.Delta;
                }
            }
        }
        #endregion
    }
}
