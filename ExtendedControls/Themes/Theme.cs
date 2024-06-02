/*
 * Copyright © 2016-2024 EDDiscovery development team
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

        private static float minfontsize = 4;

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
            
            toolstrip_back, toolstrip_border, unused_entry,     // previously assigned to toolstrip_checkbox thing
            
            s_panel, transparentcolorkey, grid_highlightback,
        };

        private string name { get; set; }         // name of scheme
        private Dictionary<CI, Color> colors { get; set;}       // dictionary of colors, indexed by CI.
        private bool windowsframe { get; set; }
        private double formopacity { get; set;}
        private string fontname { get; set; }         // Font.. (empty means don't override)
        private float fontsize { get; set; }
        private string buttonstyle { get; set; }
        private string textboxborderstyle { get; set;}

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

        public string Name { get { return name; } set { name = value; } }
        
        public Color Form { get { return colors[CI.form]; } }       // in enum order..

        public Color ButtonBackColor { get { return colors[CI.button_back]; }  }
        public Color ButtonBorderColor { get { return colors[CI.button_border]; } }
        public Color ButtonTextColor { get { return colors[CI.button_text]; }  }

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
        public Color TextBlockHighlightColor { get { return colors[CI.textbox_highlight]; }  }
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

        public Color SPanelColor { get { return colors[CI.s_panel]; }  }
        public Color TransparentColorKey { get { return colors[CI.transparentcolorkey]; } }
        public Color GridHighlightBack { get { return colors[CI.grid_highlightback]; }  }

        public bool WindowsFrame { get { return windowsframe; } set { windowsframe = value; } }
        public double Opacity { get { return formopacity; } set { formopacity = value; }  }
        public string FontName { get { return fontname; }  set { fontname = value; } }
        public float FontSize { get { return fontsize; } set { fontsize = value; } }
        public string ButtonStyle { get { return buttonstyle; } set { buttonstyle = value; } }
        public string TextBoxBorderStyle { get { return textboxborderstyle; } set { textboxborderstyle = value; } }
        public Size IconSize { get { var ft = GetFont; return new Size(ft.ScalePixels(36), ft.ScalePixels(36)); } } // calculated rep scaled icon size to use

        public void SetCustom()
        { Name = "Custom"; }                                // set so custom..
        public bool IsCustom()  { return Name.Equals("Custom"); }
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

            if ((ToolStripManager.Renderer as ThemeToolStripRenderer ) == null)      // not installed..   install one
                ToolStripManager.Renderer = new ThemeToolStripRenderer();

            UpdateToolsStripRenderer(ToolStripManager.Renderer as ThemeToolStripRenderer);

            return WindowsFrame;
        }

        const float mouseoverscaling = 1.3F;
        const float mouseselectedscaling = 1.5F;

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
                f.BackColor = colors[CI.form];
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

                ctrl.TextBoxForeColor = colors[CI.textbox_fore];
                ctrl.TextBoxBackColor = colors[CI.textbox_back];

                ctrl.ScrollBarFlatStyle = FlatStyle.System;

                if (textboxborderstyle.Equals(TextboxBorderStyles[1]))
                    ctrl.BorderStyle = BorderStyle.FixedSingle;
                else if (textboxborderstyle.Equals(TextboxBorderStyles[2]))
                    ctrl.BorderStyle = BorderStyle.Fixed3D;
                else if (textboxborderstyle.Equals(TextboxBorderStyles[3]))
                {
                    Color c1 = colors[CI.textbox_scrollbutton];
                    ctrl.BorderColor = colors[CI.textbox_border];
                    ctrl.ScrollBarBackColor = colors[CI.textbox_back];
                    ctrl.ScrollBarSliderColor = colors[CI.textbox_sliderback];
                    ctrl.ScrollBarBorderColor = ctrl.ScrollBarThumbBorderColor =
                                ctrl.ScrollBarArrowBorderColor = colors[CI.textbox_border];
                    ctrl.ScrollBarArrowButtonColor = ctrl.ScrollBarThumbButtonColor = c1;
                    ctrl.ScrollBarMouseOverButtonColor = c1.Multiply(mouseoverscaling);
                    ctrl.ScrollBarMousePressedButtonColor = c1.Multiply(mouseselectedscaling);
                    ctrl.ScrollBarForeColor = colors[CI.textbox_scrollarrow];
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
                ctrl.ForeColor = colors[CI.textbox_fore];
                ctrl.BackColor = colors[CI.textbox_back];
                ctrl.BackErrorColor = colors[CI.textbox_highlight];
                ctrl.ControlBackground = colors[CI.textbox_back]; // previously, but not sure why, GroupBoxOverride(parent, colors[CI.form]);
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
                    ctrl.BorderColor = colors[CI.textbox_border];

                if (myControl is ExtTextBoxAutoComplete || myControl is ExtDataGridViewColumnAutoComplete.CellEditControl) // derived from text box
                {
                    ExtTextBoxAutoComplete actb = myControl as ExtTextBoxAutoComplete;
                    actb.DropDownBackgroundColor = colors[CI.button_back];
                    actb.DropDownBorderColor = colors[CI.textbox_border];
                    actb.DropDownScrollBarButtonColor = colors[CI.textbox_scrollbutton];
                    actb.DropDownScrollBarColor = colors[CI.textbox_sliderback];
                    actb.DropDownMouseOverBackgroundColor = colors[CI.button_back].Multiply(mouseoverscaling);

                    if (buttonstyle.Equals(ButtonStyles[0]))
                        actb.FlatStyle = FlatStyle.System;
                    else if (buttonstyle.Equals(ButtonStyles[1])) // flat
                        actb.FlatStyle = FlatStyle.Flat;
                    else
                        actb.FlatStyle = FlatStyle.Popup;
                }

                ThemeButton(ctrl.EndButton);
                ctrl.EndButton.FlatAppearance.BorderColor =             // override some of them to make back of button disappear
                ctrl.EndButton.BackColor = colors[CI.textbox_back];
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
                    ctrl.FlatStyle = (buttonstyle.Equals(ButtonStyles[1])) ? FlatStyle.Flat : FlatStyle.Popup;
                    ctrl.TabControlBorderColor = colors[CI.tabcontrol_borderlines].Multiply(0.6F);
                    ctrl.TabControlBorderBrightColor = colors[CI.tabcontrol_borderlines];
                    ctrl.TabNotSelectedBorderColor = colors[CI.tabcontrol_borderlines].Multiply(0.4F);
                    ctrl.TabNotSelectedColor = colors[CI.button_back];
                    ctrl.TabSelectedColor = colors[CI.button_back].Multiply(mouseselectedscaling);
                    ctrl.TabMouseOverColor = colors[CI.button_back].Multiply(mouseoverscaling);
                    ctrl.TextSelectedColor = colors[CI.button_text];
                    ctrl.TextNotSelectedColor = colors[CI.button_text].Multiply(0.8F);
                    ctrl.TabStyle = new ExtendedControls.TabStyleAngled();
                }
                else
                    ctrl.FlatStyle = FlatStyle.System;

            }
            else if (myControl is ExtListBox)
            {
                ExtListBox ctrl = (ExtListBox)myControl;
                ctrl.ForeColor = colors[CI.button_text];
                ctrl.ItemSeperatorColor = colors[CI.button_border];

                if (buttonstyle.Equals(ButtonStyles[0]))
                {
                    ctrl.FlatStyle = FlatStyle.System;
                }
                else
                {
                    ctrl.BackColor = colors[CI.button_back];
                    ctrl.BorderColor = colors[CI.button_border];
                    ctrl.ScrollBarButtonColor = colors[CI.textbox_scrollbutton];
                    ctrl.ScrollBarColor = colors[CI.textbox_sliderback];

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
                var ctrl = (ExtChart)myControl;

                ctrl.Font = fnt;        // log the font with the chart, so you can use it directly in further explicit themeing
                ctrl.BackColor = colors[CI.form];

                // so the themer only overrides border/back colours if the user has set them to a value already. It does not override empty entries 
                // the user can chose if titles/legends border and back is themed

                ctrl.SetAllTitlesColorFont(colors[CI.grid_celltext], GetScaledFont(1.5f), colors[CI.grid_cellbackground],
                                          Color.Empty, 1,
                                          colors[CI.group_borderlines], 1, ChartDashStyle.Solid);

                ctrl.SetAllLegendsColorFont(colors[CI.grid_celltext], fnt, colors[CI.grid_cellbackground].Multiply(1.2f), 6, Color.FromArgb(128, 0, 0, 0),
                                            colors[CI.grid_celltext], colors[CI.grid_cellbackground], fnt, StringAlignment.Center, LegendSeparatorStyle.Line, colors[CI.group_borderlines],
                                            colors[CI.group_borderlines], ChartDashStyle.Solid, 1,
                                            LegendSeparatorStyle.Line, colors[CI.group_borderlines], 1);

                // we theme all chart areas, backwards, so chartarea0 is the one left selected            
                for (int i = ctrl.ChartAreas.Count - 1; i >= 0; i--)
                {
                    ctrl.SetCurrentChartArea(i);
                    ctrl.SetChartAreaColors(colors[CI.grid_cellbackground], colors[CI.group_borderlines]);

                    ctrl.SetXAxisMajorGridWidthColor(1, ChartDashStyle.Solid, colors[CI.group_borderlines]);
                    ctrl.SetYAxisMajorGridWidthColor(1, ChartDashStyle.Solid, colors[CI.group_borderlines]);
                    ctrl.SetXAxisLabelColorFont(colors[CI.grid_celltext], fnt);
                    ctrl.SetYAxisLabelColorFont(colors[CI.grid_celltext], fnt);

                    ctrl.SetXCursorColors(colors[CI.grid_scrollarrow], colors[CI.grid_celltext], 2);
                    ctrl.SetYCursorColors(colors[CI.grid_scrollarrow], colors[CI.grid_celltext], 2);

                    ctrl.SetXCursorScrollBarColors(colors[CI.grid_sliderback], colors[CI.grid_scrollbutton]);
                    ctrl.SetYCursorScrollBarColors(colors[CI.grid_sliderback], colors[CI.grid_scrollbutton]);
                }

                for (int i = ctrl.Series.Count - 1; i >= 0; i--)
                {
                    ctrl.SetCurrentSeries(i);
                    if (i == 0)
                        ctrl.SetSeriesColor(colors[CI.grid_celltext]);
                    ctrl.SetSeriesDataLabelsColorFont(colors[CI.grid_celltext], fnt, Color.Transparent);
                    ctrl.SetSeriesMarkersColorSize(colors[CI.grid_scrollarrow], 4, colors[CI.grid_scrollbutton], 2);
                }
            }
            else if (myControl is Chart)
            {
                System.Diagnostics.Debug.Assert(false, "Warning - Chart not allowed");
            }
            else if (myControl is MultiPipControl)
            {
                MultiPipControl ctrl = (MultiPipControl)myControl;
                ctrl.ForeColor = colors[CI.button_text];
                ctrl.PipColor = colors[CI.button_text];
                ctrl.HalfPipColor = colors[CI.button_text].MultiplyBrightness(0.6f);
                ctrl.BorderColor = colors[CI.grid_borderlines];
            }
            else if (myControl is ExtPanelResizer)      // Resizers only show when no frame is on
            {
                myControl.Visible = !WindowsFrame;
            }
            else if (myControl is ExtPanelDropDown)
            {
                ExtPanelDropDown ctrl = (ExtPanelDropDown)myControl;
                ctrl.ForeColor = colors[CI.button_text];
                ctrl.SelectionMarkColor = ctrl.ForeColor;
                ctrl.BackColor = ctrl.SelectionBackColor = colors[CI.button_back];
                ctrl.BorderColor = colors[CI.button_border];
                ctrl.MouseOverBackgroundColor = colors[CI.button_back].Multiply(mouseoverscaling);
                ctrl.ScrollBarButtonColor = colors[CI.textbox_scrollbutton];
                ctrl.ScrollBarColor = colors[CI.textbox_sliderback];
                ctrl.FlatStyle = FlatStyle.Popup;
            }
            else if (myControl is ExtComboBox)
            {
                ExtComboBox ctrl = (ExtComboBox)myControl;
                ctrl.ForeColor = colors[CI.button_text];

                if (buttonstyle.Equals(ButtonStyles[0])) // system
                {
                    ctrl.FlatStyle = FlatStyle.System;
                }
                else
                {
                    ctrl.BackColor = ctrl.DropDownBackgroundColor = colors[CI.button_back];
                    ctrl.BorderColor = colors[CI.button_border];
                    ctrl.MouseOverBackgroundColor = colors[CI.button_back].Multiply(mouseoverscaling);
                    ctrl.ScrollBarButtonColor = colors[CI.textbox_scrollbutton];
                    ctrl.ScrollBarColor = colors[CI.textbox_sliderback];

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
                myControl.ForeColor = colors[CI.textbox_fore];
            }
            else if (myControl is ExtButtonDrawn)
            {
                ExtButtonDrawn ctrl = (ExtButtonDrawn)myControl;
                ctrl.BackColor = colors[CI.form];
                ctrl.ForeColor = colors[CI.label];
                ctrl.MouseOverColor = colors[CI.label].Multiply(mouseoverscaling);
                ctrl.MouseSelectedColor = colors[CI.label].Multiply(mouseselectedscaling);
                ctrl.BorderWidth = 2;
                ctrl.BorderColor = colors[CI.grid_borderlines];

                System.Drawing.Imaging.ColorMap colormap = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images
                colormap.OldColor = Color.White;                                                        // white is defined as the forecolour
                colormap.NewColor = ctrl.ForeColor;
                ctrl.SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap });
                //System.Diagnostics.Debug.WriteLine("Drawn Panel Image button " + ctrl.Name);
            }
            else if (myControl is TableLayoutPanel)
            {
                myControl.BackColor = GroupBoxOverride(parent, colors[CI.form]);
            }
            else if (myControl is ExtPanelRollUp)
            {
                myControl.BackColor = paneldebugmode ? Color.Green : GroupBoxOverride(parent, colors[CI.form]);
            }
            else if (myControl is FlowLayoutPanel)
            {
                FlowLayoutPanel ctrl = myControl as FlowLayoutPanel;
                ctrl.BackColor = paneldebugmode ? Color.Red : GroupBoxOverride(parent, colors[CI.form]);
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
                    myControl.BackColor = paneldebugmode ? Color.Blue : colors[CI.form];
                    myControl.ForeColor = colors[CI.label];
                }
            }
            else if (myControl is Label)
            {
                myControl.ForeColor = colors[CI.label];

                if (myControl is ExtLabel)
                    (myControl as ExtLabel).TextBackColor = colors[CI.form];
            }
            else if (myControl is ExtGroupBox)
            {
                ExtGroupBox ctrl = (ExtGroupBox)myControl;
                ctrl.ForeColor = colors[CI.group_text];
                ctrl.BackColor = colors[CI.group_back];
                ctrl.BorderColor = colors[CI.group_borderlines];
                ctrl.FlatStyle = FlatStyle.Flat;           // always in Flat, always apply our border.
            }
            else if (myControl is ExtCheckBox)
            {
                ExtCheckBox ctrl = (ExtCheckBox)myControl;

                ctrl.BackColor = GroupBoxOverride(parent, colors[CI.form]);

                if (ctrl.Appearance == Appearance.Button)
                {
                    ctrl.ForeColor = colors[CI.button_text];
                    ctrl.MouseOverColor = colors[CI.button_back].Multiply(mouseoverscaling);
                    ctrl.CheckColor = colors[CI.button_back].Multiply(0.9f);

                    if (buttonstyle.Equals(ButtonStyles[0])) // system
                        ctrl.FlatStyle = FlatStyle.Standard;
                    else if (buttonstyle.Equals(ButtonStyles[1])) // flat
                        ctrl.FlatStyle = FlatStyle.Flat;
                    else
                        ctrl.FlatStyle = FlatStyle.Popup;
                }
                else
                {
                    ctrl.ForeColor = colors[CI.checkbox];
                    ctrl.CheckBoxColor = colors[CI.checkbox];
                    ctrl.CheckBoxInnerColor = colors[CI.checkbox].Multiply(1.5F);
                    ctrl.MouseOverColor = colors[CI.checkbox].Multiply(1.4F);
                    ctrl.TickBoxReductionRatio = 0.75f;
                    ctrl.CheckColor = colors[CI.checkbox_tick];

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

                ctrl.BackColor = GroupBoxOverride(parent, colors[CI.form]);
                ctrl.ForeColor = colors[CI.checkbox];
                ctrl.RadioButtonColor = colors[CI.checkbox];
                ctrl.RadioButtonInnerColor = colors[CI.checkbox].Multiply(1.5F);
                ctrl.SelectedColor = ctrl.BackColor.Multiply(0.75F);
                ctrl.MouseOverColor = colors[CI.checkbox].Multiply(1.4F);
            }
            else if (myControl is DataGridView)                     // we theme this directly
            {
                DataGridView ctrl = (DataGridView)myControl;
                ctrl.EnableHeadersVisualStyles = false;            // without this, the colours for the grid are not applied.

                ctrl.RowHeadersDefaultCellStyle.BackColor = colors[CI.grid_borderback];
                ctrl.RowHeadersDefaultCellStyle.ForeColor = colors[CI.grid_bordertext];
                ctrl.RowHeadersDefaultCellStyle.SelectionBackColor = colors[CI.grid_borderback];
                ctrl.ColumnHeadersDefaultCellStyle.BackColor = colors[CI.grid_borderback];
                ctrl.ColumnHeadersDefaultCellStyle.ForeColor = colors[CI.grid_bordertext];
                ctrl.ColumnHeadersDefaultCellStyle.SelectionBackColor = colors[CI.grid_borderback];

                ctrl.BackgroundColor = GroupBoxOverride(parent, colors[CI.form]);
                ctrl.DefaultCellStyle.BackColor = colors[CI.grid_cellbackground];
                ctrl.AlternatingRowsDefaultCellStyle.BackColor = colors[CI.grid_altcellbackground];
                ctrl.DefaultCellStyle.ForeColor = colors[CI.grid_celltext];
                ctrl.AlternatingRowsDefaultCellStyle.ForeColor = colors[CI.grid_altcelltext];
                ctrl.DefaultCellStyle.SelectionBackColor = ctrl.DefaultCellStyle.ForeColor;
                ctrl.DefaultCellStyle.SelectionForeColor = ctrl.DefaultCellStyle.BackColor;

                ctrl.BorderStyle = BorderStyle.None;        // can't control the color of this, turn it off

                ctrl.GridColor = colors[CI.grid_borderlines];
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
                    Color c1 = colors[CI.grid_scrollbutton];
                    ctrl.BorderColor = colors[CI.grid_borderlines];
                    ctrl.BackColor = colors[CI.form];
                    ctrl.SliderColor = colors[CI.grid_sliderback];
                    ctrl.BorderColor = ctrl.ThumbBorderColor =
                            ctrl.ArrowBorderColor = colors[CI.grid_borderlines];
                    ctrl.ArrowButtonColor = ctrl.ThumbButtonColor = c1;
                    ctrl.MouseOverButtonColor = c1.Multiply(mouseoverscaling);
                    ctrl.MousePressedButtonColor = c1.Multiply(mouseselectedscaling);
                    ctrl.ForeColor = colors[CI.grid_scrollarrow];
                    ctrl.FlatStyle = FlatStyle.Popup;
                }
                else
                    ctrl.FlatStyle = FlatStyle.System;
            }
            else if (myControl is ExtNumericUpDown)
            {
                ExtNumericUpDown ctrl = (ExtNumericUpDown)myControl;

                ctrl.TextBoxForeColor = colors[CI.textbox_fore];
                ctrl.TextBoxBackColor = colors[CI.textbox_back];
                ctrl.BorderColor = colors[CI.textbox_border];

                Color c1 = colors[CI.textbox_scrollbutton];
                ctrl.updown.BackColor = c1;
                ctrl.updown.ForeColor = colors[CI.textbox_scrollarrow];
                ctrl.updown.BorderColor = colors[CI.button_border];
                ctrl.updown.MouseOverColor = c1.Multiply(mouseoverscaling);
                ctrl.updown.MouseSelectedColor = c1.Multiply(mouseselectedscaling);
                ctrl.Invalidate();

                dochildren = false;
            }
            else if (myControl is CheckedIconUserControl)
            {
                CheckedIconUserControl ctrl = (CheckedIconUserControl)myControl;
                ctrl.BackColor = colors[CI.form];
                ctrl.ForeColor = colors[CI.textbox_fore];
                ctrl.BorderColor = colors[CI.grid_borderlines];
                ctrl.BackColor = colors[CI.form];
                ctrl.SliderColor = colors[CI.grid_sliderback];
                ctrl.BorderColor = ctrl.ThumbBorderColor =
                        ctrl.ArrowBorderColor = colors[CI.grid_borderlines];
                Color c1 = colors[CI.grid_scrollbutton];
                ctrl.ArrowButtonColor = ctrl.ThumbButtonColor = c1;
                ctrl.MouseOverButtonColor = c1.Multiply(mouseoverscaling);
                ctrl.MousePressedButtonColor = c1.Multiply(mouseselectedscaling);
                ctrl.CheckBoxColor = colors[CI.checkbox];
                ctrl.CheckBoxInnerColor = colors[CI.checkbox].Multiply(1.5F);
                ctrl.MouseOverColor = colors[CI.checkbox].Multiply(1.4F);
                ctrl.TickBoxReductionRatio = 0.75f;
                ctrl.CheckColor = colors[CI.checkbox_tick];
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
                ctrl.BorderColor = colors[CI.grid_borderlines];
                ctrl.ForeColor = colors[CI.textbox_fore];
                ctrl.TextBackColor = colors[CI.textbox_back];
                ctrl.BackColor = colors[CI.form];
                ctrl.SelectedColor = colors[CI.textbox_fore].MultiplyBrightness(0.6F);

                if (buttonstyle.Equals(ButtonStyles[0])) // system
                    ctrl.checkbox.FlatStyle = FlatStyle.System;
                else
                    ctrl.checkbox.FlatStyle = FlatStyle.Popup;

                ctrl.checkbox.TickBoxReductionRatio = 0.75f;
                ctrl.checkbox.ForeColor = colors[CI.checkbox];
                ctrl.checkbox.CheckBoxColor = colors[CI.checkbox];
                Color inner = colors[CI.checkbox].Multiply(1.5F);
                if (inner.GetBrightness() < 0.1)        // double checking
                    inner = Color.Gray;
                ctrl.checkbox.CheckBoxInnerColor = inner;
                ctrl.checkbox.CheckColor = colors[CI.checkbox_tick];
                ctrl.checkbox.MouseOverColor = colors[CI.checkbox].Multiply(1.4F);

                ctrl.updown.BackColor = colors[CI.button_back];
                ctrl.updown.BorderColor = colors[CI.grid_borderlines];
                ctrl.updown.ForeColor = colors[CI.textbox_fore];
                ctrl.updown.MouseOverColor = colors[CI.checkbox].Multiply(1.4F);
                ctrl.updown.MouseSelectedColor = colors[CI.checkbox].Multiply(1.5F);

                dochildren = false;
            }
            else if (myControl is StatusStrip)
            {
                myControl.BackColor = colors[CI.form];
                myControl.ForeColor = colors[CI.label];
                dochildren = false;
            }
            else if (myControl is ToolStrip)    // MenuStrip is a tool stip
            {
                myControl.Font = fnt;       // Toolstrips don't seem to inherit Forms font, so force

                foreach (ToolStripItem i in ((ToolStrip)myControl).Items)   // make sure any buttons have the button back colour set
                {
                    if (i is ToolStripButton || i is ToolStripDropDownButton)
                    {           // theme the back colour, this is the way its done.. not via the tool strip renderer
                        i.BackColor = colors[CI.button_back];
                    }
                    else if (i is ToolStripTextBox)
                    {
                        i.ForeColor = colors[CI.textbox_fore];
                        i.BackColor = colors[CI.textbox_back];
                    }
                }
            }
            else if (myControl is TabStrip)
            {
                TabStrip ts = myControl as TabStrip;
                //System.Diagnostics.Debug.WriteLine("*************** TAB Strip themeing" + myControl.Name + " " + myControl.Tag);
                ts.ForeColor = colors[CI.button_text];
                ts.DropDownBackgroundColor = colors[CI.button_back];
                ts.DropDownBorderColor = colors[CI.textbox_border];
                ts.DropDownScrollBarButtonColor = colors[CI.textbox_scrollbutton];
                ts.DropDownScrollBarColor = colors[CI.textbox_sliderback];
                ts.DropDownMouseOverBackgroundColor = colors[CI.button_back].Multiply(mouseoverscaling);
                ts.DropDownItemSeperatorColor = colors[CI.button_border];
                ts.EmptyColor = colors[CI.button_back];
                ts.SelectedBackColor = colors[CI.button_back];
            }
            else if (myControl is Controls.ImageControl)      // no theme
            {
            }
            else if (myControl is TreeView)
            {
                TreeView ctrl = myControl as TreeView;
                ctrl.ForeColor = colors[CI.textbox_fore];
                ctrl.BackColor = colors[CI.textbox_back];
            }
            else if (myControl is CheckedIconListBoxForm)
            {
                CheckedIconListBoxForm ctrl = myControl as CheckedIconListBoxForm;

                if (buttonstyle.Equals(ButtonStyles[0])) // system
                    ctrl.FlatStyle = FlatStyle.System;
                else
                    ctrl.FlatStyle = FlatStyle.Popup;
            }
            else if (myControl is CompassControl)
            {
                CompassControl compassControl = myControl as CompassControl;
                compassControl.ForeColor = colors[CI.textbox_fore];
                compassControl.StencilColor = colors[CI.textbox_fore];
                compassControl.CentreTickColor = colors[CI.textbox_fore].Multiply(1.2F);
                compassControl.BugColor = colors[CI.textbox_fore].Multiply(0.8F);
                compassControl.BackColor = colors[CI.form];
            }

            else if (myControl is LabelData)
            {
                LabelData ld = myControl as LabelData;
                ld.BorderColor = colors[CI.textbox_border];
                ld.ForeColor = colors[CI.label];
            }
            else if (myControl is Button wfb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wfb.ForeColor = colors[CI.button_text];
                wfb.BackColor = colors[CI.button_back];
                wfb.FlatStyle = FlatStyle.Popup;
                wfb.FlatAppearance.BorderColor = colors[CI.button_border];
            }
            else if (myControl is RadioButton wrb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wrb.ForeColor = colors[CI.button_text];
            }
            else if (myControl is GroupBox wgb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wgb.ForeColor = colors[CI.group_text];
                wgb.BackColor = colors[CI.group_back];
            }
            else if (myControl is CheckBox wchb)
            {
                wchb.BackColor = GroupBoxOverride(parent, colors[CI.form]);

                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                if (wchb.Appearance == Appearance.Button)
                {
                    wchb.ForeColor = colors[CI.button_text];
                }
                else
                {
                    wchb.ForeColor = colors[CI.checkbox];
                }

            }
            else if (myControl is TextBox wtb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wtb.ForeColor = colors[CI.textbox_fore];
                wtb.BackColor = colors[CI.textbox_back];
                wtb.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (myControl is RichTextBox wrtb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wrtb.ForeColor = colors[CI.textbox_fore];
                wrtb.BackColor = colors[CI.textbox_back];
            }
            else if (myControl is ComboBox wcb)
            {
                System.Diagnostics.Trace.WriteLine("Themer " + myControl.Name + " of " + controltype.Name + " from " + parent.Name + " Winform control!");
                wcb.ForeColor = colors[CI.button_text];
                wcb.BackColor = colors[CI.button_back];
            }
            else if (myControl is ExtSplitterResizeParent)      // splitter, no theme
            {
            }
            // these are not themed or are sub parts of other controls
            else if (myControl is UserControl || myControl is SplitContainer || myControl is SplitterPanel || myControl is HScrollBar || myControl is VScrollBar )
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
            ctrl.ForeColor = colors[CI.button_text];
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
                        ctrl.BackColor = colors[CI.form];
                }
                else
                {
                    ctrl.BackColor = colors[CI.button_back];       // else its a graduated back
                }

                ctrl.FlatAppearance.BorderColor = (ctrl.Image != null) ? colors[CI.form] : colors[CI.button_border];
                ctrl.FlatAppearance.BorderSize = 1;
                ctrl.FlatAppearance.MouseOverBackColor = colors[CI.button_back].Multiply(mouseoverscaling);
                ctrl.FlatAppearance.MouseDownBackColor = colors[CI.button_back].Multiply(mouseselectedscaling);

                if (buttonstyle.Equals(ButtonStyles[1])) // flat
                    ctrl.FlatStyle = FlatStyle.Flat;
                else
                    ctrl.FlatStyle = FlatStyle.Popup;
            }

        }

        private void UpdateToolsStripRenderer(ThemeToolStripRenderer toolstripRenderer)
        {
            Color menuback = colors[CI.menu_back];
            bool toodark = (menuback.GetBrightness() < 0.1);

            toolstripRenderer.colortable.colMenuText = colors[CI.menu_fore];              // and the text
            toolstripRenderer.colortable.colMenuSelectedText = colors[CI.menu_dropdownfore];
            toolstripRenderer.colortable.colMenuBackground = menuback;
            toolstripRenderer.colortable.colMenuBarBackground = colors[CI.form];
            toolstripRenderer.colortable.colMenuBorder = colors[CI.button_border];
            toolstripRenderer.colortable.colMenuSelectedBack = colors[CI.menu_dropdownback];
            toolstripRenderer.colortable.colMenuHighlightBorder = colors[CI.button_border];
            toolstripRenderer.colortable.colMenuHighlightBack = toodark ? colors[CI.menu_dropdownback].Multiply(0.7F) : colors[CI.menu_back].Multiply(1.3F);        // whole menu back

            Color menuchecked = toodark ? colors[CI.menu_dropdownback].Multiply(0.8F) : colors[CI.menu_back].Multiply(1.5F);        // whole menu back

            toolstripRenderer.colortable.colCheckButtonChecked = menuchecked;
            toolstripRenderer.colortable.colCheckButtonPressed =
            toolstripRenderer.colortable.colCheckButtonHighlighted = menuchecked.Multiply(1.1F);

            toolstripRenderer.colortable.colToolStripButtonCheckedBack = menuchecked;
            toolstripRenderer.colortable.colToolStripButtonPressedBack =
            toolstripRenderer.colortable.colToolStripButtonSelectedBack = menuchecked.Multiply(1.1F);

            toolstripRenderer.colortable.colToolStripBackground = colors[CI.toolstrip_back];
            toolstripRenderer.colortable.colToolStripBorder = colors[CI.toolstrip_border];
            toolstripRenderer.colortable.colToolStripSeparator = colors[CI.toolstrip_border];
            toolstripRenderer.colortable.colOverflowButton = colors[CI.menu_back];
            toolstripRenderer.colortable.colGripper = colors[CI.toolstrip_border];

            toolstripRenderer.colortable.colToolStripDropDownMenuImageMargin = colors[CI.button_back];
            toolstripRenderer.colortable.colToolStripDropDownMenuImageRevealed = Color.Purple;      // NO evidence, set to show up

        }
        public Color GroupBoxOverride(Control parent, Color d)      // if its a group box behind the control, use the group box back color..
        {
            Control x = parent;
            while (x != null)
            {
                if (x is GroupBox)
                    return colors[CI.group_back];
                x = x.Parent;
            }

            return d;
        }

        public bool FromJSON( JObject jo, string name, Theme defaultset = null)
        {
            if (defaultset == null)
                defaultset = new Theme("Windows");          // if none given, use the basic windows set to get a default set

            this.Name = name;

            foreach (CI ck in Enum.GetValues(typeof(CI)))           // all enums
            {
                bool foundcolour = false;

                try
                {
                    string s = jo[ck.ToString()].StrNull();
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
                        if ( v != null)
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
                    if (!foundcolour)
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
                            Color def = defaultset.colors[ck];        // 
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
