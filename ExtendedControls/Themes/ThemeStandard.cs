/*
 * Copyright © 2016-2019 EDDiscovery development team
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExtendedControls
{
    // specific implementation of a Themeable system.. does not include save/load to file etc due to the user may want their own implementation

    public class ThemeStandard : ITheme
    {
        public static readonly string[] ButtonStyles = "System Flat Gradient".Split();
        public static readonly string[] TextboxBorderStyles = "None FixedSingle Fixed3D Colour".Split();

        protected static string buttonstyle_system = ButtonStyles[0];
        protected static string buttonstyle_flat = ButtonStyles[1];
        protected static string buttonstyle_gradient = ButtonStyles[2];
        protected static string textboxborderstyle_fixedsingle = TextboxBorderStyles[1];
        protected static string textboxborderstyle_fixed3D = TextboxBorderStyles[2];
        protected static string textboxborderstyle_color = TextboxBorderStyles[3];

        public System.Drawing.Icon MessageBoxWindowIcon { get; set; }

        public struct Settings
        {
            static int MajorThemeID = 1;         // Change if major change made outside of number of colors.
            static public int ThemeID { get { return MajorThemeID * 10000 + Enum.GetNames(typeof(CI)).Length; } }

            public enum CI
            {
                form,
                button_back, button_border, button_text,
                grid_borderback, grid_bordertext,
                grid_cellbackground, grid_celltext, grid_borderlines,
                grid_sliderback, grid_scrollarrow, grid_scrollbutton,
                textbox_fore, textbox_highlight, textbox_success, textbox_back, textbox_border,
                textbox_sliderback, textbox_scrollarrow, textbox_scrollbutton,
                menu_back, menu_fore, menu_dropdownback, menu_dropdownfore,
                group_back, group_text, group_borderlines,
                travelgrid_nonvisted, travelgrid_visited,
                checkbox, checkbox_tick,
                label,
                tabcontrol_borderlines,
                toolstrip_back, toolstrip_border, unused_entry,     // previously assigned to toolstrip_checkbox thing
                s_panel
            };

            public string name;         // name of scheme
            public Dictionary<CI, Color> colors;       // dictionary of colors, indexed by CI.
            public bool windowsframe;
            public double formopacity;
            public string fontname;         // Font.. (empty means don't override)
            public float fontsize;
            public string buttonstyle;
            public string textboxborderstyle;

            public Settings(String n, Color f,
                                        Color bb, Color bf, Color bborder, string bstyle,
                                        Color gb, Color gbt, Color gbck, Color gt, Color gridlines,
                                        Color gsb, Color gst, Color gsbut,
                                        Color tn, Color tv,
                                        Color tbb, Color tbf, Color tbh, Color tbs, Color tbborder, string tbbstyle,
                                        Color tbsb, Color tbst, Color tbbut,
                                        Color c, Color ctick,
                                        Color mb, Color mf, Color mdb, Color mdf,
                                        Color l,
                                        Color grpb, Color grpt, Color grlines,
                                        Color tabborderlines,
                                        Color ttb, Color ttborder, Color ttbuttonchecked,
                                        Color sPanel,
                                        bool wf, double op, string ft, float fs)            // ft = empty means don't set it
            {
                name = n;
                colors = new Dictionary<CI, Color>();
                colors.Add(CI.form, f);
                colors.Add(CI.button_back, bb); colors.Add(CI.button_text, bf); colors.Add(CI.button_border, bborder);
                colors.Add(CI.grid_borderback, gb); colors.Add(CI.grid_bordertext, gbt);
                colors.Add(CI.grid_cellbackground, gbck); colors.Add(CI.grid_celltext, gt); colors.Add(CI.grid_borderlines, gridlines);
                colors.Add(CI.grid_sliderback, gsb); colors.Add(CI.grid_scrollarrow, gst); colors.Add(CI.grid_scrollbutton, gsbut);
                colors.Add(CI.travelgrid_nonvisted, tn); colors.Add(CI.travelgrid_visited, tv);
                colors.Add(CI.textbox_back, tbb); colors.Add(CI.textbox_fore, tbf);
                colors.Add(CI.textbox_sliderback, tbsb); colors.Add(CI.textbox_scrollarrow, tbst); colors.Add(CI.textbox_scrollbutton, tbbut);
                colors.Add(CI.textbox_highlight, tbh); colors.Add(CI.textbox_success, tbs);
                colors.Add(CI.textbox_border, tbborder);
                colors.Add(CI.checkbox, c); colors.Add(CI.checkbox_tick, ctick);
                colors.Add(CI.menu_back, mb); colors.Add(CI.menu_fore, mf); colors.Add(CI.menu_dropdownback, mdb); colors.Add(CI.menu_dropdownfore, mdf);
                colors.Add(CI.label, l);
                colors.Add(CI.group_back, grpb); colors.Add(CI.group_text, grpt); colors.Add(CI.group_borderlines, grlines);
                colors.Add(CI.tabcontrol_borderlines, tabborderlines);
                colors.Add(CI.toolstrip_back, ttb); colors.Add(CI.toolstrip_border, ttborder); colors.Add(CI.unused_entry, ttbuttonchecked);
                colors.Add(CI.s_panel, sPanel);
                buttonstyle = bstyle; textboxborderstyle = tbbstyle;
                windowsframe = wf; formopacity = op; fontname = ft; fontsize = fs;
            }

            public Settings(string n)                                               // gets you windows default colours
            {
                name = n;
                colors = new Dictionary<CI, Color>();
                colors.Add(CI.form, SystemColors.Menu);
                colors.Add(CI.button_back, Color.FromArgb(255, 225, 225, 225)); colors.Add(CI.button_text, SystemColors.ControlText); colors.Add(CI.button_border, SystemColors.ActiveBorder);
                colors.Add(CI.grid_borderback, SystemColors.Menu); colors.Add(CI.grid_bordertext, SystemColors.MenuText);
                colors.Add(CI.grid_cellbackground, SystemColors.ControlLightLight); colors.Add(CI.grid_celltext, SystemColors.MenuText); colors.Add(CI.grid_borderlines, SystemColors.ControlDark);
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
                buttonstyle = buttonstyle_system;
                textboxborderstyle = textboxborderstyle_fixed3D;
                windowsframe = true;
                formopacity = 100;
                fontname = defaultfont;
                fontsize = defaultfontsize;
            }
            // copy constructor, takes a real copy, with overrides
            public Settings(Settings other, string newname = null, string newfont = null, float newfontsize = 0, double opaque = 0)
            {
                name = (newname != null) ? newname : other.name;
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
        }

        public static float minfontsize = 4;
        public static string defaultfont = "Microsoft Sans Serif";
        public static float defaultfontsize = 8.25F;

        public string Name { get { return currentsettings.name; } set { currentsettings.name = value; } }

        public Color Form { get { return currentsettings.colors[Settings.CI.form]; } set { SetCustom(); currentsettings.colors[Settings.CI.form] = value; } }

        public Color ButtonBackColor { get { return currentsettings.colors[Settings.CI.button_back]; } set { SetCustom(); currentsettings.colors[Settings.CI.button_back] = value; } }
        public Color ButtonBorderColor { get { return currentsettings.colors[Settings.CI.button_border]; } set { SetCustom(); currentsettings.colors[Settings.CI.button_border] = value; } }
        public Color ButtonTextColor { get { return currentsettings.colors[Settings.CI.button_text]; } set { SetCustom(); currentsettings.colors[Settings.CI.button_text] = value; } }

        public Color GridCellBack { get { return currentsettings.colors[Settings.CI.grid_cellbackground]; } set { SetCustom(); currentsettings.colors[Settings.CI.grid_cellbackground] = value; } }
        public Color GridBorderBack { get { return currentsettings.colors[Settings.CI.grid_borderback]; } set { SetCustom(); currentsettings.colors[Settings.CI.grid_borderback] = value; } }
        public Color GridCellText { get { return currentsettings.colors[Settings.CI.grid_celltext]; } set { SetCustom(); currentsettings.colors[Settings.CI.grid_celltext] = value; } }
        public Color GridBorderLines { get { return currentsettings.colors[Settings.CI.grid_borderlines]; } set { SetCustom(); currentsettings.colors[Settings.CI.grid_borderlines] = value; } }

        public Color TextBlockColor { get { return currentsettings.colors[Settings.CI.textbox_fore]; } set { SetCustom(); currentsettings.colors[Settings.CI.textbox_fore] = value; } }
        public Color TextBlockHighlightColor { get { return currentsettings.colors[Settings.CI.textbox_highlight]; } set { SetCustom(); currentsettings.colors[Settings.CI.textbox_highlight] = value; } }
        public Color TextBlockSuccessColor { get { return currentsettings.colors[Settings.CI.textbox_success]; } set { SetCustom(); currentsettings.colors[Settings.CI.textbox_success] = value; } }
        public Color TextBackColor { get { return currentsettings.colors[Settings.CI.textbox_back]; } set { SetCustom(); currentsettings.colors[Settings.CI.textbox_back] = value; } }
        public Color TextBlockBorderColor { get { return currentsettings.colors[Settings.CI.textbox_border]; } set { SetCustom(); currentsettings.colors[Settings.CI.textbox_border] = value; } }

        public Color VisitedSystemColor { get { return currentsettings.colors[Settings.CI.travelgrid_visited]; } set { SetCustom(); currentsettings.colors[Settings.CI.travelgrid_visited] = value; } }
        public Color NonVisitedSystemColor { get { return currentsettings.colors[Settings.CI.travelgrid_nonvisted]; } set { SetCustom(); currentsettings.colors[Settings.CI.travelgrid_nonvisted] = value; } }

        public Color LabelColor { get { return currentsettings.colors[Settings.CI.label]; } set { SetCustom(); currentsettings.colors[Settings.CI.label] = value; } }

        public Color SPanelColor { get { return currentsettings.colors[Settings.CI.s_panel]; } set { SetCustom(); currentsettings.colors[Settings.CI.s_panel] = value; } }

        public string TextBlockBorderStyle { get { return currentsettings.textboxborderstyle; } set { SetCustom(); currentsettings.textboxborderstyle = value; } }

        public string ButtonStyle { get { return currentsettings.buttonstyle; } set { SetCustom(); currentsettings.buttonstyle = value; } }

        public bool WindowsFrame { get { return currentsettings.windowsframe; } set { SetCustom(); currentsettings.windowsframe = value; } }
        public double Opacity { get { return currentsettings.formopacity; } set { SetCustom(); currentsettings.formopacity = value; } }
        public string FontName { get { return currentsettings.fontname; } set { SetCustom(); currentsettings.fontname = value; } }
        public float FontSize { get { return currentsettings.fontsize; } set { SetCustom(); currentsettings.fontsize = value; } }

        public void SetCustom()
        { currentsettings.name = "Custom"; }                                // set so custom..

        public Font GetFont
        {
            get
            {
                return GetFontSizeStyle(currentsettings.fontsize, FontStyle.Regular);
            }
        }

        private const float dialogscaling = 0.8f;

        public Font GetDialogFont       // dialogs get a slighly smaller font
        {
            get
            {       // we don't scale down fonts < 12 since they are v.small already
                float fsize = currentsettings.fontsize >= 12 ? (currentsettings.fontsize * dialogscaling) : currentsettings.fontsize;
                return GetFontSizeStyle(fsize, FontStyle.Regular);
            }
        }

        public Font GetScaledFont(float scaling, FontStyle fs = FontStyle.Regular, float max = 999)
        {
            return GetFontSizeStyle(Math.Min(currentsettings.fontsize * scaling,max), fs);
        }

        public Font GetDialogScaledFont(float scaling, FontStyle fs = FontStyle.Regular, float max = 999)
        {
            float fsize = currentsettings.fontsize >= 12 ? (currentsettings.fontsize * dialogscaling) : currentsettings.fontsize;
            fsize = Math.Min(fsize, max);
            return GetFontSizeStyle(fsize * scaling, fs);
        }

        private Font GetFontSizeStyle(float size, FontStyle fs)
        {
            if (currentsettings.fontname.Equals("") || currentsettings.fontsize < minfontsize)
            {
                currentsettings.fontname = "Microsoft Sans Serif";          // in case schemes were loaded badly
                currentsettings.fontsize = 8.25F;
            }

            Font fnt = BaseUtils.FontLoader.GetFont(currentsettings.fontname, Math.Max(size,4f), fs);        // if it does not know the font, it will substitute Sans serif
            return fnt;
        }

        public Settings currentsettings;           // if name = custom, then its not a standard theme..
        protected List<Settings> themelist;

        public ThemeStandard()
        {
            themelist = new List<Settings>();           // theme list in
            currentsettings = new Settings("Windows Default");  // this is our default
            ToolStripManager.Renderer = new ThemeToolStripRenderer();
        }

        public void LoadBaseThemes()                                    // base themes load
        {
            themelist.Clear();

            themelist.Add(new Settings("Windows Default"));         // windows default..

            themelist.Add(new Settings("Orange Delight", Color.Black,
                Color.FromArgb(255, 48, 48, 48), Color.Orange, Color.DarkOrange, buttonstyle_gradient, // button
                Color.FromArgb(255, 176, 115, 0), Color.Black,  // grid border
                Color.Black, Color.Orange, Color.DarkOrange, // grid
                Color.Black, Color.Orange, Color.DarkOrange, // grid back, arrow, button
                Color.Orange, Color.White, // travel
                Color.Black, Color.Orange, Color.Red, Color.Green, Color.DarkOrange, textboxborderstyle_color, // text box
                Color.Black, Color.Orange, Color.DarkOrange, // text back, arrow, button
                Color.Orange, Color.FromArgb(255, 65, 33, 33), // checkbox
                Color.Black, Color.Orange, Color.DarkOrange, Color.Yellow,  // menu
                Color.Orange,  // label
                Color.FromArgb(255, 32, 32, 32), Color.Orange, Color.FromArgb(255, 130, 71, 0), // group back, text, border
                Color.DarkOrange, // tab control
                Color.Black, Color.DarkOrange, Color.Orange, // toolstrip
                Color.Orange, // spanel
                false, 95, "Microsoft Sans Serif", 8.25F));

            // ON purpose, always show them the euro caps one to give a hint!
            themelist.Add(new Settings(themelist[themelist.Count - 1], "Elite EuroCaps", "Euro Caps", 12F, 95));

            if (IsFontAvailable("Euro Caps"))
            {
                themelist.Add(new Settings(themelist[themelist.Count - 1], "Elite EuroCaps High DPI", "Euro Caps", 20F, 95));

                Color butback = Color.FromArgb(255, 32, 32, 32);
                themelist.Add(new Settings("Elite EuroCaps Less Border", Color.Black,
                    Color.FromArgb(255, 64, 64, 64), Color.Orange, Color.FromArgb(255, 96, 96, 96), buttonstyle_gradient, // button
                    Color.FromArgb(255, 176, 115, 0), Color.Black,  // grid border
                    butback, Color.Orange, Color.DarkOrange, // grid
                    butback, Color.Orange, Color.DarkOrange, // grid back, arrow, button
                    Color.Orange, Color.White, // travel
                    butback, Color.Orange, Color.Red, Color.Green, Color.FromArgb(255, 64, 64, 64), textboxborderstyle_color, // text box
                    butback, Color.Orange, Color.DarkOrange, // text back, arrow, button
                    Color.Orange, Color.FromArgb(255, 65, 33, 33),// checkbox
                    Color.Black, Color.Orange, Color.DarkOrange, Color.Yellow,  // menu
                    Color.Orange,  // label
                    Color.Black, Color.Orange, Color.FromArgb(255, 130, 71, 0), // group
                    Color.DarkOrange, // tab control
                    Color.Black, Color.DarkOrange, Color.Orange, // toolstrips
                    Color.Orange, // spanel
                    false, 100, "Euro Caps", 12F));
            }

            if (IsFontAvailable("Verdana"))
            {
                themelist.Add(new Settings(themelist[themelist.Count - 1], "Elite Verdana", "Verdana", 10F));
                themelist.Add(new Settings(themelist[themelist.Count - 1], "Elite Verdana High DPI", "Verdana", 20F));
            }

            if (IsFontAvailable("Calisto MT"))
            {
                themelist.Add(new Settings(themelist[themelist.Count - 1], "Elite Calisto", "Calisto MT", 12F));
                themelist.Add(new Settings(themelist[themelist.Count - 1], "Elite Calisto High DPI", "Calisto MT", 20F));
            }

            themelist.Add(new Settings("Easy Dark", Color.FromArgb(255, 65, 65, 65), // form
                Color.FromArgb(255, 75, 75, 75), Color.FromArgb(255, 245, 120, 30), Color.FromArgb(255, 41, 46, 51), buttonstyle_flat, // button back, text, border
                Color.FromArgb(255, 62, 68, 77), Color.FromArgb(255, 255, 120, 30), // grid borderback, bordertext
                Color.FromArgb(255, 79, 73, 68), Color.FromArgb(255, 223, 227, 238), Color.FromArgb(255, 50, 50, 50), // grid cellbackground, text, borderlines
                Color.FromArgb(255, 80, 75, 70), Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 75, 75, 75), // grid sliderback, arrow, scrollbutton
                Color.FromArgb(255, 192, 192, 192), Color.FromArgb(255, 202, 202, 255), // travelgrid_nonvisited, visited
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 248, 148, 6), Color.FromArgb(255, 90, 196, 90), Color.FromArgb(255, 46, 51, 56), textboxborderstyle_color, // textbox back, fore, highlight, success, border
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 235, 110, 20), Color.FromArgb(255, 75, 75, 75), // text sliderback, scrollarrow, scrollbutton
                Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 235, 116, 20), // checkbox, checkboxtick
                Color.FromArgb(255, 60, 55, 50), Color.FromArgb(255, 233, 227, 238), Color.FromArgb(255, 60, 55, 50), Color.FromArgb(255, 245, 245, 245),  // menuback, fore, dropdownback, dropdownfore
                Color.FromArgb(255, 240, 240, 240),  // label
                Color.FromArgb(255, 45, 50, 55), Color.FromArgb(255, 235, 110, 20), Color.FromArgb(255, 60, 55, 50), // group back, text, border
                Color.FromArgb(255, 40, 45, 50), // tab control borderlines
                Color.FromArgb(255, 75, 75, 75), Color.FromArgb(255, 45, 50, 55), Color.FromArgb(255, 40, 45, 50), // toolstrip, back, border
                Color.FromArgb(255, 250, 150, 8), // spanel
                false, 100, "Arial", 9.75F));

            themelist.Add(new Settings(themelist[themelist.Count - 1], "Easy Dark High DPI", "Arial", 20F));

            themelist.Add(new Settings("EDSM", Color.FromArgb(255, 39, 43, 48), // form
                Color.FromArgb(255, 71, 77, 84), Color.FromArgb(255, 245, 245, 245), Color.FromArgb(255, 41, 46, 51), buttonstyle_flat, // button back, text, border
                Color.FromArgb(255, 62, 68, 77), Color.FromArgb(255, 200, 200, 200), // grid borderback, bordertext
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 62, 68, 77), // grid cellbackground, text, borderlines
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 72, 78, 85), // grid sliderback, arrow, scrollbutton
                Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 90, 196, 222), // travelgrid_nonvisited, visited
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 248, 148, 6), Color.FromArgb(255, 90, 196, 90), Color.FromArgb(255, 46, 51, 56), textboxborderstyle_color, // textbox back, fore, highlight, success, border
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 72, 78, 85), // text sliderback, scrollarrow, scrollbutton
                Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 98, 196, 98), // checkbox, checkboxtick
                Color.FromArgb(255, 58, 63, 68), Color.FromArgb(255, 245, 245, 245), Color.FromArgb(255, 58, 63, 68), Color.FromArgb(255, 200, 200, 200),  // menuback, fore, dropdownback, dropdownfore
                Color.FromArgb(255, 200, 200, 200),  // label
                Color.FromArgb(255, 46, 51, 56), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 41, 46, 51), // group back, text, border
                Color.FromArgb(255, 41, 46, 51), // tab control borderlines
                Color.FromArgb(255, 71, 77, 84), Color.FromArgb(255, 46, 51, 56), Color.FromArgb(255, 41, 46, 51), // toolstrip, back, border
                Color.FromArgb(255, 255, 0, 0), // spanel
                false, 100, "Arial", 10.25F));

            themelist.Add(new Settings(themelist[themelist.Count - 1], "EDSM High DPI", "Arial", 20F));

            if (IsFontAvailable("Arial Narrow"))
            {
                themelist.Add(new Settings(themelist[themelist.Count - 1], "EDSM Arial Narrow", "Arial Narrow", 10.25F, 95));
                themelist.Add(new Settings(themelist[themelist.Count - 1], "EDSM Arial Narrow High DPI", "Arial Narrow", 20F, 95));
            }
            if (IsFontAvailable("Euro Caps"))
            {
                themelist.Add(new Settings(themelist[themelist.Count - 1], "EDSM EuroCaps", "Euro Caps", 10.25F, 95));
                themelist.Add(new Settings(themelist[themelist.Count - 1], "EDSM EuroCaps High DPI", "Euro Caps", 20F, 95));
            }

            themelist.Add(new Settings("Material Dark", Color.FromArgb(255, 54, 57, 63), // form
                Color.FromArgb(255, 75, 75, 75), Color.FromArgb(255, 255, 160, 0), Color.FromArgb(255, 41, 46, 51), buttonstyle_flat, // button back, text, border
                Color.FromArgb(255, 47, 49, 54), Color.FromArgb(255, 255, 160, 0), // grid borderback, bordertext
                Color.FromArgb(255, 37, 37, 38), Color.FromArgb(255, 223, 227, 238), Color.FromArgb(255, 82, 94, 164), // grid cellbackground, text, borderlines
                Color.FromArgb(255, 37, 37, 38), Color.FromArgb(255, 82, 94, 164), Color.FromArgb(255, 82, 94, 164), // grid sliderback, arrow, scrollbutton
                Color.FromArgb(255, 192, 192, 192), Color.FromArgb(255, 202, 202, 255), // travelgrid_nonvisited, visited
                Color.FromArgb(255, 47, 49, 54), Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 255, 160, 0), Color.FromArgb(255, 90, 196, 90), Color.FromArgb(255, 82, 94, 164), textboxborderstyle_color, // textbox back, fore, highlight, success, border
                Color.FromArgb(255, 47, 49, 54), Color.FromArgb(255, 255, 160, 0), Color.FromArgb(255, 75, 75, 75), // text sliderback, scrollarrow, scrollbutton
                Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 255, 160, 0), // checkbox, checkboxtick
                Color.FromArgb(255, 60, 55, 50), Color.FromArgb(255, 196, 196, 196), Color.FromArgb(255, 60, 55, 50), Color.FromArgb(255, 245, 245, 245),  // menuback, fore, dropdownback, dropdownfore
                Color.FromArgb(255, 240, 240, 240),  // label
                Color.FromArgb(255, 45, 50, 55), Color.FromArgb(255, 255, 160, 0), Color.FromArgb(255, 129, 138, 192), // group back, text, border
                Color.FromArgb(255, 40, 45, 50), // tab control borderlines
                Color.FromArgb(255, 75, 75, 75), Color.FromArgb(255, 45, 50, 55), Color.FromArgb(255, 40, 45, 50), // toolstrip, back, border
                Color.FromArgb(255, 250, 150, 8), // spanel
                false, 100, "Microsoft Sans Serif", 9.75F));

            themelist.Add(new Settings(themelist[themelist.Count - 1], "Material Dark High DPI", "Microsoft Sans Serif", 20F));

            Color r1 = Color.FromArgb(255, 160, 0, 0);
            Color r2 = Color.FromArgb(255, 64, 0, 0);
            themelist.Add(new Settings("Night Vision", Color.Black,
                Color.FromArgb(255, 48, 48, 48), r1, r2, buttonstyle_gradient, // button
                r2, Color.Black,  // grid border
                Color.Black, r1, r2, // grid
                Color.Black, r1, r2, // grid back, arrow, button
                r1, Color.Green, // travel
                Color.Black, r1, Color.Orange, Color.Green, r2, textboxborderstyle_color, // text box
                Color.Black, r1, r2, // text back, arrow, button
                r1, Color.FromArgb(255, 65, 33, 33), // checkbox
                Color.Black, r1, r2, Color.Yellow,  // menu
                r1,  // label
                Color.FromArgb(255, 8, 8, 8), r1, Color.FromArgb(255, 130, 71, 0), // group
                r2, // tab control
                Color.Black, r2, r1, // toolstrip
                r1, // spanel
                false, 95, "Microsoft Sans Serif", 10F));

            if (IsFontAvailable("Euro Caps"))
                themelist.Add(new Settings(themelist[themelist.Count - 1], "Night Vision EuroCaps", "Euro Caps", 12F, 95));

            if (IsFontAvailable("Euro Caps"))
            {
                themelist.Add(new Settings("EuroCaps Grey",
                                        SystemColors.Menu,
                                        SystemColors.Control, SystemColors.ControlText, Color.DarkGray, buttonstyle_gradient,// button
                                        SystemColors.Menu, SystemColors.MenuText,  // grid border
                                        SystemColors.ControlLightLight, SystemColors.MenuText, SystemColors.ControlDark, // grid
                                        SystemColors.ControlLightLight, SystemColors.MenuText, SystemColors.ControlDark, // grid scroll
                                        Color.Blue, SystemColors.MenuText, // travel
                                        SystemColors.Window, SystemColors.WindowText, Color.Red, Color.Green, Color.DarkGray, textboxborderstyle_color,// text
                                        SystemColors.ControlLightLight, SystemColors.MenuText, SystemColors.ControlDark, // text box
                                        SystemColors.MenuText, SystemColors.MenuHighlight, // checkbox
                                        SystemColors.Menu, SystemColors.MenuText, SystemColors.ControlLightLight, SystemColors.MenuText,  // menu
                                        SystemColors.MenuText,  // label
                                        SystemColors.Menu, SystemColors.MenuText, SystemColors.ControlDark, // group
                                        SystemColors.ControlDark, // tab control
                                        SystemColors.Menu, SystemColors.Menu, SystemColors.MenuText,  // toolstrip
                                        SystemColors.ControlLightLight, // spanel
                                        false, 95, "Euro Caps", 12F));
            }

            if (IsFontAvailable("Verdana"))
                themelist.Add(new Settings(themelist[themelist.Count - 1], "Verdana Grey", "Verdana", 8F));

            themelist.Add(new Settings("Blue Wonder", Color.DarkBlue,
                                               Color.Blue, Color.White, Color.White, buttonstyle_gradient,// button
                                               Color.DarkBlue, Color.White,  // grid border
                                               Color.DarkBlue, Color.White, Color.Blue, // grid
                                               Color.DarkBlue, Color.White, Color.Blue, // grid scroll
                                               Color.White, Color.Cyan, // travel
                                               Color.DarkBlue, Color.White, Color.Red, Color.Green, Color.White, textboxborderstyle_color,// text box
                                               Color.DarkBlue, Color.White, Color.Blue, // text scroll
                                               Color.White, Color.Black, // checkbox
                                               Color.DarkBlue, Color.White, Color.DarkBlue, Color.White,  // menu
                                               Color.White,  // label
                                               Color.DarkBlue, Color.White, Color.Blue, // group
                                               Color.Blue,
                                               Color.DarkBlue, Color.White, Color.Red,  // toolstrip
                                               Color.LightBlue, // spanel
                                               false, 95, "Microsoft Sans Serif", 8.25F));

            Color baizegreen = Color.FromArgb(255, 13, 68, 13);
            themelist.Add(new Settings("Green Baize", baizegreen,
                                               baizegreen, Color.White, Color.White, buttonstyle_gradient,// button
                                               baizegreen, Color.White,  // grid border
                                               baizegreen, Color.White, Color.LightGreen, // grid
                                               baizegreen, Color.White, Color.LightGreen, // grid scroll
                                               Color.White, Color.FromArgb(255, 78, 190, 27), // travel
                                               baizegreen, Color.White, Color.Red, Color.Green, Color.White, textboxborderstyle_color,// text box
                                               baizegreen, Color.White, Color.LightGreen, // text scroll
                                               Color.White, Color.Black, // checkbox
                                               baizegreen, Color.White, baizegreen, Color.White,  // menu
                                               Color.White,  // label
                                               baizegreen, Color.White, Color.LightGreen, // group
                                               Color.LightGreen,    // tabcontrol
                                               baizegreen, Color.White, Color.White,
                                               baizegreen,
                                               false, 95, "Microsoft Sans Serif", 8.25F));
        }

        public bool ApplyStd(Control ctrl)      // normally a form, but can be a control, applies to this and ones below
        {
            return Apply(ctrl, GetFont);
        }

        public bool ApplyDialog(Control ctrl)
        {
            return Apply(ctrl, GetDialogFont);
        }

        public bool Apply(Control form, Font fnt)
        {
            UpdateControls(form.Parent, form, fnt, 0);
            UpdateToolsStripRenderer();
            return WindowsFrame;
        }
        
        private void UpdateControls(Control parent, Control myControl, Font fnt, int level)    // parent can be null
        {
#if DEBUG
            //System.Diagnostics.Debug.WriteLine("                             ".Substring(0, level) + level + ":" + parent?.Name.ToString() + ":" + myControl.Name.ToString() + " " + myControl.ToString() + " " + fnt.ToString() + " c.fnt " + myControl.Font);
            //System.Diagnostics.Debug.WriteLine("                             ".Substring(0, level) + level + ":" + myControl.GetType().Name + (myControl.Name.HasChars() ? " " + myControl.Name : "") + " : " + myControl.GetHeirarchy(false));
         //   System.Diagnostics.Debug.WriteLine("                             ".Substring(0, level) + level + ":" + myControl.GetType().Name + (myControl.Name.HasChars() ? " " + myControl.Name : "") + " : " + myControl.GetHeirarchy(false) + " " + myControl.Size);
#endif

            float mouseoverscaling = 1.3F;
            float mouseselectedscaling = 1.5F;

            const bool paneldebugmode = false;      // set for some help in those pesky panels

            Type controltype = myControl.GetType();
            string parentnamespace = parent?.GetType().Namespace ?? "NoParent";

            if (myControl is Form)
            {
                Form f = myControl as Form;
                f.FormBorderStyle = WindowsFrame ? FormBorderStyle.Sizable : FormBorderStyle.None;
                f.Opacity = currentsettings.formopacity / 100;
                f.BackColor = currentsettings.colors[Settings.CI.form];
                f.Font = fnt;
            }
            else if (!parentnamespace.Equals("ExtendedControls") && (controltype.Name.Equals("Button") || controltype.Name.Equals("RadioButton") || controltype.Name.Equals("GroupBox") ||
                controltype.Name.Equals("CheckBox") || controltype.Name.Equals("TextBox") ||
                controltype.Name.Equals("ComboBox") || (controltype.Name.Equals("RichTextBox")))
                )
            {
                Debug.Assert(false, myControl.Name + " of " + controltype.Name + " from " + parent.Name + " !!! Use the new controls in Controls folder - not the non visual themed ones!");
            }
            else if (myControl is ExtRichTextBox)
            {
                ExtRichTextBox ctrl = (ExtRichTextBox)myControl;
                ctrl.BorderColor = Color.Transparent;
                ctrl.BorderStyle = BorderStyle.None;

                ctrl.TextBoxForeColor = currentsettings.colors[Settings.CI.textbox_fore];
                ctrl.TextBoxBackColor = currentsettings.colors[Settings.CI.textbox_back];

                ctrl.ScrollBarFlatStyle = FlatStyle.System;

                if (currentsettings.textboxborderstyle.Equals(TextboxBorderStyles[1]))
                    ctrl.BorderStyle = BorderStyle.FixedSingle;
                else if (currentsettings.textboxborderstyle.Equals(TextboxBorderStyles[2]))
                    ctrl.BorderStyle = BorderStyle.Fixed3D;
                else if (currentsettings.textboxborderstyle.Equals(TextboxBorderStyles[3]))
                {
                    Color c1 = currentsettings.colors[Settings.CI.textbox_scrollbutton];
                    ctrl.BorderColor = currentsettings.colors[Settings.CI.textbox_border];
                    ctrl.ScrollBarBackColor = currentsettings.colors[Settings.CI.textbox_back];
                    ctrl.ScrollBarSliderColor = currentsettings.colors[Settings.CI.textbox_sliderback];
                    ctrl.ScrollBarBorderColor = ctrl.ScrollBarThumbBorderColor =
                                ctrl.ScrollBarArrowBorderColor = currentsettings.colors[Settings.CI.textbox_border];
                    ctrl.ScrollBarArrowButtonColor = ctrl.ScrollBarThumbButtonColor = c1;
                    ctrl.ScrollBarMouseOverButtonColor = c1.Multiply(mouseoverscaling);
                    ctrl.ScrollBarMousePressedButtonColor = c1.Multiply(mouseselectedscaling);
                    ctrl.ScrollBarForeColor = currentsettings.colors[Settings.CI.textbox_scrollarrow];
                    ctrl.ScrollBarFlatStyle = FlatStyle.Popup;
                }

                if (ctrl.ContextMenuStrip != null)      // propegate font
                    ctrl.ContextMenuStrip.Font = fnt;

                ctrl.Invalidate();
                ctrl.PerformLayout();
            }
            else if (myControl is ExtTextBox)
            {
                ExtTextBox ctrl = (ExtTextBox)myControl;
                ctrl.ForeColor = currentsettings.colors[Settings.CI.textbox_fore];
                ctrl.BackColor = currentsettings.colors[Settings.CI.textbox_back];
                ctrl.BackErrorColor = currentsettings.colors[Settings.CI.textbox_highlight];
                ctrl.ControlBackground = currentsettings.colors[Settings.CI.textbox_back]; // previously, but not sure why, GroupBoxOverride(parent, currentsettings.colors[Settings.CI.form]);
                ctrl.BorderColor = Color.Transparent;
                ctrl.BorderStyle = BorderStyle.None;
                ctrl.AutoSize = true;

                if (currentsettings.textboxborderstyle.Equals(TextboxBorderStyles[0]))
                    ctrl.AutoSize = false;                                                 // with no border, the autosize clips the bottom of chars..
                else if (currentsettings.textboxborderstyle.Equals(TextboxBorderStyles[1]))
                    ctrl.BorderStyle = BorderStyle.FixedSingle;
                else if (currentsettings.textboxborderstyle.Equals(TextboxBorderStyles[2]))
                    ctrl.BorderStyle = BorderStyle.Fixed3D;
                else if (currentsettings.textboxborderstyle.Equals(TextboxBorderStyles[3]))
                    ctrl.BorderColor = currentsettings.colors[Settings.CI.textbox_border];

                if (myControl is ExtTextBoxAutoComplete || myControl is ExtDataGridViewColumnAutoComplete.CellEditControl) // derived from text box
                {
                    ExtTextBoxAutoComplete actb = myControl as ExtTextBoxAutoComplete;
                    actb.DropDownBackgroundColor = currentsettings.colors[Settings.CI.button_back];
                    actb.DropDownBorderColor = currentsettings.colors[Settings.CI.textbox_border];
                    actb.DropDownScrollBarButtonColor = currentsettings.colors[Settings.CI.textbox_scrollbutton];
                    actb.DropDownScrollBarColor = currentsettings.colors[Settings.CI.textbox_sliderback];
                    actb.DropDownMouseOverBackgroundColor = currentsettings.colors[Settings.CI.button_back].Multiply(mouseoverscaling);

                    if (currentsettings.buttonstyle.Equals(ButtonStyles[0]))
                        actb.FlatStyle = FlatStyle.System;
                    else if (currentsettings.buttonstyle.Equals(ButtonStyles[1])) // flat
                        actb.FlatStyle = FlatStyle.Flat;
                    else
                        actb.FlatStyle = FlatStyle.Popup;
                }

                ctrl.Invalidate();
            }
            else if (myControl is ExtButton)
            {
                ExtButton ctrl = (ExtButton)myControl;
                ctrl.ForeColor = currentsettings.colors[Settings.CI.button_text];
                ctrl.AutoSize = true;

                if (currentsettings.buttonstyle.Equals(ButtonStyles[0])) // system
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

                        ctrl.ImageLayout = ImageLayout.Stretch;
                    }

                    if (ctrl.Image != null && ctrl.Text.Length == 0)        // if no text, just image, background is form to make the back disappear
                    {
                        ctrl.BackColor = currentsettings.colors[Settings.CI.form];
                    }
                    else
                    {
                        ctrl.BackColor = currentsettings.colors[Settings.CI.button_back];       // else its a graduated back
                    }

                    ctrl.FlatAppearance.BorderColor = (ctrl.Image != null) ? currentsettings.colors[Settings.CI.form] : currentsettings.colors[Settings.CI.button_border];
                    ctrl.FlatAppearance.BorderSize = 1;
                    ctrl.FlatAppearance.MouseOverBackColor = currentsettings.colors[Settings.CI.button_back].Multiply(mouseoverscaling);
                    ctrl.FlatAppearance.MouseDownBackColor = currentsettings.colors[Settings.CI.button_back].Multiply(mouseselectedscaling);

                    if (currentsettings.buttonstyle.Equals(ButtonStyles[1])) // flat
                        ctrl.FlatStyle = FlatStyle.Flat;
                    else
                        ctrl.FlatStyle = FlatStyle.Popup;
                }
            }
            else if (myControl is ExtTabControl)
            {
                ExtTabControl ctrl = (ExtTabControl)myControl;

                if (!currentsettings.buttonstyle.Equals(ButtonStyles[0])) // not system
                {
                    ctrl.FlatStyle = (currentsettings.buttonstyle.Equals(ButtonStyles[1])) ? FlatStyle.Flat : FlatStyle.Popup;
                    ctrl.TabControlBorderColor = currentsettings.colors[Settings.CI.tabcontrol_borderlines].Multiply(0.6F);
                    ctrl.TabControlBorderBrightColor = currentsettings.colors[Settings.CI.tabcontrol_borderlines];
                    ctrl.TabNotSelectedBorderColor = currentsettings.colors[Settings.CI.tabcontrol_borderlines].Multiply(0.4F);
                    ctrl.TabNotSelectedColor = currentsettings.colors[Settings.CI.button_back];
                    ctrl.TabSelectedColor = currentsettings.colors[Settings.CI.button_back].Multiply(mouseselectedscaling);
                    ctrl.TabMouseOverColor = currentsettings.colors[Settings.CI.button_back].Multiply(mouseoverscaling);
                    ctrl.TextSelectedColor = currentsettings.colors[Settings.CI.button_text];
                    ctrl.TextNotSelectedColor = currentsettings.colors[Settings.CI.button_text].Multiply(0.8F);
                    ctrl.TabStyle = new ExtendedControls.TabStyleAngled();
                }
                else
                    ctrl.FlatStyle = FlatStyle.System;

            }
            else if (myControl is ExtListBox)
            {
                ExtListBox ctrl = (ExtListBox)myControl;
                ctrl.ForeColor = currentsettings.colors[Settings.CI.button_text];
                ctrl.ItemSeperatorColor = currentsettings.colors[Settings.CI.button_border];

                if (currentsettings.buttonstyle.Equals(ButtonStyles[0]))
                {
                    ctrl.FlatStyle = FlatStyle.System;
                }
                else
                {
                    ctrl.BackColor = currentsettings.colors[Settings.CI.button_back];
                    ctrl.BorderColor = currentsettings.colors[Settings.CI.button_border];
                    ctrl.ScrollBarButtonColor = currentsettings.colors[Settings.CI.textbox_scrollbutton];
                    ctrl.ScrollBarColor = currentsettings.colors[Settings.CI.textbox_sliderback];

                    if (currentsettings.buttonstyle.Equals(ButtonStyles[1])) // flat
                        ctrl.FlatStyle = FlatStyle.Flat;
                    else
                        ctrl.FlatStyle = FlatStyle.Popup;
                }

                ctrl.Repaint();            // force a repaint as the individual settings do not by design.
            }
            else if (myControl is ExtPanelResizer)      // Resizers only show when no frame is on
            {
                myControl.Visible = !WindowsFrame;
            }
            else if (myControl is ExtPanelDropDown)
            {
                ExtPanelDropDown ctrl = (ExtPanelDropDown)myControl;
                ctrl.ForeColor = currentsettings.colors[Settings.CI.button_text];
                ctrl.SelectionMarkColor = ctrl.ForeColor;
                ctrl.BackColor = ctrl.SelectionBackColor = currentsettings.colors[Settings.CI.button_back];
                ctrl.BorderColor = currentsettings.colors[Settings.CI.button_border];
                ctrl.MouseOverBackgroundColor = currentsettings.colors[Settings.CI.button_back].Multiply(mouseoverscaling);
                ctrl.ScrollBarButtonColor = currentsettings.colors[Settings.CI.textbox_scrollbutton];
                ctrl.ScrollBarColor = currentsettings.colors[Settings.CI.textbox_sliderback];
                ctrl.FlatStyle = FlatStyle.Popup;
            }
            else if (myControl is ExtComboBox)
            {
                ExtComboBox ctrl = (ExtComboBox)myControl;
                ctrl.ForeColor = currentsettings.colors[Settings.CI.button_text];

                if (currentsettings.buttonstyle.Equals(ButtonStyles[0])) // system
                {
                    ctrl.FlatStyle = FlatStyle.System;
                }
                else
                {
                    ctrl.BackColor = ctrl.DropDownBackgroundColor = currentsettings.colors[Settings.CI.button_back];
                    ctrl.BorderColor = currentsettings.colors[Settings.CI.button_border];
                    ctrl.MouseOverBackgroundColor = currentsettings.colors[Settings.CI.button_back].Multiply(mouseoverscaling);
                    ctrl.ScrollBarButtonColor = currentsettings.colors[Settings.CI.textbox_scrollbutton];
                    ctrl.ScrollBarColor = currentsettings.colors[Settings.CI.textbox_sliderback];

                    if (currentsettings.buttonstyle.Equals(ButtonStyles[1])) // flat
                        ctrl.FlatStyle = FlatStyle.Flat;
                    else
                        ctrl.FlatStyle = FlatStyle.Popup;

                }

                ctrl.Repaint();            // force a repaint as the individual settings do not by design.
            }
            else if (myControl is NumericUpDown)
            {                                                                   // BACK colour does not work..
                myControl.ForeColor = currentsettings.colors[Settings.CI.textbox_fore];
            }
            else if (myControl is ExtButtonDrawn)
            {
                ExtButtonDrawn ctrl = (ExtButtonDrawn)myControl;
                ctrl.BackColor = currentsettings.colors[Settings.CI.form];
                ctrl.ForeColor = currentsettings.colors[Settings.CI.label];
                ctrl.MouseOverColor = currentsettings.colors[Settings.CI.label].Multiply(mouseoverscaling);
                ctrl.MouseSelectedColor = currentsettings.colors[Settings.CI.label].Multiply(mouseselectedscaling);

                System.Drawing.Imaging.ColorMap colormap = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images
                colormap.OldColor = Color.White;                                                        // white is defined as the forecolour
                colormap.NewColor = ctrl.ForeColor;
                ctrl.SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap });
                //System.Diagnostics.Debug.WriteLine("Drawn Panel Image button " + ctrl.Name);
            }
            else if (myControl is TableLayoutPanel)
            {
                myControl.BackColor = GroupBoxOverride(parent, currentsettings.colors[Settings.CI.form]);
            }
            else if (myControl is ExtPanelRollUp)
            {
                myControl.BackColor = paneldebugmode ? Color.Green : GroupBoxOverride(parent, currentsettings.colors[Settings.CI.form]);
            }
            else if (myControl is FlowLayoutPanel)
            {
                FlowLayoutPanel ctrl = myControl as FlowLayoutPanel;
                ctrl.BackColor = paneldebugmode ? Color.Red : GroupBoxOverride(parent, currentsettings.colors[Settings.CI.form]);
            }
            else if (myControl is PanelNoTheme)
            {
            }
            else if (myControl is Panel)
            {
                myControl.BackColor = paneldebugmode ? Color.Blue : currentsettings.colors[Settings.CI.form];
                myControl.ForeColor = currentsettings.colors[Settings.CI.label];
            }
            else if (myControl is Label)
            {
                myControl.ForeColor = currentsettings.colors[Settings.CI.label];

                if (myControl is ExtLabel)
                    (myControl as ExtLabel).TextBackColor = currentsettings.colors[Settings.CI.form];
            }
            else if (myControl is ExtGroupBox)
            {
                ExtGroupBox ctrl = (ExtGroupBox)myControl;
                ctrl.ForeColor = currentsettings.colors[Settings.CI.group_text];
                ctrl.BackColor = currentsettings.colors[Settings.CI.group_back];
                ctrl.BorderColor = currentsettings.colors[Settings.CI.group_borderlines];
                ctrl.FlatStyle = FlatStyle.Flat;           // always in Flat, always apply our border.
            }
            else if (myControl is ExtCheckBox)
            {
                ExtCheckBox ctrl = (ExtCheckBox)myControl;

                ctrl.BackColor = GroupBoxOverride(parent, currentsettings.colors[Settings.CI.form]);

                if (ctrl.Appearance == Appearance.Button)
                {
                    ctrl.ForeColor = currentsettings.colors[Settings.CI.button_text];
                    ctrl.MouseOverColor = currentsettings.colors[Settings.CI.button_back].Multiply(mouseoverscaling);
                    ctrl.CheckColor = currentsettings.colors[Settings.CI.button_back].Multiply(0.9f);

                    if (currentsettings.buttonstyle.Equals(ButtonStyles[0])) // system
                        ctrl.FlatStyle = FlatStyle.Standard;
                    else if (currentsettings.buttonstyle.Equals(ButtonStyles[1])) // flat
                        ctrl.FlatStyle = FlatStyle.Flat;
                    else
                        ctrl.FlatStyle = FlatStyle.Popup;
                }
                else
                {
                    ctrl.ForeColor = currentsettings.colors[Settings.CI.checkbox];
                    ctrl.CheckBoxColor = currentsettings.colors[Settings.CI.checkbox];
                    ctrl.CheckBoxInnerColor = currentsettings.colors[Settings.CI.checkbox].Multiply(1.5F);
                    ctrl.MouseOverColor = currentsettings.colors[Settings.CI.checkbox].Multiply(1.4F);
                    ctrl.TickBoxReductionRatio = 0.75f;
                    ctrl.CheckColor = currentsettings.colors[Settings.CI.checkbox_tick];

                    if (currentsettings.buttonstyle.Equals(ButtonStyles[0])) // system
                        ctrl.FlatStyle = FlatStyle.System;
                    else if (currentsettings.buttonstyle.Equals(ButtonStyles[1])) // flat
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

                if (currentsettings.buttonstyle.Equals(ButtonStyles[0])) // system
                    ctrl.FlatStyle = FlatStyle.System;
                else if (currentsettings.buttonstyle.Equals(ButtonStyles[1])) // flat
                    ctrl.FlatStyle = FlatStyle.Flat;
                else
                    ctrl.FlatStyle = FlatStyle.Popup;

                //Console.WriteLine("RB:" + myControl.Name + " Apply style " + currentsettings.buttonstyle);

                ctrl.BackColor = GroupBoxOverride(parent, currentsettings.colors[Settings.CI.form]);
                ctrl.ForeColor = currentsettings.colors[Settings.CI.checkbox];
                ctrl.RadioButtonColor = currentsettings.colors[Settings.CI.checkbox];
                ctrl.RadioButtonInnerColor = currentsettings.colors[Settings.CI.checkbox].Multiply(1.5F);
                ctrl.SelectedColor = ctrl.BackColor.Multiply(0.75F);
                ctrl.MouseOverColor = currentsettings.colors[Settings.CI.checkbox].Multiply(1.4F);
            }
            else if (myControl is DataGridView)                     // we theme this directly
            {
                DataGridView ctrl = (DataGridView)myControl;
                ctrl.EnableHeadersVisualStyles = false;            // without this, the colours for the grid are not applied.

                ctrl.RowHeadersDefaultCellStyle.BackColor = currentsettings.colors[Settings.CI.grid_borderback];
                ctrl.RowHeadersDefaultCellStyle.ForeColor = currentsettings.colors[Settings.CI.grid_bordertext];
                ctrl.ColumnHeadersDefaultCellStyle.BackColor = currentsettings.colors[Settings.CI.grid_borderback];
                ctrl.ColumnHeadersDefaultCellStyle.ForeColor = currentsettings.colors[Settings.CI.grid_bordertext];

                ctrl.BackgroundColor = GroupBoxOverride(parent, currentsettings.colors[Settings.CI.form]);
                ctrl.DefaultCellStyle.BackColor = GroupBoxOverride(parent, currentsettings.colors[Settings.CI.grid_cellbackground]);
                ctrl.DefaultCellStyle.ForeColor = currentsettings.colors[Settings.CI.grid_celltext];
                ctrl.DefaultCellStyle.SelectionBackColor = ctrl.DefaultCellStyle.ForeColor;
                ctrl.DefaultCellStyle.SelectionForeColor = ctrl.DefaultCellStyle.BackColor;

                ctrl.GridColor = currentsettings.colors[Settings.CI.grid_borderlines];
                ctrl.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                ctrl.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                foreach (DataGridViewColumn col in ctrl.Columns)
                {
                    if (col.CellType == typeof(DataGridViewComboBoxCell))
                    {   // Need to set flat style for colours to take on combobox cells.
                        DataGridViewComboBoxColumn cbocol = (DataGridViewComboBoxColumn)col;
                        if (currentsettings.buttonstyle.Equals(ButtonStyles[0])) // system
                            cbocol.FlatStyle = FlatStyle.System;
                        else if (currentsettings.buttonstyle.Equals(ButtonStyles[1])) // flat
                            cbocol.FlatStyle = FlatStyle.Flat;
                        else
                            cbocol.FlatStyle = FlatStyle.Popup;
                    }
                }

                if (ctrl.ContextMenuStrip != null)       // propergate font onto any attached context menus
                    ctrl.ContextMenuStrip.Font = fnt;
            }
            else if (myControl is ExtScrollBar && !(parent is ExtListBox || parent is ExtRichTextBox))
            {                   // selected items need VScroll controlled here. Others control it themselves
                ExtScrollBar ctrl = (ExtScrollBar)myControl;

                //System.Diagnostics.Debug.WriteLine("VScrollBarCustom Theme " + level + ":" + parent.Name.ToString() + ":" + myControl.Name.ToString() + " " + myControl.ToString() + " " + parentcontroltype.Name);
                if (currentsettings.textboxborderstyle.Equals(TextboxBorderStyles[3]))
                {
                    Color c1 = currentsettings.colors[Settings.CI.grid_scrollbutton];
                    ctrl.BorderColor = currentsettings.colors[Settings.CI.grid_borderlines];
                    ctrl.BackColor = currentsettings.colors[Settings.CI.form];
                    ctrl.SliderColor = currentsettings.colors[Settings.CI.grid_sliderback];
                    ctrl.BorderColor = ctrl.ThumbBorderColor =
                            ctrl.ArrowBorderColor = currentsettings.colors[Settings.CI.grid_borderlines];
                    ctrl.ArrowButtonColor = ctrl.ThumbButtonColor = c1;
                    ctrl.MouseOverButtonColor = c1.Multiply(mouseoverscaling);
                    ctrl.MousePressedButtonColor = c1.Multiply(mouseselectedscaling);
                    ctrl.ForeColor = currentsettings.colors[Settings.CI.grid_scrollarrow];
                    ctrl.FlatStyle = FlatStyle.Popup;
                }
                else
                    ctrl.FlatStyle = FlatStyle.System;
            }
            else if (myControl is ExtNumericUpDown)
            {
                ExtNumericUpDown ctrl = (ExtNumericUpDown)myControl;

                ctrl.TextBoxForeColor = currentsettings.colors[Settings.CI.textbox_fore];
                ctrl.TextBoxBackColor = currentsettings.colors[Settings.CI.textbox_back];
                ctrl.BorderColor = currentsettings.colors[Settings.CI.textbox_border];

                Color c1 = currentsettings.colors[Settings.CI.textbox_scrollbutton];
                ctrl.updown.BackColor = c1;
                ctrl.updown.ForeColor = currentsettings.colors[Settings.CI.textbox_scrollarrow];
                ctrl.updown.BorderColor = currentsettings.colors[Settings.CI.button_border];
                ctrl.updown.MouseOverColor = c1.Multiply(mouseoverscaling);
                ctrl.updown.MouseSelectedColor = c1.Multiply(mouseselectedscaling);
                ctrl.Invalidate();
            }
            else if (myControl is Chart)
            {
                Chart ctrl = (Chart)myControl;
                ctrl.BackColor = Color.Transparent;
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
                ctrl.BorderColor = currentsettings.colors[Settings.CI.grid_borderlines];
                ctrl.ForeColor = currentsettings.colors[Settings.CI.textbox_fore];
                ctrl.TextBackColor = currentsettings.colors[Settings.CI.textbox_back];
                ctrl.BackColor = currentsettings.colors[Settings.CI.form];
                ctrl.SelectedColor = currentsettings.colors[Settings.CI.textbox_fore].MultiplyBrightness(0.6F);

                if (currentsettings.buttonstyle.Equals(ButtonStyles[0])) // system
                    ctrl.checkbox.FlatStyle = FlatStyle.System;
                else
                    ctrl.checkbox.FlatStyle = FlatStyle.Popup;

                ctrl.checkbox.TickBoxReductionRatio = 0.75f;
                ctrl.checkbox.ForeColor = currentsettings.colors[Settings.CI.checkbox];
                ctrl.checkbox.CheckBoxColor = currentsettings.colors[Settings.CI.checkbox];
                Color inner = currentsettings.colors[Settings.CI.checkbox].Multiply(1.5F);
                if (inner.GetBrightness() < 0.1)        // double checking
                    inner = Color.Gray;
                ctrl.checkbox.CheckBoxInnerColor = inner;
                ctrl.checkbox.CheckColor = currentsettings.colors[Settings.CI.checkbox_tick];
                ctrl.checkbox.MouseOverColor = currentsettings.colors[Settings.CI.checkbox].Multiply(1.4F);

                ctrl.updown.BackColor = currentsettings.colors[Settings.CI.button_back];
                ctrl.updown.BorderColor = currentsettings.colors[Settings.CI.grid_borderlines];
                ctrl.updown.ForeColor = currentsettings.colors[Settings.CI.textbox_fore];
                ctrl.updown.MouseOverColor = currentsettings.colors[Settings.CI.checkbox].Multiply(1.4F);
                ctrl.updown.MouseSelectedColor = currentsettings.colors[Settings.CI.checkbox].Multiply(1.5F);
                return;     // don't do sub controls - we are in charge of them
            }
            else if (myControl is StatusStrip)
            {
                myControl.BackColor = currentsettings.colors[Settings.CI.form];
                myControl.ForeColor = currentsettings.colors[Settings.CI.label];
            }
            else if (myControl is ToolStrip)    // MenuStrip is a tool stip
            {
                myControl.Font = fnt;       // Toolstrips don't seem to inherit Forms font, so force

                foreach (ToolStripItem i in ((ToolStrip)myControl).Items)   // make sure any buttons have the button back colour set
                {
                    if (i is ToolStripButton || i is ToolStripDropDownButton)
                    {           // theme the back colour, this is the way its done.. not via the tool strip renderer
                        i.BackColor = currentsettings.colors[Settings.CI.button_back];
                    }
                    else if (i is ToolStripTextBox)
                    {
                        i.ForeColor = currentsettings.colors[Settings.CI.textbox_fore];
                        i.BackColor = currentsettings.colors[Settings.CI.textbox_back];
                    }

                    //??                    i.Font = fnt;
                }
            }
            else if (myControl is TabStrip)
            {
                TabStrip ts = myControl as TabStrip;
                //System.Diagnostics.Debug.WriteLine("*************** TAB Strip themeing" + myControl.Name + " " + myControl.Tag);
                ts.ForeColor = currentsettings.colors[Settings.CI.button_text];
                ts.DropDownBackgroundColor = currentsettings.colors[Settings.CI.button_back];
                ts.DropDownBorderColor = currentsettings.colors[Settings.CI.textbox_border];
                ts.DropDownScrollBarButtonColor = currentsettings.colors[Settings.CI.textbox_scrollbutton];
                ts.DropDownScrollBarColor = currentsettings.colors[Settings.CI.textbox_sliderback];
                ts.DropDownMouseOverBackgroundColor = currentsettings.colors[Settings.CI.button_back].Multiply(mouseoverscaling);
                ts.DropDownItemSeperatorColor = currentsettings.colors[Settings.CI.button_border];
                ts.EmptyColor = currentsettings.colors[Settings.CI.button_back];
                ts.SelectedBackColor = currentsettings.colors[Settings.CI.button_back];
            }
            else if (myControl is CompositeButton)
            {
                return;     // no themeing of it or sub controls
            }
            else if (myControl is TreeView)
            {
                TreeView ctrl = myControl as TreeView;
                ctrl.ForeColor = currentsettings.colors[Settings.CI.textbox_fore];
                ctrl.BackColor = currentsettings.colors[Settings.CI.textbox_back];
            }
            else if (myControl is CheckedIconListBoxForm)
            {
                CheckedIconListBoxForm ctrl = myControl as CheckedIconListBoxForm;

                if (currentsettings.buttonstyle.Equals(ButtonStyles[0])) // system
                    ctrl.FlatStyle = FlatStyle.System;
                else
                    ctrl.FlatStyle = FlatStyle.Popup;
            }
            else if (myControl is CompassControl)
            {
                CompassControl compassControl = myControl as CompassControl;
                compassControl.ForeColor = currentsettings.colors[Settings.CI.textbox_fore];
                compassControl.StencilColor = currentsettings.colors[Settings.CI.textbox_fore];
                compassControl.CentreTickColor = currentsettings.colors[Settings.CI.textbox_fore].Multiply(1.2F);
                compassControl.BugColor = currentsettings.colors[Settings.CI.textbox_fore].Multiply(0.8F);
                compassControl.BackColor = currentsettings.colors[Settings.CI.form];
            }
            else
            {
                if (!parentnamespace.Equals("ExtendedControls"))
                {
                    //Console.WriteLine("THEME: Unhandled control " + controltype.Name + ":" + myControl.Name + " from " + parent.Name);
                }
            }

            //System.Diagnostics.Debug.WriteLine("                  " + level + " Control " + myControl.Name + " " + myControl.Location + " " + myControl.Size);

            foreach (Control subC in myControl.Controls)
                UpdateControls(myControl, subC, fnt, level + 1);
        }


        private void UpdateToolsStripRenderer()
        {
            ThemeToolStripRenderer toolstripRenderer = ToolStripManager.Renderer as ThemeToolStripRenderer;

            if (toolstripRenderer == null)
                return;

            Color menuback = currentsettings.colors[Settings.CI.menu_back];
            bool toodark = (menuback.GetBrightness() < 0.1);

            toolstripRenderer.colortable.colMenuText = currentsettings.colors[Settings.CI.menu_fore];              // and the text
            toolstripRenderer.colortable.colMenuSelectedText = currentsettings.colors[Settings.CI.menu_dropdownfore];
            toolstripRenderer.colortable.colMenuBackground = menuback;
            toolstripRenderer.colortable.colMenuBarBackground = currentsettings.colors[Settings.CI.form];
            toolstripRenderer.colortable.colMenuBorder = currentsettings.colors[Settings.CI.button_border];
            toolstripRenderer.colortable.colMenuSelectedBack = currentsettings.colors[Settings.CI.menu_dropdownback];
            toolstripRenderer.colortable.colMenuHighlightBorder = currentsettings.colors[Settings.CI.button_border];
            toolstripRenderer.colortable.colMenuHighlightBack = toodark ? currentsettings.colors[Settings.CI.menu_dropdownback].Multiply(0.7F) : currentsettings.colors[Settings.CI.menu_back].Multiply(1.3F);        // whole menu back

            Color menuchecked = toodark ? currentsettings.colors[Settings.CI.menu_dropdownback].Multiply(0.8F) : currentsettings.colors[Settings.CI.menu_back].Multiply(1.5F);        // whole menu back

            toolstripRenderer.colortable.colCheckButtonChecked =  menuchecked;
            toolstripRenderer.colortable.colCheckButtonPressed =
            toolstripRenderer.colortable.colCheckButtonHighlighted = menuchecked.Multiply(1.1F);

            toolstripRenderer.colortable.colToolStripButtonCheckedBack = menuchecked;
            toolstripRenderer.colortable.colToolStripButtonPressedBack =
            toolstripRenderer.colortable.colToolStripButtonSelectedBack = menuchecked.Multiply(1.1F);

            toolstripRenderer.colortable.colToolStripBackground = currentsettings.colors[Settings.CI.toolstrip_back];
            toolstripRenderer.colortable.colToolStripBorder = currentsettings.colors[Settings.CI.toolstrip_border];
            toolstripRenderer.colortable.colToolStripSeparator = currentsettings.colors[Settings.CI.toolstrip_border];
            toolstripRenderer.colortable.colOverflowButton = currentsettings.colors[Settings.CI.menu_back];
            toolstripRenderer.colortable.colGripper = currentsettings.colors[Settings.CI.toolstrip_border];

            toolstripRenderer.colortable.colToolStripDropDownMenuImageMargin = currentsettings.colors[Settings.CI.button_back];
            toolstripRenderer.colortable.colToolStripDropDownMenuImageRevealed = Color.Purple;      // NO evidence, set to show up

        }

        public Color GroupBoxOverride(Control parent, Color d)      // if its a group box behind the control, use the group box back color..
        {
            Control x = parent;
            while( x != null )
            {
                if (x is GroupBox)
                    return currentsettings.colors[Settings.CI.group_back];
                x = x.Parent;
            }

            return d;
        }

        public List<string> GetThemeList()
        {
            List<string> result = new List<string>();

            for (int i = 0; i < themelist.Count; i++)
                result.Add(themelist[i].name);

            return result;
        }

        private int FindThemeIndex(string themename)
        {
            for (int i = 0; i < themelist.Count; i++)
            {
                if (themelist[i].name.Equals(themename))
                    return i;
            }

            return -1;
        }

        public int GetIndexOfCurrentTheme()
        {
            return FindThemeIndex(currentsettings.name);
        }

        public bool IsFontAvailableInTheme(string themename, out string fontwanted)
        {
            int i = FindThemeIndex(themename);
            fontwanted = null;

            if (i != -1)
            {
                int size = (int)themelist[i].fontsize;
                fontwanted = themelist[i].fontname;
                if (size < 1)
                    size = 9;

                using (Font fntnew = BaseUtils.FontLoader.GetFont(fontwanted, size))
                {
                    return string.Compare(fntnew.Name, fontwanted, true) == 0;
                }
            }

            return false;
        }

        public bool IsFontAvailable(string fontwanted)
        {
            try
            {           // user reports instance of it excepting over "Arial Narrow".. Mine does not
                using (Font fntnew = BaseUtils.FontLoader.GetFont(fontwanted, 12))
                {
                    return string.Compare(fntnew.Name, fontwanted, true) == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool SetThemeByName(string themename)                           // given a theme name, select it if possible
        {
            int i = FindThemeIndex(themename);
            if (i != -1)
            {
                currentsettings = new Settings(themelist[i]);           // do a copy, not a reference assign..
                return true;
            }
            return false;
        }

        public bool IsCustomTheme()
        { return currentsettings.name.Equals("Custom"); }
    }
}
