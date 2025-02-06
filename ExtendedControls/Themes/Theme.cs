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

        public string Name { get; set; }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"form"} )]
        public Color Form { get { return colors[CI.form]; } }       // in enum order..
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"button_back"} )]
        public Color ButtonBackColor { get { return colors[CI.button_back]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"button_border"} )]
        public Color ButtonBorderColor { get { return colors[CI.button_border]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"button_text"} )]
        public Color ButtonTextColor { get { return colors[CI.button_text]; } }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_borderback"} )]
        public Color GridBorderBack { get { return colors[CI.grid_borderback]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_bordertext"} )]
        public Color GridBorderText { get { return colors[CI.grid_bordertext]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_cellbackground"} )]
        public Color GridCellBack { get { return colors[CI.grid_cellbackground]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_altcellbackground"} )]
        public Color GridCellAltBack { get { return colors[CI.grid_altcellbackground]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_celltext"} )]
        public Color GridCellText { get { return colors[CI.grid_celltext]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_altcelltext"} )]
        public Color GridCellAltText { get { return colors[CI.grid_altcelltext]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_borderlines"} )]
        public Color GridBorderLines { get { return colors[CI.grid_borderlines]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_sliderback"} )]
        public Color GridSliderBack { get { return colors[CI.grid_sliderback]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_scrollarrow"} )]
        public Color GridScrollArrow { get { return colors[CI.grid_scrollarrow]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_scrollbutton"} )]
        public Color GridScrollButton { get { return colors[CI.grid_scrollbutton]; } }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"travelgrid_visited"} )]
        public Color KnownSystemColor { get { return colors[CI.travelgrid_visited]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"travelgrid_nonvisited"} )]
        public Color UnknownSystemColor { get { return colors[CI.travelgrid_nonvisted]; } }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"textbox_back"} )]
        public Color TextBackColor { get { return colors[CI.textbox_back]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"textbox_fore"} )]
        public Color TextBlockColor { get { return colors[CI.textbox_fore]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"textbox_highlight"} )]
        public Color TextBlockHighlightColor { get { return colors[CI.textbox_highlight]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"textbox_success"} )]
        public Color TextBlockSuccessColor { get { return colors[CI.textbox_success]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"textbox_border"} )]
        public Color TextBlockBorderColor { get { return colors[CI.textbox_border]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"textbox_sliderback"} )]
        public Color TextBlockSliderBack { get { return colors[CI.textbox_sliderback]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"textbox_scrollarrow"} )]
        public Color TextBlockScrollArrow { get { return colors[CI.textbox_scrollarrow]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"textbox_scrollbutton"} )]
        public Color TextBlockScrollButton { get { return colors[CI.textbox_scrollbutton]; } }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"checkbox"} )]
        public Color CheckBox { get { return colors[CI.checkbox]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"checkbox_tick"} )]
        public Color CheckBoxTick { get { return colors[CI.checkbox_tick]; } }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"menu_back"} )]
        public Color MenuBack { get { return colors[CI.menu_back]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"menu_fore"} )]
        public Color MenuFore { get { return colors[CI.menu_fore]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"menu_dropdownback"} )]
        public Color MenuDropdownBack { get { return colors[CI.menu_dropdownback]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"menu_dropdownfore"} )]
        public Color MenuDropdownFore { get { return colors[CI.menu_dropdownfore]; } }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"label"} )]
        public Color LabelColor { get { return colors[CI.label]; } }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"group_back"} )]
        public Color GroupBack { get { return colors[CI.group_back]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"group_text"} )]
        public Color GroupFore { get { return colors[CI.group_text]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"group_borderlines"} )]
        public Color GroupBorder { get { return colors[CI.group_borderlines]; } }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"tabcontrol_borderlines"} )]
        public Color TabcontrolBorder { get { return colors[CI.tabcontrol_borderlines]; } }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"toolstrip_back"} )]
        public Color ToolstripBack { get { return colors[CI.toolstrip_back]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"toolstrip_border"} )]
        public Color ToolstripBorder { get { return colors[CI.toolstrip_border]; } }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"s_panel"} )]
        public Color SPanelColor { get { return colors[CI.s_panel]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"transparentcolorkey"} )]
        public Color TransparentColorKey { get { return colors[CI.transparentcolorkey]; } }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"grid_highlightback"} )]
        public Color GridHighlightBack { get { return colors[CI.grid_highlightback]; } }

        public bool WindowsFrame { get; set; }
        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"formOpacity"} )]
        public double Opacity { get; set; }
        public string FontName { get; set; }
        public float FontSize { get; set; }

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"buttonstyle"} )]
        public string ButtonStyle { get; set; }

        public static readonly string[] ButtonStyles = "System Flat Gradient".Split();

        [JsonIgnore]
        public FlatStyle ButtonFlatStyle { get {  return ButtonStyle.Equals(ButtonStyles[0]) ? 
                            FlatStyle.System : ButtonStyle.Equals(ButtonStyles[1]) ? FlatStyle.Flat : FlatStyle.Popup; }}
        [JsonIgnore]
        public bool IsButtonSystemStyle => ButtonStyle.Equals(ButtonStyles[0]);
        [JsonIgnore]
        public bool IsButtonFlatStyle => ButtonStyle.Equals(ButtonStyles[1]);

        public static string ButtonstyleSystem = ButtonStyles[0];
        public static string ButtonstyleFlat = ButtonStyles[1];
        public static string ButtonstyleGradient = ButtonStyles[2];

        [JsonNameAttribute(new string[] {"OldName"}, new string[] {"textboxborderstyle"} )]
        public string TextBoxBorderStyle { get; set; }

        public static readonly string[] TextboxBorderStyles = "None FixedSingle Fixed3D Colour".Split();
        [JsonIgnore]
        public BorderStyle TextBoxStyle
        {
            get
            {
                return TextBoxBorderStyle.Equals(TextboxBorderStyles[1]) ? BorderStyle.FixedSingle :
                    TextBoxBorderStyle.Equals(TextboxBorderStyles[2]) ? BorderStyle.Fixed3D : BorderStyle.None;
            }
        }

        [JsonIgnore]
        public bool IsTextBoxColourStyle => TextBoxBorderStyle.Equals(TextboxBorderStyles[3]);
        [JsonIgnore]
        public bool IsTextBoxNoneStyle => TextBoxBorderStyle.Equals(TextboxBorderStyles[0]);

        public static string TextboxborderstyleFixedSingle = TextboxBorderStyles[1];
        public static string TextboxborderstyleFixed3D = TextboxBorderStyles[2];
        public static string TextboxborderstyleColor = TextboxBorderStyles[3];

        [JsonIgnore]
        public Size IconSize { get { var ft = GetFont; return new Size(ft.ScalePixels(36), ft.ScalePixels(36)); } } // calculated rep scaled icon size to use


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
        private Dictionary<CI, Color> colors { get; set; }       // dictionary of colors, indexed by CI.

        private static float minFontSize = 4;

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

            ButtonStyle = bstyle; TextBoxBorderStyle = tbbstyle;
            WindowsFrame = Environment.OSVersion.Platform == PlatformID.Win32NT ? wf : true;
            Opacity = op; FontName = ft; FontSize = fs;
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
            ButtonStyle = ButtonstyleSystem;
            TextBoxBorderStyle = TextboxborderstyleFixed3D;
            WindowsFrame = true;
            Opacity = 100;
            FontName = DefaultFont;
            FontSize = DefaultFontSize;
        }

        // copy constructor, takes a real copy, with overrides
        public Theme(Theme other, string newname = null, string newfont = null, float newFontSize = 0, double opaque = 0)
        {
            Name = (newname != null) ? newname : other.Name;
            FontName = (newfont != null) ? newfont : other.FontName;
            FontSize = (newFontSize != 0) ? newFontSize : other.FontSize;
            WindowsFrame = other.WindowsFrame; Opacity = other.Opacity;
            ButtonStyle = other.ButtonStyle; TextBoxBorderStyle = other.TextBoxBorderStyle;
            Opacity = (opaque > 0) ? opaque : other.Opacity;
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

        [JsonIgnore]
        public Font GetFont
        {
            get
            {
                return GetFontSizeStyle(FontSize, FontStyle.Regular);
            }
        }

        private const float dialogscaling = 0.8f;

        [JsonIgnore]
        public Font GetDialogFont       // dialogs get a slighly smaller font
        {
            get
            {       // we don't scale down fonts < 12 since they are v.small already
                float fsize = FontSize >= 12 ? (FontSize * dialogscaling) : FontSize;
                return GetFontSizeStyle(fsize, FontStyle.Regular);
            }
        }

        public Font GetScaledFont(float scaling, FontStyle fs = FontStyle.Regular, float max = 999)
        {
            return GetFontSizeStyle(Math.Min(FontSize * scaling, max), fs);
        }

        public Font GetDialogScaledFont(float scaling, FontStyle fs = FontStyle.Regular, float max = 999)
        {
            float fsize = FontSize >= 12 ? (FontSize * dialogscaling) : FontSize;
            fsize = Math.Min(fsize, max);
            return GetFontSizeStyle(fsize * scaling, fs);
        }

        private Font GetFontSizeStyle(float size, FontStyle fs)
        {
            if (FontName.Equals("") || FontSize < minFontSize)
            {
                FontName = "Microsoft Sans Serif";          // in case schemes were loaded badly
                FontSize = 8.25F;
            }

            Font fnt = BaseUtils.FontLoader.GetFont(FontName, Math.Max(size, 4f), fs);        // if it does not know the font, it will substitute Sans serif
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
                f.Opacity = Opacity / 100;
                f.BackColor = Form;
                f.Font = fnt;
                //System.Diagnostics.Debug.WriteLine($"Form scaling now {f.CurrentAutoScaleDimensions} {f.AutoScaleDimensions} {f.CurrentAutoScaleFactor()}");
            }
            else if (myControl is CompositeAutoScaleButton || myControl is CompositeButton)        // these are not themed, they have a bitmap, and the backcolour is kept
            {
            }
            else if (myControl is ExtRichTextBox rtb)
            {
                rtb.TextBoxForeColor = TextBlockColor;
                rtb.TextBoxBackColor = TextBackColor;

                rtb.BorderColor = Color.Transparent;       // default for text box border styles
                rtb.BorderStyle = TextBoxStyle;

                if (IsTextBoxColourStyle)
                    rtb.BorderColor = TextBlockBorderColor;

                if (IsButtonSystemStyle)
                    rtb.ScrollBarFlatStyle = FlatStyle.System;
                else
                {
                    rtb.ScrollBarBackColor = TextBackColor;
                    rtb.ScrollBarSliderColor = TextBlockSliderBack;
                    rtb.ScrollBarBorderColor = rtb.ScrollBarThumbBorderColor =
                                rtb.ScrollBarArrowBorderColor = TextBlockBorderColor;
                    rtb.ScrollBarArrowButtonColor = rtb.ScrollBarThumbButtonColor = TextBlockScrollButton;
                    rtb.ScrollBarMouseOverButtonColor = TextBlockScrollButton.Multiply(mouseoverscaling);
                    rtb.ScrollBarMousePressedButtonColor = TextBlockScrollButton.Multiply(mouseselectedscaling);
                    rtb.ScrollBarForeColor = TextBlockScrollArrow;
                    rtb.ScrollBarFlatStyle = FlatStyle.Popup;
                }

                if (rtb.ContextMenuStrip != null)      // propegate font
                    rtb.ContextMenuStrip.Font = fnt;

                rtb.Invalidate();
                rtb.PerformLayout();

                dochildren = false;
            }
            else if (myControl is ExtTextBox tb)
            {
                tb.ForeColor = TextBlockColor;
                tb.BackColor = TextBackColor;
                tb.BackErrorColor = TextBlockHighlightColor;
                tb.ControlBackground = TextBackColor; // previously, but not sure why, GroupBoxOverride(parent, Form);
                tb.AutoSize = true;

                tb.BorderColor = Color.Transparent;
                tb.BorderStyle = TextBoxStyle;

                if (IsTextBoxColourStyle)
                    tb.BorderColor = TextBlockBorderColor;

                if (IsTextBoxNoneStyle)
                    tb.AutoSize = false;                                                 // with no border, the autosize clips the bottom of chars..

                if (myControl is ExtTextBoxAutoComplete || myControl is ExtDataGridViewColumnAutoComplete.CellEditControl) // derived from text box
                {
                    ExtTextBoxAutoComplete actb = myControl as ExtTextBoxAutoComplete;
                    actb.DropDownBackgroundColor = ButtonBackColor;
                    actb.DropDownBorderColor = TextBlockBorderColor;
                    actb.DropDownScrollBarButtonColor = TextBlockScrollButton;
                    actb.DropDownScrollBarColor = TextBlockSliderBack;
                    actb.DropDownMouseOverBackgroundColor = ButtonBackColor.Multiply(mouseoverscaling);
                    actb.FlatStyle = ButtonFlatStyle;
                }

                ThemeButton(tb.EndButton);
                tb.EndButton.FlatAppearance.BorderColor =             // override some of them to make back of button disappear
                tb.EndButton.BackColor = TextBackColor;
                tb.EndButton.ButtonColorScaling = tb.EndButton.ButtonDisabledScaling = 1.0F;

                tb.Invalidate();

                dochildren = false;
            }
            else if (myControl is ExtButton)
            {
                ThemeButton(myControl);
            }
            else if (myControl is ExtTabControl etc)
            {
                if (IsButtonSystemStyle) // not system
                    etc.FlatStyle = FlatStyle.System;
                else
                {
                    etc.TabControlBorderColor = TabcontrolBorder.Multiply(0.6F);
                    etc.TabControlBorderBrightColor = TabcontrolBorder;
                    etc.TabNotSelectedBorderColor = TabcontrolBorder.Multiply(0.4F);
                    etc.TabNotSelectedColor = ButtonBackColor;
                    etc.TabSelectedColor = ButtonBackColor.Multiply(mouseselectedscaling);
                    etc.TabMouseOverColor = ButtonBackColor.Multiply(mouseoverscaling);
                    etc.TextSelectedColor = ButtonTextColor;
                    etc.TextNotSelectedColor = ButtonTextColor.Multiply(0.8F);
                    etc.SetStyle(ButtonFlatStyle, tsc);
                }
            }
            else if (myControl is ExtListBox elb)
            {
                elb.ForeColor = ButtonTextColor;
                elb.ItemSeperatorColor = ButtonBorderColor;

                if (IsButtonSystemStyle)
                    elb.FlatStyle = FlatStyle.System;
                else
                {
                    elb.BackColor = ButtonBackColor;
                    elb.BorderColor = ButtonBorderColor;
                    elb.ScrollBarButtonColor = TextBlockScrollButton;
                    elb.ScrollBarColor = TextBlockSliderBack;
                    elb.FlatStyle = ButtonFlatStyle;
                }

                elb.Repaint();            // force a repaint as the individual settings do not by design.

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
            else if (myControl is MultiPipControl mpc)
            {
                mpc.ForeColor = ButtonTextColor;
                mpc.PipColor = ButtonTextColor;
                mpc.HalfPipColor = ButtonTextColor.MultiplyBrightness(0.6f);
                mpc.BorderColor = GridBorderLines;
            }
            else if (myControl is ExtPanelResizer)      // Resizers only show when no frame is on
            {
                myControl.Visible = !WindowsFrame;
            }
            else if (myControl is ExtPanelDropDown pdd)
            {
                pdd.ForeColor = ButtonTextColor;
                pdd.SelectionMarkColor = pdd.ForeColor;
                pdd.BackColor = pdd.SelectionBackColor = ButtonBackColor;
                pdd.BorderColor = ButtonBorderColor;
                pdd.MouseOverBackgroundColor = ButtonBackColor.Multiply(mouseoverscaling);
                pdd.ScrollBarButtonColor = TextBlockScrollButton;
                pdd.ScrollBarColor = TextBlockSliderBack;
                pdd.FlatStyle = FlatStyle.Popup;
            }
            else if (myControl is ExtComboBox ecb)
            {
                ecb.ForeColor = ButtonTextColor;

                if (IsButtonSystemStyle)
                    ecb.FlatStyle = FlatStyle.System;
                else
                {
                    ecb.BackColor = ecb.DropDownBackgroundColor = ButtonBackColor;
                    ecb.BorderColor = ButtonBorderColor;
                    ecb.MouseOverBackgroundColor = ButtonBackColor.Multiply(mouseoverscaling);
                    ecb.ScrollBarButtonColor = TextBlockScrollButton;
                    ecb.ScrollBarColor = TextBlockSliderBack;
                    ecb.FlatStyle = ButtonFlatStyle;
                }

                ecb.Repaint();            // force a repaint as the individual settings do not by design.

                dochildren = false;
            }
            else if (myControl is NumericUpDown)
            {                                                                   // BACK colour does not work..
                myControl.ForeColor = TextBlockColor;
            }
            else if (myControl is ExtButtonDrawn bd)
            {
                bd.BackColor = Form;
                bd.ForeColor = LabelColor;
                bd.MouseOverColor = LabelColor.Multiply(mouseoverscaling);
                bd.MouseSelectedColor = LabelColor.Multiply(mouseselectedscaling);
                bd.BorderWidth = 2;
                bd.BorderColor = GridBorderLines;

                System.Drawing.Imaging.ColorMap colormap = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images
                colormap.OldColor = Color.White;                                                        // white is defined as the forecolour
                colormap.NewColor = bd.ForeColor;
                bd.SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap });
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
            else if (myControl is FlowLayoutPanel flp)
            {
                flp.BackColor = paneldebugmode ? Color.Red : GroupBoxOverride(parent, Form);
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
            else if (myControl is ExtGroupBox gb)
            {
                gb.ForeColor = GroupFore;
                gb.BackColor = GroupBack;
                gb.BorderColor = GroupBorder;
                gb.FlatStyle = FlatStyle.Flat;           // always in Flat, always apply our border.
            }
            else if (myControl is ExtCheckBox cb)
            {
                cb.BackColor = GroupBoxOverride(parent, Form);

                if (cb.Appearance == Appearance.Button)
                {
                    cb.ForeColor = ButtonTextColor;
                    cb.MouseOverColor = ButtonBackColor.Multiply(mouseoverscaling);
                    cb.CheckColor = ButtonBackColor.Multiply(0.9f);
                }
                else
                {
                    cb.ForeColor = CheckBox;
                    cb.CheckBoxColor = CheckBox;
                    cb.CheckBoxInnerColor = CheckBox.Multiply(1.5F);
                    cb.MouseOverColor = CheckBox.Multiply(1.4F);
                    cb.TickBoxReductionRatio = 0.75f;
                    cb.CheckColor = CheckBoxTick;
                }

                cb.FlatStyle = ButtonFlatStyle;

                if (cb.Image != null)
                {
                    System.Drawing.Imaging.ColorMap colormap = new System.Drawing.Imaging.ColorMap();
                    colormap.OldColor = Color.White;                                                        // white is defined as the forecolour
                    colormap.NewColor = cb.ForeColor;
                    cb.SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap });

                    cb.ImageLayout = ImageLayout.Stretch;
                }
            }
            else if (myControl is ExtRadioButton rb)
            {
                rb.FlatStyle = ButtonFlatStyle;
                rb.BackColor = GroupBoxOverride(parent, Form);
                rb.ForeColor = CheckBox;
                rb.RadioButtonColor = CheckBox;
                rb.RadioButtonInnerColor = CheckBox.Multiply(1.5F);
                rb.SelectedColor = rb.BackColor.Multiply(0.75F);
                rb.MouseOverColor = CheckBox.Multiply(1.4F);
            }
            else if (myControl is DataGridView dgv)                     // we theme this directly
            {
                dgv.EnableHeadersVisualStyles = false;            // without this, the colours for the grid are not applied.

                dgv.RowHeadersDefaultCellStyle.BackColor = GridBorderBack;
                dgv.RowHeadersDefaultCellStyle.ForeColor = GridBorderText;
                dgv.RowHeadersDefaultCellStyle.SelectionForeColor = GridBorderText;
                dgv.RowHeadersDefaultCellStyle.SelectionBackColor = GridBorderBack;

                dgv.ColumnHeadersDefaultCellStyle.BackColor = GridBorderBack;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = GridBorderText;
                dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = GridBorderText;
                dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridBorderBack;

                dgv.DefaultCellStyle.BackColor = GridCellBack;
                dgv.DefaultCellStyle.ForeColor = GridCellText;
                dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.ForeColor;
                dgv.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.BackColor;

                dgv.BackgroundColor = GroupBoxOverride(parent, Form);

                dgv.AlternatingRowsDefaultCellStyle.BackColor = GridCellAltBack;
                dgv.AlternatingRowsDefaultCellStyle.ForeColor = GridCellAltText;

                dgv.BorderStyle = BorderStyle.None;        // can't control the color of this, turn it off

                dgv.GridColor = GridBorderLines;
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.CellType == typeof(DataGridViewComboBoxCell))
                    {   // Need to set flat style for colours to take on combobox cells.
                        DataGridViewComboBoxColumn cbocol = (DataGridViewComboBoxColumn)col;
                        cbocol.FlatStyle = ButtonFlatStyle;
                    }
                }

                if (dgv.ContextMenuStrip != null)       // propergate font onto any attached context menus
                    dgv.ContextMenuStrip.Font = fnt;
            }
            else if (myControl is ExtScrollBar esb)
            {
                //System.Diagnostics.Debug.WriteLine("VScrollBarCustom Theme " + level + ":" + parent.Name.ToString() + ":" + myControl.Name.ToString() + " " + myControl.ToString() + " " + parentcontroltype.Name);
                if (IsButtonSystemStyle)
                    esb.FlatStyle = FlatStyle.System;
                else
                {
                    esb.BorderColor = GridBorderLines;
                    esb.BackColor = Form;
                    esb.SliderColor = GridSliderBack;
                    esb.BorderColor = esb.ThumbBorderColor = esb.ArrowBorderColor = GridBorderLines;
                    esb.ArrowButtonColor = esb.ThumbButtonColor = GridScrollButton;
                    esb.MouseOverButtonColor = GridScrollButton.Multiply(mouseoverscaling);
                    esb.MousePressedButtonColor = GridScrollButton.Multiply(mouseselectedscaling);
                    esb.ForeColor = GridScrollArrow;
                    esb.FlatStyle = ButtonFlatStyle;
                }
            }
            else if (myControl is ExtNumericUpDown nud)
            {
                nud.TextBoxForeColor = TextBlockColor;
                nud.TextBoxBackColor = TextBackColor;
                nud.BorderColor = TextBlockBorderColor;

                Color c1 = TextBlockScrollButton;
                nud.updown.BackColor = c1;
                nud.updown.ForeColor = TextBlockScrollArrow;
                nud.updown.BorderColor = ButtonBorderColor;
                nud.updown.MouseOverColor = c1.Multiply(mouseoverscaling);
                nud.updown.MouseSelectedColor = c1.Multiply(mouseselectedscaling);
                nud.Invalidate();

                dochildren = false;
            }
            else if (myControl is CheckedIconUserControl iuc)
            {
                iuc.BackColor = Form;
                iuc.ForeColor = TextBlockColor;
                iuc.BorderColor = GridBorderLines;
                iuc.BackColor = Form;
                iuc.SliderColor = GridSliderBack;
                iuc.BorderColor = iuc.ThumbBorderColor =
                        iuc.ArrowBorderColor = GridBorderLines;
                Color c1 = GridScrollButton;
                iuc.ArrowButtonColor = iuc.ThumbButtonColor = c1;
                iuc.MouseOverButtonColor = c1.Multiply(mouseoverscaling);
                iuc.MousePressedButtonColor = c1.Multiply(mouseselectedscaling);
                iuc.CheckBoxColor = CheckBox;
                iuc.CheckBoxInnerColor = CheckBox.Multiply(1.5F);
                iuc.MouseOverCheckboxColor = CheckBox.Multiply(0.75F);
                iuc.MouseOverLabelColor = CheckBox.Multiply(0.75F);
                iuc.TickBoxReductionRatio = 0.75f;
                iuc.CheckColor = CheckBoxTick;
            }
            else if (myControl is PictureBox pb)
            {
                if (pb.ContextMenuStrip != null)       // propergate font onto any attached context menus
                    pb.ContextMenuStrip.Font = fnt;
            }
            else if (myControl is ExtDateTimePicker dtp)
            {
                dtp.BorderColor = GridBorderLines;
                dtp.ForeColor = TextBlockColor;
                dtp.TextBackColor = TextBackColor;
                dtp.BackColor = Form;
                dtp.SelectedColor = TextBlockColor.MultiplyBrightness(0.6F);
                dtp.checkbox.FlatStyle = ButtonFlatStyle;
                dtp.checkbox.TickBoxReductionRatio = 0.75f;
                dtp.checkbox.ForeColor = CheckBox;
                dtp.checkbox.CheckBoxColor = CheckBox;
                Color inner = CheckBox.Multiply(1.5F);
                if (inner.GetBrightness() < 0.1)        // double checking
                    inner = Color.Gray;
                dtp.checkbox.CheckBoxInnerColor = inner;
                dtp.checkbox.CheckColor = CheckBoxTick;
                dtp.checkbox.MouseOverColor = CheckBox.Multiply(1.4F);

                dtp.updown.BackColor = ButtonBackColor;
                dtp.updown.BorderColor = GridBorderLines;
                dtp.updown.ForeColor = TextBlockColor;
                dtp.updown.MouseOverColor = CheckBox.Multiply(1.4F);
                dtp.updown.MouseSelectedColor = CheckBox.Multiply(1.5F);

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
            else if (myControl is TabStrip ts)
            {
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

            else if (myControl is CompassControl compassControl)
            {
                compassControl.ForeColor = TextBlockColor;
                compassControl.StencilColor = TextBlockColor;
                compassControl.CentreTickColor = TextBlockColor.Multiply(1.2F);
                compassControl.BugColor = TextBlockColor.Multiply(0.8F);
                compassControl.BackColor = Form;
            }

            else if (myControl is LabelData ld)
            {
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

            if (IsButtonSystemStyle) // system
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
                ctrl.FlatStyle = ButtonFlatStyle;
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

            WindowsFrame = jo["windowsframe"].Bool(defaultset.WindowsFrame);
            Opacity = jo["formopacity"].Double(defaultset.Opacity);
            FontName = jo["fontname"].Str(defaultset.FontName);
            FontSize = jo["fontsize"].Float(defaultset.FontSize);
            ButtonStyle = jo["buttonstyle"].Str(defaultset.ButtonStyle);
            TextBoxBorderStyle = jo["textboxborderstyle"].Str(defaultset.TextBoxBorderStyle);

            return true;
        }

        public JObject ToJSON()
        {
            JObject jo = new JObject();

            foreach (CI ck in colors.Keys)
            {
                jo.Add(ck.ToString(), System.Drawing.ColorTranslator.ToHtml(colors[ck]));
            }

            jo.Add("windowsframe", WindowsFrame);
            jo.Add("formopacity", Opacity);
            jo.Add("fontname", FontName);
            jo.Add("fontsize", FontSize);
            jo.Add("buttonstyle", ButtonStyle);
            jo.Add("textboxborderstyle", TextBoxBorderStyle);

            return jo;
        }


        public JObject ToJSON222()
        {
            var jo = JToken.FromObject(this, false, membersearchflags:System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.Public,setname:"OldName" ).Object();
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
        public bool SaveFile2(string pathname)
        {
            JObject jo = ToJSON222();
            return BaseUtils.FileHelpers.TryWriteToFile(pathname, jo.ToString(true));
        }
    }
}
