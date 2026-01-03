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

using ExtendedControls.ImageElement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPictureBox : PictureBox, IThemeable, IEnumerable<ImageElement.Element>
    {
        public delegate void OnElement(object sender, MouseEventArgs eventargs, ImageElement.Element i, object tag);
        public event OnElement EnterElement;
        public event OnElement LeaveElement;
        public event OnElement ClickElement;

        public Action<ExtPictureBox> ImageChanged;              // call back on Rendered and Image change - does not appear that PictureBox has a native version

        public Color FillColor { get; set; } = Color.Transparent;         // fill the bitmap with this colour before pasting the bitmaps in

        public bool FreezeTracking { get; set; } = false;        // when set, all mouse movement tracking is turned off

        private ImageElement.List Elements = new ImageElement.List();

        #region Interface

        public ExtPictureBox()
        {
            hovertimer.Interval = 250;
            hovertimer.Tick += HoverEnd;
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }

        public int Count { get { return Elements.Count; } }

        public void Add(ImageElement.Element i)
        {
            i.PictureBoxParent = this;
            Elements.Add(i);
        }

        public void AddDrawFirst(ImageElement.Element i)        // add to front of queue, draw first
        {
            i.PictureBoxParent = this;
            Elements.Insert(0, i);
        }

        public void AddRange(List<ImageElement.Element> list)
        {
            Elements.AddRange(list);
            foreach (var x in list)
                x.PictureBoxParent = this;
        }

        public void AddRange(ImageElement.List list)
        {
            Elements.AddRange(list);
            foreach (var x in list)
                x.PictureBoxParent = this;
        }

        // topleft, autosized
        public ImageElement.Element AddTextAutoSize(Point topleft, Size max, string label, Font fnt, Color c, Color backcolour, float backscale, Object tag = null, string tiptext = null, StringFormat frmt = null)
        {
            ImageElement.Element lab = new ImageElement.Element();
            lab.TextAutoSize(topleft, max, label, fnt, c, backcolour, backscale, tag, tiptext, frmt);
            Elements.Add(lab);
            return lab;
        }

        public List<ImageElement.Element> AddTextAutoSize(Point topleft, Size max, string[] label, int linemargin, bool upwards, Font fnt, Color c, Color backcolour, float backscale, Object tag = null, string tiptext = null, StringFormat frmt = null)
        {
            List<ImageElement.Element> elements = new List<ImageElement.Element>();
            Point curpos = topleft;
            foreach (var x in label)
            {
                if (x != null)
                {
                    var ie = AddTextAutoSize(curpos, max, x, fnt, c, backcolour, backscale, tag, tiptext, frmt);
                    elements.Add(ie);
                    curpos.Y = ie.Bounds.Bottom + linemargin;
                }
            }

            if ( upwards)
            {
                curpos.Y -= linemargin;                     // remove extra offset
                int offset = curpos.Y - topleft.Y;          // and this is the total size
                foreach (var i in elements)
                    i.Bounds = new Rectangle(i.Bounds.Left, i.Bounds.Top - offset, i.Bounds.Width, i.Bounds.Height);
            }

            return elements;
        }

         // centre pos, autosized
        public ImageElement.Element AddTextCentred(Point poscentrehorz, Size max, string label, Font fnt, Color c, Color backcolour, float backscale, Object tag = null, string tiptext = null, StringFormat frmt = null)
        {
            ImageElement.Element lab = new ImageElement.Element();
            lab.TextCentreAutoSize(poscentrehorz, max, label, fnt, c, backcolour, backscale, tag, tiptext, frmt);
            Elements.Add(lab);
            return lab;
        }

        public ImageElement.Element AddImage(Rectangle p, Image img, Object tag = null, string tiptext = null, bool imgowned = true)    // make sure pushes it in..
        {
            ImageElement.Element lab = new ImageElement.Element();
            lab.Bitmap(p, img, tag, tiptext, imgowned);
            Elements.Add(lab);
            return lab;
        }

        public ImageElement.Element AddOwnerDraw(Action<Graphics, ImageElement.Element> callback, Rectangle p, Object tag = null, string tiptext = null)    // make sure pushes it in..
        {
            ImageElement.Element lab = new ImageElement.Element();
            lab.OwnerDraw(callback, p, tag, tiptext);
            Elements.Add(lab);
            return lab;
        }

        public ImageElement.Element AddHorizontalDivider(Color c, Rectangle area, float width = 1.0f, int offset = 0, Object t = null, string tt = null)
        {
            ImageElement.Element lab = new ImageElement.Element();
            lab.HorizontalDivider(c, area, width, offset, t, tt);
            Elements.Add(lab);
            return lab;

        }
        public void ClearImageList()        // clears the element list, not the image.  call render to do this
        {
            Elements.Clear();
        }

        public Size DisplaySize()
        {
            return Elements.DisplaySize;
        }

        // taking image elements, and minimum size/margin, create a new bitmap
        // null if no elements
        public Bitmap RenderBitmap(Size? minsize = null, Size? margin = null)
        {
            return Elements.Paint(FillColor, minsize, margin);
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

            SortedList<int, ImageElement.Element> indexes = new SortedList<int, ImageElement.Element>();

            for (int ix = 0; ix < Elements.Count;)
            {
                ImageElement.Element i = Elements[ix];

                // if not tried before, and overlays
                if (!indexes.ContainsKey(ix) && i.Visible && i.Bounds.IntersectsWith(area))
                {
                    // System.Diagnostics.Debug.WriteLine($"ReDraw {i.Location}");
                    indexes.Add(ix, i);
                    area.Intersect(i.Bounds);         // increase area
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
                            kvp.Value.Paint(gr);
                        }

                        kvp.Value.OwnerDrawCallback?.Invoke(gr, kvp.Value);
                    }
                }

                Invalidate();
            }
        }

        public void SwapToAlternateImage(ImageElement.Element i)
        {
            if (i.SwapImages(Image))
                Invalidate();
        }

        public void RemoveItem(ImageElement.Element orgimg, Color? backcolour)
        {
            if ( Elements.Contains(orgimg))
            {
                if (!backcolour.HasValue)
                    backcolour = FillColor;
                Bitmap b = Image as Bitmap;
                BaseUtils.BitMapHelpers.ClearBitmapArea(b, orgimg.Bounds, backcolour.Value); // fill old element with back colour even if transparent
                Elements.Remove(orgimg);
            }
        }

        public void AddItem(ImageElement.Element newitem)
        {
            using (Graphics gr = Graphics.FromImage(Image))     // paint new data
            {
                if (newitem.Image != null)
                    gr.DrawImage(newitem.Image, newitem.Bounds);
                newitem.OwnerDrawCallback?.Invoke(gr, newitem);
                Elements.Add(newitem);
            }

            Invalidate();
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

            if (elementin != null && !elementin.Bounds.Contains(eventargs.Location))       // go out..
            {
                //System.Diagnostics.Debug.WriteLine("Leave element " + elementin.Location);
                elementin.MouseOver = false;

                LeaveElement?.Invoke(this, eventargs, elementin, elementin.Tag);
                if ( elementin.Enabled )
                    elementin.Leave?.Invoke(this, elementin);

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
                    ImageElement.Element i = Elements[ix];
                
                    if (i.Visible && i.Bounds.Contains(eventargs.Location))
                    {
                        elementin = i;
                        elementin.MouseOver = true;

                        //System.Diagnostics.Debug.WriteLine("Enter element " + elementin.Location + " Mouse pos " + eventargs.Location);

                        if (elementin.Enabled)
                            elementin.Enter?.Invoke(this, elementin);

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
            if (elementin?.Enabled == true)
            {
                elementin.MouseDown?.Invoke(this, elementin, e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (elementin?.Enabled == true)
            {
                elementin.MouseUp?.Invoke(this, elementin, e);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Focus();

            ClearHoverTip();

            if (elementin?.Enabled == true)
            {
                if (e.Button == MouseButtons.Right && elementin?.ContextMenuStrip != null)      // right click and context menu strip
                {
                    elementin.ContextMenuStrip.Tag = elementin;
                    elementin.ContextMenuStrip.Show(this.PointToScreen(elementin.BottomRight));    // show right click
                }
                else
                {
                    elementin?.Click?.Invoke(this, elementin, e);            // tell element
                    ClickElement?.Invoke(this, e, elementin, elementin?.Tag);          // tell overall class
                }
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

        public IEnumerator<Element> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private ImageElement.Element elementin = null;
        private Timer hovertimer = new Timer();
        private ToolTip hovertip = null;
        private Point hoverpos;

    }
}

