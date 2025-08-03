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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class CheckedIconUserControl : UserControl, IThemeable
    {
        public bool SlideLeft { get; set; } = false;              // if bigger than allowed space, allow slide left
        public bool SlideUp { get; set; } = false;                // if bigger than allowed space, allow slide up
        public bool MultipleColumns { get; set; } = false;        // allow multiple columns
        public bool MultiColumnSlide { get { return MultipleColumns; } set { MultipleColumns = SlideUp = SlideLeft = value; } } // multicolumn with slide
        public Size ScreenMargin { get; set; } = new Size(16, 16);
        public bool ShowClose { get { return closeicon.Visible; } set { closeicon.Visible = value; Invalidate(); } }        // show or hide (default) close icon above scroll bar

        // Holds the item list, and various accessors to it
        public List<Item> ItemList { get; private set; }
        public int Count { get { return ItemList.Count; } }
        public int CountGroup { get { return ItemList.Where(x => x.Group).Count(); } }
        
        // maximum number of check boxes across
        public int MaxRadioColumns()
        {
            var checkboxes = ItemList.Where(x => x.Button == false);
            int maxcheckboxes = checkboxes.Count() > 0 ? checkboxes.Select(x => x.checkbox.Length).Max() : 0;
            return maxcheckboxes;
        }

        // all group options of a checkbox column
        public List<Item> GroupOptions(int checkbox) { return ItemList.Where(x => x.Group && x.CheckBoxExists(checkbox)).ToList(); }

        // User tag list, all entries, where usertag matches
        public List<Item> UserTags(object usertag) { return ItemList.Where(x => x.UserTag == usertag).ToList(); }
        // Tag list of all non group items
        public string[] TagList() { return ItemList.Where(x => !x.Group).Select(x => x.Tag).ToArray(); }       

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

        // Colours and controls for Scroll bar
        public Color BorderColor { get { return sb.BorderColor; } set { sb.BorderColor = value; } }
        public Color SliderColor { get { return sb.SliderColor; } set { sb.SliderColor = value; } }
        public Color SliderColor2 { get { return sb.SliderColor2; } set { sb.SliderColor2 = value; } }
        public float SliderDrawAngle { get { return sb.SliderDrawAngle; } set { sb.SliderDrawAngle = value; } }
        public Color ArrowButtonColor { get { return sb.ArrowButtonColor; } set { sb.ArrowButtonColor = value; } }
        public Color ArrowButtonColor2 { get { return sb.ArrowButtonColor2; } set { sb.ArrowButtonColor2 = value; } }
        public Color ArrowBorderColor { get { return sb.ArrowBorderColor; } set { sb.ArrowBorderColor = value; } }
        public float ArrowUpDrawAngle { get { return sb.ArrowUpDrawAngle; } set { sb.ArrowUpDrawAngle = value; } }
        public float ArrowDownDrawAngle { get { return sb.ArrowDownDrawAngle; } set { sb.ArrowDownDrawAngle = value; } }
        public Color ThumbButtonColor { get { return sb.ThumbButtonColor; } set { sb.ThumbButtonColor = value; } }
        public Color ThumbButtonColor2 { get { return sb.ThumbButtonColor2; } set { sb.ThumbButtonColor2 = value; } }
        public Color ThumbBorderColor { get { return sb.ThumbBorderColor; } set { sb.ThumbBorderColor = value; } }
        public float ThumbDrawAngle { get { return sb.ThumbDrawAngle; } set { sb.ThumbDrawAngle = value; } }
        public Color MouseOverButtonColor { get { return sb.MouseOverButtonColor; } set { sb.MouseOverButtonColor = value; } }
        public Color MouseOverButtonColor2 { get { return sb.MouseOverButtonColor2; } set { sb.MouseOverButtonColor2 = value; } }
        public Color MousePressedButtonColor { get { return sb.MousePressedButtonColor; } set { sb.MousePressedButtonColor = value; } }
        public Color MousePressedButtonColor2 { get { return sb.MousePressedButtonColor2; } set { sb.MousePressedButtonColor2 = value; } }
        public int LargeChange { get { return sb.LargeChange; } set { sb.LargeChange = value; } }

        // Subform, null if not active
        public CheckedIconNewListBoxForm Subform { get; private set; } = null;

        // check box changed : index, checkbox, Tag, Text, userTag, change args
        public Action<int, int, string, string, object, ItemCheckEventArgs> CheckedChanged { get; set; }       // check box changed
        // index, Tag, Text, UserTag, button args
        public Action<int,string,string,object,MouseEventArgs> ButtonPressed { get; set; }

        // On close icon click, callback
        public Action<CheckedIconUserControl> CloseClicked { get; set; }

        // what char is used for split settings
        public char SettingsSplittingChar { get; set; } = ';';      

        // default tags for all/none
        public const string All = "All";
        public const string None = "None";

        [System.Diagnostics.DebuggerDisplay("{Tag}:{Text} Grp:{Group} Btn:{Button} Exc:{Exclusive}")]
        public class Item
        {
            public bool Group { get; set; }
            public string Tag { get; set; }
            public string Text { get; set; }
            public Image Image { get; set; }
            public object UserTag { get; set; }

            // For non group buttons:
            //      For radio buttons: set DisableUncheck=true and set exclusive to All
            //          Turns off the other non group checkboxes which have Exclusive not null and disabledunchecked true
            //      For radio buttons: set DisableUncheck=true and set exclusive to all tag names like "R1;R2;R3"
            //          Turns off the other non group checkboxes with these tags. Allows multiple radio sets
            //      For general buttons which don't want others set: Set exclusive to list of tags to turn off
            //
            // For group buttons:
            //      If Exclusive=All, then all other groups/items will be turned off. Any other groups with the same tag list will be turned on
            //      other values of exclusive not supported

            // Don't allow this item to become unchecked. Radio Button use mostly
            public bool DisableUncheck { get; set; }
            // Exclusive list, which determines others to turn off
            public string Exclusive { get; set; }       
            
            // Its a button not a check box but a button type label
            public bool Button { get; set; }

            // these are for internal use only

            public ExtPictureBox.Label label;
            public ExtPictureBox.CheckBox[] checkbox;
            public ExtPictureBox.ImageElement icon;
            public ExtPictureBox.ImageElement submenuicon;

            public Item() { }
            public Item(string tag, string text, Image img = null, string exclusive = null, bool disableuncheck = false, bool button = false, object usertag = null)
            {
                Tag = tag; Text = text; Image = img; Exclusive = exclusive; DisableUncheck = disableuncheck; Button = button;  UserTag = usertag;
            }

            public bool IsSubmenu { get { return UserTag != null && UserTag is SubForm; } }
            public SubForm GetSubForm { get { return UserTag as SubForm; } }

            public bool CheckBoxExists(int i) { return checkbox != null && i < checkbox.Length && checkbox[i] != null; }

            // an exclusive group option turns off all other groups and all items (disabled is an example)
            public bool ExclusiveGroupOption { get { return Group && (Exclusive?.Equals(All)??false); } }

            // Create a UC item from a string
            // Description is Tag,Text [,ImagePath (empty string, File, Resource) [,Exclusive list [,type]]]
            // type = Group,Button,CheckBox (case insensitive)
            public Item(string description)
            {
                var sp = new StringParser(description);
                Create(sp);
            }

            // Create a Item based on the text format (see action doc for DropDownButton)
            public static Item Create(StringParser sp)
            {
                Item t = new Item();
                t.Tag = sp.NextQuotedWordComma();
                t.Text = sp.NextQuotedWord(" ,)");

                if (sp.IsCharMoveOn(','))
                {
                    string imgpath = sp.NextQuotedWord(" ,)");
                    if (imgpath.HasChars())
                    {
                        if (System.IO.File.Exists(imgpath))
                        {
                            try
                            {
                                t.Image = imgpath.LoadBitmapNoLock();       // So the file can be released
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Trace.WriteLine($"CheckedIconPictureBoxUC Image {imgpath} failed to load {ex}");
                            }
                        }
                        else
                        {
                            t.Image = BaseUtils.ResourceHelpers.GetResourceAsImage(imgpath);
                            if (t.Image == null)
                                System.Diagnostics.Trace.WriteLine($"CheckedIconPictureBoxUC Image {imgpath} failed to load");
                        }
                    }

                    if (sp.IsCharMoveOn(','))
                    {
                        string exc = sp.NextQuotedWord(" ,)");
                        if (exc.HasChars())
                            t.Exclusive = exc;

                        if (sp.IsCharMoveOn(','))
                        {
                            string type = sp.NextQuotedWord(" )");
                            if (type.EqualsIIC("Group"))
                                t.Group = true;
                            else if (type.EqualsIIC("Button"))
                                t.Button = true;
                            else if (type.EqualsIIC("CheckBox"))        // default, there for completeness.
                                t.Button = false;
                        }
                    }
                }

                return t.Tag!=null && t.Text!=null ? t: null;
            }
        }

        // the main add with many options
        public void Add(    string tag,                         // logical tag of item
                            string text,                        // text for label
                            Image img = null,                   // image to display, optional
                            bool attop = false,                 // enter at top of ItemList
                            string exclusivetags = null,        // set exclusive tags on group or normal entries
                            bool disableuncheck = false,        // for use by radio buttons
                            bool button = false,                // display as button not checkbox
                            object usertag = null,              // user tag assigned
                            bool group = false,                 // group or normal item
                            int checkmap = 1,                   // bitmap of check columns to use (1,3,5 etc)
                            string[] checkbuttontooltiptext = null, string icontooltiptext = null, string labeltooltiptext = null)      // tooltips
        {
            var cl = new Item() { Tag = tag, Text = text, Image = img, Exclusive = exclusivetags, DisableUncheck = disableuncheck, Button = button, Group = group, UserTag = usertag};
            cl.label = new ExtPictureBox.Label();
            cl.label.ToolTipText = labeltooltiptext;
            cl.label.Click += (sender, el, e) =>
            {
                if (cl.label.Enabled)       // only if enabled
                {
                    if (cl.checkbox != null && e.Button == MouseButtons.Left)
                    {
                        cl.checkbox[0].CheckState = cl.checkbox[0].CheckState == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked;
                        picturebox.Refresh(cl.checkbox[0].Location);
                    }
                    else
                    {
                        ButtonPressed?.Invoke(ItemList.IndexOf(cl), cl.Tag, cl.Text, cl.UserTag, e);
                    }
                }
            };
            if (!cl.Button)
            {
                int lastbit = checkmap.RightMostBit();
                cl.checkbox = new ExtPictureBox.CheckBox[lastbit+1];
                for(int i = 0; i < cl.checkbox.Length; i++)
                {
                    if ((checkmap & (1 << i)) != 0)
                    {
                        var x = new ExtPictureBox.CheckBox();
                        if (checkbuttontooltiptext != null)
                            x.ToolTipText = checkbuttontooltiptext[i];
                        cl.checkbox[i] = x;
                        x.CheckChanged += CheckedIconListBoxForm_CheckedChanged;      // only if enabled
                        x.MouseDown += (s, el, e) =>
                        {
                            if (e.Button == MouseButtons.Right && x.Enabled)
                                ButtonPressed.Invoke(ItemList.IndexOf(cl), cl.Tag, cl.Text, cl.UserTag, e);
                        };
                    }
                }
            }
            if (img != null)
            {
                cl.icon = new ExtPictureBox.ImageElement();
                cl.icon.ToolTipText = icontooltiptext;
                cl.icon.Click += (sender, el, e) =>
                {
                    if (cl.icon.Enabled)    
                    {
                        if (cl.checkbox != null && e.Button == MouseButtons.Left)
                        {
                            cl.checkbox[0].CheckState = cl.checkbox[0].CheckState == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked;
                            picturebox.Refresh(cl.checkbox[0].Location);
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

        // a Radio style button (Exclusive=All, disableuncheck=true)
        public void AddRadio(string tag, string text, Image img = null, bool attop = false, object usertag = null, 
                                int checkmap = 1, string[] checkbuttontooltiptext = null, string icontooltiptext = null, string labeltooltiptext = null)
        {
            Add(tag, text, img, attop, All, true, false, usertag, false, checkmap, checkbuttontooltiptext,icontooltiptext,labeltooltiptext );
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
            foreach (var x in ItemList.Where(x=>x.UserTag != null && x.UserTag.Equals(usertag)))      // for all user tag items
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
            Item i = ItemList[index];
            if (i.checkbox != null)
            {
                foreach (var x in i.checkbox)
                {
                    if (x != null)
                        x.Enabled = enable;
                }
            }
            if (i.icon != null)
                i.icon.Enabled = enable;
            if (i.submenuicon != null)
                i.submenuicon.Enabled = enable;
            i.label.Enabled = enable;
        }

        public void ShowDisabled(int index, bool showdisabled)
        {
            Item i = ItemList[index];
            if (i.checkbox != null)
            {
                foreach (var x in i.checkbox)
                {
                    if (x != null)
                        x.ShowDisabled = showdisabled;
                }
            }
            if (i.icon != null)
                i.icon.ShowDisabled = showdisabled;
            if (i.submenuicon != null)
                i.submenuicon.ShowDisabled = showdisabled;
            i.label.ShowDisabled = showdisabled;
        }

        public bool Enable(string tag, bool enable)
        {
            int index = ItemList.FindIndex(x => x.Tag == tag);
            if (index >= 0)
                Enable(index, enable);
            return index >= 0;
        }
        public bool ShowDisabled(string tag, bool showdisabled)
        {
            int index = ItemList.FindIndex(x => x.Tag == tag);
            if (index >= 0)
                ShowDisabled(index, showdisabled);
            return index >= 0;
        }
        public int EnableByUserTags(object usertag, bool enable)
        {
            var items = ItemList.Where(x => x.UserTag == usertag).ToList();
            foreach (var i in items)
            {
                if (i.checkbox != null)
                {
                    foreach (var x in i.checkbox)
                    {
                        if ( x != null)
                            x.Enabled = enable;
                    }
                }
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

        // using SettingsSplittingChar as the separator, see next function for more info
        public void Set(string taglist, bool state = true, int checkbox = 0)        
        {
            if (taglist != null)
            {
                var list = taglist.SplitNoEmptyStrings(SettingsSplittingChar);
                Set(list, state ? CheckState.Checked : CheckState.Unchecked, checkbox);
            }
        }

        // Set taglist. if taglist length =1 and first entry is All then sets all but group options on
        // Does not execute exclusive list
        // Does not apply group list
        public void Set(IEnumerable<string> taglist, CheckState state = CheckState.Checked, int checkbox = 0)
        {
            if (taglist != null)
            {
                List<string> tagstoset = taglist.ToList();
                bool alloption = tagstoset.Count == 1 && tagstoset[0] == All;

                foreach(var x in ItemList)
                {
                    if (x.CheckBoxExists(checkbox) && (taglist.Contains(x.Tag) || (alloption && !x.Group) ))
                    {
                        //System.Diagnostics.Debug.WriteLine($"Set check {controllist[i].tag} to {state}");
                        x.checkbox[checkbox].CheckState = state;
                    }
                }
            }
        }

        // can set any item incl group
        public void Set(int pos, bool state, int checkbox = 0)
        {
            Set(pos, state ? CheckState.Checked : CheckState.Unchecked,checkbox:checkbox);
        }

        // Set from start to end. Does not obey exclusive. You can set group.
        // end = 0 means just check start. -1 means check all to end
        public void Set(int startpos, CheckState state, int end = 0, string tagignorelist = "", int checkbox = 0, bool ignoregroup = false)
        {
            if (end == 0)
                end = startpos;
            else if (end < 0)
                end = ItemList.Count() - 1;

            for (int i = startpos; i <= end; i++)  // inclusive
            {
                if (ItemList[i].CheckBoxExists(checkbox) && !tagignorelist.Contains(ItemList[i].Tag + SettingsSplittingChar) &&
                        (ignoregroup == false || !ItemList[i].Group))
                {
                    ItemList[i].checkbox[checkbox].CheckState = state;
                }
            }
        }

        public bool IsChecked(string[] taglist, int checkbox = 0)    // empty array is okay
        {
            if (taglist != null)
            {
                for (int i = 0; i < ItemList.Count; i++)
                {
                    if (taglist.Contains(ItemList[i].Tag) && ItemList[i].CheckBoxExists(checkbox) && ItemList[i].checkbox?[checkbox].Checked == true )
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
        public virtual string GetChecked(bool allornone, string tagsignorelist = "", int checkbox = 0)
        {
            string ret = "";

            int total = 0;
            int totalchecks = 0;
            for (int i = 0; i < ItemList.Count; i++)
            {
                // if its an item check box and its not in the ignore list..
                if (ItemList[i].CheckBoxExists(checkbox) && ItemList[i].Group == false && !tagsignorelist.Contains(ItemList[i].Tag + SettingsSplittingChar))     
                {
                    //System.Diagnostics.Debug.WriteLine($"Tick on {controllist[i].label.Text}");
                    totalchecks++;

                    if (ItemList[i].checkbox[checkbox].CheckState == CheckState.Checked)    // if its checked
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

            var cinlbf = cl.label.Parent?.Parent?.Parent?.Parent as CheckedIconNewListBoxForm;      // parent form, may be null if not embedded in this
            SubForm sf = cl.GetSubForm;     // this is the info class

            //System.Diagnostics.Debug.WriteLine($"\r\nRequest OpenSubMenu for {cl.Text} {cl.Tag} set {sf.Setting}");

            CloseSubMenu();     // close current

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

            var pbsr = pictureboxscroll.PointToScreen(cl.submenuicon?.PositionRight ?? cl.label.PositionRight);    // previous, position right of submenuicon/positionright
            //var pbsr = this.PointToScreen(new Point(this.Width, cl.label.Position.Y));  // position to the right of this form
                
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

            for (int i = 0; i < ItemList.Count; i++)        // for all entries, except this, we set it to look disabled (though its not)
            {
                if (ItemList[i] != cl)
                    ShowDisabled(i, true);
            }

            // System.Diagnostics.Debug.WriteLine($"Subform show for {cl.Text} {cl.Tag} set {sf.Setting}");

            Subform.Show(sf.Setting, pbsr, cinlbf,cl);
        }

        // close submenu
        public void CloseSubMenu()
        {
            if (Subform != null)
            {
                //System.Diagnostics.Debug.WriteLine($"Request subform close {(Subform.Tag as Item).Text}");

                Subform?.Close();
                Subform = null;

                for (int i = 0; i < ItemList.Count; i++)        // we release the show disabled from all of them
                {
                    ShowDisabled(i, false);
                }
            }
        }

        // CONSTRUCTOR
        public CheckedIconUserControl()
        {
            ItemList = new List<Item>();
            
            // make items for pictureboxscroll
            picturebox = new ExtPictureBox();
            
            sb = new ExtScrollBar();
            sb.HideScrollBar = true;    // hide scroll bar

            closeicon = new ExtButtonDrawn();
            closeicon.Visible = false;      // default off
            closeicon.ImageSelected = ExtButtonDrawn.ImageType.Close;
            closeicon.Click += (s, e) => { CloseClicked?.Invoke(this); };

            // make pbs
            pictureboxscroll = new ExtPictureBoxScroll();
            pictureboxscroll.BorderStyle = BorderStyle.FixedSingle;
            pictureboxscroll.Controls.Add(picturebox);
            pictureboxscroll.Controls.Add(sb);
            pictureboxscroll.Controls.Add(closeicon);
            pictureboxscroll.Dock = DockStyle.Fill;
            picturebox.EnterElement += Picturebox_EnterElement;
            Controls.Add(pictureboxscroll);
        }

        // Render Options into control
        // give preferred xy, and optionally a fixed size. 
        // fixed size means we can't shift or change size, and it must fit withing that box.
        //                  preferredxy is redundant then and should be set to 0,0. FitToScreen is ignored. Just returns 0,0,width,height
        // with fixed size null, it tries to use up as much screen space right of preferredxy if MultipleColumns is set
        //                  If SlideLeft is set, we can move left to make more space
        //                  If SlideUp is set, we can move upwards to make more space
        //                  the returned rectangle is clipped to the screen space
        // returns rectangle used
        public virtual Rectangle Render(Point preferredxy, Size? fixedsize = null)
        {
            picturebox.ClearImageList();

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

            int maxcheckboxes = MaxRadioColumns();
            bool hassubmenuicons = false;

            int vtop = VerticalSpacing;        // top of area

            // calculate positions in single column into an array, calculate maxwidthsignelcol, calculate vheightsinglecol

            int maxwidthsinglecol = 0;                          // single col width including submenu 
            int vheightsinglecol = vtop;                        // spacing at top 

            List<Tuple<Rectangle, Rectangle, Rectangle>> positions = new List<Tuple<Rectangle, Rectangle, Rectangle>>();        // record info on positions of items

            // set up each item, and work out sizes

            for (int i = 0; i < ItemList.Count; i++)
            {
                var cl = ItemList[i];

                int fonth = (int)this.Font.GetHeight() + 2; // a little extra for label

                Size iconsize = ImageSize.IsEmpty ? (cl.Image?.Size ?? new Size(0, 0)) : ImageSize;
                int vspacing = Math.Max(fonth, iconsize.Height);
                vspacing = Math.Max(vspacing, chkboxsize.Height);       // vspacing is the max of label, icon and checkbox

                int chkx = HorizontalSpacing;                                                               // position of check box
                int imgx = maxcheckboxes>0 ? chkx + (chkboxsize.Width + HorizontalSpacing)*maxcheckboxes : chkx;               // position of image
                int labx = cl.Image != null ? imgx + iconsize.Width + HorizontalSpacing : imgx;             // position of label

                // label is in autosize mode, setting Text and Font will size it
                cl.label.Text = cl.Text;
                cl.label.Font = this.Font;
                cl.label.ForeColor = this.ForeColor;
                cl.label.BackColor = BackColor;
                cl.label.MouseOverBackColor = this.MouseOverLabelColor;
                cl.label.Tag = i;       // tags are index

                if (!cl.Button)
                {
                    for( int j = 0; j < cl.checkbox.Length; j++)
                    {
                        var x = cl.checkbox[j];
                        if (x != null)
                        {
                            x.BackColor = this.BackColor;
                            x.CheckBoxColor = this.CheckBoxColor;
                            x.CheckBoxInnerColor = this.CheckBoxInnerColor;
                            x.CheckColor = this.CheckColor;
                            x.MouseOverColor = this.MouseOverCheckboxColor;
                            x.TickBoxReductionRatio = TickBoxReductionRatio;
                            x.Font = Font;
                            x.Tag = i;        // store index of item in tag
                            x.Tag2 = j;     // and the button index
                        }
                    }
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
                    hassubmenuicons = true;
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

                vheightsinglecol += vspacing + VerticalSpacing;     // item spacing and space between/extra space at bottom

                maxwidthsinglecol = Math.Max(maxwidthsinglecol, labx + cl.label.Size.Width);        // this is the width excluding the sub menu icon
            }

            int submenuiconposx = maxwidthsinglecol;      // position after max label of submenu icon

            // if we have sub menu icons, arranged at labelwidthmax, we need to add a bit more on to get the true column width to estimate with
            if (hassubmenuicons)
                maxwidthsinglecol += firsticonsize.Width;

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
            int vpos = vtop;

            // position the controls

            for (int i = 0; i < ItemList.Count; i++)     
            {
                var cl = ItemList[i];
                var post = positions[i];

                int vspacing = post.Item1.Y;

                // if allowed, and we display any part of this over vavailable
                // or we have reached the depth..
                if ((checkvspacing && vpos + vspacing >= vavailable) || colindex++ == itemspercolumn)
                {
                    colused++;
                    vpos = vtop;
                    colindex = 1;
                }

                int vcentre = vpos + vspacing / 2;
                int colx = (colused - 1) * maxwidthsinglecol;           // column offset position.. given max width of single column including subform indicator

                //System.Diagnostics.Debug.WriteLine($" {cl.label.Text} = {post.Item1} : {post.Item2} : {post.Item3} @ {colx} {vpos}");

                if (cl.checkbox != null)
                {
                    for( int j = 0; j < cl.checkbox.Length; j++)
                    {
                        if (cl.checkbox[j] != null)
                        {
                            cl.checkbox[j].Location = new Rectangle(post.Item1.X + colx + j * (post.Item1.Width + HorizontalSpacing), vcentre - post.Item1.Height / 2, post.Item1.Width, post.Item1.Height);
                            picturebox.Add(cl.checkbox[j]);
                        }
                    }
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
                    int pos = cl.label.Location.Y + cl.label.Location.Height / 2 - firsticonsize.Height / 2;
                    cl.submenuicon.Location = new Rectangle(colx + submenuiconposx, pos, firsticonsize.Width, firsticonsize.Height);
                    picturebox.Add(cl.submenuicon);
                }

                vpos += vspacing + VerticalSpacing; 
            }

            // it will clip the render to the last bottom image so make sure we add on that vbottom margin to make the image bigger
            // better this way than adding on the height below

            picturebox.Render(margin:new Size(0,VerticalSpacing));

            // now return the rectangle size needed

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
            int checkbox = (int)cb.Tag2;
            var it = ItemList[index];

            if (it.DisableUncheck && cb.Checked == false) // if uncheckable, then set it back. Used in radio button situations
            {
                cb.Checked = true;
            }

            if (cb.Checked && it.Exclusive!= null && it.Group == false)      // if checking, and we have tags which must be turned off for normal buttons
            {
                if(it.Exclusive.Equals(All))            // turn off all but us
                {
                    foreach( var x in ItemList)
                    {
                        if ( x!=it && x.Group == false && x.CheckBoxExists(checkbox) && x.Exclusive != null && x.DisableUncheck ) // TBD why ? && x.Tag != All && x.Tag != None )
                        {
                            x.checkbox[checkbox].Checked = false;
                        }
                    }
                }
                else
                {
                    string[] tags = it.Exclusive.Split(SettingsSplittingChar);      // turn off this list excepting us
                    foreach (string s in tags)
                    { 
                        int offindex = ItemList.FindIndex(x => x.Tag == s);
                        if (offindex >= 0 && offindex != index && ItemList[offindex].CheckBoxExists(checkbox))     // don't uncheck us, make sure it exists
                        {
                            ItemList[offindex].checkbox[checkbox].Checked = false;
                        }
                    }
                }
            }

            var prevstate = cb.Checked ? CheckState.Unchecked : CheckState.Checked;

            ItemCheckEventArgs eventargs = new ItemCheckEventArgs(index, cb.CheckState, prevstate);
            CheckChangedEvent(checkbox, cb, eventargs);       // derived classes first.
            CheckedChanged?.Invoke(index,checkbox, it.Tag, it.Text, it.UserTag, eventargs);
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

        // used by group to override behaviour and work group options
        protected virtual void CheckChangedEvent(int checkbox, ExtPictureBox.CheckBox cb, ItemCheckEventArgs e) { }

        public bool Theme(Theme t, Font fnt)
        {
            BackColor = t.Form;
            ForeColor = t.TextBlockForeColor;
            BorderColor = t.GridBorderLines;

            // scroll bar
            BorderColor = ThumbBorderColor = ArrowBorderColor = t.GridBorderLines;
            SliderColor = t.GridSliderBack;
            SliderColor2 = t.IsButtonGradientStyle ? t.GridSliderBack2 : t.GridSliderBack;
            SliderDrawAngle = t.GridSliderGradientDirection;
            ArrowButtonColor = t.GridScrollArrowBack;
            ArrowButtonColor2 = t.IsButtonGradientStyle ? t.GridScrollArrow2Back : t.GridScrollArrowBack;
            // arrow direction is not config
            ThumbButtonColor = t.GridScrollButtonBack;
            ThumbButtonColor2 = t.IsButtonGradientStyle ? t.GridScrollButtonBack2 : t.GridScrollButtonBack;
            MouseOverButtonColor = ThumbButtonColor.Multiply(t.MouseOverScaling);
            MouseOverButtonColor2 = ThumbButtonColor2.Multiply(t.MouseOverScaling);
            MousePressedButtonColor = ThumbButtonColor.Multiply(t.MouseSelectedScaling);
            MousePressedButtonColor = ThumbButtonColor2.Multiply(t.MouseSelectedScaling);
            sb.FlatStyle = t.ButtonFlatStyle;
            sb.SkinnyStyle = t.SkinnyScrollBars;

            // checkbox for us
            CheckBoxColor = t.CheckBoxBack;
            CheckBoxInnerColor = t.CheckBoxBack.Multiply(1.5F);
            CheckColor = t.CheckBoxTick;
            MouseOverCheckboxColor = t.CheckBoxBack.Multiply(0.75F);
            MouseOverLabelColor = t.CheckBoxBack.Multiply(0.75F);
            TickBoxReductionRatio = 0.75f;

            pictureboxscroll.ScrollBarWidth = t.ScrollBarWidth();
            return false;
        }

        private ExtPictureBoxScroll pictureboxscroll;
        private ExtPictureBox picturebox;
        private ExtScrollBar sb;
        private ExtButtonDrawn closeicon;

        #endregion
    }
}


