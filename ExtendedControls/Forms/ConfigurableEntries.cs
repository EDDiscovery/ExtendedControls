/*
 * Copyright © 2024-2024 EDDiscovery development team
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
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ConfigurableEntryList
    {
        #region Properties

        // You give an array of Entries describing the controls
        // either added programatically by Add(entry) or via a string descriptor Add(string) (See action document for string descriptor format)
        // Directly Supported Types (string name/base type)
        //      "button" ExtButton, "textbox" ExtTextBox, "checkbox" ExtCheckBox 
        //      "label" Label, "datetime" ExtDateTimePicker,
        //      "numberboxdouble" NumberBoxDouble, "numberboxlong" NumberBoxLong, "numberboxint" NumberBoxInt, 
        //      "combobox" ExtComboBox
        // Or any type if you use Add(control, name..)

        // Lay the thing out like its in the normal dialog editor, with 8.25f font.  Leave space for the title bar/close icon.

        // Triggers
        // returns dialog logical name, name of control (plus options), caller tag object
        // name of control on click for button / Checkbox / ComboBox
        // name:Return for number box, textBox.  Set Entries.SwallowReturn to true before returning to swallow the return key
        // name:Validity:true/false for Number boxes
        // name:RowSelection:<list> for DGV, gives a list of semi colon separated row values, in selection order (last one first)
        // name:CellSelection:<list> for DGV, gives a list of semi colon separated cell locations, each in Row,Col format, in selection order (last one first)

        public List<Entry> Entries { get; private set; } = new List<Entry>();       // entry list
        public bool DisableTriggers { get; set; } = false;        // set to stop triggering of events
        public bool SwallowReturn { get; set; } = false;    // set in your trigger handler to swallow the return. Otherwise, return is return and is processed by windows that way
        public string Name { get; set; } = "Default";       // name to return in triggers
        public Object CallerTag { get; set; } = null;       // object to return in triggers
        public Action<string, string, Object> Trigger { get; set; } = null;      // return trigger

        public int YOffset { get; set; } = 0;               // Fixed y adjust for position when adding entries to account for any title area in the contentpanel (+ve down)

        public Action<bool, Control, MouseEventArgs> MouseUpDownOnLabelOrPanel { get; set; } = null; // click on label items handler

        #endregion

        #region Entries 

        [System.Diagnostics.DebuggerDisplay("{Name} {Location} {Size }")]
        public class Entry
        {
            public string Name { get; set; }                            // logical name of control
            public Type ControlType { get; set; }                       // if non null, activate this type.  Else if null, Control should be filled up with your specific type
            public Control Control { get; set; }                        // if controltype is set, don't set.  If controltype=null, pass your control type.
            public Point Location { get; set; }
            public Size Size { get; set; }
            public AnchorStyles Anchor { get; set; } = AnchorStyles.None;       // anchor to window
            public Size MinimumSize { get; set; }                       // for when Anchoring, set minimum size for use if anchoring both top/bottom or left/right.
                                                                        // If not set, its set to loaded display min size
            public float PostThemeFontScale { get; set; } = 1.0f;       // post theme font scaler, if not 1.0f, it scales the font of the control by this amount

            public DockStyle Dock { get; set; } = DockStyle.None;       // dock to window
            public string ToolTip { get; set; }                         // can be null.
            public bool Enabled { get; set; } = true;                   // is control enabled?
            public string InPanel { get; set; }                         // if placed in user defined panel, name of panel to place control in 
            public enum PanelType { Top, Scroll, Bottom }
            public PanelType PlacedInPanel { get; set; } = PanelType.Scroll;  // if InPanel = null, which panel to add to, top, scroll, bottom

            public string TextValue { get; set; }                       // textboxes : text
                                                                        // comboxbox def value
                                                                        // label text
                                                                        // checkbox text
                                                                        // button: if Resource:<fullpath> load embedded resource as image. If Image:<file> load image as file. Else text
                                                                        // For number boxes, the invariant value, or Null and it will use the DoubleValue or LongValue
                                                                        // for Dates, the invariant culture assumed local
                                                                        // for ExtButtonWithNewCheckedListBox, initial setting, semi colon list
            public double DoubleValue { get; set; }                     // if its a number box double, set this, Text=null
            public long LongValue { get; set; }                         // if its a number box long or int, set this, Text=null
            public DateTime DateTimeValue { get; set; }                 // if its a date time, set this, Text=null

            public ContentAlignment? TextAlign { get; set; }            // label,button. Null its not applied
            public ContentAlignment ContentAlign { get; set; } = ContentAlignment.MiddleLeft;  // align for checkbox

            public bool CheckBoxChecked { get; set; }                   // checkbox

            public bool TextBoxMultiline { get; set; }                  // textbox
            public bool TextBoxEscapeOnReport { get; set; }             // escape characters back on reporting a text box Get()
            public bool TextBoxClearOnFirstChar { get; set; }           // fill in for textbox

            public string[] ComboBoxItems { get; set; }                 // combo box

            public string CustomDateFormat { get; set; }                // fill in for datetimepicker

            public string NumberBoxFormat { get; set; } = null;         // format for number boxes
            public double NumberBoxDoubleMinimum { get; set; } = double.MinValue;   // for double box
            public double NumberBoxDoubleMaximum { get; set; } = double.MaxValue;
            public long NumberBoxLongMinimum { get; set; } = long.MinValue;   // for long and int box
            public long NumberBoxLongMaximum { get; set; } = long.MaxValue; 

            public Color? BackColor { get; set; } = null;               // panel, back colour to reapply after themeing

            public List<DataGridViewColumn> DGVColoumns { get; set; } = null;       // for DGV
            public int DGVRowHeaderWidth { get; set; } = 0;             // for DGV

            public List<CheckedIconUserControl.Item> DropDownButtonList { get; set; } = null;  // for DropDownButton
            public string ButtonSettings { get; set; } = "";             // for DropDownButton
            public bool MultiColumns { get; set; } = false;             // for DropDownButton
            public bool AllOrNoneShown { get; set; } = false;           // for DropDownButton
            public bool AllOrNoneBack { get; set; } = false;           // for DropDownButton
            public bool SortItems { get; set; } = false;                // for DropDownButton

            public Size? ImageSize { get; set; } = null;                // forced image size for dropdownbutton


            // general
            public Entry(string nam, Type c, string t, Point p, Size s, string tt)
            {
                ControlType = c; TextValue = t; Location = p; Size = s; ToolTip = tt; Name = nam; CustomDateFormat = "long";
            }
            public Entry(string nam, Type c, string t, Point p, Size s, string tt, float fontscale, ContentAlignment align = ContentAlignment.MiddleCenter)
            {
                ControlType = c; TextValue = t; Location = p; Size = s; ToolTip = tt; Name = nam; CustomDateFormat = "long"; PostThemeFontScale = fontscale; TextAlign = align;
            }
            // number boxes
            public Entry(string nam, int t, Point p, Size s, string tt)
            {
                ControlType = typeof(NumberBoxInt); LongValue = t; Location = p; Size = s; ToolTip = tt; Name = nam;
            }
            public Entry(string nam, long t, Point p, Size s, string tt)
            {
                ControlType = typeof(NumberBoxLong); LongValue = t; Location = p; Size = s; ToolTip = tt; Name = nam;
            }
            public Entry(string nam, double t, Point p, Size s, string tt)
            {
                ControlType = typeof(NumberBoxDouble); DoubleValue = t; Location = p; Size = s; ToolTip = tt; Name = nam;
            }
            // checkbox
            public Entry(string nam, bool t, string text, Point p, Size s, string tt)
            {
                ControlType = typeof(ExtCheckBox); TextValue = text; CheckBoxChecked = t; Location = p; Size = s; ToolTip = tt; Name = nam;
            }
            // Date time
            public Entry(string nam, DateTime t, Point p, Size s, string tt)
            {
                ControlType = typeof(ExtDateTimePicker); DateTimeValue = t; Location = p; Size = s; ToolTip = tt; Name = nam; CustomDateFormat = "long";
            }

            // ComboBoxCustom
            public Entry(string nam, string t, Point p, Size s, string tt, IEnumerable<string> comboitems)
            {
                ControlType = typeof(ExtComboBox); TextValue = t; Location = p; Size = s; ToolTip = tt; Name = nam;
                ComboBoxItems = comboitems.ToArray();
            }
            // custom
            public Entry(Control c, string nam, string t, System.Drawing.Point p, System.Drawing.Size s, string tt)
            {
                Name = nam; Control = c; TextValue = t; Location = p; Size = s; ToolTip = tt; TextAlign = ContentAlignment.TopLeft;
            }
        }

        #endregion

        #region Find/Enumerate/Add

        public Entry Find(Predicate<Entry> pred)
        {
            return Entries.Find(pred);
        }

        public List<Entry>.Enumerator GetEnumerator()
        {
            var x = Entries.GetEnumerator();
            return Entries.GetEnumerator();
        }

        private System.Drawing.Point lastpos = new Point(0,0); // used for dynamically making the list up

        public string Add(string instr)       // add a string definition dynamically add to list.  errmsg if something is wrong
        {
            Entry e;
            string errmsg = MakeEntry(instr, out e, ref lastpos);
            if (errmsg == null)
                Entries.Add(e);
            return errmsg;
        }
        public void Add(Entry e)               // add an entry..
        {
            Entries.Add(e);
        }
        public void Add(ref int vpos, int vspacing, Entry e)               // add an entry with vpos increase
        {
            e.Location = new Point(e.Location.X, e.Location.Y + vpos);
            Entries.Add(e);
            vpos += vspacing;
        }

        public void AddOK(Point p, string tooltip = null, Size? sz = null, AnchorStyles anchor = AnchorStyles.None, Entry.PanelType paneltype = Entry.PanelType.Scroll)
        {
            if (sz == null)
                sz = new Size(80, 24);
            Add(new Entry("OK", typeof(ExtButton), "OK".TxID(ECIDs.OK), p, sz.Value, tooltip) { Anchor = anchor, PlacedInPanel = paneltype });
        }

        public void AddCancel(Point p, string tooltip = null, Size? sz = null, AnchorStyles anchor = AnchorStyles.None, Entry.PanelType paneltype = Entry.PanelType.Scroll)
        {
            if (sz == null)
                sz = new Size(80, 24);
            Add(new Entry("Cancel", typeof(ExtButton), "Cancel".TxID(ECIDs.Cancel), p, sz.Value, tooltip) { Anchor = anchor, PlacedInPanel = paneltype });
        }

        public void AddLabelAndEntry(string labeltext, Point labelpos, Size labelsize, Entry e)
        {
            Add(new Entry("L" + e.Name, typeof(Label), labeltext, labelpos, labelsize, null) { PlacedInPanel = e.PlacedInPanel });
            Add(e);
        }

        // vpos sets the vertical position. Entry.pos sets the X and offset Y from vpos
        public void AddLabelAndEntry(string labeltext, Point labelxvoff, ref int vpos, int vspacing, Size labelsize, Entry e)
        {
            Add(new Entry("L" + e.Name, typeof(Label), labeltext, new Point(labelxvoff.X, vpos + labelxvoff.Y), labelsize, null) { PlacedInPanel = e.PlacedInPanel });
            e.Location = new Point(e.Location.X, e.Location.Y + vpos);
            Add(e);
            vpos += vspacing;
        }

        // add bool array of names and tags to scroll panel
        // optionally prefix the tags, and offset into the bools array
        public int AddBools(string[] tags, string[] names, bool[] bools, int vposstart, int vspacing, int depth, int xstart, int xspacing, string tagprefix = "", int boolsoffset = 0)
        {
            int vpos = vposstart;
            int max = 0;
            for (int i = 0; i < tags.Length; i++)
            {
                //  System.Diagnostics.Debug.WriteLine($"Add {i} {boolsoffset+i} {tags[i]} {names[i]} at {new Point(xstart, vpos)}");
                Add(ref vpos, vspacing, new Entry(tagprefix + tags[i], bools[boolsoffset + i], names[i], new Point(xstart, 0), new Size(xspacing, vspacing - 2), "Search for " + names[i]));
                max = Math.Max(vpos, max);
                if (vpos - vposstart > depth)
                {
                    vpos = vposstart;
                    xstart += xspacing;
                }
            }

            return max;
        }

        // add array of names and tags to scroll panel, using a hashset to work out what is checked
        // optionally prefix the tags, and offset into the bools array
        public int AddBools(string[] tags, string[] names, HashSet<string> ischecked, int vposstart, int vspacing, int depth, int xstart, int xspacing, string tagprefix = "")
        {
            int vpos = vposstart;
            int max = 0;
            for (int i = 0; i < tags.Length; i++)
            {
                //  System.Diagnostics.Debug.WriteLine($"Add {i} {boolsoffset+i} {tags[i]} {names[i]} at {new Point(xstart, vpos)}");
                Add(ref vpos, vspacing, new Entry(tagprefix + tags[i], ischecked.Contains(tags[i]), names[i], new Point(xstart, 0), new Size(xspacing, vspacing - 2), "Search for " + names[i]));
                max = Math.Max(vpos, max);
                if (vpos - vposstart > depth)
                {
                    vpos = vposstart;
                    xstart += xspacing;
                }
            }

            return max;
        }

        // remove a control from the list, both visually and from entries
        public bool Remove(string controlname)
        {
            Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t?.Control != null)
            {
                t.Control.Parent.Controls.Remove(t.Control);
                Entries.Remove(t);
                return true;
            }
            else
                return false;
        }


        #endregion

        #region Gets

        // get control of name as type
        public T GetControl<T>(string controlname) where T : Control      // return value of dialog control
        {
            Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
                return (T)t.Control;
            else
                return null;
        }

        // get control by name
        public Control GetControl(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
                return t.Control;
            else
                return null;
        }

        // return value of dialog control as a string. Null if can't express it as a string (not a supported type)
        public string Get(ConfigurableEntryList.Entry t)
        {
            Control c = t.Control;
            if (c is ExtTextBox)
            {
                string s = (c as ExtTextBox).Text;
                if (t.TextBoxEscapeOnReport)
                    s = s.EscapeControlChars();
                return s;
            }
            else if (c is Label)
                return (c as Label).Text;
            else if (c is ExtCheckBox)
                return (c as ExtCheckBox).Checked ? "1" : "0";
            else if (c is ExtDateTimePicker)
                return (c as ExtDateTimePicker).Value.ToString("yyyy/dd/MM HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            else if (c is NumberBoxDouble)
            {
                var cn = c as NumberBoxDouble;
                return cn.IsValid ? cn.Value.ToStringInvariant() : "INVALID";
            }
            else if (c is NumberBoxLong)
            {
                var cn = c as NumberBoxLong;
                return cn.IsValid ? cn.Value.ToStringInvariant() : "INVALID";
            }
            else if (c is NumberBoxInt)
            {
                var cn = c as NumberBoxInt;
                return cn.IsValid ? cn.Value.ToStringInvariant() : "INVALID";
            }
            else if (c is ExtComboBox)
            {
                ExtComboBox cb = c as ExtComboBox;
                return (cb.SelectedIndex != -1) ? cb.Text : "";
            }
            else
                return null;
        }

        // return value of dialog control as a native value of the control (string/timedate etc). 
        // null if invalid, null if not a supported control
        public object GetValue(ConfigurableEntryList.Entry t)
        {
            Control c = t.Control;
            if (c is ExtTextBox)
            {
                string s = (c as ExtTextBox).Text;
                if (t.TextBoxEscapeOnReport)
                    s = s.EscapeControlChars();
                return s;
            }
            else if (c is Label)
                return (c as Label).Text;
            else if (c is ExtCheckBox)
                return (c as ExtCheckBox).Checked;
            else if (c is ExtDateTimePicker)
                return (c as ExtDateTimePicker).Value;
            else if (c is NumberBoxDouble)
            {
                var cn = c as NumberBoxDouble;
                return cn.IsValid ? cn.Value : default(double?);
            }
            else if (c is NumberBoxLong)
            {
                var cn = c as NumberBoxLong;
                return cn.IsValid ? cn.Value : default(long?);
            }
            else if (c is NumberBoxInt)
            {
                var cn = c as NumberBoxInt;
                return cn.IsValid ? cn.Value : default(int?);
            }
            else if (c is ExtComboBox)
            {
                ExtComboBox cb = c as ExtComboBox;
                return cb.SelectedIndex;
            }
            else
                return null;
        }

        // Return Get() by controlname, null if can't get
        public string Get(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            return t != null ? Get(t) : null;
        }

        // Return GetValue() by controlname, null if can't get
        public T GetValue<T>(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            return t != null ? (T)GetValue(t) : default(T);
        }

        // Return Get() from controls starting with this name
        public string[] GetByStartingName(string startingcontrolname)
        {
            var list = Entries.Where(x => x.Name.StartsWith(startingcontrolname)).Select(x => x).ToArray();
            string[] res = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
                res[i] = Get(list[i]);
            return res;
        }

        // Return GetValue() from controls starting with this name
        public T[] GetByStartingName<T>(string startingcontrolname)
        {
            var list = Entries.Where(x => x.Name.StartsWith(startingcontrolname)).Select(x => x).ToArray();
            T[] res = new T[list.Length];
            for (int i = 0; i < list.Length; i++)
                res[i] = (T)GetValue(list[i]);
            return res;
        }

        // from checkbox
        public bool? GetBool(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtCheckBox);
            if (t != null)
            {
                var cn = t.Control as ExtCheckBox;
                return cn.Checked;
            }
            return null;
        }
        // from numberbox, Null if not valid
        public double? GetDouble(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is NumberBoxDouble);
            if (t != null)
            {
                var cn = t.Control as NumberBoxDouble;
                if (cn.IsValid)
                    return cn.Value;
            }
            return null;
        }

        // from numberbox, Null if not valid
        public long? GetLong(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is NumberBoxLong);
            if (t != null)
            {
                var cn = t.Control as NumberBoxLong;
                if (cn.IsValid)
                    return cn.Value;
            }
            return null;
        }
        // from numberbox, Null if not valid
        public int? GetInt(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is NumberBoxInt);
            if (t != null)
            {
                var cn = t.Control as NumberBoxInt;
                if (cn.IsValid)
                    return cn.Value;
            }
            return null;
        }
        // from DateTimePicker, Null if not valid
        public DateTime? GetDateTime(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtDateTimePicker);
            if (t != null)
            {
                ExtDateTimePicker c = t.Control as ExtDateTimePicker;
                if (c != null)
                    return c.Value;
            }

            return null;
        }

        // from ExtCheckBox controls starting with this name, get the names of the ones checked, removing the prefix unless told not too
        public string[] GetCheckedListNames(string startingcontrolname, bool removeprefix = true)
        {
            var elist = Entries.Where(x => x.Control is ExtCheckBox && x.Name.StartsWith(startingcontrolname)).Where(x => ((ExtCheckBox)x.Control).Checked).Select(x => removeprefix ? x.Name.Substring(startingcontrolname.Length) : x.Name).ToArray();
            return elist;
        }

        // from ExtCheckBox controls starting with this name, get the entries of ones checked
        public ConfigurableEntryList.Entry[] GetCheckedListEntries(string startingcontrolname)
        {
            var elist = Entries.Where(x => x.Control is ExtCheckBox && x.Name.StartsWith(startingcontrolname)).Where(x => ((ExtCheckBox)x.Control).Checked);
            return elist.ToArray();
        }

        // from ExtCheckBox controls starting with this name, get a bool array describing the check state
        public bool[] GetCheckBoxBools(string startingcontrolname)
        {
            var elist = Entries.Where(x => x.Control is ExtCheckBox && x.Name.StartsWith(startingcontrolname)).Select(x => x).ToArray();
            var result = new bool[elist.Length];
            int i = 0;
            foreach (var e in elist)
            {
                var ctr = e.Control as ExtCheckBox;
                result[i++] = ctr.Checked;
            }
            return result;
        }

        // are all entries on this table which could be invalid valid?
        public bool IsAllValid()
        {
            foreach (var t in Entries)
            {
                var c = t.Control;
                var cl = c as NumberBoxLong;
                var cd = c as NumberBoxDouble;
                var ci = c as NumberBoxInt;
                if ((cl != null && cl.IsValid == false) || (cd != null && cd.IsValid == false) || (ci != null && ci.IsValid == false))
                    return false;
            }
            return true;
        }

        // For DGV, add or reset the row cell values. 
        // in JSON or in text 
        // see action doc for format

        public string AddSetRows(string controlname, string rowstring)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtPanelDataGridViewScroll);
            if (t != null)
            {
                var cn = t.Control as ExtPanelDataGridViewScroll;
                var dgv = cn.DGV;

                if (rowstring.StartsWith("["))     // JSON
                {
                    JArray ja = JArray.Parse(rowstring, JToken.ParseOptions.CheckEOL);
                    if (ja != null)
                    {
                        foreach (JObject jo in ja)
                        {
                            int? rownumber = jo["Row"].IntNull();

                            DataGridViewRow rw = null;

                            bool insert = rownumber < 0;            // -1 insert at start, -2 add at end, else rownumber
                            if (insert)
                                rw = dgv.RowTemplate.Clone() as DataGridViewRow;
                            else if (rownumber < dgv.Rows.Count)    // else check in range
                                rw = dgv.Rows[rownumber.Value];
                            else
                            {
                                return "Incorrect row number in JSON";
                            }

                            JArray jcells = jo["Cells"].Array();

                            int cellno = 0;
                            foreach (JObject cell in jcells.DefaultIfEmpty())
                            {
                                string type = cell["Type"].Str().ToLowerInvariant();
                                string tooltip = cell["ToolTip"].Str();

                                int? cellsetnogiven = cell["Cell"].IntNull();      // we can override the cell number (only when replacing)
                                if (cellsetnogiven != null)                        // set the cell set number to the count if not set
                                    cellno = cellsetnogiven.Value;

                                if (type == "text")
                                {
                                    string value = cell["Value"].Str();

                                    if (insert)
                                        rw.Cells.Add(new DataGridViewTextBoxCell() { Value = value });
                                    else
                                        rw.Cells[cellno].Value = value;
                                }

                                if (tooltip != null)
                                    rw.Cells[cellno].ToolTipText = tooltip;

                                cellno++;
                            }

                            if (insert)
                            {
                                if (rownumber == -1 && dgv.Rows.Count > 0)
                                    dgv.Rows.Insert(0, rw);
                                else
                                    dgv.Rows.Add(rw);
                            }
                            cellno++;
                        }

                        return null;
                    }
                    else
                        return "Bad JSON";

                }
                else
                {
                    BaseUtils.StringParser sp = new BaseUtils.StringParser(rowstring);

                    while (!sp.IsEOL)
                    {
                        int? rownumber = sp.NextIntComma(" ,");
                        if (rownumber.HasValue)
                        {
                            int cellno = 0;

                            if ( sp.PeekChar() != '(')
                            {
                                int? cno = sp.NextIntComma(" ,");
                                if (cno != null)
                                    cellno = cno.Value;
                                else
                                    return "Missing cell number after row";
                            }

                            DataGridViewRow rw = null;

                            bool insert = rownumber < 0;            // -1 insert at start, -2 add at end, else rownumber
                            if (insert)
                                rw = dgv.RowTemplate.Clone() as DataGridViewRow;
                            else if (rownumber < dgv.Rows.Count)    // else check in range
                                rw = dgv.Rows[rownumber.Value];
                            else
                            {
                                return "Incorrect row format in addsetrows";
                            }

                            while (sp.IsCharMoveOn('('))
                            {
                                string coltype = sp.NextQuotedWordComma(System.Globalization.CultureInfo.InvariantCulture);      // text, etc, see below

                                if (coltype == "text")
                                {
                                    string value = sp.NextQuotedWord(" ),");

                                    if (insert)
                                        rw.Cells.Add(new DataGridViewTextBoxCell() { Value = value });
                                    else
                                    {
                                        if (cellno < dgv.Columns.Count)
                                            rw.Cells[cellno].Value = value;
                                        else
                                            return "Cell number beyond column count";
                                    }
                                }
                                else
                                {
                                    return "Unsuported Column Type " + coltype;
                                }

                                if (sp.IsCharMoveOn(','))       // more stuff, tooltip
                                {
                                    string tooltip = sp.NextQuotedWord(" ),");

                                    if (tooltip != null)
                                        rw.Cells[cellno].ToolTipText = tooltip;
                                }

                                if (!sp.IsCharMoveOn(')'))
                                    return "Missing ) at end of cell definition";

                                if (sp.IsEOL || sp.IsCharMoveOn(';'))      // EOL or semicolon ends definition of this rows cells
                                    break;
                                
                                if ( !sp.IsCharMoveOn(','))     // then must be comma
                                {
                                    return "Incorrect cell format missing comma";
                                }

                                cellno++;
                            }

                            if (insert)
                            {
                                if (rownumber == -1 && dgv.Rows.Count > 0)
                                    dgv.Rows.Insert(0, rw);
                                else
                                    dgv.Rows.Add(rw);
                            }
                        }
                        else
                        {
                            return "Incorrect row format in addsetrows";
                       }
                    }

                    return null;
                }
            }
            else
                return "Not a DGV";
        }

        public bool Clear(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtPanelDataGridViewScroll);
            if (t != null)
            {
                var cn = t.Control as ExtPanelDataGridViewScroll;
                var dgv = cn.DGV;
                dgv.Rows.Clear();
                return true;
            }

            return false;
        }

        // remove DGV,start row, count
        public bool RemoveRows(string controlname, int rowstart, int count)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtPanelDataGridViewScroll);
            if (t != null)
            {
                var cn = t.Control as ExtPanelDataGridViewScroll;
                var dgv = cn.DGV;

                if (rowstart >= 0 && rowstart < dgv.Rows.Count)
                {
                    while (count > 0 && dgv.Rows.Count>rowstart)
                    {
                        dgv.Rows.RemoveAt(rowstart);
                        count--;
                    }
                }

                return true;
            }

            return false;
        }

        public void CloseDropDown()
        {
            foreach( var x in Entries.Where(x=>x.Control is ExtButtonWithNewCheckedListBox))
            {
                var c = x.Control as ExtButtonWithNewCheckedListBox;
                if ( c.IsDropDownActive)
                {
                    c.CloseDropDown();
                }
            }

        }

        #endregion

        #region Sets
        // Set value of control by string value
        public bool Set(string controlname, string value)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
            {
                Control c = t.Control;
                if (c is ExtTextBox)
                {
                    (c as ExtTextBox).Text = value;
                    return true;
                }
                else if (c is Label)
                {
                    (c as Label).Text = value;
                    return true;
                }
                else if (c is ExtCheckBox)
                {
                    (c as ExtCheckBox).Checked = !value.Equals("0");
                    return true;
                }
                else if (c is ExtComboBox)
                {
                    ExtComboBox cb = c as ExtComboBox;
                    if (cb.Items.Contains(value))
                    {
                        cb.Enabled = false;
                        cb.SelectedItem = value;
                        cb.Enabled = true;
                        return true;
                    }
                }
                else if (c is NumberBoxDouble)
                {
                    var cn = c as NumberBoxDouble;
                    double? v = value.InvariantParseDoubleNull();
                    if (v.HasValue)
                    {
                        cn.Value = v.Value;
                        return true;
                    }
                }
                else if (c is NumberBoxLong)
                {
                    var cn = c as NumberBoxLong;
                    long? v = value.InvariantParseLongNull();
                    if (v.HasValue)
                    {
                        cn.Value = v.Value;
                        return true;
                    }
                }
            }

            return false;
        }

        // from controls starting with this name, set the names of the ones checked
        public void SetCheckedList(IEnumerable<string> controlnames, bool state)
        {
            var cnames = controlnames.ToArray();
            var elist = Entries.Where(x => x.Control is ExtCheckBox && Array.IndexOf(cnames, x.Name) >= 0).Select(x => x);
            foreach (var e in elist)
            {
                (e.Control as ExtCheckBox).Checked = state;
            }
        }

        // radio button this set, to 1 entry, or to N max
        public void RadioButton(string startingcontrolname, string controlhit, int max = 1)
        {
            var elist = Entries.Where(x => x.Control is ExtCheckBox && x.Name.StartsWith(startingcontrolname)).Select(x => x).ToArray();

            if (max == 1)
            {
                foreach (var x in elist)
                {
                    ((ExtCheckBox)x.Control).Checked = x.Name == controlhit;
                }
            }
            else
            {
                var numchecked = elist.Where(x => ((ExtCheckBox)x.Control).Checked).Count();
                if (numchecked > max)
                {
                    var chkbox = GetControl<ExtCheckBox>(controlhit);
                    chkbox.Checked = false;
                }
            }
        }

        #endregion

        #region Create any new entries in the list

        // create entry in contentpanel/toppanel (may be null)/bottompanel (may be null)
        // tooltips go to tooltipcontrol
        // autosize factor if set causes a scaling of the location/size
        // YScollPos = scroll vertical setting, which is autosized

        public void CreateEntries(ExtPanelVertScroll contentpanel, Panel toppanel, Panel bottompanel, ToolTip tooltipcontrol, SizeF? autosizefactor = null, int Yscrollpos = 0)
        {
            foreach (var ent in Entries)
            {
                if (ent.Control != null && (contentpanel.Controls.Contains(ent.Control) ||              // if already installed, do nothing
                                (toppanel != null && toppanel.Controls.Contains(ent.Control)) ||
                                (bottompanel != null && bottompanel.Controls.Contains(ent.Control))))
                {
                    continue;
                }

                Control c = ent.ControlType != null ? (Control)Activator.CreateInstance(ent.ControlType) : ent.Control;

                //System.Diagnostics.Debug.WriteLine($"Add Control {ent.Name} of {c.GetType()} at {ent.Location} {ent.Size} {ent.TextValue}");

                ent.Control = c;
                c.Size = ent.Size;

                // if we have autosizeing on, the scroll position which is added to take account of current scrolling needs to be scaled
                // the scroll position is in terms of the expanded autosize factor (ie. increased) so we need to move it back to non expanded
                // then the AutoScale below will apply anything back

                if (autosizefactor != null)         
                    Yscrollpos = (int)(Yscrollpos / autosizefactor.Value.Height);

                c.Location = new Point(ent.Location.X, ent.Location.Y + YOffset + Yscrollpos);
                c.Dock = ent.Dock;  // dock style

                c.Name = ent.Name;
                c.Enabled = ent.Enabled;
                c.Tag = ent;     // point control tag at ent structure

                if ( ent.InPanel!=null)     // if place in panel
                {
                    foreach( var pent in Entries)
                    {
                        if ( pent.Name == ent.InPanel)      // find
                        {
                            if (ent.Control != null)        // must have been made so must be made first
                                pent.Control.Controls.Add(c);
                            else
                                System.Diagnostics.Trace.WriteLine($"ConfigurableEntries cannot place {ent.Name} in non constructed panel {pent.Name}");
                            break;
                        }
                    }
                }
                else
                {
                    Panel toaddto = (ent.PlacedInPanel == ConfigurableEntryList.Entry.PanelType.Top && toppanel != null) ? toppanel :
                                    (ent.PlacedInPanel == ConfigurableEntryList.Entry.PanelType.Bottom && bottompanel != null) ? bottompanel :
                                    contentpanel;

                    toaddto.Controls.Add(c);
                }

                if (ent.ToolTip.HasChars() && tooltipcontrol != null)
                    tooltipcontrol.SetToolTip(c, ent.ToolTip);

                //        //System.Diagnostics.Debug.WriteLine($".. Control {ent.Name} of {c.GetType()} at {c.Location} {c.Size}");

                if (c is Label)
                {
                    Label l = c as Label;
                    l.Text = ent.TextValue;
                    if (ent.TextAlign.HasValue)
                        l.TextAlign = ent.TextAlign.Value;
                    l.MouseDown += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(true, (Control)md1, md2); };
                    l.MouseUp += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(false, (Control)md1, md2); };
                }
                else if (c is ExtButton)
                {
                    ExtButton b = c as ExtButton;

                    if (ent.TextValue.StartsWith("Resource:"))
                    {
                        Image img = BaseUtils.ResourceHelpers.GetResourceAsImage(ent.TextValue.Substring(9));
                        if (img != null)
                            b.Image = img;
                        else
                            b.Text = "Failed load";
                    }
                    else if (ent.TextValue.StartsWith("File:"))
                    {
                        try
                        {
                            b.Image = Image.FromFile(ent.TextValue.Substring(5));
                        }
                        catch
                        {
                            b.Text = "Failed load";
                        }
                    }
                    else
                        b.Text = ent.TextValue;

                    //b.ImageAlign = ContentAlignment.MiddleCenter;     // don't think applicable
                    //b.BackgroundImageLayout = ImageLayout.Stretch;

                    if (ent.TextAlign.HasValue)
                        b.TextAlign = ent.TextAlign.Value;

                    if (c is ExtButtonWithNewCheckedListBox)    // this is an extended button
                    {
                        ExtButtonWithNewCheckedListBox cb = c as ExtButtonWithNewCheckedListBox;
                        cb.Init(ent.DropDownButtonList, ent.ButtonSettings, (s, b1) => { SendTrigger("DropDownButtonClosed:" + s); },
                                ent.AllOrNoneShown, ent.AllOrNoneBack,
                                null, //disabled
                                ent.ImageSize,
                                new Size(16, 16), // screenmargin
                                new Size(64, 64), // close boundary
                                ent.MultiColumns,
                                null, // group, not needed
                                ent.SortItems,
                                (s, eb1) => { SendTrigger("DropDownButtonPressed:" + s); });
                    }
                    else
                    {
                        b.Click += (sender, ev) =>
                        {
                            if (!DisableTriggers)
                            {
                                ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(((Control)sender).Tag);
                                Trigger?.Invoke(Name, en.Name, this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                            }
                        };
                    }
                }
                else if (c is NumberBoxDouble)
                {
                    NumberBoxDouble cb = c as NumberBoxDouble;
                    cb.Minimum = ent.NumberBoxDoubleMinimum;
                    cb.Maximum = ent.NumberBoxDoubleMaximum;
                    double? v = ent.TextValue == null ? ent.DoubleValue : ent.TextValue.InvariantParseDoubleNull();
                    cb.Value = v.HasValue ? v.Value : cb.Minimum;
                    if (ent.NumberBoxFormat != null)
                        cb.Format = ent.NumberBoxFormat;
                    cb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        if (!DisableTriggers)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(Name, en.Name + ":Return", this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }

                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        if (!DisableTriggers)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(Name, en.Name + ":Validity:" + s.ToString(), this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                    };
                }
                else if (c is NumberBoxLong)
                {
                    NumberBoxLong cb = c as NumberBoxLong;
                    cb.Minimum = ent.NumberBoxLongMinimum;
                    cb.Maximum = ent.NumberBoxLongMaximum;
                    long? v = ent.TextValue == null ? ent.LongValue : ent.TextValue.InvariantParseLongNull();
                    cb.Value = v.HasValue ? v.Value : cb.Minimum;
                    if (ent.NumberBoxFormat != null)
                        cb.Format = ent.NumberBoxFormat;
                    cb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        if (!DisableTriggers)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(Name, en.Name + ":Return", this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        if (!DisableTriggers)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(Name, en.Name + ":Validity:" + s.ToString(), this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                                }
                    };
                }
                else if (c is NumberBoxInt)
                {
                    NumberBoxInt cb = c as NumberBoxInt;
                    cb.Minimum = ent.NumberBoxLongMinimum == long.MinValue ? int.MinValue : (int)ent.NumberBoxLongMinimum;
                    cb.Maximum = ent.NumberBoxLongMaximum == long.MaxValue ? int.MaxValue : (int)ent.NumberBoxLongMaximum;
                    int? v = ent.TextValue == null ? (int)ent.LongValue : ent.TextValue.InvariantParseIntNull();
                    cb.Value = v.HasValue ? v.Value : cb.Minimum;
                    if (ent.NumberBoxFormat != null)
                        cb.Format = ent.NumberBoxFormat;
                    cb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        if (!DisableTriggers)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(Name, en.Name + ":Return", this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        if (!DisableTriggers)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(Name, en.Name + ":Validity:" + s.ToString(), this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                    };
                }
                else if (c is ExtTextBox)
                {
                    ExtTextBox tb = c as ExtTextBox;
                    tb.Text = ent.TextValue;
                    tb.Multiline = tb.WordWrap = ent.TextBoxMultiline;

                    // this was here, but no idea why. removing as the multiline instances seem good
                    //tb.Size = ent.Size;     

                    tb.ClearOnFirstChar = ent.TextBoxClearOnFirstChar;
                    tb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        if (!DisableTriggers)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(Name, en.Name + ":Return", this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                        return SwallowReturn;
                    };

                    if (tb.ClearOnFirstChar)
                        tb.SelectEnd();
                }
                else if (c is ExtCheckBox)
                {
                    ExtCheckBox cb = c as ExtCheckBox;
                    cb.Text = ent.TextValue;
                    cb.Checked = ent.CheckBoxChecked;
                    cb.CheckAlign = ent.ContentAlign;
                    cb.Click += (sender, ev) =>
                    {
                        if (!DisableTriggers)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(((Control)sender).Tag);
                            Trigger?.Invoke(Name, en.Name, this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                    };
                }
                else if (c is ExtDateTimePicker)
                {
                    ExtDateTimePicker dt = c as ExtDateTimePicker;
                    DateTime t;

                    if (ent.TextValue == null)
                        dt.Value = ent.DateTimeValue;
                    else if (DateTime.TryParse(ent.TextValue, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out t))     // assume local, so no conversion
                        dt.Value = t;

                    switch (ent.CustomDateFormat.ToLowerInvariant())
                    {
                        case "short":
                            dt.Format = DateTimePickerFormat.Short;
                            break;
                        case "long":
                            dt.Format = DateTimePickerFormat.Long;
                            break;
                        case "time":
                            dt.Format = DateTimePickerFormat.Time;
                            break;
                        default:
                            dt.CustomFormat = ent.CustomDateFormat;
                            break;
                    }
                }
                else if (c is ExtComboBox)
                {
                    ExtComboBox cb = c as ExtComboBox;

                    cb.Items.AddRange(ent.ComboBoxItems);
                    if (cb.Items.Contains(ent.TextValue))
                        cb.SelectedItem = ent.TextValue;
                    cb.SelectedIndexChanged += (sender, ev) =>
                    {
                        Control ctr = (Control)sender;
                        if (ctr.Enabled && !DisableTriggers)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(ctr.Tag);
                            Trigger?.Invoke(Name, en.Name, this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                                }
                    };
                }
                else if ( c is ExtPanelDataGridViewScroll)
                {
                    ExtPanelDataGridViewScroll dgvs = c as ExtPanelDataGridViewScroll;
                    DataGridView dgv = new DataGridView();
                    dgv.RowHeadersVisible = ent.DGVRowHeaderWidth > 4;
                    if ( dgv.RowHeadersVisible)
                        dgv.RowHeadersWidth = ent.DGVRowHeaderWidth;

                    dgv.AllowUserToAddRows = false;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    foreach (var col in ent.DGVColoumns)
                        dgv.Columns.Add(col);

                    dgvs.Controls.Add(dgv);
                    ExtScrollBar sb = new ExtScrollBar();
                    dgvs.Controls.Add(sb);

                    dgv.Tag = ent;
                    dgv.SelectionChanged += Dgv_SelectionChanged;
                    dgv.Sorted += (s1,e1) => { SendTrigger("SortColumn:" + dgv.SortedColumn.Index ); };
                }
                else if (c is Panel)
                {
                    c.MouseDown += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(true, (Control)md1, md2); };
                    c.MouseUp += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(false, (Control)md1, md2); };
                }
                else
                {
                    if ( ent.TextValue!=null)
                        c.Text = ent.TextValue;     // rest get text value
                }

                if (autosizefactor != null)     // when adding, form scaling has already been done, so we need to scale manually
                {
                    //System.Diagnostics.Debug.WriteLine($"Auto size {autosizefactor} Current {c.Bounds}");
                    c.Scale(autosizefactor.Value);
                    //System.Diagnostics.Debug.WriteLine($"......... Now {c.Bounds}");
                }
            }

        }

        private void Dgv_Sorted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Entry ent = dgv.Tag as Entry;
            var rows = dgv.SelectedRows;
            if ( rows.Count>0)
            {
                System.Diagnostics.Debug.WriteLine($"Entry {ent.Name} changed selection Rows {rows.Count}");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataGridViewRow r in rows)
                    sb.AppendPrePad(r.Index.ToStringInvariant(), ";");
                SendTrigger("RowSelection:" + sb.ToString());
            }
            else
            {
                var cells = dgv.SelectedCells;
                System.Diagnostics.Debug.WriteLine($"Entry {ent.Name} changed selection Cells {cells.Count}");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataGridViewCell c in cells)
                    sb.AppendPrePad(c.RowIndex.ToStringInvariant() + "," + c.ColumnIndex.ToStringInvariant(), ";");
                SendTrigger("CellSelection:" + sb.ToString());
            }
        }

        #endregion

        #region Triggering

        // send trigger, if not disabled
        public void SendTrigger(string action)       
        {
            if ( !DisableTriggers)
                Trigger?.Invoke(Name, action, CallerTag);
        }

        #endregion

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

            if (type == null)
                return "Missing type";
            else if (type.Equals("label"))
                ctype = typeof(System.Windows.Forms.Label);
            else if (type.Equals("button"))
                ctype = typeof(ExtButton);
            else if (type.Equals("textbox"))
                ctype = typeof(ExtTextBox);
            else if (type.Equals("checkbox"))
                ctype = typeof(ExtCheckBox);
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
            else if (type.Equals("dgv"))
                ctype = typeof(ExtPanelDataGridViewScroll);
            else if (type.Equals("dropdownbutton"))
                ctype = typeof(ExtButtonWithNewCheckedListBox);
            else
                return "Unknown control type " + type;

            string text = sp.NextQuotedWordComma();     // normally text..

            if (text == null)
                return "Missing text";

            string inpanelname = null;
            if (sp.IsStringMoveOn("In:",StringComparison.InvariantCultureIgnoreCase))
            {
                inpanelname = sp.NextQuotedWordComma();
            }
            string dockstyle = sp.IsStringMoveOn("Dock:", StringComparison.InvariantCultureIgnoreCase) ? sp.NextWordComma(System.Globalization.CultureInfo.InvariantCulture) : null;

            int? x = sp.NextWordComma().InvariantParseIntNullOffset(lastpos.X);
            int? y = sp.NextWordComma().InvariantParseIntNullOffset(lastpos.Y);
            int? w = sp.NextWordComma().InvariantParseIntNull();
            int? h = sp.NextWordComma().InvariantParseIntNull();

            if (x == null || y == null || w == null || h == null)
                return "Missing position/size";

            string tip = sp.NextQuotedWordComma();      // tip can be null

            entry = new Entry(name, ctype, text, new System.Drawing.Point(x.Value, y.Value), new System.Drawing.Size(w.Value, h.Value), tip);

            if (dockstyle != null && Enum.TryParse<DockStyle>(dockstyle, true, out DockStyle ds))
                entry.Dock = ds;

            if (inpanelname != null)
            {
                if (inpanelname.EqualsIIC("Bottom"))
                    entry.PlacedInPanel = Entry.PanelType.Bottom;
                else if (inpanelname.EqualsIIC("Top"))
                    entry.PlacedInPanel = Entry.PanelType.Top;
                else
                    entry.InPanel = inpanelname;
            }

            if (tip != null)        // must have a tip for these extra values
            {
                if (ctype == typeof(ExtTextBox))
                {
                    int? v = sp.NextWordComma().InvariantParseIntNull();
                    entry.TextBoxMultiline = v.HasValue && v.Value != 0;
                    if (entry.TextBoxMultiline)
                    {
                        entry.TextBoxEscapeOnReport = true;
                        entry.TextValue = entry.TextValue.ReplaceEscapeControlChars();        // New! if multiline, replace escape control chars
                    }

                    v = sp.NextWordComma().InvariantParseIntNull();
                    entry.TextBoxClearOnFirstChar = v.HasValue && v.Value != 0;
                }
                else if (ctype == typeof(ExtCheckBox))
                {
                    int? v = sp.NextWordComma().InvariantParseIntNull();
                    entry.CheckBoxChecked = v.HasValue && v.Value != 0;
                }
                else if (ctype == typeof(ExtComboBox))
                {
                    entry.ComboBoxItems = sp.LineLeft.Trim().Split(",");
                    if (tip == null || entry.ComboBoxItems.Length == 0)
                        return "Missing parameters for combobox";
                }
                else if (ctype == typeof(ExtDateTimePicker))
                {
                    entry.CustomDateFormat = sp.NextWord();
                }
                else if (ctype == typeof(NumberBoxDouble))
                {
                    double? min = sp.NextWordComma().InvariantParseDoubleNull();
                    double? max = sp.NextWordComma().InvariantParseDoubleNull();
                    entry.NumberBoxDoubleMinimum = min.HasValue ? min.Value : double.MinValue;
                    entry.NumberBoxDoubleMaximum = max.HasValue ? max.Value : double.MaxValue;
                    entry.NumberBoxFormat = sp.NextWordComma();
                }
                else if (ctype == typeof(NumberBoxLong) || ctype == typeof(NumberBoxInt))
                {
                    long? min = sp.NextWordComma().InvariantParseLongNull();
                    long? max = sp.NextWordComma().InvariantParseLongNull();
                    entry.NumberBoxLongMinimum = min.HasValue ? min.Value : long.MinValue;
                    entry.NumberBoxLongMaximum = max.HasValue ? max.Value : long.MaxValue;
                    entry.NumberBoxFormat = sp.NextWordComma();
                }
                else if (ctype == typeof(Panel))
                {
                    string colourname = sp.NextQuotedWordComma();
                    if (colourname != null)
                    {
                        entry.BackColor = colourname.ColorFromNameOrValues();
                    }

                }
                else if (ctype == typeof(ExtPanelDataGridViewScroll))
                {
                    entry.DGVColoumns = new List<DataGridViewColumn>();

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
                                return "Missing DGV column description items";
                            }

                            if (coltype.EqualsIIC("text"))
                            {
                                entry.DGVColoumns.Add(new DataGridViewTextBoxColumn() { HeaderText = coldescr, FillWeight = colfillsize.Value, ReadOnly = true });
                            }
                            else
                                return "Unknown column type";

                            if (!sp.IsCharMoveOn(')'))
                                return "Missing ) at end of DGV cell definition";

                            if (sp.IsEOL)      // EOL ends definition
                                break;

                            if (!sp.IsCharMoveOn(','))     // then must be comma
                            {
                                return "Incorrect DGV cell format missing comma";
                            }
                        }
                    }
                    else
                        return "Missing DGV row header width";
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
                                return "Missing DropDownButton X/Y image sizes";
                        }
                    }

                    if (defsetting!= null && allofnoneshown.HasValue && allofnoneback.HasValue && multicolumns.HasValue && sortitems.HasValue && sp.IsCharMoveOn(';'))
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
                                return "Missing/too many parameters in creating button drop down list";
                            entry.DropDownButtonList.Add(cr);

                            if (!sp.IsCharMoveOn(','))
                                break;
                        }

                        if (!sp.IsEOL)
                            return "Incorrect format in creating button drop down list";
                    }
                }
            }

            lastpos = new System.Drawing.Point(x.Value, y.Value);
            return null;
        }


        #endregion
    }
}
