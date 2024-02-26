/*
 * Copyright © 2016-2024 EDDiscovery development team
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
    public partial class ExtPictureBox : PictureBox
    {
        // Image elements holds the bitmap and the location, plus its tag and tip

        public class ImageElement : IDisposable
        {
            public virtual Rectangle Location { get; set; }
            public Point Position { get { return new Point(Location.Left, Location.Top); } set { Location = new Rectangle(value.X, value.Y, Location.Width, Location.Height); } }
            public Size Size { get { return new Size(Location.Width, Location.Height); } set { Location = new Rectangle(Location.Left, Location.Top, value.Width, value.Height); } }
            public Image Image { get; set; }
            public bool ImageOwned { get; set; }
            public bool Visible { get; set; } = true;

            public Rectangle AltLocation { get; set; }
            public Image AltImage { get; set; }
            public bool AltImageOwned { get; set; }
            public bool InAltImage { get; set; } = false;
            public bool AlternateImageWhenMouseOver { get; set; }

            public Object Tag { get; set; }
            public string ToolTipText { get; set; }

            public ExtPictureBox Parent { get; set; }

            public bool MouseOver { get; set; }     // set when mouse over this

            public Action<Graphics, ImageElement> OwnerDrawCallback { get; set; }

            public Action<ExtPictureBox, ImageElement> Enter { get; set; }
            public Action<ExtPictureBox, ImageElement> Leave { get; set; }
            public Action<ExtPictureBox, ImageElement, MouseEventArgs> MouseDown { get; set; }
            public Action<ExtPictureBox, ImageElement, MouseEventArgs> MouseUp { get; set; }
            public Action<ExtPictureBox, ImageElement, MouseEventArgs> Click { get; set; }

            public ImageElement()
            {
            }

            public ImageElement(Rectangle p, Image i, Object t = null, string tt = null, bool imgowned = true)
            {
                Location = p; Image = i; Tag = t; ToolTipText = tt; this.ImageOwned = imgowned;
            }

            public void Bitmap(Rectangle p, Image i, Object t = null, string tt = null, bool imgowned = true)
            {
                Location = p; Image = i; Tag = t; ToolTipText = tt; this.ImageOwned = imgowned;
            }

            // centred, autosized
            public void TextCentreAutoSize(Point poscentrehorz, Size max, string text, Font dp, Color c, Color backcolour, float backscale = 1.0F,
                                            Object t = null, string tt = null, StringFormat frmt = null)
            {
                if (ImageOwned)
                    Image?.Dispose();
                Image = BaseUtils.BitMapHelpers.DrawTextIntoAutoSizedBitmap(text, max, dp, c, backcolour, backscale, frmt);
                ImageOwned = true;
                Location = new Rectangle(poscentrehorz.X - Image.Width / 2, poscentrehorz.Y, Image.Width, Image.Height);
                Tag = t;
                ToolTipText = tt;
            }

            // top left, autosized
            public void TextAutoSize(Point topleft, Size max, string text, Font dp, Color c, Color backcolour, float backscale = 1.0F,
                                        Object t = null, string tt = null, StringFormat frmt = null)
            {
                if (ImageOwned)
                    Image?.Dispose();
                Image = BaseUtils.BitMapHelpers.DrawTextIntoAutoSizedBitmap(text, max, dp, c, backcolour, backscale, frmt);
                ImageOwned = true;
                Location = new Rectangle(topleft.X, topleft.Y, Image.Width, Image.Height);
                Tag = t;
                ToolTipText = tt;
            }

 
            public void OwnerDraw(Action<Graphics, ImageElement> callback, Rectangle area, Object tag = null, string tiptext = null)
            {
                Location = area;
                OwnerDrawCallback = callback;
                Tag = tag;
                ToolTipText = tiptext;
            }

            public void HorizontalDivider(Color c, Rectangle area, float width = 1.0f, int offset = 0, Object t = null, string tt = null)
            {
                Image?.Dispose();
                Image = new Bitmap(area.Width, area.Height);
                ImageOwned = true;
                Location = area;
                Tag = t;
                ToolTipText = tt;

                using (Graphics dgr = Graphics.FromImage(Image))
                {
                    using ( Pen pen = new Pen(c,width))
                    {
                        dgr.DrawLine(pen, new Point(0, offset), new Point(area.Width, offset));
                    }
                }
            }

            public void SetAlternateImage(Image i, Rectangle p, bool mo = false, bool imgowned = true)
            {
                AltImage = i;
                AltLocation = p;
                AlternateImageWhenMouseOver = mo;
                AltImageOwned = imgowned;
            }

            public bool SwapImages(Image surface)           // swap to alternative, optionally, draw to surface
            {
                if (AltImage != null)
                {
                    Rectangle r = Location;
                    Location = AltLocation;
                    AltLocation = r;

                    Image i = Image;
                    Image = AltImage;
                    AltImage = i;

                    bool io = ImageOwned;     // swap tags
                    ImageOwned = AltImageOwned;
                    AltImageOwned = io;

                    //System.Diagnostics.Debug.WriteLine("Element @ " + pos + " " + inaltimg);
                    if (surface != null)
                    {
                        using (Graphics gr = Graphics.FromImage(surface)) //restore
                        {
                            gr.Clip = new Region(AltLocation);       // remove former
                            gr.Clear(Color.FromArgb(0, Color.Black));       // set area back to transparent before paint..
                        }

                        using (Graphics gr = Graphics.FromImage(surface)) //restore
                            gr.DrawImage(Image, Location);
                    }

                    InAltImage = !InAltImage;
                    return true;
                }
                else
                    return false;
            }

            public void Translate(int x, int y, bool alt = true)
            {
                Location = new Rectangle(Location.X + x, Location.Y + y, Location.Width, Location.Height);
                if (alt)
                    AltLocation = new Rectangle(AltLocation.X + x, AltLocation.Y + y, AltLocation.Width, AltLocation.Height);
            }

            public void TranslateAlt(int x, int y)
            {
                AltLocation = new Rectangle(AltLocation.X + x, AltLocation.Y + y, AltLocation.Width, AltLocation.Height);
            }

            public void ClearImage()
            {
                if (ImageOwned)
                {
                    Image?.Dispose();
                }
                Image = null;
                if (AltImageOwned)
                {
                    AltImage?.Dispose();
                }
                AltImage = null;
            }

            public void Dispose()
            {
                ClearImage();
                Tag = null;
            }
        }


        public class CheckBox : ImageElement
        {
            public string Text { get { return text; } set { text = value;  Parent?.Refresh(Location); } }
            public Font Font { get { return font; } set { font = value;  Parent?.Refresh(Location); } }
            public CheckState CheckState { get { return checkState; } set { checkState = value;  Parent?.Refresh(Location); } }
            public bool Checked { get { return CheckState == CheckState.Checked; } set { CheckState = value ? CheckState.Checked : CheckState.Unchecked; } }
            public bool Enabled { get { return enabled; } set { enabled = value;  Parent?.Refresh(Location); } }
            public float TickBoxReductionRatio { get { return tickBoxReductionRatio; } set { tickBoxReductionRatio = value;  Parent?.Refresh(Location); } }
            public ContentAlignment CheckAlign { get { return checkAlign; } set { CheckAlign = value;  Parent?.Refresh(Location); } }

            public Color ForeColor { get; set; } = Color.Blue;          // Normal only border area colour
            public Color BackColor { get; set; } = Color.White;          // Normal only border area colour
            public Color CheckBoxColor { get; set; } = Color.Gray;          // Normal only border area colour
            public Color CheckBoxInnerColor { get; set; } = Color.White;    // Normal only inner colour
            public Color CheckColor { get; set; } = Color.DarkBlue;         // Button - back colour when checked, Normal - check colour
            public Color MouseOverColor { get; set; } = Color.CornflowerBlue; // both - Mouse over 
            public float CheckBoxDisabledScaling { get; set; } = 0.5F;      // Both - text and check box scaling when disabled

            public Action<CheckBox> CheckChanged;                           // check has changed, only if Enabled

            private string text = "";
            private Font font;
            private CheckState checkState = CheckState.Unchecked;
            private bool enabled = true;
            private ContentAlignment checkAlign = ContentAlignment.MiddleLeft;
            private float tickBoxReductionRatio = 0.75f;       // Normal - size reduction

            public CheckBox() 
            {
                Enter += (pb, ie) => {  pb.Refresh(Location); };
                Leave += (pb, ie) => {  pb.Refresh(Location); };
                Click += (pb, ie,bt) =>
                {
                    if (bt.Button == MouseButtons.Left && Enabled)
                    {
                        if (CheckState != CheckState.Checked)
                            CheckState = CheckState.Checked;
                        else
                            CheckState = CheckState.Unchecked;
                        pb.Refresh(Location);
                        CheckChanged?.Invoke(this);
                    }
                };

                OwnerDrawCallback += (gr, ie) => { Draw(gr,ie); };
            }

            private void Draw(Graphics dgr, ImageElement ie)
            {
                var ClientRectangle = ie.Location;

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

                float discaling = Enabled ? 1.0f : CheckBoxDisabledScaling;

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
                            using (Brush textb = new SolidBrush(Enabled ? this.ForeColor : this.ForeColor.Multiply(CheckBoxDisabledScaling)))
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

        public class Label : ImageElement
        {
            public string Text { get { return text; } set { text = value; PerformAutoSize(); Parent?.Refresh(Location); } }
            public Font Font { get { return font; } set { font = value; PerformAutoSize(); Parent?.Refresh(Location); } }
            public Color ForeColor { get; set; } = Color.Blue;          // Normal only border area colour
            public Color BackColor { get; set; } = Color.White;          // Normal only border area colour
            public bool Enabled { get { return enabled; } set { enabled = value; Parent?.Refresh(Location); } }
            public float DisabledScaling { get; set; } = 0.5F;      // Both - text and check box scaling when disabled

            public bool AutoSize { get; set; } = true;

            private string text = "";
            private Font font;
            private bool enabled = true;

            public Label()
            {
                OwnerDrawCallback += (gr, ie) => { Draw(gr, ie); };
            }

            public void PerformAutoSize()
            {
                if (AutoSize && Text.HasChars() && Font != null)
                {
                    var sizef = BaseUtils.BitMapHelpers.MeasureStringInBitmap(Text, Font);
                    Size = new Size((int)sizef.Width + 1, (int)sizef.Height + 1);
                }
            }

            private void Draw(Graphics dgr, ImageElement ie)
            {
                var ClientRectangle = ie.Location;

                using (Brush br = new SolidBrush(this.BackColor))
                    dgr.FillRectangle(br, ClientRectangle);

                Rectangle textarea = ClientRectangle;

                if (Font != null)
                {
                    dgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
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
                    dgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                }

            }
        }

    }
}

