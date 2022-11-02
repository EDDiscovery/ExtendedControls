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
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */

using System;
using System.Drawing;
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

            SetPanel(panel_theme1, "Form Back Colour", Theme.CI.form);                  // using tag, and tool tips, hook up patches to enum
            SetPanel(panel_theme2, "Text box Back Colour", Theme.CI.textbox_back);
            SetPanel(panel_theme3, "Text box Text Colour", Theme.CI.textbox_fore);
            SetPanel(panel_theme4, "Text box Highlight Colour", Theme.CI.textbox_highlight);
            SetPanel(panel_theme15, "Text box Success Colour", Theme.CI.textbox_success);
            SetPanel(panel_theme5, "Button Back Colour", Theme.CI.button_back);
            SetPanel(panel_theme6, "Button Text Colour", Theme.CI.button_text);
            SetPanel(panel_theme7, "Grid Border Back Colour", Theme.CI.grid_borderback);
            SetPanel(panel_theme8, "Grid Border Text Colour", Theme.CI.grid_bordertext);
            SetPanel(panel_theme9, "Grid Cell Back Colour", Theme.CI.grid_cellbackground);
            SetPanel(panel_theme10, "Grid Cell Text Colour", Theme.CI.grid_celltext);
            SetPanel(panel_theme11, "Menu Back Colour", Theme.CI.menu_back);
            SetPanel(panel_theme12, "Menu Text Colour", Theme.CI.menu_fore);
            SetPanel(panel_theme13, "Visited system without known position", Theme.CI.travelgrid_nonvisted);
            SetPanel(panel_theme14, "Visited system with coordinates", Theme.CI.travelgrid_visited);
            SetPanel(panel_theme16, "Check Box Text Colour", Theme.CI.checkbox);
            SetPanel(panel_theme17, "Label Text Colour", Theme.CI.label);
            SetPanel(panel_theme18, "Group box Back Colour", Theme.CI.group_back);
            SetPanel(panel_theme19, "Group box Text Colour", Theme.CI.group_text);
            SetPanel(panel_theme30, "Text Box Border Colour", Theme.CI.textbox_border);
            SetPanel(panel_theme31, "Button Border Colour", Theme.CI.button_border);
            SetPanel(panel_theme32, "Grid Border Line Colour", Theme.CI.grid_borderlines);
            SetPanel(panel_theme33, "Group box Border Line Colour", Theme.CI.group_borderlines);
            SetPanel(panel_theme35, "Tab Control Border Line Colour", Theme.CI.tabcontrol_borderlines);
            SetPanel(panel_theme40, "Text Box Scroll Bar Slider Colour", Theme.CI.textbox_sliderback);
            SetPanel(panel_theme41, "Text Box Scroll Bar Arrow Colour", Theme.CI.textbox_scrollarrow);
            SetPanel(panel_theme42, "Text Box Scroll Bar Button Colour", Theme.CI.textbox_scrollbutton);
            SetPanel(panel_theme43, "Grid Scroll Bar Slider Colour", Theme.CI.grid_sliderback);
            SetPanel(panel_theme44, "Grid Scroll Bar Arrow Colour", Theme.CI.grid_scrollarrow);
            SetPanel(panel_theme45, "Grid Scroll Bar Button Colour", Theme.CI.grid_scrollbutton);
            SetPanel(panel_theme50, "Menu Dropdown Back Colour", Theme.CI.menu_dropdownback);
            SetPanel(panel_theme51, "Menu Dropdown Text Colour", Theme.CI.menu_dropdownfore);
            SetPanel(panel_theme60, "Tool Strip Back Colour", Theme.CI.toolstrip_back);
            SetPanel(panel_theme61, "Tool Strip Border Colour", Theme.CI.toolstrip_border);
            SetPanel(panel_theme70, "Check Box Tick Color", Theme.CI.checkbox_tick );
            SetPanel(panel_theme71, "S-Panel Text Colour", Theme.CI.s_panel);
            SetPanel(panel_theme72, "Transparent Colour Key", Theme.CI.transparentcolorkey);

            SetPanel(panel_theme80, "Grid Cell Alt Back Colour", Theme.CI.grid_altcellbackground);
            SetPanel(panel_theme81, "Grid Cell Alt Text Colour", Theme.CI.grid_altcelltext);
            SetPanel(panel_theme82, "Grid Highlight Back Colour", Theme.CI.grid_highlightback);

            UpdatePatchesEtc();

            trackBar_theme_opacity.Value = (int)Theme.Opacity;
            comboBox_TextBorder.SelectedItem = Theme.TextBoxBorderStyle;
            comboBox_ButtonStyle.SelectedItem = Theme.ButtonStyle;
            
            if ( Environment.OSVersion.Platform != PlatformID.Win32NT )
                checkBox_theme_windowframe.Visible = false;
        }

        public void UpdatePatchesEtc()                                         // update patch colours..
        {
            UpdatePatch(panel_theme1);
            UpdatePatch(panel_theme2);
            UpdatePatch(panel_theme3);
            UpdatePatch(panel_theme4);
            UpdatePatch(panel_theme5);
            UpdatePatch(panel_theme6);
            UpdatePatch(panel_theme7);
            UpdatePatch(panel_theme8);
            UpdatePatch(panel_theme9);
            UpdatePatch(panel_theme10);
            UpdatePatch(panel_theme11);
            UpdatePatch(panel_theme12);
            UpdatePatch(panel_theme13);
            UpdatePatch(panel_theme14);
            UpdatePatch(panel_theme15);
            UpdatePatch(panel_theme16);
            UpdatePatch(panel_theme17);
            UpdatePatch(panel_theme18);
            UpdatePatch(panel_theme19);
            UpdatePatch(panel_theme30);
            UpdatePatch(panel_theme31);
            UpdatePatch(panel_theme32);
            UpdatePatch(panel_theme33);
            UpdatePatch(panel_theme35);
            UpdatePatch(panel_theme40);
            UpdatePatch(panel_theme41);
            UpdatePatch(panel_theme42);
            UpdatePatch(panel_theme43);
            UpdatePatch(panel_theme44);
            UpdatePatch(panel_theme45);
            UpdatePatch(panel_theme50);
            UpdatePatch(panel_theme51);
            UpdatePatch(panel_theme60);
            UpdatePatch(panel_theme61);
            UpdatePatch(panel_theme70);
            UpdatePatch(panel_theme71);
            UpdatePatch(panel_theme72);
            UpdatePatch(panel_theme80);
            UpdatePatch(panel_theme81);
            UpdatePatch(panel_theme82);
            textBox_Font.Text = Theme.FontName + " " + Theme.FontSize + " points";
            checkBox_theme_windowframe.Checked = Theme.WindowsFrame;
        }

        private void UpdatePatch(Panel pn)
        {
            Theme.CI ci = (Theme.CI)(pn.Tag);
            pn.BackColor = Theme.GetColor(ci);
        }

        private void SetPanel(Panel pn, string name, Theme.CI ex)
        {
            toolTip1.SetToolTip(pn, name);        // assign tool tips and indicate which color to edit
            pn.Tag = ex;
            pn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_theme_Click);
        }

        private void trackBar_theme_opacity_MouseCaptureChanged(object sender, EventArgs e)
        {
            Theme.Opacity = (double)trackBar_theme_opacity.Value;
            Theme.SetCustom();
            ApplyChanges?.Invoke(Theme);
        }

        private void checkBox_theme_windowframe_MouseClick(object sender, MouseEventArgs e)
        {
            Theme.WindowsFrame = checkBox_theme_windowframe.Checked;
            Theme.SetCustom();
            ApplyChanges?.Invoke(Theme);
        }

        private void panel_theme_Click(object sender, EventArgs e)
        {
            Theme.CI ci = (Theme.CI)(((Control)sender).Tag);        // tag carries the colour we want to edit

            if (EditColor(ci))
            {
                UpdatePatchesEtc();
            }
        }

        public bool EditColor(Theme.CI ex)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = true;
            MyDialog.FullOpen = true;
            MyDialog.Color = Theme.GetColor(ex);

            if (MyDialog.ShowDialog(this) == DialogResult.OK)
            {
                Theme.SetColor(ex, MyDialog.Color);
                Theme.SetCustom();
                ApplyChanges?.Invoke(Theme);
                return true;
            }
            else
                return false;
        }

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
                    if (fd.Font.Style == FontStyle.Regular)
                    {
                        Theme.FontName = fd.Font.Name;
                        Theme.FontSize = fd.Font.Size;
                        UpdatePatchesEtc();

                        Theme.SetCustom();
                        ApplyChanges?.Invoke(Theme);
                    }
                    else
                        ExtendedControls.MessageBoxTheme.Show(this, "Font does not have regular style");
                }
            }
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

        private void checkBoxDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            SetLabels(this,checkBoxDarkMode.Checked ? Color.DarkOrange : Color.Black);
            if (checkBoxDarkMode.Checked)
                this.BackColor = Color.Black;
            else
                this.BackColor = SystemColors.Control;
        }

        private void SetLabels(Control ctrl, Color labeltext)
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c is Label || c is CheckBox || c is GroupBox)
                    c.ForeColor = labeltext;
                else
                    SetLabels(c, labeltext);
            }
        }
    }
}
