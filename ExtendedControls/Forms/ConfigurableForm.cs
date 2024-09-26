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
    public class ConfigurableForm : DraggableForm
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

        public ConfigurableEntryList Entries { get; private set; } = new ConfigurableEntryList();

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
        public int TopPanelHeight { get; set; } = 0;        // in design units, 0 = off
        public int BottomPanelHeight { get; set; } = 0;     // in design units, 0 = off

        #endregion

        #region Public interface

        public ConfigurableForm()
        {
            this.components = new System.ComponentModel.Container();
            lastpos = new System.Drawing.Point(0, 0);
            AllowResize = false;
        }

        public string Add(string instr)       // add a string definition dynamically add to list.  errmsg if something is wrong
        {
            return Entries.Add(instr);
        }
        public void Add(ConfigurableEntryList.Entry e)               // add an entry..
        {
            Entries.Add(e);
        }
        public void Add(ref int vpos, int vspacing, ConfigurableEntryList.Entry e)               // add an ConfigurableEntryList.Entry with vpos increase
        {
            Entries.Add(ref vpos, vspacing, e);
        }

        public void AddOK(Point p, string tooltip = null, Size? sz = null, AnchorStyles anchor = AnchorStyles.None, ConfigurableEntryList.Entry.PanelType paneltype = ConfigurableEntryList.Entry.PanelType.Scroll)
        {
            Entries.AddOK(p, tooltip, sz, anchor, paneltype);
        }

        public void AddCancel(Point p, string tooltip = null, Size? sz = null, AnchorStyles anchor = AnchorStyles.None, ConfigurableEntryList.Entry.PanelType paneltype = ConfigurableEntryList.Entry.PanelType.Scroll)
        {
            Entries.AddCancel(p, tooltip, sz, anchor, paneltype);
        }

        public void AddLabelAndEntry(string labeltext, Point labelpos, Size labelsize, ConfigurableEntryList.Entry e)
        {
            Entries.AddLabelAndEntry(labeltext, labelpos, labelsize, e);
        }

        // vpos sets the vertical position. Entry.pos sets the X and offset Y from vpos
        public void AddLabelAndEntry(string labeltext, Point labelxvoff, ref int vpos, int vspacing, Size labelsize, ConfigurableEntryList.Entry e)
        {
            Entries.AddLabelAndEntry(labeltext, labelxvoff, ref vpos, vspacing, labelsize, e);
        }

        // add bool array of names and tags to scroll panel
        // optionally prefix the tags, and offset into the bools array
        public int AddBools(string[] tags, string[] names, bool[] bools, int vposstart, int vspacing, int depth, int xstart, int xspacing, string tagprefix = "", int boolsoffset = 0)
        {
            return Entries.AddBools(tags, names, bools, vposstart, vspacing, depth, xstart, xspacing, tagprefix, boolsoffset);
        }

        // add array of names and tags to scroll panel, using a hashset to work out what is checked
        // optionally prefix the tags, and offset into the bools array
        public int AddBools(string[] tags, string[] names, HashSet<string> ischecked, int vposstart, int vspacing, int depth, int xstart, int xspacing, string tagprefix = "")
        {
            return Entries.AddBools(tags, names, ischecked, vposstart, vspacing, depth, xstart, xspacing, tagprefix);
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
            Entries.RemoveEntry(controlname);
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

            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
                MoveControlsAt(t.Location.Y + offset, move);
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

        // Helper to create a standard dialog, centred, with a top panel area, with standard triggers, and OK is enabled when all is valid
        // you need to add OK yourself.

        public static void ShowDialogCentred(Action<ConfigurableForm> addtoform, Action<ConfigurableForm> okpressed, Control frm, string title, int toppanelheight = 0, int botpanelheight = 0)
        {
            ConfigurableForm f = new ConfigurableForm();
            f.TopPanelHeight = toppanelheight;
            f.BottomPanelHeight = botpanelheight;
            addtoform(f);
            f.InstallStandardTriggers();
            f.Trigger += (name, text, obj) => { f.GetControl("OK").Enabled = f.IsAllValid(); };
            if (f.ShowDialogCentred(frm.FindForm(), frm.FindForm().Icon, title, closeicon: true) == DialogResult.OK)
            {
                okpressed(f);
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
                                AutoScaleMode asm = AutoScaleMode.Font, bool closeicon = false, Size? minsize = null, Size? maxsize = null)
        {
            Init(icon, minsize.HasValue ? minsize.Value: new Size(1,1), 
                       maxsize.HasValue ? maxsize.Value : new Size(50000,50000), 
                       new Size(1,1), new Point((p.Left + p.Right) / 2, (p.Top + p.Bottom) / 2), caption, lname, callertag, closeicon,
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

        // get control of name as type
        public T GetControl<T>(string controlname) where T : Control      // return value of dialog control
        {
            return Entries.GetControl<T>(controlname);
        }

        // get control by name
        public Control GetControl(string controlname )
        {
            return Entries.GetControl(controlname);
        }

        // return value of dialog control as a string. Null if can't express it as a string (not a supported type)
        public string Get(ConfigurableEntryList.Entry t)      
        {
            return Entries.Get(t);
        }

        // return value of dialog control as a native value of the control (string/timedate etc). 
        // null if invalid, null if not a supported control
        public object GetValue(ConfigurableEntryList.Entry t)
        {
            return Entries.GetValue(t);
        }

        // Return Get() by controlname, null if can't get
        public string Get(string controlname)
        {
            return Entries.Get(controlname);
        }

        // Return GetValue() by controlname, null if can't get
        public T GetValue<T>(string controlname)
        {
            return Entries.GetValue<T>(controlname);
        }

        // Return Get() from controls starting with this name
        public string[] GetByStartingName(string startingcontrolname)
        {
            return Entries.GetByStartingName(startingcontrolname);
        }

        // Return GetValue() from controls starting with this name
        public T[] GetByStartingName<T>(string startingcontrolname)
        {
            return Entries.GetByStartingName<T>(startingcontrolname);
        }

        // from checkbox
        public bool? GetBool(string controlname)
        {
            return Entries.GetBool(controlname);
        }
        // from numberbox, Null if not valid
        public double? GetDouble(string controlname)
        {
            return Entries.GetDouble(controlname);
        }

        // from numberbox, Null if not valid
        public long? GetLong(string controlname)     
        {
            return Entries.GetLong(controlname);
        }
        // from numberbox, Null if not valid
        public int? GetInt(string controlname)     
        {
            return Entries.GetInt(controlname);
        }
        // from DateTimePicker, Null if not valid
        public DateTime? GetDateTime(string controlname)
        {
            return Entries.GetDateTime(controlname);
        }

        // from ExtCheckBox controls starting with this name, get the names of the ones checked, removing the prefix unless told not too
        public string[] GetCheckedListNames(string startingcontrolname, bool removeprefix = true)
        {
            return Entries.GetCheckedListNames(startingcontrolname, removeprefix);
        }

        // from ExtCheckBox controls starting with this name, get the entries of ones checked
        public ConfigurableEntryList.Entry[] GetCheckedListEntries(string startingcontrolname)
        {
            return Entries.GetCheckedListEntries(startingcontrolname);
        }

        // from ExtCheckBox controls starting with this name, get a bool array describing the check state
        public bool[] GetCheckBoxBools(string startingcontrolname)
        {
            return Entries.GetCheckBoxBools(startingcontrolname);
        }

        // Set value of control by string value
        public bool Set(string controlname, string value)      
        {
            return Entries.Set(controlname, value);
        }

        // from controls starting with this name, set the names of the ones checked
        public void SetCheckedList(IEnumerable<string> controlnames,bool state)
        {
            Entries.SetCheckedList(controlnames, state);
        }

        // radio button this set, to 1 entry, or to N max
        public void RadioButton(string startingcontrolname, string controlhit , int max = 1)
        {
            Entries.RadioButton(startingcontrolname, controlhit, max);
        }

        // are all entries on this table which could be invalid valid?
        public bool IsAllValid()
        {
            return Entries.IsAllValid();
        }

         #endregion

        #region Implementation

        private void Init(Icon icon, Size minsize, Size maxsize, Size requestedsize, Point pos, 
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

            contentpanel = new ExtPanelVertScroll() { Name = "ContentPanel"};
            contentpanel.MouseDown += FormMouseDown;
            contentpanel.MouseUp += FormMouseUp;
            contentpanel.Dock = DockStyle.Fill;

            vertscrollpanel = new ExtPanelVertScrollWithBar() { Name = "VScrollPanel", BorderStyle = PanelBorderStyle, Margin = new Padding(0), Padding = new Padding(0) };
            vertscrollpanel.Controls.Add(contentpanel);
            vertscrollpanel.HideScrollBar = true;
            Controls.Add(vertscrollpanel);

            Panel titleclosepanel = contentpanel;

            if (TopPanelHeight > 0)
            {
                toppanel = new Panel() { Name = "TopPanel", BorderStyle = PanelBorderStyle, Size = new Size(100, TopPanelHeight) }; // we size now so its scaled by the themer, we never use the variables again
                toppanel.MouseDown += FormMouseDown;
                toppanel.MouseUp += FormMouseUp;
                Controls.Add(toppanel);
                titleclosepanel = toppanel;
            }

            if (BottomPanelHeight > 0)
            {
                bottompanel = new Panel() { Name = "BottomPanel", BorderStyle = PanelBorderStyle, Size = new Size(100, BottomPanelHeight) };// we size now so its scaled
                bottompanel.MouseDown += FormMouseDown;
                bottompanel.MouseUp += FormMouseUp;
                Controls.Add(bottompanel);
            }

            this.Text = caption;

            yoffset = 0;                            // adjustment to move controls up if windows frame present.
            
            if (theme.WindowsFrame && !ForceNoWindowsBorder)
            {
                yoffset = int.MaxValue;
                foreach(var e in Entries)
                    yoffset = Math.Min(yoffset, e.Location.Y);

                yoffset -= 8;           // place X spaces below top
            }
            else
            {
                titlelabel = new Label() { Name="title", Left = 4, Top = 8, Width = 10, Text = caption, AutoSize = true }; // autosize it, and set width small so it does not mess up the computation below
                titlelabel.MouseDown += FormMouseDown;
                titlelabel.MouseUp += FormMouseUp;
                titlelabel.Name = "title";
                titleclosepanel.Controls.Add(titlelabel);

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

                    titleclosepanel.Controls.Add(closebutton);            // add now so it gets themed
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

            //contentpanel.BackColor = Color.Red;
            //this.DumpTree(0);
            //System.Diagnostics.Debug.WriteLine($"ConfigurableForm autoscale {this.CurrentAutoScaleDimensions} {this.AutoScaleDimensions} {this.CurrentAutoScaleFactor()}");
            //System.Diagnostics.Debug.WriteLine($"Toppanel height {toppanel?.Height} bottom {bottompanel?.Height}");

            // if ( toppanel!= null )  toppanel.BackColor = Color.FromArgb(255, 80, 40, 40);
            // if ( bottompanel != null ) bottompanel.BackColor = Color.FromArgb(255, 40, 80, 40);
            //scrollpanel.BackColor = Color.FromArgb(255, 80, 40, 40);

            if (Transparent)
            {
                TransparencyKey = BackColor;
                timer = new Timer();      // timer to monitor for ConfigurableEntryList.Entry into form when transparent.. only sane way in forms
                timer.Interval = 500;
                timer.Tick += CheckMouse;
                timer.Start();
            }

            foreach( var e in Entries)
            {
                if (e.PostThemeFontScale != 1.0f)
                {
                    e.Control.Font = new Font(e.Control.Font.Name, e.Control.Font.SizeInPoints * e.PostThemeFontScale);
                }
            }

            // position 
            StartPosition = FormStartPosition.Manual;
            this.Location = pos;

            //System.Diagnostics.Debug.WriteLine("Bounds " + Bounds + " ClientRect " + ClientRectangle);
            //System.Diagnostics.Debug.WriteLine("Outer Bounds " + outer.Bounds + " ClientRect " + outer.ClientRectangle);
        }
    
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
          //  System.Diagnostics.Debug.WriteLine($"Form Load Start {Bounds} {ClientRectangle} cp {contentpanel.Size}");
            SizeWindow();
          //  System.Diagnostics.Debug.WriteLine($"Form Load End {Bounds} {ClientRectangle} cp {contentpanel.Size}");
        }

        private void SizeWindow()
        { 
            int boundsh = Bounds.Height - ClientRectangle.Height;                   // allow for window border..  Only works after OnLoad.
            int boundsw = Bounds.Width - ClientRectangle.Width;

            // get the scaling factor, we adjust the right/bottom margins accordingly

            var currentautocale = this.CurrentAutoScaleFactor();            // how much did we scale up?

            //System.Diagnostics.Debug.WriteLine($"Perform size {currentautocale}");

            if ( closebutton!=null )
                closebutton.Location = new Point(0, 0);     // set it back to 0,0 to ensure it does not influence find max

            // measure the items after scaling in scroll panel. Exclude the scroll bar.
            Size measureitemsinwindow = contentpanel.FindMaxSubControlArea(0, 0, new Type[] { typeof(ExtScrollBar) }, false);

            if (toppanel != null)       // width check on this
            {
                Size s = toppanel.FindMaxSubControlArea(0, 0, null, false);
                measureitemsinwindow.Width = Math.Max(measureitemsinwindow.Width, s.Width);
            }

            if (bottompanel != null)    // and on bottom
            {
                Size s = bottompanel.FindMaxSubControlArea(0, 0, null, false);
                measureitemsinwindow.Width = Math.Max(measureitemsinwindow.Width, s.Width);
            }

            measureitemsinwindow.Width += boundsw + 2 + BorderMargin * 2 + (int)(RightMargin * currentautocale.Width);        // extra width due to bounds
            measureitemsinwindow.Height += boundsh + 2 + BorderMargin * 2 + (int)(BottomMargin * currentautocale.Height);

            if (toppanel != null)
                measureitemsinwindow.Height += toppanel.Height + BorderMargin;       // border margin at top

            if (bottompanel != null)
                measureitemsinwindow.Height += bottompanel.Height + BorderMargin;    // border margin at bottom

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
                widthw += vertscrollpanel.ScrollBarWidth;
            else
                scrollbarsizeifheightnotacheived = AllowSpaceForScrollBar ? vertscrollpanel.ScrollBarWidth : 0;   // else only if asked, and only applied if needed

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

            PositionPanels();

            int curpos = contentpanel.BeingPosition();

            foreach( var e in Entries)
            {
                e.Location = e.Control.Location;
                e.Size = e.Control.Size;
                if (e.MinimumSize == Size.Empty)
                    e.MinimumSize = e.Size;
            }

            initialscrollpanelsize = contentpanel.Size;

            contentpanel.FinishedPosition(curpos);

            resizerepositionon = true;

           // System.Diagnostics.Debug.WriteLine("Form Load " + Bounds + " " + ClientRectangle + " Font " + Font);
        }

        private void PositionPanels()
        {
            int toppanelh = toppanel != null ? toppanel.Height + BorderMargin : 0;       // top margin only
            int bottompanelh = bottompanel != null ? bottompanel.Height + BorderMargin : 0; // bottom margin only
            int scrollpanelh = ClientRectangle.Height - toppanelh - bottompanelh - BorderMargin * 2;    // 2 margins around this
            int width = ClientRectangle.Width - BorderMargin * 2;
            int hpos = 0;

            if (toppanel != null)
            {
                toppanel.Location = new Point(BorderMargin, BorderMargin);
                toppanel.Size = new Size(width, toppanel.Height);
                hpos += toppanel.Height + BorderMargin;
            }

            vertscrollpanel.Location = new Point(BorderMargin, hpos + BorderMargin);
            vertscrollpanel.Size = new Size(width, scrollpanelh);
            hpos += scrollpanelh + BorderMargin;

            if (bottompanel != null)
            {
                bottompanel.Location = new Point(BorderMargin, hpos+BorderMargin);
                bottompanel.Size = new Size(width, bottompanel.Height);
            }

            if (closebutton != null)      // now position close at correct logical place
            {
                if ( closebutton.Parent == toppanel )
                    closebutton.Location = new Point(toppanel.Width - closebutton.Width - Font.ScalePixels(6), Font.ScalePixels(4));
                else
                    closebutton.Location = new Point(vertscrollpanel.Width - (AllowSpaceForScrollBar ? vertscrollpanel.ScrollBarWidth :0 )-  closebutton.Width - Font.ScalePixels(6), Font.ScalePixels(4));
                
                //System.Diagnostics.Debug.WriteLine($"Close button {closebutton.Location} size {closebutton.Size}");
            }
        }

        protected override void OnShown(EventArgs e)
        {
            Control firsttextbox = contentpanel.Controls.FirstY(new Type[] { typeof(ExtRichTextBox), typeof(ExtTextBox), typeof(ExtTextBoxAutoComplete), typeof(NumberBoxDouble), typeof(NumberBoxFloat), typeof(NumberBoxLong) });
            if (firsttextbox != null)
                firsttextbox.Focus();       // focus on first text box
            base.OnShown(e);
           // System.Diagnostics.Debug.WriteLine($"Form Shown {Bounds} {ClientRectangle} {contentpanel.Size}");
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
                PositionPanels();

                int widthdelta = contentpanel.Width - initialscrollpanelsize.Width;
                int heightdelta = contentpanel.Height - initialscrollpanelsize.Height;
                //System.Diagnostics.Debug.WriteLine($"CF Resize {teaidthdelta}x{heightdelta} scrollpanel {scrollpanel.Size}");

                foreach ( var en in Entries)
                {
                    if (en.Control.Parent == contentpanel)
                    {
                        //System.Diagnostics.Debug.WriteLine($"..CF Apply to {en.Control.Name} {en.Control.Size}");
                        en.Control.ApplyAnchor(en.Anchor, en.Location, en.Size, en.MinimumSize, widthdelta, heightdelta);
                    }
                }

                contentpanel.Recalcuate();

                Trigger?.Invoke(logicalname, "Resize", this.callertag);       // pass back the logical name of dialog, Resize, the caller tag
            }
        }

        private void AddEntries(SizeF? factor = null)
        {
            foreach( var ent  in Entries)
            {
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
                c.Location = new Point(ent.Location.X, ent.Location.Y - yoffset);
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
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(((Control)sender).Tag);
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
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }

                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        if (!ProgClose)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Validity:" + s.ToString(), this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
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
                        if (!ProgClose)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        if (!ProgClose)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
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
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
                            Trigger?.Invoke(logicalname, en.Name + ":Return", this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
                        }
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        if (!ProgClose)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
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
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(box.Tag);
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
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(((Control)sender).Tag);
                            Trigger?.Invoke(logicalname, en.Name, this.callertag);       // pass back the logical name of dialog, the name of the control, the caller tag
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
                        if (ctr.Enabled && !ProgClose)
                        {
                            ConfigurableEntryList.Entry en = (ConfigurableEntryList.Entry)(ctr.Tag);
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


        #region Variables

        private System.ComponentModel.IContainer components = null;     // replicate normal component container, so controls which look this
                                                                        // up for finding the tooltip can (TextBoxBorder)

        private Object callertag;
        private string logicalname;

        private bool ProgClose = false;

        private System.Drawing.Point lastpos; // used for dynamically making the list up

        private HorizontalAlignment? halign;
        private ControlHelpersStaticFunc.VerticalAlignment? valign;
        private Size minsize;
        private Size maxsize;
        private Size requestedsize;

        private Panel toppanel;
        private Panel bottompanel;
        private ExtPanelVertScroll contentpanel;
        private ExtPanelVertScrollWithBar vertscrollpanel;
        private ExtButtonDrawn closebutton;
        private Label titlelabel;
        private bool resizerepositionon;
        private Size initialscrollpanelsize;

        private Timer timer;
        private int panelshowcounter;

        private int yoffset;
        private ToolTip tooltipcontrol;

        #endregion
    }
}
