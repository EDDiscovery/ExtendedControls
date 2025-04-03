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
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ExtendedControls
{
    // Editor for the standard themes

    public partial class ThemeEditor : Form
    {
        public Action<Theme> ApplyChanges = null;

        public Theme Theme { get; private set; }            // theme is copied and edited in here

        public ThemeEditor()
        {
            InitializeComponent();
        }

        public void InitForm(Theme toedit)
        {
            Theme = new Theme(toedit);              // take a copy

            comboBox_TextBorder.DataSource = Theme.TextboxBorderStyles;
            comboBox_ButtonStyle.DataSource = Theme.ButtonStyles;

            var tp = typeof(Theme).GetProperties(System.Reflection.BindingFlags.Public| System.Reflection.BindingFlags.Instance);
            Dictionary<string, PropertyInfo> np = tp.ToDictionary(k => k.Name, v => v);

            InitColourPatch(panel_themeForm, "Form Back Colour", np[nameof(Theme.Form)]);                  // using tag, and tool tips, hook up patches to enum

            InitColourPatch(panel_themeTBBack, "Text box Back Colour", np[nameof(Theme.TextBlockBackColor)]);
            InitColourPatch(panel_themeTBFore, "Text box Text Colour", np[nameof(Theme.TextBlockForeColor)]);
            InitColourPatch(panel_themeTBHighlight, "Text box Highlight Colour", np[nameof(Theme.TextBlockHighlightColor)]);
            InitColourPatch(panel_themeTBSucess, "Text box Success Colour", np[nameof(Theme.TextBlockSuccessColor)]);
            InitColourPatch(panel_themeTBBorder, "Text Box Border Colour", np[nameof(Theme.TextBlockBorderColor)]);
            InitColourPatch(panel_themeTBScrollSlider, "Text Box Scroll Bar Slider Colour", np[nameof(Theme.TextBlockSliderBack)]);
            InitColourPatch(panel_themeTBScrollArrow, "Text Box Scroll Bar Arrow Colour", np[nameof(Theme.TextBlockScrollArrow)]);
            InitColourPatch(panel_themeTBScrollButton, "Text Box Scroll Bar Button Colour", np[nameof(Theme.TextBlockScrollButton)]);
            InitColourPatch(panel_themeTextBlockDropDownBackColor, "Text Box Drop Down Back Color", np[nameof(Theme.TextBlockDropDownBackColor)]);
            InitColourPatch(panel_themeTextBlockDropDownBackColor2, "Text Box Drop Down Back Color 2", np[nameof(Theme.TextBlockDropDownBackColor2)]);


            InitColourPatch(panel_themeGridBack, "Grid Border Back Colour", np[nameof(Theme.GridBorderBack)]);
            InitColourPatch(panel_themeGridFore, "Grid Border Text Colour", np[nameof(Theme.GridBorderText)]);
            InitColourPatch(panel_themeGridBorderBorder, "Grid Border Line Colour", np[nameof(Theme.GridBorderLines)]);
            InitColourPatch(panel_themeGridScrollSlider, "Grid Scroll Bar Slider Colour", np[nameof(Theme.GridSliderBack)]);
            InitColourPatch(panel_themeGridScollArrow, "Grid Scroll Bar Arrow Colour", np[nameof(Theme.GridScrollArrow)]);
            InitColourPatch(panel_themeGridScrollButton, "Grid Scroll Bar Button Colour", np[nameof(Theme.GridScrollButton)]);
            InitColourPatch(panel_themeGridHighlightBack, "Grid Highlight Back Colour", np[nameof(Theme.GridHighlightBack)]);

            InitColourPatch(panel_themeGridCellBack, "Grid Cell Back Colour", np[nameof(Theme.GridCellBack)]);
            InitColourPatch(panel_themeGridCellFore, "Grid Cell Text Colour", np[nameof(Theme.GridCellText)]);

            InitColourPatch(panel_themeGridAltBack, "Grid Cell Alt Back Colour", np[nameof(Theme.GridCellAltBack)]);
            InitColourPatch(panel_themeGridAltFore, "Grid Cell Alt Text Colour", np[nameof(Theme.GridCellAltText)]);

            InitColourPatch(panel_themeUnknown, "Visited system without known position", np[nameof(Theme.KnownSystemColor)]);
            InitColourPatch(panel_themeKnown, "Visited system with coordinates", np[nameof(Theme.UnknownSystemColor)]);

            InitColourPatch(panel_themeButtonBack, "Button Back Colour", np[nameof(Theme.ButtonBackColor)]);
            InitColourPatch(panel_themeButtonBack2, "Button Back Colour 2", np[nameof(Theme.ButtonBackColor2)]);
            InitColourPatch(panel_themeButtonFore, "Button Text Colour", np[nameof(Theme.ButtonTextColor)]);
            InitColourPatch(panel_themeButtonBorder, "Button Border Colour", np[nameof(Theme.ButtonBorderColor)]);
            InitFloat(numericUpDownButtonGradiantDir, "Button Gradient Direction in degrees", np[nameof(Theme.ButtonGradientDirection)]);

            InitColourPatch(panel_themeListBoxBack, "List Box Back Colour", np[nameof(Theme.ListBoxBackColor)]);
            InitColourPatch(panel_themeListBoxBack2, "List Box Back Colour 2", np[nameof(Theme.ListBoxBackColor2)]);
            InitColourPatch(panel_themeListBoxFore, "List Box Text Colour", np[nameof(Theme.ListBoxTextColor)]);
            InitColourPatch(panel_themeListBoxBorder, "List Box Border Colour", np[nameof(Theme.ListBoxBorderColor)]);
            InitColourPatch(panel_themeListBoxScrollSlider, "List Box Scroll Slider", np[nameof(Theme.ListBoxSliderBack)]);
            InitColourPatch(panel_themeListBoxScrollButton, "List Box Scroll Button", np[nameof(Theme.ListBoxScrollButton)]);
            InitColourPatch(panel_themeListBoxScrollArrow, "List Box Scroll Arrow", np[nameof(Theme.ListBoxScrollArrow)]);
            InitFloat(numericUpDownListBoxGradiantDir, "List Box Gradient Direction in degrees", np[nameof(Theme.ListBoxGradientDirection)]);

            InitColourPatch(panel_themeComboBoxBack, "Combo Box Back Colour", np[nameof(Theme.ComboBoxBackColor)]);
            InitColourPatch(panel_themeComboBoxBack2, "Combo Box Back Colour 2", np[nameof(Theme.ComboBoxBackColor2)]);
            InitColourPatch(panel_themeComboBoxFore, "Combo Box Text Colour", np[nameof(Theme.ComboBoxTextColor)]);
            InitColourPatch(panel_themeComboBoxBorder, "Combo Box Border Colour", np[nameof(Theme.ComboBoxBorderColor)]);
            InitColourPatch(panel_themeComboBoxScrollSlider, "Combo Box Scroll Slider", np[nameof(Theme.ComboBoxSliderBack)]);
            InitColourPatch(panel_themeComboBoxScrollButton, "Combo Box Scroll Button", np[nameof(Theme.ComboBoxScrollButton)]);
            InitColourPatch(panel_themeComboBoxScrollArrow, "Combo Box Scroll Arrow", np[nameof(Theme.ComboBoxScrollArrow)]);
            InitFloat(numericUpDownComboBoxGradiantDir, "Combo Box Gradient Direction in degrees", np[nameof(Theme.ComboBoxGradientDirection)]);

            InitColourPatch(panel_themeMenuBack, "Menu Back Colour", np[nameof(Theme.MenuBack)]);
            InitColourPatch(panel_themeMenuFore, "Menu Text Colour", np[nameof(Theme.MenuFore)]);

            InitColourPatch(panel_themeCheckBoxBack, "Check Box Back Colour", np[nameof(Theme.CheckBoxBack)]);
            InitColourPatch(panel_themeCheckBoxBack2, "Check Box Back Colour 2", np[nameof(Theme.CheckBoxBack2)]);
            InitColourPatch(panel_themeCheckBoxText, "Check Box Text Colour", np[nameof(Theme.CheckBoxText)]);
            InitColourPatch(panel_themeCheckBoxTick, "Check Box Tick Color", np[nameof(Theme.CheckBoxTick)]);
            InitColourPatch(panel_themeCheckBoxButtonApp, "Check Button Syle Back Colour when Ticked", np[nameof(Theme.CheckBoxButtonTickedBack)]);
            InitColourPatch(panel_themeCheckBoxButtonApp2, "Check Button Syle Back Colour 2 when Ticked", np[nameof(Theme.CheckBoxButtonTickedBack2)]);
            InitColourPatch(panel_themeCheckBoxBorder, "Check Button Syle Border Colour", np[nameof(Theme.CheckBoxBorderColor)]);
            InitFloat(numericUpDownCBTickStyleTickSize, "Check Box Tick Size as a proportion of the check box", np[nameof(Theme.CheckBoxTickSize)]);
            InitFloat(numericUpDownCheckBoxGradiantDir, "Check Box all styles Gradient Direction in degrees", np[nameof(Theme.CheckBoxGradientDirection)]);

            InitColourPatch(panel_themeGBBack, "Group box Back Colour", np[nameof(Theme.GroupBack)]);
            InitColourPatch(panel_themeGBBack2, "Group box Back Colour 2", np[nameof(Theme.GroupBack2)]);
            InitColourPatch(panel_themeGBFore, "Group box Text Colour", np[nameof(Theme.GroupFore)]);
            InitColourPatch(panel_themeGBBorder, "Group box Border Line Colour", np[nameof(Theme.GroupBorder)]);
            InitFloat(numericUpDownGroupBoxGradiantDir, "Group Box Gradient Direction in degrees", np[nameof(Theme.GroupBoxGradientDirection)]);

            InitColourPatch(panel_themeTabStripBack, "Tab Strip Back Color", np[nameof(Theme.TabStripBack)]);
            InitColourPatch(panel_themeTabStripBack2, "Tab Strip Back Color 2", np[nameof(Theme.TabStripBack2)]);
            InitColourPatch(panel_themeTabStripFore, "Tab Strip Text Color", np[nameof(Theme.TabStripFore)]);
            InitColourPatch(panel_themeTabStripSelected, "Tab Strip Selected Color", np[nameof(Theme.TabStripSelected)]);
            InitFloat(numericUpDownTabStripGradiantDir, "Tab Strip Gradient Direction in degrees", np[nameof(Theme.TabStripGradientDirection)]);

            InitColourPatch(panel_themeTabControlBorder, "Tab Control Border Line Colour", np[nameof(Theme.TabcontrolBorder)]);

            InitColourPatch(panel_themeSPanel, "S-Panel Text Colour", np[nameof(Theme.SPanelColor)]);
            InitColourPatch(panel_themeLabel, "Label Text Colour", np[nameof(Theme.LabelColor)]);


            InitColourPatch(panel_themeToolStripBack, "Tool Strip Back Colour", np[nameof(Theme.ToolstripBack)]);
            InitColourPatch(panel_themeToolStripBorder, "Tool Strip Border Colour", np[nameof(Theme.ToolstripBorder)]);
            InitColourPatch(panel_themeDropDownMenuBack, "Menu Dropdown Back Colour", np[nameof(Theme.ToolStripDropdownBack)]);
            InitColourPatch(panel_themeDropDownMenuText, "Menu Dropdown Text Colour", np[nameof(Theme.ToolStripDropdownFore)]);

            InitColourPatch(panel_themeTransparentColourKey, "Transparent Colour Key", np[nameof(Theme.TransparentColorKey)]);

            InitColourPatch(panel_chart1, "Chart Series 1", np[nameof(Theme.Chart1)]);
            InitColourPatch(panel_chart2, "Chart Series 2", np[nameof(Theme.Chart2)]);
            InitColourPatch(panel_chart3, "Chart Series 3", np[nameof(Theme.Chart3)]);
            InitColourPatch(panel_chart4, "Chart Series 4", np[nameof(Theme.Chart4)]);
            InitColourPatch(panel_chart5, "Chart Series 5", np[nameof(Theme.Chart5)]);
            InitColourPatch(panel_chart6, "Chart Series 6", np[nameof(Theme.Chart6)]);
            InitColourPatch(panel_chart7, "Chart Series 7", np[nameof(Theme.Chart7)]);
            InitColourPatch(panel_chart8, "Chart Series 8", np[nameof(Theme.Chart8)]);

            InitFloat(numericUpDownDialogFontScaling, "Scaling between the normal font size and a dialog font size", np[nameof(Theme.DialogFontScaling)]);
            InitFloat(numericUpDownMouseOverScaling, "Scaling of colour for mouse over events", np[nameof(Theme.MouseOverScaling)]);
            InitFloat(numericUpDownMouseSelectedScaling, "Scaling of colour for mouse down selected events", np[nameof(Theme.MouseSelectedScaling)]);
            InitFloat(numericUpDownDisabledScaling, "Scaling of colour for disabled controls", np[nameof(Theme.DisabledScaling)]);

            trackBar_theme_opacity.Value = (int)Theme.Opacity;
            comboBox_TextBorder.SelectedItem = Theme.TextBoxBorderStyle;
            comboBox_ButtonStyle.SelectedItem = Theme.ButtonStyle;
            checkBox_theme_windowframe.Checked = Environment.OSVersion.Platform != PlatformID.Win32NT  ? false : Theme.WindowsFrame;

            this.checkBox_theme_windowframe.CheckedChanged += new System.EventHandler(this.checkBox_theme_windowframe_CheckedChanged);
            this.trackBar_theme_opacity.ValueChanged += new System.EventHandler(this.trackBar_theme_opacity_ValueChanged);
            UpdateFontText();
        }

        private void InitColourPatch(Panel pn, string tooltip, PropertyInfo pi)
        {
            toolTip1.SetToolTip(pn, tooltip);        // assign tool tips and indicate which color to edit
            pn.Tag = pi;
            pn.BackColor = (Color)pi.GetValue(Theme);
            pn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_theme_Click);
        }

        private void InitFloat( NumericUpDown c, string tooltip, PropertyInfo pi)
        {
            toolTip1.SetToolTip(c, tooltip);        // assign tool tips and indicate which color to edit
            c.Tag = pi;
            c.Value = (decimal)(float)pi.GetValue(Theme);
            c.ValueChanged += C_ValueChanged;
        }

        private void C_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown c = (NumericUpDown)sender;
            PropertyInfo pi = (PropertyInfo)(((Control)sender).Tag);
            decimal value = c.Value;
            pi.SetValue(Theme, (float)value);
            Theme.SetCustom();
            ApplyChanges?.Invoke(Theme);
        }

        private void panel_theme_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            PropertyInfo pi = (PropertyInfo)(((Control)sender).Tag);
            Color c = (Color)pi.GetValue(Theme);
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = true;
            MyDialog.FullOpen = true;
            MyDialog.Color = c;

            if (MyDialog.ShowDialog(this) == DialogResult.OK)
            {
                System.Diagnostics.Debug.Write($"Theme {pi.Name} with {c}");
                Theme.SetCustom();
                pi.SetValue(Theme, MyDialog.Color);
                panel.BackColor = MyDialog.Color;
                ApplyChanges?.Invoke(Theme);
            }
        }

        private void checkBox_theme_windowframe_CheckedChanged(object sender, EventArgs e)
        {
            Theme.WindowsFrame = checkBox_theme_windowframe.Checked;
            Theme.SetCustom();
            ApplyChanges?.Invoke(Theme);
        }

        private void trackBar_theme_opacity_ValueChanged(object sender, EventArgs e)
        {
            Theme.Opacity = (double)trackBar_theme_opacity.Value;
            Theme.SetCustom();
            ApplyChanges?.Invoke(Theme);
        }


        private void buttonFontChange_Click(object sender, EventArgs e)
        {
            textBox_Font_MouseClick(sender, null);
        }

        private void textBox_Font_MouseClick(object sender, MouseEventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                fd.Font = BaseUtils.FontLoader.GetFont(Theme.FontName, Theme.FontSize);
                fd.MinSize = 4;
                fd.MaxSize = 36;
                DialogResult result;

                try
                {
                    result = fd.ShowDialog(this);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                if (result == DialogResult.OK)
                {
                    Theme.FontName = fd.Font.Name;
                    Theme.FontSize = fd.Font.Size;
                    Theme.FontStyle = fd.Font.Style;
                    UpdateFontText();
                    Theme.SetCustom();
                    ApplyChanges?.Invoke(Theme);
                }
            }
        }

        void UpdateFontText()
        {
            textBox_Font.Text = Theme.FontName + " " + (Theme.FontStyle!= FontStyle.Regular ? Theme.FontStyle.ToString() + " " : "" ) + Theme.FontSize + " points" ;
        }

        private void comboBox_TextBorder_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Theme.TextBoxBorderStyle = (string)comboBox_TextBorder.SelectedItem;
            Theme.SetCustom();
            ApplyChanges?.Invoke(Theme);
        }

        private void comboBox_ButtonStyle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Theme.ButtonStyle = (string)comboBox_ButtonStyle.SelectedItem;
            Theme.SetCustom();
            ApplyChanges?.Invoke(Theme);
        }

        //private void NumericUpDownCheckboxGradient_ValueChanged(object sender, EventArgs e)
        //{
        //    Theme.CheckBoxGradientDirection = (float)numericUpDownCheckboxGradient.Value;
        //    Theme.SetCustom();
        //    ApplyChanges?.Invoke(Theme);
        //}

        //private void NumericUpDownButtonGradient_ValueChanged(object sender, EventArgs e)
        //{
        //    Theme.ButtonGradientDirection = (float)numericUpDownButtonGradient.Value;
        //    Theme.SetCustom();
        //    ApplyChanges?.Invoke(Theme);
        //}


        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void checkBoxDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLabelColors(this, checkBoxDarkMode.Checked ? Color.DarkOrange : Color.Black);
            if (checkBoxDarkMode.Checked)
                this.BackColor = Color.Black;
            else
                this.BackColor = SystemColors.Control;
        }
        private void ChangeLabelColors(Control ctrl, Color labeltext)
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c is Label || c is CheckBox || c is GroupBox)
                    c.ForeColor = labeltext;
                else
                    ChangeLabelColors(c, labeltext);
            }
        }
    }
}
