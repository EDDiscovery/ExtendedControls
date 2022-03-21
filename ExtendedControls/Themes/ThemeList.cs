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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ExtendedControls
{
    // Standard themes

    public class ThemeList
    {
        private List<Theme> themelist;

        public ThemeList()
        {
            themelist = new List<Theme>();           // theme list in
        }

        public void LoadBaseThemes()                                    // base themes load
        {
            themelist.Clear();

            themelist.Add(new Theme("Windows Default"));         // windows default..
            var wd = new Theme("Windows Default");
            wd.ButtonStyle = Theme.ButtonstyleGradient;
            wd.Name = "Windows Default Gradient Buttons";
            themelist.Add(wd);
            wd = new Theme("Windows Default");
            wd.ButtonStyle = Theme.ButtonstyleFlat;
            wd.Name = "Windows Default Flat Buttons";
            themelist.Add(wd);

            Color hgb = Color.FromArgb(255, 10, 40, 10);

            themelist.Add(new Theme("Orange Delight", Color.Black,
                Color.FromArgb(255, 48, 48, 48), Color.Orange, Color.DarkOrange, Theme.ButtonstyleGradient, // button
                Color.FromArgb(255, 176, 115, 0), Color.Black,  // grid border
                Color.Black, Color.Black, Color.Orange,Color.Orange,hgb,     // back/alt text/alt
                Color.DarkOrange, // borderlines
                Color.Black, Color.Orange, Color.DarkOrange, // grid slider, arrow, button
                Color.Red, Color.White, // travel
                Color.Black, Color.Orange, Color.Red, Color.Green, Color.DarkOrange, Theme.TextboxborderstyleColor, // text box
                Color.Black, Color.Orange, Color.DarkOrange, // text back, arrow, button
                Color.Orange, Color.FromArgb(255, 65, 33, 33), // checkbox
                Color.Black, Color.Orange, Color.DarkOrange, Color.Yellow,  // menu
                Color.Orange,  // label
                Color.FromArgb(255, 32, 32, 32), Color.Orange, Color.FromArgb(255, 130, 71, 0), // group back, text, border
                Color.DarkOrange, // tab control
                Color.Black, Color.DarkOrange, Color.Orange, // toolstrip
                Color.Orange, // spanel
                Color.Green, // overlay
                false, 95, "Microsoft Sans Serif", 8.25F));

            // ON purpose, always show them the euro caps one to give a hint!
            themelist.Add(new Theme(themelist[themelist.Count - 1], "Elite EuroCaps", "Euro Caps", 12F, 95));

            if (IsFontAvailable("Euro Caps"))
            {
                themelist.Add(new Theme(themelist[themelist.Count - 1], "Elite EuroCaps High DPI", "Euro Caps", 20F, 95));

                Color butback = Color.FromArgb(255, 32, 32, 32);
                themelist.Add(new Theme("Elite EuroCaps Less Border", Color.Black,
                    Color.FromArgb(255, 64, 64, 64), Color.Orange, Color.FromArgb(255, 96, 96, 96), Theme.ButtonstyleGradient, // button
                    Color.FromArgb(255, 176, 115, 0), Color.Black,  // grid border
                    butback, butback ,Color.Orange, Color.Orange, hgb, // back/alt fore/alt
                    Color.DarkOrange, // borderlines
                    butback, Color.Orange, Color.DarkOrange, // grid slider, arrow, button
                    Color.Red, Color.White, // travel
                    butback, Color.Orange, Color.Red, Color.Green, Color.FromArgb(255, 64, 64, 64), Theme.TextboxborderstyleColor, // text box
                    butback, Color.Orange, Color.DarkOrange, // text back, arrow, button
                    Color.Orange, Color.FromArgb(255, 65, 33, 33),// checkbox
                    Color.Black, Color.Orange, Color.DarkOrange, Color.Yellow,  // menu
                    Color.Orange,  // label
                    Color.Black, Color.Orange, Color.FromArgb(255, 130, 71, 0), // group
                    Color.DarkOrange, // tab control
                    Color.Black, Color.DarkOrange, Color.Orange, // toolstrips
                    Color.Orange, // spanel
                    Color.Green, // overlay
                    false, 100, "Euro Caps", 12F));
            }

            if (IsFontAvailable("Verdana"))
            {
                themelist.Add(new Theme(themelist[themelist.Count - 1], "Elite Verdana", "Verdana", 10F));
                themelist.Add(new Theme(themelist[themelist.Count - 1], "Elite Verdana Small", "Verdana", 8F));
                themelist.Add(new Theme(themelist[themelist.Count - 1], "Elite Verdana High DPI", "Verdana", 20F));
                var set = new Theme(themelist[themelist.Count - 1], "Elite Verdana Alt Grid", "Verdana", 10F);
                set.SetColor(Theme.CI.grid_altcellbackground, Color.FromArgb(255, 55, 55, 55));
                themelist.Add(new Theme(set, "Elite Verdana Alt Grid", "Verdana", 10F));
                themelist.Add(new Theme(set, "Elite Verdana Alt Grid High DPI", "Verdana", 20F));
            }

            if (IsFontAvailable("Calisto MT"))
            {
                themelist.Add(new Theme(themelist[themelist.Count - 1], "Elite Calisto", "Calisto MT", 12F));
                themelist.Add(new Theme(themelist[themelist.Count - 1], "Elite Calisto Small", "Calisto MT", 8F));
                themelist.Add(new Theme(themelist[themelist.Count - 1], "Elite Calisto High DPI", "Calisto MT", 20F));
            }

            themelist.Add(new Theme("Easy Dark", Color.FromArgb(255, 65, 65, 65), // form
                Color.FromArgb(255, 75, 75, 75), Color.FromArgb(255, 245, 120, 30), Color.FromArgb(255, 41, 46, 51), Theme.ButtonstyleFlat, // button back, text, border
                Color.FromArgb(255, 62, 68, 77), Color.FromArgb(255, 255, 120, 30), // grid borderback, bordertext
                Color.FromArgb(255, 79, 73, 68), Color.FromArgb(255, 79, 73, 68), Color.FromArgb(255, 223, 227, 238), Color.FromArgb(255, 223, 227, 238), hgb, //back/alt fore/alt
                Color.FromArgb(255, 50, 50, 50), // borderlines
                Color.FromArgb(255, 80, 75, 70), Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 75, 75, 75), // grid sliderback, arrow, scrollbutton
                Color.FromArgb(255, 192, 0, 0), Color.FromArgb(255, 202, 202, 255), // travelgrid_nonvisited, visited
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 248, 148, 6), Color.FromArgb(255, 90, 196, 90), Color.FromArgb(255, 46, 51, 56), Theme.TextboxborderstyleColor, // textbox back, fore, highlight, success, border
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 235, 110, 20), Color.FromArgb(255, 75, 75, 75), // text sliderback, scrollarrow, scrollbutton
                Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 235, 116, 20), // checkbox, checkboxtick
                Color.FromArgb(255, 60, 55, 50), Color.FromArgb(255, 233, 227, 238), Color.FromArgb(255, 60, 55, 50), Color.FromArgb(255, 245, 245, 245),  // menuback, fore, dropdownback, dropdownfore
                Color.FromArgb(255, 240, 240, 240),  // label
                Color.FromArgb(255, 45, 50, 55), Color.FromArgb(255, 235, 110, 20), Color.FromArgb(255, 60, 55, 50), // group back, text, border
                Color.FromArgb(255, 40, 45, 50), // tab control borderlines
                Color.FromArgb(255, 75, 75, 75), Color.FromArgb(255, 45, 50, 55), Color.FromArgb(255, 40, 45, 50), // toolstrip, back, border
                Color.FromArgb(255, 250, 150, 8), // spanel
                Color.Green, // overlay
                false, 100, "Arial", 9.75F));

            themelist.Add(new Theme(themelist[themelist.Count - 1], "Easy Dark High DPI", "Arial", 20F));

            themelist.Add(new Theme("EDSM", Color.FromArgb(255, 39, 43, 48), // form
                Color.FromArgb(255, 71, 77, 84), Color.FromArgb(255, 245, 245, 245), Color.FromArgb(255, 41, 46, 51), Theme.ButtonstyleFlat, // button back, text, border
                Color.FromArgb(255, 62, 68, 77), Color.FromArgb(255, 200, 200, 200), // grid borderback, bordertext
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 200, 200, 200), hgb, // back/alt fore/alt
                Color.FromArgb(255, 62, 68, 77), // borderlines
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 72, 78, 85), // grid sliderback, arrow, scrollbutton
                Color.FromArgb(255, 200, 0, 0), Color.FromArgb(255, 90, 196, 222), // travelgrid_nonvisited, visited
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 248, 148, 6), Color.FromArgb(255, 90, 196, 90), Color.FromArgb(255, 46, 51, 56), Theme.TextboxborderstyleColor, // textbox back, fore, highlight, success, border
                Color.FromArgb(255, 28, 30, 34), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 72, 78, 85), // text sliderback, scrollarrow, scrollbutton
                Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 0, 129, 0), // checkbox, checkboxtick
                Color.FromArgb(255, 58, 63, 68), Color.FromArgb(255, 245, 245, 245), Color.FromArgb(255, 58, 63, 68), Color.FromArgb(255, 200, 200, 200),  // menuback, fore, dropdownback, dropdownfore
                Color.FromArgb(255, 200, 200, 200),  // label
                Color.FromArgb(255, 46, 51, 56), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(255, 41, 46, 51), // group back, text, border
                Color.FromArgb(255, 41, 46, 51), // tab control borderlines
                Color.FromArgb(255, 71, 77, 84), Color.FromArgb(255, 46, 51, 56), Color.FromArgb(255, 41, 46, 51), // toolstrip, back, border
                Color.FromArgb(255, 255, 0, 0), // spanel
                Color.Green, // overlay
                false, 100, "Arial", 10.25F));

            themelist.Add(new Theme(themelist[themelist.Count - 1], "EDSM High DPI", "Arial", 20F));

            if (IsFontAvailable("Arial Narrow"))
            {
                themelist.Add(new Theme(themelist[themelist.Count - 1], "EDSM Arial Narrow", "Arial Narrow", 10.25F, 95));
                themelist.Add(new Theme(themelist[themelist.Count - 1], "EDSM Arial Narrow High DPI", "Arial Narrow", 20F, 95));
            }
            if (IsFontAvailable("Euro Caps"))
            {
                themelist.Add(new Theme(themelist[themelist.Count - 1], "EDSM EuroCaps", "Euro Caps", 10.25F, 95));
                themelist.Add(new Theme(themelist[themelist.Count - 1], "EDSM EuroCaps High DPI", "Euro Caps", 20F, 95));
            }

            themelist.Add(new Theme("Material Dark", Color.FromArgb(255, 54, 57, 63), // form
                Color.FromArgb(255, 75, 75, 75), Color.FromArgb(255, 255, 160, 0), Color.FromArgb(255, 41, 46, 51), Theme.ButtonstyleFlat, // button back, text, border
                Color.FromArgb(255, 47, 49, 54), Color.FromArgb(255, 255, 160, 0), // grid borderback, bordertext
                Color.FromArgb(255, 37, 37, 38), Color.FromArgb(255, 37, 37, 38), Color.FromArgb(255, 223, 227, 238), Color.FromArgb(255, 223, 227, 238), hgb,//back/alt fore/alt
                Color.FromArgb(255, 82, 94, 164), // borderlines
                Color.FromArgb(255, 37, 37, 38), Color.FromArgb(255, 82, 94, 164), Color.FromArgb(255, 82, 94, 164), // grid sliderback, arrow, scrollbutton
                Color.FromArgb(255, 192, 0 , 0), Color.FromArgb(255, 202, 202, 255), // travelgrid_nonvisited, visited
                Color.FromArgb(255, 47, 49, 54), Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 255, 160, 0), Color.FromArgb(255, 90, 196, 90), Color.FromArgb(255, 82, 94, 164), Theme.TextboxborderstyleColor, // textbox back, fore, highlight, success, border
                Color.FromArgb(255, 47, 49, 54), Color.FromArgb(255, 255, 160, 0), Color.FromArgb(255, 75, 75, 75), // text sliderback, scrollarrow, scrollbutton
                Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 255, 160, 0), // checkbox, checkboxtick
                Color.FromArgb(255, 60, 55, 50), Color.FromArgb(255, 196, 196, 196), Color.FromArgb(255, 60, 55, 50), Color.FromArgb(255, 245, 245, 245),  // menuback, fore, dropdownback, dropdownfore
                Color.FromArgb(255, 240, 240, 240),  // label
                Color.FromArgb(255, 45, 50, 55), Color.FromArgb(255, 255, 160, 0), Color.FromArgb(255, 129, 138, 192), // group back, text, border
                Color.FromArgb(255, 40, 45, 50), // tab control borderlines
                Color.FromArgb(255, 75, 75, 75), Color.FromArgb(255, 45, 50, 55), Color.FromArgb(255, 40, 45, 50), // toolstrip, back, border
                Color.FromArgb(255, 250, 150, 8), // spanel
                Color.Green, // overlay
                false, 100, "Microsoft Sans Serif", 9.75F));

            themelist.Add(new Theme(themelist[themelist.Count - 1], "Material Dark High DPI", "Microsoft Sans Serif", 20F));

            Color r1 = Color.FromArgb(255, 160, 0, 0);
            Color r2 = Color.FromArgb(255, 64, 0, 0);
            themelist.Add(new Theme("Night Vision", Color.Black,
                Color.FromArgb(255, 48, 48, 48), r1, r2, Theme.ButtonstyleGradient, // button
                r2, Color.Black,  // grid border/text
                Color.Black, Color.Black, r1, r1, hgb, // back/alt fore/alt
                r2, // borderlines
                Color.Black, r1, r2, // grid slider, arrow, button
                Color.Red, Color.Green, // travel
                Color.Black, r1, Color.Orange, Color.Green, r2, Theme.TextboxborderstyleColor, // text box
                Color.Black, r1, r2, // text back, arrow, button
                r1, Color.FromArgb(255, 65, 33, 33), // checkbox
                Color.Black, r1, r2, Color.Yellow,  // menu
                r1,  // label
                Color.FromArgb(255, 8, 8, 8), r1, Color.FromArgb(255, 130, 71, 0), // group
                r2, // tab control
                Color.Black, r2, r1, // toolstrip
                r1, // spanel
                Color.Green, // overlay
                false, 95, "Microsoft Sans Serif", 10F));

            if (IsFontAvailable("Euro Caps"))
                themelist.Add(new Theme(themelist[themelist.Count - 1], "Night Vision EuroCaps", "Euro Caps", 12F, 95));

            if (IsFontAvailable("Euro Caps"))
            {
                themelist.Add(new Theme("EuroCaps Grey",
                                        SystemColors.Menu,
                                        SystemColors.Control, SystemColors.ControlText, Color.DarkGray, Theme.ButtonstyleGradient,// button
                                        SystemColors.Menu, SystemColors.MenuText,  // grid border
                                        SystemColors.ControlLightLight, SystemColors.ControlLightLight, SystemColors.MenuText, SystemColors.MenuText, Color.FromArgb(255,30,192,30), //back/alt fore/alt
                                        SystemColors.ControlDark, // borderlines
                                        SystemColors.ControlLightLight, SystemColors.MenuText, SystemColors.ControlDark, // grid scroll
                                        Color.Blue, SystemColors.MenuText, // travel
                                        SystemColors.Window, SystemColors.WindowText, Color.Red, Color.Green, Color.DarkGray, Theme.TextboxborderstyleColor,// text
                                        SystemColors.ControlLightLight, SystemColors.MenuText, SystemColors.ControlDark, // text box
                                        SystemColors.MenuText, SystemColors.MenuHighlight, // checkbox
                                        SystemColors.Menu, SystemColors.MenuText, SystemColors.ControlLightLight, SystemColors.MenuText,  // menu
                                        SystemColors.MenuText,  // label
                                        SystemColors.Menu, SystemColors.MenuText, SystemColors.ControlDark, // group
                                        SystemColors.ControlDark, // tab control
                                        SystemColors.Menu, SystemColors.Menu, SystemColors.MenuText,  // toolstrip
                                        SystemColors.ControlLightLight, // spanel
                                        Color.Green, // overlay
                                        false, 95, "Euro Caps", 12F));
            }

            if (IsFontAvailable("Verdana"))
            {
                themelist.Add(new Theme("Verdana Grey",
                                        SystemColors.Menu,
                                        SystemColors.Control, SystemColors.ControlText, Color.DarkGray, Theme.ButtonstyleGradient,// button
                                        SystemColors.Menu, SystemColors.MenuText,  // grid border
                                        SystemColors.ControlLightLight, SystemColors.ControlLightLight, SystemColors.MenuText, SystemColors.MenuText, Color.FromArgb(255, 30, 192, 30), //back/alt fore/alt
                                        SystemColors.ControlDark, // borderlines
                                        SystemColors.ControlLightLight, SystemColors.MenuText, SystemColors.ControlDark, // grid scroll
                                        Color.Blue, SystemColors.MenuText, // travel
                                        SystemColors.Window, SystemColors.WindowText, Color.Red, Color.Green, Color.DarkGray, Theme.TextboxborderstyleColor,// text
                                        SystemColors.ControlLightLight, SystemColors.MenuText, SystemColors.ControlDark, // text box
                                        SystemColors.MenuText, SystemColors.MenuHighlight, // checkbox
                                        SystemColors.Menu, SystemColors.MenuText, SystemColors.ControlLightLight, SystemColors.MenuText,  // menu
                                        SystemColors.MenuText,  // label
                                        SystemColors.Menu, SystemColors.MenuText, SystemColors.ControlDark, // group
                                        SystemColors.ControlDark, // tab control
                                        SystemColors.Menu, SystemColors.Menu, SystemColors.MenuText,  // toolstrip
                                        SystemColors.ControlLightLight, // spanel
                                        Color.Green, // overlay
                                        false, 95, "Verdana", 8F));
            }

            themelist.Add(new Theme("Blue Wonder", Color.DarkBlue,
                                               Color.Blue, Color.White, Color.White, Theme.ButtonstyleGradient,// button
                                               Color.DarkBlue, Color.White,  // grid border
                                               Color.DarkBlue, Color.DarkBlue, Color.White, Color.White, hgb, // back/alt fore/alt
                                               Color.Blue, // borderlines
                                               Color.DarkBlue, Color.White, Color.Blue, // grid scroll
                                               Color.Red, Color.Cyan, // travel
                                               Color.DarkBlue, Color.White, Color.Red, Color.Green, Color.White, Theme.TextboxborderstyleColor,// text box
                                               Color.DarkBlue, Color.White, Color.Blue, // text scroll
                                               Color.White, Color.Black, // checkbox
                                               Color.DarkBlue, Color.White, Color.DarkBlue, Color.White,  // menu
                                               Color.White,  // label
                                               Color.DarkBlue, Color.White, Color.Blue, // group
                                               Color.Blue,
                                               Color.DarkBlue, Color.White, Color.Red,  // toolstrip
                                               Color.LightBlue, // spanel
                                               Color.Green, // overlay
                                               false, 95, "Microsoft Sans Serif", 8.25F));

            Color baizegreen = Color.FromArgb(255, 13, 68, 13);
            themelist.Add(new Theme("Green Baize", baizegreen,
                                               baizegreen, Color.White, Color.White, Theme.ButtonstyleGradient,// button
                                               baizegreen, Color.White,  // grid border
                                               baizegreen, baizegreen, Color.White, Color.White, Color.FromArgb(255,30,192,30),//back/alt fore/alt
                                               Color.LightGreen, // borderlines
                                               baizegreen, Color.White, Color.LightGreen, // grid scroll
                                               Color.Red, Color.FromArgb(255, 78, 190, 27), // travel
                                               baizegreen, Color.White, Color.Red, Color.Green, Color.White, Theme.TextboxborderstyleColor,// text box
                                               baizegreen, Color.White, Color.LightGreen, // text scroll
                                               Color.White, Color.Black, // checkbox
                                               baizegreen, Color.White, baizegreen, Color.White,  // menu
                                               Color.White,  // label
                                               baizegreen, Color.White, Color.LightGreen, // group
                                               Color.LightGreen,    // tabcontrol
                                               baizegreen, Color.White, Color.White,
                                               baizegreen,
                                               Color.Green, // overlay
                                               false, 95, "Microsoft Sans Serif", 8.25F));


            if (IsFontAvailable("Verdana"))     
            {                                                                               // exported via theme load in EDDiscovery
                themelist.Add(new Theme("Deep Blue Sky",
                            Color.FromArgb(255, 0, 0, 0), // form
                            Color.FromArgb(255, 64, 64, 64), // button_back
                            Color.FromArgb(255, 100, 177, 255), // button_text
                            Color.FromArgb(255, 96, 96, 96), // button_border
                            Theme.ButtonstyleGradient,
                            Color.FromArgb(255, 0, 84, 168), // grid_borderback
                            Color.FromArgb(255, 202, 228, 255), // grid_bordertext
                            Color.FromArgb(255, 32, 32, 32), // grid_cellbackground
                            Color.FromArgb(255, 32, 32, 32), // grid_altcellbackground
                            Color.FromArgb(255, 130, 192, 255), // grid_celltext
                            Color.FromArgb(255, 130, 192, 255), // grid_altcelltext
                            hgb,
                            Color.FromArgb(255, 0, 50, 100), // grid_borderlines
                            Color.FromArgb(255, 32, 32, 32), // grid_sliderback
                            Color.FromArgb(255, 98, 176, 255), // grid_scrollarrow
                            Color.FromArgb(255, 55, 155, 255), // grid_scrollbutton
                            Color.FromArgb(255, 0, 128, 255), // travelgrid_nonvisted
                            Color.FromArgb(255, 255, 255, 255), // travelgrid_visited
                            Color.FromArgb(255, 32, 32, 32), // textbox_back
                            Color.FromArgb(255, 91, 173, 255), // textbox_fore
                            Color.FromArgb(255, 255, 0, 0), // textbox_highlight
                            Color.FromArgb(255, 0, 128, 0), // textbox_success
                            Color.FromArgb(255, 64, 64, 64), // textbox_border
                            Theme.TextboxborderstyleColor,
                            Color.FromArgb(255, 32, 32, 32), // textbox_sliderback
                            Color.FromArgb(255, 85, 170, 255), // textbox_scrollarrow
                            Color.FromArgb(255, 0, 128, 255), // textbox_scrollbutton
                            Color.FromArgb(255, 0, 128, 255), // checkbox
                            Color.FromArgb(255, 65, 33, 33), // checkbox_tick
                            Color.FromArgb(255, 0, 0, 0), // menu_back
                            Color.FromArgb(255, 113, 184, 255), // menu_fore
                            Color.FromArgb(255, 0, 98, 196), // menu_dropdownback
                            Color.FromArgb(255, 193, 224, 255), // menu_dropdownfore
                            Color.FromArgb(255, 0, 128, 255), // label
                            Color.FromArgb(255, 0, 0, 0), // group_back
                            Color.FromArgb(255, 117, 186, 255), // group_text
                            Color.FromArgb(255, 130, 71, 0), // group_borderlines
                            Color.FromArgb(255, 0, 50, 100), // tabcontrol_borderlines
                            Color.FromArgb(255, 0, 0, 0), // toolstrip_back
                            Color.FromArgb(255, 0, 50, 100), // toolstrip_border
                            Color.FromArgb(255, 0, 0, 0), // unused_entry
                            Color.FromArgb(255, 0, 128, 255), // s_panel
                            Color.FromArgb(255, 0, 128, 0), // transparentcolorkey
                            false, 100, "Verdana", 8));
                themelist.Add(new Theme(themelist[themelist.Count - 1], "Deep Blue Sky High DPI", "Verdana", 20));
            }
        }

        public bool Load(string path, string filewildcard)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                FileInfo[] allFiles = dirInfo.GetFiles(filewildcard);

                foreach (FileInfo fi in allFiles)
                {
                    Theme set = new Theme();
                    string name = Path.GetFileNameWithoutExtension(fi.Name);
                    if ( set.LoadFile(fi.FullName, name))
                    {
                        int cur = FindThemeIndex(name);
                        if (cur >= 0)
                            themelist.RemoveAt(cur);        // don't duplicate names in list
                        themelist.Add(set);
                    }
                }

                return true;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine($"Theme load file error {ex}");
                return false;
            }
        }


        public List<string> GetThemeNames()
        {
            return themelist.Select(x => x.Name).ToList();
        }

        public int FindThemeIndex(string themename)     // -1 not
        {
            return themelist.FindIndex(x => x.Name.Equals(themename));
        }

        public Theme FindTheme(string themename)
        {
            return themelist.Find(x => x.Name.Equals(themename));
        }

        public bool IsFontAvailableInTheme(string themename, out string fontwanted)
        {
            int i = FindThemeIndex(themename);
            fontwanted = null;

            if (i != -1)
            {
                int size = (int)themelist[i].FontSize;
                fontwanted = themelist[i].FontName;
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

        public bool SetThemeByName(string themename)               // given a theme name, select it if possible
        {
            int i = FindThemeIndex(themename);
            if (i != -1)
            {
                Theme.Current = new Theme(themelist[i]);           // do a copy, not a reference assign..
                return true;
            }
            return false;
        }
    }
}
