/*
 * Copyright © 2022-2022 EDDiscovery development team
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

using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls.Controls
{
    public partial class ImageControl : Control
    {
        public Size ImageSize { get; set; } = new Size(128, 128);
        public ImageLayout ImageLayout { get; set; } = ImageLayout.Stretch;     // only stretch/none at the moment
        public Color ImageBackgroundColor { get; set; } = Color.Transparent;

        private Bitmap bitmap = null;

        public ImageControl()
        {
            InitializeComponent();
        }

        // manually invalidate afterwards all of these

        // Draw Text at pos. backcolor = null means transparent

        public void DrawText(Rectangle area, string text, Font dp, Color forecolor, Color? backcolor = null,
                                                    float backscale = 1.0F, StringFormat frmt = null, int angleback = 90)
        {
            CheckBitmap();
            BaseUtils.BitMapHelpers.DrawTextIntoBitmap(bitmap, area, text, dp, forecolor, backcolor, backscale, frmt, angleback);
        }
        public void DrawText(Point pos, Size maxsize, string text, Font dp, Color forecolor, Color? backcolor = null,
                                                    float backscale = 1.0F, StringFormat frmt = null, int angleback = 90)
        {
            CheckBitmap();
            BaseUtils.BitMapHelpers.DrawTextIntoBitmap(bitmap, pos, maxsize, text, dp, forecolor, backcolor, backscale, frmt, angleback);
        }

        // draw lines. The area determines the part to draw the backcolour on, and to size any centre formatting against
        public void DrawText(Rectangle area, string[] text, Font dp, int linespacing, Color forecolor, Color? backcolor = null,
                                                    float backscale = 1.0F, StringFormat frmt = null, int angleback = 90)
        {
            CheckBitmap();

            bool firstforback = true;       // only the first draw get the back colour, so we get the right size back

            foreach (var x in text)
            {
                if (x != null)
                {
                    BaseUtils.BitMapHelpers.DrawTextIntoBitmap(bitmap, area, x, dp, forecolor, firstforback ? backcolor: null, backscale, frmt, angleback);
                    area.Y += dp.Height + linespacing;
                    firstforback = false;
                }
            }
        }

        // draw lines. Each area is individually back coloured
        public void DrawText(Point pos, Size maxsize, string[] text, Font dp, int linespacing, Color forecolor, Color? backcolor = null,
                                                    float backscale = 1.0F, StringFormat frmt = null, int angleback = 90)
        {
            CheckBitmap();

            foreach (var x in text)
            {
                if (x != null)
                {
                    BaseUtils.BitMapHelpers.DrawTextIntoBitmap(bitmap, pos, maxsize, x, dp, forecolor, backcolor, backscale, frmt, angleback);
                    pos.Y += dp.Height + linespacing;
                }
            }
        }

        public void DrawImage(Image img, Rectangle area)
        {
            CheckBitmap();
            using (Graphics gr = Graphics.FromImage(bitmap))
                gr.DrawImage(img, area);
        }

        // remember to use Using to release the graphics
        public Graphics GetGraphics()     
        {
            CheckBitmap();
            return Graphics.FromImage(bitmap);      
        }

        public void Clear()
        {
            if (bitmap != null)
            {
                using (Graphics gr = Graphics.FromImage(bitmap))
                    gr.Clear(ImageBackgroundColor);
            }
        }

        private void CheckBitmap()
        {
            if (bitmap == null)
            {
                bitmap = new Bitmap(ImageSize.Width, ImageSize.Height);
                if (ImageBackgroundColor != Color.Transparent)
                {
                    using (Graphics gr = Graphics.FromImage(bitmap))
                        gr.Clear(ImageBackgroundColor);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (BackgroundImage != null)
            {
                if (BackgroundImageLayout == ImageLayout.Stretch)
                {
                    pevent.Graphics.DrawImage(BackgroundImage, ClientRectangle);
                }
                else
                {
                    if (BackgroundImage.Width < Width || BackgroundImage.Height < Height)
                    {
                        using (Brush br = new SolidBrush(BackColor))
                        {
                            pevent.Graphics.FillRectangle(br, ClientRectangle);
                        }
                    }

                    pevent.Graphics.DrawImage(BackgroundImage, new Point(0, 0));
                }
            }
            else
            {
                using (Brush br = new SolidBrush(BackColor))
                {
                    pevent.Graphics.FillRectangle(br, ClientRectangle);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if ( bitmap != null )
            {
                if (ImageLayout == ImageLayout.Stretch)
                {
                    pe.Graphics.DrawImage(bitmap, ClientRectangle);
                }
                else
                {
                    pe.Graphics.DrawImage(bitmap, new Point(0, 0));
                }
            }
        }
    }
}
