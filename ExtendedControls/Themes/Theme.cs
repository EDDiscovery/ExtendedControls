/*
 * Copyright © 2016-2025 EDDiscovery development team
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

using QuickJSON;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExtendedControls
{
    [System.Diagnostics.DebuggerDisplay("{Name} {FontName} {FontSize} {WindowsFrame}")]
    public class Theme
    {
        public static Theme Current { get; set; } = null;             // current theme

        public string Name { get { return name; } set { name = value; } }

        public Color Form { get { return colors[CI.form]; } }       // in enum order..
        public Color ButtonBackColor { get { return colors[CI.button_back]; } }
        public Color ButtonBorderColor { get { return colors[CI.button_border]; } }
        public Color ButtonTextColor { get { return colors[CI.button_text]; } }

        public Color GridBorderBack { get { return colors[CI.grid_borderback]; } }
        public Color GridBorderText { get { return colors[CI.grid_bordertext]; } }
        public Color GridCellBack { get { return colors[CI.grid_cellbackground]; } }
        public Color GridCellAltBack { get { return colors[CI.grid_altcellbackground]; } }
        public Color GridCellText { get { return colors[CI.grid_celltext]; } }
        public Color GridCellAltText { get { return colors[CI.grid_altcelltext]; } }
        public Color GridBorderLines { get { return colors[CI.grid_borderlines]; } }
        public Color GridSliderBack { get { return colors[CI.grid_sliderback]; } }
        public Color GridScrollArrow { get { return colors[CI.grid_scrollarrow]; } }
        public Color GridScrollButton { get { return colors[CI.grid_scrollbutton]; } }

        public Color KnownSystemColor { get { return colors[CI.travelgrid_visited]; } }
        public Color UnknownSystemColor { get { return colors[CI.travelgrid_nonvisted]; } }

        public Color TextBackColor { get { return colors[CI.textbox_back]; } }
        public Color TextBlockColor { get { return colors[CI.textbox_fore]; } }
        public Color TextBlockHighlightColor { get { return colors[CI.textbox_highlight]; } }
        public Color TextBlockSuccessColor { get { return colors[CI.textbox_success]; } }
        public Color TextBlockBorderColor { get { return colors[CI.textbox_border]; } }
        public Color TextBlockSliderBack { get { return colors[CI.textbox_sliderback]; } }
        public Color TextBlockScrollArrow { get { return colors[CI.textbox_scrollarrow]; } }
        public Color TextBlockScrollButton { get { return colors[CI.textbox_scrollbutton]; } }

        public Color CheckBox { get { return colors[CI.checkbox]; } }
        public Color CheckBoxTick { get { return colors[CI.checkbox_tick]; } }

        public Color MenuBack { get { return colors[CI.menu_back]; } }
        public Color MenuFore { get { return colors[CI.menu_fore]; } }
        public Color MenuDropdownBack { get { return colors[CI.menu_dropdownback]; } }
        public Color MenuDropdownFore { get { return colors[CI.menu_dropdownfore]; } }

        public Color LabelColor { get { return colors[CI.label]; } }

        public Color GroupBack { get { return colors[CI.group_back]; } }
        public Color GroupFore { get { return colors[CI.group_text]; } }
        public Color GroupBorder { get { return colors[CI.group_borderlines]; } }

        public Color TabcontrolBorder { get { return colors[CI.tabcontrol_borderlines]; } }

        public Color ToolstripBack { get { return colors[CI.toolstrip_back]; } }
        public Color ToolstripBorder { get { return colors[CI.toolstrip_border]; } }

        public Color SPanelColor { get { return colors[CI.s_panel]; } }
        public Color TransparentColorKey { get { return colors[CI.transparentcolorkey]; } }
        public Color GridHighlightBack { get { return colors[CI.grid_highlightback]; } }

        public bool WindowsFrame { get { return windowsframe; } set { windowsframe = value; } }
        public double Opacity { get { return formopacity; } set { formopacity = value; } }
        public string FontName { get { return fontname; } set { fontname = value; } }
        public float FontSize { get { return fontsize; } set { fontsize = value; } }
        public string ButtonStyle { get { return buttonstyle; } set { buttonstyle = value; } }
        public string TextBoxBorderStyle { get { return textboxborderstyle; } set { textboxborderstyle = value; } }
        public Size IconSize { get { var ft = GetFont; return new Size(ft.ScalePixels(36), ft.ScalePixels(36)); } } // calculated rep scaled icon size to use

        public static readonly string[] ButtonStyles = "System Flat Gradient".Split();
        public static readonly string[] TextboxBorderStyles = "None FixedSingle Fixed3D Colour".Split();

        public static string ButtonstyleSystem = ButtonStyles[0];
        public static string ButtonstyleFlat = ButtonStyles[1];
        public static string ButtonstyleGradient = ButtonStyles[2];
        public static string TextboxborderstyleFixedSingle = TextboxBorderStyles[1];
        public static string TextboxborderstyleFixed3D = TextboxBorderStyles[2];
        public static string TextboxborderstyleColor = TextboxBorderStyles[3];

        public static string DefaultFont = "Microsoft Sans Serif";
        public static float DefaultFontSize = 8.25F;

        public static Color[] DefaultChartColours = new Color[] { Color.Green, Color.Red, Color.Blue, Color.Orange, Color.Purple, Color.Aqua, Color.Violet, Color.Brown };

        // use TabIndex to also indicate no theme by setting tab index to this. Added so base winform controls can disable themeing
        public const int TabIndexNoThemeIndicator = 9090;


        public enum CI
        {
            form,

            button_back, button_text, button_border,

            grid_borderback, grid_bordertext,
            grid_cellbackground, grid_altcellbackground, grid_celltext, grid_altcelltext,
            grid_borderlines,
            grid_sliderback, grid_scrollarrow, grid_scrollbutton,

            travelgrid_nonvisted, travelgrid_visited,

            textbox_back, textbox_fore, textbox_highlight, textbox_success, textbox_border,
            textbox_sliderback, textbox_scrollarrow, textbox_scrollbutton,

            checkbox, checkbox_tick,

            menu_back, menu_fore, menu_dropdownback, menu_dropdownfore,

            label,

            group_back, group_text, group_borderlines,

            tabcontrol_borderlines,

            toolstrip_back, toolstrip_border,

            unused_entry,     // previously assigned to toolstrip_checkbox thing

            s_panel, transparentcolorkey, grid_highlightback,

            chart1, chart2, chart3, chart4, chart5, chart6, chart7, chart8,
        };
        private string name { get; set; }         // name of scheme
        private Dictionary<CI, Color> colors { get; set; }       // dictionary of colors, indexed by CI.
        private bool windowsframe { get; set; }
        private double formopacity { get; set; }
        private string fontname { get; set; }         // Font.. (empty means don't override)
        private float fontsize { get; set; }
        private string buttonstyle { get; set; }
        private string textboxborderstyle { get; set; }

        private static float minfontsize = 4;

        public Theme()
        {
            colors = new Dictionary<CI, Color>();
            foreach (CI ci in Enum.GetValues(typeof(CI)))      // pre fill in the array, so its always has all colours, so the getters won't except
                colors[ci] = Color.DarkOrange;
        }

        public Theme(String n, Color f,
                                    Color butback, Color buttext, Color butborder, string bstyle,
                                    Color gridborderback, Color gridbordertext,
                                    Color gridcellback, Color gridaltcellback, Color gridcelltext, Color gridaltcelltext, Color gridhighlightback,
                                    Color gridborderlines,
                                    Color gridsliderback, Color gridscrollarrow, Color gridscrollbutton,
                                    Color travelgridnonvisited, Color travelgridvisited,
                                    Color textboxback, Color textboxfore, Color textboxhighlight, Color textboxsuccess, Color textboxborder, string tbbstyle,
                                    Color textboxsliderback, Color textboxscrollarrow, Color textboxscrollbutton,
                                    Color checkboxfore, Color checkboxtick,
                                    Color menuback, Color menufore, Color menudropbackback, Color menudropdownfore,
                                    Color label,
                                    Color groupboxback, Color groupboxtext, Color groupboxlines,
                                    Color tabborderlines,
                                    Color toolstripback, Color toolstripborder, Color toolstripbuttonunused,
                                    Color sPanel, Color keycolor,
                                    Color[] chartcolours,
                                    bool wf, double op, string ft, float fs)             // ft = empty means don't set it
        {
            Name = n;
            colors = new Dictionary<CI, Color>();
            colors.Add(CI.form, f);

            colors.Add(CI.button_back, butback); colors.Add(CI.button_text, buttext); colors.Add(CI.button_border, butborder);

            colors.Add(CI.grid_borderback, gridborderback); colors.Add(CI.grid_bordertext, gridbordertext);
            colors.Add(CI.grid_cellbackground, gridcellback); colors.Add(CI.grid_altcellbackground, gridaltcellback);
            colors.Add(CI.grid_celltext, gridcelltext); colors.Add(CI.grid_altcelltext, gridaltcelltext); colors.Add(CI.grid_highlightback, gridhighlightback);
            colors.Add(CI.grid_borderlines, gridborderlines);
            colors.Add(CI.grid_sliderback, gridsliderback); colors.Add(CI.grid_scrollarrow, gridscrollarrow); colors.Add(CI.grid_scrollbutton, gridscrollbutton);

            colors.Add(CI.travelgrid_nonvisted, travelgridnonvisited); colors.Add(CI.travelgrid_visited, travelgridvisited);

            colors.Add(CI.textbox_back, textboxback); colors.Add(CI.textbox_fore, textboxfore);
            colors.Add(CI.textbox_highlight, textboxhighlight); colors.Add(CI.textbox_success, textboxsuccess); colors.Add(CI.textbox_border, textboxborder);
            colors.Add(CI.textbox_sliderback, textboxsliderback); colors.Add(CI.textbox_scrollarrow, textboxscrollarrow); colors.Add(CI.textbox_scrollbutton, textboxscrollbutton);

            colors.Add(CI.checkbox, checkboxfore); colors.Add(CI.checkbox_tick, checkboxtick);

            colors.Add(CI.menu_back, menuback); colors.Add(CI.menu_fore, menufore); colors.Add(CI.menu_dropdownback, menudropbackback); colors.Add(CI.menu_dropdownfore, menudropdownfore);

            colors.Add(CI.label, label);

            colors.Add(CI.group_back, groupboxback); colors.Add(CI.group_text, groupboxtext); colors.Add(CI.group_borderlines, groupboxlines);
            colors.Add(CI.tabcontrol_borderlines, tabborderlines);
            colors.Add(CI.toolstrip_back, toolstripback); colors.Add(CI.toolstrip_border, toolstripborder); colors.Add(CI.unused_entry, toolstripbuttonunused);
            colors.Add(CI.s_panel, sPanel);
            colors.Add(CI.transparentcolorkey, keycolor);

            for (int i = 0; i < DefaultChartColours.Length; i++)        // first ensure all is filled
                colors.Add(CI.chart1 + i, DefaultChartColours[i]);
            for (int i = 0; i < chartcolours.Length; i++)               // then fill what is defined
                colors[CI.chart1 + i] = chartcolours[i];

            buttonstyle = bstyle; textboxborderstyle = tbbstyle;
            windowsframe = Environment.OSVersion.Platform == PlatformID.Win32NT ? wf : true;
            formopacity = op; fontname = ft; fontsize = fs;
        }

        public Theme(string n)                                               // gets you windows default colours
        {
            Name = n;
            colors = new Dictionary<CI, Color>();
            colors.Add(CI.form, SystemColors.Menu);
            colors.Add(CI.button_back, Color.FromArgb(255, 225, 225, 225)); colors.Add(CI.button_text, SystemColors.ControlText); colors.Add(CI.button_border, SystemColors.ActiveBorder);
            colors.Add(CI.grid_borderback, SystemColors.Menu); colors.Add(CI.grid_bordertext, SystemColors.MenuText);
            colors.Add(CI.grid_cellbackground, SystemColors.ControlLightLight); colors.Add(CI.grid_altcellbackground, SystemColors.ControlLightLight);
            colors.Add(CI.grid_celltext, SystemColors.MenuText); colors.Add(CI.grid_altcelltext, SystemColors.MenuText); colors.Add(CI.grid_highlightback, Color.LightGreen);
            colors.Add(CI.grid_borderlines, SystemColors.ControlDark);
            colors.Add(CI.grid_sliderback, SystemColors.ControlLight); colors.Add(CI.grid_scrollarrow, SystemColors.MenuText); colors.Add(CI.grid_scrollbutton, SystemColors.Control);
            colors.Add(CI.travelgrid_nonvisted, Color.Blue); colors.Add(CI.travelgrid_visited, SystemColors.MenuText);
            colors.Add(CI.textbox_back, SystemColors.Window); colors.Add(CI.textbox_fore, SystemColors.WindowText); colors.Add(CI.textbox_highlight, Color.Red); colors.Add(CI.textbox_success, Color.Green); colors.Add(CI.textbox_border, SystemColors.Menu);
            colors.Add(CI.textbox_sliderback, SystemColors.ControlLight); colors.Add(CI.textbox_scrollarrow, SystemColors.MenuText); colors.Add(CI.textbox_scrollbutton, SystemColors.Control);
            colors.Add(CI.checkbox, SystemColors.MenuText); colors.Add(CI.checkbox_tick, SystemColors.MenuHighlight);
            colors.Add(CI.menu_back, SystemColors.Menu); colors.Add(CI.menu_fore, SystemColors.MenuText); colors.Add(CI.menu_dropdownback, SystemColors.ControlLightLight); colors.Add(CI.menu_dropdownfore, SystemColors.MenuText);
            colors.Add(CI.label, SystemColors.MenuText);
            colors.Add(CI.group_back, SystemColors.Menu); colors.Add(CI.group_text, SystemColors.MenuText); colors.Add(CI.group_borderlines, SystemColors.ControlDark);
            colors.Add(CI.tabcontrol_borderlines, SystemColors.ControlDark);
            colors.Add(CI.toolstrip_back, SystemColors.Control); colors.Add(CI.toolstrip_border, SystemColors.Menu); colors.Add(CI.unused_entry, SystemColors.MenuText);
            colors.Add(CI.s_panel, Color.Orange);
            colors.Add(CI.transparentcolorkey, Color.Green);
            for (int i = 0; i < DefaultChartColours.Length; i++)
                colors.Add(CI.chart1 + i, DefaultChartColours[i]);
            buttonstyle = ButtonstyleSystem;
            textboxborderstyle = TextboxborderstyleFixed3D;
            windowsframe = true;
            formopacity = 100;
            fontname = DefaultFont;
            fontsize = DefaultFontSize;
        }

        // copy constructor, takes a real copy, with overrides
        public Theme(Theme other, string newname = null, string newfont = null, float newfontsize = 0, double opaque = 0)
        {
            Name = (newname != null) ? newname : other.Name;
            fontname = (newfont != null) ? newfont : other.fontname;
            fontsize = (newfontsize != 0) ? newfontsize : other.fontsize;
            windowsframe = other.windowsframe; formopacity = other.formopacity;
            buttonstyle = other.buttonstyle; textboxborderstyle = other.textboxborderstyle;
            formopacity = (opaque > 0) ? opaque : other.formopacity;
            colors = new Dictionary<CI, Color>();
            foreach (CI ck in other.colors.Keys)
            {
                colors.Add(ck, other.colors[ck]);
            }
        }


        public void SetCustom()
        { Name = "Custom"; }                                // set so custom..
        public bool IsCustom() { return Name.Equals("Custom"); }
        public void SetColor(CI name, Color c) { colors[name] = c; }
        public Color GetColor(CI name) { return colors[name]; }

        public Font GetFont
        {
            get
            {
                return GetFontSizeStyle(fontsize, FontStyle.Regular);
            }
        }

        private const float dialogscaling = 0.8f;

        public Font GetDialogFont       // dialogs get a slighly smaller font
        {
            get
            {       // we don't scale down fonts < 12 since they are v.small already
                float fsize = fontsize >= 12 ? (fontsize * dialogscaling) : fontsize;
                return GetFontSizeStyle(fsize, FontStyle.Regular);
            }
        }

        public Font GetScaledFont(float scaling, FontStyle fs = FontStyle.Regular, float max = 999)
        {
            return GetFontSizeStyle(Math.Min(fontsize * scaling, max), fs);
        }

        public Font GetDialogScaledFont(float scaling, FontStyle fs = FontStyle.Regular, float max = 999)
        {
            float fsize = fontsize >= 12 ? (fontsize * dialogscaling) : fontsize;
            fsize = Math.Min(fsize, max);
            return GetFontSizeStyle(fsize * scaling, fs);
        }

        private Font GetFontSizeStyle(float size, FontStyle fs)
        {
            if (fontname.Equals("") || fontsize < minfontsize)
            {
                fontname = "Microsoft Sans Serif";          // in case schemes were loaded badly
                fontsize = 8.25F;
            }

            Font fnt = BaseUtils.FontLoader.GetFont(fontname, Math.Max(size, 4f), fs);        // if it does not know the font, it will substitute Sans serif
            return fnt;
        }


        public bool ApplyStd(Control ctrl, bool nowindowsborderoverride = false)      // normally a form, but can be a control, applies to this and ones below
        {
            // System.Diagnostics.Debug.WriteLine($"Themer apply standard {ctrl.Name} Font {GetFont}");
            var ret = Apply(ctrl, GetFont, nowindowsborderoverride);
            //System.Diagnostics.Debug.WriteLine($"Finish standard themeing to {ctrl.Name}");
            return ret;
        }

        public bool ApplyDialog(Control ctrl, bool nowindowsborderoverride = false)
        {
            //System.Diagnostics.Debug.WriteLine($"Themer apply dialog {ctrl.Name} Font {GetDialogFont}");
            var ret = Apply(ctrl, GetDialogFont, nowindowsborderoverride);
            //System.Diagnostics.Debug.WriteLine($"Finished dialog themeing to {ctrl.Name}");
            return ret;
        }

        internal bool Apply(Control form, Font fnt, bool nowindowsborderoverride = false)
        {
            UpdateControls(form.Parent, form, fnt, 0, nowindowsborderoverride);

            if ((ToolStripManager.Renderer as ThemeToolStripRenderer) == null)      // not installed..   install one
                ToolStripManager.Renderer = new ThemeToolStripRenderer();

            UpdateToolsStripRenderer(ToolStripManager.Renderer as ThemeToolStripRenderer);

            return WindowsFrame;
        }

        const float mouseoverscaling = 1.3F;
        const float mouseselectedscaling = 1.5F;
        static TabStyleCustom tsc = new TabStyleAngled();

        private void UpdateControls(Control parent, Control myControl, Font fnt, int level, bool noborderoverride = false)    // parent can be null
        {

#if DEBUG
            //System.Diagnostics.Debug.WriteLine("                             ".Substring(0, level) + level + ":" + parent?.Name.ToString() + ":" + myControl.Name.ToString() + " " + myControl.ToString() + " " + fnt.ToString() + " c.fnt " + myControl.Font);
            //System.Diagnostics.Debug.WriteLine("                             ".Substring(0, level) + level + ":" + (myControl.GetType().Name + ":" + myControl.Name??"") + " : " + myControl.GetHeirarchy(false));
            // System.Diagnostics.Debug.WriteLine("                             ".Substring(0, level) + level + ":" + myControl.GetType().Name + (myControl.Name.HasChars() ? " " + myControl.Name : "") + " : " + myControl.GetHeirarchy(false) + " " + myControl.Size);
#endif
            myControl.SuspendLayout();

            const bool paneldebugmode = false;      // set for some help in those pesky panels

            Type controltype = myControl.GetType();
            string parentnamespace = parent?.GetType().Namespace ?? "NoParent";

            bool dochildren = true;

            // this dodge allows no themeing on controls
            if (myControl.TabIndex == TabIndexNoThemeIndicator)
            {
                //System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent?.Name + " Tabindex indicates no theme!");
                dochildren = false;
            }
            else if (myControl is Form)
            {
                Form f = myControl as Form;
                f.FormBorderStyle = (WindowsFrame && !noborderoverride) ? FormBorderStyle.Sizable : FormBorderStyle.None;
                f.Opacity = formopacity / 100;
                f.BackColor = Form;
                f.Font = fnt;
                //System.Diagnostics.Debug.WriteLine($"Form scaling now {f.CurrentAutoScaleDimensions} {f.AutoScaleDimensions} {f.CurrentAutoScaleFactor()}");
            }
            else if (myControl is CompositeAutoScaleButton || myControl is CompositeButton)        // these are not themed, they have a bitmap, and the backcolour is kept
            {
            }
            else if (myControl is ExtRichTextBox)
            {
                ExtRichTextBox ctrl = (ExtRichTextBox)myControl;
                ctrl.BorderColor = Color.Transparent;
                ctrl.BorderStyle = BorderStyle.None;

                ctrl.TextBoxForeColor = TextBlockColor;
                ctrl.TextBoxBackColor = TextBackColor;

                ctrl.ScrollBarFlatStyle = FlatStyle.System;

                if (textboxborderstyle.Equals(TextboxBorderStyles[1]))
                    ctrl.BorderStyle = BorderStyle.FixedSingle;
                else if (textboxborderstyle.Equals(TextboxBorderStyles[2]))
                    ctrl.BorderStyle = BorderStyle.Fixed3D;
                else if (textboxborderstyle.Equals(TextboxBorderStyles[3]))
                {
                    Color c1 = TextBlockScrollButton;
                    ctrl.BorderColor = TextBlockBorderColor;
                    ctrl.ScrollBarBackColor = TextBackColor;
                    ctrl.ScrollBarSliderColor = TextBlockSliderBack;
                    ctrl.ScrollBarBorderColor = ctrl.ScrollBarThumbBorderColor =
                                ctrl.ScrollBarArrowBorderColor = TextBlockBorderColor;
                    ctrl.ScrollBarArrowButtonColor = ctrl.ScrollBarThumbButtonColor = c1;
                    ctrl.ScrollBarMouseOverButtonColor = c1.Multiply(mouseoverscaling);
                    ctrl.ScrollBarMousePressedButtonColor = c1.Multiply(mouseselectedscaling);
                    ctrl.ScrollBarForeColor = TextBlockScrollArrow;
                    ctrl.ScrollBarFlatStyle = FlatStyle.Popup;
                }

                if (ctrl.ContextMenuStrip != null)      // propegate font
                    ctrl.ContextMenuStrip.Font = fnt;

                ctrl.Invalidate();
                ctrl.PerformLayout();

                dochildren = false;
            }
            else if (myControl is ExtTextBox)
            {
                ExtTextBox ctrl = (ExtTextBox)myControl;
                ctrl.ForeColor = TextBlockColor;
                ctrl.BackColor = TextBackColor;
                ctrl.BackErrorColor = TextBlockHighlightColor;
                ctrl.ControlBackground = TextBackColor; // previously, but not sure why, GroupBoxOverride(parent, Form);
                ctrl.BorderColor = Color.Transparent;
                ctrl.BorderStyle = BorderStyle.None;
                ctrl.AutoSize = true;

                if (textboxborderstyle.Equals(TextboxBorderStyles[0]))
                    ctrl.AutoSize = false;                                                 // with no border, the autosize clips the bottom of chars..
                else if (textboxborderstyle.Equals(TextboxBorderStyles[1]))
                    ctrl.BorderStyle = BorderStyle.FixedSingle;
                else if (textboxborderstyle.Equals(TextboxBorderStyles[2]))
                    ctrl.BorderStyle = BorderStyle.Fixed3D;
                else if (textboxborderstyle.Equals(TextboxBorderStyles[3]))
                    ctrl.BorderColor = TextBlockBorderColor;

                if (myControl is ExtTextBoxAutoComplete || myControl is ExtDataGridViewColumnAutoComplete.CellEditControl) // derived from text box
                {
                    ExtTextBoxAutoComplete actb = myControl as ExtTextBoxAutoComplete;
                    actb.DropDownBackgroundColor = ButtonBackColor;
                    actb.DropDownBorderColor = TextBlockBorderColor;
                    actb.DropDownScrollBarButtonColor = TextBlockScrollButton;
                    actb.DropDownScrollBarColor = TextBlockSliderBack;
                    actb.DropDownMouseOverBackgroundColor = ButtonBackColor.Multiply(mouseoverscaling);

                    if (buttonstyle.Equals(ButtonStyles[0]))
                        actb.FlatStyle = FlatStyle.System;
                    else if (buttonstyle.Equals(ButtonStyles[1])) // flat
                        actb.FlatStyle = FlatStyle.Flat;
                    else
                        actb.FlatStyle = FlatStyle.Popup;
                }

                ThemeButton(ctrl.EndButton);
                ctrl.EndButton.FlatAppearance.BorderColor =             // override some of them to make back of button disappear
                ctrl.EndButton.BackColor = TextBackColor;
                ctrl.EndButton.ButtonColorScaling = ctrl.EndButton.ButtonDisabledScaling = 1.0F;

                ctrl.Invalidate();

                dochildren = false;
            }
            else if (myControl is ExtButton)
            {
                ThemeButton(myControl);
            }
            else if (myControl is ExtTabControl)
            {
                ExtTabControl ctrl = (ExtTabControl)myControl;

                if (!buttonstyle.Equals(ButtonStyles[0])) // not system
                {
                    ctrl.TabControlBorderColor = TabcontrolBorder.Multiply(0.6F);
                    ctrl.TabControlBorderBrightColor = TabcontrolBorder;
                    ctrl.TabNotSelectedBorderColor = TabcontrolBorder.Multiply(0.4F);
                    ctrl.TabNotSelectedColor = ButtonBackColor;
                    ctrl.TabSelectedColor = ButtonBackColor.Multiply(mouseselectedscaling);
                    ctrl.TabMouseOverColor = ButtonBackColor.Multiply(mouseoverscaling);
                    ctrl.TextSelectedColor = ButtonTextColor;
                    ctrl.TextNotSelectedColor = ButtonTextColor.Multiply(0.8F);
                    ctrl.SetStyle((buttonstyle.Equals(ButtonStyles[1])) ? FlatStyle.Flat : FlatStyle.Popup, tsc);
                }
                else
                    ctrl.FlatStyle = FlatStyle.System;

            }
            else if (myControl is ExtListBox)
            {
                ExtListBox ctrl = (ExtListBox)myControl;
                ctrl.ForeColor = ButtonTextColor;
                ctrl.ItemSeperatorColor = ButtonBorderColor;

                if (buttonstyle.Equals(ButtonStyles[0]))
                {
                    ctrl.FlatStyle = FlatStyle.System;
                }
                else
                {
                    ctrl.BackColor = ButtonBackColor;
                    ctrl.BorderColor = ButtonBorderColor;
                    ctrl.ScrollBarButtonColor = TextBlockScrollButton;
                    ctrl.ScrollBarColor = TextBlockSliderBack;

                    if (buttonstyle.Equals(ButtonStyles[1])) // flat
                        ctrl.FlatStyle = FlatStyle.Flat;
                    else
                        ctrl.FlatStyle = FlatStyle.Popup;
                }

                ctrl.Repaint();            // force a repaint as the individual settings do not by design.

                dochildren = false;
            }
            else if (myControl is ExtSafeChart)  // as a panel, we intercept to say okay before the panel. Item itself needs no themeing
            {
            }
            else if (myControl is ExtChart)     // Note you should be using ExtSafeChart
            {
                ThemeChart(fnt, (ExtChart)myControl);
            }
            else if (myControl is Chart)
            {
                System.Diagnostics.Debug.Assert(false, "Warning - Chart not allowed");
            }
            else if (myControl is MultiPipControl)
            {
                MultiPipControl ctrl = (MultiPipControl)myControl;
                ctrl.ForeColor = ButtonTextColor;
                ctrl.PipColor = ButtonTextColor;
                ctrl.HalfPipColor = ButtonTextColor.MultiplyBrightness(0.6f);
                ctrl.BorderColor = GridBorderLines;
            }
            else if (myControl is ExtPanelResizer)      // Resizers only show when no frame is on
            {
                myControl.Visible = !WindowsFrame;
            }
            else if (myControl is ExtPanelDropDown)
            {
                ExtPanelDropDown ctrl = (ExtPanelDropDown)myControl;
                ctrl.ForeColor = ButtonTextColor;
                ctrl.SelectionMarkColor = ctrl.ForeColor;
                ctrl.BackColor = ctrl.SelectionBackColor = ButtonBackColor;
                ctrl.BorderColor = ButtonBorderColor;
                ctrl.MouseOverBackgroundColor = ButtonBackColor.Multiply(mouseoverscaling);
                ctrl.ScrollBarButtonColor = TextBlockScrollButton;
                ctrl.ScrollBarColor = TextBlockSliderBack;
                ctrl.FlatStyle = FlatStyle.Popup;
            }
            else if (myControl is ExtComboBox)
            {
                ExtComboBox ctrl = (ExtComboBox)myControl;
                ctrl.ForeColor = ButtonTextColor;

                if (buttonstyle.Equals(ButtonStyles[0])) // system
                {
                    ctrl.FlatStyle = FlatStyle.System;
                }
                else
                {
                    ctrl.BackColor = ctrl.DropDownBackgroundColor = ButtonBackColor;
                    ctrl.BorderColor = ButtonBorderColor;
                    ctrl.MouseOverBackgroundColor = ButtonBackColor.Multiply(mouseoverscaling);
                    ctrl.ScrollBarButtonColor = TextBlockScrollButton;
                    ctrl.ScrollBarColor = TextBlockSliderBack;

                    if (buttonstyle.Equals(ButtonStyles[1])) // flat
                        ctrl.FlatStyle = FlatStyle.Flat;
                    else
                        ctrl.FlatStyle = FlatStyle.Popup;

                }

                ctrl.Repaint();            // force a repaint as the individual settings do not by design.

                dochildren = false;
            }
            else if (myControl is NumericUpDown)
            {                                                                   // BACK colour does not work..
                myControl.ForeColor = TextBlockColor;
            }
            else if (myControl is ExtButtonDrawn)
            {
                ExtButtonDrawn ctrl = (ExtButtonDrawn)myControl;
                ctrl.BackColor = Form;
                ctrl.ForeColor = LabelColor;
                ctrl.MouseOverColor = LabelColor.Multiply(mouseoverscaling);
                ctrl.MouseSelectedColor = LabelColor.Multiply(mouseselectedscaling);
                ctrl.BorderWidth = 2;
                ctrl.BorderColor = GridBorderLines;

                System.Drawing.Imaging.ColorMap colormap = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images
                colormap.OldColor = Color.White;                                                        // white is defined as the forecolour
                colormap.NewColor = ctrl.ForeColor;
                ctrl.SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap });
                //System.Diagnostics.Debug.WriteLine("Drawn Panel Image button " + ctrl.Name);
            }
            else if (myControl is TableLayoutPanel)
            {
                myControl.BackColor = GroupBoxOverride(parent, Form);
            }
            else if (myControl is ExtPanelRollUp)
            {
                myControl.BackColor = paneldebugmode ? Color.Green : GroupBoxOverride(parent, Form);
            }
            else if (myControl is FlowLayoutPanel)
            {
                FlowLayoutPanel ctrl = myControl as FlowLayoutPanel;
                ctrl.BackColor = paneldebugmode ? Color.Red : GroupBoxOverride(parent, Form);
            }
            else if (myControl is Panel)
            {
                if (myControl is PanelNoTheme)      // this type indicates no theme
                {
                    //System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent?.Name + " Panel no theme!");
                    dochildren = false;
                }
                else
                {
                    //System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent?.Name + " " + myControl.TabIndex + "Panel Theme It!");
                    myControl.BackColor = paneldebugmode ? Color.Blue : Form;
                    myControl.ForeColor = LabelColor;
                }
            }
            else if (myControl is Label)
            {
                myControl.ForeColor = LabelColor;

                if (myControl is ExtLabel)
                    (myControl as ExtLabel).TextBackColor = Form;
            }
            else if (myControl is ExtGroupBox)
            {
                ExtGroupBox ctrl = (ExtGroupBox)myControl;
                ctrl.ForeColor = GroupFore;
                ctrl.BackColor = GroupBack;
                ctrl.BorderColor = GroupBorder;
                ctrl.FlatStyle = FlatStyle.Flat;           // always in Flat, always apply our border.
            }
            else if (myControl is ExtCheckBox)
            {
                ExtCheckBox ctrl = (ExtCheckBox)myControl;

                ctrl.BackColor = GroupBoxOverride(parent, Form);

                if (ctrl.Appearance == Appearance.Button)
                {
                    ctrl.ForeColor = ButtonTextColor;
                    ctrl.MouseOverColor = ButtonBackColor.Multiply(mouseoverscaling);
                    ctrl.CheckColor = ButtonBackColor.Multiply(0.9f);

                    if (buttonstyle.Equals(ButtonStyles[0])) // system
                        ctrl.FlatStyle = FlatStyle.Standard;
                    else if (buttonstyle.Equals(ButtonStyles[1])) // flat
                        ctrl.FlatStyle = FlatStyle.Flat;
                    else
                        ctrl.FlatStyle = FlatStyle.Popup;
                }
                else
                {
                    ctrl.ForeColor = CheckBox;
                    ctrl.CheckBoxColor = CheckBox;
                    ctrl.CheckBoxInnerColor = CheckBox.Multiply(1.5F);
                    ctrl.MouseOverColor = CheckBox.Multiply(1.4F);
                    ctrl.TickBoxReductionRatio = 0.75f;
                    ctrl.CheckColor = CheckBoxTick;

                    if (buttonstyle.Equals(ButtonStyles[0])) // system
                        ctrl.FlatStyle = FlatStyle.System;
                    else if (buttonstyle.Equals(ButtonStyles[1])) // flat
                        ctrl.FlatStyle = FlatStyle.Flat;
                    else
                        ctrl.FlatStyle = FlatStyle.Popup;
                }

                if (ctrl.Image != null)
                {
                    System.Drawing.Imaging.ColorMap colormap = new System.Drawing.Imaging.ColorMap();
                    colormap.OldColor = Color.White;                                                        // white is defined as the forecolour
                    colormap.NewColor = ctrl.ForeColor;
                    ctrl.SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap });

                    ctrl.ImageLayout = ImageLayout.Stretch;
                }
            }
            else if (myControl is ExtRadioButton)
            {
                ExtRadioButton ctrl = (ExtRadioButton)myControl;

                ctrl.FlatStyle = FlatStyle.System;

                if (buttonstyle.Equals(ButtonStyles[0])) // system
                    ctrl.FlatStyle = FlatStyle.System;
                else if (buttonstyle.Equals(ButtonStyles[1])) // flat
                    ctrl.FlatStyle = FlatStyle.Flat;
                else
                    ctrl.FlatStyle = FlatStyle.Popup;

                //Console.WriteLine("RB:" + myControl.Name + " Apply style " + buttonstyle);

                ctrl.BackColor = GroupBoxOverride(parent, Form);
                ctrl.ForeColor = CheckBox;
                ctrl.RadioButtonColor = CheckBox;
                ctrl.RadioButtonInnerColor = CheckBox.Multiply(1.5F);
                ctrl.SelectedColor = ctrl.BackColor.Multiply(0.75F);
                ctrl.MouseOverColor = CheckBox.Multiply(1.4F);
            }
            else if (myControl is DataGridView)                     // we theme this directly
            {
                DataGridView ctrl = (DataGridView)myControl;
                ctrl.EnableHeadersVisualStyles = false;            // without this, the colours for the grid are not applied.

                ctrl.RowHeadersDefaultCellStyle.BackColor = GridBorderBack;
                ctrl.RowHeadersDefaultCellStyle.ForeColor = GridBorderText;
                ctrl.RowHeadersDefaultCellStyle.SelectionForeColor = GridBorderText;
                ctrl.RowHeadersDefaultCellStyle.SelectionBackColor = GridBorderBack;

                ctrl.ColumnHeadersDefaultCellStyle.BackColor = GridBorderBack;
                ctrl.ColumnHeadersDefaultCellStyle.ForeColor = GridBorderText;
                ctrl.ColumnHeadersDefaultCellStyle.SelectionForeColor = GridBorderText;
                ctrl.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridBorderBack;

                ctrl.DefaultCellStyle.BackColor = GridCellBack;
                ctrl.DefaultCellStyle.ForeColor = GridCellText;
                ctrl.DefaultCellStyle.SelectionBackColor = ctrl.DefaultCellStyle.ForeColor;
                ctrl.DefaultCellStyle.SelectionForeColor = ctrl.DefaultCellStyle.BackColor;

                ctrl.BackgroundColor = GroupBoxOverride(parent, Form);

                ctrl.AlternatingRowsDefaultCellStyle.BackColor = GridCellAltBack;
                ctrl.AlternatingRowsDefaultCellStyle.ForeColor = GridCellAltText;

                ctrl.BorderStyle = BorderStyle.None;        // can't control the color of this, turn it off

                ctrl.GridColor = GridBorderLines;
                ctrl.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                ctrl.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                foreach (DataGridViewColumn col in ctrl.Columns)
                {
                    if (col.CellType == typeof(DataGridViewComboBoxCell))
                    {   // Need to set flat style for colours to take on combobox cells.
                        DataGridViewComboBoxColumn cbocol = (DataGridViewComboBoxColumn)col;
                        if (buttonstyle.Equals(ButtonStyles[0])) // system
                            cbocol.FlatStyle = FlatStyle.System;
                        else if (buttonstyle.Equals(ButtonStyles[1])) // flat
                            cbocol.FlatStyle = FlatStyle.Flat;
                        else
                            cbocol.FlatStyle = FlatStyle.Popup;
                    }
                }

                if (ctrl.ContextMenuStrip != null)       // propergate font onto any attached context menus
                    ctrl.ContextMenuStrip.Font = fnt;
            }
            else if (myControl is ExtScrollBar)
            {
                ExtScrollBar ctrl = (ExtScrollBar)myControl;

                //System.Diagnostics.Debug.WriteLine("VScrollBarCustom Theme " + level + ":" + parent.Name.ToString() + ":" + myControl.Name.ToString() + " " + myControl.ToString() + " " + parentcontroltype.Name);
                if (textboxborderstyle.Equals(TextboxBorderStyles[3]))
                {
                    Color c1 = GridScrollButton;
                    ctrl.BorderColor = GridBorderLines;
                    ctrl.BackColor = Form;
                    ctrl.SliderColor = GridSliderBack;
                    ctrl.BorderColor = ctrl.ThumbBorderColor =
                            ctrl.ArrowBorderColor = GridBorderLines;
                    ctrl.ArrowButtonColor = ctrl.ThumbButtonColor = c1;
                    ctrl.MouseOverButtonColor = c1.Multiply(mouseoverscaling);
                    ctrl.MousePressedButtonColor = c1.Multiply(mouseselectedscaling);
                    ctrl.ForeColor = GridScrollArrow;
                    ctrl.FlatStyle = FlatStyle.Popup;
                }
                else
                    ctrl.FlatStyle = FlatStyle.System;
            }
            else if (myControl is ExtNumericUpDown)
            {
                ExtNumericUpDown ctrl = (ExtNumericUpDown)myControl;

                ctrl.TextBoxForeColor = TextBlockColor;
                ctrl.TextBoxBackColor = TextBackColor;
                ctrl.BorderColor = TextBlockBorderColor;

                Color c1 = TextBlockScrollButton;
                ctrl.updown.BackColor = c1;
                ctrl.updown.ForeColor = TextBlockScrollArrow;
                ctrl.updown.BorderColor = ButtonBorderColor;
                ctrl.updown.MouseOverColor = c1.Multiply(mouseoverscaling);
                ctrl.updown.MouseSelectedColor = c1.Multiply(mouseselectedscaling);
                ctrl.Invalidate();

                dochildren = false;
            }
            else if (myControl is CheckedIconUserControl)
            {
                CheckedIconUserControl ctrl = (CheckedIconUserControl)myControl;
                ctrl.BackColor = Form;
                ctrl.ForeColor = TextBlockColor;
                ctrl.BorderColor = GridBorderLines;
                ctrl.BackColor = Form;
                ctrl.SliderColor = GridSliderBack;
                ctrl.BorderColor = ctrl.ThumbBorderColor =
                        ctrl.ArrowBorderColor = GridBorderLines;
                Color c1 = GridScrollButton;
                ctrl.ArrowButtonColor = ctrl.ThumbButtonColor = c1;
                ctrl.MouseOverButtonColor = c1.Multiply(mouseoverscaling);
                ctrl.MousePressedButtonColor = c1.Multiply(mouseselectedscaling);
                ctrl.CheckBoxColor = CheckBox;
                ctrl.CheckBoxInnerColor = CheckBox.Multiply(1.5F);
                ctrl.MouseOverCheckboxColor = CheckBox.Multiply(0.75F);
                ctrl.MouseOverLabelColor = CheckBox.Multiply(0.75F);
                ctrl.TickBoxReductionRatio = 0.75f;
                ctrl.CheckColor = CheckBoxTick;
            }
            else if (myControl is PictureBox)
            {
                PictureBox ctrl = (PictureBox)myControl;

                if (ctrl.ContextMenuStrip != null)       // propergate font onto any attached context menus
                    ctrl.ContextMenuStrip.Font = fnt;
            }
            else if (myControl is ExtDateTimePicker)
            {
                ExtDateTimePicker ctrl = (ExtDateTimePicker)myControl;
                ctrl.BorderColor = GridBorderLines;
                ctrl.ForeColor = TextBlockColor;
                ctrl.TextBackColor = TextBackColor;
                ctrl.BackColor = Form;
                ctrl.SelectedColor = TextBlockColor.MultiplyBrightness(0.6F);

                if (buttonstyle.Equals(ButtonStyles[0])) // system
                    ctrl.checkbox.FlatStyle = FlatStyle.System;
                else
                    ctrl.checkbox.FlatStyle = FlatStyle.Popup;

                ctrl.checkbox.TickBoxReductionRatio = 0.75f;
                ctrl.checkbox.ForeColor = CheckBox;
                ctrl.checkbox.CheckBoxColor = CheckBox;
                Color inner = CheckBox.Multiply(1.5F);
                if (inner.GetBrightness() < 0.1)        // double checking
                    inner = Color.Gray;
                ctrl.checkbox.CheckBoxInnerColor = inner;
                ctrl.checkbox.CheckColor = CheckBoxTick;
                ctrl.checkbox.MouseOverColor = CheckBox.Multiply(1.4F);

                ctrl.updown.BackColor = ButtonBackColor;
                ctrl.updown.BorderColor = GridBorderLines;
                ctrl.updown.ForeColor = TextBlockColor;
                ctrl.updown.MouseOverColor = CheckBox.Multiply(1.4F);
                ctrl.updown.MouseSelectedColor = CheckBox.Multiply(1.5F);

                dochildren = false;
            }
            else if (myControl is StatusStrip)
            {
                myControl.BackColor = Form;
                myControl.ForeColor = LabelColor;
                dochildren = false;
            }
            else if (myControl is ToolStrip)    // MenuStrip is a tool stip
            {
                myControl.Font = fnt;       // Toolstrips don't seem to inherit Forms font, so force

                foreach (ToolStripItem i in ((ToolStrip)myControl).Items)   // make sure any buttons have the button back colour set
                {
                    if (i is ToolStripButton || i is ToolStripDropDownButton)
                    {           // theme the back colour, this is the way its done.. not via the tool strip renderer
                        i.BackColor = ButtonBackColor;
                    }
                    else if (i is ToolStripTextBox)
                    {
                        i.ForeColor = TextBlockColor;
                        i.BackColor = TextBackColor;
                    }
                }
            }
            else if (myControl is TabStrip)
            {
                TabStrip ts = myControl as TabStrip;
                //System.Diagnostics.Debug.WriteLine("*************** TAB Strip themeing" + myControl.Name + " " + myControl.Tag);
                ts.ForeColor = ButtonTextColor;
                ts.DropDownBackgroundColor = ButtonBackColor;
                ts.DropDownBorderColor = TextBlockBorderColor;
                ts.DropDownScrollBarButtonColor = TextBlockScrollButton;
                ts.DropDownScrollBarColor = TextBlockSliderBack;
                ts.DropDownMouseOverBackgroundColor = ButtonBackColor.Multiply(mouseoverscaling);
                ts.DropDownItemSeperatorColor = ButtonBorderColor;
                ts.EmptyColor = ButtonBackColor;
                ts.SelectedBackColor = ButtonBackColor;
            }
            else if (myControl is Controls.ImageControl)      // no theme
            {
            }
            else if (myControl is TreeView)
            {
                TreeView ctrl = myControl as TreeView;
                ctrl.ForeColor = TextBlockColor;
                ctrl.BackColor = TextBackColor;
            }

            else if (myControl is CompassControl)
            {
                CompassControl compassControl = myControl as CompassControl;
                compassControl.ForeColor = TextBlockColor;
                compassControl.StencilColor = TextBlockColor;
                compassControl.CentreTickColor = TextBlockColor.Multiply(1.2F);
                compassControl.BugColor = TextBlockColor.Multiply(0.8F);
                compassControl.BackColor = Form;
            }

            else if (myControl is LabelData)
            {
                LabelData ld = myControl as LabelData;
                ld.BorderColor = TextBlockBorderColor;
                ld.ForeColor = LabelColor;
            }
            else if (myControl is Button wfb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wfb.ForeColor = ButtonTextColor;
                wfb.BackColor = ButtonBackColor;
                wfb.FlatStyle = FlatStyle.Popup;
                wfb.FlatAppearance.BorderColor = ButtonBorderColor;
            }
            else if (myControl is RadioButton wrb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wrb.ForeColor = ButtonTextColor;
            }
            else if (myControl is GroupBox wgb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wgb.ForeColor = GroupFore;
                wgb.BackColor = GroupBack;
            }
            else if (myControl is CheckBox wchb)
            {
                wchb.BackColor = GroupBoxOverride(parent, Form);

                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                if (wchb.Appearance == Appearance.Button)
                {
                    wchb.ForeColor = ButtonTextColor;
                }
                else
                {
                    wchb.ForeColor = CheckBox;
                }

            }
            else if (myControl is TextBox wtb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wtb.ForeColor = TextBlockColor;
                wtb.BackColor = TextBackColor;
                wtb.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (myControl is RichTextBox wrtb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wrtb.ForeColor = TextBlockColor;
                wrtb.BackColor = TextBackColor;
            }
            else if (myControl is ComboBox wcb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wcb.ForeColor = ButtonTextColor;
                wcb.BackColor = ButtonBackColor;
            }
            else if (myControl is ExtSplitterResizeParent)      // splitter, no theme
            {
            }
            // these are not themed or are sub parts of other controls
            else if (myControl is UserControl || myControl is SplitContainer || myControl is SplitterPanel || myControl is HScrollBar || myControl is VScrollBar)
            {
            }
            else
            {
                System.Diagnostics.Trace.WriteLine($"Themer {myControl.Name}:{controltype.Name} from {parent.Name} Unknown control!");
            }

            //System.Diagnostics.Debug.WriteLine("                  " + level + " Control " + myControl.Name + " " + myControl.Location + " " + myControl.Size);

            //if (myControl.Name == "textBoxSystem")  System.Diagnostics.Debug.WriteLine($"Theme begin sub controls of {myControl.Name} ");

            if (dochildren)
            {
                foreach (Control subC in myControl.Controls)
                {
                    //if (myControl.Name == "textBoxSystem")  System.Diagnostics.Debug.WriteLine($"Theme sub controls of {myControl.Name} sub {subC.GetType().Name}");
                    UpdateControls(myControl, subC, fnt, level + 1);
                }
            }

            myControl.ResumeLayout();
            //if (myControl.Name == "textBoxSystem")  System.Diagnostics.Debug.WriteLine($"Theme Control {myControl.Name} to {myControl.Bounds}");
        }

        private void ThemeButton(Control myControl)
        {
            ExtButton ctrl = (ExtButton)myControl;
            ctrl.ForeColor = ButtonTextColor;

            if (ctrl.Text.HasChars())      // we autosize text to make it fit.. we do not autosize image buttons
                ctrl.AutoSize = true;

            if (buttonstyle.Equals(ButtonStyles[0])) // system
            {
                ctrl.FlatStyle = (ctrl.Image != null) ? FlatStyle.Standard : FlatStyle.System;
                ctrl.UseVisualStyleBackColor = true;           // this makes it system..
            }
            else
            {
                ctrl.ButtonColorScaling = ctrl.ButtonDisabledScaling = 0.5F;

                if (ctrl.Image != null)     // any images, White and a gray (for historic reasons) gets replaced.
                {
                    System.Drawing.Imaging.ColorMap colormap1 = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images
                    colormap1.OldColor = Color.FromArgb(134, 134, 134);                                      // gray is defined as the forecolour
                    colormap1.NewColor = ctrl.ForeColor;
                    //System.Diagnostics.Debug.WriteLine("Theme Image in " + ctrl.Name + " Map " + colormap1.OldColor + " to " + colormap1.NewColor);

                    System.Drawing.Imaging.ColorMap colormap2 = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images
                    colormap2.OldColor = Color.FromArgb(255, 255, 255);                                      // and white is defined as the forecolour
                    colormap2.NewColor = ctrl.ForeColor;
                    //System.Diagnostics.Debug.WriteLine("Theme Image in " + ctrl.Name + " Map " + colormap2.OldColor + " to " + colormap2.NewColor);

                    ctrl.SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap1, colormap2 });     // used ButtonDisabledScaling note!
                }

                // if image, and no text or text over image centred (so over the image), background is form to make the back disappear
                if (ctrl.Image != null && (ctrl.Text.Length == 0 || (ctrl.TextAlign == ContentAlignment.MiddleCenter && ctrl.ImageAlign == ContentAlignment.MiddleCenter)))
                {
                    if (ctrl.Parent != null && ctrl.Parent is CompositeAutoScaleButton)       // composite auto scale buttons inherit parent back colour and have disabled scaling turned off
                    {
                        ctrl.BackColor = ctrl.Parent.BackColor;
                        ctrl.ButtonColorScaling = ctrl.ButtonDisabledScaling = 1.0F;
                    }
                    else
                        ctrl.BackColor = Form;
                }
                else
                {
                    ctrl.BackColor = ButtonBackColor;       // else its a graduated back
                }

                ctrl.FlatAppearance.BorderColor = (ctrl.Image != null) ? Form : ButtonBorderColor;
                ctrl.FlatAppearance.BorderSize = 1;
                ctrl.FlatAppearance.MouseOverBackColor = ButtonBackColor.Multiply(mouseoverscaling);
                ctrl.FlatAppearance.MouseDownBackColor = ButtonBackColor.Multiply(mouseselectedscaling);

                if (buttonstyle.Equals(ButtonStyles[1])) // flat
                    ctrl.FlatStyle = FlatStyle.Flat;
                else
                    ctrl.FlatStyle = FlatStyle.Popup;
            }

        }

        // You can reapply themeing individually to a chart if you change the series/chart areas.
        public void ThemeChart(Font fnt, ExtChart ctrl)
        {
            ctrl.Font = fnt;        // log the font with the chart, so you can use it directly in further explicit themeing
            ctrl.BackColor = Form;

            // so the themer only overrides border/back colours if the user has set them to a value already. It does not override empty entries 
            // the user can chose if titles/legends border and back is themed

            ctrl.SetAllTitlesColorFont(GridCellText, GetScaledFont(1.5f), GridCellBack,
                                      Color.Empty, 1,
                                      GroupBorder, 1, ChartDashStyle.Solid);

            ctrl.SetAllLegendsColorFont(GridCellText, fnt, ctrl.BackColor, 6, Color.FromArgb(128, 0, 0, 0),
                                        GridCellText, GridCellBack, fnt, StringAlignment.Center, LegendSeparatorStyle.Line, GridBorderLines,
                                        GridBorderLines, ChartDashStyle.Solid, 0,
                                        LegendSeparatorStyle.Line, GroupBorder, 1);

            // we theme all chart areas, backwards, so chartarea0 is the one left selected            
            for (int i = ctrl.ChartAreas.Count - 1; i >= 0; i--)
            {
                // System.Diagnostics.Debug.WriteLine($"Themer {ctrl.Parent.Name} Theme Chart Area {i}");
                ctrl.SetCurrentChartArea(i);
                ctrl.SetChartAreaColors(GridCellBack, GridBorderLines);

                ctrl.SetXAxisMajorGridWidthColor(1, ChartDashStyle.Solid, GridBorderLines);
                ctrl.SetYAxisMajorGridWidthColor(1, ChartDashStyle.Solid, GridBorderLines);
                ctrl.SetXAxisLabelColorFont(GridCellText, fnt);
                ctrl.SetYAxisLabelColorFont(GridCellText, fnt);
                ctrl.SetXAxisTitle(ctrl.CurrentChartArea.AxisX.Title, fnt, GridCellText);
                ctrl.SetYAxisTitle(ctrl.CurrentChartArea.AxisY.Title, fnt, GridCellText);

                ctrl.SetXCursorColors(GridScrollArrow, GridCellText, 2);
                ctrl.SetYCursorColors(GridScrollArrow, GridCellText, 2);

                ctrl.SetXCursorScrollBarColors(GridSliderBack, GridScrollButton);
                ctrl.SetYCursorScrollBarColors(GridSliderBack, GridScrollButton);
            }

            for (int i = ctrl.Series.Count - 1; i >= 0; i--)        // backwards so chart 0 is left the pick
            {
                // System.Diagnostics.Debug.WriteLine($"Themer {ctrl.Parent.Name} Theme series {i}");
                ctrl.SetCurrentSeries(i);
                ctrl.SetSeriesColor(colors[CI.chart1 + i]);
                ctrl.SetSeriesDataLabelsColorFont(GridCellText, fnt, Color.Transparent);
                ctrl.SetSeriesMarkersColorSize(GridScrollArrow, 4, GridScrollButton, 2);
            }
        }

        private void UpdateToolsStripRenderer(ThemeToolStripRenderer toolstripRenderer)
        {
            Color menuback = MenuBack;
            bool toodark = (menuback.GetBrightness() < 0.1);

            toolstripRenderer.colortable.colMenuText = MenuFore;              // and the text
            toolstripRenderer.colortable.colMenuSelectedText = MenuDropdownFore;
            toolstripRenderer.colortable.colMenuBackground = menuback;
            toolstripRenderer.colortable.colMenuBarBackground = Form;
            toolstripRenderer.colortable.colMenuBorder = ButtonBorderColor;
            toolstripRenderer.colortable.colMenuSelectedBack = MenuDropdownBack;
            toolstripRenderer.colortable.colMenuHighlightBorder = ButtonBorderColor;
            toolstripRenderer.colortable.colMenuHighlightBack = toodark ? MenuDropdownBack.Multiply(0.7F) : MenuBack.Multiply(1.3F);        // whole menu back

            Color menuchecked = toodark ? MenuDropdownBack.Multiply(0.8F) : MenuBack.Multiply(1.5F);        // whole menu back

            toolstripRenderer.colortable.colCheckButtonChecked = menuchecked;
            toolstripRenderer.colortable.colCheckButtonPressed =
            toolstripRenderer.colortable.colCheckButtonHighlighted = menuchecked.Multiply(1.1F);

            toolstripRenderer.colortable.colToolStripButtonCheckedBack = menuchecked;
            toolstripRenderer.colortable.colToolStripButtonPressedBack =
            toolstripRenderer.colortable.colToolStripButtonSelectedBack = menuchecked.Multiply(1.1F);

            toolstripRenderer.colortable.colToolStripBackground = ToolstripBack;
            toolstripRenderer.colortable.colToolStripBorder = ToolstripBorder;
            toolstripRenderer.colortable.colToolStripSeparator = ToolstripBorder;
            toolstripRenderer.colortable.colOverflowButton = MenuBack;
            toolstripRenderer.colortable.colGripper = ToolstripBorder;

            toolstripRenderer.colortable.colToolStripDropDownMenuImageMargin = ButtonBackColor;
            toolstripRenderer.colortable.colToolStripDropDownMenuImageRevealed = Color.Purple;      // NO evidence, set to show up

        }
        public Color GroupBoxOverride(Control parent, Color d)      // if its a group box behind the control, use the group box back color..
        {
            Control x = parent;
            while (x != null)
            {
                if (x is GroupBox)
                    return GroupBack;
                x = x.Parent;
            }

            return d;
        }


        // From JSON, read theme.
        // default set is the values to use if the theme Json does not have an entry
        public bool FromJSON(JObject jo, string name, Theme defaultset = null)
        {
            if (defaultset == null)
                defaultset = new Theme("Windows");                  // if none given, use the basic windows set to get a default set

            this.Name = name;

            foreach (CI ck in Enum.GetValues(typeof(CI)))           // all enums
            {
                bool foundcolour = false;

                try
                {
                    string s = jo[ck.ToString()].StrNull();     // look up key name
                    if (s != null)
                    {
                        Color c = System.Drawing.ColorTranslator.FromHtml(s);   // may except if not valid HTML colour
                        colors[ck] = c;
                        foundcolour = true;
                        //System.Diagnostics.Debug.WriteLine("Color.FromArgb({0},{1},{2},{3}), // {4}", c.A, c.R, c.G, c.B, ck.ToString());
                    }
                    else
                    {
                        int? v = jo[ck.ToString()].IntNull();       // or it can be a number
                        if (v != null)
                        {
                            colors[ck] = Color.FromArgb(v.Value);
                            foundcolour = true;
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Theme has invalid colour");
                }

                try
                {
                    if (!foundcolour)       // does not have a colour
                    {
                        string gridtext = jo[CI.grid_celltext.ToString()].StrNull();
                        string gridback = jo[CI.grid_cellbackground.ToString()].StrNull();

                        if (ck == CI.grid_altcelltext && gridtext != null)
                        {
                            Color c = System.Drawing.ColorTranslator.FromHtml(gridtext);   // may except if not valid HTML colour
                            colors[ck] = c;
                        }
                        else if (ck == CI.grid_altcellbackground && gridback != null)
                        {
                            Color c = System.Drawing.ColorTranslator.FromHtml(gridback);   // may except if not valid HTML colour
                            colors[ck] = c;
                        }
                        else
                        {
                            Color def = defaultset.colors[ck];        // use default set's value
                            colors[ck] = def;
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Theme has invalid colour");
                }
            }

            windowsframe = jo["windowsframe"].Bool(defaultset.windowsframe);
            formopacity = jo["formopacity"].Double(defaultset.formopacity);
            fontname = jo["fontname"].Str(defaultset.fontname);
            fontsize = jo["fontsize"].Float(defaultset.fontsize);
            buttonstyle = jo["buttonstyle"].Str(defaultset.buttonstyle);
            textboxborderstyle = jo["textboxborderstyle"].Str(defaultset.textboxborderstyle);

            return true;
        }

        public JObject ToJSON()
        {
            JObject jo = new JObject();

            foreach (CI ck in colors.Keys)
            {
                jo.Add(ck.ToString(), System.Drawing.ColorTranslator.ToHtml(colors[ck]));
            }

            jo.Add("windowsframe", windowsframe);
            jo.Add("formopacity", formopacity);
            jo.Add("fontname", fontname);
            jo.Add("fontsize", fontsize);
            jo.Add("buttonstyle", buttonstyle);
            jo.Add("textboxborderstyle", textboxborderstyle);
            return jo;
        }

        public bool LoadFile(string pathname, string name, Theme defaultset = null)
        {
            try
            {
                JObject jo = JObject.Parse(File.ReadAllText(pathname), JToken.ParseOptions.AllowTrailingCommas | JToken.ParseOptions.CheckEOL);
                if (jo != null)
                {
                    return FromJSON(jo, name, defaultset);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Theme load file error {ex}");
            }
            return false;
        }

        public bool SaveFile(string pathname)
        {
            JObject jo = ToJSON();

            try
            {
                using (StreamWriter writer = new StreamWriter(pathname))
                {
                    writer.Write(jo.ToString(true));
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Theme save file error {ex}");
                return false;
            }
        }
    }
}
