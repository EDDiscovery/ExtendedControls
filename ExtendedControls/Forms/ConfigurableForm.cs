﻿/*
 * Copyright © 2017-2023 EDDiscovery development team
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
    public class ConfigurableForm : DraggableForm
    {
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

        public new bool AllowResize { get { return base.AllowResize; } set { base.AllowResize = value; } } // if form resizing (you need a BorderMargin)
        public int BorderMargin { get; set; } = 3;       // space between window edge and outer area
        public int BottomMargin { get; set; } = 8;      // Extra space right/bot to allow for extra space past the controls
        public int RightMargin { get; set; } = 8;       // Size this at 8.25f font size, it will be scaled to suit. 
        public bool AllowSpaceForScrollBar { get; set; } = true;       // allow for a scroll bar on right, reserves space for it if it thinks it needs it, else don't
        public bool ForceNoWindowsBorder { get; set; } = false;       // set to force no border theme
        public bool AllowSpaceForCloseButton { get; set; } = false;       // Allow space on right for close button (only set if your design means there won't normally be space for it)
        public bool Transparent { get; set; } = false;
        public bool SwallowReturn { get; set; }     // set in your trigger handler to swallow the return. Otherwise, return is return
        public Color BorderRectColour { get; set; } = Color.Empty;  // force border colour
        public BorderStyle PanelBorderStyle { get; set; } = BorderStyle.FixedSingle;
        public Size ExtraMarginRightBottom { get; set; } = new Size(16, 16);
        public float FontScale { get; set; } = 1.0f;

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
            public long LongValue { get; set; }                      // if its a number box long or int, set this, Text=null
            public DateTime DateTimeValue { get; set; }             // if its a date time, set this, Text=null

            public Point Location {get; set;}
            public Size Size {get; set;}
            public string ToolTip { get; set; }                     // can be null.

            public AnchorStyles Anchor { get; set; } = AnchorStyles.None;       // anchor to window

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

            public bool Enabled { get; set; } = true;       // is control enabled?


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

        #region Public interface

        public ConfigurableForm()
        {
            this.components = new System.ComponentModel.Container();
            entries = new List<Entry>();
            lastpos = new System.Drawing.Point(0, 0);
            AllowResize = false;
        }

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

        public void AddOK(Point p, string tooltip = null, Size? sz = null, AnchorStyles anchor = AnchorStyles.None)
        {
            if (sz == null)
                sz = new Size(80, 24);
            Add(new Entry("OK", typeof(ExtButton), "OK".TxID(ECIDs.OK), p, sz.Value, tooltip) { Anchor = anchor });
        }

        public void AddCancel(Point p, string tooltip = null, Size? sz = null, AnchorStyles anchor = AnchorStyles.None)
        {
            if (sz == null)
                sz = new Size(80, 24);
            Add(new Entry("Cancel", typeof(ExtButton), "Cancel".TxID(ECIDs.Cancel), p, sz.Value, tooltip) { Anchor = anchor });
        }

        public void AddLabelAndEntry(string labeltext, Point labelpos, Size labelsize, Entry e)
        {
            Add(new Entry("L" + e.Name, typeof(Label), labeltext, labelpos, labelsize, null));
            Add(e);
        }

        // vpos sets the vertical position. Entry.pos sets the X and offset Y from vpos
        public void AddLabelAndEntry(string labeltext, Point labelxvoff, ref int vpos, int vspacing, Size labelsize, Entry e)
        {
            Add(new Entry("L" + e.Name, typeof(Label), labeltext, new Point(labelxvoff.X, vpos + labelxvoff.Y), labelsize, null));
            e.Location = new Point(e.Location.X, e.Location.Y + vpos);
            Add(e);
            vpos += vspacing;
        }

        // handle OK and Close/Escape/Cancel
        public void InstallStandardTriggers(Action<string, string, Object> othertrigger = null)
        {
            Trigger += (dialogname, controlname, xtag) =>
            {
                if (controlname == "OK")
                    ReturnResult(DialogResult.OK);
                else if (controlname == "Close" || controlname == "Escape" || controlname == "Cancel")
                    ReturnResult(DialogResult.Cancel);
                else
                    othertrigger?.Invoke(dialogname, controlname, xtag);
            };
        }

        // remove a control from the list, both visually and from entries
        public void RemoveEntry(string controlname)
        {
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if ( t?.Control != null )
            {
                outerpanel.Controls.Remove(t.Control);
                entries.Remove(t);
            }
        }

        // must call if you add new controls after shown in a trigger
        public void UpdateDisplayAfterAddNewControls()
        {
            AddEntries(this.CurrentAutoScaleFactor());          // make new controls, and scale up by autoscalefactor
            Theme.Current.Apply(this, Theme.Current.GetScaledFont(FontScale), ForceNoWindowsBorder);    // retheme
            //this.DumpTree(0);
            SizeWindow(); // and size window again
        }

        // move controls at or below up/down by move. positions are before scaling so as you specified on creation
        public void MoveControls(int atorbelow, int move)
        {
            System.Diagnostics.Debug.WriteLine($"Shift {atorbelow} by {move}");
            atorbelow = (int)(atorbelow * this.CurrentAutoScaleFactor().Height + 0.5);        // must scale up, round up a little
            move = (int)(move * this.CurrentAutoScaleFactor().Height + 0.5);        // must scale up
            MoveControlsAt(atorbelow, move);
        }

        // move all controls at or below this control. Offset is before scaling
        public void MoveControls(string controlname, int move, int offset = -10)
        {
            move = (int)(move * this.CurrentAutoScaleFactor().Height + 0.5);        // must scale up
            offset = (int)(offset * this.CurrentAutoScaleFactor().Height + 0.5);        // must scale up

            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
                MoveControlsAt(t.Location.Y + offset, move);
        }

        // move controls at or below up/down by move. positions/move are after scaling 
        public void MoveControlsAt(int atorbelow, int move)
        {
            //System.Diagnostics.Debug.WriteLine($"Move Scaled {atorbelow} by {move}");
            foreach (Control c in outerpanel.Controls)
            {
                if (c.Top >= atorbelow)
                {
                    //System.Diagnostics.Debug.WriteLine($".. shift {c.Name} at {c.Top} by {move}");
                    c.Top += move;
               }
            }
        }

        // requestedsize.value < N force, >N minimum width

        public DialogResult ShowDialogCentred(Form p, Icon icon, string caption, string lname = null, Object callertag = null, Action callback = null, bool closeicon = false,
                                              Size? minsize = null, Size? maxsize = null, Size? requestedsize = null)
        {
            InitCentred(p, minsize.HasValue ? minsize.Value : new Size(1, 1), maxsize.HasValue ? maxsize.Value : new Size(50000,50000),
                           requestedsize.HasValue ? requestedsize.Value : new Size(1, 1), icon, caption, lname, callertag, closeicon: closeicon);
            callback?.Invoke();
            return ShowDialog(p);
        }

        public DialogResult ShowDialog(Form p, Point pos, Icon icon, string caption, string lname = null, Object callertag = null, Action callback = null, bool closeicon = false,
                                              Size? minsize = null, Size ? maxsize = null, Size? requestedsize = null)
        {
            Init(minsize.HasValue ? minsize.Value : new Size(1, 1), maxsize.HasValue ? maxsize.Value : new Size(50000, 50000),
                            requestedsize.HasValue ? requestedsize.Value : new Size(1, 1),
                            pos, icon, caption, lname, callertag, closeicon: closeicon);
            callback?.Invoke();
            return ShowDialog(p);
        }

        public void InitCentred(Form p, Icon icon, string caption, string lname = null, Object callertag = null,
                                AutoScaleMode asm = AutoScaleMode.Font, bool closeicon = false)
        {
            Init(icon, new Size(1,1), new Size(50000,50000), new Size(1,1), new Point((p.Left + p.Right) / 2, (p.Top + p.Bottom) / 2), caption, lname, callertag, closeicon,
                                    HorizontalAlignment.Center, ControlHelpersStaticFunc.VerticalAlignment.Middle, asm);
        }
        public void InitCentred(Form p, Size minsize, Size maxsize, Size requestedsize, Icon icon, string caption, string lname = null, Object callertag = null,
                                AutoScaleMode asm = AutoScaleMode.Font, bool closeicon = false)
        {
            Init(icon, minsize, maxsize, requestedsize, new Point((p.Left + p.Right) / 2, (p.Top + p.Bottom) / 2), caption, lname, callertag, closeicon,
                                    HorizontalAlignment.Center, ControlHelpersStaticFunc.VerticalAlignment.Middle, asm);
        }

        public void Init(Size minsize, Size maxsize, Size requestedsize, Point pos, Icon icon, string caption, string lname = null, Object callertag = null, 
                            AutoScaleMode asm = AutoScaleMode.Font, bool closeicon = false)
        {
            Init(icon, minsize, maxsize, requestedsize, pos, caption, lname, callertag, closeicon, null, null, asm);
        }

        public void ReturnResult(DialogResult result)           // MUST call to return result and close.  DO NOT USE DialogResult directly
        {
            ProgClose = true;
            DialogResult = result;
            base.Close();
        }

        public T GetControl<T>(string controlname) where T : Control      // return value of dialog control
        {
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
                return (T)t.Control;
            else
                return null;
        }

        public Control GetControl(string controlname )
        {
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
                return t.Control;
            else
                return null;
        }

        // from supported controls
        public string Get(string controlname)      
        {
            Entry t = entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            return t != null ? Get(t) : null;
        }

        // from supported controls, a list of results
        public List<string> GetList(string startingcontrolname)    
        {
            var list = entries.Where(x => x.Name.StartsWith(startingcontrolname)).Select(x=>x);
            List<string> res = new List<string>();
            foreach (var e in list)
                res.Add(Get(e));
            return res;
        }

        public string Get(Entry t)      // return value of dialog control
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

        public bool Set(string controlname, string value)      // set value of dialog control
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

        private void Init(Icon icon, System.Drawing.Size minsize, System.Drawing.Size maxsize, Size requestedsize, System.Drawing.Point pos, 
                                string caption, string lname, Object callertag, bool closeicon,
                                HorizontalAlignment? halign , ControlHelpersStaticFunc.VerticalAlignment? valign , 
                                AutoScaleMode asm)
        {
            this.logicalname = lname;    // passed back to caller via trigger
            this.callertag = callertag;      // passed back to caller via trigger

            this.halign = halign;
            this.valign = valign;

            this.minsize = minsize;       // set min size window
            this.maxsize = maxsize;
            this.requestedsize = requestedsize;

            Theme theme = Theme.Current;
            System.Diagnostics.Debug.Assert(theme != null);

            FormBorderStyle = FormBorderStyle.FixedDialog;

            //outer = new ExtPanelScroll() { Dock = DockStyle.Fill, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(0), Padding = new Padding(0) };
            outerpanel = new ExtPanelScroll() { Name = "Outer", BorderStyle = PanelBorderStyle, Margin = new Padding(0), Padding = new Padding(0) };
            outerpanel.MouseDown += FormMouseDown;
            outerpanel.MouseUp += FormMouseUp;
            Controls.Add(outerpanel);

            ExtScrollBar scr = new ExtScrollBar();
            scr.HideScrollBar = true;
            outerpanel.Controls.Add(scr);

            this.Text = caption;

            yoffset = 0;                            // adjustment to move controls up if windows frame present.
            
            if (theme.WindowsFrame && !ForceNoWindowsBorder)
            {
                yoffset = int.MaxValue;
                for (int i = 0; i < entries.Count; i++)             // find minimum control Y
                    yoffset = Math.Min(yoffset, entries[i].Location.Y);

                yoffset -= 8;           // place X spaces below top
            }
            else
            {
                titlelabel = new Label() { Name="title", Left = 4, Top = 8, Width = 10, Text = caption, AutoSize = true }; // autosize it, and set width small so it does not mess up the computation below
                titlelabel.MouseDown += FormMouseDown;
                titlelabel.MouseUp += FormMouseUp;
                titlelabel.Name = "title";
                outerpanel.Controls.Add(titlelabel);

                if (closeicon)
                {
                    closebutton = new ExtButtonDrawn() { Name = "closebut", Size = new Size(18, 18), Location = new Point(0, 0) };     // purposely at top left to make it not contribute to overall size
                    closebutton.ImageSelected = ExtButtonDrawn.ImageType.Close;
                    closebutton.Click += (sender, f) =>
                    {
                        if (!ProgClose)
                        {
                            Trigger?.Invoke(logicalname, "Close", callertag);
                        }
                    };

                    outerpanel.Controls.Add(closebutton);            // add now so it gets themed
                }
            }

            tooltipcontrol = new ToolTip(components);
            tooltipcontrol.ShowAlways = true;

            AddEntries();

            ShowInTaskbar = false;

            this.Icon = icon;

            this.AutoScaleMode = asm;

            // outer.FindMaxSubControlArea(0, 0,null,true); // debug

            theme.Apply(this, theme.GetScaledFont(FontScale), ForceNoWindowsBorder);
            //theme.Apply(this, new Font("ms Sans Serif", 16f));
            //this.DumpTree(0);
            //System.Diagnostics.Debug.WriteLine($"ConfigurableForm autoscale {this.CurrentAutoScaleDimensions} {this.AutoScaleDimensions} {this.CurrentAutoScaleFactor()}");

            if (Transparent)
            {
                TransparencyKey = BackColor;
                timer = new Timer();      // timer to monitor for entry into form when transparent.. only sane way in forms
                timer.Interval = 500;
                timer.Tick += CheckMouse;
                timer.Start();
            }

            for (int i = 0; i < entries.Count; i++)     // post scale any controls which ask for different font ratio sizes
            {
                if (entries[i].PostThemeFontScale != 1.0f)
                {
                    entries[i].Control.Font = new Font(entries[i].Control.Font.Name, entries[i].Control.Font.SizeInPoints * entries[i].PostThemeFontScale);
                }
            }

            // position 
            StartPosition = FormStartPosition.Manual;
            this.Location = pos;

            //System.Diagnostics.Debug.WriteLine("Bounds " + Bounds + " ClientRect " + ClientRectangle);
            //System.Diagnostics.Debug.WriteLine("Outer Bounds " + outer.Bounds + " ClientRect " + outer.ClientRectangle);
        }
    
        private void AddEntries(SizeF? factor = null)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                Entry ent = entries[i];

                if (ent.Control != null && outerpanel.Controls.Contains(ent.Control))       // don't double add
                    continue;

                Control c = ent.ControlType != null ? (Control)Activator.CreateInstance(ent.ControlType) : ent.Control;

                //System.Diagnostics.Debug.WriteLine($"Add Control {ent.Name} of {c.GetType()} at {ent.Location} {ent.Size} {ent.TextValue}");

                ent.Control = c;
                c.Size = ent.Size;
                c.Location = new Point(ent.Location.X, ent.Location.Y - yoffset);
                c.Name = ent.Name;
                c.Enabled = ent.Enabled;
                if (!(ent.TextValue == null || c is ExtComboBox || c is ExtDateTimePicker
                        || c is NumberBoxDouble || c is NumberBoxLong || c is NumberBoxInt))        // everything but get text
                    c.Text = ent.TextValue;
                c.Tag = ent;     // point control tag at ent structure
                
                
                outerpanel.Controls.Add(c);
                if (ent.ToolTip != null)
                    tooltipcontrol.SetToolTip(c, ent.ToolTip);

                //System.Diagnostics.Debug.WriteLine($".. Control {ent.Name} of {c.GetType()} at {c.Location} {c.Size}");

                if (c is Label)
                {
                    Label l = c as Label;
                    if (ent.TextAlign.HasValue)
                        l.TextAlign = ent.TextAlign.Value;
                    l.MouseDown += (md1, md2) => { OnCaptionMouseDown((Control)md1, md2); };        // make em draggable
                    l.MouseUp += (md1, md2) => { OnCaptionMouseUp((Control)md1, md2); };
                }
                else if (c is ExtButton)
                {
                    ExtButton b = c as ExtButton;
                    if (ent.TextAlign.HasValue)
                        b.TextAlign = ent.TextAlign.Value;
                    b.Click += (sender, ev) =>
                    {
                        if (!ProgClose)
                        {
                            Entry en = (Entry)(((Control)sender).Tag);
                            Trigger?.Invoke(logicalname, en.Name, this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
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
                        if (!ProgClose)
                        {
                            Entry en = (Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }

                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        if (!ProgClose)
                        {
                            Entry en = (Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Validity:" + s.ToString(), this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                    };
                }
                else if (c is NumberBoxLong)
                {
                    NumberBoxLong cb = c as NumberBoxLong;
                    cb.Minimum = ent.NumberBoxLongMinimum;
                    cb.Maximum = ent.NumberBoxLongMaximum;
                    long? v = ent.TextValue == null ?  ent.LongValue : ent.TextValue.InvariantParseLongNull();
                    cb.Value = v.HasValue ? v.Value : cb.Minimum;
                    if (ent.NumberBoxFormat != null)
                        cb.Format = ent.NumberBoxFormat;
                    cb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        if (!ProgClose)
                        {
                            Entry en = (Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        if (!ProgClose)
                        {
                            Entry en = (Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Validity:" + s.ToString(), this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
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
                        if (!ProgClose)
                        {
                            Entry en = (Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        if (!ProgClose)
                        {
                            Entry en = (Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Validity:" + s.ToString(), this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
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
                        if (!ProgClose)
                        {
                            Entry en = (Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
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
                        if (!ProgClose)
                        {
                            Entry en = (Entry)(((Control)sender).Tag);
                            Trigger?.Invoke(logicalname, en.Name, this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                    };
                }
                else if (c is ExtDateTimePicker)
                {
                    ExtDateTimePicker dt = c as ExtDateTimePicker;
                    DateTime t;

                    if (ent.TextValue == null)
                        t = ent.DateTimeValue;
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
                        if (ctr.Enabled && !ProgClose)
                        {
                            Entry en = (Entry)(ctr.Tag);
                            Trigger?.Invoke(logicalname, en.Name, this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                    };
                }

                if (factor != null)     // when adding, form scaling has already been done, so we need to scale manually
                {
                    c.Scale(factor.Value);
                }
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SizeWindow();
        }

        private void SizeWindow()
        { 
            int boundsh = Bounds.Height - ClientRectangle.Height;                   // allow for window border..  Only works after OnLoad.
            int boundsw = Bounds.Width - ClientRectangle.Width;
            int outerh = 2 + BorderMargin;
            int outerw = 2 + BorderMargin;

            // get the scaling factor, we adjust the right/bottom margins accordingly

            var currentautocale = this.CurrentAutoScaleFactor();            // how much did we scale up?

            //System.Diagnostics.Debug.WriteLine($"Perform size {currentautocale}");

            // measure the items after scaling. Exclude the scroll bar. Add on bounds/outers/margin

            if ( closebutton!=null )
                closebutton.Location = new Point(0, 0);     // set it back to 0,0 to ensure it does not influence find max

            Size measureitemsinwindow = outerpanel.FindMaxSubControlArea(boundsw + outerw + (int)(RightMargin * currentautocale.Width),
                                                                   boundsh + outerh + (int)(BottomMargin * currentautocale.Height),
                                                                   new Type[] { typeof(ExtScrollBar) }, false);


            //System.Diagnostics.Debug.WriteLine($"Size Controls {boundsh} {boundsw} {outerh} {outerw} wdata {measureitemsinwindow}");
            // now position in the screen, allowing for a scroll bar if required due to height restricted

            MinimumSize = minsize;       // setting this allows for small windows

            if (maxsize.Width < 32000)       // only set if not stupid (i.e default). If you set it stupid, it sure screws up the system when double click max
                MaximumSize = maxsize;      // and force limits

            int widthw = measureitemsinwindow.Width;
            if (closebutton != null && AllowSpaceForCloseButton)
                widthw += closebutton.Width;

            int scrollbarsizeifheightnotacheived = 0;
            if (AllowResize)                                                        // if resizable, must allow for scroll bar
                widthw += outerpanel.ScrollBarWidth;
            else
                scrollbarsizeifheightnotacheived = AllowSpaceForScrollBar ? outerpanel.ScrollBarWidth : 0;   // else only if asked, and only applied if needed

            widthw += ExtraMarginRightBottom.Width;

            if (requestedsize.Width < 0)
                widthw = -requestedsize.Width;
            else
                widthw = Math.Max(requestedsize.Width, widthw);

            int height = measureitemsinwindow.Height + ExtraMarginRightBottom.Height;
            if (requestedsize.Height < 0)
                height = -requestedsize.Height;
            else
                height = Math.Max(requestedsize.Height, height);

            this.PositionSizeWithinScreen(widthw, height, false, new Size(64,64), halign, valign, scrollbarsizeifheightnotacheived);

            outerpanel.Size = new Size(ClientRectangle.Width - BorderMargin * 2, ClientRectangle.Height - BorderMargin * 2);
            outerpanel.Location = new Point(BorderMargin, BorderMargin);

            if (closebutton != null)      // now position close at correct place, its not contributed to overall size
            {
                closebutton.Location = new Point(outerpanel.Width - closebutton.Width - (AllowSpaceForScrollBar ? outerpanel.ScrollBarWidth : 0), Font.ScalePixels(4));
                closebutton.Padding = new Padding(Font.ScalePixels(4));
            }

            for (int i = 0; i < entries.Count; i++)     // record nominal pos after all positioning done
            {
                entries[i].Location = entries[i].Control.Location;
                entries[i].Size = entries[i].Control.Size;
            }

            initialsize = outerpanel.Size;

            resizerepositionon = true;

           // System.Diagnostics.Debug.WriteLine("Form Load " + Bounds + " " + ClientRectangle + " Font " + Font);
        }

        protected override void OnShown(EventArgs e)
        {
            Control firsttextbox = outerpanel.Controls.FirstY(new Type[] { typeof(ExtRichTextBox), typeof(ExtTextBox), typeof(ExtTextBoxAutoComplete), typeof(NumberBoxDouble), typeof(NumberBoxFloat), typeof(NumberBoxLong) });
            if (firsttextbox != null)
                firsttextbox.Focus();       // focus on first text box
            base.OnShown(e);
            //System.Diagnostics.Debug.WriteLine("Form Shown " + Bounds + " " + ClientRectangle);

        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            if (!ProgClose && resizerepositionon )
                Trigger?.Invoke(logicalname, "Reposition", this.callertag);       // pass back the logical name of dialog, Moved, the caller tag
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!ProgClose && resizerepositionon)
            {
                outerpanel.Size = new Size(ClientRectangle.Width - BorderMargin * 2, ClientRectangle.Height - BorderMargin * 2);

                if (closebutton != null)      // now position close at correct logical place
                    closebutton.Location = new Point(outerpanel.Width - closebutton.Width - (AllowSpaceForScrollBar ? outerpanel.ScrollBarWidth : 0), Font.ScalePixels(4));

                int widthdelta = outerpanel.Width - initialsize.Width;
                int heightdelta = outerpanel.Height - initialsize.Height;
                //System.Diagnostics.Debug.WriteLine(Environment.NewLine + "Resize {0} {1} so {2}", widthdelta, heightdelta, outer.ScrollOffset);

                foreach ( var en in entries)
                {
                    en.Control.ApplyAnchor(en.Anchor, en.Location, en.Size, widthdelta, heightdelta);
                }

                Trigger?.Invoke(logicalname, "Resize", this.callertag);       // pass back the logical name of dialog, Resize, the caller tag
            }
        }

        private void CheckMouse(object sender, EventArgs e)     // best way of knowing your inside the client.. using mouseleave/enter with transparency does not work..
        {
            if (!ProgClose)
            {
                if (ClientRectangle.Contains(this.PointToClient(MousePosition)))
                {
                    panelshowcounter++;
                    if (panelshowcounter == 3)
                    {
                        TransparencyKey = Color.Empty;
                    }
                }
                else
                {
                    if (panelshowcounter >= 3 && Theme.Current != null)
                    {
                        TransparencyKey = Theme.Current.Form;
                    }
                    panelshowcounter = 0;
                }
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (ProgClose == false)
            {
                Trigger?.Invoke(logicalname, "Close", callertag);
                e.Cancel = ProgClose == false;     // if ProgClose is false, we don't want to close. Callback did not call ReturnResponse
            }

            base.OnFormClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Trigger?.Invoke(logicalname, "Escape", callertag);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            OnCaptionMouseDown((Control)sender, e);
        }

        private void FormMouseUp(object sender, MouseEventArgs e)
        {
            OnCaptionMouseUp((Control)sender, e);
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

            entry = new ConfigurableForm.Entry(name, ctype,
                        text, new System.Drawing.Point(x.Value, y.Value), new System.Drawing.Size(w.Value, h.Value), tip);

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

        private System.ComponentModel.IContainer components = null;     // replicate normal component container, so controls which look this
                                                                        // up for finding the tooltip can (TextBoxBorder)

        private List<Entry> entries;
        private Object callertag;
        private string logicalname;

        private bool ProgClose = false;

        private System.Drawing.Point lastpos; // used for dynamically making the list up

        private HorizontalAlignment? halign;
        private ControlHelpersStaticFunc.VerticalAlignment? valign;
        private Size minsize;
        private Size maxsize;
        private Size requestedsize;

        private ExtPanelScroll outerpanel;
        private ExtButtonDrawn closebutton;
        private Label titlelabel;
        private bool resizerepositionon;
        private Size initialsize;

        private Timer timer;
        private int panelshowcounter;

        private int yoffset;
        private ToolTip tooltipcontrol;
    }
}
