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

namespace ExtendedControls
{
    public class DropDownTheme
    {
        // list box
        public Color ListBoxSelectionBackgroundColor { get; set; } = Color.Gray;       // background
        public Color ListBoxSelectionBackgroundColor2 { get; set; } = Color.Gray;      // background
        public Color ListBoxSelectionColor { get; set; } = Color.Green;       // selection bar
        public float ListBoxGradientDirection { get; set; }

        // scroll bar
        public Color SliderColor { get; set; } = Color.Green;
        public Color SliderColor2 { get; set; } = Color.Green;
        public float SliderGradientDirection { get; set; } = 90F;

        public Color SliderArrowBackColor { get; set; } = Color.Cyan;
        public Color SliderArrowBackColor2 { get; set; } = Color.Cyan;

        public Color SliderButtonBackColor { get; set; } = Color.Blue;
        public Color SliderButtonBackColor2 { get; set; } = Color.Blue;
        public float SliderButtonGradientDirection { get; set; } = 90F;

        public Color SliderButtonArrowColor { get; set; } = Color.White;

        public bool SliderSkinnyTheme { get; set; } = false;

        public Color MouseOverSliderButtonColor { get; set; } = Color.Red;
        public Color MouseOverSliderButtonColor2 { get; set; } = Color.Red;
        public Color PressedSliderButtonColor { get; set; } = Color.DarkCyan;
        public Color PressedSliderButtonColor2 { get; set; } = Color.DarkCyan;

        public void Theme(ExtListBox lb, Color ForeColor, Color BackColor, Color BorderColor)
        {
            lb.BackColor = BackColor;
            lb.ForeColor = ForeColor;
            lb.BorderColor = BorderColor;
            lb.SelectionBackColor = this.ListBoxSelectionBackgroundColor;
            lb.SelectionBackColor2 = this.ListBoxSelectionBackgroundColor2;
            lb.SelectionColor = this.ListBoxSelectionColor;
            lb.BackGradientDirection = this.ListBoxGradientDirection;
            lb.SelectionColor = this.ListBoxSelectionColor;
        }

        public void Theme(ExtScrollBar ScrollBar, Color BorderColor, Font f)
        {
            ScrollBar.ThumbBorderColor = ScrollBar.ArrowBorderColor = ScrollBar.BorderColor = BorderColor;
            ScrollBar.BackColor = ScrollBar.SliderColor = this.SliderColor;
            ScrollBar.SliderColor2 = this.SliderColor2;
            ScrollBar.SliderDrawAngle = this.SliderGradientDirection;
            ScrollBar.ForeColor = this.SliderArrowBackColor;    // arrow
            ScrollBar.ArrowButtonColor = this.SliderArrowBackColor;
            ScrollBar.ArrowButtonColor2 = this.SliderArrowBackColor2;
            ScrollBar.ThumbButtonColor = this.SliderButtonBackColor;
            ScrollBar.ThumbButtonColor2 = this.SliderButtonBackColor2;
            ScrollBar.ThumbDrawAngle = this.SliderButtonGradientDirection;
            ScrollBar.MouseOverButtonColor = this.MouseOverSliderButtonColor;
            ScrollBar.MouseOverButtonColor2 = this.MouseOverSliderButtonColor2;
            ScrollBar.MousePressedButtonColor = this.PressedSliderButtonColor;
            ScrollBar.MousePressedButtonColor2 = this.PressedSliderButtonColor2;
            ScrollBar.SkinnyStyle = this.SliderSkinnyTheme;
            ScrollBar.Width = ExtendedControls.Theme.ScrollBarWidth(f, this.SliderSkinnyTheme);
        }

        public void SetFromCombo(Theme t)
        {
            ListBoxSelectionBackgroundColor = t.ComboBoxBackColor;
            ListBoxSelectionBackgroundColor2 = t.ComboBoxBackColor2;
            ListBoxSelectionColor = t.ComboBoxBackColor.Multiply(t.MouseSelectedScaling);
            ListBoxGradientDirection = t.ComboBoxBackAndDropDownGradientDirection;

            SliderColor = t.ComboBoxDropDownSliderBack;
            SliderColor2 = t.IsButtonGradientStyle ? t.ComboBoxDropDownSliderBack2 : t.ComboBoxDropDownSliderBack;
            SliderGradientDirection = t.ComboBoxDropDownSliderGradientDirection;

            SliderArrowBackColor = t.ComboBoxScrollArrowBack;
            SliderArrowBackColor2 = t.IsButtonGradientStyle ? t.ComboBoxScrollArrowBack2 : t.ComboBoxScrollArrowBack;
            // arrow dir not configurable

            SliderButtonBackColor = t.ComboBoxScrollButtonBack;
            SliderButtonBackColor2 = t.IsButtonGradientStyle ? t.ComboBoxScrollButtonBack2 : t.ComboBoxScrollButtonBack;
            SliderButtonGradientDirection = t.ComboBoxDropDownScrollButtonGradientDirection;

            SliderSkinnyTheme = t.SkinnyScrollBars;

            SliderButtonArrowColor = t.ComboBoxScrollArrow;

            MouseOverSliderButtonColor = SliderButtonBackColor.Multiply(t.MouseOverScaling);
            MouseOverSliderButtonColor2 = SliderButtonBackColor2.Multiply(t.MouseOverScaling);
            PressedSliderButtonColor = SliderButtonBackColor.Multiply(t.MouseSelectedScaling);
            PressedSliderButtonColor2 = SliderButtonBackColor2.Multiply(t.MouseSelectedScaling);

        }

        public void SetFromTextBlock(Theme t)
        {
            ListBoxSelectionBackgroundColor = t.TextBlockDropDownBackColor;
            ListBoxSelectionBackgroundColor2 = t.TextBlockDropDownBackColor2;
            ListBoxSelectionColor = ListBoxSelectionBackgroundColor.Multiply(t.MouseSelectedScaling);
            ListBoxGradientDirection = t.TextBlockDropDownBackGradientDirection;

            SliderColor = t.TextBlockSliderBack;
            SliderColor2 = t.IsButtonGradientStyle ? t.TextBlockSliderBack2 : t.TextBlockSliderBack;
            SliderGradientDirection = t.TextBlockSliderGradientDirection;

            SliderArrowBackColor = t.TextBlockScrollArrowBack;
            SliderArrowBackColor2 = t.IsButtonGradientStyle ? t.TextBlockScrollArrowBack2 : t.TextBlockScrollArrowBack;
            // arrow dir not configurable

            SliderButtonBackColor = t.TextBlockScrollButtonBack;
            SliderButtonBackColor2 = t.IsButtonGradientStyle ? t.TextBlockScrollButtonBack2 : t.TextBlockScrollButtonBack;
            SliderButtonGradientDirection = t.TextBlockScrollButtonGradientDirection;

            SliderSkinnyTheme = t.SkinnyScrollBars;

            MouseOverSliderButtonColor = SliderButtonBackColor.Multiply(t.MouseOverScaling);
            MouseOverSliderButtonColor2 = SliderButtonBackColor2.Multiply(t.MouseOverScaling);
            PressedSliderButtonColor = SliderButtonBackColor.Multiply(t.MouseSelectedScaling);
            PressedSliderButtonColor2 = SliderButtonBackColor2.Multiply(t.MouseSelectedScaling);
        }

    }

}
