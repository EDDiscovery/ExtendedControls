﻿/*
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

        public List<Entry> Entries { get; private set; } = new List<Entry>();       // entry list
        public bool DisableTriggers { get; set; } = false;        // set to stop triggering of events
        public bool SwallowReturn { get; set; } = false;    // set in your trigger handler to swallow the return. Otherwise, return is return and is processed by windows that way
        public string Name { get; set; } = "Default";       // name to return in triggers
        public Object CallerTag { get; set; } = null;       // object to return in triggers
        public Action<string, string, Object> Trigger { get; set; } = null;      // return trigger

        public int YOffset { get; set; } = 0;               // y adjust for position when adding entries to account for any title area in the contentpanel (+ve down)

        public Action<bool, Control, MouseEventArgs> MouseUpDownOnLabel { get; set; } = null; // click on label items handler

        #endregion

        #region Entries 

        [System.Diagnostics.DebuggerDisplay("{Name} {Location} {Size }")]
        public class Entry
        {
            public string Name { get; set; }                          // logical name of control
            public Type ControlType { get; set; }                     // if non null, activate this type.  Else if null, Control should be filled up with your specific type
            public Control Control { get; set; }                    // if controltype is set, don't set.  If controltype=null, pass your control type.

            public string TextValue { get; set; }                     // textboxes, comboxbox def value, label, button, checkbox
                                                                      // For number boxes, the invariant value, or Null use Double or Long
                                                                      // for Dates, the invariant culture assumed local
            public double DoubleValue { get; set; }                 // if its a number box double, set this, Text=null
            public long LongValue { get; set; }                     // if its a number box long or int, set this, Text=null
            public DateTime DateTimeValue { get; set; }             // if its a date time, set this, Text=null

            public Point Location { get; set; }
            public Size Size { get; set; }
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
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
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
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
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
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
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
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
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
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
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

        public void CreateEntries(ExtPanelVertScroll contentpanel, Panel toppanel, Panel bottompanel, ToolTip tooltipcontrol, SizeF? factor = null)
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
                c.Location = new Point(ent.Location.X, ent.Location.Y + YOffset);
                c.Name = ent.Name;
                c.Enabled = ent.Enabled;
                if (!(ent.TextValue == null || c is ExtComboBox || c is ExtDateTimePicker
                        || c is NumberBoxDouble || c is NumberBoxLong || c is NumberBoxInt))        // everything but get text
                    c.Text = ent.TextValue;
                c.Tag = ent;     // point control tag at ent structure

                if (ent.Panel == ConfigurableEntryList.Entry.PanelType.Top && toppanel != null)
                    toppanel.Controls.Add(c);
                else if (ent.Panel == ConfigurableEntryList.Entry.PanelType.Bottom && bottompanel != null)
                    bottompanel.Controls.Add(c);
                else
                    contentpanel.Controls.Add(c);

                if (ent.ToolTip != null && tooltipcontrol != null)
                    tooltipcontrol.SetToolTip(c, ent.ToolTip);

                //        //System.Diagnostics.Debug.WriteLine($".. Control {ent.Name} of {c.GetType()} at {c.Location} {c.Size}");

                if (c is Label)
                {
                    Label l = c as Label;
                    if (ent.TextAlign.HasValue)
                        l.TextAlign = ent.TextAlign.Value;
                    l.MouseDown += (md1, md2) => { MouseUpDownOnLabel?.Invoke(true, (Control)md1, md2); };        
                    l.MouseUp += (md1, md2) => { MouseUpDownOnLabel?.Invoke(false, (Control)md1, md2); };
                }
                else if (c is ExtButton)
                {
                    ExtButton b = c as ExtButton;
                    if (ent.TextAlign.HasValue)
                        b.TextAlign = ent.TextAlign.Value;
                    b.Click += (sender, ev) =>
                    {
                        if (!DisableTriggers)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(((Control)sender).Tag);
                            Trigger?.Invoke(Name, en.Name, this.CallerTag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                    };
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

                if (factor != null)     // when adding, form scaling has already been done, so we need to scale manually
                {
                    c.Scale(factor.Value);
                }
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
