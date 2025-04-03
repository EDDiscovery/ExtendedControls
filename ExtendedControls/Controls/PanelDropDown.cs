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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPanelDropDown : Panel, IThemeable
    {
        public event EventHandler SelectedIndexChanged;

        public int SelectionSize { get; set; } = 8;

        public List<string> Items { get { return dropdown.Items; } set { dropdown.Items = value; } }

        public override Color ForeColor { get { return base.ForeColor; } set { dropdown.ForeColor = base.ForeColor = value; } }
        public bool FitToItemsHeight { get { return dropdown.FitToItemsHeight; } set { dropdown.FitToItemsHeight = value; } }

        public Color SelectionMarkColor { get; set; } = Color.Yellow;
        public Color BorderColor { get { return dropdown.BorderColor; } set { dropdown.BorderColor = value; } } 
        public FlatStyle FlatStyle { get { return dropdown.FlatStyle; } set { dropdown.FlatStyle = value; } }
        public float GradientDirection { get; set; } = 90F;
        // drop down box for selection
        public Color DropDownSelectionBackgroundColor { get; set; } = Color.Gray;
        public Color DropDownSelectionBackgroundColor2 { get; set; } = Color.Gray;      // background
        public Color DropDownSelectionColor { get; set; } = Color.Green;       // selection bar
        public Color DropDownSliderColor { get; set; } = Color.Green;
        public Color DropDownSliderArrowColor { get; set; } = Color.Cyan;
        public Color DropDownBorderColor { get; set; } = Color.Green;
        public Color DropDownSliderButtonColor { get; set; } = Color.Blue;
        public Color DropDownMouseOverSliderButtonColor { get; set; } = Color.Red;
        public Color PressedDropDownSliderButtonColor { get; set; } = Color.DarkCyan;

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
                    dropdown.ListBox.BackColor = BackColor;
                    dropdown.ListBox.ForeColor = ForeColor;
                    dropdown.ListBox.SelectionBackColor = this.DropDownSelectionBackgroundColor;
                    dropdown.ListBox.SelectionBackColor2 = this.DropDownSelectionBackgroundColor2;
                    dropdown.ListBox.SelectionColor = this.DropDownSelectionColor;
                    dropdown.ListBox.GradientDirection = this.GradientDirection;
                    dropdown.ListBox.BorderColor = this.BorderColor;
                    dropdown.ListBox.ScrollBar.BackColor = dropdown.ListBox.ScrollBar.SliderColor = this.DropDownSliderColor;
                    dropdown.ListBox.ScrollBar.ForeColor = this.DropDownSliderArrowColor;    // arrow
                    dropdown.ListBox.ScrollBar.ThumbBorderColor = dropdown.ListBox.ScrollBar.ArrowBorderColor =
                                                                    dropdown.ListBox.ScrollBar.BorderColor = this.DropDownBorderColor;
                    dropdown.ListBox.ScrollBar.ArrowButtonColor = dropdown.ListBox.ScrollBar.ThumbButtonColor = this.DropDownSliderButtonColor;
                    dropdown.ListBox.ScrollBar.MouseOverButtonColor = this.DropDownMouseOverSliderButtonColor;
                    dropdown.ListBox.ScrollBar.MousePressedButtonColor = this.PressedDropDownSliderButtonColor;

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

        public bool Theme(Theme t, Font fnt)
        {
            ForeColor = t.ButtonTextColor;
            SelectionMarkColor = t.ButtonTextColor;
            BorderColor = t.ButtonBorderColor;
            FlatStyle = FlatStyle.Popup;
            GradientDirection = t.ComboBoxGradientDirection;

            DropDownSelectionBackgroundColor = t.ComboBoxBackColor;
            DropDownSelectionBackgroundColor2 = t.ComboBoxBackColor2;
            DropDownSelectionColor = t.ComboBoxBackColor.Multiply(t.MouseSelectedScaling);
            DropDownSliderColor = t.ComboBoxSliderBack;
            DropDownSliderArrowColor = t.ComboBoxScrollArrow;
            DropDownBorderColor = t.ComboBoxBorderColor;
            DropDownSliderButtonColor = t.ComboBoxScrollButton;
            DropDownMouseOverSliderButtonColor = DropDownSliderButtonColor.Multiply(t.MouseOverScaling);
            PressedDropDownSliderButtonColor = DropDownSliderButtonColor.Multiply(t.MouseSelectedScaling);

            return true;
        }
    }
}
