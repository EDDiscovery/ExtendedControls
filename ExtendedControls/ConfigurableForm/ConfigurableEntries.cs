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

using BaseUtils;
using QuickJSON;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static ExtendedControls.ConfigurableEntryList;

namespace ExtendedControls
{
    public partial class ConfigurableEntryList
    {
        #region Properties

        // You give an array of Entries describing the controls
        // either added programatically by Add(entry) or via a string descriptor Add(string) - See action document for string descriptor format
        // Lay the thing out like its in the normal dialog editor, with 8.25f font.  Leave space for the title bar/close icon.
        // Triggers are sent back when actions happen. Two versions - Trigger (backwards compatible) and TriggerAdv with an additional data field.
        // See action document for trigger descriptions

        public List<Entry> Entries { get; private set; } = new List<Entry>();       // entry list
        public bool DisableTriggers { get; set; } = false;        // set to stop triggering of events
        public bool SwallowReturn { get; set; } = false;    // set in your trigger handler to swallow the return. Otherwise, return is return and is processed by windows that way
        public string Name { get; set; } = "Default";       // name to return in triggers
        public Object CallerTag { get; set; } = null;       // object to return in triggers

        // See action doc for triggers returned
        public Action<string, string, Object, Object,Object> UITrigger { get; set; } = null;      // return trigger (Dialog, control, value1, value2, tag)

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

            public bool Created { get; set; }                           // has the control been created by CreateEntries
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
                                                                        // button/ExtButtonWithNewCheckedListBox/CheckBox: if Resource:<fullpath> load embedded resource as image. If File:<file> load image as file. Else text
                                                                        // For number boxes, the invariant value, or Null and it will use the DoubleValue or LongValue
                                                                        // for Dates, the invariant culture assumed local
                                                                        // Can be NULL if you've passed your control in manually
            public double? DoubleValue { get; set; }                    // if its a number box double and you want it set (overrides Text)
                                                                        // Also used for splitter container as % (0-100) for top. Null will mean splitter is left as default
            public long? LongValue { get; set; }                        // if its a number box long, number box int or numeric up/down, and you want it set, use this with Text=null
            public DateTime? DateTimeValue { get; set; } = DateTime.MinValue;   // backup value of datetime, set to null to set in an external control

            public ContentAlignment? TextAlign { get; set; }            // label,button. Null its not applied
            public ContentAlignment ContentAlign { get; set; } = ContentAlignment.MiddleLeft;  // align for checkbox

            public bool? Horizontal { get; set; } = false;              // Split container, is horizontal split. Flow panel, is right to left (Horizontal) or top to bottom. Leave as Null for control sets up
            public bool? Pinned { get; set; } = false;                  // panelrollup. Set to null for control sets up
            public Padding Margin { get; set; }                         // flow panel, margin around item

            public bool CheckBoxChecked { get; set; }                   // checkbox

            public bool? TextBoxMultiline { get; set; } = false;        // textbox
            public bool TextBoxEscapeOnReport { get; set; } = false;   // escape characters back on reporting a text box Get()
            public bool? TextBoxClearOnFirstChar { get; set; } = false;    // fill in for textbox

            public string[] ComboBoxItems { get; set; }                 // for combo box, null if external control sets it up

            public string CustomDateFormat { get; set; }                // fill in for datetimepicker, or set null if want to control it externally

            public string NumberBoxFormat { get; set; } = null;         // format for number boxes
            public double? NumberBoxDoubleMinimum { get; set; } = double.MinValue;   // for double box. Can set to null for don't change if created externally
            public double? NumberBoxDoubleMaximum { get; set; } = double.MaxValue;
            public long? NumberBoxLongMinimum { get; set; } = long.MinValue;   // for long and int box. Can set to null for don't change if created externally. 
            public long? NumberBoxLongMaximum { get; set; } = long.MaxValue;   // Also for numericupdown 

            public Color? BackColor { get; set; } = null;               // panel, back colour to reapply after themeing

            public List<DataGridViewColumn> DGVColumns { get; set; } = null;       // for DGV. Set to null for external control, else set up
            public List<string> DGVSortMode { get; set; } = null;       // for DGV, sort mode control. May be left null for default sorting
            public int? DGVRowHeaderWidth { get; set; } = 0;             // for DGV. Set to null for don't change if created externally.

            //public System.Windows.Forms.ContextMenuStrip columnContextMenu { get; set; }

            public List<CheckedIconUserControl.Item> DropDownButtonList { get; set; } = null;  // for DropDownButton when created by ControlType. Else leave null for external created types 
            public string ButtonSettings { get; set; } = "";             // for DropDownButton
            public bool MultiColumns { get; set; } = false;             // for DropDownButton
            public bool AllOrNoneShown { get; set; } = false;           // for DropDownButton
            public bool AllOrNoneBack { get; set; } = false;           // for DropDownButton
            public bool SortItems { get; set; } = false;                // for DropDownButton
            public Size? ImageSize { get; set; } = null;                // forced image size for dropdownbutton

            public int Panel1MinPixelSize { get; set; } = 0;           // non zero, set. 0 for control sets up
            public int Panel2MinPixelSize { get; set; } = 0;           // non zero, set. 0 for control sets up


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

        #region Add

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
            Add(new Entry("OK", typeof(ExtButton), "OK".Tx(), p, sz.Value, tooltip) { Anchor = anchor, PlacedInPanel = paneltype });
        }

        public void AddCancel(Point p, string tooltip = null, Size? sz = null, AnchorStyles anchor = AnchorStyles.None, Entry.PanelType paneltype = Entry.PanelType.Scroll)
        {
            if (sz == null)
                sz = new Size(80, 24);
            Add(new Entry("Cancel", typeof(ExtButton), "Cancel".Tx(), p, sz.Value, tooltip) { Anchor = anchor, PlacedInPanel = paneltype });
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

  
        #region Triggering

        // send trigger, if not disabled
        public void SendTrigger(string actionorcontrolname, string value = null, Object advvalue = null, Object advvalue2 = null)
        {
            if (!DisableTriggers)
            {
                UITrigger?.Invoke(Name, actionorcontrolname + (value != null ? ":" + value : ""), advvalue, advvalue2, CallerTag);
            }
        }



        #endregion
    }
}
