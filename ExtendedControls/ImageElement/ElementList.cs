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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace ExtendedControls.ImageElement
{
    [System.Diagnostics.DebuggerDisplay("{elements.Count} sz {Size} min {Min} max {Max} ")]
    public class List : IDisposable, IEnumerable<Element>
    {
        public int Count { get { return elements.Count; } }
        public Size Size => new Size(Max.X - Min.X, Max.Y - Min.Y);
        public Size DisplaySize => new Size(Math.Max(0,Max.X), Math.Max(0,Max.Y));           // visible size, with pixels >= 0
        public Point Min { get; private set; } = new Point(int.MaxValue, int.MaxValue);
        public Point Max { get; private set; } = new Point(int.MinValue, int.MinValue);
        public Point MinOrDefault => new Point(Min.X == int.MaxValue ? 0 : Min.X, Min.Y == int.MaxValue ? 0 : Min.Y);       // if nothing is yet drawn, this will return 0,0. 
        public Point MaxOrDefault => new Point(Max.X == int.MinValue ? 0 : Max.X, Max.Y == int.MinValue ? 0 : Max.Y);       // if nothing is yet drawn, this will return 0,0. 
        public Point Centre { get => new Point((Min.X+Max.X)/2,(Min.Y+Max.Y)/2); }

        public IEnumerator<Element> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Element this[int i] { get => elements[i]; }

        public bool Contains(Element e) => elements.IndexOf(e) >= 0;

        public object Tag { get; set; }

        public void Add(Element e)
        {
            elements.Add(e);
            e.BoundsChanged += Recalc;      // BUG: WHEN in a list, moving it must cause a recalc of min/max
            Min = new Point(Math.Min(Min.X, e.Bounds.X), Math.Min(Min.Y, e.Bounds.Y));
            Max = new Point(Math.Max(Max.X, e.Bounds.Right), Math.Max(Max.Y, e.Bounds.Bottom));
        }
        public void Insert(int index, Element e)
        {
            elements.Insert(index,e);
            e.BoundsChanged += Recalc;
            Min = new Point(Math.Min(Min.X, e.Bounds.X), Math.Min(Min.Y, e.Bounds.Y));
            Max = new Point(Math.Max(Max.X, e.Bounds.Right), Math.Max(Max.Y, e.Bounds.Bottom));
        }

        public void AddRange(IEnumerable<Element> e)
        {
            foreach (var x in e)
                Add(x);
        }

        public void Remove(Element e)
        {
            int i = elements.IndexOf(e);
            e.BoundsChanged -= Recalc;
            Remove(i);
        }

        public void Remove(int i)
        {
            elements.RemoveAt(i);
            elements[i].BoundsChanged -= Recalc;
            Recalc();
        }

        public void Clear()
        {
            foreach (var e in elements)
            {
                e.Dispose();
            }
            elements.Clear();
            Min = new Point(int.MaxValue, int.MaxValue);
            Max = new Point(int.MinValue, int.MinValue);
        }

        // shift so no negative values, top left is most left/top pixel at 0,0
        public void ShiftTopLeft()
        {
            Point shift = new Point(-Min.X, -Min.Y);
            Shift(shift);
        }

        // shift so no negative values, top left is most left/top pixel at 0,0
        public void ShiftRight()
        {
            Point shift = new Point(-Min.X, 0);
            Shift(shift);
        }

        public void Shift(Point offset)
        {
            disablerecalc = true;       // stop repeated recalcs due to element calling boundschanged

            foreach (Element c in elements)
            {
                c.Translate(offset.X, offset.Y);
            }

            disablerecalc = false;

            Min = new Point(Min.X + offset.X, Min.Y + offset.Y);
            Max = new Point(Max.X + offset.X, Max.Y + offset.Y);
        }

        public void Dispose()
        {
            Clear();
        }

        // will return null if can't paint
        public Bitmap Paint(Color FillColor, Size? minsize = null, Size? margin = null)
        {
            Size size = DisplaySize;

            if (size.Width > 0 && size.Height > 0) // will be zero if no elements
            {
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

                Bitmap newrender = Paint(size,FillColor);
                return newrender;
            }
            else
                return null;
        }


        public Bitmap Paint(Size size, Color FillColor)
        {
            //System.Diagnostics.Debug.WriteLine($"Picture box draw {size}");
            Bitmap newrender = new Bitmap(size.Width, size.Height);

            if (!FillColor.IsFullyTransparent())
            {
                BaseUtils.BitMapHelpers.FillBitmap(newrender, FillColor);
            }

            using (Graphics gr = Graphics.FromImage(newrender))
            {
                foreach (ImageElement.Element i in elements)
                    i.Paint(gr);
            }

            return newrender;
        }


        private void Recalc()
        {
            if (!disablerecalc)
            {
                Min = new Point(int.MaxValue, int.MaxValue);
                Max = new Point(int.MinValue, int.MinValue);
                foreach (var e in elements)
                {
                    Min = new Point(Math.Min(Min.X, e.Bounds.X), Math.Min(Min.Y, e.Bounds.Y));
                    Max = new Point(Math.Max(Max.X, e.Bounds.Right), Math.Max(Max.Y, e.Bounds.Bottom));
                }
            }
        }

        private void Recalc(Element e)
        {
            Recalc();
        }

        // Image is optional and is not owned by ImageElements
        public void AddPictureTextHorzDivider(Rectangle area, Image image, Size imagesize,
                                       string text, Font font, bool wrap, StringAlignment alignment,
                                       bool horzdivider, Color textcolour, Color backcolour, int texttoimagemargin = 8)
        {
            using (StringFormat frmt = new StringFormat(wrap ? 0 : StringFormatFlags.NoWrap) { Alignment = alignment })
            {
                int textwidthavailable = area.Width - (image != null ? imagesize.Width : 0);

                var textie = new Element();
                textie.TextAutoSize(
                        area.Location,
                        new Size(textwidthavailable, area.Height),      // we allow the text to be this width, but the bitmap will be the size needed
                        text,
                        font,
                        textcolour,
                        backcolour,
                        1.0F,
                        frmt: frmt,
                        t:text
                       );

                var imageie = image != null ? new ImageElement.Element(new Rectangle(area.X, area.Y, imagesize.Width, imagesize.Height), image, imgowned: false) : null;
                int totalwidth = textie.Width + (imageie!=null ? imageie.Width + texttoimagemargin : 0);

                if (alignment == StringAlignment.Center)
                {
                    int areacentre = area.X + area.Width / 2;

                    if (imageie != null)
                    {
                        imageie.Location = new Point(areacentre - totalwidth / 2, textie.Y);
                        textie.Location = new Point(imageie.Right + texttoimagemargin, textie.Y);
                    }
                    else
                        textie.Location = new Point(areacentre - totalwidth / 2, textie.Y);
                }
                else if (alignment == StringAlignment.Far)
                {
                    textie.Location = new Point(area.Right - textie.Width, textie.Y);

                    if (imageie != null)
                        imageie.Location = new Point(textie.Left - imageie.Width - texttoimagemargin, imageie.Y);
                }
                else
                {
                    textie.Location = new Point(area.X + (imageie!=null ? imageie.Width + texttoimagemargin : 0), textie.Y);
                }

                int vpos = Math.Max(textie.Bottom, imageie?.Bottom ?? 0);

                if (imageie != null)
                    Add(imageie);

                Add(textie);

                if (horzdivider)
                    AddHorizontalDivider(textcolour.Multiply(0.4f), new Rectangle(imageie!=null ? imageie.Left : textie.Left, vpos, totalwidth, 8), 1, 4);
            }
        }

        public ImageElement.Element AddHorizontalDivider(Color c, Rectangle area, float width = 1.0f, int offset = 0, Object t = null, string tt = null)
        {
            ImageElement.Element lab = new ImageElement.Element();
            lab.HorizontalDivider(c, area, width, offset, t, tt);
            Add(lab);
            return lab;
        }

        private List<Element> elements = new List<Element>();
        private bool disablerecalc = false;
    }
}

