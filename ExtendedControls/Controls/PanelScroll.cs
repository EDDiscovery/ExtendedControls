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
        public bool InternalScrollbar { get; set; } = false;        // indicates internal scroll bar. put a scroll bar inside the control on the designer to do this

        public bool VerticalScrollBarDockRight { get; set; } = true;        // true for dock right

        public int ScrollBarWidth { get { return Font.ScalePixels(20); } }

        public ExtScrollBar ScrollBar;

        public int ScrollOffset { get {return -scrollpos; } }

        private int scrollpos = 0;

        public ExtPanelScroll()
        {
            MouseWheel += Control_MouseWheel;         // grab the controls mouse wheel and direct to our scroll
        }

        public void AddScrollBar(ExtScrollBar bar)      // for use of an external scroll bar
        {
            ScrollBar = bar;
            ScrollBar.MouseWheel += Control_MouseWheel;         // grab the controls mouse wheel and direct to our scroll, including the ExtScrollBar
            ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(OnScrollBarChanged);
            InternalScrollbar = false;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {  
            if (e.Control is ExtScrollBar)
            {
                ScrollBar = e.Control as ExtScrollBar;
                ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(OnScrollBarChanged);
                ScrollBar.Name = "VScrollPanel";
                InternalScrollbar = true;
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
            if (ScrollBar != null )
            {
                SetSC();
                ScrollTo(scrollpos);
            }

            base.OnResize(eventargs);       // call base class to let any hooks run
        }

        private void SetSC()
        {
            Rectangle area = ClientRectangle;
            if (InternalScrollbar && ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
            {
                Point p = new Point(area.X + ((VerticalScrollBarDockRight) ? (area.Width - ScrollBarWidth) : 0), area.Y);
                ScrollBar.Location = p;
                ScrollBar.Size = new Size(ScrollBarWidth, area.Height);
            }
        }

        private void Control_SizeChanged(object sender, EventArgs e)
        {
            ScrollTo(scrollpos);
        }

        private void Control_VisibleChanged(object sender, EventArgs e)
        {
            ScrollTo(scrollpos);
        }

        bool ignorelocationchange = false;      // location changes triggered when we reposition controls to scroll, so we need to mask them 

        private void Control_LocationChanged(object sender, EventArgs e)
        {
            if ( !ignorelocationchange )
                ScrollTo(scrollpos);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if ( ScrollBar != null )
                SetSC();

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

                ScrollTo(ScrollBar.Value);
            }
        }

        protected virtual void OnScrollBarChanged(object sender, ScrollEventArgs e)
        {
            ScrollTo(e.NewValue);
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
            //System.Diagnostics.Debug.WriteLine("Scroll panel is " + ClientRectangle + " curscrollpos " + scrollpos);
            //System.Diagnostics.Debug.WriteLine("From " + Environment.StackTrace.StackTrace("ScrollTo",5));
            int maxy = 0;
            foreach (Control c in Controls)
            {
                if (!(c is ExtScrollBar) && c.Visible)
                {
                    int ynoscroll = c.Location.Y + scrollpos;
                    maxy = Math.Max(maxy, ynoscroll + c.Height);
                    //       System.Diagnostics.Debug.WriteLine("Control " + c.Size + " " + c.Location + " maxy " + maxy);
                }
            }

            int maxscr = maxy - ClientRectangle.Height + (ScrollBar?.LargeChange??0);       // large change is needed due to the way the scroll bar works (which matches the windows scroll bar)

            if (maxy <= ClientRectangle.Height)          // limit..
                newscrollpos = 0;
            else if (newscrollpos > maxscr)
                newscrollpos = maxscr;

            //System.Diagnostics.Debug.WriteLine("Maxy " + maxy + " maxscr " + maxscr  + " new scr " + newscrollpos + " old scroll " + scrollpos);

            if (newscrollpos != scrollpos)
            {
                SuspendLayout();
                ignorelocationchange = true;
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

                ignorelocationchange = false;
                ResumeLayout();
                Update(); // force redisplay
            }

            if (ScrollBar != null)
            {
                ScrollBar.SetValueMaximumMinimum(newscrollpos, maxscr, 0);
               // System.Diagnostics.Debug.WriteLine("Scroll {0} to {1} maxscr {2} maxy {3} ClientH {4}", scrollpos, newscrollpos, maxscr , maxy , ClientRectangle.Height);
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
