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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPanelDropDown : ExtPanelGradientFill, IThemeable
    {
        public event EventHandler SelectedIndexChanged;

        public int SelectionSize { get; set; } = 8;

        public List<string> Items { get { return dropdown.Items; } set { dropdown.Items = value; } }
        public Color BorderColor { get; set; } = Color.Blue;
        public Color SelectionMarkColor { get; set; } = Color.Yellow;

        [System.ComponentModel.BrowsableAttribute(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DropDownTheme DropDownTheme { get; set; } = new DropDownTheme();

        // drop down box for selection
        public bool FitToItemsHeight { get { return dropdown.FitToItemsHeight; } set { dropdown.FitToItemsHeight = value; } }

        public int SelectedIndex { get { return dropdown.SelectedIndex; } set { dropdown.SelectedIndex = value; } }
        public string SelectedItem { get { return (dropdown.SelectedIndex>=0) ? dropdown.Items[dropdown.SelectedIndex] : null; }  }

        private ExtListBoxForm dropdown;

        public ExtPanelDropDown()
        {
            InitializeComponent();
            dropdown = new ExtListBoxForm("PSDD",false);
            dropdown.SelectedIndexChanged += Ddc_SelectedIndexChanged;
            dropdown.Deactivate += Ddc_Deactivate;
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
            dropdown.Font = Font;
        }

        bool dropdownactivated = false;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if ( e.X>=Width-SelectionSize && e.Y>=Height-SelectionSize)
            {
                if (dropdownactivated == false)
                {
                    DropDownTheme.Theme(dropdown.ListBox, ForeColor, BackColor, BorderColor);
                    DropDownTheme.Theme(dropdown.ListBox.ScrollBar, BorderColor, this.Font);

                    dropdown.PositionBelow(this,this.Width);
                    dropdown.RightAlignedToLocation = true;
                    System.Diagnostics.Debug.WriteLine("dcc border " + dropdown.BorderColor);
                    dropdown.Show(FindForm());
                }
                else
                {
                    dropdown.Hide();
                }

                dropdownactivated = !dropdownactivated;
            }
        }

        private void Ddc_Deactivate(object sender, EventArgs e)
        {
            dropdownactivated = false;
            dropdown.Hide();
        }

        private void Ddc_SelectedIndexChanged(object sender, EventArgs e, bool key)
        {
            dropdownactivated = false;
            dropdown.Hide();
            SelectedIndexChanged?.Invoke(this, e);
        }

        protected override bool ThemeDerived(Theme t, Font fnt)
        {
            ForeColor = t.ButtonTextColor;
            SelectionMarkColor = t.ButtonTextColor;
            BorderColor = t.ButtonBorderColor;
            DropDownTheme.SetFromCombo(t);
            dropdown.FlatStyle = FlatStyle.Popup;
            return true;
        }
    }
}
