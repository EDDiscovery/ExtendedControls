/*
 * Copyright 2024-2024 EDDiscovery development team
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
    public partial class ConfigurableEntryList
    {
        public Entry Find(Predicate<Entry> pred)
        {
            var f = Entries.Find(pred);
            return f != null && f.Control != null ? f : null;
        }

        public Entry Find(string controlname)
        {
            return Entries.Find(x => x.Name.Equals(controlname) && x.Control != null);
        }

        public List<Entry>.Enumerator GetEnumerator()
        {
            var x = Entries.GetEnumerator();
            return Entries.GetEnumerator();
        }


        // get control of name as type
        public T GetControl<T>(string controlname) where T : Control      // return value of dialog control
        {
            Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t?.Control != null)
                return (T)t.Control;
            else
                return null;
        }

        // get control by name
        public Control GetControl(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t?.Control != null)
                return t.Control;
            else
                return null;
        }

        // return value of dialog control as a string. Null if can't express it as a string (not a supported type)
        public string Get(ConfigurableEntryList.Entry t)
        {
            // same order as set

            Control c = t.Control;
            if (c == null)
                return null;
            else if (c is Label || c.GetType() == typeof(ExtButton))
                return c.Text;
            else if (c is ExtTextBox)
            {
                string s = (c as ExtTextBox).Text;
                if (t.TextBoxEscapeOnReport)
                    s = s.EscapeControlChars();
                return s;
            }
            else if (c is ExtRichTextBox)
                return c.Text;
            else if (c is ExtCheckBox)
                return (c as ExtCheckBox).Checked ? "1" : "0";
            else if (c is ExtRadioButton)
                return (c as ExtRadioButton).Checked ? "1" : "0";
            else if (c is ExtDateTimePicker)
                return (c as ExtDateTimePicker).Value.ToString("yyyy/dd/MM HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            else if (c is NumberBoxLong)
            {
                var cn = c as NumberBoxLong;
                return cn.IsValid ? cn.Value.ToStringInvariant() : "INVALID";
            }
            else if (c is NumberBoxDouble)
            {
                var cn = c as NumberBoxDouble;
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
            else if (c is ExtPanelRollUp)
            {
                var cn = c as ExtPanelRollUp;
                return cn.PinState.ToStringIntValue();
            }
            else if (c is ExtButtonWithNewCheckedListBox)
            {
                var cn = c as ExtButtonWithNewCheckedListBox;
                return cn.Get();
            }
            else if (c is SplitContainer)
            {
                var cn = c as SplitContainer;
                return (cn.GetSplitterDistance() * 100).ToStringInvariant("N6");
            }
            else
                return null;
        }

        // return value of dialog control as a native value of the control (string/timedate etc). 
        // null if invalid, null if not a supported control
        public object GetValue(ConfigurableEntryList.Entry t)
        {
            // order the same as set

            Control c = t.Control;
            if (c == null)
                return null;
            else if (c is Label || c.GetType() == typeof(ExtButton))
                return c.Text;
            else if (c is ExtTextBox)
            {
                string s = (c as ExtTextBox).Text;
                if (t.TextBoxEscapeOnReport)
                    s = s.EscapeControlChars();
                return s;
            }
            else if (c is ExtRichTextBox)
                return c.Text;
            else if (c is ExtCheckBox)
                return (c as ExtCheckBox).Checked;
            else if (c is ExtDateTimePicker)
                return (c as ExtDateTimePicker).Value;
            else if (c is NumberBoxLong)
            {
                var cn = c as NumberBoxLong;
                return cn.IsValid ? cn.Value : default(long?);
            }
            else if (c is NumberBoxDouble)
            {
                var cn = c as NumberBoxDouble;
                return cn.IsValid ? cn.Value : default(double?);
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
            else if (c is ExtPanelRollUp)
            {
                var cn = c as ExtPanelRollUp;
                return cn.PinState;
            }
            else if (c is ExtButtonWithNewCheckedListBox)
            {
                var cn = c as ExtButtonWithNewCheckedListBox;
                return cn.Get();
            }
            else if (c is SplitContainer)
            {
                var cn = c as SplitContainer;
                return cn.GetSplitterDistance() * 100.0;
            }
            else
                return null;
        }

        // Return Get() by controlname, 
        public bool Get(string controlname, out string value)
        {
            ConfigurableEntryList.Entry t = Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            value = t != null ? Get(t) : null;
            return t != null;
        }

        // Return GetValue() by controlname, null if can't get
        public T GetValue<T>(string controlname)
        {
            ConfigurableEntryList.Entry t = Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            return t != null ? (T)GetValue(t) : default(T);
        }

        // Return Get() from controls starting with this name
        public string[] GetByStartingName(string startingcontrolname)
        {
            var list = Entries.Where(x => x.Name.StartsWith(startingcontrolname) && x.Control!=null).Select(x => x).ToArray();
            string[] res = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
                res[i] = Get(list[i]);
            return res;
        }

        // Return GetValue() from controls starting with this name
        public T[] GetByStartingName<T>(string startingcontrolname)
        {
            var list = Entries.Where(x => x.Name.StartsWith(startingcontrolname)&& x.Control!=null).Select(x => x).ToArray();
            T[] res = new T[list.Length];
            for (int i = 0; i < list.Length; i++)
                res[i] = (T)GetValue(list[i]);
            return res;
        }

        // from checkbox
        public bool? GetBool(string controlname)
        {
            ConfigurableEntryList.Entry t = Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
                    
            if (t != null)
            {
                if (t.Control is ExtRadioButton rb)
                    return rb.Checked;
                else if (t.Control is ExtCheckBox cb)
                    return cb.Checked;
            }
            return null;
        }
        // from numberbox, Null if not valid
        public double? GetDouble(string controlname)
        {
            ConfigurableEntryList.Entry t = Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is NumberBoxDouble);
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
            ConfigurableEntryList.Entry t = Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is NumberBoxLong);
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
            ConfigurableEntryList.Entry t = Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is NumberBoxInt);
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
            ConfigurableEntryList.Entry t = Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtDateTimePicker);
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

        public bool GetPosition(string controlname, out Rectangle r, SizeF autoscalefactor)
        {
            var entry = Find(controlname);
            if (entry != null)
            {
                r = new Rectangle((int)(entry.Control.Left / autoscalefactor.Width), (int)(entry.Control.Top / autoscalefactor.Height),
                                  (int)(entry.Control.Width / autoscalefactor.Width), (int)(entry.Control.Height / autoscalefactor.Height)
                    );
                return true;
            }
            else
            {
                r = Rectangle.Empty;
                return false;
            }
        }

        public object GetDGVColumnSettings(string controlname)
        {
            ConfigurableEntryList.Entry t = Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtPanelDataGridViewScroll);
            if (t != null)
            {
                var cn = t.Control as ExtPanelDataGridViewScroll;
                var dgv = cn.DGV;
                return dgv.GetColumnSettings();
            }
            return null;
        }


    }
}
