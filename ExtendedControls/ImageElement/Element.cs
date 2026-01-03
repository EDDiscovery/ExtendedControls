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
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ExtendedControls.ImageElement
{
    [System.Diagnostics.DebuggerDisplay("{Name} {Bounds} {Visible} {ToolTipText}")]
    public class Element : IDisposable
    {
        public virtual Rectangle Bounds { get; set; }
        public Point Location { get { return new Point(Bounds.Left, Bounds.Top); } set { Bounds = new Rectangle(value.X, value.Y, Bounds.Width, Bounds.Height); } }
        public Point RightTop { get { return new Point(Bounds.Left + Bounds.Width, Bounds.Top); } }
        public Point Centre { get { return new Point(Bounds.Left + Bounds.Width/2, Bounds.Top + Bounds.Height / 2); } }
        public Point BottomRight { get { return new Point(Bounds.Right, Bounds.Bottom); } }
        public Size Size { get { return new Size(Bounds.Width, Bounds.Height); } set { Bounds = new Rectangle(Bounds.Left, Bounds.Top, value.Width, value.Height); } }
        public Image Image { get; set; }
        public bool ImageOwned { get; set; }
        public bool Visible { get; set; } = true;

        public Rectangle AltBounds { get; set; }
        public Image AltImage { get; set; }
        public bool AltImageOwned { get; set; }
        public bool InAltImage { get; set; } = false;
        public bool AlternateImageWhenMouseOver { get; set; }

        public string Name { get; set; }
        public Object Tag { get; set; }
        public Object Tag2 { get; set; }
        public string ToolTipText { get; set; }

        public ExtPictureBox PictureBoxParent { get; set; }

        // enable affects display and clickabilty
        public bool Enabled { get { return enabled; } set { if (enabled != value) { enabled = value; PictureBoxParent?.Refresh(Bounds); } } }

        // showdisable is display only
        public bool ShowDisabled { get { return showdisabled; } set { if (showdisabled != value) { showdisabled = value; PictureBoxParent?.Refresh(Bounds); } } }

        public float DisabledScaling { get; set; } = 0.5F;      // scaling if not enabled or ShowDisabled

        public bool MouseOver { get; set; }     // set when mouse over this
        public Action<Graphics, Element> OwnerDrawCallback { get; set; }

        public Action<object, Element> Enter { get; set; }
        public Action<object, Element> Leave { get; set; }
        public Action<object, Element, MouseEventArgs> MouseDown { get; set; }
        public Action<object, Element, MouseEventArgs> MouseUp { get; set; }
        public Action<object, Element, MouseEventArgs> Click { get; set; }

        public ContextMenuStrip ContextMenuStrip { get; set; }  // if set, right click invokes this, with its tag set to the element clicked. Click is not called.

        private bool enabled = true;
        private bool showdisabled = false;

        public Element()
        {
        }

        public Element(Rectangle p, Image i, Object t = null, string tt = null, bool imgowned = true)
        {
            Bounds = p; Image = i; Tag = t; ToolTipText = tt; this.ImageOwned = imgowned;
        }

        public void Bitmap(Rectangle p, Image i, Object t = null, string tt = null, bool imgowned = true)
        {
            Bounds = p; Image = i; Tag = t; ToolTipText = tt; this.ImageOwned = imgowned;
        }

        // centred, autosized
        public void TextCentreAutoSize(Point poscentrehorz, Size max, string text, Font dp, Color c, Color backcolour, float backscale = 1.0F,
                                        Object t = null, string tt = null, StringFormat frmt = null)
        {
            if (ImageOwned)
                Image?.Dispose();
            Image = BaseUtils.BitMapHelpers.DrawTextIntoAutoSizedBitmap(text, max, dp, c, backcolour, backscale, frmt);
            ImageOwned = true;
            Bounds = new Rectangle(poscentrehorz.X - Image.Width / 2, poscentrehorz.Y, Image.Width, Image.Height);
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
            Bounds = new Rectangle(topleft.X, topleft.Y, Image.Width, Image.Height);
            Tag = t;
            ToolTipText = tt;
        }

 
        public void OwnerDraw(Action<Graphics, Element> callback, Rectangle area, Object tag = null, string tiptext = null)
        {
            Bounds = area;
            OwnerDrawCallback = callback;
            Tag = tag;
            ToolTipText = tiptext;
        }

        public void HorizontalDivider(Color c, Rectangle area, float width = 1.0f, int offset = 0, Object t = null, string tt = null)
        {
            Image?.Dispose();
            Image = new Bitmap(area.Width, area.Height);
            ImageOwned = true;
            Bounds = area;
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
            AltBounds = p;
            AlternateImageWhenMouseOver = mo;
            AltImageOwned = imgowned;
        }

        public bool SwapImages(Image surface)           // swap to alternative, optionally, draw to surface
        {
            if (AltImage != null)
            {
                Rectangle r = Bounds;
                Bounds = AltBounds;
                AltBounds = r;

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
                        gr.Clip = new Region(AltBounds);       // remove former
                        gr.Clear(Color.FromArgb(0, Color.Black));       // set area back to transparent before paint..
                    }

                    using (Graphics gr = Graphics.FromImage(surface)) //restore
                        gr.DrawImage(Image, Bounds);
                }

                InAltImage = !InAltImage;
                return true;
            }
            else
                return false;
        }

        public void Translate(int x, int y, bool alt = true)
        {
            Bounds = new Rectangle(Bounds.X + x, Bounds.Y + y, Bounds.Width, Bounds.Height);
            if (alt)
                AltBounds = new Rectangle(AltBounds.X + x, AltBounds.Y + y, AltBounds.Width, AltBounds.Height);
        }

        public void TranslateAlt(int x, int y)
        {
            AltBounds = new Rectangle(AltBounds.X + x, AltBounds.Y + y, AltBounds.Width, AltBounds.Height);
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

        public void Paint(Graphics gr)
        {
            if (Visible)
            {
                if (Image != null)
                {
                    if (!Enabled || ShowDisabled)       // if show with disabled scaling
                    {
                        ColorMatrix alphaMatrix = new ColorMatrix();
                        alphaMatrix.Matrix00 = alphaMatrix.Matrix11 = alphaMatrix.Matrix22 = alphaMatrix.Matrix44 = 1;
                        alphaMatrix.Matrix33 = DisabledScaling;

                        using (ImageAttributes alphaAttributes = new ImageAttributes())
                        {
                            alphaAttributes.SetColorMatrix(alphaMatrix);
                            gr.DrawImage(Image, new Rectangle(Bounds.X, Bounds.Y, Size.Width, Size.Height), 0, 0, Size.Width, Size.Height, GraphicsUnit.Pixel, alphaAttributes);
                        }
                    }
                    else
                    {
                        // System.Diagnostics.Debug.WriteLine($"Draw {Tag} @ {Location}");
                        gr.DrawImage(Image, Bounds);
                    }
                }
                else
                {
                    //   System.Diagnostics.Debug.WriteLine($"Draw {Tag} @ {Location} no image");
                }

                OwnerDrawCallback?.Invoke(gr, this);
            }
        }
    }
}

