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

using BaseUtils;
using QuickJSON;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
            public long? LongValue { get; set; }                        // if its a number box long or int and you want it set, set this with Text=null
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
            public long? NumberBoxLongMinimum { get; set; } = long.MinValue;   // for long and int box. Can set to null for don't change if created externally
            public long? NumberBoxLongMaximum { get; set; } = long.MaxValue; 

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

        #region Create any new entries in the list

        // create entry in contentpanel/toppanel (may be null)/bottompanel (may be null)
        // tooltips go to tooltipcontrol
        // autosize factor if set causes a scaling of the location/size
        // YScollPos = scroll vertical setting, which is autosized

        public void CreateEntries(ExtPanelVertScroll contentpanel, Panel toppanel, Panel bottompanel, ToolTip tooltipcontrol, 
                                SizeF? autosizefactor = null, Font themefont = null, int Yscrollpos = 0)
        {
            foreach (var ent in Entries)
            {
                if (ent.Created)        // don't double create
                    continue;

                Control c = ent.ControlType != null ? (Control)Activator.CreateInstance(ent.ControlType) : ent.Control;

                //System.Diagnostics.Debug.WriteLine($"Add Control {ent.Name} of {c.GetType()} at {ent.Location} {ent.Size} {ent.TextValue}");

                ent.Control = c;
                c.Size = ent.Size;
                c.Margin = ent.Margin;

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
                    var find = Entries.Where(x => x.Name == ent.InPanel && x.Control != null).Select(y => y.Control).FirstOrDefault();
                    if (find == null)
                        find = Entries.Where(x => x.Name + ".1" == ent.InPanel && x.Control is SplitContainer).Select(y => ((SplitContainer)y.Control).Panel1).FirstOrDefault(); 
                    if (find == null)
                        find = Entries.Where(x => x.Name + ".2" == ent.InPanel && x.Control is SplitContainer).Select(y => ((SplitContainer)y.Control).Panel2).FirstOrDefault();

                    if (find != null)
                    {
                        find.Controls.Add(c);
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine($"ConfigurableEntries cannot find {ent.Name} in {ent.InPanel}, ensure panel is named correctly and already made before this entry");
                        System.Diagnostics.Debug.Assert(false, $"ConfigurableEntries cannot find {ent.Name} in {ent.InPanel}, ensure panel is named correctly and already made before this entry");
                        continue;
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
                    if (ent.TextValue != null)      // may be externally set up
                        l.Text = ent.TextValue;
                    if (ent.TextAlign.HasValue)
                        l.TextAlign = ent.TextAlign.Value;
                    l.MouseDown += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(true, (Control)md1, md2); };
                    l.MouseUp += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(false, (Control)md1, md2); };
                }
                else if (c is ExtButton)
                {
                    ExtButton b = c as ExtButton;

                    if ( ent.TextValue != null)     // it may be an external button which has set up text/image, if so, ent.TextValue = null
                        SetTextOrImage(ent, b);

                    if (ent.TextAlign.HasValue)
                        b.TextAlign = ent.TextAlign.Value;

                    if (c is ExtButtonWithNewCheckedListBox)    // this is an extended button
                    {
                        if (ent.DropDownButtonList != null)     // if we are creating using ent, do it.  Else we may be creating externally so don't re-init
                        {
                            ExtButtonWithNewCheckedListBox cb = c as ExtButtonWithNewCheckedListBox;
                            cb.Init(ent.DropDownButtonList, ent.ButtonSettings,
                                    (s, b1) => { SendTrigger(ent.Name, "DropDownButtonClosed:" + s, s); },
                                    ent.AllOrNoneShown, ent.AllOrNoneBack,
                                    null, //disabled
                                    ent.ImageSize,
                                    new Size(16, 16), // screenmargin
                                    new Size(64, 64), // close boundary
                                    ent.MultiColumns,
                                    null, // group, not needed
                                    ent.SortItems,
                                    (s, eb1) => { SendTrigger(ent.Name, "DropDownButtonPressed:" + s, s); });
                        }
                    }
                    else
                    {
                        b.Click += (sender, ev) =>
                        {
                            SendTrigger(ent.Name);
                        };
                    }
                }
                else if (c is NumberBoxDouble)
                {
                    NumberBoxDouble cb = c as NumberBoxDouble;
                    if ( ent.NumberBoxDoubleMinimum.HasValue)
                        cb.Minimum = ent.NumberBoxDoubleMinimum.Value;
                    if (ent.NumberBoxDoubleMaximum.HasValue)
                        cb.Maximum = ent.NumberBoxDoubleMaximum.Value;
                    if (ent.DoubleValue.HasValue)
                        cb.Value = ent.DoubleValue.Value;
                    else if (ent.TextValue != null)
                        cb.Value = ent.TextValue.InvariantParseDoubleNull() ?? cb.Minimum;

                    if (ent.NumberBoxFormat != null)
                        cb.Format = ent.NumberBoxFormat;
                    cb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        SendTrigger(ent.Name , "Return",cb.Value);
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name , "Validity:" + s.ToString(),cb.Value);
                    };
                    cb.ValueChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name, "ValueChanged:" + cb.Value.ToString(), cb.Value);
                    };
                }
                else if (c is NumberBoxLong)
                {
                    NumberBoxLong cb = c as NumberBoxLong;
                    if (ent.NumberBoxLongMinimum.HasValue)
                        cb.Maximum = ent.NumberBoxLongMinimum.Value;
                    if (ent.NumberBoxLongMaximum.HasValue)
                        cb.Minimum = ent.NumberBoxLongMaximum.Value;
                    if (ent.LongValue.HasValue)
                        cb.Value = ent.LongValue.Value;
                    else if (ent.TextValue != null)
                        cb.Value = ent.TextValue.InvariantParseLongNull() ?? cb.Minimum;
                    if (ent.NumberBoxFormat != null)
                        cb.Format = ent.NumberBoxFormat;
                    cb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        SendTrigger(ent.Name, "Return",cb.Value);
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name, "Validity:" + s.ToString(), cb.Value);
                    };
                    cb.ValueChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name, "ValueChanged:" + cb.Value.ToString(), cb.Value);
                    };
                }
                else if (c is NumberBoxInt)
                {
                    NumberBoxInt cb = c as NumberBoxInt;
                    if (ent.NumberBoxLongMinimum.HasValue)
                        cb.Minimum = ent.NumberBoxLongMinimum == long.MinValue ? int.MinValue : (int)ent.NumberBoxLongMinimum;
                    if (ent.NumberBoxLongMaximum.HasValue)
                        cb.Maximum = ent.NumberBoxLongMaximum == long.MaxValue ? int.MaxValue : (int)ent.NumberBoxLongMaximum;
                    if (ent.LongValue.HasValue)
                        cb.Value = (int)ent.LongValue.Value;
                    else if (ent.TextValue != null)
                        cb.Value = ent.TextValue.InvariantParseIntNull() ?? cb.Minimum;
                    if (ent.NumberBoxFormat != null)
                        cb.Format = ent.NumberBoxFormat;
                    cb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        SendTrigger(ent.Name , "Return",cb.Value);
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name , "Validity:" + s.ToString(),cb.Value);
                    };
                    cb.ValueChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name, "ValueChanged:" + cb.Value.ToString(), cb.Value);
                    };
                }
                else if (c is ExtTextBox)
                {
                    ExtTextBox tb = c as ExtTextBox;
                    if (ent.TextValue != null) // may be externally set up
                        tb.Text = ent.TextValue;

                    if ( ent.TextBoxMultiline.HasValue)
                        tb.Multiline = tb.WordWrap = ent.TextBoxMultiline.Value;

                    // this was here, but no idea why. removing as the multiline instances seem good
                    //tb.Size = ent.Size;     

                    if ( ent.TextBoxClearOnFirstChar.HasValue)
                        tb.ClearOnFirstChar = ent.TextBoxClearOnFirstChar.Value;

                    tb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        SendTrigger(ent.Name, "Return", tb.Text);
                        return SwallowReturn;
                    };

                    if (tb.ClearOnFirstChar)
                        tb.SelectEnd();
                }
                else if (c is ExtRichTextBox)
                {
                    ExtRichTextBox tb = c as ExtRichTextBox;
                    if (ent.TextValue != null) // may be externally set up
                        tb.Text = ent.TextValue;
                }
                else if (c is ExtCheckBox)
                {
                    ExtCheckBox cb = c as ExtCheckBox;
                     
                    if (ent.TextValue != null) // may be externally set up
                    {
                        if (SetTextOrImage(ent, cb))
                            cb.Appearance = Appearance.Button;
                    }

                    cb.Checked = ent.CheckBoxChecked;
                    cb.CheckAlign = ent.ContentAlign;
                    cb.Click += (sender, ev) =>
                    {
                        SendTrigger(ent.Name,null,cb.CheckState,cb.Checked.ToStringIntValue());       // backwards compatible, trigger does not send value
                    };
                }
                else if (c is ExtDateTimePicker)
                {
                    ExtDateTimePicker dt = c as ExtDateTimePicker;

                    if (ent.TextValue != null)      // if text set, use it to set value
                    {
                        if (DateTime.TryParse(ent.TextValue, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out DateTime t))     // assume local, so no conversion
                            dt.Value = t;
                        else
                            dt.Value = DateTime.MinValue;
                    }
                    else if (ent.DateTimeValue.HasValue)    // else if this is set, use this
                        dt.Value = ent.DateTimeValue.Value;

                    if (ent.CustomDateFormat != null)   
                    {
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

                    dt.ValueChanged += (s, e) => { SendTrigger(ent.Name, "ValueChanged:" + dt.Value.ToStringZuluInvariant(), dt.Value); };
                }
                else if (c is ExtComboBox)
                {
                    ExtComboBox cb = c as ExtComboBox;

                    if ( ent.ComboBoxItems != null )
                        cb.Items.AddRange(ent.ComboBoxItems);

                    if (ent.TextValue != null)
                    {
                        if (cb.Items.Contains(ent.TextValue))
                            cb.SelectedItem = ent.TextValue;
                    }
                    cb.SelectedIndexChanged += (sender, ev) =>
                    {
                        SendTrigger(ent.Name,null,cb.SelectedItem);     // again backwards compatible
                    };
                }
                else if ( c is ExtPanelDataGridViewScroll)
                {
                    ExtPanelDataGridViewScroll dgvs = c as ExtPanelDataGridViewScroll;
                    BaseUtils.DataGridViewColumnControl dgv = new BaseUtils.DataGridViewColumnControl();

                    if (ent.DGVRowHeaderWidth.HasValue )
                    {
                        dgv.RowHeadersVisible = ent.DGVRowHeaderWidth > 4;
                        if ( dgv.RowHeadersVisible )
                            dgv.RowHeadersWidth = ent.DGVRowHeaderWidth.Value;
                    }

                    dgv.AllowUserToAddRows = false;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv.ScrollBars = ScrollBars.None;

                    if (ent.DGVColumns != null)
                    {
                        for (int i = 0; i < ent.DGVColumns.Count; i++)
                        {
                            dgv.Columns.Add(ent.DGVColumns[i]);
                            dgv.Columns[i].SortMode = ent.DGVSortMode[i].EqualsIIC("Off") ? DataGridViewColumnSortMode.NotSortable : DataGridViewColumnSortMode.Automatic;
                        }
                    }

                    if (ent.DGVSortMode != null)
                    {
                        dgv.SortCompare += (s, e) =>
                        {
                            string sortmode = ent.DGVSortMode[e.Column.Index];
                            if (sortmode.EqualsIIC("Alpha"))
                                e.SortDataGridViewColumnAlpha();
                            else if (sortmode.EqualsIIC("Date"))
                                e.SortDataGridViewColumnDate();
                            else if (sortmode.EqualsIIC("Numeric"))
                                e.SortDataGridViewColumnNumeric();
                            else if (sortmode.EqualsIIC("NumericAlpha"))
                                e.SortDataGridViewColumnNumericThenAlpha();
                            else if (sortmode.EqualsIIC("AlphaInt"))
                                e.SortDataGridViewColumnAlphaInt();
                            else
                                e.SortDataGridViewColumnAlpha();
                        };
                    }

                    dgvs.Controls.Add(dgv);
                    ExtScrollBar sb = new ExtScrollBar();
                    dgvs.Controls.Add(sb);

                    dgv.Tag = ent;
                    dgv.SelectionChanged += Dgv_SelectionChanged;
                    dgv.Sorted += (s1,e1) => { SendTrigger(ent.Name, "SortColumn:" + dgv.SortedColumn.Index.ToStringInvariant(), dgv.SortedColumn.Index); };
                }
                else if (c is Panel)
                {
                    if (c is FlowLayoutPanel)
                    {
                        FlowLayoutPanel fp = c as FlowLayoutPanel;
                        if ( ent.Horizontal.HasValue)
                            fp.FlowDirection = ent.Horizontal.Value ? FlowDirection.LeftToRight : FlowDirection.TopDown;
                    }
                    else if (c is ExtPanelRollUp)
                    {
                        ExtPanelRollUp pr = c as ExtPanelRollUp;
                        if ( ent.Pinned.HasValue)
                            pr.PinState = ent.Pinned.Value;
                    }

                    c.MouseDown += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(true, (Control)md1, md2); };
                    c.MouseUp += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(false, (Control)md1, md2); };
                }
                else if (c is SplitContainer)
                {
                    var splitter = c as SplitContainer;
                    if ( ent.Horizontal.HasValue)
                        splitter.Orientation = ent.Horizontal.Value ? Orientation.Horizontal : Orientation.Vertical;

                    try
                    {
                        if (ent.Panel1MinPixelSize > 0)
                            splitter.Panel1MinSize = ent.Panel1MinPixelSize;
                        if (ent.Panel2MinPixelSize > 0)
                            splitter.Panel2MinSize = ent.Panel2MinPixelSize;
                        if ( ent.DoubleValue.HasValue)
                            splitter.SplitterDistance(ent.DoubleValue.Value / 100.0);
                    }
                    catch { }

                    splitter.SplitterMoved += (s2, e2) => { SendTrigger(ent.Name , "SplitterMoved:" + (splitter.GetSplitterDistance() * 100.0).ToStringInvariant(), splitter.GetSplitterDistance() * 100.0); };
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

                if (themefont != null)          // if themeing, apply it
                    Theme.Current.Apply(c, themefont);

                ent.Created = true;
            }
        }

        public bool SetTextOrImage(Entry ent, ButtonBase b) 
        {
            if (ent.TextValue.StartsWith("Resource:"))
            {
                Image img = BaseUtils.ResourceHelpers.GetResourceAsImage(ent.TextValue.Substring(9));
                if (img != null)
                {
                    b.Text = "";
                    b.Image = img;
                    return true;
                }
                else
                    b.Text = "Failed load";
            }
            else if (ent.TextValue.StartsWith("File:"))
            {
                try
                {
                    b.Text = "";
                    b.Image = ent.TextValue.Substring(5).LoadBitmapNoLock();    // So the file can be released
                    return true;
                }
                catch
                {
                    b.Text = "Failed load";
                }
            }
            else
            {
                b.Image = null;
                b.Text = ent.TextValue;
            }

            return false;
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Entry ent = dgv.Tag as Entry;
            var rows = dgv.SelectedRows;
            if ( rows.Count>0)
            {
               // System.Diagnostics.Debug.WriteLine($"Entry {ent.Name} changed selection Rows {rows.Count}");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataGridViewRow r in rows)
                    sb.AppendPrePad(r.Index.ToStringInvariant(), ";");
                SendTrigger(ent.Name, "RowSelection:"+ sb.ToString(),sb.ToString());
            }
            else
            {
                var cells = dgv.SelectedCells;
              //  System.Diagnostics.Debug.WriteLine($"Entry {ent.Name} changed selection Cells {cells.Count}");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataGridViewCell c in cells)
                    sb.AppendPrePad(c.RowIndex.ToStringInvariant() + "," + c.ColumnIndex.ToStringInvariant(), ";");
                SendTrigger(ent.Name, "CellSelection:"+sb.ToString(),sb.ToString());
            }
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
            else if (ctype == typeof(ExtCheckBox))
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
            else if (ctype == typeof(ExtRichTextBox) || ctype == typeof(ExtButton) || ctype == typeof(Label) )  // all of these don't have additional paras
            {
            }

            // if missing the tip (therefore no more paras) or have tip but no more text, we have an error, as the below all need paras

            else if (!moreparaspos || sp.IsEOL)     
            {
                return $"Missing Parameters for {name}";
            }

            else if (ctype == typeof(ExtComboBox))
            {
                var list = sp.NextQuotedWordList(otherterminators:"", replaceescape: true);     // quotes allowed, spaces between commas allowed
                if (list != null && list.Count>0)
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
            else
                System.Diagnostics.Debug.Assert(false, "Missing handling type");

            if (!sp.IsEOL)
                return $"Extra parameters at end of line for {name}";

            lastpos = new System.Drawing.Point(x.Value, y.Value);
            return null;
        }


        #endregion
    }
}
