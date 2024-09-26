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
    public class ConfigurableUC : UserControl
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

        // Lay the thing out like its in the normal dialog editor, with 8.25f font.  Leave space for the window less title bar/close icon.

        // returns dialog logical name, name of control (plus options), caller tag object
        // name of control on click for button / Checkbox / ComboBox
        // name:Return for number box, textBox.  Set SwallowReturn to true before returning to swallow the return key
        // name:Validity:true/false for Number boxes
        // Close if the close button is pressed
        // Escape if escape pressed
        // Resize if changed size
        // Reposition if position changed

        public event Action<string, string, Object> Trigger;

        public List<Entry> Entries { get { return entries; } }

        public bool IsResizable { get; set; } = true;
        public bool DisableTriggering { get; set; } = false;        // set to stop triggers

        public int BorderMargin { get; set; } = 3;       // space between window edge and outer area
        public bool AllowSpaceForScrollBar { get; set; } = true;       // allow for a scroll bar on right, reserves space for it if it thinks it needs it, else don't
        public bool SwallowReturn { get; set; }     // set in your trigger handler to swallow the return. Otherwise, return is return
        public BorderStyle PanelBorderStyle { get; set; } = BorderStyle.FixedSingle;
        public int TopPanelHeight { get; set; } = 0;        // in design units, 0 = off
        public int BottomPanelHeight { get; set; } = 0;     // in design units, 0 = off

        #endregion

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

           // public float PostThemeFontScale { get; set; } = 1.0f;   // post theme font scaler

            // general
            public Entry(string nam, Type c, string t, Point p, Size s, string tt)
            {
                ControlType = c; TextValue = t; Location = p; Size = s; ToolTip = tt; Name = nam; CustomDateFormat = "long";
            }
            //public Entry(string nam, Type c, string t, Point p, Size s, string tt, float fontscale, ContentAlignment align = ContentAlignment.MiddleCenter)
            //{
            //    ControlType = c; TextValue = t; Location = p; Size = s; ToolTip = tt; Name = nam; CustomDateFormat = "long"; PostThemeFontScale = fontscale; TextAlign = align;
            //}
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

        #region Public interface

        public string Add(string instr)       // add a string definition dynamically add to list.  errmsg if something is wrong
        {
            Entry e;
            string errmsg = MakeEntry(instr, out e, ref lastpos);
            if (errmsg == null)
                entries.Add(e);
            return errmsg;
        }
        public void Add(Entry e)               // add an entry..
        {
            entries.Add(e);
        }
        public void Add(ref int vpos, int vspacing, Entry e)               // add an entry with vpos increase
        {
            e.Location = new Point(e.Location.X, e.Location.Y + vpos);
            entries.Add(e);
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
            for ( int i = 0; i < tags.Length; i++)
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
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if ( t?.Control != null )
            {
                t.Control.Parent.Controls.Remove(t.Control);
                entries.Remove(t);
                entriesprocessed--;
            }
        }

        // move scroll panel controls at or below up/down by move. positions/move are after scaling 
        public void MoveControlsAt(int atorbelow, int move)
        {
            //System.Diagnostics.Debug.WriteLine($"Move Scaled {atorbelow} by {move}");
            foreach (Control c in contentpanel.Controls)
            {
                if (c.Top >= atorbelow)
                {
                    //System.Diagnostics.Debug.WriteLine($".. shift {c.Name} at {c.Top} by {move}");
                    c.Top += move;
               }
            }
        }

        // get control of name as type
        public T GetControl<T>(string controlname) where T : Control      // return value of dialog control
        {
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
                return (T)t.Control;
            else
                return null;
        }

        // get control by name
        public Control GetControl(string controlname )
        {
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
                return t.Control;
            else
                return null;
        }

        // return value of dialog control as a string. Null if can't express it as a string (not a supported type)
        public string Get(Entry t)      
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
        public object GetValue(Entry t)
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
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            return t != null ? Get(t) : null;
        }

        // Return GetValue() by controlname, null if can't get
        public T GetValue<T>(string controlname)
        {
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            return t != null ? (T)GetValue(t) : default(T);
        }

        // Return Get() from controls starting with this name
        public string[] GetByStartingName(string startingcontrolname)
        {
            var list = entries.Where(x => x.Name.StartsWith(startingcontrolname)).Select(x => x).ToArray();
            string[] res = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
                res[i] = Get(list[i]);
            return res;
        }

        // Return GetValue() from controls starting with this name
        public T[] GetByStartingName<T>(string startingcontrolname)
        {
            var list = entries.Where(x => x.Name.StartsWith(startingcontrolname)).Select(x => x).ToArray();
            T[] res = new T[list.Length];
            for (int i = 0; i < list.Length; i++)
                res[i] = (T)GetValue(list[i]);
            return res;
        }

        // from checkbox
        public bool? GetBool(string controlname)
        {
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
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
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
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
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
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
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
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
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
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
            var elist = entries.Where(x => x.Control is ExtCheckBox && x.Name.StartsWith(startingcontrolname)).Where(x => ((ExtCheckBox)x.Control).Checked).Select(x => removeprefix ? x.Name.Substring(startingcontrolname.Length) : x.Name).ToArray();
            return elist;
        }

        // from ExtCheckBox controls starting with this name, get the entries of ones checked
        public Entry[] GetCheckedListEntries(string startingcontrolname)
        {
            var elist = entries.Where(x => x.Control is ExtCheckBox && x.Name.StartsWith(startingcontrolname)).Where(x => ((ExtCheckBox)x.Control).Checked);
            return elist.ToArray();
        }

        // from ExtCheckBox controls starting with this name, get a bool array describing the check state
        public bool[] GetCheckBoxBools(string startingcontrolname)
        {
            var elist = entries.Where(x => x.Control is ExtCheckBox && x.Name.StartsWith(startingcontrolname)).Select(x => x).ToArray();
            var result = new bool[elist.Length];
            int i = 0;
            foreach (var e in elist)
            {
                var ctr = e.Control as ExtCheckBox;
                result[i++] = ctr.Checked;
            }
            return result;
        }

        // Set value of control by string value
        public bool Set(string controlname, string value)      
        {
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
            {
                Control c = t.Control;
                if (c is ExtTextBox )
                {
                    (c as ExtTextBox).Text = value;
                    return true;
                }
                else if ( c is Label)
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
        public void SetCheckedList(IEnumerable<string> controlnames,bool state)
        {
            var cnames = controlnames.ToArray();
            var elist = entries.Where(x => x.Control is ExtCheckBox && Array.IndexOf(cnames, x.Name) >= 0).Select(x => x);
            foreach (var e in elist)
            {
                (e.Control as ExtCheckBox).Checked = state;
            }
        }

        // radio button this set, to 1 entry, or to N max
        public void RadioButton(string startingcontrolname, string controlhit , int max = 1)
        {
            var elist = entries.Where(x => x.Control is ExtCheckBox && x.Name.StartsWith(startingcontrolname)).Select(x => x).ToArray();

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
                if ( numchecked>max)
                {
                    var chkbox = GetControl<ExtCheckBox>(controlhit);
                    chkbox.Checked = false;
                }
            }
        }

        // are all entries on this table which could be invalid valid?
        public bool IsAllValid()
        {
            foreach( var t in entries)
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

        #region Implementation
        public void Init(string caption, string lname, Object callertag, bool closeicon)
        {
            this.logicalname = lname;    // passed back to caller via trigger
            this.callertag = callertag;      // passed back to caller via trigger

            contentpanel = new ExtPanelVertScroll() { Name = "ContentPanel" };
          //  contentpanel.MouseDown += FormMouseDown;
          //  contentpanel.MouseUp += FormMouseUp;

            vertscrollpanel = new ExtPanelVertScrollWithBar() { Name = "VScrollPanel", BorderStyle = PanelBorderStyle, Margin = new Padding(0), Padding = new Padding(0) };
            vertscrollpanel.Controls.Add(contentpanel);
            vertscrollpanel.HideScrollBar = true;
            vertscrollpanel.Dock = DockStyle.Fill;
            Controls.Add(vertscrollpanel);

            Panel titleclosepanel = contentpanel;

            if (TopPanelHeight > 0) 
            {
                toppanel = new Panel() { Name = "TopPanel", BorderStyle = PanelBorderStyle, Size = new Size(100, TopPanelHeight) }; // we size now so its scaled by the themer, we never use the variables again
             //   toppanel.MouseDown += FormMouseDown;
             //   toppanel.MouseUp += FormMouseUp;
                Controls.Add(toppanel);
                titleclosepanel = toppanel;
            }

            if (BottomPanelHeight > 0)
            {
                bottompanel = new Panel() { Name = "BottomPanel", BorderStyle = PanelBorderStyle, Size = new Size(100, BottomPanelHeight) };// we size now so its scaled
            //    bottompanel.MouseDown += FormMouseDown;
            //    bottompanel.MouseUp += FormMouseUp;
                Controls.Add(bottompanel);
            }

            if (caption.HasChars())
            {
               // Label titlelabel = new Label() { Name = "title", Left = 4, Top = 8, Width = 10, Text = caption, AutoSize = true }; // autosize it, and set width small so it does not mess up the computation below
               //// titlelabel.MouseDown += FormMouseDown;
               //// titlelabel.MouseUp += FormMouseUp;
               // titlelabel.Name = "title";
               // titleclosepanel.Controls.Add(titlelabel);

                //if (closeicon)
                //{
                //    closebutton = new ExtButtonDrawn() { Name = "closebut", Size = new Size(18, 18), Location = new Point(0, 0) };     // purposely at top left to make it not contribute to overall size
                //    closebutton.ImageSelected = ExtButtonDrawn.ImageType.Close;
                //    closebutton.Click += (sender, f) =>
                //    {
                //        if (!DisableTriggering)
                //        {
                //            Trigger?.Invoke(logicalname, "Close", callertag);
                //        }
                //    };

                //    titleclosepanel.Controls.Add(closebutton);            // add now so it gets themed
                //}

      //          if (TopPanelHeight == 0)
           //         yoffset = titlelabel.Height + 8;
            }
        }

        public void Themed()
        {
            for (int i = 0; i < entries.Count; i++)     // record nominal pos after all positioning done
            {
                System.Diagnostics.Debug.WriteLine($"Theme applied {entries[i].Name} {entries[i].Location} {entries[i].Size} -> {entries[i].Control.Location} {entries[i].Control.Size}");
                entries[i].Location = entries[i].Control.Location;
                entries[i].Size = entries[i].Control.Size;
                if (entries[i].MinimumSize == Size.Empty)
                    entries[i].MinimumSize = entries[i].Size;
            }

            contentpanel.Recalcuate();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (contentpanel != null)       // only after Init
            {
                System.Diagnostics.Debug.WriteLine($"Configurable UC Layout");

                //int toppanelh = toppanel != null ? toppanel.Height + BorderMargin : 0;       // top margin only
                //int bottompanelh = bottompanel != null ? bottompanel.Height + BorderMargin : 0; // bottom margin only
                //int scrollpanelh = ClientRectangle.Height - toppanelh - bottompanelh - BorderMargin * 2;    // 2 margins around this
                //int width = ClientRectangle.Width - BorderMargin * 2;
                //int hpos = 0;

                //if (toppanel != null)
                //{
                //    toppanel.Location = new Point(BorderMargin, BorderMargin);
                //    toppanel.Size = new Size(width, toppanel.Height);
                //    hpos += toppanel.Height + BorderMargin;
                //}

                //foreach (Control c in contentpanel.Controls)
                //{
                //    System.Diagnostics.Debug.WriteLine($"   ContentPanel2 Pos {c.Name} {c.Bounds} {c.Bottom}");
                //}

                //vertscrollpanel.Bounds = new Rectangle(new Point(BorderMargin, hpos + BorderMargin), new Size(width, scrollpanelh));

                //foreach (Control c in contentpanel.Controls)
                //{
                //    System.Diagnostics.Debug.WriteLine($"   ContentPanel3 Pos {c.Name} {c.Bounds} {c.Bottom}");
                //}

                //hpos += scrollpanelh + BorderMargin;

                //if (bottompanel != null)
                //{
                //    bottompanel.Location = new Point(BorderMargin, hpos + BorderMargin);
                //    bottompanel.Size = new Size(width, bottompanel.Height);
                //}


                //if (closebutton != null)      // now position close at correct logical place
                //{
                //    if (closebutton.Parent == toppanel)
                //        closebutton.Location = new Point(toppanel.Width - closebutton.Width - Font.ScalePixels(6), Font.ScalePixels(4) - vertscrollpanel.ScrollValue);
                //    else
                //        closebutton.Location = new Point(vertscrollpanel.Width - (AllowSpaceForScrollBar ? vertscrollpanel.ScrollBarWidth : 0) - closebutton.Width - Font.ScalePixels(6), Font.ScalePixels(4));

                //    //System.Diagnostics.Debug.WriteLine($"Close button {closebutton.Location} size {closebutton.Size}");
                //}



            }
            base.OnLayout(e);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if ( !DisableTriggering && contentpanel != null )
            {
                System.Diagnostics.Debug.WriteLine($"ConfigurableUC On Resize scroll panel pos {vertscrollpanel.ScrollValue}");
                int pos = contentpanel.BeingPosition();
                contentpanel.FinishedPosition(pos);
    //            contentpanel.Recalcuate();
                Trigger?.Invoke(logicalname, "Resize", this.callertag);       // pass back the logical name of dialog, Resize, the caller tag
            }
        }

        public void AddEntries(SizeF? factor = null)
        {
            while( entriesprocessed < entries.Count)
            {
                int i = entriesprocessed++;
                Entry ent = entries[i];

                if (ent.Control != null && (contentpanel.Controls.Contains(ent.Control) ||
                                (toppanel != null && toppanel.Controls.Contains(ent.Control)) ||
                                (bottompanel != null && bottompanel.Controls.Contains(ent.Control))))
                {
                    continue;
                }                            

                Control c = ent.ControlType != null ? (Control)Activator.CreateInstance(ent.ControlType) : ent.Control;

                //System.Diagnostics.Debug.WriteLine($"Add Control {ent.Name} of {c.GetType()} at {ent.Location} {ent.Size} {ent.TextValue}");

                ent.Control = c;
                c.Size = ent.Size;
                c.Location = new Point(ent.Location.X, ent.Location.Y + yoffset);
                c.Resize += (s, e) => { Control c11 = s as Control; System.Diagnostics.Debug.WriteLine($"Control {c11.Name} resize {c11.Size}"); };
                c.LocationChanged += (s, e) => { Control c11 = s as Control; System.Diagnostics.Debug.WriteLine($"Control {c11.Name} loc changed {c11.Location}"); };
                c.Name = ent.Name;
                c.Enabled = ent.Enabled;
                if (!(ent.TextValue == null || c is ExtComboBox || c is ExtDateTimePicker
                        || c is NumberBoxDouble || c is NumberBoxLong || c is NumberBoxInt))        // everything but get text
                    c.Text = ent.TextValue;
                //             c.Tag = ent;     // point control tag at ent structure

                if (ent.Panel == Entry.PanelType.Top && toppanel != null)
                    toppanel.Controls.Add(c);
                else if (ent.Panel == Entry.PanelType.Bottom && bottompanel != null)
                    bottompanel.Controls.Add(c);
                else 
                    contentpanel.Controls.Add(c);

   //             if (ent.ToolTip != null)
   //                 this.FindToolTipControl()?.SetToolTip(c, ent.ToolTip);

   //             //System.Diagnostics.Debug.WriteLine($".. Control {ent.Name} of {c.GetType()} at {c.Location} {c.Size}");

   //             if (c is Label)
   //             {
   //                 Label l = c as Label;
   //                 if (ent.TextAlign.HasValue)
   //                     l.TextAlign = ent.TextAlign.Value;
   ////               //  l.MouseDown += (md1, md2) => { OnCaptionMouseDown((Control)md1, md2); };        // make em draggable
   //               //  l.MouseUp += (md1, md2) => { OnCaptionMouseUp((Control)md1, md2); };
   //             }
   //             else if (c is ExtButton)
   //             {
   //                 ExtButton b = c as ExtButton;
   //                 if (ent.TextAlign.HasValue)
   //                     b.TextAlign = ent.TextAlign.Value;
   //                 b.Click += (sender, ev) =>
   //                 {
   //                     if (!DisableTriggering)
   //                     {
   //                         Entry en = (Entry)(((Control)sender).Tag);
   //                         Trigger?.Invoke(logicalname, en.Name, this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
   //                     }
   //                 };
   //             }
   //             else if (c is NumberBoxDouble)
   //             {
   //                 NumberBoxDouble cb = c as NumberBoxDouble;
   //                 cb.Minimum = ent.NumberBoxDoubleMinimum;
   //                 cb.Maximum = ent.NumberBoxDoubleMaximum;
   //                 double? v = ent.TextValue == null ? ent.DoubleValue : ent.TextValue.InvariantParseDoubleNull();
   //                 cb.Value = v.HasValue ? v.Value : cb.Minimum;
   //                 if (ent.NumberBoxFormat != null)
   //                     cb.Format = ent.NumberBoxFormat;
   //                 cb.ReturnPressed += (box) =>
   //                 {
   //                     SwallowReturn = false;
   //                     if (!DisableTriggering)
   //                     {
   //                         Entry en = (Entry)(box.Tag);
   //                         Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
   //                     }

   //                     return SwallowReturn;
   //                 };
   //                 cb.ValidityChanged += (box, s) =>
   //                 {
   //                     if (!DisableTriggering)
   //                     {
   //                         Entry en = (Entry)(box.Tag);
   //                         Trigger?.Invoke(logicalname, en.Name + ":Validity:" + s.ToString(), this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
   //                     }
   //                 };
   //             }
   //             else if (c is NumberBoxLong)
   //             {
   //                 NumberBoxLong cb = c as NumberBoxLong;
   //                 cb.Minimum = ent.NumberBoxLongMinimum;
   //                 cb.Maximum = ent.NumberBoxLongMaximum;
   //                 long? v = ent.TextValue == null ? ent.LongValue : ent.TextValue.InvariantParseLongNull();
   //                 cb.Value = v.HasValue ? v.Value : cb.Minimum;
   //                 if (ent.NumberBoxFormat != null)
   //                     cb.Format = ent.NumberBoxFormat;
   //                 cb.ReturnPressed += (box) =>
   //                 {
   //                     SwallowReturn = false;
   //                     if (!DisableTriggering)
   //                     {
   //                         Entry en = (Entry)(box.Tag);
   //                         Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
   //                     }
   //                     return SwallowReturn;
   //                 };
   //                 cb.ValidityChanged += (box, s) =>
   //                 {
   //                     if (!DisableTriggering)
   //                     {
   //                         Entry en = (Entry)(box.Tag);
   //                         Trigger?.Invoke(logicalname, en.Name + ":Validity:" + s.ToString(), this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
   //                     }
   //                 };
   //             }
   //             else if (c is NumberBoxInt)
   //             {
   //                 NumberBoxInt cb = c as NumberBoxInt;
   //                 cb.Minimum = ent.NumberBoxLongMinimum == long.MinValue ? int.MinValue : (int)ent.NumberBoxLongMinimum;
   //                 cb.Maximum = ent.NumberBoxLongMaximum == long.MaxValue ? int.MaxValue : (int)ent.NumberBoxLongMaximum;
   //                 int? v = ent.TextValue == null ? (int)ent.LongValue : ent.TextValue.InvariantParseIntNull();
   //                 cb.Value = v.HasValue ? v.Value : cb.Minimum;
   //                 if (ent.NumberBoxFormat != null)
   //                     cb.Format = ent.NumberBoxFormat;
   //                 cb.ReturnPressed += (box) =>
   //                 {
   //                     SwallowReturn = false;
   //                     if (!DisableTriggering)
   //                     {
   //                         Entry en = (Entry)(box.Tag);
   //                         Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
   //                     }
   //                     return SwallowReturn;
   //                 };
   //                 cb.ValidityChanged += (box, s) =>
   //                 {
   //                     if (!DisableTriggering)
   //                     {
   //                         Entry en = (Entry)(box.Tag);
   //                         Trigger?.Invoke(logicalname, en.Name + ":Validity:" + s.ToString(), this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
   //                     }
   //                 };
   //             }
   //             else if (c is ExtTextBox)
   //             {
   //                 ExtTextBox tb = c as ExtTextBox;
   //                 tb.Multiline = tb.WordWrap = ent.TextBoxMultiline;

   //                 // this was here, but no idea why. removing as the multiline instances seem good
   //                 //tb.Size = ent.Size;     

   //                 tb.ClearOnFirstChar = ent.TextBoxClearOnFirstChar;
   //                 tb.ReturnPressed += (box) =>
   //                 {
   //                     SwallowReturn = false;
   //                     if (!DisableTriggering)
   //                     {
   //                         Entry en = (Entry)(box.Tag);
   //                         Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
   //                     }
   //                     return SwallowReturn;
   //                 };

   //                 if (tb.ClearOnFirstChar)
   //                     tb.SelectEnd();
   //             }
   //             else if (c is ExtCheckBox)
   //             {
   //                 ExtCheckBox cb = c as ExtCheckBox;
   //                 cb.Checked = ent.CheckBoxChecked;
   //                 cb.CheckAlign = ent.ContentAlign;
   //                 cb.Click += (sender, ev) =>
   //                 {
   //                     if (!DisableTriggering)
   //                     {
   //                         Entry en = (Entry)(((Control)sender).Tag);
   //                         Trigger?.Invoke(logicalname, en.Name, this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
   //                     }
   //                 };
   //             }
   //             else if (c is ExtDateTimePicker)
   //             {
   //                 ExtDateTimePicker dt = c as ExtDateTimePicker;
   //                 DateTime t;

   //                 if (ent.TextValue == null)
   //                     dt.Value = ent.DateTimeValue;
   //                 else if (DateTime.TryParse(ent.TextValue, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out t))     // assume local, so no conversion
   //                     dt.Value = t;

   //                 switch (ent.CustomDateFormat.ToLowerInvariant())
   //                 {
   //                     case "short":
   //                         dt.Format = DateTimePickerFormat.Short;
   //                         break;
   //                     case "long":
   //                         dt.Format = DateTimePickerFormat.Long;
   //                         break;
   //                     case "time":
   //                         dt.Format = DateTimePickerFormat.Time;
   //                         break;
   //                     default:
   //                         dt.CustomFormat = ent.CustomDateFormat;
   //                         break;
   //                 }
   //             }
   //             else if (c is ExtComboBox)
   //             {
   //                 ExtComboBox cb = c as ExtComboBox;

   //                 cb.Items.AddRange(ent.ComboBoxItems);
   //                 if (cb.Items.Contains(ent.TextValue))
   //                     cb.SelectedItem = ent.TextValue;
   //                 cb.SelectedIndexChanged += (sender, ev) =>
   //                 {
   //                     Control ctr = (Control)sender;
   //                     if (ctr.Enabled && !DisableTriggering)
   //                     {
   //                         Entry en = (Entry)(ctr.Tag);
   //                         Trigger?.Invoke(logicalname, en.Name, this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
   //                     }
   //                 };
   //             }

   //             if (factor != null)     // when adding, form scaling has already been done, so we need to scale manually
   //             {
   //                 c.Scale(factor.Value);
   //             }
            }
        }


        #endregion

        #region Text creator

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

        #region Variables

        private List<Entry> entries = new List<Entry>();

        private int entriesprocessed = 0;

        private Object callertag;
        private string logicalname;


        private System.Drawing.Point lastpos; // used for dynamically making the list up

        private Panel toppanel;
        private Panel bottompanel;
        private ExtPanelVertScroll contentpanel;
        private ExtPanelVertScrollWithBar vertscrollpanel;
        private ExtButtonDrawn closebutton;

        private int yoffset = 0;

        #endregion
    }
}
