/*
 * Copyright 2016 - 2025 EDDiscovery development team
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
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ExtendedControls
{
    /// <summary>
    /// Represents a Windows <see cref="Button"/>-style <see cref="Control"/> that is drawn with enumerated graphics
    /// (see <see cref="ImageSelected"/>) or <see cref="Control.Text"/>.
    /// </summary>
    public class ExtButtonDrawn : Control, IButtonControl, IThemeable
    {
        public enum ImageType       // Specifies the available image types to be displayed on a <see cref="ExtPanelDrawn"/>.
        {
            Expand,
            Collapse,
            Close,
            Maximize,
            Minimize,
            OnTop,
            Floating,
            NotTransparent,
            Transparent,
            FullyTransparent,
            TransparentClickThru,
            NotCaptioned,
            Captioned,
            Gripper,
            EDDB,
            Ross,
            Text,
            InverseText,
            Move,
            WindowInTaskBar,
            WindowNotInTaskBar,
            Bars,
            Restore,
            LeftDelta,
            RightDelta,
            TextBorder,

            None,
        };

        public ImageType ImageSelected  // Gets or sets a value indicating which <see cref="ImageType"/> this <see cref="ExtPanelDrawn"/> will display.
        {
            get { return imageselected; }
            set
            {
                if (imageselected != value)
                {
                    imageselected = value;
                    OnImageSelectedChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler ImageSelectedChanged;

        public Image Image      // Gets or sets a value representing the background image that this <see cref="ExtPanelDrawn"/> will display. The
        {
            get { return image; }
            set
            {
                if (image != value)
                {
                    image = value;
                    Invalidate();
                }
            }
        }
                                // and its layout
        public override ImageLayout BackgroundImageLayout { get { return base.BackgroundImageLayout; } set { base.BackgroundImageLayout = value; } }

        public Color MouseOverColor { get; set; } = Color.White;
        public Color MouseSelectedColor { get; set; } = Color.Green;
        public bool MouseSelectedColorEnable { get; set; } = true;
        public float ButtonDisabledScaling { get; set; } = 0.25F;        // scaling when disabled
        public Color BorderColor { get; set; } = Color.Orange;
        public int BorderWidth { get; set; } = 1;

        public ContentAlignment TextAlign       // Gets or sets the alignment of the <see cref="Control.Text"/> displayed on the <see cref="ExtPanelDrawn"/> when
        {
            get { return textalign; }
            set
            {
                if (textalign != value)
                {
                    textalign = value;
                    if (imageselected == ImageType.Text || imageselected == ImageType.InverseText)
                        Invalidate();
                }
            }
        }

        public bool AutoEllipsis { get; set; } = false; // Gets or sets a value indicating whether the ellipsis character (...) appears at the right edge of the control

        public bool UseMnemonic { get; set; } = true;       // Gets or sets a value indicating whether the first character that is preceded by an ampersand (&amp;) is


        public bool Selectable
        {
            get { return selectable; }
            set
            {
                if (selectable != value)
                {
                    if (!value)
                        this.TabStop = false;
                    selectable = value;
                    SetStyle(ControlStyles.Selectable, value);
                }
            }
        }


        #region Implementation

        public ExtButtonDrawn() : base()
        {
            base.BackgroundImageLayout = ImageLayout.Zoom;

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |        // "Free" double-buffering (1/3).
                ControlStyles.OptimizedDoubleBuffer |       // "Free" double-buffering (2/3).
                ControlStyles.ResizeRedraw |                // Invalidate after a resize or if the Padding changes.
                ControlStyles.Selectable |                  // We can receive focus from mouse-click or tab (see the Selectable prop).
                ControlStyles.SupportsTransparentBackColor |// BackColor.A can be less than 255.
                ControlStyles.UserPaint |                   // "Free" double-buffering (3/3); OnPaintBackground and OnPaint are needed.
                ControlStyles.UseTextForAccessibility,      // Use Text for the mnemonic char (and accessibility) if not empty, else the previous Label in the tab order.
                true);

            // We have to handle MouseClick-to-Click logic, otherwise Click fires for any mouse button. Double-click is fully ignored.
            SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, false);
        }

        // disable these..
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler DoubleClick { add { base.DoubleClick += value; } remove { base.DoubleClick -= value; } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event MouseEventHandler MouseDoubleClick { add { base.MouseDoubleClick += value; } remove { base.MouseDoubleClick -= value; } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Image BackgroundImage { get { return base.BackgroundImage; } set { base.BackgroundImage = value; } }
        
        /// <summary>
        /// Gets or sets a value that is returned to the parent <see cref="Form"/> when the <see cref="ExtButtonDrawn"/> is
        /// clicked.
        /// </summary>
        [Category("Behavior"), DefaultValue(typeof(DialogResult), nameof(DialogResult.None)),
            Description("The dialog-box result produced in a form by clicking the DrawnPanel.")]
        public DialogResult DialogResult { get; set; } = DialogResult.None;

        /// <summary>
        /// Receive notifications when the <see cref="ExtButtonDrawn"/> is the default button such that the appearance can
        /// be adjusted.
        /// </summary>
        /// <param name="value"><c>true</c> if the <see cref="ExtButtonDrawn"/> is the default button; <c>false</c>
        /// otherwise.</param>
        public virtual void NotifyDefault(bool value)
        {
            // we're possibly the default "button" on a form. Paint boldly or something. we don't care
        }

        /// <summary>
        /// Generates a <see cref="Control.Click"/> event for the <see cref="ExtButtonDrawn"/>.
        /// </summary>
        public virtual void PerformClick()
        {
            // CanSelect uses (ControlStyles.Selectable && Enabled && Visible) for control and all parents. Negate only the Selectable check when (!_Selectable).
            if ((selectable && this.CanSelect) || (!selectable && this.Enabled && this.Visible && Parent.CanSelect))
                OnClick(EventArgs.Empty);
        }

        #endregion


        public void SetDrawnBitmapRemapTable(ColorMap[] remap, float[][] colormatrix = null)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.Name ?? nameof(ExtButtonDrawn));

            drawnImageAttributesDisabled?.Dispose();
            drawnImageAttributesDisabled = null;
            drawnImageAttributesEnabled?.Dispose();
            drawnImageAttributesEnabled = null;

            DrawingHelpersStaticFunc.ComputeDrawnPanel(out drawnImageAttributesEnabled, out drawnImageAttributesDisabled, ButtonDisabledScaling, remap, colormatrix);

            if (image != null)
                Invalidate();
        }


        #region Implementation

        private enum DrawState { Disabled = -1, Normal = 0, Hover, Click };

        private ImageAttributes drawnImageAttributesDisabled = null;        // Image override (colour etc) while !Enabled.
        private ImageAttributes drawnImageAttributesEnabled = null;         // Image override (colour etc) while Enabled.
        private DrawState drawState = DrawState.Normal;                     // The current state of our control.

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]                   // Visible via Image
        private Image image = null;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]                   // Visible via ImageSelected
        private ImageType imageselected = ImageType.Close;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]                   // Visible via Selectable
        private bool selectable = true;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]                   // Visible via TextAlign
        private ContentAlignment textalign = ContentAlignment.MiddleCenter;

        protected override Padding DefaultPadding { get { return new Padding(4); } }
        protected override Size DefaultSize { get { return new Size(24, 24); } }

        protected override void Dispose(bool disposing)
        {
            // Do not dispose of _Image, as it *should* be a managed resource.
            if (disposing)
            {
                drawnImageAttributesDisabled?.Dispose();
                drawnImageAttributesEnabled?.Dispose();
            }
            ImageSelectedChanged = null;

            image = null;
            drawnImageAttributesDisabled = null;
            drawnImageAttributesEnabled = null;

            base.Dispose(disposing);
        }

        protected override void OnClick(EventArgs e)
        {
            Form f = this.FindForm();
            if (f != null)
                f.DialogResult = this.DialogResult;

            base.OnClick(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (!Enabled)
                SetDrawState(DrawState.Disabled);
            else
                SetDrawState(DrawState.Normal);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();   // Invalidate to update the focus rectangle.
        }

        protected virtual void OnImageSelectedChanged(EventArgs e)
        {
            ImageSelectedChanged?.Invoke(this, e);
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                SetDrawState(DrawState.Click);
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                OnClick(EventArgs.Empty);
                SetDrawState(DrawState.Normal);
            }
            e.Handled = true;

            base.OnKeyUp(e);
        }


        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();   // Invalidate to update the focus rectangle.
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !GetStyle(ControlStyles.StandardClick))
                OnClick(e);

            base.OnMouseClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            //System.Diagnostics.Debug.WriteLine("DP MD");
            if (mevent.Button == MouseButtons.Left)
                SetDrawState(DrawState.Click);

            base.OnMouseDown(mevent);
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            // If Disabled, this method doesn't get called, so we're safe to always switch to Normal.
            // Also, if a mouse button is depressed when the mouse leaves, this doesn't get called until the button gets released.
            SetDrawState(DrawState.Normal);

            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            // Allow a very long click to come and go from our bounds, updating the draw state the whole time.
            if (ClientRectangle.Contains(mevent.Location))
                SetDrawState(mevent.Button == MouseButtons.Left ? DrawState.Click : DrawState.Hover);
            else
                SetDrawState(DrawState.Normal);       // OnMouseLeave doesn't actually fire until /after/ LMB is released, so clear this here.

            base.OnMouseMove(mevent);
        }


        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            //System.Diagnostics.Debug.WriteLine("DP MU");
            if (drawState == DrawState.Click)
            {
                OnMouseClick(mevent);
                SetDrawState(DrawState.Hover);
            }   

            base.OnMouseUp(mevent);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            //Debug.WriteLine($"Draw button selected image {imageselected} Enabled {Enabled}, State {drawState}");

            Color cFore = this.ForeColor;
            switch (drawState)
            {
                case DrawState.Disabled:
                    cFore = this.ForeColor.Average(this.BackColor, ButtonDisabledScaling);
                    break;
                case DrawState.Hover:
                    if (MouseSelectedColorEnable)
                        cFore = this.MouseOverColor;
                    break;
                case DrawState.Click:
                    if (MouseSelectedColorEnable)
                        cFore = this.MouseSelectedColor;
                    break;
            }

            if (imageselected != ImageType.None)
            {
                int rcwidth = this.ClientRectangle.Width - this.Padding.Horizontal;
                int rcheight = this.ClientRectangle.Height - this.Padding.Vertical;
                int shortestDim = Math.Min(rcwidth, rcheight);

                var rc = new Rectangle( this.ClientRectangle.Left + this.Padding.Left, 
                                        this.ClientRectangle.Top + this.Padding.Top,
                                        this.ClientRectangle.Width - this.Padding.Horizontal -1,         // -1 takes into account how the rectange is drawn
                                        this.ClientRectangle.Height - this.Padding.Vertical -1);     

                var sc = new Rectangle( rc.Left + (int)Math.Round((float)(rcwidth - shortestDim) / 2),  // Largest square that fits entirely inside rcClip, centered.
                                        rc.Top + (int)Math.Round((float)(rcheight - shortestDim) / 2), 
                                        shortestDim-1, 
                                        shortestDim-1);

                var scwidth = sc.Width + 1;
                var scheight = sc.Height + 1;

                int centrehorzpx = this.ClientRectangle.Width / 2;
                int centrevertpx = this.ClientRectangle.Height / 2;

                if (imageselected == ImageType.Close)
                {
                   // System.Diagnostics.Debug.Write("For " + imageselected + " wav" + rcwidth + " hav" + rcheight + " rc=" + rc + " cx" + centrehorzpx + " cy" + centrevertpx );
                   // System.Diagnostics.Debug.WriteLine(".. " + " sc=" + sc + " wav" + scwidth + " hav" + scheight);
                }

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                switch (imageselected)
                {
                    case ImageType.Expand:
                    case ImageType.Collapse:
                        {
                            using (var p1 = new Pen(cFore, 1.0F))
                            {
                                e.Graphics.DrawRectangle(p1, sc);
                            }

                            if ( scwidth < 16 )
                                e.Graphics.SmoothingMode = SmoothingMode.None;

                            int adjsize = Math.Max(2, shortestDim / 8);
                            float pensize = rcwidth < 16 ? 1.0f : 2.0f;
                          //  System.Diagnostics.Debug.WriteLine(rcwidth + " Pen " + pensize + " offset" + adjsize);

                            using (var pen = new Pen(cFore, pensize ))
                            {
                                Point p1 = new Point(sc.Left + adjsize, centrevertpx);
                                Point p2 = new Point(sc.Right - adjsize, centrevertpx);
                                e.Graphics.DrawLine(pen, p1,p2 );
                             //   System.Diagnostics.Debug.WriteLine("                         horz "+ p1 + " " + p2);

                                if ( imageselected == ImageType.Expand )
                                    e.Graphics.DrawLine(pen, new Point(centrehorzpx, sc.Top+ adjsize), new Point(centrehorzpx, sc.Bottom - adjsize));
                            }
                            break;
                        }

                    case ImageType.Close:
                        {
                            // Draw a centered 'X'
                            using (var p2 = new Pen(cFore, 2.0F))
                            {
                                e.Graphics.DrawLine(p2, new Point(sc.Left, sc.Top), new Point(sc.Right, sc.Bottom));
                                e.Graphics.DrawLine(p2, new Point(sc.Left, sc.Bottom), new Point(sc.Right, sc.Top));
                            }
                            break;
                        }
                    case ImageType.Maximize:
                        {
                            // Draw a thin square outline with a slightly thicker top edge
                            using (var p1 = new Pen(cFore, 1.0F))
                                e.Graphics.DrawRectangle(p1, sc);
                            float pensize = Math.Max(2.0F,scheight/6);
                            int off = ((int)pensize)/2;
                            using (var p2 = new Pen(cFore, pensize))
                                e.Graphics.DrawLine(p2, new Point(sc.Left, sc.Top + off), new Point(sc.Right, sc.Top + off));
                            break;
                        }
                    case ImageType.Minimize:
                        {
                            // Draw an '_'
                            using (var p2 = new Pen(cFore, 2.0F))
                                e.Graphics.DrawLine(p2, new Point(rc.Left, rc.Bottom), new Point(rc.Right, rc.Bottom));
                            break;
                        }
                    case ImageType.OnTop:
                        {
                            // Draw a filled square
                            using (Brush bFore = new SolidBrush(cFore))
                                e.Graphics.FillRectangle(bFore, sc);
                            break;
                        }
                    case ImageType.Floating:
                        {
                            // Draw an outlined square
                            using (var p2 = new Pen(cFore, 2.0F))
                                e.Graphics.DrawRectangle(p2, sc);
                            break;
                        }
                    case ImageType.TransparentClickThru:
                    case ImageType.FullyTransparent:
                    case ImageType.NotTransparent:
                    case ImageType.Transparent:
                    case ImageType.NotCaptioned:
                    case ImageType.Captioned:
                        {
                            using (var p2 = new Pen(cFore, 2.0F))
                            {
                                if (imageselected != ImageType.NotTransparent && imageselected != ImageType.NotCaptioned )
                                    e.Graphics.DrawRectangle(p2, sc);

                                float penwidth = scwidth < 23 ? 1.0f : 2f;
                                int widthoff = Math.Max(scwidth < 23 ? 2 : 3, scwidth / 8);
                                int heigthoff = Math.Max(scwidth < 23 ? 2 : 3, scheight / 8);

                                //System.Diagnostics.Debug.WriteLine("C " + scwidth + " " + widthoff + " " + topoff);

                                using (var p1 = new Pen(cFore, penwidth))
                                {
                                    if (imageselected == ImageType.Captioned || imageselected == ImageType.NotCaptioned)
                                    {
                                        // big C
                                        widthoff *= 2;
                                        heigthoff *= 2;
                                        e.Graphics.DrawLine(p1, new Point(sc.Left + widthoff, sc.Top + heigthoff), new Point(sc.Right - widthoff, sc.Top + heigthoff));
                                        e.Graphics.DrawLine(p1, new Point(sc.Left + widthoff, sc.Top + heigthoff), new Point(sc.Left + widthoff, sc.Bottom - heigthoff));
                                        e.Graphics.DrawLine(p1, new Point(sc.Left + widthoff, sc.Bottom - heigthoff), new Point(sc.Right - widthoff, sc.Bottom - heigthoff));

                                    }
                                    else
                                    {
                                        // the T
                                        e.Graphics.DrawLine(p1, new Point(sc.Left + widthoff, sc.Top + heigthoff), new Point(sc.Right - widthoff, sc.Top + heigthoff));
                                        e.Graphics.DrawLine(p1, new Point(centrehorzpx, sc.Top + heigthoff), new Point(centrehorzpx, sc.Bottom - heigthoff));

                                        // the C or F
                                        if (imageselected != ImageType.NotTransparent && imageselected != ImageType.Transparent)
                                        {
                                            bool f = imageselected == ImageType.FullyTransparent;

                                            int cleft = centrehorzpx + widthoff;
                                            int cright = sc.Right - 1 - widthoff;
                                            int ctop = sc.Top + heigthoff * 2;
                                            int cbot = sc.Bottom - heigthoff * 2;

                                            //System.Diagnostics.Debug.WriteLine(".. " + ctop +" " + cbot);

                                            e.Graphics.DrawLine(p1, new Point(cleft, ctop), new Point(cright, ctop));
                                            e.Graphics.DrawLine(p1, new Point(cleft, ctop), new Point(cleft, cbot));

                                            if (f)
                                                e.Graphics.DrawLine(p1, new Point(cleft, ctop + heigthoff * 2), new Point(cright, ctop + heigthoff * 2));
                                            else
                                                e.Graphics.DrawLine(p1, new Point(cleft, cbot), new Point(cright, cbot));
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    case ImageType.Gripper:
                        {
                            // Draw 3 thin parallel diagonal lines from the bottom edge to the right edge
                            int mDim = (int)Math.Round(shortestDim / 6f);

                            using (var p1 = new Pen(cFore, 1.0F))
                            {
                                int x = rc.Right - 3;
                                int y = rc.Bottom - 3;

                                for (int i = 0; i < 3; i++)
                                {
                                    Point pt1 = new Point(x, rc.Bottom - 1);
                                    Point pt2 = new Point(rc.Right - 1, y);
                                    e.Graphics.DrawLine(p1, pt1, pt2);
                                    x -= mDim;
                                    y -= mDim;
                                }
                            }
                            break;
                        }

                    case ImageType.EDDB:
                        {
                            // Draw an inverted arrow extending from the middle/bottom-right to the middle/bottom-center, then "curved" up to end at the middle/top-center
                            int mDim = (int)Math.Round(shortestDim / 6f);
                            var btmRight = new Point(rc.Right, rc.Bottom - mDim);
                            var btmCenter = new Point(centrehorzpx - 1, rc.Bottom - mDim);
                            var topCenter = new Point(centrehorzpx - 1, rc.Top + mDim);
                            var topLeftCenter = new Point(centrehorzpx - 1 - mDim, topCenter.Y + mDim);
                            var topRightCenter = new Point(centrehorzpx - 1 + mDim, topCenter.Y + mDim);

                            cFore = this.BackColor;     // INVERTED

                            using (Pen pb = new Pen(cFore, 2.0F))
                            {
                                e.Graphics.DrawLine(pb, btmRight, btmCenter);
                                e.Graphics.DrawLine(pb, btmCenter, topCenter);
                                e.Graphics.DrawLine(pb, topCenter, topLeftCenter);
                                e.Graphics.DrawLine(pb, topCenter, topRightCenter);
                            }
                            break;
                        }
                    case ImageType.Ross:
                        {
                            // Draw an inverted thick '┌'-style symbol
                            cFore = this.BackColor;     // INVERTED
                            using (Pen pb = new Pen(cFore, 3.0F))
                            {
                                int left = centrehorzpx - Math.Max(3, rcwidth / 4);
                                int right = centrehorzpx + Math.Max(3,rcwidth / 4);
                                int top = rc.Top + rcheight / 6;
                                int bot = rc.Bottom - rcheight / 6;

                                e.Graphics.DrawLine(pb, new Point(left, top), new Point(right, top));
                                e.Graphics.DrawLine(pb, new Point(left, top), new Point(left, bot));
                            }
                            break;
                        }

                    case ImageType.InverseText:
                    case ImageType.TextBorder:
                    case ImageType.Text:
                        {
                            if (imageselected == ImageType.InverseText)
                                cFore = this.BackColor;

                            if (!string.IsNullOrWhiteSpace(this.Text))      // draw text
                            {
                                var txalign = Environment.OSVersion.Platform == PlatformID.Win32NT ? RtlTranslateAlignment(TextAlign) : TextAlign;      // MONO Bug cover over

                                using (var fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(txalign))
                                {
                                    using (var textb = new SolidBrush(cFore))
                                    {
                                        if (this.UseMnemonic)
                                            fmt.HotkeyPrefix = this.ShowKeyboardCues ? HotkeyPrefix.Show : HotkeyPrefix.Hide;
                                        if (this.AutoEllipsis)
                                            fmt.Trimming = StringTrimming.EllipsisCharacter;
                                        e.Graphics.DrawString(this.Text, this.Font, textb, this.ClientRectangle, fmt);
                                    }
                                }
                            }
                            if (imageselected == ImageType.TextBorder)
                            {
                                e.Graphics.SmoothingMode = SmoothingMode.None;
                                using (var pen = new Pen(BorderColor, BorderWidth))
                                {
                                    e.Graphics.DrawRectangle(pen, new Rectangle(BorderWidth-1, BorderWidth-1, ClientSize.Width - BorderWidth * 1, ClientSize.Height - BorderWidth * 1));
                                }
                            }

                            break;
                        }
                    case ImageType.Move:
                        {
                            // Draw two perpendicular (1 vertical, 1 horizontal) double-ended arrows that cross in the center
                            int mDim = (int)Math.Round(shortestDim / 8f);
                            var btmCenter = new Point(centrehorzpx, sc.Bottom);
                            var topCenter = new Point(centrehorzpx, sc.Top);
                            var lftCenter = new Point(sc.Left, centrevertpx);
                            var rgtCenter = new Point(sc.Right, centrevertpx);

                            using (var p1 = new Pen(cFore, 1.0F))
                            {
                                using (var p2 = new Pen(cFore, 2.0F))
                                {
                                    e.Graphics.DrawLine(p2, btmCenter, topCenter);
                                    e.Graphics.DrawLine(p2, lftCenter, rgtCenter);

                                    e.Graphics.DrawLine(p1, btmCenter, new Point(centrehorzpx - mDim, sc.Bottom - mDim));
                                    e.Graphics.DrawLine(p1, btmCenter, new Point(centrehorzpx + mDim, sc.Bottom - mDim));
                                    e.Graphics.DrawLine(p1, topCenter, new Point(centrehorzpx - mDim, sc.Top + mDim));
                                    e.Graphics.DrawLine(p1, topCenter, new Point(centrehorzpx + mDim, sc.Top + mDim));

                                    e.Graphics.DrawLine(p1, lftCenter, new Point(sc.Left + mDim, centrevertpx - mDim));
                                    e.Graphics.DrawLine(p1, lftCenter, new Point(sc.Left + mDim, centrevertpx + mDim));
                                    e.Graphics.DrawLine(p1, rgtCenter, new Point(sc.Right - mDim, centrevertpx - mDim));
                                    e.Graphics.DrawLine(p1, rgtCenter, new Point(sc.Right - mDim, centrevertpx + mDim));
                                }
                            }
                            break;
                        }
                    case ImageType.WindowNotInTaskBar:
                    case ImageType.WindowInTaskBar:
                        {
                            // Draw a thin horizontal rectangle outline, possibly with two small filled squares in the left

                            int boxheight = rcheight / 2;
                            int top = centrevertpx - rcheight/4;

                            float penwidth = scwidth < 23 ? 1.0f : 2f;

                            using (var p1 = new Pen(cFore, penwidth))
                                e.Graphics.DrawRectangle(p1, new Rectangle(rc.Left, top, rc.Width, boxheight));

                            if (imageselected == ImageType.WindowInTaskBar)
                            {
                                int off = (int)penwidth + 1;
                                int boxsize = boxheight - 2*(int)penwidth - 2;

                                using (Brush bbck = new SolidBrush(cFore))
                                {
                                    e.Graphics.FillRectangle(bbck, new Rectangle(rc.Left + off, top + off, boxsize,boxsize));
                                    e.Graphics.FillRectangle(bbck, new Rectangle(rc.Right - off - boxsize, top + off, boxsize,boxsize));
                                }
                            }
                            break;
                        }
                    case ImageType.Bars:
                        {
                            // Draw two thin horizontal bars along the top edge
                            using (var p1 = new Pen(cFore, 1.0F))
                            {
                                e.Graphics.DrawLine(p1, new Point(rc.Left, rc.Top), new Point(rc.Right, rc.Top));
                                e.Graphics.DrawLine(p1, new Point(rc.Left, rc.Top + 2), new Point(rc.Right, rc.Top + 2));
                            }
                            break;
                        }
                    case ImageType.Restore:
                        {
                            // Draw two overlapping, but identically-sized with an offset, thin square outlines each with slightly thicker top edges
                            int iDim = (int)Math.Round((float)shortestDim * 2 / 3);    // Length of a "window" edge

                            using (var p1 = new Pen(cFore, 1.0F))
                            using (var p2 = new Pen(cFore, 2.0F))
                            {
                                // lower-left foreground "window", clockwise from top-left
                                e.Graphics.DrawLine(p2, new Point(sc.Left, sc.Bottom - iDim), new Point(sc.Left + iDim, sc.Bottom - iDim));
                                e.Graphics.DrawLine(p1, new Point(sc.Left + iDim, sc.Bottom - iDim), new Point(sc.Left + iDim, sc.Bottom));
                                e.Graphics.DrawLine(p1, new Point(sc.Left + iDim, sc.Bottom), new Point(sc.Left, sc.Bottom));
                                e.Graphics.DrawLine(p1, new Point(sc.Left, sc.Bottom), new Point(sc.Left, sc.Bottom - iDim));

                                // upper-right background "window" clockwise from (obscured!) bottom-left
                                e.Graphics.DrawLine(p1, new Point(sc.Right - iDim, sc.Bottom - iDim), new Point(sc.Right - iDim, sc.Top));
                                e.Graphics.DrawLine(p2, new Point(sc.Right - iDim, sc.Top), new Point(sc.Right, sc.Top));
                                e.Graphics.DrawLine(p1, new Point(sc.Right, sc.Top), new Point(sc.Right, sc.Top + iDim));
                                e.Graphics.DrawLine(p1, new Point(sc.Right, sc.Top + iDim), new Point(sc.Left + iDim, sc.Top + iDim));
                            }
                            break;
                        }

                    case ImageType.LeftDelta:
                        {
                            using (Brush bFore = new SolidBrush(cFore))
                            {
                                e.Graphics.FillPolygon(bFore, new System.Drawing.Point[]
                                {
                                    new Point(sc.Left,sc.YCenter()),
                                    new Point(sc.Right,sc.Top),
                                    new Point(sc.Right,sc.Bottom),
                                });
                            }
                            break;
                        }

                    case ImageType.RightDelta:
                        {
                            using (Brush bFore = new SolidBrush(cFore))
                            {
                                e.Graphics.FillPolygon(bFore, new System.Drawing.Point[]
                                {
                                    new Point(sc.Right,sc.YCenter()),
                                    new Point(sc.Left,sc.Top),
                                    new Point(sc.Left,sc.Bottom),
                                });
                            }
                            break;
                        }

                    default:
                        throw new NotImplementedException($"ImageType ({imageselected}) painting is apparantly not implemented; please add support for it.");
                }

                e.Graphics.SmoothingMode = SmoothingMode.Default;
            }

            // Draw a focus rectangle. CanFocus: (IsHandleCreated && Visible && Enabled)
            if (this.CanFocus && this.Focused && this.ShowFocusCues)
            {
                using (var p = new Pen(cFore))
                {
                    var rcFocus = new Rectangle(new Point(1, 1), new Size(this.ClientSize.Width - 3, this.ClientSize.Height - 3));
                    e.Graphics.DrawRectangle(p, rcFocus);   // Draw a rectangle outline 1px smaller than ClientSize ...

                    rcFocus.Inflate(-1, -1);
                    p.DashStyle = DashStyle.Dash;
                    p.DashPattern = new[] { 1f, 1f };
                    e.Graphics.DrawRectangle(p, rcFocus);   // Then draw a dashed rectangle outline 1px smaller than that.
                }
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// Paints the background of the <see cref="ExtButtonDrawn"/>.
        /// </summary>
        /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            switch (imageselected)
            {
                // Manually paint the background for types with inverted colors. Hope that you didn't want BackgroundImage.
                case ImageType.EDDB:
                case ImageType.InverseText:
                case ImageType.Ross:
                    {
                        // This colour is used for the background of the inverted image types.
                        Color cFore = this.ForeColor;
                        switch (drawState)
                        {
                            case DrawState.Disabled:
                                cFore = cFore.Average(this.BackColor, ButtonDisabledScaling);
                                break;
                            case DrawState.Hover:
                                if (MouseSelectedColorEnable)
                                    cFore = this.MouseOverColor;
                                break;
                            case DrawState.Click:
                                if (MouseSelectedColorEnable)
                                    cFore = this.MouseSelectedColor;
                                break;
                        }

                        using (var b = new SolidBrush(cFore))
                            e.Graphics.FillRectangle(b, this.ClientRectangle);
                        break;
                    }



                // Otherwise, base can handle it.
                default:
                    {
                        base.OnPaintBackground(e);
                        break;
                    }
            }

            if (image != null)
            {
                ImageAttributes iattrib = this.Enabled ? drawnImageAttributesEnabled : drawnImageAttributesDisabled;

                switch (base.BackgroundImageLayout)
                {
                    case ImageLayout.None:      // Image {0,0} = ClientRectangle {0,0} with no scaling
                        {
                            // maybe honour RightToLeft; Image {1,0} = ClientRect {1,0}
                            var dstRc = this.ClientRectangle;
                            dstRc.Intersect(new Rectangle(Point.Empty, image.Size));
                            if (iattrib != null)
                                e.Graphics.DrawImage(image, dstRc, 0, 0, dstRc.Width, dstRc.Height, GraphicsUnit.Pixel, iattrib);
                            else
                                e.Graphics.DrawImage(image, dstRc, 0, 0, dstRc.Width, dstRc.Height, GraphicsUnit.Pixel);
                            break;
                        }
                    case ImageLayout.Tile:      // Image {0,0} = ClientRectangle {0,0} with no scaling, and repeated to fill
                        {
                            // Honour iattrib here? Doesn't seem fully possible?
                            using (var tb = new TextureBrush(image, WrapMode.Tile))
                                e.Graphics.FillRectangle(tb, this.ClientRectangle);
                            break;
                        }
                    case ImageLayout.Center:    // Image center = ClientRectangle center with no scaling
                        {
                            var imgRc = new Rectangle(Point.Empty, image.Size);
                            var dstRc = this.ClientRectangle;

                            if (image.Width > this.ClientSize.Width)
                            {
                                imgRc.X = (image.Width - this.ClientSize.Width) / 2;
                                imgRc.Width = dstRc.Width;
                            }
                            else
                            {
                                dstRc.X = (this.ClientSize.Width - image.Width) / 2;
                                dstRc.Width = imgRc.Width;
                            }
                            if (image.Height > this.ClientSize.Height)
                            {
                                imgRc.Y = (image.Height - this.ClientSize.Height) / 2;
                                imgRc.Height = dstRc.Height;
                            }
                            else
                            {
                                dstRc.Y = (this.ClientSize.Height - image.Height) / 2;
                                dstRc.Height = imgRc.Height;
                            }
                            if (iattrib != null)
                                e.Graphics.DrawImage(image, dstRc, imgRc.X, imgRc.Y, imgRc.Width, imgRc.Height, GraphicsUnit.Pixel, iattrib);
                            else
                                e.Graphics.DrawImage(image, dstRc, imgRc.X, imgRc.Y, imgRc.Width, imgRc.Height, GraphicsUnit.Pixel);
                            break;
                        }
                    case ImageLayout.Stretch:   // Pin the image corners to our corners without any concerns for aspect ratio
                        {
                            if (iattrib != null)
                                e.Graphics.DrawImage(image, this.ClientRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, iattrib);
                            else
                                e.Graphics.DrawImage(image, this.ClientRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                            break;
                        }
                    case ImageLayout.Zoom:      // Like Stretch, but centered and mindful of the image aspect ratio
                    default:
                        {
                            var dstRc = this.ClientRectangle;
                            var wRatio = (float)dstRc.Width / (float)image.Width;
                            var hRatio = (float)dstRc.Height / (float)image.Height;
                            if (wRatio < hRatio)
                            {
                                dstRc.Height = (int)((image.Height * wRatio) + 0.5);
                                dstRc.Y = (this.ClientSize.Height - dstRc.Height) / 2;
                            }
                            else
                            {
                                dstRc.Width = (int)((image.Width * hRatio) + 0.5);
                                dstRc.X = (this.ClientSize.Width - dstRc.Width) / 2;
                            }

                            if (iattrib != null)
                                e.Graphics.DrawImage(image, dstRc, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, iattrib);
                            else
                                e.Graphics.DrawImage(image, dstRc, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.TextChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (imageselected == ImageType.Text || imageselected == ImageType.InverseText)
            {
                Invalidate();
            }
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The character to process.</param>
        /// <returns><c>true</c> if the character was processed as a mnemonic by the control; otherwise, <c>false</c>.</returns>
        protected override bool ProcessMnemonic(char charCode)
        {
            if (this.UseMnemonic && this.Enabled && this.Visible && IsMnemonic(charCode, this.Text))
            {
                PerformClick();
                return true;
            }
            return base.ProcessMnemonic(charCode);
        }

        // Change the current DrawState, and invalidate as needed.
        private void SetDrawState(DrawState value)
        {
            Debug.Assert((Enabled && value != DrawState.Disabled) || (!Enabled && value == DrawState.Disabled),
                $"New DrawState ({value}) doesn't align with Enabled state ({(Enabled ? "Enabled" : "Disabled")}).");

            if (drawState != value)
            {
                var old = drawState;
                drawState = value;
                if (MouseSelectedColorEnable || old == DrawState.Disabled || value == DrawState.Disabled)
                    Invalidate();   // only invalidate if required
            }
        }

        public bool Theme(Theme t, Font fnt)
        {
            BackColor = Color.Transparent;
            ForeColor = t.LabelColor;
            MouseOverColor = t.LabelColor.Multiply(t.MouseOverScaling);
            MouseSelectedColor = t.LabelColor.Multiply(t.MouseSelectedScaling);
            BorderWidth = 2;
            BorderColor = t.GridBorderLines;
            ButtonDisabledScaling = t.DisabledScaling;

            ColorMap colormap = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images
            colormap.OldColor = Color.White;                                                        // white is defined as the forecolour
            colormap.NewColor = ForeColor;
            SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap });

            return false;
        }

        #endregion
    }
}
