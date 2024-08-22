/*
 * Copyright 2021-2024 EDDiscovery development team
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
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    // apply a ExtPictureBox and a ExtScrollBar to this panel.
    // Support a close icon above the scroll bar if a ButtonDrawn is added to it.  Visibility of this controls if shown
    public class ExtPictureBoxScroll : Panel      
    {
        public bool VerticalScrollBarDockRight { get; set; } = true;        // true for dock right
        public int ScrollBarWidth { get { return Font.ScaleScrollbar(); } }
        // disabling it makes the picturebox be the same size as the client area, enabling it means the picture box grows with the data
        public bool ScrollBarEnabled { get { return scrollbarenabled; } set { scrollbarenabled = value; PerformLayout(); } }

        #region Implementation
        public ExtPictureBoxScroll() : base()
        {
        }

        // call this instead of the picturebox render
        // if resizepercentage > 0, it resizes to the % of the height of the parent.
        // if 0, the size is set by the size of this control and not altered.
        public void Render(int resizepercentage = 0, Size? margin = null)     
        {
            if (pbox != null)
            {
                if (ScrollBarEnabled)
                {
                    pbox.Render(true, margin: margin);
                }
                else
                {
                    pbox.Render(false, margin: margin);
                }

                if (resizepercentage > 0)
                {
                    Height = Math.Max(4, Math.Min(pbox.Image.Size.Height, Parent.Height * resizepercentage / 100));
                }
            }

            UpdateScrollBar();
        }

        // MUST add a picture box, and Scroll bar
        // CAN add a ExtButtonDrawn as a close icon

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
            else if (e.Control is ExtButtonDrawn)
            {
                closeicon = e.Control as ExtButtonDrawn;
            }
            else
                Debug.Assert(true, "Picture Box Scroller Panel requires ExtPictureBox and ExtScrollBar to be added");
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            int left = 0;
            int right = ClientRectangle.Width;

            bool closeiconon = closeicon != null && closeicon.Visible;

            if (closeiconon)     // position close icon if present and visible
            {
                closeicon.Bounds = new Rectangle(VerticalScrollBarDockRight ? right - ScrollBarWidth : 0, 0, ScrollBarWidth, ScrollBarWidth);
            }

            if ( vsc != null )      // attach to right or left..
            {
                int vpos = closeiconon ? ScrollBarWidth : 0;     // position scroll top, dependent on close icon

                vsc.Size = new Size(ScrollBarWidth, ClientSize.Height - vpos);

                if (!VerticalScrollBarDockRight)
                {
                    vsc.Location = new Point(left, vpos );
                    left += ScrollBarWidth;
                }
                else
                {
                    right -= ScrollBarWidth;
                    vsc.Location = new Point(right, vpos);
                }
            }

            if (pbox != null)                                   // finally, put the box between left and right
            {
                pbox.Location = new Point(left, pbox.Top);      // position left only, leave positioning for update scroll bar
                pbox.Size = new Size(right-left, pbox.Height);  // don't change the height
            }

            UpdateScrollBar();
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

        private void UpdateScrollBar()
        {
            if (pbox != null && vsc != null ) // may not be attached at various design points
            {
                if (ScrollBarEnabled)
                {
                    int range = pbox.Height - ClientSize.Height;        //+ve if pbox > available

                    if (range < 0)                              // if we don't need the scroll bar, put back to zero
                        vsc.Value = 0;
                    else if (vsc.Value >= range)                // if we are past the range, set to maximum
                        vsc.Value = range;

                    pbox.Location = new Point(pbox.Left, -vsc.Value);

                    //System.Diagnostics.Debug.WriteLine("pscroll {0},{1},{2}", offset, pbox.Height - 1, ClientRectangle.Height);

                    vsc.SetValueMaximumLargeChange(vsc.Value, pbox.Height - 1, ClientRectangle.Height);
                }
                else
                {
                    pbox.Location = new Point(pbox.Left, 0);
                    vsc.SetValueMaximumLargeChange(0, 10, 100);    // inert the bar, give it numbers which would make it go empty/hide
                }
            }
        }

        #endregion

        #region Variables

        private ExtPictureBox pbox;
        private ExtScrollBar vsc;
        private ExtButtonDrawn closeicon;
        private bool scrollbarenabled = true;

        #endregion
    }
}
