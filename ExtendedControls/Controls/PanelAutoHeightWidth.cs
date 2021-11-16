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
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    // when a flow panel is inside the other panel, the autosize on the outer panel does not work - winform bug
    // this one autoheight fixes it if required
    public class ExtPanelAutoHeightWidth : Panel
    {
        public bool AutoHeight { get { return autoHeight; } set { autoHeight = value; } }
        public bool AutoWidth { get { return autoWidth; } set { autoWidth = value; } }
        public bool AutoHeightWidthDisable { get { return autoDisable; } set { autoDisable = value; } }

        private bool autoHeight = false;
        private bool autoWidth = false;
        private bool autoDisable = false;
        public ExtPanelAutoHeightWidth() : base() { }

        private bool ignoreresize = false;

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            AutoSizePanel();
        }
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            AutoSizePanel();
        }

        private void AutoSizePanel()
        { 
            if (!AutoHeightWidthDisable && (AutoHeight||AutoWidth) && !ignoreresize)
            {
                Size s = this.FindMaxSubControlArea(Padding.Horizontal, Padding.Vertical, null, true);
                if ( this.Height != s.Height || this.Width != s.Width)
                {
                    System.Diagnostics.Debug.WriteLine($"AutoHeightWidth Panel {Name} adjust {s}");
                    ignoreresize = true;
                    if (AutoHeight && autoWidth)
                        this.Size = s;
                    else if (AutoWidth)
                        this.Width = s.Width;
                    else
                        this.Height = s.Height;
                    ignoreresize = false;
                }
            }
        }
    }
}
