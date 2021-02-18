/*
 * Copyright © 2021 EDDiscovery development team
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
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    // apply a ExtPictureBox and a ExtScrollBar to this

    public class ExtPictureBoxScroll : Panel      
    {
        public bool VerticalScrollBarDockRight { get; set; } = true;        // true for dock right

        public int ScrollBarWidth { get { return Font.ScalePixels(24); } }       // if internal

        // disabling it makes the picturebox be the same size as the client area, enabling it means the picture box grows with the data
        public bool ScrollBarEnabled { get { return scrollbarenabled; } set { scrollbarenabled = value; PerformLayout(); } }

        #region Implementation
        public ExtPictureBoxScroll() : base()
        {
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {  // as controls are added, remember them in local variables.
            if (e.Control is PictureBox)
            {
                pbox = e.Control as ExtPictureBox;
                pbox.MouseWheel += Wheel;
            }
            else if (e.Control is ExtScrollBar)
            {
                vsc = e.Control as ExtScrollBar;
                vsc.Scroll += new System.Windows.Forms.ScrollEventHandler(ScrollBarMoved);
            }
            else
                Debug.Assert(true, "Picture Box view Scroller Panel requires PictureBox and VScrollBarCustom to be added");
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            int left = 0;
            int right = ClientRectangle.Width;

            bool scrollbaron = vsc != null && ScrollBarEnabled;

            if ( scrollbaron )      // attach to right or left..
            {
                vsc.Size = new Size(ScrollBarWidth, ClientSize.Height);

                if (!VerticalScrollBarDockRight)
                {
                    vsc.Location = new Point(left, 0 );
                    left += ScrollBarWidth;
                }
                else
                {
                    right -= ScrollBarWidth;
                    vsc.Location = new Point(right, 0);
                }
            }

            if (pbox != null)                       // finally, put the dgv between left and right
            {
                pbox.Location = new Point(left, -(scrollbaron ? vsc.Value : 0));
                pbox.Size = new Size(right-left, pbox.Height);
            }

            UpdateScrollBar();
        }

        public void Render(Size? margin = null)
        {
            if (pbox != null)
            {
                if (ScrollBarEnabled)
                {
                    pbox.Render(true, margin: margin);
                }
                else
                {
                    pbox.Height = ClientRectangle.Height;
                    pbox.Render(false, margin: margin);
                }
            }

            UpdateScrollBar();
        }

        public void UpdateScrollBar()
        {
            if (pbox != null && vsc != null && ScrollBarEnabled) // may not be attached at various design points
            {
                Size sz = pbox.DisplaySize();
                int range = sz.Height - ClientSize.Height;      // +ve if display > height
                if ( range < 0 )                // if items are smaller than window
                {
                    pbox.Location = new Point(pbox.Left, 0);
                    range = 0;
                }

                int offset = -pbox.Location.Y;                  // <0 its scrolled..
                vsc.SetValueMaximumLargeChange(offset, pbox.Height, ClientRectangle.Height);
            }
        }

        protected virtual void Wheel(object sender, MouseEventArgs e)   // wheel changed, move vsc
        {
            if (vsc != null && ScrollBarEnabled)
            {
                vsc.Nudge(e.Delta > 0);
            }
        }

        protected virtual void ScrollBarMoved(object sender, ScrollEventArgs e)     // scroll bar moved, move dgv
        {
            pbox.Location = new Point(pbox.Left, -vsc.Value);
        }

        #endregion

        #region Variables

        private ExtPictureBox pbox;
        private ExtScrollBar vsc;
        private bool scrollbarenabled = true;

        #endregion
    }
}
