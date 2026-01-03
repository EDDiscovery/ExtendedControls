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

namespace ExtendedControls.ImageElement
{
    public class CheckBox : Element
    {
        public string Text { get { return text; } set { text = value;  PictureBoxParent?.Refresh(Bounds); } }
        public Font Font { get { return font; } set { font = value;  PictureBoxParent?.Refresh(Bounds); } }
        public CheckState CheckState { get { return checkState; } set { checkState = value;  PictureBoxParent?.Refresh(Bounds); } }
        public bool Checked { get { return CheckState == CheckState.Checked; } set { CheckState = value ? CheckState.Checked : CheckState.Unchecked; } }
        public float TickBoxReductionRatio { get { return tickBoxReductionRatio; } set { tickBoxReductionRatio = value;  PictureBoxParent?.Refresh(Bounds); } }
        public ContentAlignment CheckAlign { get { return checkAlign; } set { CheckAlign = value;  PictureBoxParent?.Refresh(Bounds); } }

        public Color ForeColor { get; set; } = Color.Blue;          // Normal only border area colour
        public Color BackColor { get; set; } = Color.White;          // Normal only border area colour
        public Color CheckBoxColor { get; set; } = Color.Gray;          // Normal only border area colour
        public Color CheckBoxInnerColor { get; set; } = Color.White;    // Normal only inner colour
        public Color CheckColor { get; set; } = Color.DarkBlue;         // Button - back colour when checked, Normal - check colour
        public Color MouseOverColor { get; set; } = Color.CornflowerBlue; // both - Mouse over 

        public Action<CheckBox> CheckChanged;                           // check has changed, only if Enabled

        private string text = "";
        private Font font;
        private CheckState checkState = CheckState.Unchecked;
        private ContentAlignment checkAlign = ContentAlignment.MiddleLeft;
        private float tickBoxReductionRatio = 0.75f;       // Normal - size reduction

        public CheckBox() 
        {
            Enter += (o,ie) => { ie.PictureBoxParent?.Refresh(Bounds); };
            Leave += (o, ie) => { ie.PictureBoxParent?.Refresh(Bounds); };
            Click += (o, ie,bt) =>
            {
                if (bt.Button == MouseButtons.Left)
                {
                    if (CheckState != CheckState.Checked)
                        CheckState = CheckState.Checked;
                    else
                        CheckState = CheckState.Unchecked;
                    ie.PictureBoxParent?.Refresh(Bounds);
                    CheckChanged?.Invoke(this);
                }
            };

            OwnerDrawCallback += (gr, ie) => { Draw(gr,ie); };
        }

        private void Draw(Graphics dgr, Element ie)
        {
            var ClientRectangle = ie.Bounds;

            using (Brush br = new SolidBrush(this.BackColor))
                dgr.FillRectangle(br, ClientRectangle);

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

            float discaling = Enabled && !ShowDisabled ? 1.0f : DisabledScaling;

            Color checkboxbasecolour = (Enabled && MouseOver) ? MouseOverColor : CheckBoxColor.Multiply(discaling);

            // System.Diagnostics.Debug.WriteLine($"Draw chkbox {Tag} {MouseOver} {Enabled} {checkboxbasecolour}");

            using (Pen outer = new Pen(checkboxbasecolour))
                dgr.DrawRectangle(outer, tickarea);

            tickarea.Inflate(-1, -1);

            Rectangle checkarea = tickarea;
            checkarea.Width++; checkarea.Height++;          // convert back to area

            using (Pen second = new Pen(CheckBoxInnerColor.Multiply(discaling), 1F))
                dgr.DrawRectangle(second, tickarea);

            tickarea.Inflate(-1, -1);

            using (Brush inner = new LinearGradientBrush(tickarea, CheckBoxInnerColor.Multiply(discaling), checkboxbasecolour, 225))
                dgr.FillRectangle(inner, tickarea);      // fill slightly over size to make sure all pixels are painted

            using (Pen third = new Pen(checkboxbasecolour.Multiply(discaling), 1F))
                dgr.DrawRectangle(third, tickarea);

            dgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (Font != null)
            {
                using (StringFormat fmt = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.FitBlackBox })
                {
                    if (this.Text.HasChars())
                    {
                        using (Brush textb = new SolidBrush(Enabled ? this.ForeColor : this.ForeColor.Multiply(DisabledScaling)))
                        {
                            dgr.DrawString(this.Text, Font, textb, textarea, fmt);
                        }
                    }
                }
            }

            Color c1 = Color.FromArgb(200, CheckColor.Multiply(discaling));
            if (CheckState == CheckState.Checked)
            {
                Point pt1 = new Point(checkarea.X + 2, checkarea.Y + checkarea.Height / 2 - 1);
                Point pt2 = new Point(checkarea.X + checkarea.Width / 2 - 1, checkarea.Bottom - 2);
                Point pt3 = new Point(checkarea.X + checkarea.Width - 2, checkarea.Y);

                using (Pen pcheck = new Pen(c1, 2.0F))
                {
                    dgr.DrawLine(pcheck, pt1, pt2);
                    dgr.DrawLine(pcheck, pt2, pt3);
                }
            }
            else if (CheckState == CheckState.Indeterminate)
            {
                Size cb = new Size(checkarea.Width - 5, checkarea.Height - 5);
                if (cb.Width > 0 && cb.Height > 0)
                {
                    using (Brush br = new SolidBrush(c1))
                    {
                        dgr.FillRectangle(br, new Rectangle(new Point(checkarea.X + 2, checkarea.Y + 2), cb));
                    }
                }
            }

            dgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }
    }

    public class Label : Element
    {
        public string Text { get { return text; } set { text = value; PerformAutoSize(); PictureBoxParent?.Refresh(Bounds); } }
        public Font Font { get { return font; } set { font = value; PerformAutoSize(); PictureBoxParent?.Refresh(Bounds); } }
        public Color ForeColor { get; set; } = Color.Blue;          // Normal only border area colour
        public Color BackColor { get; set; } = Color.White;          // Normal only border area colour
        public Color MouseOverBackColor { get; set; } = Color.CornflowerBlue; // both - Mouse over 

        public bool AutoSize { get; set; } = true;

        private string text = "";
        private Font font;

        public Label()
        {
            OwnerDrawCallback += (gr, ie) => { Draw(gr, ie); };
            Enter += (o, ie) => { PictureBoxParent?.Refresh(Bounds); };
            Leave += (o, ie) => { PictureBoxParent?.Refresh(Bounds); };
        }

        public void PerformAutoSize()
        {
            if (AutoSize && Text.HasChars() && Font != null)
            {
                var sizef = BaseUtils.BitMapHelpers.MeasureStringInBitmap(Text, Font);
                Size = new Size((int)sizef.Width + 2, (int)sizef.Height + 1);
            }
        }

        private void Draw(Graphics dgr, Element ie)
        {
            var ClientRectangle = ie.Bounds;

            using (Brush br = new SolidBrush((Enabled && MouseOver) ? MouseOverBackColor : this.BackColor))
                dgr.FillRectangle(br, ClientRectangle);

            Rectangle textarea = ClientRectangle;

            if (Font != null)
            {
                dgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (StringFormat fmt = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.FitBlackBox })
                {
                    if (this.Text.HasChars())
                    {
                        using (Brush textb = new SolidBrush(Enabled && !ShowDisabled ? this.ForeColor : this.ForeColor.Multiply(DisabledScaling)))
                        {
                            dgr.DrawString(this.Text, Font, textb, textarea, fmt);
                        }
                    }
                }
                dgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            }

        }
    }

    public class Button : Element
    {
        public string Text { get { return text; } set { text = value; PictureBoxParent?.Refresh(Bounds); } }
        public Font Font { get { return font; } set { font = value; PictureBoxParent?.Refresh(Bounds); } }
        public Color ForeColor { get; set; } = Color.Blue;          // Normal only border area colour
        public Color BackColor { get; set; } = Color.White;          // Normal only border area colour
        public Color ButtonFaceColor { get; set; } = Color.Gray;
        public Color MouseOverFaceColor { get; set; } = Color.CornflowerBlue;
        public float BackDisabledScaling { get; set; } = 0.5F;
        public float MousePressedHighlightScaling { get; set; } = 1.3F;
        public float FaceColorScaling { get; set; } = 0.75F;
        public float ForeDisabledScaling { get; set; } = 0.75F;
        public ContentAlignment TextAlign { get; set; } = ContentAlignment.MiddleCenter;

        public bool MousePressed { get; set; } = false;

        private string text = "";
        private Font font;

        public Button()
        {
            Enter += (o, ie) => { MousePressed = false; ie.PictureBoxParent?.Refresh(Bounds); };
            Leave += (o, ie) => { MousePressed = false; ie.PictureBoxParent?.Refresh(Bounds); };
            MouseDown += (o, ie, ea) => { MousePressed = true; ie.PictureBoxParent?.Refresh(Bounds); };
            MouseUp += (o, ie, ea) => { MousePressed = false; ie.PictureBoxParent?.Refresh(Bounds); };
            OwnerDrawCallback += (gr, ie) => { Draw(gr, ie); };
        }

        private protected Color PaintButtonFaceColor()
        {
            Color colBack;

            if (Enabled == false)
                colBack = ButtonFaceColor.Multiply(BackDisabledScaling);
            else if (MousePressed)
                colBack = MouseOverFaceColor.Multiply(MousePressedHighlightScaling);
            else if (MouseOver)
                colBack = MouseOverFaceColor;
            else
                colBack = ButtonFaceColor;

            return colBack;
        }

        private void Draw(Graphics dgr, Element ie)
        {
            var ClientRectangle = ie.Bounds;
            if (ClientRectangle.Width < 1 || ClientRectangle.Height < 1)
                return;

            using (Brush br = new SolidBrush(this.BackColor))
                dgr.FillRectangle(br, ClientRectangle);

            var backarea = new Rectangle(ClientRectangle.Left+2, ClientRectangle.Top+2, ClientRectangle.Width - 4, ClientRectangle.Height - 4);

            if (backarea.Width > 0 && backarea.Height > 0)
            {
                using (var b = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(backarea.Left, backarea.Top - 1, backarea.Width, backarea.Height + 1),
                    PaintButtonFaceColor(), PaintButtonFaceColor().Multiply(FaceColorScaling), 90))
                {
                    dgr.FillRectangle(b, backarea);       // linear grad brushes do not respect smoothing mode, btw
                }

                if (!string.IsNullOrEmpty(Text))
                {
                    dgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    using (var fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(TextAlign))
                    {
                        //System.Diagnostics.Debug.WriteLine($"Draw {Text} {Enabled} {ForeDisabledScaling}");
                        using (Brush textb = new SolidBrush((Enabled) ? this.ForeColor : this.ForeColor.Multiply(ForeDisabledScaling)))
                        {
                            dgr.DrawString(this.Text, this.Font, textb, backarea, fmt);
                        }
                    }
                }

                dgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            }
        }
    }

}

