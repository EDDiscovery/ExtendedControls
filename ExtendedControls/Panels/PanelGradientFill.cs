﻿/*
 * Copyright 2016-2025 EDDiscovery development team
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
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ExtendedControls
{
    // Extended panel with gradient fill, child control, Themeing from Panel set, LayoutComplete call back
    // base class of:
    // -> ExtPanelAutoHeightWidth
    // -> ExtPanelDropDown
    // -> ExtPanelNoChildThemed
    // -> ExtPanelResizer
  

    public class ExtPanelGradientFill : Panel, IThemeable
    {
        public bool ChildrenThemed { get; set; } = true;        // Control if children is themed

        // ThemeColors only used if ThemeColourSet>=0.  
        public Color[] ThemeColors { get; set; } = new Color[4] { SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control };
        // -1 = system, 0 use ThemeColours but don't set when calling Theme(), else 1,2,3,4.. one of the Panel colour sets
        public int ThemeColorSet { get; set; } = -1;
        public float GradientDirection { get; set; } = 0;

        // if you set this to a colour, the background becomes that colour, for transparency purposes.
        // If you set it to Color.Transparent, it goes to normal
        public Color PaintTransparentColor { get { return transparency; } set { transparency = value; Invalidate(); } }

        // Set to lefttoright for a basic flow
        public FlowDirection? FlowDirection { get { return flowdirection; } set { flowdirection = value; } }

        // Override if you want your derived class to have a go at themeing.
        protected virtual bool ThemeDerived(Theme t, Font fnt) 
        { return ChildrenThemed; }

        public Action<ExtPanelGradientFill> LayoutComplete;         // callback useful to know when layout is complete - there is no winform one doing this, layout callback is called before layout.

        public ExtPanelGradientFill()
        {
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"PanelGradient {Name} OnPaintBackground {ClientRectangle} tc {PaintTransparentColor} {ThemeColorSet}");

            if (PaintTransparentColor != Color.Transparent)
                e.Graphics.DrawFilledRectangle(ClientRectangle,PaintTransparentColor);
            else if (ThemeColorSet < 0)
                base.OnPaintBackground(e);
            else
            {
                e.Graphics.DrawMultiColouredRectangles(ClientRectangle, ThemeColors, GradientDirection);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (ClientRectangle.Width > 0)
            {
                if (flowdirection == System.Windows.Forms.FlowDirection.LeftToRight)
                {
                    int xl = 0;
                    int xr = ClientRectangle.Width - 1;
                    foreach (Control ctrl in Controls)
                    {
                        if (ctrl.Visible)
                        {
                            if ((ctrl.Anchor & AnchorStyles.Left) != 0)
                            {
                                xl += ctrl.Margin.Left;
                                //System.Diagnostics.Debug.WriteLine($"GradientFill flow {ctrl.Name} left to {xl}");
                                ctrl.Location = new Point(xl, ctrl.Top);
                                xl += ctrl.Width + ctrl.Margin.Right;
                            }
                            else if ((ctrl.Anchor & AnchorStyles.Right) != 0)
                            {
                                xr -= ctrl.Margin.Right + ctrl.Width;
                                //System.Diagnostics.Debug.WriteLine($"GradientFill flow {ctrl.Name} right to {xr}");
                                ctrl.Location = new Point(xr, ctrl.Top);
                                xr -= ctrl.Margin.Left;
                            }
                        }
                    }
                }

            }

            base.OnLayout(levent);

            LayoutComplete?.Invoke(this);
        }

        public bool Theme(Theme t, Font fnt)
        {
            if (ThemeColorSet > 0)
            {
                ThemeColors = t.GetPanelSet(ThemeColorSet);
                GradientDirection = t.GetPanelDirection(ThemeColorSet);
            }

            // interfaces in this version of c# can't be virtual, but we want to give the derived class a go if required
            return ThemeDerived(t,fnt);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            Invalidate();       // need this to cause a repaint
            base.OnResize(eventargs);
        }

        private Color transparency = Color.Transparent;
        private FlowDirection? flowdirection = null;
    }

    // as per GradientFill, without child theming
    public class ExtPanelNoChildThemed : ExtPanelGradientFill
    {
        public ExtPanelNoChildThemed() : base()
        {
            ChildrenThemed = false;
        }
    }


}
