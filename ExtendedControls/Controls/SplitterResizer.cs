/*
 * Copyright 2016-2019 EDDiscovery development team
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
    // resize the parent, either height or width. Place inside the parent container and dock to the size required
    public class ExtSplitterResizeParent : Splitter, IThemeable
    {
        // Use MinSize to set minimum parent size, use Dock to indicate direction of growth, use MaxSize to indicate maximum
        // if you dock it between dockable containers, the base functionality will apply - beware

        public int MaxSize { get; set; } = int.MaxValue;        // Maximum size of move

        public ExtSplitterResizeParent()
        {
            MinSize = 10;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (splitstart.X>-99999)
            {
                if ( Dock == DockStyle.Bottom)
                {
                    int delta = this.PointToScreen(e.Location).Y - splitstart.Y;
                    int height = Math.Max(MinSize, Math.Min(parentbounds.Height + delta, MaxSize));
                    Parent.Height = height;
                    //System.Diagnostics.Debug.WriteLine($"Splitter height {e.Location} {Parent.Size} {delta}");
                }
                else if (Dock == DockStyle.Right)
                {
                    int delta = this.PointToScreen(e.Location).X - splitstart.X;
                    int width = Math.Max(MinSize, Math.Min(parentbounds.Width + delta, MaxSize));
                    Parent.Width = width;
                    //System.Diagnostics.Debug.WriteLine($"Splitter width {e.Location} {Parent.Size} {delta}");
                }
                else if (Dock == DockStyle.Left)
                {
                    int delta = this.PointToScreen(e.Location).X - splitstart.X;
                    if (parentbounds.Width - delta < MinSize)
                        delta = parentbounds.Width - MinSize;
                    if (parentbounds.Width - delta > MaxSize)
                        delta = parentbounds.Width - MaxSize;
                    Parent.Bounds = new Rectangle(parentbounds.Left + delta, parentbounds.Top, parentbounds.Width - delta, parentbounds.Height);
                    //System.Diagnostics.Debug.WriteLine($"Splitter left {e.Location} {Parent.Size} {delta}");
                }
                else if (Dock == DockStyle.Top)
                {
                    int delta = this.PointToScreen(e.Location).Y - splitstart.Y;
                    if (parentbounds.Height - delta < MinSize)
                        delta = parentbounds.Height - MinSize;
                    if (parentbounds.Height - delta > MaxSize)
                        delta = parentbounds.Height - MaxSize;
                    Parent.Bounds = new Rectangle(parentbounds.Left, parentbounds.Top + delta, parentbounds.Width, parentbounds.Height - delta);
                    //System.Diagnostics.Debug.WriteLine($"Splitter top {e.Location} {Parent.Size} {delta}");
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && Parent!=null)
            {
                splitstart = this.PointToScreen(e.Location);
                parentbounds = Parent.Bounds;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            splitstart = new Point(-99999, -99999);
        }

        public bool Theme(Theme t, Font fnt)
        {
            return true; // no action, do children
        }

        private Point splitstart = new Point(-99999, -99999);
        private Rectangle parentbounds;
    }
}
