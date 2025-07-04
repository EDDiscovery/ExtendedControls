/*
 * Copyright 2024-2025 EDDiscovery development team
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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ExtendedControls
{
    // a Panel with vertical scroll capabilites. Manually controlled via Value or ScrollTo
    // on a control add/remove, call BeginPosition/FinishedPosition
    // on a resize, you need to call Recalculate again manually

    public partial class ExtPanelVertScroll : Panel
    {
        public Action<int, int> ScrollSet { get; set; }      // called when scroll is set each time
        public int Value { get { return currentscroll; } set { ScrollTo(value); } } // current scroll
        public int MaxScroll { get { return maxscroll; } }  // max scroll, controlled by Recalcuate().

        public ExtPanelVertScroll()
        {
        }

        // call when your going to reset the positions of all the controls and want to go back to zero
        // or after a resize if the window has reset the control positions (see ConfigurableUC)
        // returns current scroll pos 
        public int BeingPosition()
        {
            int cur = currentscroll;
            currentscroll = 0;
            SuspendLayout();
            return cur;
        }

        // call when your controls are in position after BeginPosition()
        public void FinishedPosition(int scrollto)
        {
            System.Diagnostics.Debug.Assert(currentscroll == 0);        // must have reset

            Recalcuate();

            ResumeLayout();

            if (scrollto >= 0)
                ScrollTo(scrollto);
            else
                ToEnd();

        }

        // call if you have resized your window
        public void Recalcuate()
        {
            // find area of controls
            //System.Diagnostics.Debug.WriteLine($"PVS Recalc cur scroll {currentscroll}");

            int maxy = int.MinValue;
            foreach (Control c in Controls)
            {
                int bot = c.Bottom + currentscroll;
                if (bot > maxy)        // we add on currentscroll to compensate if we have been scrolling, as the actual control y will have been changed
                    maxy = bot;
                //System.Diagnostics.Debug.WriteLine($"   PVS Recalc {c.Name} {c.Bounds} {c.Bottom} {bot} {maxy}");
            }

            // here we find the maximum scroll, which is the difference between the maximum controls and the client height
            maxscroll = maxy > int.MinValue ? Math.Max(maxy - ClientRectangle.Height , 0) : 0;

            // this is the bottom of our view from currentscroll down height
            int botcurrentview = currentscroll + ClientRectangle.Height;

           // System.Diagnostics.Debug.WriteLine($"      Scroll cur {currentscroll} scrollarea {maxscroll} scr height {ClientRectangle.Height} maxy {maxy} view {currentscroll}..{botcurrentview}");

            // if we have currentscroll non zero , but we are viewing below the max, we can scroll up
            if (botcurrentview > maxy && currentscroll>0)     
            {
                int newscroll = Math.Max(0,currentscroll+ (botcurrentview - maxy));     // change scroll position
                //System.Diagnostics.Debug.WriteLine($"  view too big, scroll to {newscroll}");
                ScrollTo(newscroll);
            }
            else
                ScrollSet?.Invoke(currentscroll, maxscroll);        // not below maxy, just inform scroll change
        }

        // call for manual go to end
        public void ToEnd()
        {
            ScrollTo(maxscroll);
        }

        // call for manual go to scroll.  Normally we call back
        public void ScrollTo(int value, bool callback = true)
        {
            if (value < 0)
                value = 0;
            else if (value > maxscroll)
                value = maxscroll;

            if (value != currentscroll)
            {
                Rectangle cr = ClientRectangle;
                BaseUtils.Win32.RECT rcClip = BaseUtils.Win32.RECT.FromXYWH(cr.X, cr.Y, cr.Width, cr.Height);
                BaseUtils.Win32.RECT rcUpdate = BaseUtils.Win32.RECT.FromXYWH(cr.X, cr.Y, cr.Width, cr.Height);

                BaseUtils.Win32.SafeNativeMethods.ScrollWindowEx(new HandleRef(this, this.Handle), 0, currentscroll -value,
                                                 null,
                                                 ref rcClip,
                                                 BaseUtils.Win32.NativeMethods.NullHandleRef,
                                                 ref rcUpdate,
                                                 BaseUtils.Win32.NativeMethods.SW_INVALIDATE
                                                 | BaseUtils.Win32.NativeMethods.SW_ERASE
                                                 | BaseUtils.Win32.NativeMethods.SW_SCROLLCHILDREN);

                currentscroll = value;
                //System.Diagnostics.Debug.WriteLine($"Scrolled showing {currentscroll}..{currentscroll + ClientRectangle.Height} max {maxscroll}");
                Update();
                if ( callback )
                    ScrollSet?.Invoke(currentscroll, maxscroll);

                //foreach (Control c in Controls) System.Diagnostics.Debug.WriteLine($"   ScrollTo Pos {c.Name} {c.Bounds} {c.Bottom}");

            }
        }

        private int currentscroll;
        private int maxscroll;
    }

    /// Panel which accepts a ExtPanelVertScroll as child, and has a scroll bar on the right. 
    /// implements the scroll bar controlling the vert scroll, and mouse wheel

    public partial class ExtPanelVertScrollWithBar : Panel, IThemeable
    {
        public int ScrollBarWidth { get { return scrollbar.Width; } }
        public int ContentWidth { get { return Width - scrollbar.Width; } }
        public int LargeChange { get { return scrollbar.LargeChange; } set { scrollbar.LargeChange = value; } }
        public int SmallChange { get { return scrollbar.SmallChange; } set { scrollbar.SmallChange = value; } }
        public bool HideScrollBar { get { return scrollbar.HideScrollBar; } set { scrollbar.HideScrollBar = value; } }
        public int ScrollValue { get { return scrollbar.Value; } set { scrollbar.Value = value; } }

        public ExtPanelVertScrollWithBar()
        {
            SuspendLayout();
            scrollbar = new ExtScrollBar();
            scrollbar.Name = "VScrollPanel";
            scrollbar.Scroll += new ScrollEventHandler(OnScrollBarChanged);
            scrollbar.Dock = DockStyle.Right;
            scrollbar.Size = new Size(48, 48);     // width holds the scroll bar width, set by themeing
            Controls.Add(scrollbar);
            ResumeLayout();
        }

        public void Recalcuate()
        {
            panel?.Recalcuate();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if ( e.Control is ExtPanelVertScroll)
            {
                panel = e.Control as ExtPanelVertScroll;
                panel.Dock = DockStyle.Fill;
                panel.ScrollSet += (value, max) => {
                    scrollbar.SetValueMaximumMinimum(value, max > 0 ? max+scrollbar.LargeChange : 0, 0); 
                };
                panel.MouseWheel += (o, mw) => { 
                            panel.Value += mw.Delta > 0 ? -scrollbar.LargeChange : scrollbar.LargeChange; 
                };
                Controls.SetChildIndex(panel, 0); // to make dock work etc vs scroll bar, make it so that its first
            }
        }

        protected virtual void OnScrollBarChanged(object sender, ScrollEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("On Scroll Bar Changed " + e.NewValue);
            panel?.ScrollTo(e.NewValue,false);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            //scrollbar.Width = ScrollBarWidth;
        }

        public bool Theme(Theme t, Font fnt)
        {
            int newwidth = t.ScrollBarWidth();  // theme sets the scroll bar width
            if (newwidth != ScrollBarWidth)
            {
                scrollbar.Width = newwidth;
            }

            return true;
        }

        private ExtPanelVertScroll panel;
        private ExtScrollBar scrollbar;
    }

}
