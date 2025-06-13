/*
 * Copyright 2024-2025 EDDiscovery development team
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
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ConfigurableEntryList
    {
        #region From string
        static private string MakeEntry(string instr, out Entry entry, ref Point lastpos)
        {
            entry = null;

            BaseUtils.StringParser sp = new BaseUtils.StringParser(instr);

            string name = sp.NextQuotedWordComma();

            if (name == null)
                return "Missing name";

            string type = sp.NextWordCommaLCInvariant();

            Type ctype = null;

            // in same order as Control Definition in Action doc and in same order as Set String
           
            if (type == null)
                return "Missing type";
            else if (type.Equals("label"))
                ctype = typeof(System.Windows.Forms.Label);
            else if (type.Equals("button"))
                ctype = typeof(ExtButton);
            else if (type.Equals("textbox"))
                ctype = typeof(ExtTextBox);
            else if (type.Equals("richtextbox"))
                ctype = typeof(ExtRichTextBox);
            else if (type.Equals("checkbox"))
                ctype = typeof(ExtCheckBox);
            else if (type.Equals("radiobutton"))
                ctype = typeof(ExtRadioButton);
            else if (type.Equals("datetime"))
                ctype = typeof(ExtDateTimePicker);
            else if (type.Equals("numberboxlong"))
                ctype = typeof(NumberBoxLong);
            else if (type.Equals("numberboxdouble"))
                ctype = typeof(NumberBoxDouble);
            else if (type.Equals("numberboxint"))
                ctype = typeof(NumberBoxInt);
            else if (type.Equals("combobox"))
                ctype = typeof(ExtComboBox);
            else if (type.Equals("panel"))
                ctype = typeof(Panel);
            else if (type.Equals("panelvertscroll"))
                ctype = typeof(ExtPanelVertScrollWithBar);
            else if (type.Equals("flowpanel"))
                ctype = typeof(FlowLayoutPanel);
            else if (type.Equals("panelrollup"))
                ctype = typeof(ExtPanelRollUp);
            else if (type.Equals("dgv"))
                ctype = typeof(ExtPanelDataGridViewScroll);
            else if (type.Equals("dropdownbutton"))
                ctype = typeof(ExtButtonWithNewCheckedListBox);
            else if (type.Equals("splitter"))
                ctype = typeof(SplitContainer);
            else if (type.Equals("numericupdown"))
                ctype = typeof(ExtNumericUpDown);
            else
                return $"Unknown control type for {name}";

            string text = sp.NextQuotedWordComma();     // normally text..

            if (text == null)
                return $"Missing text for {name}";

            string inpanelname = null;
            if (sp.IsStringMoveOn("In:",StringComparison.InvariantCultureIgnoreCase))
            {
                inpanelname = sp.NextQuotedWordComma();
            }

            string dockstyle = sp.IsStringMoveOn("Dock:", StringComparison.InvariantCultureIgnoreCase) ? sp.NextWordComma(System.Globalization.CultureInfo.InvariantCulture) : null;

            string anchorstyle = sp.IsStringMoveOn("Anchor:", StringComparison.InvariantCultureIgnoreCase) ? sp.NextWordComma(System.Globalization.CultureInfo.InvariantCulture) : null;

            Padding? margin = new Padding(3,3,3,3);     // default for controls

            if (sp.IsStringMoveOn("Margin:", StringComparison.InvariantCultureIgnoreCase))
            {
                int? left = sp.NextIntComma(", ");
                int? top = sp.NextIntComma(", ");
                int? right = sp.NextIntComma(", ");
                int? bottom = sp.NextIntComma(", ");

                if (left.HasValue && top.HasValue && right.HasValue && bottom.HasValue)
                    margin = new Padding(left.Value, top.Value, right.Value, bottom.Value);
                else
                    return $"Margin is missing values for {name}";
            }

            int? x = sp.NextWordComma().InvariantParseIntNullOffset(lastpos.X);
            int? y = sp.NextWordComma().InvariantParseIntNullOffset(lastpos.Y);
            int? w = sp.NextWordComma().InvariantParseIntNull();
            int? h = sp.NextWordComma().InvariantParseIntNull();

            if (x == null || y == null || w == null || h == null)
                return $"Missing position/size for {name}";

            string tip = sp.NextQuotedWordComma();      // tip can be null

            entry = new Entry(name, ctype, text, new System.Drawing.Point(x.Value, y.Value), new System.Drawing.Size(w.Value, h.Value), tip);

            if (dockstyle != null && Enum.TryParse<DockStyle>(dockstyle, true, out DockStyle ds))
                entry.Dock = ds;

            if (anchorstyle.EqualsIIC("Bottom"))
                entry.Anchor = AnchorStyles.Bottom;
            else if (anchorstyle.EqualsIIC("Right"))
                entry.Anchor = AnchorStyles.Right;
            else if (anchorstyle.EqualsIIC("BottomRight"))
                entry.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            else if (anchorstyle != null)
                return $"Unknown Anchor Style for {name}";

            if (inpanelname != null)
            {
                if (inpanelname.EqualsIIC("Bottom"))
                    entry.PlacedInPanel = Entry.PanelType.Bottom;
                else if (inpanelname.EqualsIIC("Top"))
                    entry.PlacedInPanel = Entry.PanelType.Top;
                else
                    entry.InPanel = inpanelname;
            }

            entry.Margin = margin.Value;

            bool moreparaspos = tip != null;

            // these all can have optional parameters, but don't need them

            if (ctype == typeof(ExtTextBox))
            {
                if (moreparaspos)
                {
                    int? v = sp.NextWordComma().InvariantParseIntNull();
                    entry.TextBoxMultiline = v.HasValue && v.Value != 0;
                    if (entry.TextBoxMultiline == true)
                    {
                        entry.TextBoxEscapeOnReport = true;
                        entry.TextValue = entry.TextValue.ReplaceEscapeControlChars();        // New! if multiline, replace escape control chars
                    }

                    v = sp.NextWordComma().InvariantParseIntNull();
                    entry.TextBoxClearOnFirstChar = v.HasValue && v.Value != 0;
                }
            }
            else if (ctype == typeof(ExtCheckBox) || ctype == typeof(ExtRadioButton))
            {
                if (moreparaspos)
                {
                    int? v = sp.NextWordComma().InvariantParseIntNull();
                    entry.CheckBoxChecked = v.HasValue && v.Value != 0;
                }
            }
            else if (ctype == typeof(ExtDateTimePicker))
            {
                if (moreparaspos)
                    entry.CustomDateFormat = sp.NextWord();
            }
            else if (ctype == typeof(NumberBoxDouble))
            {
                if (moreparaspos)
                {
                    double? min = sp.NextWordComma().InvariantParseDoubleNull();
                    double? max = sp.NextWordComma().InvariantParseDoubleNull();
                    entry.NumberBoxDoubleMinimum = min.HasValue ? min.Value : double.MinValue;
                    entry.NumberBoxDoubleMaximum = max.HasValue ? max.Value : double.MaxValue;
                    entry.NumberBoxFormat = sp.NextWordComma();
                }
            }
            else if (ctype == typeof(NumberBoxLong) || ctype == typeof(NumberBoxInt))
            {
                if (moreparaspos)
                {
                    long? min = sp.NextWordComma().InvariantParseLongNull();
                    long? max = sp.NextWordComma().InvariantParseLongNull();
                    entry.NumberBoxLongMinimum = min.HasValue ? min.Value : long.MinValue;
                    entry.NumberBoxLongMaximum = max.HasValue ? max.Value : long.MaxValue;
                    entry.NumberBoxFormat = sp.NextWordComma();
                }
            }
            else if (ctype == typeof(ExtPanelVertScrollWithBar))
            {
                if (moreparaspos)
                {
                    string colourname = sp.NextQuotedWordComma();
                    if (colourname != null)
                    {
                        entry.BackColor = colourname.ColorFromNameOrValues();
                    }
                }
            }
            else if (ctype == typeof(Panel))
            {
                if (moreparaspos)
                {
                    string colourname = sp.NextQuotedWordComma();
                    if (colourname != null)
                    {
                        entry.BackColor = colourname.ColorFromNameOrValues();
                    }
                }
            }
            else if (ctype == typeof(FlowLayoutPanel))
            {
                if (moreparaspos)
                {
                    string horzsetting = sp.NextQuotedWord(" ,");
                    entry.Horizontal = horzsetting.EqualsIIC("Horizontal");
                    if (entry.Horizontal == true || horzsetting.EqualsIIC("Vertical"))
                    {
                        string colourname = sp.IsCharMoveOn(',') ? sp.NextQuotedWord() : null;
                        if (colourname != null)
                        {
                            entry.BackColor = colourname.ColorFromNameOrValues();
                        }
                    }
                    else
                        return $"Missing direction in flowlayoutpanel for {name}";
                }
                else
                    entry.Horizontal = true; // we are going across with no paras
            }
            else if (ctype == typeof(ExtPanelRollUp))
            {
                if (moreparaspos)
                {
                    bool? pinned = sp.NextBool(" ,");
                    if (pinned.HasValue)
                    {
                        entry.Pinned = pinned.Value;

                        string colourname = sp.IsCharMoveOn(',') ? sp.NextQuotedWord() : null;
                        if (colourname != null)
                        {
                            entry.BackColor = colourname.ColorFromNameOrValues();
                        }
                    }
                }
            }
            else if (ctype == typeof(ExtRichTextBox) || ctype == typeof(ExtButton) || ctype == typeof(Label))  // all of these don't have additional paras
            {
            }

            // if missing the tip (therefore no more paras) or have tip but no more text, we have an error, as the below all need paras

            else if (!moreparaspos || sp.IsEOL)
            {
                return $"Missing Parameters for {name}";
            }

            else if (ctype == typeof(ExtComboBox))
            {
                var list = sp.NextQuotedWordList(otherterminators: "", replaceescape: true);     // quotes allowed, spaces between commas allowed
                if (list != null && list.Count > 0)
                {
                    entry.ComboBoxItems = list.ToArray();
                }
                else
                    return $"Missing parameters in combobox for {name}";
            }
            else if (ctype == typeof(ExtPanelDataGridViewScroll))
            {
                entry.DGVColumns = new List<DataGridViewColumn>();
                entry.DGVSortMode = new List<string>();

                // see action doc for format

                int? rowpixelwidth = sp.NextWordComma(separ: ';').InvariantParseIntNull();

                if (rowpixelwidth != null)
                {
                    entry.DGVRowHeaderWidth = rowpixelwidth.Value;

                    while (sp.IsCharMoveOn('('))
                    {
                        string coltype = sp.NextQuotedWordComma();
                        string coldescr = sp.NextQuotedWordComma();
                        int? colfillsize = sp.NextInt(" ),");

                        if (!coltype.HasChars() || !coldescr.HasChars() || colfillsize == null)
                        {
                            return $"Missing DGV column description items for {name}";
                        }

                        string sortmode = "Alpha";
                        if (sp.IsCharMoveOn(','))           // if comma after the col fill size, there is an optional sort mode
                            sortmode = sp.NextQuotedWord(" ,)");

                        if (coltype.EqualsIIC("text"))
                        {
                            entry.DGVColumns.Add(new DataGridViewTextBoxColumn() { HeaderText = coldescr, FillWeight = colfillsize.Value, ReadOnly = true });
                            entry.DGVSortMode.Add(sortmode);
                        }
                        else
                            return $"Unknown column type for {name}";

                        if (!sp.IsCharMoveOn(')'))
                            return $"Missing ) at end of DGV cell definition for {name}";

                        if (sp.IsEOL)      // EOL ends definition
                            break;

                        if (!sp.IsCharMoveOn(','))     // then must be comma
                        {
                            return $"Incorrect DGV cell format missing comma for {name}";
                        }
                    }
                }
                else
                    return $"Missing DGV row header width for {name}";
            }
            else if (ctype == typeof(ExtButtonWithNewCheckedListBox))
            {
                // see action doc for format

                string defsetting = sp.NextQuotedWordComma();
                bool? allofnoneshown = sp.NextBoolComma(" ,");
                bool? allofnoneback = sp.NextBoolComma(" ,");
                bool? multicolumns = sp.NextBool(" ,;");

                bool? sortitems = false;
                if (sp.IsCharMoveOn(','))       // optional sortitems
                {
                    sortitems = sp.NextBool(" ,;");
                    if (sp.IsCharMoveOn(','))   // optional sizex,sizey
                    {
                        int? imagexsize = sp.NextIntComma(" ,");
                        int? imageysize = sp.NextInt(" ,;");
                        if (imagexsize.HasValue && imageysize.HasValue)
                        {
                            entry.ImageSize = new Size(imagexsize.Value, imageysize.Value);
                        }
                        else
                            return $"Missing DropDownButton X/Y image sizes for {name}";
                    }
                }

                if (defsetting != null && allofnoneshown.HasValue && allofnoneback.HasValue && multicolumns.HasValue && sortitems.HasValue && sp.IsCharMoveOn(';'))
                {
                    entry.ButtonSettings = defsetting;
                    entry.MultiColumns = multicolumns.Value;
                    entry.AllOrNoneShown = allofnoneshown.Value;
                    entry.AllOrNoneBack = allofnoneback.Value;
                    entry.SortItems = sortitems.Value;

                    entry.DropDownButtonList = new List<CheckedIconUserControl.Item>();

                    while (sp.IsCharMoveOn('('))
                    {
                        var cr = CheckedIconUserControl.Item.Create(sp);
                        if (cr == null || !sp.IsCharMoveOn(')'))
                            return $"Missing/too many parameters in creating button drop down list for {name}";
                        entry.DropDownButtonList.Add(cr);

                        if (!sp.IsCharMoveOn(','))
                            break;
                    }

                    if (!sp.IsEOL)
                        return $"Incorrect format in creating button drop down list for {name}";
                }
            }
            else if (ctype == typeof(SplitContainer))
            {
                string horzsetting = sp.NextQuotedWordComma();
                entry.Horizontal = horzsetting.EqualsIIC("Horizontal");
                if (entry.Horizontal == true || horzsetting.EqualsIIC("Vertical"))
                {
                    double? percentage = sp.NextDouble(" ,");
                    if (percentage.HasValue)
                    {
                        entry.DoubleValue = percentage.Value;
                        entry.Panel1MinPixelSize = sp.IsCharMoveOn(',') ? sp.NextInt(" ,") ?? 0 : 0;
                        entry.Panel2MinPixelSize = sp.IsCharMoveOn(',') ? sp.NextInt(" ") ?? 0 : 0;
                    }
                    else
                        return $"Splitter percentage missing or in error for {name}";
                }
                else
                    return $"Incorrect splitter type given for {name}";
            }
            else if (ctype == typeof(ExtNumericUpDown))
            {
                int? min = sp.NextIntComma(" ,");
                int? max = sp.NextInt(" ");
                if ( min != null && max != null)
                {
                    entry.NumberBoxLongMinimum = min;
                    entry.NumberBoxLongMaximum = max;
                }
                else
                    return $"Missign min and or max for numeric up down";
            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "Missing handling type");
            }

            if (!sp.IsEOL)
                return $"Extra parameters at end of line for {name}";

            lastpos = new System.Drawing.Point(x.Value, y.Value);
            return null;
        }


        #endregion
    }
}
