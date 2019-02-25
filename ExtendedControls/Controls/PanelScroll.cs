/*
 * Copyright © 2016-2019 EDDiscovery development team
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
        public int ScrollBarWidth { get; set; } = 20;
        public bool VerticalScrollBarDockRight { get; set; } = true;        // true for dock right
        public ExtScrollBar ScrollBar;
        public int ScrollOffset { get {return -scrollpos; } }

        private int scrollpos = 0;

        public ExtPanelScroll()
        {
            MouseWheel += Control_MouseWheel;         // grab the controls mouse wheel and direct to our scroll
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {  // as controls are added, remember them in local variables.

            //System.Diagnostics.Debug.WriteLine((Environment.TickCount % 10000).ToString("00000") + " VS start");

            if (e.Control is ExtScrollBar)
            {
                ScrollBar = e.Control as ExtScrollBar;
                ScrollBar.Width = ScrollBarWidth;
                ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(OnScrollBarChanged);
                ScrollBar.Name = "VScrollPanel";
                ScrollTo(0);
            }

            e.Control.MouseWheel += Control_MouseWheel;         // grab the controls mouse wheel and direct to our scroll
        }

        bool doneonescroll = false;

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            Rectangle area = ClientRectangle;

            if (ScrollBar != null && area.Width>0 && area.Height>0)
            {
                Point p = new Point(area.X + ((VerticalScrollBarDockRight) ? (area.Width - ScrollBarWidth) : 0), area.Y);
                ScrollBar.Location = p;
                ScrollBar.Size = new Size(ScrollBarWidth, area.Height);

                if ( !doneonescroll )
                {
                    doneonescroll = true;
                    ScrollTo(scrollpos);
                }
            }

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

                ScrollTo(ScrollBar.Value);
            }
        }

        protected virtual void OnScrollBarChanged(object sender, ScrollEventArgs e)
        {
            ScrollTo(e.NewValue);
        }


        public void RestateScroll()             // call this if you've messed about with the position of controls..
        {
            foreach (Control c in Controls)
            {
                if (!(c is ExtScrollBar))
                {
                    c.Location = new Point(c.Left, c.Top - scrollpos); 
                }
            }
        }

        public void ToEnd()
        {
            ScrollTo(99999999);
        }

        public void ToCurrent()
        {
            ScrollTo(scrollpos);
        }

        private int ScrollTo(int newscrollpos )
        {
            System.Diagnostics.Debug.WriteLine("Scroll panel is " + ClientRectangle + " curscrollpos " + scrollpos);
            System.Diagnostics.Debug.WriteLine("From " + Environment.StackTrace.StackTrace("ScrollTo",5));
            int maxy = 0;
            foreach (Control c in Controls)
            {
                if (!(c is ExtScrollBar) && c.Visible)
                {
                    int ynoscroll = c.Location.Y + scrollpos;
                    maxy = Math.Max(maxy, ynoscroll + c.Height + 4);
                    System.Diagnostics.Debug.WriteLine("Control " + c.Size + " " + c.Location + " maxy " + maxy);
                }
            }

            //                int maxscr = maxy - ClientRectangle.Height + ((ScrollBar != null) ? ScrollBar.LargeChange : 0);
            int maxscr = maxy - ClientRectangle.Height;

            System.Diagnostics.Debug.WriteLine("Maxy " + maxy + " maxscr " + maxscr);

            if (maxy <= ClientRectangle.Height)          // see if need scroll..
                newscrollpos = 0;
            else if (newscrollpos > maxscr)
                newscrollpos = maxscr;

            if (newscrollpos != scrollpos)
            {
                SuspendLayout();
                foreach (Control c in Controls)
                {
                    if (!(c is ExtScrollBar))
                    {
                        // System.Diagnostics.Debug.WriteLine("Move {0}", c.Name);

                        int ynoscroll = c.Location.Y + scrollpos;
                        int ynewscroll = ynoscroll - newscrollpos;
                        c.Location = new Point(c.Location.X, ynewscroll);       // SPENT AGES with the bloody AutoScrollPosition.. could not get it to work..
                    }
                }

                ResumeLayout();
                Update(); // force redisplay
            }

            if (ScrollBar != null)
            {
                System.Diagnostics.Debug.WriteLine("Set scroll " + newscrollpos + " m " + maxscr + " lc" + ScrollBar.LargeChange);
                ScrollBar.SetValueMaximumMinimum(newscrollpos, maxscr, 0);
                //System.Diagnostics.Debug.WriteLine("Scroll {0} to {1} maxy {2} sb {3} ch {4}", scrollpos, newscrollpos, maxy, vsc.Maximum, ClientRectangle.Height);
            }

            scrollpos = newscrollpos;

            return maxy;
        }

        public void RemoveAllControls( List<Control> excluded = null)
        {
            SuspendLayout();
            List<Control> listtoremove = (from Control s in Controls where (!(s is ExtScrollBar) && (excluded==null || !excluded.Contains(s))) select s).ToList();
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
