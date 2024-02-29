/*
 * Copyright © 2024-2024 EDDiscovery development team
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

    public partial class ExtPanelVertScroll : Panel
    {
        public Action<int, int> ScrollSet;

        public int Value { get { return currentscroll; } set { ScrollTo(value); } }

        public int MaxScroll { get { return maxscroll; } }

        public ExtPanelVertScroll()
        {
        }

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
                Update();
                if ( callback )
                    ScrollSet?.Invoke(currentscroll, maxscroll);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            CalculateScrollArea();
        }

        private void CalculateScrollArea()
        {
            int miny = int.MaxValue;
            int maxy = int.MinValue;
            foreach (Control c in Controls)
            {
                if (c.Top < miny)
                    miny = c.Top;
                if (c.Bottom > maxy)
                    maxy = c.Bottom;
            }

            maxscroll= miny<int.MaxValue ? maxy-miny- ClientRectangle.Height : 0;

            if (currentscroll > maxscroll)      // if now out of range, scroll to max range
                ScrollTo(maxscroll);

         //   System.Diagnostics.Debug.WriteLine($"Scroll 0-{maxscroll} current {currentscroll} height {ClientRectangle.Height} miny {miny} maxy {maxy}");
            ScrollSet?.Invoke(currentscroll, maxscroll);
        }

        private int currentscroll;
        private int maxscroll;
    }

    /// Panel which accepts a ExtPanelVertScroll as child, and has a scroll bar on the right. 
    /// implements the scroll bar controllin the vert scroll, and mouse wheel

    public partial class ExtPanelVertScrollWithBar : Panel
    {
        public int ScrollBarWidth { get { return Font.ScalePixels(24); } }

        public ExtPanelVertScrollWithBar()
        {
            SuspendLayout();
            scrollbar = new ExtScrollBar();
            scrollbar.Name = "VScrollPanel";
            scrollbar.Scroll += new ScrollEventHandler(OnScrollBarChanged);
            scrollbar.Dock = DockStyle.Right;
            scrollbar.Width = ScrollBarWidth;
            Controls.Add(scrollbar);
            ResumeLayout();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if ( e.Control is ExtPanelVertScroll)
            {
                panel = e.Control as ExtPanelVertScroll;
                panel.Dock = DockStyle.Fill;
                panel.ScrollSet += (value, max) => { scrollbar.SetValueMaximumMinimum(value, max + scrollbar.LargeChange, 0); };
                panel.MouseWheel += (o, mw) => { panel.Value += mw.Delta > 0 ? -scrollbar.LargeChange : scrollbar.LargeChange; };
                Controls.SetChildIndex(panel, 0); // to make dock work etc vs scroll bar, make it so that its first
            }
        }

        protected virtual void OnScrollBarChanged(object sender, ScrollEventArgs e)
        {
            //  System.Diagnostics.Debug.WriteLine("On Scroll Bar Changed " + e.NewValue);
            panel?.ScrollTo(e.NewValue,false);
        }

        private ExtPanelVertScroll panel;
        private ExtScrollBar scrollbar;
    }

}
