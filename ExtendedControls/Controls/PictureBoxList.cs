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
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPictureBox : PictureBox
    {
        // Image List holds set of ImageElements, and auto collates min/max x&y and size.
        // with shift code to move whole set around

        [System.Diagnostics.DebuggerDisplay("{elements.Count} sz {Size} min {Min} max {Max} ")]
        public class ImageList 
        {
            public int Count { get { return elements.Count; } }
            public Size Size => new Size(Max.X-Min.X,Max.Y-Min.Y);  
            public Point Min { get; private set; } = new Point(int.MaxValue, int.MaxValue);
            public Point Max { get; private set; } = new Point(int.MinValue, int.MinValue);
            public IEnumerable<ImageElement> Enumerable { get { return elements; } }

            public ImageElement this[int i] { get => elements[i]; }

            public object Tag { get; set; }

            public void Add(ImageElement e)
            {
                elements.Add(e);
                Min = new Point(Math.Min(Min.X, e.Location.X), Math.Min(Min.Y, e.Location.Y));
                Max = new Point(Math.Max(Max.X, e.Location.Right), Math.Max(Max.Y, e.Location.Bottom));
            }

            public void AddRange(IEnumerable<ImageElement> e)
            {
                foreach (var x in e)
                    Add(x);
            }

            public void AddRange(ImageList e)
            {
                foreach (var x in e.Enumerable)
                    Add(x);
            }

            public void Clear()
            {
                Min = new Point(int.MaxValue, int.MaxValue);
                Max = new Point(int.MinValue, int.MinValue);
                elements.Clear();
            }

            // shift so no negative values, top left is most left/top pixel at 0,0
            public void ShiftTopLeft()      
            {
                Point shift = new Point(-Min.X, -Min.Y);
                Shift(shift);
            }

            public void Shift(Point offset)
            {
                foreach (ImageElement c in elements)
                {
                    c.Translate(offset.X, offset.Y);
                }
                Min = new Point(Min.X + offset.X, Min.Y + offset.Y);
                Max = new Point(Max.X + offset.X, Max.Y + offset.Y);
            }

            private List<ImageElement> elements = new List<ImageElement>();
        }
    }
}

