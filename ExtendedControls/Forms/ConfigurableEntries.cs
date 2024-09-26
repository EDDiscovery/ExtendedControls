/*
 * Copyright © 2017-2024 EDDiscovery development team
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
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ConfigurableEntryList
    {
        #region Properties
        
        public List<Entry> Entries { get; private set; } = new List<Entry>();

        #region Entries 

        [System.Diagnostics.DebuggerDisplay("{Name} {Location} {Size }")]
        public class Entry
        {
            public string Name {get; set;}                          // logical name of control
            public Type ControlType {get; set;}                     // if non null, activate this type.  Else if null, Control should be filled up with your specific type
            public Control Control { get; set; }                    // if controltype is set, don't set.  If controltype=null, pass your control type.

            public string TextValue {get; set;}                     // textboxes, comboxbox def value, label, button, checkbox
                                                                    // For number boxes, the invariant value, or Null use Double or Long
                                                                    // for Dates, the invariant culture assumed local
            public double DoubleValue { get; set; }                 // if its a number box double, set this, Text=null
            public long LongValue { get; set; }                     // if its a number box long or int, set this, Text=null
            public DateTime DateTimeValue { get; set; }             // if its a date time, set this, Text=null

            public Point Location {get; set;}
            public Size Size {get; set;}
            public string ToolTip { get; set; }                     // can be null.
            public bool Enabled { get; set; } = true;       // is control enabled?
            public enum PanelType { Top, Scroll, Bottom }
            public PanelType Panel { get; set; } = PanelType.Scroll;        // which panel to add to
            public AnchorStyles Anchor { get; set; } = AnchorStyles.None;       // anchor to window
            public Size MinimumSize { get; set; }                              // for when Anchoring, set minimum size for use if anchoring both top/bottom or left/right. If not set, its set to loaded display min size
            public ContentAlignment? TextAlign { get; set; }                    // label,button. nominal not applied
            public ContentAlignment ContentAlign { get; set; } = ContentAlignment.MiddleLeft;  // align for checkbox

            public bool CheckBoxChecked { get; set; }        // fill in for checkbox

            public bool TextBoxMultiline { get; set; }       // fill in for textbox
            public bool TextBoxEscapeOnReport { get; set; }  // escape characters back on reporting a text box Get()
            public bool TextBoxClearOnFirstChar { get; set; }       // fill in for textbox

            public string[] ComboBoxItems { get; set; }

            public string CustomDateFormat { get; set; }     // fill in for datetimepicker

            public double NumberBoxDoubleMinimum { get; set; } = double.MinValue;   // for double box
            public double NumberBoxDoubleMaximum { get; set; } = double.MaxValue;
            public long NumberBoxLongMinimum { get; set; } = long.MinValue;   // for long and int box
            public long NumberBoxLongMaximum { get; set; } = long.MaxValue; // for long and int box
            public string NumberBoxFormat { get; set; } = null;      // for both number boxes

            public float PostThemeFontScale { get; set; } = 1.0f;   // post theme font scaler

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

        private System.Drawing.Point lastpos; // used for dynamically making the list up

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
            Add(new Entry("OK", typeof(ExtButton), "OK".TxID(ECIDs.OK), p, sz.Value, tooltip) { Anchor = anchor, Panel = paneltype });
        }

        public void AddCancel(Point p, string tooltip = null, Size? sz = null, AnchorStyles anchor = AnchorStyles.None, Entry.PanelType paneltype = Entry.PanelType.Scroll)
        {
            if (sz == null)
                sz = new Size(80, 24);
            Add(new Entry("Cancel", typeof(ExtButton), "Cancel".TxID(ECIDs.Cancel), p, sz.Value, tooltip) { Anchor = anchor, Panel = paneltype });
        }

        public void AddLabelAndEntry(string labeltext, Point labelpos, Size labelsize, Entry e)
        {
            Add(new Entry("L" + e.Name, typeof(Label), labeltext, labelpos, labelsize, null) { Panel = e.Panel });
            Add(e);
        }

        // vpos sets the vertical position. Entry.pos sets the X and offset Y from vpos
        public void AddLabelAndEntry(string labeltext, Point labelxvoff, ref int vpos, int vspacing, Size labelsize, Entry e)
        {
            Add(new Entry("L" + e.Name, typeof(Label), labeltext, new Point(labelxvoff.X, vpos + labelxvoff.Y), labelsize, null) { Panel = e.Panel });
            e.Location = new Point(e.Location.X, e.Location.Y + vpos);
            Add(e);
            vpos += vspacing;
        }

        // add bool array of names and tags to scroll panel
        // optionally prefix the tags, and offset into the bools array
        public int AddBools(string[] tags, string[] names, bool[] bools, int vposstart, int vspacing, int depth, int xstart, int xspacing, string tagprefix = "", int boolsoffset = 0, Entry.PanelType paneltype = Entry.PanelType.Scroll)
        {
            int vpos = vposstart;
            int max = 0;
            for (int i = 0; i < tags.Length; i++)
            {
                //  System.Diagnostics.Debug.WriteLine($"Add {i} {boolsoffset+i} {tags[i]} {names[i]} at {new Point(xstart, vpos)}");
                Add(ref vpos, vspacing, new Entry(tagprefix + tags[i], bools[boolsoffset + i], names[i], new Point(xstart, 0), new Size(xspacing, vspacing - 2), "Search for " + names[i]) { Panel = paneltype });
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
        public int AddBools(string[] tags, string[] names, HashSet<string> ischecked, int vposstart, int vspacing, int depth, int xstart, int xspacing, string tagprefix = "", Entry.PanelType paneltype = Entry.PanelType.Scroll)
        {
            int vpos = vposstart;
            int max = 0;
            for (int i = 0; i < tags.Length; i++)
            {
                //  System.Diagnostics.Debug.WriteLine($"Add {i} {boolsoffset+i} {tags[i]} {names[i]} at {new Point(xstart, vpos)}");
                Add(ref vpos, vspacing, new Entry(tagprefix + tags[i], ischecked.Contains(tags[i]), names[i], new Point(xstart, 0), new Size(xspacing, vspacing - 2), "Search for " + names[i]) { Panel = paneltype });
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
        public void RemoveEntry(string controlname)
        {
            Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t?.Control != null)
            {
                t.Control.Parent.Controls.Remove(t.Control);
                Entries.Remove(t);
            }
        }




        #endregion


        #region From string

        static private string MakeEntry(string instr, out Entry entry, ref System.Drawing.Point lastpos)
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
            else if (type.Equals("button"))
                ctype = typeof(ExtButton);
            else if (type.Equals("textbox"))
                ctype = typeof(ExtTextBox);
            else if (type.Equals("checkbox"))
                ctype = typeof(ExtCheckBox);
            else if (type.Equals("label"))
                ctype = typeof(System.Windows.Forms.Label);
            else if (type.Equals("combobox"))
                ctype = typeof(ExtComboBox);
            else if (type.Equals("datetime"))
                ctype = typeof(ExtDateTimePicker);
            else if (type.Equals("numberboxlong"))
                ctype = typeof(NumberBoxLong);
            else if (type.Equals("numberboxdouble"))
                ctype = typeof(NumberBoxDouble);
            else if (type.Equals("numberboxint"))
                ctype = typeof(NumberBoxInt);
            else
                return "Unknown control type " + type;

            string text = sp.NextQuotedWordComma();     // normally text..

            if (text == null)
                return "Missing text";

            int? x = sp.NextWordComma().InvariantParseIntNullOffset(lastpos.X);
            int? y = sp.NextWordComma().InvariantParseIntNullOffset(lastpos.Y);
            int? w = sp.NextWordComma().InvariantParseIntNull();
            int? h = sp.NextWordComma().InvariantParseIntNull();

            if (x == null || y == null || w == null || h == null)
                return "Missing position/size";

            string tip = sp.NextQuotedWordComma();      // tip can be null

            entry = new Entry(name, ctype, text, new System.Drawing.Point(x.Value, y.Value), new System.Drawing.Size(w.Value, h.Value), tip);

            if (tip != null)        // must have a tip for these..
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
            }

            if (ctype == typeof(ExtComboBox))
            {
                entry.ComboBoxItems = sp.LineLeft.Trim().Split(",");
                if (tip == null || entry.ComboBoxItems.Length == 0)
                    return "Missing parameters for combobox";
            }

            if (ctype == typeof(ExtDateTimePicker))
            {
                entry.CustomDateFormat = sp.NextWord();
            }

            if (ctype == typeof(NumberBoxDouble))
            {
                double? min = sp.NextWordComma().InvariantParseDoubleNull();
                double? max = sp.NextWordComma().InvariantParseDoubleNull();
                entry.NumberBoxDoubleMinimum = min.HasValue ? min.Value : double.MinValue;
                entry.NumberBoxDoubleMaximum = max.HasValue ? max.Value : double.MaxValue;
                entry.NumberBoxFormat = sp.NextWordComma();
            }

            if (ctype == typeof(NumberBoxLong))
            {
                long? min = sp.NextWordComma().InvariantParseLongNull();
                long? max = sp.NextWordComma().InvariantParseLongNull();
                entry.NumberBoxLongMinimum = min.HasValue ? min.Value : long.MinValue;
                entry.NumberBoxLongMaximum = max.HasValue ? max.Value : long.MaxValue;
                entry.NumberBoxFormat = sp.NextWordComma();
            }

            lastpos = new System.Drawing.Point(x.Value, y.Value);
            return null;
        }


        #endregion
    }
}
