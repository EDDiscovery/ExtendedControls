/** Copyright 2016-2025 EDDiscovery development team
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

        #region Properties

        public string Name { get; set; }
        public void SetCustom()
        { Name = "Custom"; }                                // set so custom..
        public bool IsCustom() { return Name.Equals("Custom"); }

        // AltFmt names are the names used previously in the CI. structure, and are the ones saved to the theme file, and passed to DLLs
        // using these disassociates the c# name from the JSON name for the future.

        [JsonCustomFormat("AltFmt","Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "form" })]                     //existing
        public Color Form { get; set; } = SystemColors.Menu;

        //-------------

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "button_back" })]      //existing
        public Color ButtonBackColor { get; set; } = Color.FromArgb(255, 225, 225, 225);
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "button_back2" })]
        public Color ButtonBackColor2 { get; set; } = Color.Transparent;
        public float ButtonBackGradientDirection { get; set; } = 90F;

        [JsonCustomFormat("AltFmt","Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "button_border" })]        //existing
        public Color ButtonBorderColor { get; set; } = SystemColors.ActiveBorder;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "button_text" })]      //existing
        public Color ButtonTextColor { get; set; } = SystemColors.ControlText;

        //-------------

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_back" })]
        public Color ComboBoxBackColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_back2" })]
        public Color ComboBoxBackColor2 { get; set; } = Color.Transparent;
        public float ComboBoxBackAndDropDownGradientDirection { get; set; } = 90F;                 // background of combo box and drop down

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_border" })]
        public Color ComboBoxBorderColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_text" })]
        public Color ComboBoxTextColor { get; set; } = Color.Transparent;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_sliderback" })]
        public Color ComboBoxDropDownSliderBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_sliderback2" })]    
        public Color ComboBoxDropDownSliderBack2 { get; set; } = Color.Transparent;
        public float ComboBoxDropDownSliderGradientDirection { get; set; } = 90F;      

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_scrollarrowback" })]
        public Color ComboBoxScrollArrowBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_scrollarrowback2" })]   
        public Color ComboBoxScrollArrowBack2 { get; set; } = Color.Transparent;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_scrollbuttonback" })]
        public Color ComboBoxScrollButtonBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_scrollbuttonback2" })]      
        public Color ComboBoxScrollButtonBack2 { get; set; } = Color.Transparent;
        public float ComboBoxDropDownScrollButtonGradientDirection { get; set; } = 90F;       

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "combobox_scrollarrow" })]      
        public Color ComboBoxScrollArrow { get; set; } = Color.Transparent;

        //-------------

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "list_back" })]
        public Color ListBoxBackColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "list_back2" })]
        public Color ListBoxBackColor2 { get; set; } = Color.Transparent;
        public float ListBoxBackGradientDirection { get; set; } = 90F;      // tbd designer
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
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "listbox_sliderback2" })]
        public Color ListBoxSliderBack2 { get; set; } = Color.Transparent;
        public float ListBoxSliderGradientDirection { get; set; } = 90F;            // tbd designer
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "listbox_scrollarrowback" })]
        public Color ListBoxScrollArrowBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "listbox_scrollarrowback2" })]
        public Color ListBoxScrollArrowBack2 { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "listbox_scrollbuttonback" })]
        public Color ListBoxScrollButtonBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "listbox_scrollbuttonback2" })]
        public Color ListBoxScrollButtonBack2 { get; set; } = Color.Transparent;
        public float ListBoxScrollButtonGradientDirection { get; set; } = 90F;            // tbd designer

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "listbox_scrollarrow" })]
        public Color ListBoxScrollArrow { get; set; } = Color.Transparent;



        [JsonCustomFormat("AltFmt","Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_borderback" })]      //existing
        public Color GridBorderBack { get; set; } = SystemColors.Menu;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_bordertext" })]      //existing
        public Color GridBorderText { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt","Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_cellbackground" })]  //existing
        public Color GridCellBack { get; set; } = SystemColors.ControlLightLight;
        [JsonCustomFormat("AltFmt","Std")]  
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_altcellbackground" })]   //existing
        public Color GridCellAltBack { get; set; } = SystemColors.Control;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_celltext" })]    //existing
        public Color GridCellText { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_altcelltext" })] //existing
        public Color GridCellAltText { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_borderlines" })] //existing
        public Color GridBorderLines { get; set; } = SystemColors.ControlDark;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_sliderback" })]  //existing
        public Color GridSliderBack { get; set; } = SystemColors.ControlLight;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_sliderback2" })]    
        public Color GridSliderBack2 { get; set; } = Color.Transparent;
        public float GridSliderGradientDirection { get; set; } = 90F;       // tbd editor

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_scrollarrowback" })]
        public Color GridScrollArrowBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_scrollarrowback2" })] 
        public Color GridScrollArrow2Back { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_scrollbutton" })]    //existing
        public Color GridScrollButtonBack { get; set; } = SystemColors.Control;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_scrollbuttonback2" })]      
        public Color GridScrollButtonBack2 { get; set; } = Color.Transparent;
        public float GridScrollButtonGradientDirection { get; set; } = 90F;        // tbd editor
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_highlightback" })]       //existing
        public Color GridHighlightBack { get; set; } = Color.LightGreen;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "grid_scrollarrow" })]      //existing
        public Color GridScrollArrow { get; set; } = SystemColors.ControlLight;


        //-------------

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_back" })]     //existing
        public Color TextBlockBackColor { get; set; } = SystemColors.Window;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_fore" })]     //existing
        public Color TextBlockForeColor { get; set; } = SystemColors.WindowText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_highlight" })]        //existing
        public Color TextBlockHighlightColor { get; set; } = Color.Red;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_success" })]      //existing
        public Color TextBlockSuccessColor { get; set; } = Color.Green;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_border" })]       //existing
        public Color TextBlockBorderColor { get; set; } = SystemColors.Menu;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_border2" })]
        public Color TextBlockBorderColor2 { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_sliderback" })]   //existing
        public Color TextBlockSliderBack { get; set; } = SystemColors.ControlLight;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_sliderback2" })]
        public Color TextBlockSliderBack2 { get; set; } = Color.Transparent;
        public float TextBlockSliderGradientDirection { get; set; } = 90F;       // slider back

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_scrollarrowback" })]
        public Color TextBlockScrollArrowBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_scrollarrowback2" })]
        public Color TextBlockScrollArrowBack2 { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_scrollbutton" })]     //existing
        public Color TextBlockScrollButtonBack { get; set; } = SystemColors.Control;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_scrollbuttonback2" })]
        public Color TextBlockScrollButtonBack2 { get; set; } = Color.Transparent;
        public float TextBlockScrollButtonGradientDirection { get; set; } = 90F;        // thumb

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_scrollarrow" })]      //existing
        public Color TextBlockScrollArrow { get; set; } = SystemColors.ControlLight;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_dropdownback" })]
        public Color TextBlockDropDownBackColor { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textbox_dropdownback2" })]
        public Color TextBlockDropDownBackColor2 { get; set; } = Color.Transparent;
        public float TextBlockDropDownBackGradientDirection { get; set; } = 90F;             // Listbox TBD DESIGNER

        //-------------

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox" })]     // Fore colour of text
        public Color CheckBoxText { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_back" })]     // Back colour
        public Color CheckBoxBack { get; set; } = Color.Transparent;          
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_back2" })]     // Back colour
        public Color CheckBoxBack2 { get; set; } = Color.Transparent;
        public float CheckBoxBackGradientDirection { get; set; } = 225F;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_tick" })]
        public Color CheckBoxTick { get; set; } = SystemColors.MenuHighlight;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_buttontick" })]
        public Color CheckBoxButtonTickedBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_buttontick2" })]
        public Color CheckBoxButtonTickedBack2 { get; set; } = Color.Transparent;
        public float CheckBoxButtonGradientDirection { get; set; } = 90F;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "checkbox_border" })]
        public Color CheckBoxBorderColor { get; set; } = Color.Transparent;
        public float CheckBoxTickSize { get; set; } = 0.75F;

        //-----------------

        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] GroupBack { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
        public float GroupBoxGradientDirection { get; set; } = 90F;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "group_text" })]
        public Color GroupFore { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "group_borderlines" })]
        public Color GroupBorder { get; set; } = SystemColors.ControlDark;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "group_borderlines2" })]
        public Color GroupBorder2 { get; set; } = Color.Transparent;

        //----------------

        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] TabStripBack { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
        public float TabStripGradientDirection { get; set; } = 0F;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabstrip_text" })]
        public Color TabStripFore { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabstrip_selected" })]
        public Color TabStripSelected { get; set; } = Color.Transparent;

        //-------------------

        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] TabControlBack { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
        public float TabControlBackGradientDirection { get; set; } = 0F;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabcontrol_text" })]
        public Color TabControlText { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabcontrol_buttonback" })]
        public Color TabControlButtonBack { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabcontrol_buttonback2" })]
        public Color TabControlButtonBack2 { get; set; } = Color.Transparent;
        public float TabControlTabGradientDirection { get; set; } = 90F;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabcontrol_borderlines" })]
        public Color TabControlBorder { get; set; } = SystemColors.ControlDark;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabcontrol_borderlines2" })]
        public Color TabControlBorder2 { get; set; } = Color.Transparent;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "tabcontrol_pageback" })]
        public Color TabControlPageBack { get; set; } = Color.Transparent;

        //-------------------

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "menu_back" })]
        public Color MenuBack { get; set; } = SystemColors.Menu;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "menu_fore" })]
        public Color MenuFore { get; set; } = SystemColors.MenuText;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "menu_dropdownfore" })]        // repurposed to selected text, which it was used for.
        public Color MenuSelectedText { get; set; } = SystemColors.MenuText;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "menu_dropdownback" })]
        public Color MenuSelectedBack { get; set; } = SystemColors.ControlLightLight;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "toolstrip_back" })]
        public Color MenuDropDownBack { get; set; } = SystemColors.Control;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "toolstrip_border" })]
        public Color MenuBorder { get; set; } = SystemColors.Menu;


        //-------------------

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "label" })]
        public Color LabelColor { get; set; } = SystemColors.MenuText;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "s_panel" })]
        public Color SPanelColor { get; set; } = Color.DarkOrange;

        //-----------------

        const int PanelsSets = 8;

        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] Panel1 { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] Panel2 { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] Panel3 { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] Panel4 { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] Panel5 { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] Panel6 { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] Panel7 { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
        [JsonCustomFormatArray("AltFmt", "Std")]
        public Color[] Panel8 { get; set; } = new Color[4] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };

        [JsonCustomArrayLength(PanelsSets)]     // previously, only 4 entries, force using QuickJSON 2.9 to min size
        public float[] PanelGradientDirection { get; set; } = new float[PanelsSets];        // all set to zero, default

        // i = 1 to N
        public Color[] GetPanelSet(int i) { return i == 8 ? Panel8 : i == 7 ? Panel7 : i == 6 ? Panel6 : i == 5 ? Panel5 : i == 4 ? Panel4 : i == 3 ? Panel3 : i == 2 ? Panel2 : Panel1; }
        public float GetPanelDirection(int i) { return PanelGradientDirection[i - 1]; }

        //-----------------

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "travelgrid_visited" })]
        public Color KnownSystemColor { get; set; } = SystemColors.MenuText;
        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "travelgrid_nonvisited" })]
        public Color UnknownSystemColor { get; set; } = Color.Blue;

        [JsonCustomFormat("AltFmt", "Std")]
        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "transparentcolorkey" })]
        public Color TransparentColorKey { get; set; } = Color.Green;

        //-----------------
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

        //------------------

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
        [JsonIgnore]
        public bool IsButtonGradientStyle => ButtonStyle.Equals(ButtonStyles[2]);

        public static string ButtonstyleSystem = ButtonStyles[0];
        public static string ButtonstyleFlat = ButtonStyles[1];
        public static string ButtonstyleGradient = ButtonStyles[2];

        [JsonNameAttribute(new string[] { "AltFmt" }, new string[] { "textboxborderstyle" })]
        public string TextBoxBorderStyle { get; set; } = TextboxborderstyleFixed3D;

        public static readonly string[] TextboxBorderStyles = "None FixedSingle Fixed3D Colour".Split();

        [JsonIgnore]
        public bool IsTextBoxBorderColour => TextBoxBorderStyle.Equals(TextboxBorderStyles[3]);
        [JsonIgnore]
        public bool IsTextBoxBorderSingle => TextBoxBorderStyle.Equals(TextboxBorderStyles[1]);
        [JsonIgnore]
        public bool IsTextBoxBorder3D => TextBoxBorderStyle.Equals(TextboxBorderStyles[2]);
        [JsonIgnore]
        public bool IsTextBoxBorderNone => TextBoxBorderStyle.Equals(TextboxBorderStyles[0]);

        public static string TextboxborderstyleFixedSingle = TextboxBorderStyles[1];
        public static string TextboxborderstyleFixed3D = TextboxBorderStyles[2];
        public static string TextboxborderstyleColor = TextboxBorderStyles[3];

        [JsonIgnore]
        public BorderStyle TextBoxStyle
        {
            get
            {
                return TextBoxBorderStyle.Equals(TextboxBorderStyles[1]) ? BorderStyle.FixedSingle :
                    TextBoxBorderStyle.Equals(TextboxBorderStyles[2]) ? BorderStyle.Fixed3D : BorderStyle.None;
            }
        }

        // scroll bar apperance
        public bool SkinnyScrollBars { get; set; } = false;
        public bool SkinnyScrollBarsHaveButtons { get; set; } = false;
        public int ScrollBarWidth() { return ScrollBarWidth(GetFont,SkinnyScrollBars); }
        public static int ScrollBarWidth(Font f, bool skinny) 
        {
            int stdsize = f.ScaleScrollbar();
            int mult = f.GetHeight() < 17 ? 11 : 10;
            if (skinny)
            {
                int size = stdsize * mult / 16;
               // System.Diagnostics.Debug.WriteLine($"Scroll bar Skinny {f.GetHeight()} stdsize {stdsize} mult {mult} size {size}");
                return size;
            }
            else
            {
               // System.Diagnostics.Debug.WriteLine($"Scroll bar Normal {f.GetHeight()} stdsize {stdsize}");
                return stdsize;
            }
        }

        // Scaling and direction values, exported using these names
        public float DialogFontScaling { get; set; } = 0.8f;
        public float MouseOverScaling { get; set; } = 1.3F;
        public float MouseSelectedScaling { get; set; } = 1.6F;
        public float DisabledScaling { get; set; } = 0.5F;

        [JsonIgnore]
        public Size IconSize { get { var ft = GetFont; return new Size(ft.ScalePixels(36), ft.ScalePixels(36)); } } // calculated rep scaled icon size to use

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
            else if (strikeout)
                style |= FontStyle.Strikeout;
            // System.Diagnostics.Debug.WriteLine($"Theme Lookup font {FontName} {fsize} {style}");
            Font fnt = BaseUtils.FontLoader.GetFont(FontName, fsize, style);        // if it does not know the font, it will substitute Sans serif
            return fnt;
        }

        #endregion

        #region Construction

        // create raw, with apr 25 new colours still set up as transparent. Used by ToObject.
        public Theme()              
        {
            Name = "Unset";
        }

        // windows default colours as set up above, with new apr 25 colours filled in by fillinnewoptions
        public Theme(string name)         
        {
            this.Name = name;
            FillInNewOptions(false);         // Fill in any missing transparent entries from other info
        }

        // Create a theme using colours. 
        public Theme(String n, Color form,
                                    Color buttonbackcolor_textblockdropdownbackcolor, Color buttontextcolor_listbox_combobox_tab, Color buttonbordercolor_listbox_combobox, string bstyle,
                                    Color gridborderback, Color gridbordertext,
                                    Color gridcellback, Color gridaltcellback, Color gridcelltext, Color gridaltcelltext, Color gridhighlightback,
                                    Color gridborderlines,
                                    Color gridsliderback, Color gridscrollarrow, Color gridscrollbuttonback,
                                    Color unknownsystemcolor, Color knownsystemcolor,
                                    Color textboxbackcolor, Color textboxforecolor, Color textboxhighlightcolor, Color textboxsuccesscolor, 
                                    Color textboxbordercolor, string textboxborderstyle,
                                    Color textboxsliderback_listbox_combo, Color textboxscrollarrow_listbox_combo, Color textboxscrollbutton_listbox_combo,
                                    Color checkboxtext_checkboxback, Color checkboxtick, Color checkboxbuttontickedback,
                                    Color menuback, Color menufore, Color menuselectedback, Color menuselectedtext,
                                    Color labelcolor,
                                    Color groupback, Color groupfore, Color groupborder,
                                    Color tabcontrolborder,
                                    Color menudropdownback, Color menuborder,
                                    Color spanelcolor, Color transparentcolorkey,
                                    bool windowsframe, double opacity, 
                                    string fontname, float fontsize, FontStyle fontstyle, 
                                    float scalingsecondcolour = 0.6F)             
        {
            Name = n;
            Form = form;

            ButtonBackColor = buttonbackcolor_textblockdropdownbackcolor; ButtonBackColor2 = buttonbackcolor_textblockdropdownbackcolor.Multiply(scalingsecondcolour);
            ButtonTextColor = buttontextcolor_listbox_combobox_tab; ButtonBorderColor = buttonbordercolor_listbox_combobox; 
            ButtonStyle = bstyle;

            TextBlockBackColor = textboxbackcolor;
            TextBlockForeColor = textboxforecolor;
            TextBlockHighlightColor = textboxhighlightcolor;
            TextBlockSuccessColor = textboxsuccesscolor;
            TextBlockBorderColor = textboxbordercolor;
            TextBlockBorderColor2 = textboxbordercolor.Multiply(scalingsecondcolour);
            TextBlockSliderBack = textboxsliderback_listbox_combo;
            TextBlockSliderBack2 = textboxsliderback_listbox_combo.Multiply(scalingsecondcolour);
            TextBlockScrollArrowBack = textboxscrollbutton_listbox_combo;
            TextBlockScrollArrowBack2 = textboxscrollbutton_listbox_combo.Multiply(scalingsecondcolour);
            TextBlockScrollButtonBack = textboxscrollbutton_listbox_combo;
            TextBlockScrollButtonBack2 = textboxscrollbutton_listbox_combo.Multiply(scalingsecondcolour);
            TextBlockScrollArrow = textboxscrollarrow_listbox_combo;
            TextBlockDropDownBackColor = TextBlockDropDownBackColor2 = buttonbackcolor_textblockdropdownbackcolor;

            ListBoxBackColor = buttonbackcolor_textblockdropdownbackcolor; ListBoxBackColor2 = buttonbackcolor_textblockdropdownbackcolor.Multiply(scalingsecondcolour);
            ListBoxTextColor = buttontextcolor_listbox_combobox_tab; ListBoxBorderColor = buttonbordercolor_listbox_combobox;
            ListBoxSliderBack = textboxsliderback_listbox_combo;
            ListBoxSliderBack2 = textboxsliderback_listbox_combo.Multiply(scalingsecondcolour);
            ListBoxScrollArrowBack = textboxscrollbutton_listbox_combo;
            ListBoxScrollArrowBack2 = textboxscrollbutton_listbox_combo.Multiply(scalingsecondcolour);
            ListBoxScrollButtonBack = textboxscrollbutton_listbox_combo;
            ListBoxScrollButtonBack2 = textboxscrollbutton_listbox_combo.Multiply(scalingsecondcolour);
            ListBoxScrollArrow = textboxscrollarrow_listbox_combo;

            GridBorderBack = gridborderback; GridBorderText = gridbordertext;
            GridCellBack = gridcellback; GridCellAltBack = gridaltcellback;
            GridCellText = gridcelltext; GridCellAltText = gridaltcelltext; GridHighlightBack = gridhighlightback;
            GridBorderLines = gridborderlines;
            GridSliderBack = gridsliderback; GridSliderBack2 = gridsliderback.Multiply(scalingsecondcolour);
            GridScrollArrowBack = gridscrollbuttonback; GridScrollArrow2Back = gridscrollbuttonback.Multiply(scalingsecondcolour);
            GridScrollButtonBack = gridscrollbuttonback; GridScrollButtonBack2 = gridscrollbuttonback.Multiply(scalingsecondcolour);
            GridScrollArrow = gridscrollarrow;

            ComboBoxBackColor = buttonbackcolor_textblockdropdownbackcolor; ComboBoxBackColor2 = buttonbackcolor_textblockdropdownbackcolor.Multiply(scalingsecondcolour);
            ComboBoxTextColor = buttontextcolor_listbox_combobox_tab; ComboBoxBorderColor = buttonbordercolor_listbox_combobox;
            ComboBoxDropDownSliderBack = textboxsliderback_listbox_combo; ComboBoxDropDownSliderBack2 = textboxsliderback_listbox_combo.Multiply(scalingsecondcolour);
            ComboBoxScrollArrowBack = textboxscrollarrow_listbox_combo; ComboBoxScrollArrowBack2 = textboxscrollarrow_listbox_combo.Multiply(scalingsecondcolour);
            ComboBoxScrollButtonBack = textboxscrollbutton_listbox_combo; ComboBoxScrollButtonBack2 = textboxscrollbutton_listbox_combo.Multiply(scalingsecondcolour);
            ComboBoxScrollArrow = textboxscrollarrow_listbox_combo;

            KnownSystemColor = knownsystemcolor; UnknownSystemColor = unknownsystemcolor;

            CheckBoxText = CheckBoxBack = checkboxtext_checkboxback; CheckBoxBack2 = CheckBoxBack.Multiply(scalingsecondcolour); 
            CheckBoxTick = checkboxtick;
            CheckBoxButtonTickedBack = checkboxbuttontickedback;
            CheckBoxButtonTickedBack2 = CheckBoxButtonTickedBack.Multiply(scalingsecondcolour);
            CheckBoxBorderColor = ButtonBorderColor;

            MenuBack = menuback; MenuFore = menufore; MenuSelectedBack = menuselectedback; MenuSelectedText = menuselectedtext;
            MenuDropDownBack = menudropdownback; MenuBorder = menuborder;

            LabelColor = labelcolor;

            GroupBack[0] = GroupBack[1] = GroupBack[2] = GroupBack[3] = groupback; 
            GroupFore = groupfore;
            GroupBorder = groupborder; GroupBorder2 = groupborder.Multiply(scalingsecondcolour);

            TabStripBack[0] = TabStripBack[1] = TabStripBack[2] = TabStripBack[3] = form;
            TabStripFore = buttontextcolor_listbox_combobox_tab;
            TabStripSelected = buttonbackcolor_textblockdropdownbackcolor;

            TabControlBack[0] = TabControlBack[1] = TabControlBack[2] = TabControlBack[3] = form;
            TabControlText = buttontextcolor_listbox_combobox_tab;
            TabControlButtonBack = buttonbackcolor_textblockdropdownbackcolor;
            TabControlButtonBack2 = buttonbackcolor_textblockdropdownbackcolor.Multiply(scalingsecondcolour);
            TabControlBorder = tabcontrolborder; TabControlBorder2 = tabcontrolborder.Multiply(scalingsecondcolour);      // darker colour
            TabControlPageBack = form;

            // default colours for panel1 is form
            for (int i = 0; i < Panel1.Length; i++)
                Panel1[i] = form;

            // the rest are set to gentle flow of colours

            Panel2[0] = textboxbackcolor;
            Panel2[1] = Color.Red.Multiply(0.3f);
            Panel2[2] = Color.Red.Multiply(0.4f);
            Panel2[3] = textboxbackcolor;
            Panel3[0] = textboxbackcolor;
            Panel3[1] = Color.Orange.Multiply(0.3f);
            Panel3[2] = Color.Orange.Multiply(0.4f);
            Panel3[3] = textboxbackcolor;
            Panel4[0] = textboxbackcolor;
            Panel4[1] = Color.Yellow.Multiply(0.3f);
            Panel4[2] = Color.Yellow.Multiply(0.4f);
            Panel4[3] = textboxbackcolor;
            Panel5[0] = textboxbackcolor;
            Panel5[1] = Color.Magenta.Multiply(0.3f);
            Panel5[2] = Color.Magenta.Multiply(0.4f);
            Panel5[3] = textboxbackcolor;
            Panel6[0] = textboxbackcolor;
            Panel6[1] = Color.Green.Multiply(0.3f);
            Panel6[2] = Color.Green.Multiply(0.4f);
            Panel6[3] = textboxbackcolor;
            Panel7[0] = textboxbackcolor;
            Panel7[1] = Color.Blue.Multiply(0.3f);
            Panel7[2] = Color.Blue.Multiply(0.4f);
            Panel7[3] = textboxbackcolor;
            Panel8[0] = textboxbackcolor;
            Panel8[1] = Color.Gold.Multiply(0.3f);
            Panel8[2] = Color.Gold.Multiply(0.4f);
            Panel8[3] = textboxbackcolor;

            SPanelColor = spanelcolor; TransparentColorKey = transparentcolorkey;
            TextBoxBorderStyle = textboxborderstyle;
            WindowsFrame = Environment.OSVersion.Platform == PlatformID.Win32NT ? windowsframe : true;
            Opacity = opacity; FontName = fontname; FontSize = fontsize; FontStyle = fontstyle;

#if DEBUG
            int count = FillInNewOptions(false);       // will show up any missing sets debug - should be none 
            System.Diagnostics.Debug.Assert(count == 0);
#endif
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

        #endregion

        #region Theming

        public bool ApplyStd(Control ctrl, bool nowindowsborderoverride = false)      // normally a form, but can be a control, applies to this and ones below
        {
             System.Diagnostics.Debug.WriteLine($"Themer apply standard {ctrl.Name} {ctrl.GetType().Name} Font {GetFont}");
            var ret = Apply(ctrl, GetFont, nowindowsborderoverride);
            System.Diagnostics.Debug.WriteLine($"Finish standard themeing to {ctrl.Name}");
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

        internal bool Apply(Control form, Font fnt, bool nowindowsborderoverride = false, bool applytoolstriprendering = true)
        {
            UpdateControls(form, fnt, 0, nowindowsborderoverride);
            ToolStripRendererTheming(applytoolstriprendering);
            return WindowsFrame;
        }

        // if you want to theme a specific control
        public void UpdateControls(Control ctrl, Font fnt, int level, bool formnoborderoverride = false)    // parent can be null
        {
            Control parent = ctrl.Parent;
            Type controltype = ctrl.GetType();

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
                //System.Diagnostics.Debug.WriteLine($"Theme form {form.BackColor} scaling {form.CurrentAutoScaleDimensions} {form.AutoScaleDimensions} {form.CurrentAutoScaleFactor()}");
            }
            else if ( ctrl is TabPage)      // TabPage is a panel, get to it first
            {
                ctrl.BackColor = TabControlPageBack;
            }
            else if (ctrl is Panel)    // WINFORM, and ext panels rely on this if they don't need to theme
            {
                ctrl.BackColor = GroupBoxOverride(parent, Form);
            }
            else if (ctrl is Label)        // WINFORM
            {
                ctrl.ForeColor = LabelColor;
                ctrl.BackColor = Color.Transparent; // seems to need it
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
            }
            else if (ctrl is TreeView) // WINFORM
            {
                TreeView tctrl = ctrl as TreeView;
                tctrl.ForeColor = TextBlockForeColor;
                tctrl.BackColor = TextBlockBackColor;
            }
            else if (ctrl is Button wfb) // WINFORM
            {
                //System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wfb.ForeColor = ButtonTextColor;
                wfb.BackColor = ButtonBackColor;
                wfb.FlatStyle = FlatStyle.Popup;
                wfb.FlatAppearance.BorderColor = ButtonBorderColor;
            }
            else if (ctrl is RadioButton wrb) // WINFORM
            {
                //System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wrb.ForeColor = ButtonTextColor;
            }
            else if (ctrl is GroupBox wgb)// WINFORM
            {
                //System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wgb.ForeColor = GroupFore;
                wgb.BackColor = GroupBack[0];
            }
            else if (ctrl is CheckBox wchb) // WINFORM
            {
                wchb.BackColor = GroupBoxOverride(parent, Form);

                //System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
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
                //System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wtb.ForeColor = TextBlockForeColor;
                wtb.BackColor = TextBlockBackColor;
                wtb.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (ctrl is RichTextBox wrtb) // WINFORM
            {
                //System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wrtb.ForeColor = TextBlockForeColor;
                wrtb.BackColor = TextBlockBackColor;
            }
            else if (ctrl is ComboBox wcb) // WINFORM
            {
                //System.Diagnostics.Trace.WriteLine("Themer " + ctrl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
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

          //  System.Diagnostics.Debug.WriteLine($"{new string(' ', 256).Substring(0, level)} {level} : {ctrl.Name} <- {parent?.Name} : {ctrl.GetType().Name} ChildThemed {dochildren}");

            if (dochildren)
            {
                foreach (Control subC in ctrl.Controls)
                {
                    //if (myControl.Name == "textBoxSystem")  System.Diagnostics.Debug.WriteLine($"Theme sub controls of {myControl.Name} sub {subC.GetType().Name}");
                    UpdateControls(subC, fnt, level + 1);
                }
            }

            ctrl.ResumeLayout();
        }

        // see the ToolStripCustomColourTable for meaning of these colours
        public void ToolStripRendererTheming(bool on)
        {
            if (on)
            {
                if ((ToolStripManager.Renderer as ThemeToolStripRenderer) == null)      // not installed..   install one
                    ToolStripManager.Renderer = new ThemeToolStripRenderer();

                var toolstripRenderer = ToolStripManager.Renderer as ThemeToolStripRenderer;

                bool foretoodark = (MenuFore.GetBrightness() < 0.1);

                // horizontal menu bar
                toolstripRenderer.colortable.colMenuBarBackground = MenuBack;
                toolstripRenderer.colortable.colMenuText = MenuFore;
                toolstripRenderer.colortable.colMenuSelectedOrPressedText = MenuSelectedText;

                toolstripRenderer.colortable.colMenuTopLevelHoveredBack = MenuSelectedBack.Multiply(0.8f);
                toolstripRenderer.colortable.colMenuTopLevelSelectedBack = MenuSelectedBack;
                toolstripRenderer.colortable.colMenuItemSelectedBack = MenuSelectedBack;

                toolstripRenderer.colortable.colMenuDropDownBackground = MenuDropDownBack;
                toolstripRenderer.colortable.colMenuLeftMarginBackground = MenuDropDownBack.Multiply(1.2F);

                // we can't seem to set checkmark colour to anything but black so make sure the Fore is not dark            
                toolstripRenderer.colortable.colMenuItemCheckedBackground = foretoodark ? MenuBack : MenuFore;
                toolstripRenderer.colortable.colMenuItemHoverOverCheckmarkBackground = foretoodark ? MenuBack : MenuFore.Multiply(MouseSelectedScaling);

                Color border = MenuBorder;

                toolstripRenderer.colortable.colMenuDropDownBorder = border;
                toolstripRenderer.colortable.colMenuHighlightedPartBorder = border;

                toolstripRenderer.colortable.colToolStripBackground = MenuBack;
                toolstripRenderer.colortable.colToolStripBorder = border;
                toolstripRenderer.colortable.colToolStripSeparator = border;

                toolstripRenderer.colortable.colToolStripButtonPressedBack = MenuSelectedBack.Multiply(1.2f);
                toolstripRenderer.colortable.colToolStripButtonSelectedBack = MenuSelectedBack;
                toolstripRenderer.colortable.colToolStripButtonPressedSelectedBorder = border.Multiply(MouseSelectedScaling);
                toolstripRenderer.colortable.colToolBarGripper = MenuFore;
                toolstripRenderer.colortable.colToolStripOverflowButton = MenuFore;
            }
            else
                ToolStripManager.Renderer = null;
        }

        #endregion

        #region Save Load

        public JObject ToJSON(bool altnames = true)
        {
            var ctrl = JToken.GetMemberAttributeSettings(typeof(Theme), "AltFmt", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            var jo = JToken.FromObject(this, false, membersearchflags: System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public,
                                        setname: altnames ? "AltFmt" : "Std",
                                        customconverter: (mi,o) => {
                                            return JToken.CreateToken(System.Drawing.ColorTranslator.ToHtml((Color)o)); }).Object();
            return jo;
        }

        // load from json, normally taking the name from json but can be overridden, and using altnames by default
        public static Theme FromJSON(JToken jo, string usename = null, bool altnames = true, bool emitdebug = false)
        {
            // convert, can create, using the Theme() constructor to leave new colours transparent if the json does not have it

            bool lackingthemepanelcolours = false;

            // so the original theme for gradients did not define the other panel from 2 onwards, so detect if its set up like currently

            int count = 0;
            for (int i = 2; i <= PanelsSets; i++)       // all 2..7 must be black
            {
                var array = jo[$"Panel{i}"].Array();
                // if not there, or all black
                if (array == null || (array[0].Str() == "Black" && array[0].Str() == "Black" && array[0].Str() == "Black" && array[0].Str() == "Black"))
                    count++;
            }

            if (count == PanelsSets - 1)    // all are black
            {
                lackingthemepanelcolours = true;
            }

            Object jconv = jo.ToObjectProtected(typeof(Theme), false, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, new Theme(),
                                    null, altnames ? "AltFmt" : "Std", 
                                    (tt, o) => { if (o is string) return System.Drawing.ColorTranslator.FromHtml((string)o); else return Color.Orange; 
                                    });

            if (jconv is Theme)
            {
                Theme ret = (Theme)jconv;

                if ( lackingthemepanelcolours ) // if reset panel colours 2 to 8
                {
                    ThemeList tl = new ThemeList();
                    tl.LoadBaseThemes();
                    Theme r = tl.FindTheme("Elite Verdana Gradiant");       // load a set with gradients
                    for (int i = 2; i <= PanelsSets; i++)
                    {
                        var ps = ret.GetPanelSet(i);
                        var ss = r.GetPanelSet(i);
                        ps[0] = ss[0];
                        ps[1] = ss[1];
                        ps[2] = ss[2];
                        ps[3] = ss[3];
                    }
                }

                if (usename!=null)
                    ret.Name = usename;

                ret.FillInNewOptions(emitdebug);     // fill in and complain about any missing colours
                return ret;
            }
            else
            {
                var err = jconv as QuickJSON.JTokenExtensions.ToObjectError;
                if ( err != null)
                    System.Diagnostics.Debug.WriteLine($"Theme failed to convert {err.ErrorString}");
            }

            return null;
        }

        // load file
        public static Theme LoadFile(string pathname, string usethisname = null, bool altnames = true, bool emitdebug = false)
        {

            JToken jo = pathname.ReadJSONFile();
            if ( jo != null )
            {
                Theme thm = FromJSON(jo, usethisname, altnames, emitdebug);
                if (thm != null)
                {
                    return thm;
                }
            }

            return null;
        }
        public bool SaveFile(string pathname)
        {
            return FileHelpers.TryWriteToFile(pathname, ToJSON().ToString(true));
        }

        #endregion

        #region Helpers

        // if a parent above is a group box behind the control, use the group box back color, else use the form colour
        private Color GroupBoxOverride(Control parent, Color d)
        {
            Control x = parent;
            while (x != null)
            {
                if (x is GroupBox)
                    return GroupBack[0];
                x = x.Parent;
            }

            return d;
        }

        // New options introduced in march/april 25 are marked as transparent..
        // if missing, fill them in from existing info
        private int FillInNewOptions(bool emitdebug)
        {
            float sc = 0.6F;
            int count = 0;

            count += Replace(emitdebug, nameof(ButtonBackColor2), ButtonBackColor.Multiply(sc));       // in order

            count += Replace(emitdebug, nameof(TextBlockBorderColor2), TextBlockBorderColor.Multiply(sc));
            count += Replace(emitdebug, nameof(TextBlockSliderBack2), TextBlockSliderBack.Multiply(sc));
            count += Replace(emitdebug, nameof(TextBlockScrollArrowBack), TextBlockScrollButtonBack);
            count += Replace(emitdebug, nameof(TextBlockScrollArrowBack2), TextBlockScrollButtonBack.Multiply(sc));
            count += Replace(emitdebug, nameof(TextBlockScrollButtonBack2), TextBlockScrollButtonBack.Multiply(sc));
            count += Replace(emitdebug, nameof(TextBlockDropDownBackColor), TextBlockBackColor);
            count += Replace(emitdebug, nameof(TextBlockDropDownBackColor2), TextBlockBackColor);

            count += Replace(emitdebug, nameof(ListBoxBackColor), ButtonBackColor);
            count += Replace(emitdebug, nameof(ListBoxBackColor2), ButtonBackColor2);
            count += Replace(emitdebug, nameof(ListBoxBorderColor), ButtonBorderColor);
            count += Replace(emitdebug, nameof(ListBoxTextColor), ButtonTextColor);
            count += Replace(emitdebug, nameof(ListBoxSliderBack), TextBlockSliderBack);
            count += Replace(emitdebug, nameof(ListBoxSliderBack2), TextBlockSliderBack.Multiply(sc));
            count += Replace(emitdebug, nameof(ListBoxScrollArrowBack), TextBlockScrollButtonBack);
            count += Replace(emitdebug, nameof(ListBoxScrollArrowBack2), TextBlockScrollButtonBack.Multiply(sc));
            count += Replace(emitdebug, nameof(ListBoxScrollButtonBack), TextBlockScrollButtonBack);
            count += Replace(emitdebug, nameof(ListBoxScrollButtonBack2), TextBlockScrollButtonBack.Multiply(sc));
            count += Replace(emitdebug, nameof(ListBoxScrollArrow), TextBlockScrollArrowBack);

            count += Replace(emitdebug, nameof(GridSliderBack2), GridSliderBack.Multiply(sc));
            count += Replace(emitdebug, nameof(GridScrollArrowBack), GridScrollButtonBack);
            count += Replace(emitdebug, nameof(GridScrollArrow2Back), GridScrollButtonBack.Multiply(sc));
            count += Replace(emitdebug, nameof(GridScrollButtonBack2), GridScrollButtonBack.Multiply(sc));

            count += Replace(emitdebug, nameof(ComboBoxBackColor), ButtonBackColor);
            count += Replace(emitdebug, nameof(ComboBoxBackColor2), ButtonBackColor2);
            count += Replace(emitdebug, nameof(ComboBoxBorderColor), ButtonBorderColor);
            count += Replace(emitdebug, nameof(ComboBoxTextColor), ButtonTextColor);
            count += Replace(emitdebug, nameof(ComboBoxDropDownSliderBack), TextBlockSliderBack);
            count += Replace(emitdebug, nameof(ComboBoxDropDownSliderBack2), TextBlockSliderBack.Multiply(sc));
            count += Replace(emitdebug, nameof(ComboBoxScrollArrowBack), TextBlockScrollArrowBack);
            count += Replace(emitdebug, nameof(ComboBoxScrollArrowBack2), TextBlockScrollArrowBack2);
            count += Replace(emitdebug, nameof(ComboBoxScrollButtonBack), TextBlockScrollButtonBack);
            count += Replace(emitdebug, nameof(ComboBoxScrollButtonBack2), TextBlockScrollButtonBack2);
            count += Replace(emitdebug, nameof(ComboBoxScrollArrow), TextBlockScrollArrow);

            count += Replace(emitdebug, nameof(CheckBoxBack), CheckBoxText);
            count += Replace(emitdebug, nameof(CheckBoxBack2), CheckBoxText.Multiply(sc));
            count += Replace(emitdebug, nameof(CheckBoxButtonTickedBack), CheckBoxTick);
            count += Replace(emitdebug, nameof(CheckBoxButtonTickedBack2), CheckBoxTick.Multiply(sc));
            count += Replace(emitdebug, nameof(CheckBoxBorderColor), ButtonBorderColor);

            count += Replace(emitdebug, nameof(GroupBorder2), GroupBorder.Multiply(sc));

            count += Replace(emitdebug, nameof(TabStripFore), ButtonTextColor);
            count += Replace(emitdebug, nameof(TabStripSelected), ButtonBackColor);

            count += Replace(emitdebug, nameof(TabControlText), ButtonTextColor);
            count += Replace(emitdebug, nameof(TabControlButtonBack), ButtonBackColor);
            count += Replace(emitdebug, nameof(TabControlButtonBack2), ButtonBackColor.Multiply(sc));
            count += Replace(emitdebug, nameof(TabControlBorder2), TabControlBorder.Multiply(sc));        // border is bright, this is darker
            count += Replace(emitdebug, nameof(TabControlPageBack), Form);        // border is bright, this is darker

            for (int i = 0; i < Panel1.Length; i++)
            {
                count += Replace(emitdebug, nameof(GroupBack), Form, i);
                count += Replace(emitdebug, nameof(TabControlBack), Form, i);
                count += Replace(emitdebug, nameof(TabStripBack), Form, i);

                for( int j =  1; j <= PanelsSets;j++)
                    count += Replace(emitdebug, $"Panel{j}", Form, i);
            }

            // verify we have no transparent colours left due to missing above
#if DEBUG            
            var proplist = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var p in proplist)
            {
                if (p.PropertyType == typeof(Color))
                {
                    Color c = (Color)p.GetValue(this);
                    System.Diagnostics.Debug.Assert(c != Color.Transparent);
                }
            }
#endif
            return count;
        }

        private int Replace(bool debug, string nameofitem, Color y, int index = 0 )
        {
            System.Diagnostics.Debug.Assert(y != Color.Transparent);

            PropertyInfo p = typeof(Theme).GetProperty(nameofitem);
            if (p.PropertyType.IsArray)
            {
                Array a = (Array)p.GetValue(this);
                Color curv = (Color)a.GetValue(index);

                if (curv == Color.Transparent)
                {
                    if (debug)
                        System.Diagnostics.Debug.WriteLine($"Theme {Name} missing item {nameofitem}[{index}] setting to {y}");
                    a.SetValue(y,index);
                    return 1;
                }
            }
            else
            {
                Color curv = (Color)p.GetValue(this);
                if (curv == Color.Transparent)
                {
                    if (debug)
                        System.Diagnostics.Debug.WriteLine($"Theme {Name} missing item {nameofitem} setting to {y}");
                    p.SetValue(this, y);
                    return 1;
                }
            }

            return 0;
        }

        #endregion

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
