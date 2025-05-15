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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtCheckBox : CheckBox, IThemeable
    {
        // Flatstyle Popout/Flat mode, not Apprearance Button only
        public Color CheckBoxColor { get; set; } = Color.Gray;          // Normal only border area colour
        public Color CheckBoxInnerColor { get; set; } = Color.White;    // Normal only inner colour
        public Color CheckColor { get; set; } = Color.DarkBlue;         // Button - back colour when checked 1, Normal - check colour
        public Color CheckColor2 { get; set; } = Color.DarkBlue;        // Button - back colour when checked 2, Normal - unused
        public float MouseOverScaling { get; set; } = 1.3F;
        public float MouseSelectedScaling { get; set; } = 1.3F;
        public float TickBoxReductionRatio { get; set; } = 0.75f;       // Normal - size reduction
        public Image ImageUnchecked { get; set; } = null;               // Both - set image when unchecked.  Also set Image
        public Image ImageIndeterminate { get; set; } = null;           // Both - optional - can set this, if required, if using indeterminate value

        public float DisabledScaling { get; set; } = 0.5F;   // Both - scaling when disabled - must call SetDrawnBitmapRemapTable
        public float ButtonGradientDirection { get { return buttongradientdirection; } set { buttongradientdirection = value; Invalidate(); } }
        public float CheckBoxGradientDirection { get { return checkboxgradientdirection; } set { checkboxgradientdirection = value; Invalidate(); } }
        public ImageLayout ImageLayout { get { return imagelayout; } set { imagelayout = value; Invalidate(); } }   // Both. Also use TextLayout for Buttons

        public ImageAttributes DrawnImageAttributesEnabled = null;         // Image override (colour etc) for images using Image
        public ImageAttributes DrawnImageAttributesDisabled = null;         // Image override (colour etc) for images using Image

        public void SetDrawnBitmapRemapTable(ColorMap[] remap, float[][] colormatrix = null)        // call to set up disable scaling
        {
            DrawingHelpersStaticFunc.ComputeDrawnPanel(out DrawnImageAttributesEnabled, out DrawnImageAttributesDisabled, DisabledScaling, remap, colormatrix);
        }

        public ExtCheckBox() : base()
        {
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            FontToUse = null;   // need to reestimate
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            FontToUse = null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (FlatStyle == FlatStyle.System || FlatStyle == FlatStyle.Standard)
            {
                base.OnPaint(e);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Control {Name} colour {BackColor}");

                if (BackColor == Color.Transparent)     // if we are transparent, only way i've found to make this work is to grab the parent image
                {
                    using (var backImageControlBitmap = new Bitmap(Width, Height))
                    {
                        using (Graphics backGraphics = Graphics.FromImage(backImageControlBitmap))
                        {
                            if (Parent != null)     // double check!
                            {
                                // we want the graphics to be offset painted into bitmap 0,0 at our location, so shift axis
                                backGraphics.TranslateTransform((float)-Location.X, (float)-Location.Y);
                                PaintEventArgs ep = new PaintEventArgs(backGraphics, ClientRectangle);
                                InvokePaintBackground(Parent, ep); // we force it to paint into our bitmap
                                InvokePaint(Parent, ep);
                            }
                        }

                   //     backImageControlBitmap.Save(@"c:\code\bitmap.bmp");
                        e.Graphics.DrawImage(backImageControlBitmap, new Point(0, 0));
                    }
                }
                else
                {
                    using (Brush br = new SolidBrush(BackColor))
                        e.Graphics.FillRectangle(br, ClientRectangle);
                }

                bool hasimages = Image != null;

                if (Appearance == Appearance.Button)
                {
                    Rectangle area = ClientRectangle;

                    if (FlatAppearance.BorderSize > 0)
                    {
                        using (var p = new Pen(FlatAppearance.BorderColor))     // only 1 pixel wide border
                        {
                            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, area.Width - 1, area.Height - 1));
                        }

                        area.Inflate(-1, -1);
                    }

                    if (Enabled)
                    {
                        if (mouseover || CheckState == CheckState.Checked)
                        {
                            Color bk1 = CheckColor.Multiply(mouseover ? MouseOverScaling : 1.0f);
                            Color bk2 = CheckColor2.Multiply(mouseover ? MouseOverScaling : 1.0f);

                            if (FlatStyle == FlatStyle.Flat)
                            {
                                using (Brush brush = new SolidBrush(bk1))
                                    e.Graphics.FillRectangle(brush, area);
                            }
                            else
                            {
                                using (var brush = new LinearGradientBrush(area, bk1, bk2, ButtonGradientDirection))
                                    e.Graphics.FillRectangle(brush, area);
                            }
                        }
                    }

                    if (hasimages)
                        DrawImage(ClientRectangle, e.Graphics);

                    var txalign = Environment.OSVersion.Platform == PlatformID.Win32NT ? RtlTranslateAlignment(TextAlign) : TextAlign;      // MONO Bug cover over

                    using (var fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(txalign))
                    {
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        DrawText(ClientRectangle, e.Graphics, fmt);
                        e.Graphics.SmoothingMode = SmoothingMode.Default;
                    }
                }
                else
                {
                    Rectangle tickarea = ClientRectangle;
                    Rectangle textarea = ClientRectangle;

                    int reduce = (int)(tickarea.Height * TickBoxReductionRatio);
                    tickarea.Y += (tickarea.Height - reduce) / 2;
                    tickarea.Height = reduce;
                    tickarea.Width = tickarea.Height;

                    if (CheckAlign == ContentAlignment.MiddleRight)
                    {
                        tickarea.X = ClientRectangle.Width - tickarea.Width;
                        textarea.Width -= tickarea.Width;
                    }
                    else
                    {
                        textarea.X = tickarea.Width;
                        textarea.Width -= tickarea.Width;
                    }

                    float scaling = Enabled ? (mouseover ? MouseOverScaling : 1.0f) : DisabledScaling;

                    Color checkboxbasecolour = CheckBoxColor.Multiply(scaling);
                    Color checkboxbasecolour2 = (FlatStyle == FlatStyle.Flat ? CheckBoxColor : CheckBoxInnerColor).Multiply(scaling);

                    if (!hasimages)      // draw the over box of the checkbox if no images
                    {
                        using (Pen outer = new Pen(checkboxbasecolour))
                            e.Graphics.DrawRectangle(outer, tickarea);
                    }

                    tickarea.Inflate(-1, -1);

                    if (!tickarea.Size.IsEmpty)
                    {
                        Rectangle checkarea = tickarea;
                        checkarea.Width++; checkarea.Height++;          // convert back to area

                        //                System.Diagnostics.Debug.WriteLine("Owner draw " + Name + checkarea + rect);

                        if (hasimages)
                        {
                            if (Enabled && mouseover)
                            {
                                using (Brush mover = new LinearGradientBrush(tickarea, checkboxbasecolour2, checkboxbasecolour, CheckBoxGradientDirection))
                                {
                                    e.Graphics.FillRectangle(mover, checkarea);
                                }
                            }
                        }
                        else
                        {                                   // in no image, we draw a set of boxes
                            using (Pen second = new Pen(CheckBoxInnerColor.Multiply(scaling), 1F))
                                e.Graphics.DrawRectangle(second, tickarea);

                            tickarea.Inflate(-1, -1);

                            if (!tickarea.Size.IsEmpty)
                            {
                                using (Brush inner = new LinearGradientBrush(tickarea, checkboxbasecolour2, checkboxbasecolour, CheckBoxGradientDirection))
                                    e.Graphics.FillRectangle(inner, tickarea);      // fill slightly over size to make sure all pixels are painted

                                using (Pen third = new Pen(checkboxbasecolour, 1F))
                                    e.Graphics.DrawRectangle(third, tickarea);
                            }
                        }

                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        using (StringFormat fmt = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.FitBlackBox })
                            DrawText(textarea, e.Graphics, fmt);

                        if (hasimages)
                        {
                            DrawImage(checkarea, e.Graphics);
                        }
                        else
                        {
                            Color c1 = Color.FromArgb(200, CheckColor.Multiply(scaling));
                            if (CheckState == CheckState.Checked)
                            {
                                Point pt1 = new Point(checkarea.X + 2, checkarea.Y + checkarea.Height / 2 - 1);
                                Point pt2 = new Point(checkarea.X + checkarea.Width / 2 - 1, checkarea.Bottom - 2);
                                Point pt3 = new Point(checkarea.X + checkarea.Width - 2, checkarea.Y);

                                using (Pen pcheck = new Pen(c1, 2.0F))
                                {
                                    e.Graphics.DrawLine(pcheck, pt1, pt2);
                                    e.Graphics.DrawLine(pcheck, pt2, pt3);
                                }
                            }
                            else if (CheckState == CheckState.Indeterminate)
                            {
                                Size cb = new Size(checkarea.Width - 5, checkarea.Height - 5);
                                if (cb.Width > 0 && cb.Height > 0)
                                {
                                    using (Brush br = new SolidBrush(c1))
                                    {
                                        e.Graphics.FillRectangle(br, new Rectangle(new Point(checkarea.X + 2, checkarea.Y + 2), cb));
                                    }
                                }
                            }
                        }
                    }
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                }

            }
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            mouseover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);
            mouseover = false;
            Invalidate();
        }

        private void DrawImage(Rectangle box, Graphics g)
        {
            Image image = CheckState == CheckState.Checked ? Image : ((CheckState == CheckState.Indeterminate && ImageIndeterminate != null) ? ImageIndeterminate : (ImageUnchecked!=null ?ImageUnchecked: Image));
            Size isize = (imagelayout == ImageLayout.Stretch) ? box.Size : image.Size;
            Rectangle drawarea = ImageAlign.ImagePositionFromContentAlignment(box, isize);

            //System.Diagnostics.Debug.WriteLine("Image for " + Name + " " + Enabled + " " + DrawnImageAttributesEnabled);

            if (DrawnImageAttributesEnabled != null)
                g.DrawImage(image, drawarea, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, (Enabled) ? DrawnImageAttributesEnabled : DrawnImageAttributesDisabled);
            else
                g.DrawImage(image, drawarea, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
        }

        private void DrawText(Rectangle box, Graphics g, StringFormat fmt)
        {
            if (this.Text.HasChars())
            {
                using (Brush textb = new SolidBrush(Enabled ? this.ForeColor : this.ForeColor.Multiply(DisabledScaling)))
                {
                    if (FontToUse == null || FontToUse.FontFamily != Font.FontFamily || FontToUse.Style != Font.Style)
                        FontToUse = g.GetFontToFit(this.Text, Font, box.Size, fmt);

                    g.DrawString(this.Text, FontToUse, textb, box, fmt);
                }
            }
        }

        public bool Theme(Theme t, Font fnt)
        {
            BackColor = Color.Transparent;
            ForeColor = t.CheckBoxText;
            DisabledScaling = t.DisabledScaling;
            MouseOverScaling = t.MouseOverScaling;
            MouseSelectedScaling = t.MouseSelectedScaling;

            if (Appearance == Appearance.Button)
            {
                CheckColor = t.CheckBoxButtonTickedBack;
                CheckColor2 = t.CheckBoxButtonTickedBack2;
                ButtonGradientDirection = t.CheckBoxButtonGradientDirection;
                FlatAppearance.BorderColor = t.CheckBoxBorderColor;
                FlatAppearance.BorderSize = 1;
            }
            else
            {
                CheckBoxColor = t.CheckBoxBack;
                CheckBoxInnerColor = t.CheckBoxBack2;
                CheckColor = t.CheckBoxTick;
                CheckBoxGradientDirection = t.CheckBoxBackGradientDirection;
                TickBoxReductionRatio = t.CheckBoxTickSize;
            }

            FlatStyle = t.ButtonFlatStyle;

            if (Image != null)
            {
                System.Drawing.Imaging.ColorMap colormap = new System.Drawing.Imaging.ColorMap();
                colormap.OldColor = Color.White;                                                        // white is defined as the forecolour
                colormap.NewColor = ForeColor;
                SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap });

                ImageLayout = ImageLayout.Stretch;
            }

            Invalidate();

            return false;
        }

        private bool mouseover = false;
        private Font FontToUse = null;
        private ImageLayout imagelayout = ImageLayout.Center;           // image layout
        private float checkboxgradientdirection = 225F;
        private float buttongradientdirection = 90F;
    }
}

