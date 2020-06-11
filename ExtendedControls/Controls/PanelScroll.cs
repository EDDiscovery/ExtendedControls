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
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPanelScroll : Panel               // Written because I could not get the manual autoscroll to work when controls dynamically added
    {
        public bool VerticalScrollBarDockRight { get; set; } = true;        // true for dock right
        public bool FlowControlsLeftToRight { get; set; } = false;        // if true, position controls left to right overflow

        public int ScrollBarWidth { get { return Font.ScalePixels(24); } }

        public ExtScrollBar ScrollBar = null;

        public int ScrollOffset { get { return -scrollpos; } }

        private int scrollpos = 0;

        public ExtPanelScroll()
        {
            MouseWheel += Control_MouseWheel;         // grab the controls mouse wheel and direct to our scroll
        }

        // override DisplayRectangle so children get the rectangle without the size of the scroll bar area.
        public override Rectangle DisplayRectangle
        {
            get
            {
                if (VerticalScrollBarDockRight)
                    return new Rectangle(base.DisplayRectangle.Left, base.DisplayRectangle.Top, base.DisplayRectangle.Width - ScrollBarWidth, base.DisplayRectangle.Height);
                else
                    return new Rectangle(base.DisplayRectangle.Left + ScrollBarWidth, base.DisplayRectangle.Top, base.DisplayRectangle.Width - ScrollBarWidth, base.DisplayRectangle.Height);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is ExtScrollBar)
            {
                ScrollBar = e.Control as ExtScrollBar;
                ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(OnScrollBarChanged);
                ScrollBar.Name = "VScrollPanel";
            }
            else
            {
                e.Control.LocationChanged += Control_LocationChanged;   // and any location/size/visible changes means scroll bar is changed
                e.Control.VisibleChanged += Control_VisibleChanged;
                e.Control.SizeChanged += Control_SizeChanged;
            }

            e.Control.MouseWheel += Control_MouseWheel;         // grab the controls mouse wheel and direct to our scroll, including the ExtScrollBar

            base.OnControlAdded(e);
        }

        protected override void OnResize(EventArgs eventargs)       // added in resize event - should have been in here
        {
            base.OnResize(eventargs);       // call base class to let any hooks run

            //System.Diagnostics.Debug.WriteLine("Panel Scroll Resize " + this.Name + " " + this.Bounds);
            if (ScrollBar != null)
            {
                SetScrollBarLocationSize();
                ScrollTo(scrollpos, true);
            }

        }

        private void SetScrollBarLocationSize()
        {
            Rectangle area = ClientRectangle;
            if (ScrollBar != null && ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
            {
                Point p = new Point(area.X + ((VerticalScrollBarDockRight) ? (area.Width - ScrollBarWidth) : 0), area.Y);
                ScrollBar.Location = p;
                ScrollBar.Size = new Size(ScrollBarWidth, area.Height);
            }
        }

        private void Control_SizeChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("CS size Change " + ((Control)sender).Name + " " + ((Control)sender).Bounds);
            ScrollTo(scrollpos, true);
        }

        private void Control_VisibleChanged(object sender, EventArgs e)
        {
            // Control c = sender as Control; System.Diagnostics.Debug.WriteLine("CS Visible Change " + c.Name + " " + c.Bounds + " " + c.Visible);
            ScrollTo(scrollpos, true);
        }

        int ignorelocationchange = 0;      // location changes triggered when we reposition controls to scroll, so we need to mask them 

        private void Control_LocationChanged(object sender, EventArgs e)
        {
            if (ignorelocationchange == 0)
            {
               // System.Diagnostics.Debug.WriteLine("CS loc Change " + ((Control)sender).Name + " " + ((Control)sender).Bounds);
                ignorelocationchange++;        // stop recursion
                Control c = sender as Control;
                c.Top = c.Top - scrollpos;      // account for scroll pos and move control to scroll pos offset
                ScrollTo(scrollpos, true);    // check bar within bounds
                ignorelocationchange--;
                //System.Diagnostics.Debug.WriteLine("PS Move " + c.Name + " " + c.Location);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (ScrollBar != null)
                SetScrollBarLocationSize();

            base.OnLayout(levent);
        }

        private void Control_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ScrollBar != null)
            {
                //System.Diagnostics.Debug.WriteLine("Mousewheel" + Environment.TickCount);
                if (e.Delta > 0)
                    ScrollBar.ValueLimited -= ScrollBar.LargeChange;
                else
                    ScrollBar.ValueLimited += ScrollBar.LargeChange;

                //System.Diagnostics.Debug.WriteLine("CS Mouse Wheel ");
                ScrollTo(ScrollBar.Value, false);
            }
        }

        protected virtual void OnScrollBarChanged(object sender, ScrollEventArgs e)
        {
           // System.Diagnostics.Debug.WriteLine("CS Scroll bar ");
            ScrollTo(e.NewValue, false);
        }

        public void ToEnd()
        {
            ScrollTo(99999999, false);
        }

        // set forcereposition if the vscroll pos does not change but something like a control resize may have messed about with the positioning
        private void ScrollTo(int newscrollpos, bool forcereposition)
        {
            //System.Diagnostics.Debug.WriteLine("  Scroll panel " + Name + " is " + ClientRectangle + " curscrollpos " + scrollpos + " " + Controls.Count);
            //System.Diagnostics.Debug.WriteLine("From " + Environment.StackTrace.StackTrace("ScrollTo",5));

              int maxy =0;
            List<Point> cposnorm = new List<Point>();

            Point flowpos = new Point(0, 0);            // for flow
            int dwidth = DisplayRectangle.Width;
            int rowymax = 0;

            foreach (Control c in Controls)
            {
                if (!(c is ExtScrollBar) && c.Visible)      // if in a tab, when tab is turned on, we get a call but will all things invisible (it comes thru resize)
                {                                           // therefore setting scroll pos to zero. This stage double checks we do not have controls
                    if ( -c.Top > scrollpos )               // below zero meaning we have lost scroll pos. If so, set it back
                    {
                        //System.Diagnostics.Debug.WriteLine("   Before Control " + c.Text + " " + c.Size + " " + c.Location + " " + c.Visible + " before " + scrollpos + " set to " + -c.Top);
                        scrollpos = -c.Top;
                        newscrollpos = scrollpos;
                    }
                }
            }

            foreach (Control c in Controls)
            {
                if (!(c is ExtScrollBar) && c.Visible)      
                {
                    if (FlowControlsLeftToRight)
                    {
                        if (flowpos.X + c.Width + c.Margin.Horizontal >= dwidth && flowpos.X > 0)
                        {
                            flowpos = new Point(0, flowpos.Y + rowymax);
                            rowymax = 0;
                        }

                        cposnorm.Add(new Point(flowpos.X + c.Margin.Left, flowpos.Y + c.Margin.Top));
                        flowpos.X += c.Width + c.Margin.Horizontal;
                        rowymax = Math.Max(rowymax, c.Height + c.Margin.Vertical);
                        maxy = flowpos.Y + c.Height - 1 + c.Margin.Vertical;
                    }
                    else
                    {
                        cposnorm.Add(new Point(c.Left, c.Top + scrollpos));
                        maxy = Math.Max(maxy, c.Top + scrollpos + c.Height -1);     // -1 because top+height = 1 pixel beyond last displayed
                    }

                }

                //System.Diagnostics.Debug.WriteLine("   Control " + c.Name + " " + c.Size + " " + c.Location + " " + c.Visible + " maxy " + maxy);
            }

            int maxscr = maxy - ClientRectangle.Height + (ScrollBar?.LargeChange ?? 0);       // large change is needed due to the way the scroll bar works (which matches the windows scroll bar)

            if (maxy <= ClientRectangle.Height)          // limit..
            {
                //System.Diagnostics.Debug.WriteLine("   Maxy <= Client Height, set to zero");
                newscrollpos = 0;
            }
            else if (newscrollpos > maxscr)
                newscrollpos = maxscr;

            //System.Diagnostics.Debug.WriteLine("   Maxy " + maxy + " CH "+ ClientRectangle.Height + "  maxscr " + maxscr + " new scr " + newscrollpos + " old scroll " + scrollpos);

            if (newscrollpos != scrollpos || (FlowControlsLeftToRight && forcereposition))  // only need forcereposition on flowing
            {
                SuspendLayout();
                ignorelocationchange++;
                int posi = 0;
                foreach (Control c in Controls)
                {
                    if (!(c is ExtScrollBar) && c.Visible)      // new! take into account visibility
                    {
                        c.Location = new Point(cposnorm[posi].X, cposnorm[posi].Y - newscrollpos);
                        posi++;
                    }
                }

                ignorelocationchange--;
                ResumeLayout();
                Update(); // force redisplay
            }

            if (ScrollBar != null)
            {
                ScrollBar.SetValueMaximumMinimum(newscrollpos, maxscr, 0);
                // System.Diagnostics.Debug.WriteLine("Scroll {0} to {1} maxscr {2} maxy {3} ClientH {4}", scrollpos, newscrollpos, maxscr , maxy , ClientRectangle.Height);
            }

            scrollpos = newscrollpos;
        }

        public void RemoveAllControls(List<Control> excluded = null)
        {
            SuspendLayout();
            List<Control> listtoremove = (from Control s in Controls where (!(s is ExtScrollBar) && (excluded == null || !excluded.Contains(s))) select s).ToList();
            foreach (Control c in listtoremove)
            {
                Debug.Assert(!(c is ExtScrollBar));

                c.Hide();
                c.Dispose();
                Controls.Remove(c);
            }

            scrollpos = 0;
            ResumeLayout();
        }

    }
}
