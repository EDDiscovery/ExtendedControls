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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ExtendedControls
{
    // Editor for the standard themes

    public partial class ThemeEditor : Form
    {
        public Action<Theme> ApplyChanges { get; set; } = null;

        public Theme Theme { get; private set; }            // theme is copied and edited in here

        private bool pendingchanges = false;

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
            InitColourPatch(panel_themeTBBorder2, "Text Box Border Colour 2", np[nameof(Theme.TextBlockBorderColor2)]);
            InitColourPatch(panel_themeTBScrollSliderBack, "Text Box Scroll Bar Slider Colour", np[nameof(Theme.TextBlockSliderBack)]);
            InitColourPatch(panel_themeTBScrollSliderBack2, "Text Box Scroll Bar Slider Colour 2", np[nameof(Theme.TextBlockSliderBack2)]);
            InitColourPatch(panel_themeTBScrollArrowBack, "Text Box Scroll Bar Arrow Colour", np[nameof(Theme.TextBlockScrollArrowBack)]);
            InitColourPatch(panel_themeTBScrollArrowBack2, "Text Box Scroll Bar Arrow Colour 2", np[nameof(Theme.TextBlockScrollArrowBack2)]);
            InitColourPatch(panel_themeTBScrollButtonBack, "Text Box Scroll Bar Button Colour", np[nameof(Theme.TextBlockScrollButtonBack)]);
            InitColourPatch(panel_themeTBScrollButtonBack2, "Text Box Scroll Bar Button Colour 2", np[nameof(Theme.TextBlockScrollButtonBack2)]);
            InitColourPatch(panel_themeTBScrollArrow, "Text Box Scroll Bar Arrow Colour", np[nameof(Theme.TextBlockScrollArrow)]);
            InitColourPatch(panel_themeTextBlockDropDownBackColor, "Text Box Drop Down Back Colour", np[nameof(Theme.TextBlockDropDownBackColor)]);
            InitColourPatch(panel_themeTextBlockDropDownBackColor2, "Text Box Drop Down Back Color 2", np[nameof(Theme.TextBlockDropDownBackColor2)]);
            InitGradient(buttonTextBlockButtonGradient, "Text Box Button Gradient Direction in degrees", np[nameof(Theme.TextBlockScrollButtonGradientDirection)]);
            InitGradient(buttonTextBlockSliderGradient, "Text Box Slider Gradient Direction in degrees", np[nameof(Theme.TextBlockSliderGradientDirection)]);
            InitGradient(buttonTextBlockDropdownGradient, "Text Box Dropdown background Gradient Direction in degrees", np[nameof(Theme.TextBlockDropDownBackGradientDirection)]);

            InitColourPatch(panel_themeGridBack, "Grid Border Back Colour", np[nameof(Theme.GridBorderBack)]);
            InitColourPatch(panel_themeGridFore, "Grid Border Text Colour", np[nameof(Theme.GridBorderText)]);
            InitColourPatch(panel_themeGridBorderBorder, "Grid Border Line Colour", np[nameof(Theme.GridBorderLines)]);
            InitColourPatch(panel_themeGridScrollSlider, "Grid Scroll Bar Slider Colour", np[nameof(Theme.GridSliderBack)]);
            InitColourPatch(panel_themeGridScrollSlider2, "Grid Scroll Bar Slider Colour", np[nameof(Theme.GridSliderBack2)]);
            InitColourPatch(panel_themeGridScollArrow, "Grid Scroll Bar Arrow Colour", np[nameof(Theme.GridScrollArrowBack)]);
            InitColourPatch(panel_themeGridScollArrow2, "Grid Scroll Bar Arrow Colour", np[nameof(Theme.GridScrollArrow2Back)]);
            InitColourPatch(panel_themeGridScrollButton, "Grid Scroll Bar Button Colour", np[nameof(Theme.GridScrollButtonBack)]);
            InitColourPatch(panel_themeGridScrollButton2, "Grid Scroll Bar Button Colour 2", np[nameof(Theme.GridScrollButtonBack2)]);
            InitColourPatch(panel_themeGridScrollArrow, "Grid Scroll Bar Arrow Colour", np[nameof(Theme.GridScrollArrow)]);
            InitColourPatch(panel_themeGridHighlightBack, "Grid Highlight Back Colour", np[nameof(Theme.GridHighlightBack)]);
            InitGradient(buttonGridButtonGradient, "Grid Button Gradient Direction in degrees", np[nameof(Theme.GridScrollButtonGradientDirection)]);
            InitGradient(buttonGridSliderGradient, "Grid Slider Gradient Direction in degrees", np[nameof(Theme.GridSliderGradientDirection)]);

            InitColourPatch(panel_themeGridCellBack, "Grid Cell Back Colour", np[nameof(Theme.GridCellBack)]);
            InitColourPatch(panel_themeGridCellFore, "Grid Cell Text Colour", np[nameof(Theme.GridCellText)]);
            InitColourPatch(panel_themeGridAltBack, "Grid Cell Alt Back Colour", np[nameof(Theme.GridCellAltBack)]);
            InitColourPatch(panel_themeGridAltFore, "Grid Cell Alt Text Colour", np[nameof(Theme.GridCellAltText)]);

            InitColourPatch(panel_themeComboBoxBack, "Combo Box Back Colour", np[nameof(Theme.ComboBoxBackColor)]);
            InitColourPatch(panel_themeComboBoxBack2, "Combo Box Back Colour 2", np[nameof(Theme.ComboBoxBackColor2)]);
            InitColourPatch(panel_themeComboBoxFore, "Combo Box Text Colour", np[nameof(Theme.ComboBoxTextColor)]);
            InitColourPatch(panel_themeComboBoxBorder, "Combo Box Border Colour", np[nameof(Theme.ComboBoxBorderColor)]);
            InitColourPatch(panel_themeComboBoxScrollSlider, "Combo Box Scroll Slider", np[nameof(Theme.ComboBoxDropDownSliderBack)]);
            InitColourPatch(panel_themeComboBoxScrollSlider2, "Combo Box Scroll Slider 2", np[nameof(Theme.ComboBoxDropDownSliderBack2)]);
            InitColourPatch(panel_themeComboBoxScrollButtonBack, "Combo Box Scroll Button Back", np[nameof(Theme.ComboBoxScrollButtonBack)]);
            InitColourPatch(panel_themeComboBoxScrollButtonBack2, "Combo Box Scroll Button Back 2", np[nameof(Theme.ComboBoxScrollButtonBack2)]);
            InitColourPatch(panel_themeComboBoxScrollArrowBack, "Combo Box Scroll Arrow Back", np[nameof(Theme.ComboBoxScrollArrowBack)]);
            InitColourPatch(panel_themeComboBoxScrollArrowBack2, "Combo Box Scroll Arrow Back 2", np[nameof(Theme.ComboBoxScrollArrowBack2)]);
            InitColourPatch(panel_themeComboBoxScrollArrow, "Combo Box Scroll Arrow", np[nameof(Theme.ComboBoxScrollArrow)]);
            InitGradient(buttonComboBoxBackGradient, "Combo Box Back Gradient Direction in degrees", np[nameof(Theme.ComboBoxBackAndDropDownGradientDirection)]);
            InitGradient(buttonComboBoxSliderGradient, "Combo Box Slider Gradient Direction in degrees", np[nameof(Theme.ComboBoxDropDownSliderGradientDirection)]);
            InitGradient(buttonComboBoxButtonGradient, "Combo Box Slider Button Gradient Direction in degrees", np[nameof(Theme.ComboBoxDropDownScrollButtonGradientDirection)]);


            InitColourPatch(panel_themeListBoxBack, "List Box Back Colour", np[nameof(Theme.ListBoxBackColor)]);
            InitColourPatch(panel_themeListBoxBack2, "List Box Back Colour 2", np[nameof(Theme.ListBoxBackColor2)]);
            InitColourPatch(panel_themeListBoxFore, "List Box Text Colour", np[nameof(Theme.ListBoxTextColor)]);
            InitColourPatch(panel_themeListBoxBorder, "List Box Border Colour", np[nameof(Theme.ListBoxBorderColor)]);
            InitColourPatch(panel_themeListBoxScrollSlider, "List Box Scroll Slider", np[nameof(Theme.ListBoxSliderBack)]);
            InitColourPatch(panel_themeListBoxScrollSlider2, "List Box Scroll Slider 2", np[nameof(Theme.ListBoxSliderBack2)]);
            InitColourPatch(panel_themeListBoxScrollButton, "List Box Scroll Button", np[nameof(Theme.ListBoxScrollButtonBack)]);
            InitColourPatch(panel_themeListBoxScrollButton2, "List Box Scroll Button 2", np[nameof(Theme.ListBoxScrollButtonBack2)]);
            InitColourPatch(panel_themeListBoxScrollArrowBack, "List Box Scroll Arrow", np[nameof(Theme.ListBoxScrollArrowBack)]);
            InitColourPatch(panel_themeListBoxScrollArrowBack2, "List Box Scroll Arrow 2", np[nameof(Theme.ListBoxScrollArrowBack2)]);
            InitColourPatch(panel_themeListBoxScrollArrow, "List Box Scroll Arrow", np[nameof(Theme.ListBoxScrollArrow)]);
            InitGradient(buttonListBoxBackGradient, "List Box Back Gradient Direction in degrees", np[nameof(Theme.ListBoxBackGradientDirection)]);
            InitGradient(buttonListBoxSliderGradient, "List Box Slider Gradient Direction in degrees", np[nameof(Theme.ListBoxSliderGradientDirection)]);
            InitGradient(buttonListBoxButtonGradient, "List Box Slider Button Gradient Direction in degrees", np[nameof(Theme.ListBoxScrollButtonGradientDirection)]);

            InitColourPatch(panel_themeButtonBack, "Button Back Colour", np[nameof(Theme.ButtonBackColor)]);
            InitColourPatch(panel_themeButtonBack2, "Button Back Colour 2", np[nameof(Theme.ButtonBackColor2)]);
            InitColourPatch(panel_themeButtonFore, "Button Text Colour", np[nameof(Theme.ButtonTextColor)]);
            InitColourPatch(panel_themeButtonBorder, "Button Border Colour", np[nameof(Theme.ButtonBorderColor)]);
            InitGradient(buttonButtonBackGradient, "Button Back Gradient Direction in degrees", np[nameof(Theme.ButtonBackGradientDirection)]);

            InitColourPatch(panel_themeCheckBoxBack, "Check Box Back Colour", np[nameof(Theme.CheckBoxBack)]);
            InitColourPatch(panel_themeCheckBoxBack2, "Check Box Back Colour 2", np[nameof(Theme.CheckBoxBack2)]);
            InitColourPatch(panel_themeCheckBoxText, "Check Box Text Colour", np[nameof(Theme.CheckBoxText)]);
            InitColourPatch(panel_themeCheckBoxTick, "Check Box Tick Colour", np[nameof(Theme.CheckBoxTick)]);
            InitColourPatch(panel_themeCheckBoxButtonApp, "Check Button Syle Back Colour when Ticked", np[nameof(Theme.CheckBoxButtonTickedBack)]);
            InitColourPatch(panel_themeCheckBoxButtonApp2, "Check Button Syle Back Colour 2 when Ticked", np[nameof(Theme.CheckBoxButtonTickedBack2)]);
            InitColourPatch(panel_themeCheckBoxBorder, "Check Button Syle Border Colour", np[nameof(Theme.CheckBoxBorderColor)]);
            InitGradient(buttonCheckBoxBackGradient, "Check Box Gradient Direction in degrees", np[nameof(Theme.CheckBoxBackGradientDirection)]);
            InitGradient(buttonCheckBoxButtonGradient, "Check Box Button Checked Gradient Direction in degrees", np[nameof(Theme.CheckBoxButtonGradientDirection)]);
            InitFloat(numericUpDownCBTickStyleTickSize, "Check Box Tick Size as a proportion of the check box", np[nameof(Theme.CheckBoxTickSize)]);

            InitColourPatch(panel_themeGroupBack1, "Group box Back 1 Colour", np[nameof(Theme.GroupBack)], 0);
            InitColourPatch(panel_themeGroupBack2, "Group box Back 2 Colour", np[nameof(Theme.GroupBack)], 1);
            InitColourPatch(panel_themeGroupBack3, "Group box Back 3 Colour", np[nameof(Theme.GroupBack)], 2);
            InitColourPatch(panel_themeGroupBack4, "Group box Back 4 Colour", np[nameof(Theme.GroupBack)], 3);
            InitColourPatch(panel_themeGBFore, "Group box Text Colour", np[nameof(Theme.GroupFore)]);
            InitColourPatch(panel_themeGBBorder, "Group box Border Line Colour", np[nameof(Theme.GroupBorder)]);
            InitColourPatch(panel_themeGBBorder2, "Group box Border Line Colour 2", np[nameof(Theme.GroupBorder2)]);
            InitGradient(buttonGBGradient, "Group Box Gradient Direction in degrees", np[nameof(Theme.GroupBoxGradientDirection)]);

            InitColourPatch(panel_themeTabStripBack1, "Tab Strip 1 Back Colour", np[nameof(Theme.TabStripBack)], 0);
            InitColourPatch(panel_themeTabStripBack2, "Tab Strip 2 Back Colour", np[nameof(Theme.TabStripBack)], 1);
            InitColourPatch(panel_themeTabStripBack3, "Tab Strip 3 Back Colour", np[nameof(Theme.TabStripBack)], 2);
            InitColourPatch(panel_themeTabStripBack4, "Tab Strip 4 Back Colour", np[nameof(Theme.TabStripBack)], 3);
            InitColourPatch(panel_themeTabStripFore, "Tab Strip Text Colour", np[nameof(Theme.TabStripFore)]);
            InitColourPatch(panel_themeTabStripSelected, "Tab Strip Selected Colour", np[nameof(Theme.TabStripSelected)]);
            InitGradient(buttonTabStripGradient, "Tab Strip Gradient Direction in degrees", np[nameof(Theme.TabStripGradientDirection)]);

            InitColourPatch(panel_themeTabControlBack1, "Tab Control 1 Back Colour", np[nameof(Theme.TabControlBack)], 0);
            InitColourPatch(panel_themeTabControlBack2, "Tab Control 2 Back Colour", np[nameof(Theme.TabControlBack)], 1);
            InitColourPatch(panel_themeTabControlBack3, "Tab Control 3 Back Colour", np[nameof(Theme.TabControlBack)], 2);
            InitColourPatch(panel_themeTabControlBack4, "Tab Control 4 Back Colour", np[nameof(Theme.TabControlBack)], 3);
            InitGradient(buttonTabControlTabGradient, "Tab Control Tab Gradient Direction in degrees", np[nameof(Theme.TabControlTabGradientDirection)]);

            InitColourPatch(panel_themeTabControlFore, "Tab Control Text Colour", np[nameof(Theme.TabControlText)]);
            InitColourPatch(panel_themeTabControlButtonBack, "Tab Control Button Back Colour", np[nameof(Theme.TabControlButtonBack)]);
            InitColourPatch(panel_themeTabControlButtonBack2, "Tab Control Button Back Colour 2", np[nameof(Theme.TabControlButtonBack2)]);
            InitColourPatch(panel_themeTabControlBorder, "Tab Control Border Line Colour", np[nameof(Theme.TabControlBorder)]);
            InitColourPatch(panel_themeTabControlBorder2, "Tab Control Border Line Colour 2", np[nameof(Theme.TabControlBorder2)]);
            InitColourPatch(panel_themeTabControlPageBack, "Tab Control Page Back Colour", np[nameof(Theme.TabControlPageBack)]);
            InitGradient(buttonTabControlBackGradient, "Tab Control Background Gradient Direction in degrees", np[nameof(Theme.TabControlBackGradientDirection)]);
            InitGradient(buttonTabControlTabGradient, "Tab Control Tab Gradient Direction in degrees", np[nameof(Theme.TabControlTabGradientDirection)]);

            InitColourPatch(panel_themeUnknown, "Visited system without known position", np[nameof(Theme.KnownSystemColor)]);
            InitColourPatch(panel_themeKnown, "Visited system with coordinates", np[nameof(Theme.UnknownSystemColor)]);

            InitColourPatch(panel_themeMenuBack, "Menu Back Colour", np[nameof(Theme.MenuBack)]);
            InitColourPatch(panel_themeMenuFore, "Menu Text Colour", np[nameof(Theme.MenuFore)]);
            InitColourPatch(panel_themeMenuDropDownBack, "Tool Strip Back Colour", np[nameof(Theme.MenuDropDownBack)]);
            InitColourPatch(panel_themeMenuBorder, "Tool Strip Border Colour", np[nameof(Theme.MenuBorder)]);
            InitColourPatch(panel_themeMenuSelectedBack, "Menu Dropdown Back Colour", np[nameof(Theme.MenuSelectedBack)]);
            InitColourPatch(panel_themeMenuSelectedText, "Menu Dropdown Text Colour", np[nameof(Theme.MenuSelectedText)]);

            InitColourPatch(panel_themePanel11, "Panel 1 Back Colour", np[nameof(Theme.Panel1)], 0);
            InitColourPatch(panel_themePanel12, "Panel 1 Back 2 Colour", np[nameof(Theme.Panel1)], 1);
            InitColourPatch(panel_themePanel13, "Panel 1 Back 3 Colour", np[nameof(Theme.Panel1)], 2);
            InitColourPatch(panel_themePanel14, "Panel 1 Back 4 Colour", np[nameof(Theme.Panel1)], 3);
            InitGradient(buttonPanelGradient1, "Panel 1 Gradient Direction in degrees", np[nameof(Theme.PanelGradientDirection)], 0);
            InitColourPatch(panel_themePanel21, "Panel 2 Back Colour", np[nameof(Theme.Panel2)], 0);
            InitColourPatch(panel_themePanel22, "Panel 2 Back 2 Colour", np[nameof(Theme.Panel2)], 1);
            InitColourPatch(panel_themePanel23, "Panel 2 Back 3 Colour", np[nameof(Theme.Panel2)], 2);
            InitColourPatch(panel_themePanel24, "Panel 2 Back 4 Colour", np[nameof(Theme.Panel2)], 3);
            InitGradient(buttonPanelGradient2, "Panel 2 Gradient Direction in degrees", np[nameof(Theme.PanelGradientDirection)], 1);
            InitColourPatch(panel_themePanel31, "Panel 3 Back Colour", np[nameof(Theme.Panel3)], 0);
            InitColourPatch(panel_themePanel32, "Panel 3 Back 2 Colour", np[nameof(Theme.Panel3)], 1);
            InitColourPatch(panel_themePanel33, "Panel 3 Back 3 Colour", np[nameof(Theme.Panel3)], 2);
            InitColourPatch(panel_themePanel34, "Panel 3 Back 4 Colour", np[nameof(Theme.Panel3)], 3);
            InitGradient(buttonPanelGradient3, "Panel 3 Gradient Direction in degrees", np[nameof(Theme.PanelGradientDirection)], 2);
            InitColourPatch(panel_themePanel41, "Panel 4 Back Colour", np[nameof(Theme.Panel4)], 0);
            InitColourPatch(panel_themePanel42, "Panel 4 Back 2 Colour", np[nameof(Theme.Panel4)], 1);
            InitColourPatch(panel_themePanel43, "Panel 4 Back 3 Colour", np[nameof(Theme.Panel4)], 2);
            InitColourPatch(panel_themePanel44, "Panel 4 Back 4 Colour", np[nameof(Theme.Panel4)], 3);
            InitGradient(buttonPanelGradient4, "Panel 4 Gradient Direction in degrees", np[nameof(Theme.PanelGradientDirection)], 3);

            InitColourPatch(panel_chart1, "Chart Series 1", np[nameof(Theme.Chart1)]);
            InitColourPatch(panel_chart2, "Chart Series 2", np[nameof(Theme.Chart2)]);
            InitColourPatch(panel_chart3, "Chart Series 3", np[nameof(Theme.Chart3)]);
            InitColourPatch(panel_chart4, "Chart Series 4", np[nameof(Theme.Chart4)]);
            InitColourPatch(panel_chart5, "Chart Series 5", np[nameof(Theme.Chart5)]);
            InitColourPatch(panel_chart6, "Chart Series 6", np[nameof(Theme.Chart6)]);
            InitColourPatch(panel_chart7, "Chart Series 7", np[nameof(Theme.Chart7)]);
            InitColourPatch(panel_chart8, "Chart Series 8", np[nameof(Theme.Chart8)]);

            comboBox_TextBorder.SelectedItem = Theme.TextBoxBorderStyle;
            comboBox_ButtonStyle.SelectedItem = Theme.ButtonStyle;

            InitColourPatch(panel_themeSPanel, "S-Panel Text Colour", np[nameof(Theme.SPanelColor)]);
            InitColourPatch(panel_themeLabel, "Label Text Colour", np[nameof(Theme.LabelColor)]);

            checkBox_theme_windowframe.Checked = Environment.OSVersion.Platform != PlatformID.Win32NT ? false : Theme.WindowsFrame;
            UpdateFontText();
            trackBar_theme_opacity.Value = (int)Theme.Opacity;
            InitColourPatch(panel_themeTransparentColourKey, "Transparent Colour Key", np[nameof(Theme.TransparentColorKey)]);

            numericUpDownDialogFontScaling.Value = (decimal)Theme.DialogFontScaling;
            numericUpDownMouseOverScaling.Value = (decimal)Theme.MouseOverScaling;
            numericUpDownMouseSelectedScaling.Value = (decimal)Theme.MouseSelectedScaling;
            numericUpDownDisabledScaling.Value = (decimal)Theme.DisabledScaling;

            this.checkBox_theme_windowframe.CheckedChanged += new System.EventHandler(this.checkBox_theme_windowframe_CheckedChanged);
            this.comboBoxSkinnyStyle.SelectedIndex = Theme.SkinnyScrollBars ? (Theme.SkinnyScrollBarsHaveButtons ? 2 : 1) : 0;
            this.comboBoxSkinnyStyle.SelectedIndexChanged += ComboBoxSkinnyStyle_SelectedIndexChanged;
            this.trackBar_theme_opacity.ValueChanged += new System.EventHandler(this.trackBar_theme_opacity_ValueChanged);
            this.numericUpDownDisabledScaling.ValueChanged += new System.EventHandler(this.numericUpDownDisabledScaling_ValueChanged);
            this.numericUpDownMouseSelectedScaling.ValueChanged += new System.EventHandler(this.numericUpDownMouseSelectedScaling_ValueChanged);
            this.numericUpDownMouseOverScaling.ValueChanged += new System.EventHandler(this.numericUpDownMouseOverScaling_ValueChanged);
            this.numericUpDownDialogFontScaling.ValueChanged += new System.EventHandler(this.numericUpDownDialogFontScaling_ValueChanged);
        }

        // all call this to change 
        private void Apply()
        {
            Theme.SetCustom();
            if (checkBoxApplyOnEachChange.Checked)
            {
                ApplyChanges?.Invoke(Theme);
                pendingchanges = false;
            }
            else
                pendingchanges = true;
        }


        // Colour patch, can handle indexes.
        private void InitColourPatch(Panel pn, string tooltip, PropertyInfo pi, int index = 0)
        {
            toolTip1.SetToolTip(pn, tooltip);        // assign tool tips and indicate which color to edit
            Array a = null;
            if (pi.PropertyType.IsArray)
            {
                pn.Tag = new Tuple<PropertyInfo, int>(pi, index);
                a = (Array)pi.GetValue(Theme);
                pn.BackColor = (Color)a.GetValue(index);
            }
            else
            {
                pn.Tag = pi;
                pn.BackColor = (Color)pi.GetValue(Theme);
            }

            pn.MouseClick += (s, e) =>
            {
                Color c;
                if (pi.PropertyType.IsArray)
                {
                    c = (Color)a.GetValue(index);
                }
                else
                    c = (Color)pi.GetValue(Theme);

                ColorDialog MyDialog = new ColorDialog();
                MyDialog.AllowFullOpen = true;
                MyDialog.FullOpen = true;
                MyDialog.Color = c;

                if (MyDialog.ShowDialog(this) == DialogResult.OK)
                {
                    //System.Diagnostics.Debug.Write($"Theme {pi.Name} with {c}");
                    if (pi.PropertyType.IsArray)
                    {
                        a.SetValue(MyDialog.Color, index);
                    }
                    else
                        pi.SetValue(Theme, MyDialog.Color);
                    pn.BackColor = MyDialog.Color;
                    Apply();
                }

            };
            pn.ContextMenuStrip = contextMenuStripColours;
            //System.Diagnostics.Debug.WriteLine($"Panel {pn.Name} to {pn.BackColor}");
        }

        private void InitFloat(NumericUpDown c, string tooltip, PropertyInfo pi)
        {
            toolTip1.SetToolTip(c, tooltip);        // assign tool tips and indicate which color to edit
            c.Tag = pi;
            c.Value = (decimal)(float)pi.GetValue(Theme);
            c.ValueChanged += (s, e) => {
                decimal value = c.Value;
                pi.SetValue(Theme, (float)value);
                Apply();
            };
        }

        // Gradient, can handle indexes.
        private void InitGradient( ButtonAngle c, string tooltip, PropertyInfo pi, int index = 0)
        {
            toolTip1.SetToolTip(c, tooltip);        // assign tool tips and indicate which color to edit
            Array a = null;

            if (pi.PropertyType.IsArray)
            {
                a = (Array)pi.GetValue(Theme);
                c.Value = (float)a.GetValue(index);
            }
            else
            {
                c.Value = (float)pi.GetValue(Theme);
            }

            c.Click += (s, e) =>
            {
                Form f = new Form();
                f.BackColor = Color.Beige;
                f.FormBorderStyle = FormBorderStyle.None;
                f.Size = new Size(190, 28);
                f.StartPosition = FormStartPosition.Manual;
                f.Location = this.PointToScreen(new Point(c.Right + 8, c.Top + c.Height / 2 - 4));

                NumericUpDown nup = new NumericUpDown();
                nup.Size = new Size(120, 24);
                nup.Location = new Point(8, 4);
                nup.Maximum = 360;
                nup.Increment = 10;
                if (pi.PropertyType.IsArray)
                    nup.Value = (decimal)(float)a.GetValue(index);
                else
                    nup.Value = (decimal)c.Value;
                nup.KeyDown += (s2, e2) => { if (e2.KeyCode == Keys.Return) { f.DialogResult = DialogResult.OK; f.Close(); } };
                nup.ValueChanged += (s3, e3) => {
                    c.Value = (float)nup.Value;
                    if (pi.PropertyType.IsArray)
                        a.SetValue(c.Value, index);
                    else
                        pi.SetValue(Theme, c.Value);
                    Apply();
                };
                f.Controls.Add(nup);
                Button ok = new Button();
                ok.DialogResult = DialogResult.OK;
                ok.Size = new Size(40, 24);
                ok.Location = new Point(140, 4);
                ok.Text = "OK";
                f.Shown += (s1, e1) => { nup.Focus(); };
                f.Controls.Add(ok);
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    c.Value = (float)nup.Value;
                    if (pi.PropertyType.IsArray)
                        a.SetValue(c.Value, index);
                    else
                        pi.SetValue(Theme, c.Value);
                    Apply();
                }
                ;
            };
        }

        private void checkBox_theme_windowframe_CheckedChanged(object sender, EventArgs e)
        {
            Theme.WindowsFrame = checkBox_theme_windowframe.Checked;
            Apply();
        }

        private void ComboBoxSkinnyStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Theme.SkinnyScrollBars = comboBoxSkinnyStyle.SelectedIndex > 0;
            Theme.SkinnyScrollBarsHaveButtons = comboBoxSkinnyStyle.SelectedIndex == 2;
            Apply();
        }

        private void trackBar_theme_opacity_ValueChanged(object sender, EventArgs e)
        {
            Theme.Opacity = (double)trackBar_theme_opacity.Value;
            Apply();
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
                    Apply();
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
            Apply();
        }

        private void comboBox_ButtonStyle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Theme.ButtonStyle = (string)comboBox_ButtonStyle.SelectedItem;
            Apply();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (pendingchanges)
                checkBoxApplyOnEachChange.Checked = true;

            DialogResult = DialogResult.OK;
            Theme.ToolStripRendererTheming(true);      // turn back on
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Theme.ToolStripRendererTheming(true);      // turn back on
            Close();
        }

        private void checkBoxApplyOnEachChange_CheckedChanged(object sender, EventArgs e)
        {
            if (pendingchanges && checkBoxApplyOnEachChange.Checked)      // if pending changes, and we have gone true, apply
                Apply();
        }

        private void checkBoxDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLabelColors(this, checkBoxDarkMode.Checked ? Color.DarkOrange : Color.Black);
            if (checkBoxDarkMode.Checked)
                this.BackColor = Color.Black;
            else
                this.BackColor = SystemColors.Control;
        }

        Color? pastecolour = null;
        private void copyColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control s = contextMenuStripColours.SourceControl;
            pastecolour = s.BackColor;
        }

        private void pasteColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control s = contextMenuStripColours.SourceControl;
            PropertyInfo pi = s.Tag as PropertyInfo;

            if (pi != null)                                             
            {   
                pi.SetValue(Theme, pastecolour.Value);                  
            }
            else
            {
                var tu = (Tuple<PropertyInfo, int>)s.Tag;
                Array a = (Array)tu.Item1.GetValue(Theme);
                a.SetValue(pastecolour.Value, tu.Item2);
            }

            s.BackColor = pastecolour.Value;
            Apply();
        }

        private void contextMenuStripColours_Opening(object sender, CancelEventArgs e)
        {
            Theme.ToolStripRendererTheming(false);      // turn off so we get a normal right click menu
            pasteColourToolStripMenuItem.Enabled = pastecolour != null;

        }
        private void contextMenuStripColours_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            Theme.ToolStripRendererTheming(true);      // turn back on so we get normal theming
        }

        private void numericUpDownDialogFontScaling_ValueChanged(object sender, EventArgs e)
        {
            Theme.DialogFontScaling = (float)numericUpDownDisabledScaling.Value;
            Apply();
        }

        private void numericUpDownMouseOverScaling_ValueChanged(object sender, EventArgs e)
        {
            Theme.MouseOverScaling = (float)numericUpDownMouseOverScaling.Value;
            Apply();
        }

        private void numericUpDownMouseSelectedScaling_ValueChanged(object sender, EventArgs e)
        {
            Theme.MouseSelectedScaling = (float)numericUpDownMouseSelectedScaling.Value;
            Apply();
        }

        private void numericUpDownDisabledScaling_ValueChanged(object sender, EventArgs e)
        {
            Theme.DisabledScaling = (float)numericUpDownDisabledScaling.Value;
            Apply();
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

    public class ButtonAngle : Button
    {
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)] 
        public Color PointerColor { get; set; } = Color.Blue;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)] 
        public float PointerLengthPercent { get; set; } = 80;
        
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public float Value { get => degreescossin; set { degreescossin = value; Invalidate(); } }         // cos/sin, 0 = right, as per normal, 90 = north, 270 = south
        
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public float CompassValue { get => (90F - degreescossin + 360F) % 360F; set { degreescossin = (90 -value  + 360F) % 360F; Invalidate(); } }         // compass angles, 0 = north, 90 = east, etc

        private float degreescossin = 0;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            using (Pen p = new Pen(PointerColor))
            {
                int centrex = ClientRectangle.Width / 2;
                int centrey = ClientRectangle.Height / 2;
                float pointerlength = ClientRectangle.Width / 2  * PointerLengthPercent / 100f;
                int offsetx = (int)(Math.Cos(Value.Radians()) * pointerlength);
                int offsety = (int)(Math.Sin(Value.Radians()) * pointerlength);
                pevent.Graphics.DrawLine(p, new Point(centrex,centrey), new Point(centrex+offsetx,centrey-offsety));
            }
        }

    }
}
