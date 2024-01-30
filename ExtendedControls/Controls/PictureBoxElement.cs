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
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPictureBox : PictureBox
    {
        // Image elements holds the bitmap and the location, plus its tag and tip

        public class ImageElement : IDisposable
        {
            public Rectangle Location { get; set; }
            public Point Position { get { return new Point(Location.Left, Location.Top); } set { Location = new Rectangle(value.X, value.Y, Location.Width, Location.Height); } }
            public Size Size { get { return new Size(Location.Width, Location.Height); } set { Location = new Rectangle(Location.Left, Location.Top, value.Width, value.Height); } }
            public Image Image { get; set; }
            public bool ImageOwned { get; set; }
            public bool Visible { get; set; } = true;

            public Rectangle AltLocation { get; set; }
            public Image AltImage { get; set; }
            public bool AltImageOwned { get; set; }
            public bool InAltImage { get; set; } = false;

            public Object Tag { get; set; }
            public string ToolTipText { get; set; }

            public bool MouseOver { get; set; }

            public Action<Graphics, ImageElement> OwnerDrawCallback { get; set; }

            public Action Enter { get; set; }
            public Action Leave { get; set; }
            public Action Click { get; set; }

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
                MouseOver = mo;
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

    }
}

