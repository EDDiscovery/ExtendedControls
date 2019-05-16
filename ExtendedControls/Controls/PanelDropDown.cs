/*
 * Copyright © 2016 - 2019 EDDiscovery development team
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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPanelDropDown : Panel
    {
        public event EventHandler SelectedIndexChanged;

        public Color SelectionMarkColor { get; set; } = Color.Yellow;
        public int SelectionSize { get; set; } = 8;

        public List<string> Items { get { return ddc.Items; } set { ddc.Items = value; } }

        public override Color ForeColor { get { return base.ForeColor; } set { ddc.ForeColor = base.ForeColor = value; } }
        public bool FitToItemsHeight { get { return ddc.FitToItemsHeight; } set { ddc.FitToItemsHeight = value; } }

        public Color BorderColor { get { return ddc.BorderColor; } set { ddc.BorderColor = value; } } 
        public FlatStyle FlatStyle { get { return ddc.FlatStyle; } set { ddc.FlatStyle = value; } }
        public Color SelectionBackColor { get { return ddc.SelectionBackColor; } set { ddc.SelectionBackColor = value; } }
        public float GradientColorScaling { get { return ddc.GradientColorScaling; } set { ddc.GradientColorScaling = value; } }
        public Color ScrollBarColor { get { return ddc.ScrollBarColor; } set { ddc.ScrollBarColor = value; } }
        public Color ScrollBarButtonColor { get { return ddc.ScrollBarButtonColor; } set { ddc.ScrollBarButtonColor = value; } }
        public Color MouseOverBackgroundColor { get { return ddc.MouseOverBackgroundColor; } set { ddc.MouseOverBackgroundColor = value; } }

        public int SelectedIndex { get { return ddc.SelectedIndex; } set { ddc.SelectedIndex = value; } }
        public string SelectedItem { get { return (ddc.SelectedIndex>=0) ? ddc.Items[ddc.SelectedIndex] : null; }  }

        private ExtListBoxForm ddc;

        public ExtPanelDropDown()
        {
            InitializeComponent();
            ddc = new ExtListBoxForm("PSDD",false);
            ddc.SelectedIndexChanged += Ddc_SelectedIndexChanged;
            ddc.Deactivate += Ddc_Deactivate;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            
            if (BorderColor != Color.Transparent)
            {
                rect.Height--;
                rect.Width--;
                using (Pen p1 = new Pen(BorderColor, 1.0F))
                    e.Graphics.DrawRectangle(p1, rect);
            }

            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

            using (SolidBrush b1 = new SolidBrush(SelectionMarkColor))
            {
                int x = rect.Right;
                int y = rect.Bottom;

                Point[] tri = new Point[] { new Point(x - SelectionSize, y), new Point(x, y - SelectionSize), new Point(x, y) };

                e.Graphics.FillPolygon(b1, tri);
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            ddc.Font = Font;
        }

        bool dropdown = false;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if ( e.X>=Width-SelectionSize && e.Y>=Height-SelectionSize)
            {
                if (dropdown == false)
                {
                    Point location = this.PointToScreen(new Point(0, 0));
                    ddc.SetLocation = new Point(location.X + this.Width, location.Y + this.Height);   // -n means align right to this loc
                    ddc.RightAlignedToLocation = true;
                    System.Diagnostics.Debug.WriteLine("dcc border " + ddc.BorderColor);
                    ddc.Show(FindForm());
                }
                else
                {
                    ddc.Hide();
                }

                dropdown = !dropdown;
            }
        }

        private void Ddc_Deactivate(object sender, EventArgs e)
        {
            dropdown = false;
            ddc.Hide();
        }

        private void Ddc_SelectedIndexChanged(object sender, EventArgs e)
        {
            dropdown = false;
            ddc.Hide();
            SelectedIndexChanged?.Invoke(this, e);
        }
    }
}
