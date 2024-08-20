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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class CheckedIconUserControl : UserControl
    {
        public bool SlideLeft { get; set; } = false;              // if bigger than allowed space, allow slide left
        public bool SlideUp { get; set; } = false;                // if bigger than allowed space, allow slide up
        public bool MultipleColumns { get; set; } = false;        // allow multiple columns
        public bool MultiColumnSlide { get { return MultipleColumns; } set { MultipleColumns = SlideUp = SlideLeft = value; } } // multicolumn with slide
        public Size ScreenMargin { get; set; } = new Size(16, 16);


        // Holds the item list, and various accessors to it
        public List<Item> ItemList { get; private set; }
        public int Count { get { return ItemList.Count; } }
        public int CountGroup { get { return ItemList.Where(x => x.Group).Count(); } }
        public List<Item> GroupOptions() { return ItemList.Where(x => x.Group).ToList(); }
        public List<Item> GroupOptionsWithUserTag() { return ItemList.Where(x => x.Group && x.UserTag != null).ToList(); }
        public List<Item> StandardOptions() { return ItemList.Where(x => !x.Group).ToList(); }
        public List<Item> UserTags(object usertag) { return ItemList.Where(x => x.UserTag == usertag).ToList(); }
        public string[] TagList() { return ItemList.Where(x => !x.Group).Select(x => x.Tag).ToArray(); }        // tags of non group items

        // back colour of picture box
        public override Color BackColor { get => base.BackColor; set { picturebox.FillColor = base.BackColor = value; } }

        // Check box control for check box
        public Color CheckBoxColor { get; set; } = Color.Gray;
        public Color CheckBoxInnerColor { get; set; } = Color.White;
        public Color CheckColor { get; set; } = Color.DarkBlue;
        public Color MouseOverCheckboxColor { get; set; } = Color.CornflowerBlue;
        public Color MouseOverLabelColor { get; set; } = Color.CornflowerBlue;
        public Size CheckBoxSize { get; set; } = Size.Empty;                  // if not set, ImageSize sets the size, or first image, else 24/24
        public float TickBoxReductionRatio { get; set; } = 0.75f;                        // After working out size, reduce by this amount
        public Size ImageSize { get; set; } = Size.Empty;                     // if not set, each image sets its size. 
        public int VerticalSpacing { get; set; } = 3;           // padding..
        public int HorizontalSpacing { get; set; } = 3;

        // Colours for Scroll bar
        public Color BorderColor { get { return sb.BorderColor; } set { sb.BorderColor = value; } }
        public Color SliderColor { get { return sb.SliderColor; } set { sb.SliderColor = value; } }
        public Color ArrowButtonColor { get { return sb.ArrowButtonColor; } set { sb.ArrowButtonColor = value; } }
        public Color ArrowBorderColor { get { return sb.ArrowBorderColor; } set { sb.ArrowBorderColor = value; } }
        public float ArrowUpDrawAngle { get { return sb.ArrowUpDrawAngle; } set { sb.ArrowUpDrawAngle = value; } }
        public float ArrowDownDrawAngle { get { return sb.ArrowDownDrawAngle; } set { sb.ArrowDownDrawAngle = value; } }
        public float ArrowColorScaling { get { return sb.ArrowColorScaling; } set { sb.ArrowColorScaling = value; } }
        public Color ThumbButtonColor { get { return sb.ThumbButtonColor; } set { sb.ThumbButtonColor = value; } }
        public Color ThumbBorderColor { get { return sb.ThumbBorderColor; } set { sb.ThumbBorderColor = value; } }
        public float ThumbDrawAngle { get { return sb.ThumbDrawAngle; } set { sb.ThumbDrawAngle = value; } }
        public float ThumbColorScaling { get { return sb.ThumbColorScaling; } set { sb.ThumbColorScaling = value; } }
        public Color MouseOverButtonColor { get { return sb.MouseOverButtonColor; } set { sb.MouseOverButtonColor = value; } }
        public Color MousePressedButtonColor { get { return sb.MousePressedButtonColor; } set { sb.MousePressedButtonColor = value; } }
        public int LargeChange { get { return sb.LargeChange; } set { sb.LargeChange = value; } }

        // Subform, null if not active
        public CheckedIconNewListBoxForm Subform { get; private set; } = null;

        // check box changed : index, Tag, Text, userTag, change args
        public Action<int, string, string, object, ItemCheckEventArgs> CheckedChanged { get; set; }       // check box changed
        // index, Tag, Text, UserTag, button args
        public Action<int,string,string,object,MouseEventArgs> ButtonPressed { get; set; }

        // what char is used for split settings
        public char SettingsSplittingChar { get; set; } = ';';      

        // default tags for all/none
        public const string All = "All";
        public const string None = "None";

        [System.Diagnostics.DebuggerDisplay("{Tag}:{Text} Grp:{Group} Btn:{Button}")]
        public class Item
        {
            public bool Group { get; set; }
            public string Tag { get; set; }
            public string Text { get; set; }
            public Image Image { get; set; }
            public object UserTag { get; set; }

            // for Group=false, turn off these tags when checked
            // For Group=true, and this is set All, then when returing GetCheckedGroup, and this is checked, return this tag as the setting.
            public string Exclusive { get; set; }       
            // Radio button use, don't allow this item to become unchecked. Group = false.   For radio buttons, set exclusive to All to turn off the other radio buttons
            public bool DisableUncheck { get; set; }
            // Its a button not a check box but a button type label
            public bool Button { get; set; }

            // these are for internal use only

            public ExtPictureBox.Label label;
            public ExtPictureBox.CheckBox checkbox;
            public ExtPictureBox.ImageElement icon;
            public ExtPictureBox.ImageElement submenuicon;

            public Item() { }
            public Item(string tag, string text, Image img = null, string exclusive = null, bool disableuncheck = false, bool button = false, object usertag = null)
            {
                Tag = tag; Text = text; Image = img; Exclusive = exclusive; DisableUncheck = disableuncheck; Button = button;  UserTag = usertag;
            }

            public bool IsSubmenu { get { return UserTag != null && UserTag is SubForm; } }
            public SubForm GetSubForm { get { return UserTag as SubForm; } }
        }

        // the main add
        public void Add(string tag, string text, Image img = null, bool attop = false, string exclusivetags = null,
                                bool disableuncheck = false, bool button = false, object usertag = null, bool group = false)
        {
            var cl = new Item() { Tag = tag, Text = text, Image = img, Exclusive = exclusivetags, DisableUncheck = disableuncheck, Button = button, Group = group, UserTag = usertag};
            cl.label = new ExtPictureBox.Label();
            cl.label.Click += (sender, el, e) =>
            {
                if (cl.label.Enabled)       // will get clicks even if disabled
                {
                    if (cl.checkbox != null && e.Button == MouseButtons.Left)
                    {
                        cl.checkbox.CheckState = cl.checkbox.CheckState == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked;
                        picturebox.Refresh(cl.checkbox.Location);
                    }
                    else
                    {
                        ButtonPressed?.Invoke(ItemList.IndexOf(cl), cl.Tag, cl.Text, cl.UserTag, e);
                    }
                }
            };
            if (!cl.Button)
            {
                cl.checkbox = new ExtPictureBox.CheckBox();
                cl.checkbox.CheckChanged += CheckedIconListBoxForm_CheckedChanged;      // only if enabled
                cl.checkbox.MouseDown += (s, el, e) => { if (e.Button == MouseButtons.Right) ButtonPressed.Invoke(ItemList.IndexOf(cl), cl.Tag, cl.Text, cl.UserTag, e); };
            }
            if (img != null)
            {
                cl.icon = new ExtPictureBox.ImageElement();
                cl.icon.Click += (sender, el, e) =>
                {
                    if (cl.label.Enabled)       // will get clicks even if disabled, use label to tell
                    {
                        if (cl.checkbox != null && e.Button == MouseButtons.Left)
                        {
                            cl.checkbox.CheckState = cl.checkbox.CheckState == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked;
                            picturebox.Refresh(cl.checkbox.Location);
                        }
                        else
                        {
                            ButtonPressed?.Invoke(ItemList.IndexOf(cl), cl.Tag, cl.Text, cl.UserTag, e);
                        }
                    }
                };
            }
            if ( cl.IsSubmenu && cl.GetSubForm.SubmenuIcon != NoSubMenuIcon)        // if submenu, and icon set is null (default) and not NoSubMenuIcon
            {
                cl.submenuicon = new ExtPictureBox.ImageElement();                  // add
            }

            if (group)
            {
                if (attop)
                    ItemList.Insert(0, cl);                // at the top always
                else
                    ItemList.Insert(CountGroup, cl);       // at the end of the last group entry
            }
            else
            {
                if (attop)
                    ItemList.Insert(CountGroup, cl);      // after groups
                else
                    ItemList.Add(cl);                      // at the end
            }
        }

        // a Radio style button
        public void AddRadio(string tag, string text, Image img = null, bool attop = false, object usertag = null)
        {
            Add(tag, text, img, attop, All, true, false, usertag);
        }

        // add as a tuple
        public void Add(Tuple<string, string, Image> ev)
        {
            Add(ev.Item1, ev.Item2, ev.Item3);
        }

        // add options from array/list.
        // If force group is on, item is added as a group item, not a normal item
        // if at top, insert at top all items
        public void Add(IEnumerable<Item> list, bool forcegroup = false, bool attop = false)       
        {
            foreach (var x in list)
                Add(x.Tag, x.Text, x.Image, attop, x.Exclusive, x.DisableUncheck, x.Button,x.UserTag,forcegroup ? true : x.Group);
        }

        // add multiple items as a tuple 
        public void Add(List<Tuple<string, string, Image>> list, bool group = false)
        {
            foreach (var x in list)
                Add(x.Item1, x.Item2, x.Item3,group:group);
        }

        // Add items formatted in a string as name<partssepar>tag<itemssepar>..
        // all stored with the same usertag and image
        // group = true, its a group item
        public void AddStringListDefinitions(string namesidslist, object usertag, bool group = false, Image img = null, char itemssepar = '\u24C2', char partsepar = '\u2713')
        {
            string[] items = namesidslist.Split(itemssepar);
            foreach (var x in items)
            {
                int nsp = x.IndexOf(partsepar);
                if (nsp > 0)
                {
                    var tag = x.Substring(nsp + 1);     // post partssepar
                    var name = x.Substring(0, nsp);     // pre 
                    Add(tag, name, img, group: group, usertag: usertag);
                }
            }
        }

        // given a specific usertag, make a list of name<partssepar>tag<itemssepar>..
        public string GetUserTagDefinitions(object usertag, char itemssepar = '\u24C2', char partsepar = '\u2713')
        {
            string ret = "";
            foreach (var x in ItemList.Where(x=>x.UserTag == usertag))      // for all user tag items
            {
                ret = ret.AppendPrePad($"{x.Text}{partsepar}{x.Tag}", new string(itemssepar, 1));
            }
            return ret;
        }

        // add a button
        public void AddButton(string tag, string text, Image img = null, object usertag = null, bool attop = false)
        {
            Add(tag, text, img, button: true, usertag: usertag, attop: attop);
        }

        // use a long tag (bit field, 1,2,4,8 etc).  Use SettingsStringToLong to convert back
        // if using long as the tag, this is the accumulated long value of all bits
        public long LongConfigurationValue { get; private set; }
        // add a long tag item
        public void Add(long tag, string text, Image img = null, bool attop = false, string exclusivetags = null,
                        bool disableuncheck = false, bool button = false, object usertag = null)
        {
            LongConfigurationValue |= tag;
            Add(tag.ToStringInvariant(), text, img, attop, exclusivetags, disableuncheck, button, usertag);
        }

        public void AddRadio(long tag, string text, Image img = null, bool attop = false, object usertag = null)
        {
            LongConfigurationValue |= tag;
            Add(tag.ToStringInvariant(), text, img, attop, All, true, false, usertag);
        }

        // if using long as the tag, use this to convert the 1;2;4; value back to long
        public static long SettingsStringToLong(string s, char splitchar = ';')
        {
            string[] split = s.SplitNoEmptyStartFinish(splitchar);
            long v = 0;
            foreach (var str in split)
            {
                v |= str.InvariantParseLong(0);
            }
            return v;
        }

        // removing an entry by index. From a form, use ForceRedrawOnNextShow() to make the form update
        public void Remove(int index)
        {
            ItemList.RemoveAt(index);
        }

        // removing an entry by tag. From a form, use ForceRedrawOnNextShow() to make the form update
        public bool Remove(string tag)
        {
            int index = ItemList.FindIndex(x => x.Tag == tag);
            if (index >= 0)
                ItemList.RemoveAt(index);
            return index >= 0;
        }

        // removing entries by usertag. All are removed with this tag.  Returnn number removed.
        // From a form, use ForceRedrawOnNextShow() to make the form update
        public int RemoveUserTags(object usertag)
        {
            var items = ItemList.Where(x => x.UserTag == usertag).ToList();
            foreach (var i in items)
                ItemList.Remove(i);
            return items.Count();
        }

        // Enable item

        public void Enable(int index, bool enable)
        {
            if (ItemList[index].checkbox!=null)
                ItemList[index].checkbox.Enabled = enable;
            ItemList[index].label.Enabled = enable;
        }

        public bool Enable(string tag, bool enable)
        {
            int index = ItemList.FindIndex(x => x.Tag == tag);
            if (index >= 0)
                Enable(index, enable);
            return index >= 0;
        }
        public int EnableByUserTags(object usertag, bool enable)
        {
            var items = ItemList.Where(x => x.UserTag == usertag).ToList();
            foreach (var i in items)
            {
                if ( i.checkbox != null)
                    i.checkbox.Enabled = enable;
                i.label.Enabled = enable;
            }
            return items.Count();
        }

        // sorts non grouped items only
        public void Sort()
        {
            List<Item> std = ItemList.Where(x => !x.Group).ToList();
            ItemList = ItemList.Where(x => x.Group).ToList();
            std.Sort(delegate (Item left, Item right)     // in order, oldest first
            {
                return left.Text.CompareTo(right.Text);
            });

            ItemList.AddRange(std);
        }

        // using SettingsSplittingChar as the separator, respects All or All; as a tag for all
        // Does not obey exclusive. You can set group.
        public void Set(string taglist, bool state = true, int startpos = 0, int end = -1)        
        {
            if (taglist != null)
            {
                var list = taglist.SplitNoEmptyStrings(SettingsSplittingChar);
                if (list.Length >= 1 && list[0] == All)      
                    SetNonGroup(CheckState.Checked,startpos,end);
                else
                    Set(list, state,startpos,end);
            }
        }

        // Set taglist. Does not obey exclusive. You can set group.
        public void Set(IEnumerable<string> taglist, bool state = true, int startpos = 0, int end = -1)
        {
            if (taglist != null)
                Set(taglist, state ? CheckState.Checked : CheckState.Unchecked, startpos , end);
        }

        // Set taglist. Does not obey exclusive. You can set group.
        public void Set(IEnumerable<string> taglist, CheckState state = CheckState.Checked, int startpos = 0, int end = -1)
        {
            if (taglist != null)
            {
                if (end < 0)
                    end = ItemList.Count() - 1;

                for (int i = startpos; i <= end; i++)
                {
                    if (taglist.Contains(ItemList[i].Tag) && ItemList[i].checkbox != null)
                    {
                        //System.Diagnostics.Debug.WriteLine($"Set check {controllist[i].tag} to {state}");
                        ItemList[i].checkbox.CheckState = state;
                    }
                }
            }
        }

        // Set non group on. Does not obey exclusive
        public void SetNonGroup(CheckState state, int startpos = 0, int end = -1)
        {
            if (end < 0)
                end = ItemList.Count() - 1;

            for (int i = startpos; i <= end; i++)  // inclusive
            {
                if (ItemList[i].checkbox != null && ItemList[i].Group == false)
                    ItemList[i].checkbox.CheckState = state;
            }
        }

        // can set any item incl group
        public void Set(int pos, bool state)
        {
            Set(pos, state ? CheckState.Checked : CheckState.Unchecked);
        }
        // from here onwards set
        public void SetFromToEnd(int startpos, bool state, string tagignorelist = "")
        {
            Set(startpos, state ? CheckState.Checked : CheckState.Unchecked, -1, tagignorelist);
        }

        // Set from start to end. Does not obey exclusive. You can set group.
        // end = 0 means just check start. -1 means check all to end
        public void Set(int startpos, CheckState state, int end = 0, string tagignorelist = "")
        {
            if (end == 0)
                end = startpos;
            else if (end < 0)
                end = ItemList.Count() - 1;

            for (int i = startpos; i <= end; i++)  // inclusive
            {
                if (ItemList[i].checkbox != null && !tagignorelist.Contains(ItemList[i].Tag + SettingsSplittingChar))
                    ItemList[i].checkbox.CheckState = state;
            }
        }

        public bool IsChecked(string[] taglist)    // empty array is okay
        {
            if (taglist != null)
            {
                for (int i = 0; i < ItemList.Count; i++)
                {
                    if (taglist.Contains(ItemList[i].Tag) && ItemList[i].checkbox?.Checked == true )
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        // List of options separated by SettingsSplittingChar with  SettingsSplittingChar at end, or
        // All, or None is returned if allornone = true
        // group options are ignored
        // optional list of tags;tags; to be ignored
        public virtual string GetChecked(bool allornone, string tagsignorelist = "")
        {
            string ret = "";

            int total = 0;
            int totalchecks = 0;
            for (int i = 0; i < ItemList.Count; i++)
            {
                // if its an item check box and its not in the ignore list..
                if (ItemList[i].checkbox != null && ItemList[i].Group == false && !tagsignorelist.Contains(ItemList[i].Tag + SettingsSplittingChar))     
                {
                    //System.Diagnostics.Debug.WriteLine($"Tick on {controllist[i].label.Text}");
                    totalchecks++;

                    if (ItemList[i].checkbox.CheckState == CheckState.Checked)    // if its checked
                    {
                        ret += ItemList[i].Tag + SettingsSplittingChar;
                        total++;
                    }
                }
            }

            if (allornone)
            {
                if (total == totalchecks)
                    ret = All;
                else if (ret.Length == 0)
                    ret = None;
            }

            return ret;
        }

        // Class to define a subform of items and a holder for settings
        public class SubForm
        {
            public List<Item> Items { get; set; } = new List<Item>();
            public string Setting { get; set; } = "";
            public Size? ClosedBoundaryRegion { get; set; } = null;     // set to override inheriting parent size
            public bool? AllOrNoneBack { get; set; } = null;            // set to override inheriting parent size
            public Point OffsetPosition { get; set; } = new Point(16, 0);
            public Image SubmenuIcon { get; set; } = null;              // icon used to indicate submenu. If null, use the built in.  If NoSubMenuIcon (see below) then none is printed
        }

        static public Image NoSubMenuIcon = new Bitmap(1, 1);

        // open a submenu on item cl.  Closes any existing one. Prevents double open
        // subform Tag holds cl
        public void OpenSubMenu(Item cl)
        {
            if ( Subform != null && Subform.Tag == cl)      // prevent double open
            {
                //System.Diagnostics.Debug.WriteLine($"Subform already open {cl.Text}");
                return;
            }

            CloseSubMenu();     // close current

            var cinlbf = cl.label.Parent?.Parent?.Parent?.Parent as CheckedIconNewListBoxForm;      // parent form, may be null if not embedded in this

            SubForm sf = cl.GetSubForm;     // this is the info class

            Subform = new CheckedIconNewListBoxForm();      // make a new form

            foreach (var i in sf.Items)             // transfer data from the Subform into the Form.
            {
                Subform.UC.Add(i.Tag, i.Text, i.Image, false, i.Exclusive, i.DisableUncheck, i.Button, i.UserTag, i.Group);
            }

            // copy and set parameters of the Subform from the parent or parent form, or for some from the SF
            Subform.UC.MultipleColumns = MultipleColumns;
            Subform.UC.SlideLeft = SlideLeft;
            Subform.UC.SlideUp = SlideUp;
            Subform.UC.MultiColumnSlide = MultiColumnSlide;
            Subform.UC.ScreenMargin = ScreenMargin;
            Subform.CloseBoundaryRegion = sf.ClosedBoundaryRegion.HasValue ? sf.ClosedBoundaryRegion.Value : cinlbf != null ? cinlbf.CloseBoundaryRegion : new Size(64, 64);
            Subform.AllOrNoneBack = sf.AllOrNoneBack.HasValue ? sf.AllOrNoneBack.Value : cinlbf != null ? cinlbf.AllOrNoneBack : false;
            // other Form parameters (CloseOnDeactivate etc) leave on default

            //var pbsr = pictureboxscroll.PointToScreen(cl.submenuicon?.PositionRight ?? cl.label.PositionRight);    // previous, position right of submenuicon/positionright
            var pbsr = this.PointToScreen(new Point(this.Width, cl.label.Position.Y));  // position to the right of this form
                
            Subform.SetLocation = pbsr;

            if (cinlbf != null) // if we have a CheckedIconNewListBoxForm at its parent, set in submenu mode
            {
                //System.Diagnostics.Debug.WriteLine($"OpenSubMenu set submenu on for {cinlbf.Name}");
                cinlbf.SetSubMenuActive(true);              // submenu active informs form not to do anything on deactivation
                Subform.CloseDownTime = cinlbf.CloseDownTime;
            }

            Subform.Name = Parent.Name + "_Subform";

            Subform.UC.ButtonPressed += (index, stag, text, utag, bev) =>  // sub buttons reflected to our button press
            {
                //System.Diagnostics.Debug.WriteLine($"Sub form button pressed {index} {stag} {text}");
                ButtonPressed?.Invoke(index, stag, text, utag, bev);
            };

            Subform.SaveSettings += (s, p) =>       // form close save settings update the subform settings value
            {
                //System.Diagnostics.Debug.WriteLine($"*** Save Settings {Subform.Name} {s} {p} was {sf.Setting}");       
                sf.Setting = s;
            };

            //System.Diagnostics.Debug.WriteLine($"OpenSubMenu for {cl.Text} {cl.Tag} set {sf.Setting}");

            //System.Diagnostics.Debug.WriteLine($"Subform open {cl.Text}");

            Subform.Show(sf.Setting, pbsr, cinlbf,cl);
        }

        // close submenu
        public void CloseSubMenu()
        {
            if (Subform != null)
            {
                //System.Diagnostics.Debug.WriteLine($"Subform close {(Subform.Tag as Item).Text}");
                Subform?.Close();
                Subform = null;
            }
        }

        public CheckedIconUserControl()
        {
            ItemList = new List<Item>();
            picturebox = new ExtPictureBox();
            sb = new ExtScrollBar();
            pictureboxscroll = new ExtPictureBoxScroll();
            pictureboxscroll.BorderStyle = BorderStyle.FixedSingle;
            pictureboxscroll.Controls.Add(picturebox);
            pictureboxscroll.Controls.Add(sb);
            pictureboxscroll.Dock = DockStyle.Fill;
            picturebox.EnterElement += Picturebox_EnterElement;
            sb.HideScrollBar = true;
            Controls.Add(pictureboxscroll);
        }

        // Render Options into control
        // give preferred xy, and optionally a fixed size. 
        // fixed size means we can't shift or change size, and it must fit withing that box.
        //                  preferredxy is redundant then and should be set to 0,0. FitToScreen is ignored. Just returns 0,0,width,height
        // with fixed size null, it tries to use up as much screen space right of preferredxy if MultipleColumns is set
        //                  If SlideLeft is set, we can move left to make more space
        //                  the returned rectangle is clipped to the screen space
        //                  returns xy to place, and client size (not window size)
        public virtual Rectangle Render(Point preferredxy, Size? fixedsize = null)
        {
            // work out icon size default

            Size firsticonsize = ImageSize.IsEmpty ? new Size(24, 24) : ImageSize;            // if we have an image size, the default size

            if (ImageSize.IsEmpty)      // if empty, the first icon will be used
            {
                foreach (var ce in ItemList)
                {
                    if (ce.Image != null)                  // find first non null and use for defsize
                    {
                        firsticonsize = ce.Image.Size;
                        break;
                    }
                }
            }

            Size chkboxsize = CheckBoxSize.IsEmpty ? firsticonsize : CheckBoxSize;
            chkboxsize = new Size(Math.Max(4, chkboxsize.Width), Math.Max(4, chkboxsize.Height));

            int maxwidthsinglecol = 0;
            int vheightsinglecol = VerticalSpacing;
            List<Tuple<Rectangle, Rectangle, Rectangle>> positions = new List<Tuple<Rectangle, Rectangle, Rectangle>>();

            // calculate positions in single column into an array, calculate maxwidthsignelcol, calculate vheightsinglecol

            picturebox.ClearImageList();
    
            bool hasacheckbox = ItemList.Where(x => x.Button == false).Count() > 0;     // do we have any checkboxes, if so, we will have to reserve space

            // set up each item, and work out sizes

            for (int i = 0; i < ItemList.Count; i++)
            {
                var cl = ItemList[i];

                int fonth = (int)this.Font.GetHeight() + 2; // a little extra for label

                Size iconsize = ImageSize.IsEmpty ? (cl.Image?.Size ?? new Size(0, 0)) : ImageSize;
                int vspacing = Math.Max(fonth, iconsize.Height);
                vspacing = Math.Max(vspacing, chkboxsize.Height);       // vspacing is the max of label, icon and checkbox

                int chkx = HorizontalSpacing;
                int imgx = hasacheckbox ? chkx + chkboxsize.Width + HorizontalSpacing : chkx;
                int labx = cl.Image != null ? imgx + iconsize.Width + HorizontalSpacing : imgx;

                // label is in autosize mode, setting Text and Font will size it
                cl.label.Text = cl.Text;
                cl.label.Font = this.Font;
                cl.label.ForeColor = this.ForeColor;
                cl.label.BackColor = BackColor;
                cl.label.MouseOverBackColor = this.MouseOverLabelColor;
                cl.label.Tag = i;       // tags are index

                if (!cl.Button)
                {
                    cl.checkbox.BackColor = this.BackColor;
                    cl.checkbox.CheckBoxColor = this.CheckBoxColor;
                    cl.checkbox.CheckBoxInnerColor = this.CheckBoxInnerColor;
                    cl.checkbox.CheckColor = this.CheckColor;
                    cl.checkbox.MouseOverColor = this.MouseOverCheckboxColor;
                    cl.checkbox.TickBoxReductionRatio = TickBoxReductionRatio;
                    cl.checkbox.Font = Font;
                    cl.checkbox.Tag = i;        // store index of control when displayed
                }

                if (cl.Image != null)
                {
                    cl.icon.Image = cl.Image;
                    cl.icon.Tag = i;        // tags are index
                }

                if (cl.submenuicon != null)
                {
                    cl.submenuicon.Image = cl.GetSubForm.SubmenuIcon ?? Properties.Resources.ArrowRightSmall;
                    cl.submenuicon.Tag = i;        // store index of control when displayed
                }

                // Y is not holding Y position. Only use for Y is to record vspacing on first entry only, see below for vpositioning
                // Item1 holds checkbox left, vspacing, and checkbox size
                // Item2 holds image left, icon size
                // Item3 holds label left, label size
                var pos = new Tuple<Rectangle, Rectangle, Rectangle>(
                                new Rectangle(chkx, vspacing, chkboxsize.Width, chkboxsize.Height),
                                new Rectangle(imgx, 0, iconsize.Width, iconsize.Height),
                                new Rectangle(labx, 0, cl.label.Size.Width, cl.label.Size.Height));


                positions.Add(pos);

                vheightsinglecol += vspacing + VerticalSpacing;

                maxwidthsinglecol = Math.Max(maxwidthsinglecol, labx + cl.label.Size.Width);
            }

            // we now work out an estimated columns to display

            int estcolstodisplay = 1;       // default is one
            bool checkvspacing = false;     // set if we can allow vspacing overflow check to work, only if we are less than cols on screen, so we can afford another column

            // find screen for non fixedsize
            Rectangle screen = Screen.FromPoint(preferredxy).WorkingArea;
            //System.Diagnostics.Debug.WriteLine($"Render at {preferredxy} screensize {screen} icon {firsticonsize} chkbox {chkboxsize} ");

            // work out available pixels left and down. Or if fixedsize, its those pixels
            int vavailable = fixedsize != null ? fixedsize.Value.Height : screen.Y + screen.Height - ScreenMargin.Width - preferredxy.Y;
            int wavailable = fixedsize != null ? fixedsize.Value.Width : screen.X + screen.Width - ScreenMargin.Height - preferredxy.X;

            //System.Diagnostics.Debug.WriteLine($".. max w single col {maxwidthsinglecol} total vert height {vheightsinglecol} wavailable {wavailable} vavailable {vavailable} Screenmargin {ScreenMargin}");

            bool slidup = false;
            if (MultipleColumns && vheightsinglecol >= vavailable)               // if we have multiple columns, and we need them
            {
                int estcolsneeded = (int)Math.Ceiling((double)vheightsinglecol / vavailable);        // this is how many we need with the vheight we have
                int pixelsright = wavailable - pictureboxscroll.ScrollBarWidth; // pixels for the controls, excluding the scroll bar
                int colstoright = pixelsright / maxwidthsinglecol;      // number we can do to the right

                //System.Diagnostics.Debug.WriteLine($".. multicol pixels to right {pixelsright} col width {maxwidthsinglecol} needed cols {estcolsneeded} colrtoright {colstoright}");

                if (estcolsneeded < colstoright)      // if we have estimated less than cols available to the right, its good and can allow for 1 cols growth
                {
                    estcolstodisplay = estcolsneeded;   // we can go for these cols needed
                    checkvspacing = true;           // we can allow an overflow into the next column
                }
                else if (estcolsneeded == colstoright)      // if we have estimated exactly, we can go, but no creeping over to the next column
                {
                    estcolstodisplay = estcolsneeded;   // we can go for these cols needed, with no checkvspacing
                }
                else if (SlideLeft && fixedsize == null)    // if we want to slide left to accomodate. Can't do that on fixed size
                {
                    int maxwidth = screen.Width - ScreenMargin.Width * 2 - pictureboxscroll.ScrollBarWidth - 16;       // 16 is to give a bit
                    int colsonscreen = maxwidth / maxwidthsinglecol;       // this is maximum which the screen can support

                    //System.Diagnostics.Debug.WriteLine($".. slideleft width {maxwidth} giving {colsonscreen} cols");
                    if ( estcolsneeded <= colsonscreen)     // if we have enough cols
                    {
                        estcolstodisplay = estcolsneeded;   // we give them all 
                    }
                    else
                    {
                        if (SlideUp)
                        {
                            vavailable = screen.Height - 2 * ScreenMargin.Height;       // give all available
                            estcolsneeded = (int)Math.Ceiling((double)vheightsinglecol / vavailable);        // this is how many we need with the vheight we have
                            estcolstodisplay = Math.Min(estcolsneeded, colsonscreen);
                            slidup = true;
                            //System.Diagnostics.Debug.WriteLine($".. slideup max v available {vavailable} giving cols {estcolsneeded} needed, estcolstodisplay {estcolstodisplay}");
                        }
                        else
                            estcolstodisplay = colsonscreen;
                    }

                    checkvspacing = estcolstodisplay < colsonscreen;          // if we have more cols that estimated, we can afford an overflow due to vspacing 

                    //System.Diagnostics.Debug.WriteLine($".. fittoscreen width available {maxwidth} giving cols {colsonscreen} columns available, cols needed {estcolsneeded} vspacingallowed {checkvspacing}");
                }
                else
                {
                    estcolstodisplay = colstoright;        // settle on colstoright, no vspacing check, use scroll bar
                }
            }

            int itemspercolumn = (int)Math.Ceiling((double)ItemList.Count / estcolstodisplay);  // floating point divide with rounding up

            //System.Diagnostics.Debug.WriteLine($".. decided Cols {estcolstodisplay} allow vspacing {checkvspacing} items/col {itemspercolumn}");

            // try and make it as boxy as can be by estimating the number of items per column

            int colindex = 0;
            int colused = 1;        // actually displayed
            int vpos = VerticalSpacing;
            int heightrequired = 0;     // maximum we got to..

            picturebox.ClearImageList();

            int labelwidthmax = positions.Select(x => x.Item3.Width).Max();       // max width of labels

            for (int i = 0; i < ItemList.Count; i++)     // now turn them on, once all are in position
            {
                var cl = ItemList[i];
                var post = positions[i];

                int vspacing = post.Item1.Y;

                // if allowed, and we display any part of this over vavailable
                // or we have reached the depth..
                if ((checkvspacing && vpos + vspacing >= vavailable) || colindex++ == itemspercolumn)
                {
                    colused++;
                    vpos = VerticalSpacing;
                    colindex = 1;
                }

                int vcentre = vpos + vspacing / 2;
                int colx = (colused - 1) * maxwidthsinglecol;

                //System.Diagnostics.Debug.WriteLine($" {cl.label.Text} = {post.Item1} : {post.Item2} : {post.Item3} @ {colx} {vpos}");

                if (cl.checkbox != null)
                {
                    cl.checkbox.Location = new Rectangle(post.Item1.X + colx, vcentre - post.Item1.Height / 2, post.Item1.Width, post.Item1.Height);
                    picturebox.Add(cl.checkbox);
                }
                if (cl.icon != null)
                {
                    cl.icon.Location = new Rectangle(post.Item2.X + colx, vcentre - post.Item2.Height / 2, post.Item2.Width, post.Item2.Height);
                    picturebox.Add(cl.icon);
                }

                cl.label.Location = new Rectangle(post.Item3.X + colx, vcentre - post.Item3.Height / 2, post.Item3.Width, post.Item3.Height);
                picturebox.Add(cl.label);

                if (cl.submenuicon != null)
                {
                    cl.submenuicon.Location = new Rectangle(post.Item3.X + labelwidthmax, cl.label.Location.Y, post.Item1.Width, post.Item1.Height);
                    picturebox.Add(cl.submenuicon);
                }

                vpos += vspacing + VerticalSpacing; 

                heightrequired = Math.Max(heightrequired, vpos);
            }

            picturebox.Render();

            if (fixedsize == null)      // if not in fixed size mode, return the screen rectangle clipping to size.
            {
                // this will slide left if required to fit the box into the screen
                int bordermargin = pictureboxscroll.BorderStyle == BorderStyle.FixedSingle ? 2 : 0;
                var rect = preferredxy.CalculateRectangleWithinScreen(picturebox.Image.Width + sb.Width + bordermargin, picturebox.Image.Height + bordermargin, !slidup, ScreenMargin);
                return rect;
            }
            else
            {
                return new Rectangle(new Point(0, 0), new Size(picturebox.Image.Width + sb.Width, picturebox.Image.Height)); // just return size, probably not used
            }
        }

        #region Implementation

        private void CheckedIconListBoxForm_CheckedChanged(ExtPictureBox.CheckBox cb)
        {
            int index = (int)cb.Tag;

            if (ItemList[index].DisableUncheck && cb.Checked == false) // if uncheckable, then set it back. Used in radio button situations
            {
                cb.Checked = true;
            }

            if (cb.Checked && ItemList[index].Exclusive!= null)      // if checking, and we have tags which must be turned off
            {
                if (ItemList[index].Exclusive.Equals(All))     // turn off all but us
                {
                    for (int ix = 0; ix < ItemList.Count; ix++)
                    {
                        if (ItemList[ix].checkbox != null && ix != index && ItemList[ix].Tag != All && ItemList[ix].Tag != None)
                            ItemList[ix].checkbox.Checked = false;
                    }
                }
                else
                {
                    string[] tags = ItemList[index].Exclusive.Split(SettingsSplittingChar);
                    foreach (string s in tags)
                    {
                        //System.Diagnostics.Debug.WriteLine($"Tag {controllist[index].tag} turn off {s}");
                        int offindex = ItemList.FindIndex(x => x.Tag == s);
                        if (offindex >= 0)
                            ItemList[offindex].checkbox.Checked = false;
                    }
                }
            }

            var prevstate = cb.Checked ? CheckState.Unchecked : CheckState.Checked;

            ItemCheckEventArgs eventargs = new ItemCheckEventArgs(index, cb.CheckState, prevstate);
            CheckChangedEvent(cb, eventargs);       // derived classes first.
            CheckedChanged?.Invoke(index,ItemList[index].Tag, ItemList[index].Text, ItemList[index].UserTag, eventargs);
        }

        // operate subform popups on entering a element
        private void Picturebox_EnterElement(object sender, MouseEventArgs eventargs, ExtPictureBox.ImageElement i, object tag)
        {
            Item cl = ItemList[(int)i.Tag];     // all elements have their tags set to index in ItemList
            //System.Diagnostics.Debug.WriteLine($"Enter element {cl.Text}");
            if (cl.IsSubmenu)
                OpenSubMenu(cl);
            else
                CloseSubMenu();
        }

        protected virtual void CheckChangedEvent(ExtPictureBox.CheckBox cb, ItemCheckEventArgs e) { }

        private ExtPictureBoxScroll pictureboxscroll;
        private ExtPictureBox picturebox;
        private ExtScrollBar sb;

        #endregion
    }
}


