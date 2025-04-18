/*
 * Copyright 2023-2023 EDDiscovery development team
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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtFlowLayoutPanel : FlowLayoutPanel, IThemeable
    {
        public bool ChildrenThemed { get; set; } = true;        // Control if children is themed

        // ThemeColors only used if ThemeColourSet>=0.  
        public Color[] ThemeColors { get; set; } = new Color[4] { SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control };
        // -1 = system, 0 use ThemeColours but don't set when calling theme(), else 1,2,3,4.. one of the Panel colour sets
        public int ThemeColorSet { get; set; } = -1;
        public float GradientDirection { get; set; }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (ThemeColorSet < 0)
                base.OnPaintBackground(e);
            else
                DrawingHelpersStaticFunc.PaintMultiColouredRectangles(e.Graphics, ClientRectangle, ThemeColors, GradientDirection);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            Invalidate();       // need this to cause a repaint
            base.OnResize(eventargs);
        }

        public bool Theme(Theme t, Font fnt)
        {
            if (ThemeColorSet > 0)
            {
                ThemeColors = t.GetPanelSet(ThemeColorSet);
                GradientDirection = t.GetPanelDirection(ThemeColorSet);
            }
            
            return ChildrenThemed;
        }
    }

    /// <summary>
    /// This automatically sizes the parent container to the flow container.
    /// set the flow container to autosize and dock top.  The parent container will be set to the size needed by this
    /// supports only one flow layout panel in a container such as a group box or a panel
    /// Margin.Bottom allows for extra space under if required
    /// </summary>
    public class ExtFlowLayoutPanelParentResize : ExtFlowLayoutPanel
    {
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            if (Dock == DockStyle.Top)
            {
                //System.Diagnostics.Debug.WriteLine($"Parent {Parent.Name} {Parent.Bounds} cr {Parent.ClientRectangle} {Parent.ClientSize}");
                //System.Diagnostics.Debug.WriteLine($"this {Bounds} cr {ClientRectangle} {ClientSize}");

                // controls under group boxes seem to have client areas starting above 0. Add it on
                Parent.ClientSize = new Size(Parent.Width, Bounds.Y + Bounds.Height + Margin.Bottom);
            }
        }
    }
}
