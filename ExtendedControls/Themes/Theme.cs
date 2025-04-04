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

using BaseUtils;
using BaseUtils.Win32;
using BaseUtils.Win32Constants;
using QuickJSON;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExtendedControls
{
    [System.Diagnostics.DebuggerDisplay("{Name} {FontName} {FontSize} {FontStyle} {WindowsFrame}")]
    public class Theme
    {
        public static Theme Current { get; set; } = null;             // current theme

        public string Name { get; set; }
        public void SetCustom()
        { Name = "Custom"; }                                // set so custom..
        public bool IsCustom() { return Name.Equals("Custom"); }

        // AltFmt names are the names used previously in the CI. structure, and are the ones saved to the theme file, and passed to DLLs
        // using these disassociates the c# name from the JSON name for the future.

        [JsonCustomFormat("AltFmt","Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "form" })]
        public Color Form { get; set; } = SystemColors.Menu;


        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "button_back" })]
        public Color ButtonBackColor { get; set; } = Color.FromArgb(255, 225, 225, 225);
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "button_back2" })]
        public Color ButtonBackColor2 { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt","Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "button_border" })]
        public Color ButtonBorderColor { get; set; } = SystemColors.ActiveBorder;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "button_text" })]
        public Color ButtonTextColor { get; set; } = SystemColors.ControlText;
        public float ButtonGradientDirection { get; set; } = 90F;


        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_back" })]
        public Color ComboBoxBackColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_back2" })]
        public Color ComboBoxBackColor2 { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_border" })]
        public Color ComboBoxBorderColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_text" })]
        public Color ComboBoxTextColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_sliderback" })]
        public Color ComboBoxSliderBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_scrollarrow" })]
        public Color ComboBoxScrollArrow { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_scrollbutton" })]
        public Color ComboBoxScrollButton { get; set; } = Color.Transparent;
        public float ComboBoxGradientDirection { get; set; } = 90F;



        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "list_back" })]
        public Color ListBoxBackColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "list_back2" })]
        public Color ListBoxBackColor2 { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "list_border" })]
        public Color ListBoxBorderColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "list_text" })]
        public Color ListBoxTextColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "listbox_sliderback" })]
        public Color ListBoxSliderBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "listbox_scrollarrow" })]
        public Color ListBoxScrollArrow { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "listbox_scrollbutton" })]
        public Color ListBoxScrollButton { get; set; } = Color.Transparent;
        public float ListBoxGradientDirection { get; set; } = 90F;


        [JsonCustomFormat("AltFmt","Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_borderback" })]
        public Color GridBorderBack { get; set; } = SystemColors.Menu;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_bordertext" })]
        public Color GridBorderText { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt","Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_cellbackground" })]
        public Color GridCellBack { get; set; } = SystemColors.ControlLightLight;
        [JsonCustomFormat("AltFmt","Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_altcellbackground" })]
        public Color GridCellAltBack { get; set; } = SystemColors.Control;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_celltext" })]
        public Color GridCellText { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_altcelltext" })]
        public Color GridCellAltText { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_borderlines" })]
        public Color GridBorderLines { get; set; } = SystemColors.ControlDark;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_sliderback" })]
        public Color GridSliderBack { get; set; } = SystemColors.ControlLight;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_scrollarrow" })]
        public Color GridScrollArrow { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_scrollbutton" })]
        public Color GridScrollButton { get; set; } = SystemColors.Control;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_highlightback" })]
        public Color GridHighlightBack { get; set; } = Color.LightGreen;


        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "travelgrid_visited" })]
        public Color KnownSystemColor { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "travelgrid_nonvisited" })]
        public Color UnknownSystemColor { get; set; } = Color.Blue;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_back" })]
        public Color TextBlockBackColor { get; set; } = SystemColors.Window;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_fore" })]
        public Color TextBlockForeColor { get; set; } = SystemColors.WindowText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_highlight" })]
        public Color TextBlockHighlightColor { get; set; } = Color.Red;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_success" })]
        public Color TextBlockSuccessColor { get; set; } = Color.Green;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_border" })]
        public Color TextBlockBorderColor { get; set; } = SystemColors.Menu;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_sliderback" })]
        public Color TextBlockSliderBack { get; set; } = SystemColors.ControlLight;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_scrollarrow" })]
        public Color TextBlockScrollArrow { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_scrollbutton" })]
        public Color TextBlockScrollButton { get; set; } = SystemColors.Control;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_dropdownback" })]
        public Color TextBlockDropDownBackColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_dropdownback2" })]
        public Color TextBlockDropDownBackColor2 { get; set; } = Color.Transparent;


        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox" })]     // Fore colour of text
        public Color CheckBoxText { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_back" })]     // Back colour
        public Color CheckBoxBack { get; set; } = Color.Transparent;          
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_back2" })]     // Back colour
        public Color CheckBoxBack2 { get; set; } = Color.Transparent;           
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_tick" })]
        public Color CheckBoxTick { get; set; } = SystemColors.MenuHighlight;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_buttontick" })]
        public Color CheckBoxButtonTickedBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_buttontick2" })]
        public Color CheckBoxButtonTickedBack2 { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_border" })]
        public Color CheckBoxBorderColor { get; set; } = Color.Transparent;
        public float CheckBoxGradientDirection { get; set; } = 225F;
        public float CheckBoxTickSize { get; set; } = 0.75F;


        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "menu_back" })]
        public Color MenuBack { get; set; } = SystemColors.Menu;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "menu_fore" })]
        public Color MenuFore { get; set; } = SystemColors.MenuText;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "label" })]
        public Color LabelColor { get; set; } = SystemColors.MenuText;


        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "group_back" })]
        public Color GroupBack { get; set; } = SystemColors.Menu;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "group_back2" })]
        public Color GroupBack2 { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "group_text" })]
        public Color GroupFore { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "group_borderlines" })]
        public Color GroupBorder { get; set; } = SystemColors.ControlDark;
        public float GroupBoxGradientDirection { get; set; } = 90F;


        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabstrip_back" })]
        public Color TabStripBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabstrip_back2" })]
        public Color TabStripBack2 { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabstrip_text" })]
        public Color TabStripFore { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabstrip_selected" })]
        public Color TabStripSelected { get; set; } = Color.Transparent;
        public float TabStripGradientDirection { get; set; } = 0F;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabcontrol_borderlines" })]
        public Color TabcontrolBorder { get; set; } = SystemColors.ControlDark;


        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "toolstrip_back" })]
        public Color ToolstripBack { get; set; } = SystemColors.Control;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "toolstrip_border" })]
        public Color ToolstripBorder { get; set; } = SystemColors.Menu;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "menu_dropdownback" })]
        public Color ToolStripDropdownBack { get; set; } = SystemColors.ControlLightLight;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "menu_dropdownfore" })]
        public Color ToolStripDropdownFore { get; set; } = SystemColors.MenuText;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "s_panel" })]
        public Color SPanelColor { get; set; } = Color.DarkOrange;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "transparentcolorkey" })]
        public Color TransparentColorKey { get; set; } = Color.Green;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "chart1" })]
        public Color Chart1 { get; set; } = Color.Green;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "chart2" })]
        public Color Chart2 { get; set; } = Color.Red;
        [JsonCustomFormat("AltFmt","Std")] 
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "chart3" })]
        public Color Chart3 { get; set;} = Color.Blue;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "chart4" })]
        public Color Chart4 { get; set; } = Color.Orange;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "chart5" })]
        public Color Chart5 { get; set; } = Color.Purple;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "chart6" })]
        public Color Chart6 { get; set; } = Color.Aqua;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "chart7" })]
        public Color Chart7 { get; set; } = Color.Violet;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "chart8" })]
        public Color Chart8 { get; set; } = Color.Brown;

        // i= 0 to 7
        public Color GetChartColor(int i) { return i == 0 ? Chart1 : i == 1 ? Chart2 : i == 2 ? Chart3 : i == 3 ? Chart4 : i == 4 ? Chart5 : i == 5 ? Chart6 : i == 6 ? Chart7 : Chart8; }

        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "windowsframe" })]
        public bool WindowsFrame { get; set; } = true;
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "formopacity" })]
        public double Opacity { get; set; } = 100;
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "fontname" })]
        public string FontName { get; set; } = DefaultFont;
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "fontsize" })]
        public float FontSize { get; set; } = DefaultFontSize;
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;

        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "buttonstyle" })]
        public string ButtonStyle { get; set; } = ButtonstyleSystem;

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

        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textboxborderstyle" })]
        public string TextBoxBorderStyle { get; set; } = TextboxborderstyleFixed3D;

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

        // Scaling and direction values, exported using these names
        public float DialogFontScaling { get; set; } = 0.8f;
        public float MouseOverScaling { get; set; } = 1.3F;
        public float MouseSelectedScaling { get; set; } = 1.6F;
        public float DisabledScaling { get; set; } = 0.5F;

        public Theme(string name = "Unknown")      // windows default colours
        {
            this.Name = name;
        }

        [JsonIgnore]
        public Size IconSize { get { var ft = GetFont; return new Size(ft.ScalePixels(36), ft.ScalePixels(36)); } } // calculated rep scaled icon size to use

        public Theme(String n, Color form,
                                    Color butback, Color buttext, Color butborder, string bstyle,
                                    Color gridborderback, Color gridbordertext,
                                    Color gridcellback, Color gridaltcellback, Color gridcelltext, Color gridaltcelltext, Color gridhighlightback,
                                    Color gridborderlines,
                                    Color gridsliderback, Color gridscrollarrow, Color gridscrollbutton,
                                    Color travelgridnonvisited, Color travelgridvisited,
                                    Color textboxback, Color textboxfore, Color textboxhighlight, Color textboxsuccess, Color textboxborder, string tbbstyle,
                                    Color textboxsliderback, Color textboxscrollarrow, Color textboxscrollbutton,
                                    Color checkboxfore, Color checkboxtick, Color checkboxbuttontick,
                                    Color menuback, Color menufore, Color menudropbackback, Color menudropdownfore,
                                    Color label,
                                    Color groupboxback, Color groupboxtext, Color groupboxlines,
                                    Color tabborderlines,
                                    Color toolstripback, Color toolstripborder, Color _,
                                    Color sPanel, Color keycolor,
                                    bool wf, double op, string ft, float fs, FontStyle fstyle)             // ft = empty means don't set it
        {
            Name = n;
            Form = form;

            ButtonBackColor = butback; ButtonBackColor2 = butback.Multiply(0.6F);
            ButtonTextColor = buttext; ButtonBorderColor = butborder; 
            ButtonStyle = bstyle;

            ListBoxBackColor = butback; ListBoxBackColor2 = butback.Multiply(0.6F);
            ListBoxTextColor = buttext; ListBoxBorderColor = butborder;
            ListBoxSliderBack = textboxsliderback; ListBoxScrollArrow = textboxscrollarrow; ListBoxScrollButton = textboxscrollbutton;

            ComboBoxBackColor = butback; ComboBoxBackColor2 = butback.Multiply(0.6F);
            ComboBoxTextColor = buttext; ComboBoxBorderColor = butborder;
            ComboBoxSliderBack = textboxsliderback; ComboBoxScrollArrow = textboxscrollarrow; ComboBoxScrollButton = textboxscrollbutton;

            GridBorderBack = gridborderback; GridBorderText = gridbordertext;
            GridCellBack = gridcellback; GridCellAltBack = gridaltcellback; GridCellText = gridcelltext; GridCellAltText = gridaltcelltext; GridHighlightBack = gridhighlightback;
            GridBorderLines = gridborderlines;
            GridSliderBack = gridsliderback; GridScrollArrow = gridscrollarrow; GridScrollButton = gridscrollbutton;
            
            KnownSystemColor = travelgridvisited; UnknownSystemColor = travelgridnonvisited;
            
            TextBlockBackColor = textboxback; TextBlockForeColor = textboxfore; TextBlockHighlightColor = textboxhighlight; TextBlockSuccessColor = textboxsuccess; TextBlockBorderColor = textboxborder;
            TextBlockSliderBack = textboxsliderback; TextBlockScrollArrow = textboxscrollarrow; TextBlockScrollButton = textboxscrollbutton;
            TextBlockDropDownBackColor = TextBlockDropDownBackColor2 = butback;

            CheckBoxText = CheckBoxBack = checkboxfore; CheckBoxBack2 = CheckBoxBack.Multiply(0.6F); 
            CheckBoxTick = checkboxtick;
            CheckBoxButtonTickedBack = checkboxbuttontick;
            CheckBoxButtonTickedBack2 = CheckBoxButtonTickedBack.Multiply(0.6F);
            CheckBoxBorderColor = ButtonBorderColor;

            MenuBack = menuback; MenuFore = menufore; ToolStripDropdownBack = menudropbackback; ToolStripDropdownFore = menudropdownfore;
            LabelColor = label;
            GroupBack = GroupBack2= groupboxback; GroupFore = groupboxtext; GroupBorder = groupboxlines;

            TabStripBack = TabStripBack2 = form;
            TabStripFore = buttext;
            TabStripSelected = butback;

            TabcontrolBorder = tabborderlines;
            ToolstripBack = toolstripback; ToolstripBorder = toolstripborder;
            SPanelColor = sPanel; TransparentColorKey = keycolor;
            TextBoxBorderStyle = tbbstyle;
            WindowsFrame = Environment.OSVersion.Platform == PlatformID.Win32NT ? wf : true;
            Opacity = op; FontName = ft; FontSize = fs; FontStyle = fstyle;

            CheckTheme();       // will show up any missing sets debug
        }

        // copy constructor, takes a real copy, with overrides
        public Theme(Theme other, string newname = null, string newfont = null, float newFontSize = 0, double newopacity = -1, FontStyle? fstyle = null)
        {
            this.CopyPropertiesFields(other);       // dynamic copy over of all properties the lazy way
            if (newname != null)
                Name = newname;
            if (newfont != null)
                this.FontName = newfont;
            if (newFontSize > 0)
                this.FontSize = newFontSize;
            if (newopacity >= 0)
                this.Opacity = newopacity;
            if ( fstyle.HasValue)
                this.FontStyle = fstyle.Value;  
        }

        [JsonIgnore]
        public Font GetFont
        {
            get
            {
                return GetScaledFont(1.0f);
            }
        }


        [JsonIgnore]
        // dialogs get a slighly smaller font
        public Font GetDialogFont      
        {
            get
            {
                return GetScaledFont(DialogFontScaling);
            }
        }

        // scale dialog font, as long as font >= 12pt
        public Font GetDialogScaledFont(float scaling, float max = 999) 
        {
            if (FontSize >= 12)             // only if >=12 do we add on dialog scaling
                scaling *= DialogFontScaling;       

            return GetScaledFont(scaling, max);
        }

        public Font GetScaledFont(float scaling, float max = 999, bool underline = false, bool strikeout = false)
        {
            float fsize = Math.Max(minFontSize, FontSize * scaling);
            fsize = Math.Min(fsize, max);
            FontStyle style = FontStyle;
            if (underline)
                style |= FontStyle.Underline;
            else if ( strikeout )
                style |= FontStyle.Strikeout;
           // System.Diagnostics.Debug.WriteLine($"Theme Lookup font {FontName} {fsize} {style}");
            Font fnt = BaseUtils.FontLoader.GetFont(FontName, fsize, style);        // if it does not know the font, it will substitute Sans serif
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
        public bool Apply(Control ctrl, bool nowindowsborderoverride = false, float fontscale = 1.0f)
        {
            //System.Diagnostics.Debug.WriteLine($"Themer apply dialog {ctrl.Name} Font {GetDialogFont}");
            var ret = Apply(ctrl, GetScaledFont(fontscale), nowindowsborderoverride);
            //System.Diagnostics.Debug.WriteLine($"Finished dialog themeing to {ctrl.Name}");
            return ret;
        }

        internal bool Apply(Control form, Font fnt, bool nowindowsborderoverride = false)
        {
            UpdateControls(form, fnt, 0, nowindowsborderoverride);

            if ((ToolStripManager.Renderer as ThemeToolStripRenderer) == null)      // not installed..   install one
                ToolStripManager.Renderer = new ThemeToolStripRenderer();

            UpdateToolsStripRenderer(ToolStripManager.Renderer as ThemeToolStripRenderer);

            return WindowsFrame;
        }

        // if you want to theme itself.
        public void UpdateControls(Control ctrl, Font fnt, int level, bool formnoborderoverride = false)    // parent can be null
        {
            Control parent = ctrl.Parent;
            Type controltype = ctrl.GetType();

#if DEBUG
            //System.Diagnostics.Debug.WriteLine(new string(' ',256).Substring(0, level) + level + ":" + parent?.Name.ToString() + ":" + ctrl.Name.ToString() + " " + ctrl.ToString() + " " + fnt.ToString() + " c.fnt " + ctrl.Font);
            //System.Diagnostics.Debug.WriteLine("                             ".Substring(0, level) + level + ":" + (myControl.GetType().Name + ":" + myControl.Name??"") + " : " + myControl.GetHeirarchy(false));
            // System.Diagnostics.Debug.WriteLine("                             ".Substring(0, level) + level + ":" + myControl.GetType().Name + (myControl.Name.HasChars() ? " " + myControl.Name : "") + " : " + myControl.GetHeirarchy(false) + " " + myControl.Size);
#endif
            ctrl.SuspendLayout();


            bool dochildren = true;

            if ( ctrl is IThemeable)
            {
                dochildren = (ctrl as IThemeable).Theme(this,fnt);
            }

            else if (ctrl is Form form) // WINFORM
            {
                form.FormBorderStyle = (WindowsFrame && !formnoborderoverride) ? FormBorderStyle.Sizable : FormBorderStyle.None;
                form.Opacity = Opacity / 100;
                form.BackColor = Form;
                form.Font = fnt;
                //System.Diagnostics.Debug.WriteLine($"Form scaling now {f.CurrentAutoScaleDimensions} {f.AutoScaleDimensions} {f.CurrentAutoScaleFactor()}");
            }
            else if (ctrl is Panel)    // WINFORM, and ext panels rely on this if they don't need to theme
            {
                ctrl.BackColor = GroupBoxOverride(parent, Form);
                System.Diagnostics.Debug.WriteLine($"Theme panel {ctrl.Name}");
            }
            else if (ctrl is Label)        // WINFORM
            {
                ctrl.ForeColor = LabelColor;
                System.Diagnostics.Debug.WriteLine($"Theme label {ctrl.Name}");
            }
            else if (ctrl is Chart)    // WINFORM
            {
                System.Diagnostics.Debug.Assert(false, "Warning - Chart not allowed");
            }
            else if (ctrl is NumericUpDown)        // WINFORM
            {                                                                   // BACK colour does not work..
                ctrl.ForeColor = TextBlockForeColor;
            }
            else if (ctrl is TableLayoutPanel)     // WINFORM
            {
                ctrl.BackColor = GroupBoxOverride(parent, Form);
            }
            else if (ctrl is FlowLayoutPanel flp)  // WINFORM
            {
                flp.BackColor = GroupBoxOverride(parent, Form);
            }
            else if (ctrl is DataGridView dgv)  // WINFORM           
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
            else if (ctrl is PictureBox pb)    // WINFORM
            {
                if (pb.ContextMenuStrip != null)       // propergate font onto any attached context menus
                    pb.ContextMenuStrip.Font = fnt;
            }
            else if (ctrl is StatusStrip)  // WINFORM
            {
                ctrl.BackColor = Form;
                ctrl.ForeColor = LabelColor;
                dochildren = false;
            }
            else if (ctrl is ToolStrip)    // WINFORM MenuStrip is a tool stip
            {
                ctrl.Font = fnt;       // Toolstrips don't seem to inherit Forms font, so force

                foreach (ToolStripItem i in ((ToolStrip)ctrl).Items)   // make sure any buttons have the button back colour set
                {
                    if (i is ToolStripButton || i is ToolStripDropDownButton)
                    {           // theme the back colour, this is the way its done.. not via the tool strip renderer
                        i.BackColor = ButtonBackColor;
                    }
                    else if (i is ToolStripTextBox)
                    {
                        i.ForeColor = TextBlockForeColor;
                        i.BackColor = TextBlockBackColor;
                    }
                }
            }
            else if (ctrl is TreeView) // WINFORM
            {
                TreeView tctrl = ctrl as TreeView;
                tctrl.ForeColor = TextBlockForeColor;
                tctrl.BackColor = TextBlockBackColor;
            }
            else if (ctrl is Button wfb) // WINFORM
            {
                System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wfb.ForeColor = ButtonTextColor;
                wfb.BackColor = ButtonBackColor;
                wfb.FlatStyle = FlatStyle.Popup;
                wfb.FlatAppearance.BorderColor = ButtonBorderColor;
            }
            else if (ctrl is RadioButton wrb) // WINFORM
            {
                System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wrb.ForeColor = ButtonTextColor;
            }
            else if (ctrl is GroupBox wgb)// WINFORM
            {
                System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wgb.ForeColor = GroupFore;
                wgb.BackColor = GroupBack;
            }
            else if (ctrl is CheckBox wchb) // WINFORM
            {
                wchb.BackColor = GroupBoxOverride(parent, Form);

                System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                if (wchb.Appearance == Appearance.Button)
                {
                    wchb.ForeColor = ButtonTextColor;
                }
                else
                {
                    wchb.ForeColor = CheckBoxText;
                }

            }
            else if (ctrl is TextBox wtb) // WINFORM
            {
                System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wtb.ForeColor = TextBlockForeColor;
                wtb.BackColor = TextBlockBackColor;
                wtb.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (ctrl is RichTextBox wrtb) // WINFORM
            {
                System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wrtb.ForeColor = TextBlockForeColor;
                wrtb.BackColor = TextBlockBackColor;
            }
            else if (ctrl is ComboBox wcb) // WINFORM
            {
                System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wcb.ForeColor = ButtonTextColor;
                wcb.BackColor = ButtonBackColor;
            }
            // these are not themed or are sub parts of other controls
            else if (ctrl is UserControl || ctrl is SplitContainer || ctrl is SplitterPanel || ctrl is HScrollBar || ctrl is VScrollBar) // WINFORM
            {
            }
            else
            {
                System.Diagnostics.Trace.WriteLine($"Themer {ctrl.Name}:{controltype.Name} from {parent.Name} Unknown control!");
            }

            //System.Diagnostics.Debug.WriteLine("                  " + level + " Control " + myControl.Name + " " + myControl.Location + " " + myControl.Size);

            //if (myControl.Name == "textBoxSystem")  System.Diagnostics.Debug.WriteLine($"Theme begin sub controls of {myControl.Name} ");

            if (dochildren)
            {
                foreach (Control subC in ctrl.Controls)
                {
                    //if (myControl.Name == "textBoxSystem")  System.Diagnostics.Debug.WriteLine($"Theme sub controls of {myControl.Name} sub {subC.GetType().Name}");
                    UpdateControls(subC, fnt, level + 1);
                }
            }

            ctrl.ResumeLayout();
            //if (myControl.Name == "textBoxSystem")  System.Diagnostics.Debug.WriteLine($"Theme Control {myControl.Name} to {myControl.Bounds}");
        }

        private void UpdateToolsStripRenderer(ThemeToolStripRenderer toolstripRenderer)
        {
            Color menuback = MenuBack;
            bool toodark = (menuback.GetBrightness() < 0.1);

            toolstripRenderer.colortable.colMenuText = MenuFore;              // and the text
            toolstripRenderer.colortable.colMenuSelectedText = ToolStripDropdownFore;
            toolstripRenderer.colortable.colMenuBackground = menuback;
            toolstripRenderer.colortable.colMenuBarBackground = Form;
            toolstripRenderer.colortable.colMenuBorder = ButtonBorderColor;
            toolstripRenderer.colortable.colMenuSelectedBack = ToolStripDropdownBack;
            toolstripRenderer.colortable.colMenuHighlightBorder = ButtonBorderColor;
            toolstripRenderer.colortable.colMenuHighlightBack = toodark ? ToolStripDropdownBack.Multiply(0.7F) : MenuBack.Multiply(1.3F);        // whole menu back

            Color menuchecked = toodark ? ToolStripDropdownBack.Multiply(0.8F) : MenuBack.Multiply(1.5F);        // whole menu back

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

        // if a parent above is a group box behind the control, use the group box back color, else use the form colour
        public Color GroupBoxOverride(Control parent, Color d)      
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

        public JObject ToJSON(bool altnames = true)
        {
            var jo = JToken.FromObject(this, false, membersearchflags:System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.Public,
                                        setname: altnames ? "AltFmt" : "Std",
                                        customconvert: (o) => { return JToken.CreateToken(System.Drawing.ColorTranslator.ToHtml((Color)o)); }).Object();
            return jo;
        }

        public static Theme FromJSON(JToken jo, bool altnames = true)
        {
            Object jconv = jo.ToObjectProtected(typeof(Theme), false, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, new Theme(),
                                    null, altnames ? "AltFmt" : "Std", 
                                    (tt, o) => { if (o is string) return System.Drawing.ColorTranslator.FromHtml((string)o); else return Color.Orange; 
                                    });

            if (jconv != null)
            {
                Theme ret = (Theme)jconv;
                ret.CheckTheme();
                return ret;
            }

            return null;
        }

        public void CheckTheme()
        {
            float sc = 0.6F;

            Replace(nameof(ButtonBackColor2), ButtonBackColor.Multiply(sc));       // in order

            Replace(nameof(ComboBoxBackColor), ButtonBackColor);
            Replace(nameof(ComboBoxBackColor2), ButtonBackColor2);
            Replace(nameof(ComboBoxBorderColor), ButtonBorderColor);
            Replace(nameof(ComboBoxTextColor), ButtonTextColor);
            Replace(nameof(ComboBoxSliderBack), TextBlockSliderBack);
            Replace(nameof(ComboBoxScrollArrow), TextBlockScrollArrow);
            Replace(nameof(ComboBoxScrollButton), TextBlockScrollButton);

            Replace(nameof(ListBoxBackColor), ButtonBackColor);
            Replace(nameof(ListBoxBackColor2), ButtonBackColor2);
            Replace(nameof(ListBoxBorderColor), ButtonBorderColor);
            Replace(nameof(ListBoxTextColor), ButtonTextColor);
            Replace(nameof(ListBoxSliderBack), TextBlockSliderBack);
            Replace(nameof(ListBoxScrollArrow), TextBlockScrollArrow);
            Replace(nameof(ListBoxScrollButton), TextBlockScrollButton);

            Replace(nameof(CheckBoxBack), CheckBoxText);
            Replace(nameof(CheckBoxBack2), CheckBoxText.Multiply(sc));
            Replace(nameof(CheckBoxButtonTickedBack), CheckBoxTick);
            Replace(nameof(CheckBoxButtonTickedBack2), CheckBoxTick.Multiply(sc));
            Replace(nameof(CheckBoxBorderColor), ButtonBorderColor);

            Replace(nameof(GroupBack2), GroupBack.Multiply(sc));

            Replace(nameof(TabStripBack), Form);
            Replace(nameof(TabStripBack2), Form);
            Replace(nameof(TabStripFore), ButtonTextColor);
            Replace(nameof(TabStripSelected), ButtonBackColor);

            Replace(nameof(TextBlockDropDownBackColor), TextBlockBackColor);
            Replace(nameof(TextBlockDropDownBackColor2), TextBlockBackColor);
        }

        private void Replace(string nameofitem, Color y)
        {
            PropertyInfo p = typeof(Theme).GetProperty(nameofitem);
            Color curv = (Color)p.GetValue(this);
            if (curv == Color.Transparent)
            {
                System.Diagnostics.Debug.WriteLine($"Theme missing item {nameofitem} setting to {y}");
                p.SetValue(this, y);
            }
        }

        public static Theme LoadFile(string pathname, string usethisname = null)
        {
            JToken jo = pathname.ReadJSONFile();
            if ( jo != null )
            {
                Theme thm = FromJSON(jo);
                if (thm != null)
                {
                    if (usethisname != null)
                        thm.Name = usethisname;

                    return thm;
                }
            }

            return null;
        }

        public bool SaveFile(string pathname)
        {
            return FileHelpers.TryWriteToFile(pathname, ToJSON().ToString(true));
        }

        private const string DefaultFont = "Microsoft Sans Serif";
        private const float DefaultFontSize = 8.25F;
        private const float minFontSize = 4;
    }

    // Controls which can theme implement this
    interface IThemeable
    {
        bool Theme(Theme t,Font fnt);
    }
}
