/*
 * Copyright © 2016-2021 EDDiscovery development team
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
 *
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

            InitColourPatch(panel_theme1, "Form Back Colour", np[nameof(Theme.Form)]);                  // using tag, and tool tips, hook up patches to enum
            InitColourPatch(panel_theme2, "Text box Back Colour", np[nameof(Theme.TextBackColor)]);
            InitColourPatch(panel_theme3, "Text box Text Colour", np[nameof(Theme.TextBlockColor)]);
            InitColourPatch(panel_theme4, "Text box Highlight Colour", np[nameof(Theme.TextBlockHighlightColor)]);
            InitColourPatch(panel_theme15, "Text box Success Colour", np[nameof(Theme.TextBlockSuccessColor)]);
            InitColourPatch(panel_theme5, "Button Back Colour", np[nameof(Theme.ButtonBackColor)]);
            InitColourPatch(panel_theme6, "Button Text Colour", np[nameof(Theme.ButtonTextColor)]);
            InitColourPatch(panel_theme7, "Grid Border Back Colour", np[nameof(Theme.GridBorderBack)]);
            InitColourPatch(panel_theme8, "Grid Border Text Colour", np[nameof(Theme.GridBorderText)]);
            InitColourPatch(panel_theme9, "Grid Cell Back Colour", np[nameof(Theme.GridCellBack)]);
            InitColourPatch(panel_theme10, "Grid Cell Text Colour", np[nameof(Theme.GridCellText)]);
            InitColourPatch(panel_theme11, "Menu Back Colour", np[nameof(Theme.MenuBack)]);
            InitColourPatch(panel_theme12, "Menu Text Colour", np[nameof(Theme.MenuFore)]);
            InitColourPatch(panel_theme13, "Visited system without known position", np[nameof(Theme.KnownSystemColor)]);
            InitColourPatch(panel_theme14, "Visited system with coordinates", np[nameof(Theme.UnknownSystemColor)]);
            InitColourPatch(panel_theme16, "Check Box Text Colour", np[nameof(Theme.CheckBox)]);
            InitColourPatch(panel_theme17, "Label Text Colour", np[nameof(Theme.LabelColor)]);
            InitColourPatch(panel_theme18, "Group box Back Colour", np[nameof(Theme.GroupBack)]);
            InitColourPatch(panel_theme19, "Group box Text Colour", np[nameof(Theme.GroupFore)]);
            InitColourPatch(panel_theme30, "Text Box Border Colour", np[nameof(Theme.TextBlockBorderColor)]);
            InitColourPatch(panel_theme31, "Button Border Colour", np[nameof(Theme.ButtonBorderColor)]);
            InitColourPatch(panel_theme32, "Grid Border Line Colour", np[nameof(Theme.GridBorderLines)]);
            InitColourPatch(panel_theme33, "Group box Border Line Colour", np[nameof(Theme.GroupBorder)]);
            InitColourPatch(panel_theme35, "Tab Control Border Line Colour", np[nameof(Theme.TabcontrolBorder)]);
            InitColourPatch(panel_theme40, "Text Box Scroll Bar Slider Colour", np[nameof(Theme.TextBlockSliderBack)]);
            InitColourPatch(panel_theme41, "Text Box Scroll Bar Arrow Colour", np[nameof(Theme.TextBlockScrollArrow)]);
            InitColourPatch(panel_theme42, "Text Box Scroll Bar Button Colour", np[nameof(Theme.TextBlockScrollButton)]);
            InitColourPatch(panel_theme43, "Grid Scroll Bar Slider Colour", np[nameof(Theme.GridSliderBack)]);
            InitColourPatch(panel_theme44, "Grid Scroll Bar Arrow Colour", np[nameof(Theme.GridScrollArrow)]);
            InitColourPatch(panel_theme45, "Grid Scroll Bar Button Colour", np[nameof(Theme.GridScrollButton)]);
            InitColourPatch(panel_theme50, "Menu Dropdown Back Colour", np[nameof(Theme.ToolStripDropdownBack)]);
            InitColourPatch(panel_theme51, "Menu Dropdown Text Colour", np[nameof(Theme.ToolStripDropdownFore)]);
            InitColourPatch(panel_theme60, "Tool Strip Back Colour", np[nameof(Theme.ToolstripBack)]);
            InitColourPatch(panel_theme61, "Tool Strip Border Colour", np[nameof(Theme.ToolstripBorder)]);
            InitColourPatch(panel_theme70, "Check Box Tick Color", np[nameof(Theme.CheckBoxTick)]);
            InitColourPatch(panel_theme71, "S-Panel Text Colour", np[nameof(Theme.SPanelColor)]);
            InitColourPatch(panel_theme72, "Transparent Colour Key", np[nameof(Theme.TransparentColorKey)]);
            InitColourPatch(panel_theme80, "Grid Cell Alt Back Colour", np[nameof(Theme.GridCellAltBack)]);
            InitColourPatch(panel_theme81, "Grid Cell Alt Text Colour", np[nameof(Theme.GridCellAltText)]);
            InitColourPatch(panel_theme82, "Grid Highlight Back Colour", np[nameof(Theme.GridHighlightBack)]);
            InitColourPatch(panel_theme90, "Check Button Tick Color", np[nameof(Theme.CheckBoxButtonTick)]);

            InitColourPatch(panel_chart1, "Chart Series 1", np[nameof(Theme.Chart1)]);
            InitColourPatch(panel_chart2, "Chart Series 2", np[nameof(Theme.Chart2)]);
            InitColourPatch(panel_chart3, "Chart Series 3", np[nameof(Theme.Chart3)]);
            InitColourPatch(panel_chart4, "Chart Series 4", np[nameof(Theme.Chart4)]);
            InitColourPatch(panel_chart5, "Chart Series 5", np[nameof(Theme.Chart5)]);
            InitColourPatch(panel_chart6, "Chart Series 6", np[nameof(Theme.Chart6)]);
            InitColourPatch(panel_chart7, "Chart Series 7", np[nameof(Theme.Chart7)]);
            InitColourPatch(panel_chart8, "Chart Series 8", np[nameof(Theme.Chart8)]);

            InitFloat(numericUpDownButDir, "Button Gradient Direction in degrees", np[nameof(Theme.ButtonGradientDirection)]);
            InitFloat(numericUpDownCBDir, "Check Box Gradient Direction in degrees", np[nameof(Theme.CheckBoxGradientDirection)]);
            InitFloat(numericUpDownLBDir, "List Box Gradient Direction in degrees", np[nameof(Theme.ListBoxGradientDirection)]);
            InitFloat(numericUpDownGBDir, "Group Box Gradient Direction in degrees", np[nameof(Theme.GroupBoxGradientDirection)]);

            InitFloat(numericUpDownButGA, "Button Gradient Amount", np[nameof(Theme.ButtonGradientAmount)]);
            InitFloat(numericUpDownCBGA, "Check Box Gradient Amount", np[nameof(Theme.CheckBoxGradientAmount)]);
            InitFloat(numericUpDownLBGA, "List Box Gradient Amount", np[nameof(Theme.ListBoxGradientAmount)]);
            InitFloat(numericUpDownGBGA, "Group Box Gradient Amount", np[nameof(Theme.GroupBoxGradientAmount)]);

            InitFloat(numericUpDownCBCS, "Check Box Scaling of colour", np[nameof(Theme.CheckBoxInnerScaling)]);
            InitFloat(numericUpDownCBTS, "Check Box Tick Size as a proportion of the check box", np[nameof(Theme.CheckBoxTickSize)]);

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
