/*
 * Copyright © 2016-2020 EDDiscovery development team
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
using System.Collections.Generic;
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

            public Rectangle AltLocation { get; set; }
            public Image AltImage { get; set; }
            public bool AltImageOwned { get; set; }
            public bool InAltImage { get; set; } = false;

            public Object Tag { get; set; }
            public string ToolTipText { get; set; }

            public bool MouseOver { get; set; }

            public Action<Graphics, ImageElement> OwnerDrawCallback { get; set; }

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

            // top left, sized
            public void TextFixedSizeC(Point topleft, Size size, string text, Font dp, Color c, Color backcolour,
                                    float backscale = 1.0F, bool centertext = false,
                                    Object t = null, string tt = null, StringFormat frmt = null)
            {
                Image = BaseUtils.BitMapHelpers.DrawTextIntoFixedSizeBitmapC(text, size, dp, c, backcolour, backscale, centertext, frmt);
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

        public delegate void OnElement(object sender, MouseEventArgs eventargs, ImageElement i, object tag);
        public event OnElement EnterElement;
        public event OnElement LeaveElement;
        public event OnElement ClickElement;

        public Color FillColor = Color.Transparent;         // fill the bitmap with this colour before pasting the bitmaps in

        private ImageElement elementin = null;
        private Timer hovertimer = new Timer();
        private ToolTip hovertip = null;
        private Point hoverpos;

        public List<ImageElement> Elements { get; private set; } = new List<ImageElement>();

        #region Interface

        public ExtPictureBox()
        {
            hovertimer.Interval = 250;
            hovertimer.Tick += HoverEnd;
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }

        public int Count { get { return Elements.Count; } }

        public void Add(ImageElement i)
        {
            Elements.Add(i);
        }

        public void AddDrawFirst(ImageElement i)        // add to front of queue, draw first
        {
            Elements.Insert(0, i);
        }

        public void AddRange(List<ImageElement> list)
        {
            Elements.AddRange(list);
        }

        // topleft, autosized
        public ImageElement AddTextAutoSize(Point topleft, Size max, string label, Font fnt, Color c, Color backcolour, float backscale, Object tag = null, string tiptext = null, StringFormat frmt = null)
        {
            ImageElement lab = new ImageElement();
            lab.TextAutoSize(topleft, max, label, fnt, c, backcolour, backscale, tag, tiptext, frmt);
            Elements.Add(lab);
            return lab;
        }

        // topleft, sized
        public ImageElement AddTextFixedSizeC(Point topleft, Size size, string label, Font fnt, Color c, Color backcolour, float backscale, bool centered, Object tag = null, string tiptext = null, StringFormat frmt = null)
        {
            ImageElement lab = new ImageElement();
            lab.TextFixedSizeC(topleft, size, label, fnt, c, backcolour, backscale, centered, tag, tiptext, frmt);
            Elements.Add(lab);
            return lab;
        }

        // centre pos, autosized
        public ImageElement AddTextCentred(Point poscentrehorz, Size max, string label, Font fnt, Color c, Color backcolour, float backscale, Object tag = null, string tiptext = null, StringFormat frmt = null)
        {
            ImageElement lab = new ImageElement();
            lab.TextCentreAutoSize(poscentrehorz, max, label, fnt, c, backcolour, backscale, tag, tiptext, frmt);
            Elements.Add(lab);
            return lab;
        }

        public ImageElement AddImage(Rectangle p, Image img, Object tag = null, string tiptext = null, bool imgowned = true)    // make sure pushes it in..
        {
            ImageElement lab = new ImageElement();
            lab.Bitmap(p, img, tag, tiptext, imgowned);
            Elements.Add(lab);
            return lab;
        }

        public ImageElement AddOwnerDraw(Action<Graphics, ImageElement> callback, Rectangle p, Object tag = null, string tiptext = null)    // make sure pushes it in..
        {
            ImageElement lab = new ImageElement();
            lab.OwnerDraw(callback, p, tag, tiptext);
            Elements.Add(lab);
            return lab;
        }

        public ImageElement AddHorizontalDivider(Color c, Rectangle area, float width = 1.0f, int offset = 0, Object t = null, string tt = null)
        {
            ImageElement lab = new ImageElement();
            lab.HorizontalDivider(c, area, width, offset, t, tt);
            Elements.Add(lab);
            return lab;

        }
        public void ClearImageList()        // clears the element list, not the image.  call render to do this
        {
            if (Elements != null && Elements.Count >= 1)
            {
                foreach (var e in Elements)
                {
                    e.Dispose();
                }

                Elements.Clear();
            }
        }

        public Size DisplaySize()
        {
            int maxh = 0, maxw = 0;
            if (Elements != null)
            {
                foreach (ImageElement i in Elements)
                {
                    if (i.Location.X + i.Location.Width > maxw)
                        maxw = i.Location.X + i.Location.Width;
                    if (i.Location.Y + i.Location.Height > maxh)
                        maxh = i.Location.Y + i.Location.Height;
                }
            }

            return new Size(maxw, maxh);
        }

        // taking image elements, draw to main bitmap. set if resize control, and if we have a min size of bitmap, or a margin
        public void Render(bool resizecontrol = true, Size? minsize = null, Size? margin = null)
        {
            Size size = DisplaySize();
            if (size.Width > 0 && size.Height > 0) // will be zero if no elements
            {
                elementin = null;

                if (minsize.HasValue)           // minimum map size
                {
                    size.Width = Math.Max(size.Width, minsize.Value.Width);
                    size.Height = Math.Max(size.Height, minsize.Value.Height);
                }

                if (margin.HasValue)            // and any margin to allow for control growth
                {
                    size.Width += margin.Value.Width;
                    size.Height += margin.Value.Height;
                }

                Bitmap newrender = new Bitmap(size.Width, size.Height);   // size bitmap to contents

                if (!FillColor.IsFullyTransparent())
                {
                    BaseUtils.BitMapHelpers.FillBitmap(newrender, FillColor);
                }

                using (Graphics gr = Graphics.FromImage(newrender))
                {
                    foreach (ImageElement i in Elements)
                    {
                        if (i.Image != null)
                            gr.DrawImage(i.Image, i.Location);

                        i.OwnerDrawCallback?.Invoke(gr, i);
                    }
                }

                Image lastimage = Image;

                Image = newrender;      // and replace the image in one go, to try and minimise distortion

                if (resizecontrol)
                    this.Size = new Size(size.Width, size.Height);

                lastimage?.Dispose();
                lastimage = null;
            }
            else
            {
                Image?.Dispose();
                Image = null;       // nothing, null image
            }
        }

        public void SwapToAlternateImage(ImageElement i)
        {
            if (i.SwapImages(Image))
                Invalidate();
        }

        public void RemoveItem(ImageElement orgimg, Color? backcolour)
        {
            int i = Elements.IndexOf(orgimg);
            if (i >= 0)
            {
                if (!backcolour.HasValue)
                    backcolour = FillColor;
                Bitmap b = Image as Bitmap;
                BaseUtils.BitMapHelpers.ClearBitmapArea(b, orgimg.Location, backcolour.Value); // fill old element with back colour even if transparent
                orgimg.Dispose();
                Elements.RemoveAt(i);
            }
        }

        public void AddItem(ImageElement newitem)
        {
            using (Graphics gr = Graphics.FromImage(Image))     // paint new data
            {
                if (newitem.Image != null)
                    gr.DrawImage(newitem.Image, newitem.Location);
                newitem.OwnerDrawCallback?.Invoke(gr, newitem);
                Elements.Add(newitem);
            }

            Invalidate();
        }

        public void LeaveCurrentElement()
        {
            if (elementin != null)
            {
                if (elementin.AltImage != null && elementin.MouseOver && elementin.InAltImage)
                {
                    elementin.SwapImages(Image);
                    Invalidate();
                }

                elementin = null;
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EnterElement = LeaveElement = ClickElement = null;
                ClearImageList();
                Elements = null;
                ClearHoverTip();
                hovertimer.Dispose();
                hovertimer = null;
            }

            base.Dispose(disposing);
        }

        protected override void OnMouseMove(MouseEventArgs eventargs)
        {
            base.OnMouseMove(eventargs);

            if (elementin != null && !elementin.Location.Contains(eventargs.Location))       // go out..
            {
                LeaveCurrentElement();
                if (LeaveElement != null)
                    LeaveElement(this, eventargs, elementin, elementin.Tag);
            }

            if (elementin == null)      // is in?
            {
                foreach (ImageElement i in Elements)
                {
                    if (i.Location.Contains(eventargs.Location))
                    {
                        elementin = i;

                        //System.Diagnostics.Debug.WriteLine("Enter element " + elements.FindIndex(x=>x==i));

                        if (elementin.AltImage != null && elementin.MouseOver && !elementin.InAltImage)
                        {
                            elementin.SwapImages(Image);
                            Invalidate();
                        }

                        if (EnterElement != null)
                            EnterElement(this, eventargs, elementin, elementin.Tag);
                    }
                }
            }

            if (Math.Abs(eventargs.X - hoverpos.X) + Math.Abs(eventargs.Y - hoverpos.Y) > 8 || elementin == null)
            {
                ClearHoverTip();
            }

            if (elementin != null && !hovertimer.Enabled && hovertip == null)
            {
                hoverpos = eventargs.Location;
                hovertimer.Start();
            }
        }

        void ClearHoverTip()
        {
            hovertimer.Stop();

            if (hovertip != null)
            {
                hovertip.Dispose();
                hovertip = null;
            }
        }

        void HoverEnd(object sender, EventArgs e)
        {
            hovertimer.Stop();

            if (elementin != null && elementin.ToolTipText != null && elementin.ToolTipText.Length > 0)
            {
                hovertip = new ToolTip();

                hovertip.InitialDelay = 0;
                hovertip.AutoPopDelay = 30000;
                hovertip.ReshowDelay = 0;
                hovertip.IsBalloon = true;
                hovertip.ShowAlways = true;
                hovertip.SetToolTip(this, elementin.ToolTipText);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Focus();

            ClearHoverTip();

            if (ClickElement != null)
                ClickElement(this, e, elementin, elementin?.Tag);          // null if no element clicked
        }

    }
}

