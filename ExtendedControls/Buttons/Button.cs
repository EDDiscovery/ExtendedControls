/*
 * Copyright 2016-2025 EDDiscovery development team
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ExtendedControls
{
    /// <summary>
    /// Represents a customizable Windows <see cref="Button"/> <see cref="Control"/>.
    /// </summary>
    public class ExtButton : Button, IThemeable, ITranslatableControl
    {
        public ExtButton() : base() { }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new Image BackgroundImage
        {
            get { return null; }
            set { throw new InvalidOperationException("The BackgroundImage property is not supported by this control. Use Image instead."); }
        }

        public Color BackColor2 { get { return backColor2; } set { backColor2 = value; Invalidate(); } }
        public float MouseOverScaling { get; set; } = 1.3F;
        public float MouseSelectedScaling { get; set; } = 1.3F;
        public float ButtonDisabledScaling { get { return buttonDisabledScaling; } set { buttonDisabledScaling = value; Invalidate(); } }
        public float GradientDirection { get { return gradientdirection; } set { gradientdirection = value; Invalidate(); } }

        public bool TranslateDoChildren => false;

        /// <summary>
        /// Specify a <see cref="Color"/> <paramref name="remap"/> table and corresponding <see cref="ColorMatrix"/>.
        /// </summary>
        /// <param name="remap">An array of <see cref="ColorMap"/> items detailing how colors should be remapped in the
        /// provided <see cref="ButtonBase.Image"/>.</param>
        /// <param name="colormatrix">If not null, these values will be used to construct a <see cref="ColorMatrix"/>
        /// for adjusting the intensity of the displayed <see cref="ButtonBase.Image"/> colors.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="remap"/> is <c>null</c>.</exception>
        public void SetDrawnBitmapRemapTable(ColorMap[] remap, float[][] colormatrix = null)
        {
            if (remap == null)
                throw new ArgumentNullException(nameof(remap));

            drawnImageAttributesEnabled?.Dispose();
            drawnImageAttributesDisabled?.Dispose();

            DrawingHelpersStaticFunc.ComputeDrawnPanel(out drawnImageAttributesEnabled, out drawnImageAttributesDisabled, buttonDisabledScaling, remap, colormatrix);
        }


        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="ExtButton"/> and optionally releases the managed
        /// resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to
        /// release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                drawnImageAttributesDisabled?.Dispose();
                drawnImageAttributesEnabled?.Dispose();
            }
            drawnImageAttributesDisabled = null;
            drawnImageAttributesEnabled = null;
            base.Dispose(disposing);
        }

        /// <summary>
        /// Raises the <see cref="Control.EnabledChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
                SetDrawState(DrawState.Normal);
            else
                SetDrawState(DrawState.Disabled);

            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.KeyDown"/> event.
        /// </summary>
        /// <param name="kevent">A <see cref="KeyEventArgs"/> containing the event data.</param>
        protected override void OnKeyDown(KeyEventArgs kevent)
        {
            // Enter/Return and mnemonics shall not adjust the appearance, while space gets treated just like the left mouse button.
            if (kevent.KeyData == Keys.Space)
                SetDrawState(DrawState.Click);

            base.OnKeyDown(kevent);
        }

        /// <summary>
        /// Raises the <see cref="Control.KeyUp"/> event.
        /// </summary>
        /// <param name="kevent">A <see cref="KeyEventArgs"/> containing the event data.</param>
        protected override void OnKeyUp(KeyEventArgs kevent)
        {
            if (kevent.KeyData == Keys.Space)
                SetDrawState(DrawState.Normal);

            base.OnKeyUp(kevent);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseDown"/> event.
        /// </summary>
        /// <param name="mevent">A <see cref="MouseEventArgs"/> containing the event data.</param>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
                SetDrawState(DrawState.Click);

            base.OnMouseDown(mevent);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseLeave"/> event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs"/> containing the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            SetDrawState(DrawState.Normal);

            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.Click"/> event.
        /// </summary>
        /// <param name="mevent">A <see cref="MouseEventArgs"/> containing the event data.</param>
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            // Allow a very long click to come and go from our bounds, updating the draw state the whole time.
            if (ClientRectangle.Contains(mevent.Location))
                SetDrawState(mevent.Button == MouseButtons.Left ? DrawState.Click : DrawState.Hover);
            else
                SetDrawState(DrawState.Normal);       // OnMouseLeave doesn't actually fire until /after/ LMB is released, so clear this here.

            base.OnMouseMove(mevent);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseUp"/> event.
        /// </summary>
        /// <param name="mevent">A <see cref="MouseEventArgs"/> containing the event data.</param>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (drawState == DrawState.Click)
                SetDrawState(DrawState.Hover);

            base.OnMouseUp(mevent);
        }

        // implements Flat and Gradient from Oct 26.
        // System should not call it but there for protection
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (FlatStyle == FlatStyle.System)      
            {
                base.OnPaint(pe);
            }
            else
            {
                // we use BorderSize for the outline, and we want something left inside that, a minimum of 3 pixels are needed (2 for border)

                var cr = ClientRectangle;
                // debugcr = new Rectangle(0, 0, 3, 3);

                if (cr.Width<= 1+FlatAppearance.BorderSize || cr.Height<= 1+FlatAppearance.BorderSize)
                    return;

                Color colBack = BackColor;
                Color colBack2 = BackColor2;
                Color colBorder = FlatAppearance.BorderColor;

                if (drawState == DrawState.Disabled)
                {
                    colBack = colBack.Multiply(buttonDisabledScaling);
                    colBack2 = colBack2.Multiply(buttonDisabledScaling);
                    colBorder = colBorder.Multiply(buttonDisabledScaling);
                }
                else if (drawState != DrawState.Normal)
                {
                    if (colBack.GetBrightness() < 0.05f)        // ensure a color for these states, if background is back it won't do anything
                    {
                        colBack2 = colBack = Color.FromArgb(255, 32, 32, 32);
                    }

                    if (drawState == DrawState.Hover)
                    {
                        colBack = colBack.Multiply(MouseOverScaling);
                        colBack2 = colBack2.Multiply(MouseOverScaling);
                        colBorder = colBorder.Multiply(MouseOverScaling);
                    }
                    else
                    {
                        colBack = colBack.Multiply(MouseSelectedScaling);
                        colBack2 = colBack2.Multiply(MouseSelectedScaling);
                        colBorder = colBorder.Multiply(MouseSelectedScaling);
                    }
                }

                Rectangle border = new Rectangle(0, 0, cr.Width - FlatAppearance.BorderSize, cr.Height - FlatAppearance.BorderSize);

               // System.Diagnostics.Debug.WriteLine($"\r\nControl Button {Name} {cr}, {FlatStyle}, brd {border} back {colBack}-{colBack2} border {colBorder}:{FlatAppearance.BorderSize}");

                //System.Diagnostics.Debug.WriteLine($"Control Button Background {Name} {colBack} {GradientDirection}");
                using (var b = new LinearGradientBrush(cr, colBack, colBack2, GradientDirection))
                    pe.Graphics.FillRectangle(b, cr);       // linear grad brushes do not respect smoothing mode, btw

                pe.Graphics.SmoothingMode = SmoothingMode.None;

                if (FlatAppearance.BorderSize>0)        // do we want a border to our button
                {
                    //colBorder = Color.Purple;
                    using (var p = new Pen(colBorder,FlatAppearance.BorderSize))
                        pe.Graphics.DrawRectangle(p, border);
                }

                Rectangle buttonarea = new Rectangle(FlatAppearance.BorderSize, FlatAppearance.BorderSize, cr.Width - FlatAppearance.BorderSize * 2, cr.Height - FlatAppearance.BorderSize * 2);

                if (buttonarea.Width < 1 || buttonarea.Height < 1)  // triage for crashing and no point drawing any more in the button area if its too small, it will except
                    return;

                if (Image != null )
                {
                    // if we are in the centre, we use the whole of the buttonarea, else we use the button height.
                    Size isize = (ImageAlign == ContentAlignment.MiddleCenter) ? new Size(buttonarea.Width, buttonarea.Height) : new Size(buttonarea.Height, buttonarea.Height);

                    var destrect = ImageAlign.ImagePositionFromContentAlignment(buttonarea, isize);

                    // System.Diagnostics.Debug.WriteLine($"Control Button Image {this.Name} {buttonarea} {destrect} {Image.Width}x {Image.Height} {ImageAlign}");

                    if ((Enabled && drawnImageAttributesEnabled != null) || (!Enabled && drawnImageAttributesDisabled != null))
                    {
                        //using (var b = new SolidBrush(Color.FromArgb(128, 64, 0, 0))) pe.Graphics.FillRectangle(b, destrect);       // linear grad brushes do not respect smoothing mode, btw
                        pe.Graphics.DrawImage(Image, destrect, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, (Enabled) ? drawnImageAttributesEnabled : drawnImageAttributesDisabled);
                    }
                    else
                    {
                        pe.Graphics.DrawImage(Image, destrect,  0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel);
                    }
                }

                pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if (!string.IsNullOrEmpty(Text))
                {
                    var txalign = Environment.OSVersion.Platform == PlatformID.Win32NT ? RtlTranslateAlignment(TextAlign) : TextAlign;      // MONO Bug cover over

                    using (var fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(txalign))
                    {
                        using (Brush textb = new SolidBrush((Enabled) ? this.ForeColor : this.ForeColor.Multiply(ButtonDisabledScaling)))
                        {
                            if (this.UseMnemonic)
                                fmt.HotkeyPrefix = this.ShowKeyboardCues ? HotkeyPrefix.Show : HotkeyPrefix.Hide;
                            if (this.AutoEllipsis)
                                fmt.Trimming = StringTrimming.EllipsisCharacter;
                            pe.Graphics.DrawString(this.Text, this.Font, textb, buttonarea, fmt);
                        }
                    }
                }

                pe.Graphics.SmoothingMode = SmoothingMode.None;

                if (Focused && ShowFocusCues)
                {
                    using (var p = new Pen(colBorder))
                    {
                        Rectangle rcFocus = border;

                        // Thicken the standard border by 1px.
                        rcFocus.Inflate(-1, -1);
                        pe.Graphics.DrawRectangle(p, rcFocus);

                        // Thicken that by an additional 1px, using something similar to ControlPaint.DrawFocusRectangle, but capable of honouring forecolour.
                        rcFocus.Inflate(-1, -1);
                        p.DashStyle = DashStyle.Dash;
                        p.DashPattern = new[] { 1f, 1f };
                        pe.Graphics.DrawRectangle(p, rcFocus);
                    }
                }

                pe.Graphics.SmoothingMode = SmoothingMode.Default;
            }
        }


        // Change the current DrawState, and invalidate as needed.
        private void SetDrawState(DrawState state)
        {
            if (drawState != state)
            {
                drawState = state;
                if (FlatStyle == FlatStyle.Popup)
                    Invalidate();
            }
        }

        public bool Theme(Theme t, Font fnt)
        {
            ExtButton ctrl = this;
            ctrl.ForeColor = t.ButtonTextColor;

            if (ctrl.Text.HasChars())      // we autosize text to make it fit.. we do not autosize image buttons
                ctrl.AutoSize = true;

            //System.Diagnostics.Debug.WriteLine($"Control Theme {ctrl.Name} size {Size} {ctrl.FlatStyle}");

            // if in system mode, and no image, we can use the system theme

            if (t.IsButtonSystemStyle && Image == null)
            {
                ctrl.UseVisualStyleBackColor = true;
                ctrl.FlatStyle = FlatStyle.System;
            }
            else
            {
                // flatstyle, gradient, system when image

                ctrl.ButtonDisabledScaling = t.DisabledScaling;
                ctrl.GradientDirection = t.ButtonBackGradientDirection;
                ctrl.MouseOverScaling = t.MouseOverScaling;
                ctrl.MouseSelectedScaling = t.MouseSelectedScaling;

                if (ctrl.Image != null)     // any images, White and a gray (for historic reasons) gets replaced.
                {
                    System.Drawing.Imaging.ColorMap colormap1 = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images
                    colormap1.OldColor = Color.FromArgb(134, 134, 134);                                      // gray is defined as the forecolour
                    colormap1.NewColor = ctrl.ForeColor;
                    //System.Diagnostics.Debug.WriteLine("Theme Image in " + ctrl.Name + " Map " + colormap1.OldColor + " to " + colormap1.NewColor);

                    System.Drawing.Imaging.ColorMap colormap2 = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images
                    colormap2.OldColor = Color.FromArgb(255, 255, 255);                                      // and white is defined as the forecolour
                    colormap2.NewColor = ctrl.ForeColor;
                    //System.Diagnostics.Debug.WriteLine("Theme Image in " + ctrl.Name + " Map " + colormap2.OldColor + " to " + colormap2.NewColor);

                    ctrl.SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap1, colormap2 });     // used ButtonDisabledScaling note!
                }

                // if image, and no text or text over image centred (so over the image),
                // set background to form to make the back disappear

                if (ctrl.Image != null && (ctrl.Text.Length == 0 || (ctrl.TextAlign == ContentAlignment.MiddleCenter && ctrl.ImageAlign == ContentAlignment.MiddleCenter)))
                {
                    // composite auto scale buttons inherit parent back colour and have disabled scaling turned off

                    if (ctrl.Parent != null && ctrl.Parent is CompositeAutoScaleButton)       
                    {
                        ctrl.BackColor = ctrl.BackColor2 = ctrl.Parent.BackColor;
                        ctrl.ButtonDisabledScaling = 1.0F;          
                    }
                    else 
                    {
                        if (t.ImageButtonsHaveBackground)
                        {
                            ctrl.BackColor = t.ButtonBackColor;
                            ctrl.BackColor2 = t.IsButtonGradientStyle ? t.ButtonBackColor2 : t.ButtonBackColor;
                        }
                        else
                        {
                            ctrl.BackColor = ctrl.BackColor2 = t.Form;
                        }
                    }

                    if (t.ImageButtonsHaveBorders)
                    {
                        ctrl.FlatAppearance.BorderColor = t.ButtonBorderColor; 
                        ctrl.FlatAppearance.BorderSize = 1;
                    }
                    else
                    {
                        ctrl.FlatAppearance.BorderColor = t.Form;
                        ctrl.FlatAppearance.BorderSize = 0;
                    }
                }
                else
                {
                    // no image, gradient only uses back color 2

                    ctrl.BackColor = t.ButtonBackColor;
                    ctrl.BackColor2 = t.IsButtonGradientStyle ? t.ButtonBackColor2 : t.ButtonBackColor;
                    ctrl.FlatAppearance.BorderColor = t.ButtonBorderColor;
                    ctrl.FlatAppearance.BorderSize = 1;
                }

                // if we are here in system, then it had an image, and we use flat, else we use the style

                ctrl.FlatStyle = t.IsButtonSystemStyle ? FlatStyle.Flat :  t.ButtonFlatStyle;
            }

            return false;
        }

        // debug for scaling
        //protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        //{
        //    Size inits = Size;
        //    Point initl = Location;
        //    base.ScaleControl(factor, specified);
        //    if (Name == "buttonSize" || Name == "buttonExtEditAddOns")
        //    {
        //        System.Diagnostics.Debug.WriteLine("Scaling " + GetType().Name + " " + Name + " " + this.GetHeirarchy() + " " + inits + initl + " factor " + factor + " -> " + Size + Location);
        //    }
        //}

        private ImageAttributes drawnImageAttributesEnabled = null;         // Image override (colour etc) for background when using Image while Enabled.
        private ImageAttributes drawnImageAttributesDisabled = null;        // Image override (colour etc) for background when using Image while !Enabled.
        private enum DrawState { Disabled = -1, Normal = 0, Hover, Click };
        private DrawState drawState = DrawState.Normal;                    // The current state of our control, even if base is currently doing the painting.

        private float gradientdirection = 90F;
        private float buttonDisabledScaling = 0.5F;
        private Color backColor2 = Color.White;

    }
}
