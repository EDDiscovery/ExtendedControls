/*
 * Copyright © 2015 - 2021 EDDiscovery development team
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ExtendedControls
{
    public partial class ImageViewer : ScrollableControl
    {
        #region Public Values

        [DefaultValue(true), Category("Appearance")]
        public bool AutoCenter
        {
            get { return autoCenter; }
            set
            {
                if (autoCenter != value)
                {
                    autoCenter = value;
                    this.OnAutoCenterChanged(EventArgs.Empty);
                }
            }
        }

        [DefaultValue(true), Category("Behavior")]
        public bool AutoPan
        {
            get { return autoPan; }
            set
            {
                if (autoPan != value)
                {
                    autoPan = value;
                    this.OnAutoPanChanged(EventArgs.Empty);

                    if (value)
                        this.SizeToFit = false;
                }
            }
        }

        [DefaultValue(true), Category("Behavior")]
        public bool ClickToZoom
        {
            get { return clicktozoom; }
            set
            {
                clicktozoom = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMinSize
        {
            get { return base.AutoScrollMinSize; }
            set { base.AutoScrollMinSize = value; }
        }



        [Category("Appearance"), DefaultValue(null)]
        public virtual Image Image
        {
            get { return image; }
            set
            {
                if (image != value)
                {
                    image = value;
                    this.OnImageChanged(EventArgs.Empty);
                }
            }
        }

        [DefaultValue(InterpolationMode.Default), Category("Appearance")]
        public InterpolationMode InterpolationMode
        {
            get { return interpolationMode; }
            set
            {
                if (value == InterpolationMode.Invalid)
                    value = InterpolationMode.Default;

                if (interpolationMode != value)
                {
                    interpolationMode = value;
                    this.OnInterpolationModeChanged(EventArgs.Empty);
                }
            }
        }

        public int MinZoom { get; set; } = 10;
        public int MaxZoom { get; set; } = 400;

        [DefaultValue(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsPanning
        {
            get { return isPanning; }
            protected set
            {
                if (isPanning != value)
                {
                    isPanning = value;
                    startScrollPosition = this.AutoScrollPosition;

                    if (value)
                    {
                        this.Cursor = Cursors.SizeAll;
                        this.OnPanStart(EventArgs.Empty);
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        this.OnPanEnd(EventArgs.Empty);
                    }
                }
            }
        }

        [DefaultValue(false), Category("Appearance")]
        public bool SizeToFit
        {
            get { return sizeToFit; }
            set
            {
                if (sizeToFit != value)
                {
                    sizeToFit = value;
                    this.OnSizeToFitChanged(EventArgs.Empty);

                    if (value)
                        this.AutoPan = false;
                }
            }
        }

        [DefaultValue(100), Category("Appearance")]
        public int Zoom
        {
            get { return zoom; }
            set
            {
                int newzoom = Math.Max(Math.Min(value, MaxZoom), MinZoom);        // limit zoom

                if (zoom != newzoom)
                {
                    zoom = newzoom;
                    this.OnZoomChanged(EventArgs.Empty);
                }
            }
        }

        public void ZoomIn()
        {
            ZoomSet(Zoom + ZoomIncrement);
        }

        public void ZoomOut()
        {
            ZoomSet(Zoom - ZoomIncrement);
        }

        [DefaultValue(20), Category("Behavior")]
        public int ZoomIncrement
        {
            get { return zoomIncrement; }
            set
            {
                if (zoomIncrement != value)
                {
                    zoomIncrement = value;
                    this.OnZoomIncrementChanged(EventArgs.Empty);
                }
            }
        }

        public virtual int ScaledImageHeight
        { get { return this.Image != null ? (int)(this.Image.Size.Height * this.ZoomFactor) : 0; } }

        public virtual int ScaledImageWidth
        { get { return this.Image != null ? (int)(this.Image.Size.Width * this.ZoomFactor) : 0; } }

        public virtual double ZoomFactor
        { get { return (double)this.Zoom / 100; } }

        #endregion

        #region  Events  

        public event EventHandler AutoCenterChanged;
        public event EventHandler AutoPanChanged;
        public event EventHandler ImageChanged;
        public event EventHandler InterpolationModeChanged;
        public event EventHandler InvertMouseChanged;
        public event EventHandler PanEnd;
        public event EventHandler PanStart;
        public event EventHandler SizeToFitChanged;
        public event EventHandler ZoomChanged;
        public event EventHandler ZoomIncrementChanged;

        #endregion  Events  

        #region  Public Methods  

        public virtual Rectangle GetImageViewPort()     // ViewPort is the area to paint on
        {
            Rectangle viewPort;

            if (this.Image != null)
            {
                Rectangle innerRectangle;
                Point offset;

                innerRectangle = this.GetInsideViewPort();

                if (this.AutoCenter)
                {
                    int x;
                    int y;

                    x = !this.HScroll ? (innerRectangle.Width - (this.ScaledImageWidth + this.Padding.Horizontal)) / 2 : 0;
                    y = !this.VScroll ? (innerRectangle.Height - (this.ScaledImageHeight + this.Padding.Vertical)) / 2 : 0;

                    offset = new Point(x, y);
                }
                else
                    offset = Point.Empty;

                viewPort = new Rectangle(offset.X + innerRectangle.Left + this.Padding.Left, offset.Y + innerRectangle.Top + this.Padding.Top, innerRectangle.Width - (this.Padding.Horizontal + (offset.X * 2)), innerRectangle.Height - (this.Padding.Vertical + (offset.Y * 2)));
            }
            else
                viewPort = Rectangle.Empty;

            return viewPort;
        }

        public virtual Rectangle GetInsideViewPort(bool includePadding = false)     // this is the client size less padding optionally
        {
            int left = 0;
            int top = 0;
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            if (includePadding)
            {
                left += this.Padding.Left;
                top += this.Padding.Top;
                width -= this.Padding.Horizontal;
                height -= this.Padding.Vertical;
            }

            return new Rectangle(left, top, width, height);
        }

        public virtual Rectangle GetSourceImageRegion()     // area being viewed
        {
            if (this.Image != null)
            {
                Rectangle viewPort = this.GetImageViewPort();
                int sourceLeft = (int)(-this.AutoScrollPosition.X / this.ZoomFactor);
                int sourceTop = (int)(-this.AutoScrollPosition.Y / this.ZoomFactor);
                int sourceWidth = (int)(viewPort.Width / this.ZoomFactor);
                int sourceHeight = (int)(viewPort.Height / this.ZoomFactor);
                return new Rectangle(sourceLeft, sourceTop, sourceWidth, sourceHeight);
            }
            else
                return Rectangle.Empty;
        }

        // given a mouse position over the image, what is the logical position. 0,0 is top left.
        public bool PositionFromMouse(Point mouse, out Point res)
        {
            res = Point.Empty;
            Rectangle imageviewport = GetImageViewPort();
            int mx = mouse.X - imageviewport.X;
            int my = mouse.Y - imageviewport.Y;

            if (mx >= 0 && mx < imageviewport.Width && my >= 0 && my <= imageviewport.Height)
            {
                float fractx = (float)mx / imageviewport.Width;
                float fracty = (float)my / imageviewport.Height;

                Rectangle imagesourceregion = GetSourceImageRegion();  // This is what we are displaying

                res = new Point(imagesourceregion.X + (int)(imagesourceregion.Width * fractx), imagesourceregion.Y + (int)(imagesourceregion.Height * fracty));

                return true;
            }
            else
                return false;
        }

        public virtual void ZoomToFit()
        {
            if (this.Image != null)
            {
                Rectangle innerRectangle;
                double zoom;
                double aspectRatio;

                this.AutoScrollMinSize = Size.Empty;

                innerRectangle = this.GetInsideViewPort(true);

                if (this.Image.Width > this.Image.Height)
                {
                    aspectRatio = ((double)innerRectangle.Width) / ((double)this.Image.Width);
                    zoom = aspectRatio * 100.0;

                    if (innerRectangle.Height < ((this.Image.Height * zoom) / 100.0))
                    {
                        aspectRatio = ((double)innerRectangle.Height) / ((double)this.Image.Height);
                        zoom = aspectRatio * 100.0;
                    }
                }
                else
                {
                    aspectRatio = ((double)innerRectangle.Height) / ((double)this.Image.Height);
                    zoom = aspectRatio * 100.0;

                    if (innerRectangle.Width < ((this.Image.Width * zoom) / 100.0))
                    {
                        aspectRatio = ((double)innerRectangle.Width) / ((double)this.Image.Width);
                        zoom = aspectRatio * 100.0;
                    }
                }

                this.Zoom = (int)zoom;
            }
        }

        #endregion  Public Methods  



        #region  Overriden Properties  

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue(true)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set
            {
                if (base.AutoSize != value)
                {
                    base.AutoSize = value;
                    this.AdjustLayout();
                }
            }
        }

        [DefaultValue(typeof(Color), "White")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion  Overriden Properties  


        private bool autoCenter;
        private bool autoPan;
        private bool clicktozoom;

        private System.Drawing.Image image;
        private InterpolationMode interpolationMode;
        private bool isPanning;
        private bool sizeToFit;
        private Point startMousePosition;
        private Point startScrollPosition;
        private TextureBrush texture;
        private int zoom;
        private int zoomIncrement;


        #region  Public Overridden Methods  

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size size;

            if (this.Image != null)
            {
                int width;
                int height;

                // get the size of the image
                width = this.ScaledImageWidth;
                height = this.ScaledImageHeight;

                // add an offset based on padding
                width += this.Padding.Horizontal;
                height += this.Padding.Vertical;

                size = new Size(width, height);
            }
            else
                size = base.GetPreferredSize(proposedSize);

            return size;
        }

        #endregion  Public Overridden Methods  

        
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        #region Implementation

        public ImageViewer()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, false);
            this.UpdateStyles();

            this.BackColor = Color.White;
            this.AutoSize = true;
            this.AutoPan = true;
            this.ClickToZoom = true;
            this.Zoom = 100;
            this.ZoomIncrement = 10;
            this.InterpolationMode = InterpolationMode.Default;
            this.AutoCenter = true;
        }

        #region  Protected Overridden Methods  

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                if (texture != null)
                {
                    texture.Dispose();
                    texture = null;
                }

            }
            base.Dispose(disposing);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            bool result;

            if ((keyData & Keys.Right) == Keys.Right | (keyData & Keys.Left) == Keys.Left | (keyData & Keys.Up) == Keys.Up | (keyData & Keys.Down) == Keys.Down)
                result = true;
            else
                result = base.IsInputKey(keyData);

            return result;
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            this.Invalidate();
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);

            if (this.Dock != DockStyle.None)
                this.AutoSize = false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Left:
                    this.AdjustScroll(-(e.Modifiers == Keys.None ? this.HorizontalScroll.SmallChange : this.HorizontalScroll.LargeChange), 0);
                    break;
                case Keys.Right:
                    this.AdjustScroll(e.Modifiers == Keys.None ? this.HorizontalScroll.SmallChange : this.HorizontalScroll.LargeChange, 0);
                    break;
                case Keys.Up:
                    this.AdjustScroll(0, -(e.Modifiers == Keys.None ? this.VerticalScroll.SmallChange : this.VerticalScroll.LargeChange));
                    break;
                case Keys.Down:
                    this.AdjustScroll(0, e.Modifiers == Keys.None ? this.VerticalScroll.SmallChange : this.VerticalScroll.LargeChange);
                    break;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!this.IsPanning && !this.SizeToFit && ClickToZoom)
            {
                if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.None)
                {
                    if (this.Zoom >= 100)
                        ZoomSet((int)Math.Round((double)(this.Zoom + 100) / 100) * 100, e);
                    else if (this.Zoom >= 75)
                        ZoomSet(100,e);
                    else
                        ZoomSet((int)(this.Zoom / 0.75F),e);
                }
                else if (e.Button == MouseButtons.Right || (e.Button == MouseButtons.Left && Control.ModifierKeys != Keys.None))
                {
                    if (this.Zoom > 100 && this.Zoom <= 125)
                        ZoomSet(100, e);
                    else if (this.Zoom > 100)
                        ZoomSet((int)Math.Round((double)(this.Zoom - 100) / 100) * 100,e);
                    else
                        ZoomSet((int)(this.Zoom * 0.75F),e);
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!this.Focused)
                this.Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left && this.AutoPan && this.Image != null)
            {
                if (!this.IsPanning)
                {
                    startMousePosition = e.Location;
                    this.IsPanning = true;
                }

                if (this.IsPanning)
                {
                    int x = -startScrollPosition.X + (startMousePosition.X - e.Location.X);
                    int y = -startScrollPosition.Y + (startMousePosition.Y - e.Location.Y);
                    this.UpdateScrollPosition(new Point(x, y));
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (this.IsPanning)
                this.IsPanning = false;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!this.SizeToFit)
            {
                int increment;

                if (Control.ModifierKeys == Keys.None)
                    increment = 1;
                else
                    increment = 5;

                ZoomSet(Zoom + increment * ZoomIncrement * (e.Delta<0 ? -1 : 1), e);
            }
        }

        protected Point? ScreenPoint(Point mouse)
        {
            Rectangle imageviewport = GetImageViewPort();
            int mx = mouse.X - imageviewport.X;
            int my = mouse.Y - imageviewport.Y;

            if (mx > 0 && mx < imageviewport.Width && my > 0 && my < imageviewport.Height)
                return new Point((int)((mx - AutoScrollPosition.X) / ZoomFactor), (int)((my - AutoScrollPosition.Y) / ZoomFactor));
            else
                return null;
        }

        protected void ZoomSet(int newzoom, MouseEventArgs mouse = null)
        {
            if (mouse != null)
            {
                Point? spos = ScreenPoint(mouse.Location);

                newzoom = Math.Max(Math.Min(newzoom, MaxZoom), MinZoom);        // limit zoom

                if (spos != null && newzoom != Zoom)
                {
                    double changezoom = (double)(newzoom-Zoom) / 100.0;
                    //System.Diagnostics.Debug.WriteLine("Screen Point sx {0} Image {1} Autoscroll {2} {3}->{4} {5}", spos, Image.Size, AutoScrollPosition, Zoom, newzoom, changezoom);

                    int nx = -AutoScrollPosition.X + (int)(spos.Value.X * changezoom);
                    int ny = -AutoScrollPosition.Y + (int)(spos.Value.Y * changezoom);
                    Point newpos = new Point(nx, ny);
                    //System.Diagnostics.Debug.WriteLine("Moved to {0}", newpos);
                    Zoom = newzoom;
                    UpdateScrollPosition(newpos);
                }
            }
            else
                Zoom = newzoom;
        }

        protected override void OnPaddingChanged(System.EventArgs e)
        {
            base.OnPaddingChanged(e);
            this.AdjustLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle innerRectangle;

            // draw the borders
            //ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, this.ForeColor, ButtonBorderStyle.Solid);

            innerRectangle = this.GetInsideViewPort();

            // draw the background
            using (SolidBrush brush = new SolidBrush(this.BackColor))
                e.Graphics.FillRectangle(brush, innerRectangle);


            // draw the image
            if (this.Image != null)
            {
                e.Graphics.InterpolationMode = this.InterpolationMode;
                e.Graphics.DrawImage(this.Image, this.GetImageViewPort(), this.GetSourceImageRegion(), GraphicsUnit.Pixel);
            }

            base.OnPaint(e);
        }

        protected override void OnParentChanged(System.EventArgs e)
        {
            base.OnParentChanged(e);
            this.AdjustLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            this.AdjustLayout();

            base.OnResize(e);
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            this.Invalidate();

            base.OnScroll(se);
        }

        #endregion  Protected Overridden Methods  


        #region  Protected Methods  

        protected virtual void AdjustLayout()
        {
            if (this.AutoSize)
                this.AdjustSize();
            else if (this.SizeToFit)
                this.ZoomToFit();
            else if (this.AutoScroll)
                this.AdjustViewPort();
            this.Invalidate();
        }

        protected virtual void AdjustScroll(int x, int y)
        {
            Point scrollPosition;

            scrollPosition = new Point(this.HorizontalScroll.Value + x, this.VerticalScroll.Value + y);

            this.UpdateScrollPosition(scrollPosition);
        }

        protected virtual void AdjustSize()
        {
            if (this.AutoSize && this.Dock == DockStyle.None)
                base.Size = base.PreferredSize;
        }

        protected virtual void AdjustViewPort()
        {
            if (this.AutoScroll && this.Image != null)
                this.AutoScrollMinSize = new Size(this.ScaledImageWidth + this.Padding.Horizontal, this.ScaledImageHeight + this.Padding.Vertical);
        }


        protected virtual void OnAutoCenterChanged(EventArgs e)
        {
            this.Invalidate();

            if (this.AutoCenterChanged != null)
                this.AutoCenterChanged(this, e);
        }

        protected virtual void OnAutoPanChanged(EventArgs e)
        {
            if (this.AutoPanChanged != null)
                this.AutoPanChanged(this, e);
        }

    

        protected virtual void OnImageChanged(EventArgs e)
        {
            this.AdjustLayout();

            if (this.ImageChanged != null)
                this.ImageChanged(this, e);
        }

        protected virtual void OnInterpolationModeChanged(EventArgs e)
        {
            this.Invalidate();

            if (this.InterpolationModeChanged != null)
                this.InterpolationModeChanged(this, e);
        }

        protected virtual void OnInvertMouseChanged(EventArgs e)
        {
            if (this.InvertMouseChanged != null)
                this.InvertMouseChanged(this, e);
        }

        protected virtual void OnPanEnd(EventArgs e)
        {
            if (this.PanEnd != null)
                this.PanEnd(this, e);
        }

        protected virtual void OnPanStart(EventArgs e)
        {
            if (this.PanStart != null)
                this.PanStart(this, e);
        }

        protected virtual void OnSizeToFitChanged(EventArgs e)
        {
            this.AdjustLayout();

            if (this.SizeToFitChanged != null)
                this.SizeToFitChanged(this, e);
        }

        protected virtual void OnZoomChanged(EventArgs e)
        {
            this.AdjustLayout();

            if (this.ZoomChanged != null)
                this.ZoomChanged(this, e);
        }

        protected virtual void OnZoomIncrementChanged(EventArgs e)
        {
            if (this.ZoomIncrementChanged != null)
                this.ZoomIncrementChanged(this, e);
        }

        protected virtual void UpdateScrollPosition(Point position)
        {
            this.AutoScrollPosition = position;
            this.Invalidate();
            this.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, 0));
           // System.Diagnostics.Debug.WriteLine("Position {0} Auto scroll {1}", position, AutoScrollPosition);
        }

        #endregion  Protected Methods  

        #endregion
    }
}

