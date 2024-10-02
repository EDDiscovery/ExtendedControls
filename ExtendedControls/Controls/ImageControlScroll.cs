/*
 * Copyright © 2022-2022 EDDiscovery development team
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
 *
*/

using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls.Controls
{
    public partial class ImageControlScroll : Panel
    {
        public bool VerticalScrollBarDockRight { get; set; } = true;        // true for dock right

        public int ScrollBarWidth { get { return Font.ScaleScrollbar(); } }      

        public int ImageControlMinimumHeight { get; set; } = 0;     // if zero, image control height is kept. If non zero, this is the minimum, and stretch out to available

        public bool ScrollBarEnabled { get { return scrollbarenabled; } set { scrollbarenabled = value; PerformLayout(); } }

        #region Implementation

        protected override void OnControlAdded(ControlEventArgs e)
        {  // as controls are added, remember them in local variables.
            if (e.Control is ImageControl)
            {
                imgctrl = e.Control as ImageControl;
                imgctrl.MouseWheel += Wheel;
            }
            else if (e.Control is ExtScrollBar)
            {
                vsc = e.Control as ExtScrollBar;
                vsc.Scroll += new System.Windows.Forms.ScrollEventHandler(ScrollBarMoved);
            }
            else
                System.Diagnostics.Debug.Assert(true, "Image Control Scroll requires ImageControl and ExtScrollBar to be added");
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            int left = 0;
            int right = ClientRectangle.Width;

            if (vsc != null)      // attach to right or left..
            {
                vsc.Size = new Size(ScrollBarWidth, ClientSize.Height);

                if (!VerticalScrollBarDockRight)
                {
                    vsc.Location = new Point(left, 0);
                    left += ScrollBarWidth;
                }
                else
                {
                    right -= ScrollBarWidth;
                    vsc.Location = new Point(right, 0);
                }
            }

            if (imgctrl != null)                       // finally, put the box between left and right
            {
                imgctrl.Location = new Point(left, imgctrl.Top);      // position left only, leave positioning for update scroll bar

                // always stretch to width available
                if ( ImageControlMinimumHeight>0)                           // if set, set to either available or this min height
                    imgctrl.Size = new Size(right - left, System.Math.Max(ClientRectangle.Height, ImageControlMinimumHeight));  
                else
                    imgctrl.Size = new Size(right - left, imgctrl.Height);  // don't change the height, but change the width to suit.

               // System.Diagnostics.Debug.WriteLine($"ICS layout imgctrl {imgctrl.Bounds} scroll panel {Bounds} sizing {right - left}");
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
            imgctrl.Location = new Point(imgctrl.Left, -vsc.Value);
        }

        private void UpdateScrollBar()
        {
            if (imgctrl != null && vsc != null) // may not be attached at various design points
            {
                if (ScrollBarEnabled)
                {
                    int range = imgctrl.Height - ClientSize.Height;        //+ve if pbox > available

                    if (range < 0)                              // if we don't need the scroll bar, put back to zero
                        vsc.Value = 0;
                    else if (vsc.Value >= range)                // if we are past the range, set to maximum
                        vsc.Value = range;

                    imgctrl.Location = new Point(imgctrl.Left, -vsc.Value);

                   // System.Diagnostics.Debug.WriteLine($"ICS imgctrl {imgctrl.Bounds}");
                  
                    //System.Diagnostics.Debug.WriteLine("pscroll {0},{1},{2}", offset, pbox.Height - 1, ClientRectangle.Height);

                    vsc.SetValueMaximumLargeChange(vsc.Value, imgctrl.Height - 1, ClientRectangle.Height);
                }
                else
                {
                    imgctrl.Location = new Point(imgctrl.Left, 0);
                    vsc.SetValueMaximumLargeChange(0, 10, 100);    // inert the bar, give it numbers which would make it go empty/hide
                }
            }
        }

        #endregion

        #region Variables

        private ImageControl imgctrl;
        private ExtScrollBar vsc;
        private bool scrollbarenabled = true;

        #endregion
    }
}
