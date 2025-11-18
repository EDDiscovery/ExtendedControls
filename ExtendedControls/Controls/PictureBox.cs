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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPictureBox : PictureBox, IThemeable
    {
        public delegate void OnElement(object sender, MouseEventArgs eventargs, ImageElement i, object tag);
        public event OnElement EnterElement;
        public event OnElement LeaveElement;
        public event OnElement ClickElement;

        public Action<ExtPictureBox> ImageChanged;              // call back on Rendered and Image change - does not appear that PictureBox has a native version

        public Color FillColor { get; set; } = Color.Transparent;         // fill the bitmap with this colour before pasting the bitmaps in

        public bool FreezeTracking { get; set; } = false;        // when set, all mouse movement tracking is turned off

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
            i.Parent = this;
            Elements.Add(i);
        }

        public void AddDrawFirst(ImageElement i)        // add to front of queue, draw first
        {
            i.Parent = this;
            Elements.Insert(0, i);
        }

        public void AddRange(List<ImageElement> list)
        {
            Elements.AddRange(list);
            foreach (var x in list)
                x.Parent = this;
        }

        public void AddRange(ImageList list)
        {
            Elements.AddRange(list.Enumerable);
            foreach (var x in list.Enumerable)
                x.Parent = this;
        }

        // topleft, autosized
        public ImageElement AddTextAutoSize(Point topleft, Size max, string label, Font fnt, Color c, Color backcolour, float backscale, Object tag = null, string tiptext = null, StringFormat frmt = null)
        {
            ImageElement lab = new ImageElement();
            lab.TextAutoSize(topleft, max, label, fnt, c, backcolour, backscale, tag, tiptext, frmt);
            Elements.Add(lab);
            return lab;
        }

        public List<ImageElement> AddTextAutoSize(Point topleft, Size max, string[] label, int linemargin, bool upwards, Font fnt, Color c, Color backcolour, float backscale, Object tag = null, string tiptext = null, StringFormat frmt = null)
        {
            List<ImageElement> elements = new List<ImageElement>();
            Point curpos = topleft;
            foreach (var x in label)
            {
                if (x != null)
                {
                    var ie = AddTextAutoSize(curpos, max, x, fnt, c, backcolour, backscale, tag, tiptext, frmt);
                    elements.Add(ie);
                    curpos.Y = ie.Location.Bottom + linemargin;
                }
            }

            if ( upwards)
            {
                curpos.Y -= linemargin;                     // remove extra offset
                int offset = curpos.Y - topleft.Y;          // and this is the total size
                foreach (var i in elements)
                    i.Location = new Rectangle(i.Location.Left, i.Location.Top - offset, i.Location.Width, i.Location.Height);
            }

            return elements;
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

        // taking image elements, and minimum size/margin, create a new bitmap
        // null if no elements
        public Bitmap RenderBitmap(Size? minsize = null, Size? margin = null)
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


                Bitmap newrender = PaintBitmap(size);
                return newrender;
            }
            else
                return null;
        }

        // taking image elements, draw to main bitmap. set if resize control, and if we have a min size of bitmap, or a margin
        public void Render(bool resizecontrol = true, Size? minsize = null, Size? margin = null)
        {
            // render all elements, images or self drawn, into the bitmap

            Bitmap newrender = RenderBitmap(minsize, margin);

            if ( newrender != null )
            {
                Image lastimage = Image;

                Image = newrender;      // and replace the image in one go, to try and minimise distortion
                ImageChanged?.Invoke(this);     // and indicate image has changed

                if (resizecontrol)
                    this.Size = new Size(newrender.Width, newrender.Height);

                lastimage?.Dispose();
            }
            else
            {
                Image?.Dispose();
                Image = null;       // nothing, null image
                ImageChanged?.Invoke(this);     // and indicate image has changed
            }
        }

        public void Refresh(Rectangle area)
        {
            if (Image == null)      // not rendered yet
                return;

            SortedList<int, ImageElement> indexes = new SortedList<int, ImageElement>();

            for (int ix = 0; ix < Elements.Count;)
            {
                ImageElement i = Elements[ix];

                // if not tried before, and overlays
                if (!indexes.ContainsKey(ix) && i.Visible && i.Location.IntersectsWith(area))
                {
                    // System.Diagnostics.Debug.WriteLine($"ReDraw {i.Location}");
                    indexes.Add(ix, i);
                    area.Intersect(i.Location);         // increase area
                    ix = 0;
                }
                else
                    ix++;
            }

            if (indexes.Count > 0)  // if anything to do..
            {
                using (Graphics gr = Graphics.FromImage(Image))
                {
                    foreach (var kvp in indexes)        // in order, since its a sorted list
                    {
                        if (kvp.Value.Image != null)    // if we have an image, repaint it
                        {
                            PaintElement(gr, kvp.Value);
                        }

                        kvp.Value.OwnerDrawCallback?.Invoke(gr, kvp.Value);
                    }
                }

                Invalidate();
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

        #endregion

        #region Painting

        // paint an element, thru its ownerdraw and if it has an Image, painting it (taking into account Enabled/ShowDisabled)
        private void PaintElement(Graphics gr, ImageElement i)
        {
            if (i.Visible)
            {
                if (i.Image != null)
                {
                    if (!i.Enabled || i.ShowDisabled)       // if show with disabled scaling
                    {
                        ColorMatrix alphaMatrix = new ColorMatrix();
                        alphaMatrix.Matrix00 = alphaMatrix.Matrix11 = alphaMatrix.Matrix22 = alphaMatrix.Matrix44 = 1;
                        alphaMatrix.Matrix33 = i.DisabledScaling;

                        using (ImageAttributes alphaAttributes = new ImageAttributes())
                        {
                            alphaAttributes.SetColorMatrix(alphaMatrix);
                            gr.DrawImage(i.Image, new Rectangle(i.Location.X, i.Location.Y, i.Size.Width, i.Size.Height), 0, 0, i.Size.Width, i.Size.Height, GraphicsUnit.Pixel, alphaAttributes);
                        }
                    }
                    else
                    {
                        // System.Diagnostics.Debug.WriteLine($"Draw {i.Tag} @ {i.Location}");
                        gr.DrawImage(i.Image, i.Location);
                    }
                }
                else
                {
                    //   System.Diagnostics.Debug.WriteLine($"Draw {i.Tag} @ {i.Location} no image");
                }

                i.OwnerDrawCallback?.Invoke(gr, i);
            }
        }

        // into a new Bitmap, paint the whole scene
        private Bitmap PaintBitmap(Size size)
        {
            //System.Diagnostics.Debug.WriteLine($"Picture box draw {size}");
            Bitmap newrender = new Bitmap(size.Width, size.Height);

            if (!FillColor.IsFullyTransparent())
            {
                BaseUtils.BitMapHelpers.FillBitmap(newrender, FillColor);
            }

            using (Graphics gr = Graphics.FromImage(newrender))
            {
                foreach (ImageElement i in Elements)
                    PaintElement(gr,i);
            }

            return newrender;
        }

        #endregion

        #region Dispose
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

        #endregion

        #region Mouse

        protected override void OnMouseMove(MouseEventArgs eventargs)
        {
            base.OnMouseMove(eventargs);

            if (FreezeTracking)
                return;

            if (elementin != null && !elementin.Location.Contains(eventargs.Location))       // go out..
            {
                //System.Diagnostics.Debug.WriteLine("Leave element " + elementin.Location);
                elementin.MouseOver = false;

                LeaveElement?.Invoke(this, eventargs, elementin, elementin.Tag);
                elementin.Leave?.Invoke(this,elementin);

                if (elementin.AltImage != null && elementin.AlternateImageWhenMouseOver && elementin.InAltImage)
                {
                    elementin.SwapImages(Image);
                    Invalidate();
                }
                elementin = null;
            }

            if (elementin == null)      // is in?
            {
                for( int ix = Elements.Count-1; ix >= 0; ix--)      // we paint in 0..N-1 order, so N-1 has priority.  So pick backwards
                {
                    ImageElement i = Elements[ix];
                
                    if (i.Visible && i.Location.Contains(eventargs.Location))
                    {
                        elementin = i;
                        elementin.MouseOver = true;

                        //System.Diagnostics.Debug.WriteLine("Enter element " + elementin.Location + " Mouse pos " + eventargs.Location);

                        elementin.Enter?.Invoke(this,elementin);

                        if (elementin.AltImage != null && elementin.AlternateImageWhenMouseOver && !elementin.InAltImage)
                        {
                            elementin.SwapImages(Image);
                            Invalidate();
                        }

                        EnterElement?.Invoke(this, eventargs, elementin, elementin.Tag);
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            elementin?.MouseDown?.Invoke(this, elementin,e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            elementin?.MouseUp?.Invoke(this, elementin,e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Focus();

            ClearHoverTip();

            if (e.Button == MouseButtons.Right && elementin?.ContextMenuStrip != null)      // right click and context menu strip
            {
                elementin.ContextMenuStrip.Tag = elementin;
                elementin.ContextMenuStrip.Show(this.PointToScreen(elementin.PositionBottomRight));    // show right click
            }
            else
            {
                elementin?.Click?.Invoke(this, elementin, e);            // tell element
                ClickElement?.Invoke(this, e, elementin, elementin?.Tag);          // tell overall class
            }
        }

        #endregion

        #region Hover
        private void ClearHoverTip()
        {
            hovertimer.Stop();

            if (hovertip != null)
            {
                hovertip.Dispose();
                hovertip = null;
            }
        }

        private void HoverEnd(object sender, EventArgs e)
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

        #endregion

        public bool Theme(Theme t, Font fnt)
        {
            return false;   // no action, no children
        }

        private ImageElement elementin = null;
        private Timer hovertimer = new Timer();
        private ToolTip hovertip = null;
        private Point hoverpos;

    }
}

