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
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Linq;
using System.Drawing.Drawing2D;
using BaseUtils;
using EDDiscovery.Controls;
using System.Security.Cryptography;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ExtendedControls.Controls
{
    public partial class AstroPlot : Control
    {
        private ExtPictureBox plotCanvas;
        private ContextMenuStrip contextMenuStrip;
        private IContainer components;
        private ExtLabel extPlotLabel;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.plotCanvas = new ExtendedControls.ExtPictureBox();
            this.extPlotLabel = new ExtendedControls.ExtLabel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.plotCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // plotCanvas
            // 
            this.plotCanvas.BackColor = System.Drawing.Color.Black;
            this.plotCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotCanvas.Location = new System.Drawing.Point(0, 0);
            this.plotCanvas.Name = "plotCanvas";
            this.plotCanvas.Size = new System.Drawing.Size(400, 400);
            this.plotCanvas.TabIndex = 0;
            this.plotCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.PlotCanvas_Paint);
            this.plotCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlotCanvas_MouseClick);
            this.plotCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlotCanvas_MouseDown);
            this.plotCanvas.MouseLeave += new System.EventHandler(this.PlotCanvas_MouseLeave);
            this.plotCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PlotCanvas_MouseMove);
            this.plotCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlotCanvas_MouseUp);
            this.plotCanvas.Resize += new System.EventHandler(this.PlotCanvas_Resize);
            // 
            // extPlotLabel
            // 
            this.extPlotLabel.AutoSize = true;
            this.extPlotLabel.BackColor = System.Drawing.Color.Black;
            this.extPlotLabel.ForeColor = System.Drawing.Color.White;
            this.extPlotLabel.Location = new System.Drawing.Point(8, 8);
            this.extPlotLabel.Name = "extPlotLabel";
            this.extPlotLabel.Size = new System.Drawing.Size(0, 13);
            this.extPlotLabel.TabIndex = 1;
            this.extPlotLabel.TextBackColor = System.Drawing.Color.Black;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // AstroPlot
            // 
            this.Controls.Add(this.extPlotLabel);
            this.Controls.Add(this.plotCanvas);
            this.ForeColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(400, 400);
            ((System.ComponentModel.ISupportInitialize)(this.plotCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private readonly List<AnchorPoint> Axes = new List<AnchorPoint>();
        private readonly List<AnchorPoint> GridHp = new List<AnchorPoint>();
        private readonly List<AnchorPoint> GridHm = new List<AnchorPoint>();

        private readonly List<AnchorPoint[]> Grids = new List<AnchorPoint[]>();

        private readonly List<AnchorPoint> Planes = new List<AnchorPoint>();
        private readonly List<AnchorPoint> Frames = new List<AnchorPoint>();
        private readonly List<PlotObjects> MapObjects = new List<PlotObjects>();

        // Projection
        internal double[] cameraPosition = new double[3];
        internal double[] centerCoordinates = new double[3];
        
        internal double lateralDrag;
        internal double longitudinalDrag;
        internal double[] dragCoords = new double[2];

        public double[] GetCenterCoordinates()
        {
            return centerCoordinates;
        }

        public void SetCenterCoordinates(double[] value)
        { centerCoordinates = value; SetFrameAnchors(FramesLength); SetAxesAnchors(AxesLength); SetGridAnchors(GridCount, GridUnit); }

        private double distance;
        [Description("Set the distance at which the camera stands from the plot")]
        public double Distance
        {
            get => distance; set { distance = (value >= 0.1) ? distance = value : distance; UpdateProjection(); }
        }

        internal double lastAzimuth, azimuth;
        [Description("Horizontal position of the camera expressed as an angular distance")]
        public double Azimuth
        {
            get => azimuth; set { azimuth = value; UpdateProjection(); }
        }

        internal double lastElevation, elevation;
        [Description("Vertical position of the camera expressed as an angular distance")]
        public double Elevation
        {
            get => elevation; set { elevation = value; UpdateProjection(); }
        }

        private double focalLength;
        [Description("Focal length of the camera")]
        public new double Focus
        {
            get => focalLength; set { focalLength = value; UpdateProjection(); }
        }

        // Look'n'Feel
        public int SmallDotSize { get; set; }
        public int MediumDotSize { get; set; }
        public int LargeDotSize { get; set; }
        public Color VisitedColor { get; set; }
        public Color UnVisitedColor { get; set; }
        public Color CurrentColor { get; set; }
        
        // Mouse
        private bool leftMousePressed = false;
        private bool rightMousePressed = false;
        private bool middleMousePressed = false;

        internal PointF ptMouseClick;
        internal Point mousePosition;
        internal Point lastMousePosition;

        private int mouseSensitivity;
        [Description("Set the sensitivity of the mouse movement")]
        public int Mouse_Sensitivity
        {
            get => mouseSensitivity; set { mouseSensitivity = value;}
        }

        private double mouseWheelResistance;
        [Description("Set the resistance of the mouse wheel")]
        public double MouseWheel_Resistance
        {
            get => mouseWheelResistance; set { mouseWheelResistance = value;}
        }

        private double mouseWheelMultiply;
        [Description("Set the multiply of the mouse wheel")]
        public double MouseWheel_Multiply
        {
            get => mouseWheelMultiply; set { mouseWheelMultiply = value; }
        }

        private double mouseDragSensitivity;
        public double MouseDragSensitivity
        {
            get => mouseDragSensitivity; set { mouseDragSensitivity = value; }
        }

        private int hotspotSize;
        [Description("Define the size of the hotspot area for the map points")]
        public int HotSpotSize
        {
            get => hotspotSize; set { hotspotSize = value; UpdateProjection(); }
        }
                
        // Axes Widget
        private bool showAxesWidget;
        public bool ShowAxesWidget
        {
            get => showAxesWidget; set { showAxesWidget = value; UpdateProjection(); }
        }

        private int axesThickness;
        [Description("Set the thickness of each axis in the axes widget")]
        public int AxesThickness
        {
            get => axesThickness; set { axesThickness = value; UpdateProjection(); }
        }

        private int axesLength;
        [Description("Set the length of each axis in the axes widget")]
        public int AxesLength
        {
            get => axesLength; set { axesLength = value; SetAxesAnchors(AxesLength); UpdateProjection(); }
        }

        // Grid Widget
        private bool showGridWidget;
        public bool ShowGridWidget
        {
            get => showGridWidget; set { showGridWidget = value; UpdateProjection(); }
        }

        private int gridUnit;
        public int GridUnit
        {
            get => gridUnit; set { gridUnit = value; UpdateProjection(); }
        }

        private int gridCount;
        public int GridCount
        {
            get => gridCount; set { gridCount = value; UpdateProjection(); }
        }

        private Color gridColor;
        public Color GridColor
        {
            get => gridColor; set { gridColor = value; UpdateProjection(); }
        }
        
        // Frame Widget
        private bool showFrameWidget;
        public bool ShowFrameWidget
        {
            get => showFrameWidget; set { showFrameWidget = value; UpdateProjection(); }
        }

        public enum Shape { Cube = 0, Sphere = 1 };

        public Shape FrameShape;
        
        public Shape GetFrameShape()
        {
            return FrameShape;
        }
        public void SetFrameShape(Shape value)
        {
            ShowFrameWidget = false;
            FrameShape = value;
            ShowFrameWidget = true;
        }

        private double framesLength;
        [Description("Set the boundaries frame radius")]
        public double FramesLength
        {
            get => framesLength; set { framesLength = value; SetFrameAnchors(FramesLength); UpdateProjection(); }
        }

        private int framesThickness;
        [Description("Set the boundaries frame thickness")]
        public int FramesThickness
        {
            get => framesThickness; set { framesThickness = value; UpdateProjection(); }
        }

        // Output

        protected bool isObjectSelected;
        public bool IsObjectSelected
        {
            get => isObjectSelected; set { isObjectSelected = value; }
        }

        protected string selectedObjectName;
        public string SelectedObjectName
        {
            get => selectedObjectName; private set { selectedObjectName = value; }
        }

        protected Point selectedObjectPoint;
        public Point SelectedObjectPoint
        {
            get => selectedObjectPoint; private set { selectedObjectPoint = value; }
        }

        protected double[] selectedObjectCoords;
        
        public double[] SelectedObjectCoords
        {
            get => selectedObjectCoords; private set { selectedObjectCoords = value; }
        }

        // Timer
        private readonly System.Timers.Timer _mouseIdleTimer = new System.Timers.Timer(); //add _mouseIdleTimer.Dispose(); to the Dispose method on another file.

        private const int WS_EX_COMPOSITED = 0x02000000;
        private const int WS_EX_TRANSPARENT = 0x20;
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_COMPOSITED;    // Turn on double buffering
                cp.ExStyle |= WS_EX_TRANSPARENT;    // Turn on transparencies
                return cp;
            }
        }
                        
        protected override void OnHandleCreated(EventArgs e)
        {
            MouseWheel_Multiply = 2;
            MouseDragSensitivity = 5;
            MouseWheel_Resistance = 100;
            ShowAxesWidget = true;
            AxesLength = 10;
            AxesThickness = 1;
            ShowFrameWidget = true;
            FrameShape = Shape.Cube;
            FramesLength = 20;
            FramesThickness = 1;
            ShowGridWidget = true;
            GridCount = 5;
            GridUnit = 10;
            Distance = 300;
            Focus = 1000;
            Azimuth = -0.4;
            Elevation = -0.3;
            SmallDotSize = 5;
            MediumDotSize = 10;
            LargeDotSize = 15;
            VisitedColor = Color.Aqua;
            UnVisitedColor = Color.Yellow;
            CurrentColor = Color.Red;
            GridColor = Color.FromArgb(80, 30, 190, 240);
            base.OnHandleCreated(e);
        }

        public AstroPlot()
        {
            InitializeComponent();

            selectedObjectName = "";
            isObjectSelected = false;

            _mouseIdleTimer.AutoReset = false;
            _mouseIdleTimer.Interval = 100;
            _mouseIdleTimer.Elapsed += MouseIdleTimer_Elapsed;
        }

        public void SetCenterOfMap(double[] coords)
        {
            if (coords != null)
            {
                centerCoordinates = new double[] { coords[0], coords[1], coords[2] };
                SetAxesAnchors(AxesLength);
                SetFrameAnchors(FramesLength);
                SetGridAnchors(GridCount, GridUnit);
            }
        }
                
        public void AddSystemsToMap(List<object[]> mapObjects)
        {
            for (int i = 0; i < mapObjects.Count; i++)
            {
                MapObjects.Add(new PlotObjects
                {
                    Name = mapObjects[i][0].ToString(),
                    X = (double)mapObjects[i][1],
                    Y = (double)mapObjects[i][2],
                    Z = (double)mapObjects[i][3],
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

        private void ShowContextMenu()
        {
#if DEBUG
            //Debug.WriteLine("Show context menu");
#endif
            contextMenuStrip.Items.Clear();

            var CenterTo = new ToolStripMenuItem
            {
                Text = "Center to " + SelectedObjectName,
                Tag = SelectedObjectCoords
            };
            CenterTo.Click += CenterTo_Click;
            contextMenuStrip.Items.Add(CenterTo);

            contextMenuStrip.Show(this, mousePosition);
        }

        private void CenterTo_Click(object sender, EventArgs e)
        {
            var selected = sender as ToolStripMenuItem;
            SetCenterCoordinates((double[])selected.Tag);
        }

        private void UpdateProjection()
        {
            extPlotLabel.Text = "";
            extPlotLabel.Visible = false;

            var x = (distance * Math.Cos(elevation) * Math.Cos(azimuth));
            var y = (distance * Math.Cos(elevation) * Math.Sin(azimuth));
            var z = (distance * Math.Sin(elevation));

            cameraPosition = new double[3] { -y, z, -x };

            if (Axes != null) // we calculate that even if the axes widget is hidden, because its center coordinates are used for other calculations
            {
                Update.Projection(Axes, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
            }
                        
            if (ShowFrameWidget && Frames != null)
            {
                Update.Projection(Frames, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);                
                Update.Projection(Planes, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
            }

            if (ShowGridWidget && GridHp != null && GridHm != null)
            {
                Update.Projection(Grids, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
            }

            if (MapObjects != null)
            {
                Update.Projection(MapObjects, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
            }

            Invalidate();
        }

        private void PlotCanvas_Resize(object sender, EventArgs e)
        {
            UpdateProjection();
        }

        private void PlotCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            // give some love to the rendering engine
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.CompositingMode = CompositingMode.SourceOver;

            /// axes
            using (var AxisPen = new Pen(new SolidBrush(ForeColor))
            {
                Width = 2,
                DashStyle = DashStyle.Solid
            })
            {
                if (ShowAxesWidget && Axes != null)
                {
                    for (int i = 0; i < Axes.Count; i++)
                    {
                        if (i == 1)
                        {
                            AxisPen.Color = Color.Red;
                            g.DrawLine(AxisPen, Axes[0].Coords, Axes[1].Coords);
                        }
                        if (i == 2)
                        {
                            AxisPen.Color = Color.Green;
                            g.DrawLine(AxisPen, Axes[0].Coords, Axes[2].Coords);
                        }

                        if (i == 3)
                        {
                            AxisPen.Color = Color.Blue;
                            g.DrawLine(AxisPen, Axes[0].Coords, Axes[3].Coords);
                        }
                    }
                }
            }

            /// Grid
            if (ShowGridWidget)
            {
                using (var GridPen = new Pen(new SolidBrush(Color.FromArgb(80, 40, 160, 220))) { Width = 1 })
                {
                    for (int i = 0; i < Grids.Count; i++)
                    {
                        g.DrawLine(GridPen, Grids[i][0].Coords, Grids[i][1].Coords);
                    }
                }
            }

            /// Frame
            if (ShowFrameWidget)
            {
                using (var FramePen = new Pen(new SolidBrush(ForeColor))
                {
                    Width = 1,
                    DashStyle = DashStyle.Solid
                })
                {
                    /// Cubical frame
                    if (GetFrameShape() == Shape.Cube && Frames.Count > 0)
                    {
                        /// bottom
                        g.DrawLine(FramePen, Frames[0].Coords, Frames[1].Coords);
                        g.DrawLine(FramePen, Frames[1].Coords, Frames[5].Coords);
                        g.DrawLine(FramePen, Frames[5].Coords, Frames[3].Coords);
                        g.DrawLine(FramePen, Frames[3].Coords, Frames[0].Coords);

                        /// left
                        g.DrawLine(FramePen, Frames[0].Coords, Frames[2].Coords);
                        g.DrawLine(FramePen, Frames[2].Coords, Frames[4].Coords);
                        g.DrawLine(FramePen, Frames[4].Coords, Frames[3].Coords);

                        /// right
                        g.DrawLine(FramePen, Frames[1].Coords, Frames[6].Coords);
                        g.DrawLine(FramePen, Frames[6].Coords, Frames[7].Coords);
                        g.DrawLine(FramePen, Frames[7].Coords, Frames[5].Coords);

                        /// top
                        g.DrawLine(FramePen, Frames[2].Coords, Frames[6].Coords);
                        g.DrawLine(FramePen, Frames[4].Coords, Frames[7].Coords);
                    }
                }

                /// Frame
                using (var PlanePen = new Pen(new SolidBrush(ForeColor))
                {
                    Width = 1,
                    DashStyle = DashStyle.Solid
                })
                {
                    /// Spherical frame
                    if (GetFrameShape() == Shape.Sphere && Frames.Count > 0)
                    {
                        var horizontalPlane = new PointF[] { Planes[0].Coords, Planes[2].Coords, Planes[1].Coords, Planes[3].Coords };
                        var verticalPlane = new PointF[] { Planes[0].Coords, Planes[4].Coords, Planes[1].Coords, Planes[5].Coords };
                        var longitudinalPlane = new PointF[] { Planes[2].Coords, Planes[4].Coords, Planes[3].Coords, Planes[5].Coords };

                        PlanePen.Color = Color.Red;
                        g.DrawClosedCurve(PlanePen, horizontalPlane, (float)0.85, FillMode.Winding);
                        PlanePen.Color = Color.Green;
                        g.DrawClosedCurve(PlanePen, verticalPlane, (float)0.85, FillMode.Winding);
                        PlanePen.Color = Color.Blue;
                        g.DrawClosedCurve(PlanePen, longitudinalPlane, (float)0.85, FillMode.Winding);
                    }
                }
            }

            if (MapObjects != null)
            {
                for (int i = MapObjects.Count - 1; i >= 0; i--)
                {
                    var FillColor = MapObjects[i].IsVisited ? MapObjects[i].IsCurrent ? CurrentColor : VisitedColor : UnVisitedColor;

                    using (var objectBrush = new SolidBrush(FillColor))
                    {
                        g.FillEllipse(objectBrush, new RectangleF(
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
                            g.DrawLine(TravelMapPen, MapObjects[i].Coords, MapObjects[i + 1].Coords);
                        }
                    }
                }

            }
            base.OnPaint(e);
        }
                
        private void PlotCanvas_MouseLeave(object sender, EventArgs e)
        {
            _mouseIdleTimer.Stop();
#if DEBUG
            //Debug.WriteLine("AstroPlot timer stopped");
#endif
        }

        private void PlotCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            _mouseIdleTimer.Stop();

            mousePosition = e.Location;

            if (leftMousePressed)
            {
                azimuth = lastAzimuth - ((ptMouseClick.X - e.X) / mouseSensitivity);
                elevation = lastElevation + ((ptMouseClick.Y - e.Y) / mouseSensitivity);
                UpdateProjection();
            }

            if (middleMousePressed)
            {
                var dragPosition = GetMouseDelta(mousePosition, ptMouseClick);

                //Debug.WriteLine(dragPosition);

                GetCenterCoordinates()[0] += dragPosition.X * mouseDragSensitivity;
                GetCenterCoordinates()[2] += dragPosition.Y * mouseDragSensitivity;
                SetCenterCoordinates(GetCenterCoordinates());
            }
                        
            _mouseIdleTimer.Start();
        }

        private Point GetMouseDelta(Point mousePosition, PointF ptMouseClick)
        {
            var delta = new Point();

            if (mousePosition != lastMousePosition)
            {
                delta.X = (int)(mousePosition.X - ptMouseClick.X);
                delta.Y = -(int)(mousePosition.Y - ptMouseClick.Y);
            }

            lastMousePosition = mousePosition;

            return delta;
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
                lastAzimuth = azimuth;
                lastElevation = elevation;
                ptMouseClick = new PointF(e.X, e.Y);
            }
            if (e.Button == MouseButtons.Right)
            {
                rightMousePressed = true;
            }
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

        private void PlotCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                extPlotLabel.Text = "";
                extPlotLabel.Visible = false;
            }
            if (e.Button == MouseButtons.Middle)
            {
                extPlotLabel.Text = "";
                extPlotLabel.Visible = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (isObjectSelected)
                {                    
                    ShowContextMenu();
                }
            }
        }

        private void MouseIdleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var hs = HotSpotSize;
            var text = "";
            var point = new Point();
            var coords = new double[3];
            var lastText = text;
            isObjectSelected = false;

            if (MapObjects != null)
            {
                for (int i = MapObjects.Count - 1; i >= 0; i--)
                {
                    if (mousePosition.X > (MapObjects[i].Coords.X - hs) && mousePosition.X < (MapObjects[i].Coords.X + hs) &&
                        mousePosition.Y > (MapObjects[i].Coords.Y - hs) && mousePosition.Y > (MapObjects[i].Coords.Y - hs))
                    {
                        isObjectSelected = true;
                        text = MapObjects[i].Name;
                        point = new Point((int)MapObjects[i].Coords.X, (int)MapObjects[i].Coords.Y);
                        coords = new double[] { MapObjects[i].X, MapObjects[i].Y, MapObjects[i].Z };
                    }
                }
            }

            BeginInvoke(
                (Action)(
                    () =>
                    {
                        if (text != "" && text != lastText)
                        {
                            selectedObjectName = text;
                            selectedObjectPoint = point;
                            selectedObjectCoords = coords;
                            lastText = text;

                            extPlotLabel.Visible = true;
                            extPlotLabel.Text = selectedObjectName;
                            extPlotLabel.Location = new Point(point.X - SmallDotSize, point.Y - (SmallDotSize * 3));
#if DEBUG
                            //Debug.WriteLine("AstroPlot - Mouse on: " + selectedObjectName + ", " + selectedObjectCoords[0] + ", " + selectedObjectCoords[1] + ", " + selectedObjectCoords[2]);
#endif
                        }
                    }
                )
            );
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!middleMousePressed)
            {
                extPlotLabel.Visible = false;

                // zoom
                if (MouseWheel_Resistance != 0)
                {
                    Distance += (-e.Delta * MouseWheel_Multiply) / MouseWheel_Resistance;
                    Debug.WriteLine(Distance);
                }
                else
                {
                    Distance += (-e.Delta * MouseWheel_Multiply);
                    Debug.WriteLine(Distance);
                }
            }
        }        
    }
}
