/*
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

using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    // Extended panel with gradient fill and child control

    public class ExtPanelGradientFill : Panel, IThemeable
    {
        public bool ChildrenThemed { get; set; } = true;        // Control if children is themed

        // ThemeColors only used if ThemeColourSet>=0.  
        public Color[] ThemeColors { get; set; } = new Color[4] { SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control };
        // -1 = system, 0 use ThemeColours but don't set when calling theme(), else 1,2,3,4.. one of the Panel colour sets
        public int ThemeColorSet { get; set; } = -1;      
        public float GradientDirection { get; set; }

        protected virtual bool ThemeDerived(Theme t, Font fnt) { return ChildrenThemed; }       // default implementation if derived does not want to do more theming

        public ExtPanelGradientFill()
        {
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (ThemeColorSet < 0)
                base.OnPaintBackground(e);
            else
                DrawingHelpersStaticFunc.PaintMultiColouredRectangles(e.Graphics, ClientRectangle, ThemeColors, GradientDirection);
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
    }

    // as per GradientFill, without child theming
    public class ExtPanelChildThemeControl : ExtPanelGradientFill
    {
        public ExtPanelChildThemeControl() : base()
        {
            ChildrenThemed = false;
        }
    }

    // a resizer panel, clicking on it causes a resize.

    public partial class ExtPanelResizer : ExtPanelGradientFill
    {
        public DockStyle Movement { get { return movement; } set { SetMovement(value); } }
        private DockStyle movement = DockStyle.Top;

        private void SetMovement(DockStyle m)
        {
            if (Movement == DockStyle.Top || Movement == DockStyle.Bottom)
                Cursor = Cursors.SizeNS;
            else
                Cursor = Cursors.SizeWE;

            movement = m;
        }

        public ExtPanelResizer()
        {
            SetMovement(DockStyle.Top);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Form f = FindForm();
            if (f != null && f is DraggableForm)
            {
                ((DraggableForm)f).PerformResizeMouseDown(this, e, Movement);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Form f = FindForm();
            if (f != null && f is DraggableForm)
            {
                ((DraggableForm)f).PerformResizeMouseUp(this, e, Movement);
            }
        }

        protected override bool ThemeDerived(Theme t, Font fnt)
        {
            Visible = !t.WindowsFrame;
            return base.ThemeDerived(t, fnt);
        }
    }

}
