/*
 * Copyright © 2016-2019 EDDiscovery development team
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtCheckBox : CheckBox
    {
        // Flatstyle Popout/Flat mode, not Apprearance Button only
        public Color CheckBoxColor { get; set; } = Color.Gray;          // Normal only border area colour
        public Color CheckBoxInnerColor { get; set; } = Color.White;    // Normal only inner colour
        public Color CheckColor { get; set; } = Color.DarkBlue;         // Button - back colour when checked, Normal - check colour
        public Color MouseOverColor { get; set; } = Color.CornflowerBlue; // both - Mouse over 

        public float TickBoxReductionRatio { get; set; } = 0.75f;       // Normal - size reduction
        public Image ImageUnchecked { get; set; } = null;               // Both - set image when unchecked.  Also set Image
        public Image ImageIndeterminate { get; set; } = null;           // Both - optional - can set this, if required, if using indeterminate value
        public float ImageButtonDisabledScaling { get; set; } = 0.5F;   // Both - scaling when disabled - must call SetDrawnBitmapRemapTable
        public float CheckBoxDisabledScaling { get; set; } = 0.5F;      // Both - text and check box scaling when disabled
        public ImageLayout ImageLayout { get { return imagelayout; } set { imagelayout = value; Invalidate(); } }   // Both. Also use TextLayout for Buttons

        public ImageAttributes DrawnImageAttributesEnabled = null;         // Image override (colour etc) for images using Image
        public ImageAttributes DrawnImageAttributesDisabled = null;         // Image override (colour etc) for images using Image

        private Font FontToUse = null;
        private ImageLayout imagelayout = ImageLayout.Center;           // image layout

        public void SetDrawnBitmapRemapTable(ColorMap[] remap, float[][] colormatrix = null)        // call to set up disable scaling
        {
            DrawingHelpersStaticFunc.ComputeDrawnPanel(out DrawnImageAttributesEnabled, out DrawnImageAttributesDisabled, ImageButtonDisabledScaling, remap, colormatrix);
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
            if ( FlatStyle == FlatStyle.System || FlatStyle == FlatStyle.Standard )
                base.OnPaint(e);
            else
            {
                bool hasimages = Image != null;

                using (Brush br = new SolidBrush(this.BackColor))
                    e.Graphics.FillRectangle(br, ClientRectangle);

                if (Appearance == Appearance.Button)
                {
                    if ( Enabled )
                    {
                        Rectangle marea = ClientRectangle;
                        marea.Inflate(-2, -2);

                        if ( mouseover )
                        {
                            using (Brush mover = new SolidBrush(MouseOverColor))
                                e.Graphics.FillRectangle(mover, marea);
                        }
                        else if ( CheckState == CheckState.Checked )
                        {
                            using (Brush mover = new SolidBrush(CheckColor))
                                e.Graphics.FillRectangle(mover, marea);
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

                    float discaling = Enabled ? 1.0f : CheckBoxDisabledScaling;

                    Color checkboxbasecolour = (Enabled && mouseover) ? MouseOverColor : CheckBoxColor.Multiply(discaling);

                    if (!hasimages)      // draw the over box of the checkbox if no images
                    {
                        using (Pen outer = new Pen(checkboxbasecolour))
                            e.Graphics.DrawRectangle(outer, tickarea);
                    }

                    tickarea.Inflate(-1, -1);

                    Rectangle checkarea = tickarea;
                    checkarea.Width++; checkarea.Height++;          // convert back to area

                    //                System.Diagnostics.Debug.WriteLine("Owner draw " + Name + checkarea + rect);

                    if (hasimages)
                    {
                        if (Enabled && mouseover)                // if mouse over, draw a nice box around it
                        {
                            using (Brush mover = new SolidBrush(MouseOverColor))
                            {
                                e.Graphics.FillRectangle(mover, checkarea);
                            }
                        }
                    }
                    else
                    {                                   // in no image, we draw a set of boxes
                        using (Pen second = new Pen(CheckBoxInnerColor.Multiply(discaling), 1F))
                            e.Graphics.DrawRectangle(second, tickarea);

                        tickarea.Inflate(-1, -1);

                        if (FlatStyle == FlatStyle.Flat)
                        {
                            using (Brush inner = new SolidBrush(checkboxbasecolour.Multiply(discaling)))
                                e.Graphics.FillRectangle(inner, tickarea);      // fill slightly over size to make sure all pixels are painted
                        }
                        else
                        {
                            using (Brush inner = new LinearGradientBrush(tickarea, CheckBoxInnerColor.Multiply(discaling), checkboxbasecolour, 225))
                                e.Graphics.FillRectangle(inner, tickarea);      // fill slightly over size to make sure all pixels are painted
                        }

                        using (Pen third = new Pen(checkboxbasecolour.Multiply(discaling), 1F))
                            e.Graphics.DrawRectangle(third, tickarea);
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
                        Color c1 = Color.FromArgb(200, CheckColor.Multiply(discaling));
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
                using (Brush textb = new SolidBrush(Enabled ? this.ForeColor : this.ForeColor.Multiply(CheckBoxDisabledScaling)))
                {
                    if (FontToUse == null || FontToUse.FontFamily != Font.FontFamily || FontToUse.Style != Font.Style)
                        FontToUse = g.GetFontToFit(this.Text, Font, box.Size, fmt);

                    g.DrawString(this.Text, FontToUse, textb, box, fmt);
                }
            }
        }

        private bool mouseover = false;
    }
}

