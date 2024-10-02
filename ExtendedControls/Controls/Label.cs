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
 *
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    // WARNING ONLY USE IN SPECIAL CIRCUMSTANCES.. NORMALLY YOU WILL WANT THE NORMAL LABEL
    // draws label using a bitmap - solves problems with aliasing over transparent backgrounds
    // but it does not antialias properly if the background is not drawn..

    public class ExtLabel : Label
    {
        private new Color BackColor { get; set; }          // DONT - has no meaning for this label

        public Color TextBackColor { get { return textbackcolor; } set { textbackcolor = value; Invalidate(); } }      // area of text box only
        private Color textbackcolor = Color.Transparent;

        public ExtLabel()
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            SizeF sizef = pe.Graphics.MeasureString(this.Text, this.Font);
            Size sz = new Size((int)(sizef.Width + 1), (int)(sizef.Height + 1));

            //Console.WriteLine("Label {0} {1} size {2}", Name, Text, sz);

            if (sz.Width > 0 && sz.Height > 0 && this.Text.Length > 0)
            {
                using (Bitmap mp = new Bitmap(sz.Width, sz.Height))   // bitmaps .. drawing directly does not work due to aliasing
                {
                    using (Graphics mpg = Graphics.FromImage(mp))
                    {
                        if (!this.TextBackColor.IsFullyTransparent())
                        {
                            using (Brush b = new SolidBrush(this.TextBackColor))
                            {
                                mpg.FillRectangle(b, new Rectangle(0, 0, sz.Width, sz.Height));
                                //System.Diagnostics.Debug.WriteLine("Draw {0} with back {1}", Text, this.TextBackColor);
                            }
                        }

                        mpg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        using (Brush b = new SolidBrush(this.ForeColor))
                        {
                            mpg.DrawString(this.Text, this.Font, b, new Point(0, 0));
                        }

                        mpg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                        Rectangle pos = TextAlign.ImagePositionFromContentAlignment(ClientRectangle, mp.Size, true);
                        pe.Graphics.DrawImageUnscaled(mp, pos.Left, pos.Top);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Fixed Width label with auto calculated height
    /// </summary>
    public class ExtLabelAutoHeight : Label
    {
        public ExtLabelAutoHeight()
        {
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SizeIt();
           // System.Diagnostics.Debug.WriteLine($"Resized to {Size} ");
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            SizeIt();
           // System.Diagnostics.Debug.WriteLine($"text changed to {Size} ");
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            SizeIt();
           // System.Diagnostics.Debug.WriteLine($"font changed to {Size} ");
        }

        private void SizeIt()
        {
            if (Text.HasChars())
            {
                using (StringFormat fmt = new StringFormat())
                {
                    // https://stackoverflow.com/questions/1203087/why-is-graphics-measurestring-returning-a-higher-than-expected-number
                    // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.textrenderer.measuretext?view=netframework-4.8&devlangs=csharp&f1url=%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Windows.Forms.TextRenderer.MeasureText)%3Bk(TargetFrameworkMoniker-.NETFramework%2CVersion%253Dv4.8)%3Bk(DevLang-csharp)%26rd%3Dtrue
                    // use this render measure, its more accurate with windows controls. 
                    // Application.SetCompatibleTextRenderingDefault(false) is default, so items use GDI TextRender
                    // this means some of the other code is out of date, heh-ho.

                    Size newsize = TextRenderer.MeasureText(Text, Font, new Size(this.Width, 1280), TextFormatFlags.WordBreak);

                    if (newsize != Size)
                    {
                        //System.Diagnostics.Debug.WriteLine($"current size {Size} new size {newsize}");
                        ClientSize = new Size(this.Width, newsize.Height);
                    }
                }
            }
        }
    }
}
