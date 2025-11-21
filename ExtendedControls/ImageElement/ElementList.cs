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
            Min = new Point(Math.Min(Min.X, e.Bounds.X), Math.Min(Min.Y, e.Bounds.Y));
            Max = new Point(Math.Max(Max.X, e.Bounds.Right), Math.Max(Max.Y, e.Bounds.Bottom));
        }
        public void Insert(int index, Element e)
        {
            elements.Insert(index,e);
            Min = new Point(Math.Min(Min.X, e.Bounds.X), Math.Min(Min.Y, e.Bounds.Y));
            Max = new Point(Math.Max(Max.X, e.Bounds.Right), Math.Max(Max.Y, e.Bounds.Bottom));
        }

        public void AddRange(IEnumerable<Element> e)
        {
            foreach (var x in e)
                Add(x);
        }

        public void AddRange(List e)
        {
            foreach (var x in e)
                Add(x);
        }

        public void Remove(Element e)
        {
            int i = elements.IndexOf(e);
            Remove(i);
        }

        public void Remove(int i)
        {
            elements.RemoveAt(i);
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
            foreach (Element c in elements)
            {
                c.Translate(offset.X, offset.Y);
            }
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

        private List<Element> elements = new List<Element>();

        private void Recalc()
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
}

