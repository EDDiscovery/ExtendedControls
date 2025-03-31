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
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtRadioButton : RadioButton, IThemeable
    {
        public Color RadioButtonColor { get; set; } = Color.Gray;       // border of
        public Color RadioButtonInnerColor { get; set; } = Color.White; // inner border of

        public Color SelectedColor { get; set; } = Color.DarkBlue;      // the bullit eye color
        public Color SelectedColorRing { get; set; } = Color.Black;     // ring around it, Transparent for off
        public float RadioButtonDisabledScaling { get; set; } = 0.5F;              // when disabled
        public Color MouseOverColor { get; set; } = Color.CornflowerBlue;   // mouse over colour
        public float GradientDirection { get { return gradientdirection; } set { gradientdirection = value; Invalidate(); } }

        private Font FontToUse = null;
        private float gradientdirection = 225F;

        public ExtRadioButton() : base()
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
            FontToUse = null;   // need to reestimate
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Console.WriteLine("RB " + Name + ":" + ClientRectangle.ToString());

            if (FlatStyle == FlatStyle.System || FlatStyle == FlatStyle.Standard)
                base.OnPaint(e);
            else
            {
                Rectangle rect = ClientRectangle;

                using (Brush br = new SolidBrush(this.BackColor))
                    e.Graphics.FillRectangle(br, rect);

                rect.Height -= 6;
                rect.Y += 2;
                rect.Width = rect.Height;

                Rectangle textarea = ClientRectangle;
                textarea.X = rect.Width;
                textarea.Width -= rect.Width;

                Color basecolor = (mouseover) ? MouseOverColor : RadioButtonColor;

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (Brush outer = new SolidBrush(basecolor))
                    e.Graphics.FillEllipse(outer, rect);

                rect.Inflate(-1, -1);

                if (Enabled)
                {
                    using (Brush second = new SolidBrush(RadioButtonInnerColor))
                        e.Graphics.FillEllipse(second, rect);

                    rect.Inflate(-1, -1);

                    if (FlatStyle == FlatStyle.Flat)
                    {
                        using (Brush inner = new SolidBrush(basecolor))
                            e.Graphics.FillEllipse(inner, rect);      // fill slightly over size to make sure all pixels are painted
                    }
                    else
                    {
                        using (Brush inner = new LinearGradientBrush(rect, RadioButtonInnerColor, basecolor, GradientDirection))
                            e.Graphics.FillEllipse(inner, rect);      // fill slightly over size to make sure all pixels are painted
                    }
                }
                else
                {
                    using (Brush disabled = new SolidBrush(RadioButtonInnerColor))
                    {
                        e.Graphics.FillEllipse(disabled, rect);
                    }
                }

                rect.Inflate(-1, -1);

                if (Checked)
                {
                    Color c1 = Color.FromArgb(255, SelectedColor);

                    if (FlatStyle == FlatStyle.Flat)
                    {
                        using (Brush inner = new SolidBrush(c1))
                            e.Graphics.FillEllipse(inner, rect);      // fill slightly over size to make sure all pixels are painted
                    }
                    else
                    {
                        using (Brush inner = new LinearGradientBrush(rect, RadioButtonInnerColor, c1, (GradientDirection+180.0F) % 360F))
                            e.Graphics.FillEllipse(inner, rect);      // fill slightly over size to make sure all pixels are painted
                    }

                    using (Pen ring = new Pen(SelectedColorRing))
                    {
                        e.Graphics.DrawEllipse(ring, rect);

                    }
                }

                using (Brush textb = new SolidBrush((Enabled) ? this.ForeColor : this.ForeColor.Multiply(RadioButtonDisabledScaling)))
                {
                    using (StringFormat fmt = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
                    {
                        if (FontToUse == null || FontToUse.FontFamily != Font.FontFamily || FontToUse.Style != Font.Style)
                            FontToUse = e.Graphics.GetFontToFit(this.Text, Font, textarea.Size, fmt);

                        e.Graphics.DrawString(this.Text, FontToUse, textb, textarea, fmt);
                    }
                }

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            }
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            mouseover = true;
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            mouseover = false;
        }

        public bool Theme(Theme t, Font fnt)
        {
            FlatStyle = t.ButtonFlatStyle;
            BackColor = t.GroupBoxOverride(Parent, t.Form);
            ForeColor = t.CheckBoxText;
            RadioButtonColor = t.CheckBoxBack;
            RadioButtonInnerColor = t.CheckBoxBack.Multiply(t.CheckBoxTickStyleInnerScaling);
            SelectedColor = BackColor.Multiply(t.DisabledScaling);
            MouseOverColor = t.CheckBoxBack.Multiply(t.MouseOverScaling);
            GradientDirection = t.CheckBoxGradientDirection;
            RadioButtonDisabledScaling = t.DisabledScaling;
            return false;
        }

        private bool mouseover = false;

    }
}
