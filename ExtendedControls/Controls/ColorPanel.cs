/*
 * Copyright 2022-2025 EDDiscovery development team
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
    // this is a coloured area, either solid (BackColor) or gradient filled
    public class ColorPanel : Control, IThemeable
    {
        public Color[] ThemeColors { get; set; } = new Color[4] { SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control };
        public float GradientDirection { get; set; } = 0;
        public override Color BackColor { get => ThemeColors[0]; set { ThemeColors[0] = ThemeColors[1] = ThemeColors[2] = ThemeColors[3] = value; Invalidate(); } }
        public ColorPanel()
        {
            BackColor = Color.Red;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"PanelGradient {Name} OnPaintBackground {ClientRectangle} tc {PaintTransparentColor} {ThemeColorSet}");
            e.Graphics.DrawMultiColouredRectangles(ClientRectangle, ThemeColors, GradientDirection);
        }

        // no themeing, no children
        public bool Theme(Theme t, Font fnt)        
        {
            return false;
        }
    }
}
