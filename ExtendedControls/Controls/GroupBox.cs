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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtGroupBox : GroupBox, IThemeable
    {
        // ForeColor = text, BackColor = for system mode
        public bool ChildrenThemed { get; set; } = true;        // Control if children is themed

        // ThemeColors only used if ThemeColourSet>=0.  
        public Color[] ThemeColors { get; set; } = new Color[4] { SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control };
        // -1 = system, 0 use ThemeColours but don't set when calling theme(), else 1,2,3,4.. one of the Panel colour sets
        public int ThemeColorSet { get; set; } = -1;
        public float GradientDirection { get; set; }

        public Color BorderColor { get; set; } = Color.LightGray;       // border
        public Color BorderColor2 { get; set; } = Color.Gray;           // darker

        public int TextStartPosition { get; set; } = -1;                // -1 left, +1 right, 0 centre, else pixel start pos
        public int TextPadding { get; set; } = 0;                       // pixels at start/end of text

        public ExtGroupBox() : base()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (FlatStyle == FlatStyle.System || FlatStyle == FlatStyle.Standard)
            {
                base.OnPaint(e);
            }
            else if (DisplayRectangle.Width > 0 && DisplayRectangle.Height > 0) // Popup and Flat are ours, as long as its got size
            {
                int topline = DisplayRectangle.Y / 2;

                DrawingHelpersStaticFunc.DrawMultiColouredRectangles(e.Graphics, ClientRectangle, ThemeColors, GradientDirection);


                if (!BorderColor.IsFullyTransparent())
                {
                    int textlength = 0;
                    if (this.Text != "")
                    {           // +1 for rounding down..
                        textlength = (int)e.Graphics.MeasureString(this.Text, this.Font).Width + TextPadding * 2 + 1;
                    }

                    int textstart = TextStartPosition;
                    if (textstart == 0)                                          // auto centre
                        textstart = ClientRectangle.Width / 2 - textlength / 2;     // centre
                    else if (textstart == -1)                                          // left
                        textstart = 15;
                    else if (textstart == 1)                                          // right
                        textstart = ClientRectangle.Width - 15 - textlength;

                    if (textstart < 4)                                          // need 4 pixels 
                        textstart = 4;

                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                    using (GraphicsPath g1 = DrawingHelpersStaticFunc.RectCutCorners(1, topline + 1, ClientRectangle.Width - 2, ClientRectangle.Height - topline - 1, 1, 1, textstart - 1, textlength))
                    using (Pen pc1 = new Pen(BorderColor, 1.0F))
                        e.Graphics.DrawPath(pc1, g1);

                    using (GraphicsPath g2 = DrawingHelpersStaticFunc.RectCutCorners(0, topline, ClientRectangle.Width, ClientRectangle.Height - topline - 1, 2, 2, textstart, textlength))
                    using (Pen pc2 = new Pen(BorderColor2, 1.0F))
                        e.Graphics.DrawPath(pc2, g2);

                    if (textlength > 0)
                    {
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        int twidth = ClientRectangle.Width - textstart - 4;            // What size have we left..
                        twidth = (textlength < twidth) ? textlength : twidth;              // clip
                        Rectangle textarea = new Rectangle(textstart, 0, twidth, DisplayRectangle.Y);

                        using (Brush textb = new SolidBrush(this.ForeColor))
                        using (StringFormat fmt = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                        {
                            e.Graphics.DrawString(this.Text, this.Font, textb, textarea, fmt);
                        }

                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                    }
                }
            }
        }

        public bool Theme(Theme t, Font fnt)
        {
            ForeColor = t.GroupFore;
            BackColor = t.GroupBack[0]; // for system mode

            if (ThemeColorSet > 0)
            {
                ThemeColors = t.GetPanelSet(ThemeColorSet);
                GradientDirection = t.GetPanelDirection(ThemeColorSet);
            }
            else
            {
                GradientDirection = t.GroupBoxGradientDirection;
                ThemeColors = t.GroupBack;
            }

            BorderColor = t.GroupBorder;
            BorderColor2 = t.GroupBorder2;
            FlatStyle = FlatStyle.Popup;
            GradientDirection = t.GroupBoxGradientDirection;
            return true;
        }

    }
}
