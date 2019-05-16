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
        public Color CheckBoxColor { get; set; } = Color.Gray;       // border
        public Color CheckBoxInnerColor { get; set; } = Color.White;
        public Color CheckColor { get; set; } = Color.DarkBlue;
        public Color MouseOverColor { get; set; } = Color.CornflowerBlue;
        public float TickBoxReductionRatio { get; set; } = 0.75f;
        public Image ImageUnchecked = null;                         // set both this and Image to draw a image instead of the check. 
        public Image ImageIndeterminate = null;                     // can set this, if required, if using indeterminate value
        public float ImageButtonDisabledScaling { get; set; } = 0.5F;   // scaling when disabled
        public ImageLayout ImageLayout { get { return imagelayout; } set { imagelayout = value; Invalidate(); } }

        public ImageAttributes DrawnImageAttributesEnabled = null;         // Image override (colour etc) for images using Image
        public ImageAttributes DrawnImageAttributesDisabled = null;         // Image override (colour etc) for images using Image

        private Font FontToUse = null;
        private ImageLayout imagelayout = ImageLayout.Center;               // new! image layout

        public void SetDrawnBitmapRemapTable(ColorMap[] remap, float[][] colormatrix = null)
        {
            ControlHelpersStaticFunc.ComputeDrawnPanel(out DrawnImageAttributesEnabled, out DrawnImageAttributesDisabled, ImageButtonDisabledScaling, remap, colormatrix);
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
            if (Appearance == Appearance.Button || FlatStyle == FlatStyle.System || FlatStyle == FlatStyle.Standard )
                base.OnPaint(e);
            else
            {
                Rectangle rect = ClientRectangle;

                using (Brush br = new SolidBrush(this.BackColor))
                    e.Graphics.FillRectangle(br, rect);

                int reduce = (int)(rect.Height * TickBoxReductionRatio);
                rect.Y += (rect.Height - reduce) / 2;
                rect.Height = reduce;
                rect.Width = rect.Height;

                Rectangle textarea = ClientRectangle;
                textarea.X = rect.Width;
                textarea.Width -= rect.Width;
                Color basecolor = (mouseover) ? MouseOverColor : CheckBoxColor;

                if (Image == null)      // don't drawn when image defined
                {
                    using (Pen outer = new Pen(basecolor))
                        e.Graphics.DrawRectangle(outer, rect);
                }

                rect.Inflate(-1, -1);

                Rectangle checkarea = rect;
                checkarea.Width++; checkarea.Height++;          // convert back to area

                if (Enabled)
                {
                    if (Image == null)
                    {
                        using (Pen second = new Pen(CheckBoxInnerColor, 1F))
                            e.Graphics.DrawRectangle(second, rect);

                        rect.Inflate(-1, -1);

                        if (FlatStyle == FlatStyle.Flat)
                        {
                            using (Brush inner = new SolidBrush(basecolor))
                                e.Graphics.FillRectangle(inner, rect);      // fill slightly over size to make sure all pixels are painted
                        }
                        else
                        {
                            using (Brush inner = new LinearGradientBrush(rect, CheckBoxInnerColor, basecolor, 225))
                                e.Graphics.FillRectangle(inner, rect);      // fill slightly over size to make sure all pixels are painted
                        }

                        using (Pen third = new Pen(basecolor, 1F))
                            e.Graphics.DrawRectangle(third, rect);
                    }
                }
                else
                {
                    using (Brush disabled = new SolidBrush(CheckBoxInnerColor))
                    {
                        e.Graphics.FillRectangle(disabled, checkarea);
                    }
                }

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (StringFormat fmt = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.FitBlackBox })
                {
                    using (Brush textb = new SolidBrush((Enabled) ? this.ForeColor : this.ForeColor.Multiply(0.5F)))
                    {
                        if (FontToUse == null || FontToUse.FontFamily != Font.FontFamily || FontToUse.Style != Font.Style)
                            FontToUse = e.Graphics.GetFontToFitRectangle(this.Text, Font, textarea, fmt);

                        e.Graphics.DrawString(this.Text, FontToUse, textb, textarea, fmt);
                    }
                }

                if (Image != null && ImageUnchecked != null)
                {
                    Image image = CheckState == CheckState.Checked ? Image : ( (CheckState == CheckState.Indeterminate && ImageIndeterminate != null ) ? ImageIndeterminate : ImageUnchecked);
                    Size isize = (imagelayout == ImageLayout.Stretch) ? checkarea.Size : Image.Size;

                    if (DrawnImageAttributesEnabled != null)
                        e.Graphics.DrawImage(image, new Rectangle(0, 0, isize.Width, isize.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, (Enabled) ? DrawnImageAttributesEnabled : DrawnImageAttributesDisabled);
                    else
                        e.Graphics.DrawImage(image, new Rectangle(0, 0, isize.Width, isize.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                }
                else
                {
                    Color c1 = Color.FromArgb(200, CheckColor);
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
                    else if ( CheckState == CheckState.Indeterminate )
                    {
                        Size cb = new Size(checkarea.Width - 5, checkarea.Height - 5);
                        if (cb.Width > 0 && cb.Height > 0)
                        {
                            using (Brush br = new SolidBrush(c1))
                            {
                                e.Graphics.FillRectangle(br, new Rectangle(new Point(checkarea.X + 2, checkarea.Y + 2), cb ));
                            }
                        }
                    }
                }

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

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

        private bool mouseover = false;
    }
}

