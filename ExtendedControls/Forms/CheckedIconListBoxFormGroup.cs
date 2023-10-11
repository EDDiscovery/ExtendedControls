/*
 * Copyright © 2016-2022 EDDiscovery development team
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
    // Adds on group control. Must be made using these Add function and then create() is called, before show(), or use the show helpers.

    public class CheckedIconListBoxFormGroup : CheckedIconListBoxForm
    {
        public class Options
        {
            public string Tag { get; set; }
            public string Text { get; set; }
            public Image Image { get; set; }
            public string Exclusive { get; set; }
            public bool DisableUncheck { get; set; }
            public bool Button { get; set; }
            public object UserTag { get; set; }
        }

        public List<Options> GroupOptions() { return groupoptions; }
        public List<Options> GroupOptionsWithUserTag() { return groupoptions.Where(x=>x.UserTag!=null).ToList(); }
        public List<Options> StandardOptions() { return standardoptions; }

        public string GetChecked() { return GetChecked(groupoptions.Count, AllOrNoneBack); }

        public long LongConfigurationValue { get; private set; }                    // if using long as the tag, this is the accumulated long value of all bits

        public void AddAllNone()
        {
            AddGroupOptionAtTop("None", "None".TxID(ECIDs.None), Properties.Resources.None);
            AddGroupOptionAtTop("All", "All".TxID(ECIDs.All), Properties.Resources.All);       // displayed, translate
        }

        public void AddGroupOption(string tags, string text, Image img = null, object usertag = null)      // group option
        {
            var o = new Options() { Tag = tags, Text = text, Image = img, UserTag = usertag };
            groupoptions.Add(o);
        }

        public void AddGroupOptionAtTop(string tag, string text, Image img = null , object usertag = null)      // group option
        {
            var o = new Options() { Tag = tag, Text = text, Image = img , UserTag = usertag};
            groupoptions.Insert(0, o);
        }

        public class GroupOption                                                 // alternate neater method
        {
            public string Tag { get; set; }
            public string Text { get; set; }
            public Image Image { get; set; }
            public Object UserTag { get; set; }

            public bool AtTop { get; set; }

            public GroupOption(string tag, string text, Image img = null, Object usertag = null, bool attop = false)
            {
                Tag = tag; Text = text; Image = img; UserTag = usertag; AtTop = attop;
            }
        }

        public void AddGroupOptions(IEnumerable<GroupOption> list)                // standard option
        {
            foreach (var x in list)
            {
                if (x.AtTop)
                    AddGroupOptionAtTop(x.Tag, x.Text, x.Image, x.UserTag);
                else
                    AddGroupOption(x.Tag, x.Text, x.Image, x.UserTag);
            }
        }

        // group options formatted in a string
        public void AddGroupOptions(string groupswithids, object usertag, Image img = null, char groupsepar = '\u24C2', char partsepar = '\u2713')
        {
            string[] groups = groupswithids.Split(groupsepar);
            foreach (var x in groups)
            {
                int nsp = x.IndexOf(partsepar);
                if (nsp > 0)
                {
                    var tag = x.Substring(nsp + 1);
                    var name = x.Substring(0, nsp);
                    AddGroupOption(tag, name , img, usertag: usertag);        // use the usertag and just put a marker in it
                }
            }
        }

        public string GetUserGroupDefinition(object usertag)                    // given a specific usertag, find group definition string
        {
            string ret = "";
            foreach (var x in groupoptions)
            {
                if ( x.UserTag != null && x.UserTag.Equals(usertag))
                    ret = ret.AppendPrePad($"{x.Text}\u2713{x.Tag}", "\u24C2");
            }
            return ret;
        }

        public void RemoveGroupOption(int i)
        {
            groupoptions.RemoveAt(i);
        }

        // use a long tag (bit field, 1,2,4,8 etc).  Use SettingsStringToLong to convert back
        public void AddStandardOption(long tag, string text, Image img = null, string exclusivetags = null, bool disableuncheck = false)   
        {
            LongConfigurationValue |= tag;
            standardoptions.Add(new Options() { Tag = tag.ToStringInvariant(), Text = text, Image = img, Exclusive = exclusivetags, DisableUncheck = disableuncheck });
        }

        public void AddStandardOption(string tag, string text, Image img = null, string exclusivetags = null, bool disableuncheck = false, bool button = false, object usertag = null)   // standard option
        {
            standardoptions.Add(new Options() { Tag = tag, Text = text, Image = img, Exclusive = exclusivetags, DisableUncheck = disableuncheck, Button = button, UserTag = usertag });
        }

        public void AddStandardOptionAtTop(string tag, string text, Image img = null, string exclusivetags = null, bool disableuncheck = false, bool button = false, object usertag = null)   // standard option
        {
            var o = new Options() { Tag = tag, Text = text, Image = img, Exclusive = exclusivetags, DisableUncheck = disableuncheck, Button = button, UserTag = usertag };
            standardoptions.Insert(0, o);
        }

        public void AddStandardOption(List<Tuple<string, string, Image>> list)                // standard option
        {
            foreach (var x in list)
                AddStandardOption(x.Item1, x.Item2, x.Item3);
        }
        public void AddStandardOption(Tuple<string, string, Image> ev)                // standard option
        {
            AddStandardOption(ev.Item1, ev.Item2, ev.Item3);
        }

        public class StandardOption                                                 // alternate neater method
        {
            public string Tag { get; set; }
            public string Text { get; set; }
            public Image Image { get; set; }

            public StandardOption(string tag, string text, Image img = null)
            {
                Tag = tag; Text = text; Image = img;
            }
        }

        public void AddStandardOptions(IEnumerable<StandardOption> list)                // standard option
        {
            foreach (var x in list)
                AddStandardOption(x.Tag,x.Text,x.Image);
        }

        public void SortStandardOptions()
        {
            standardoptions.Sort(delegate (Options left, Options right)     // in order, oldest first
            {
                return left.Text.CompareTo(right.Text);
            });
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

        public string[] SettingsTagList()
        {
            return standardoptions.Select(x => x.Tag).ToArray();
        }

        public void Create(string settings, bool applytheme = true)         // create, set settings, theme.  Call show(parent) afterwards
        {
            StartAdd();
            foreach (var x in groupoptions)
                AddItem(x.Tag, x.Text, x.Image,usertag:x.UserTag);

            foreach (var x in standardoptions)
                AddItem(x.Tag, x.Text, x.Image, false, x.Exclusive, x.DisableUncheck, x.Button,x.UserTag);

            string[] slist = settings.SplitNoEmptyStartFinish(SettingsSplittingChar);
            if (slist.Length == 1 && slist[0].Equals("All"))
                SetCheckedFromToEnd(groupoptions.Count);
            else
                SetChecked(settings);

            SetGroupOptions();

            EndAdd();

            LargeChange = ItemCount * Properties.Resources.All.Height / 40;   // 40 ish scroll movements

            if (applytheme)
            {
                Theme.Current?.ApplyStd(this);
                FormBorderStyle = FormBorderStyle.None;
            }
        }


        // show using a long to control - the tags should be numeric decimal versions of the flags
        // if no flags are set, then you can use a default standard option
        public void Show(long settings, Control ctr, Form parent, Object tag = null, int setonfornoflags = -1)         
        {
            string s = "";
            for (int i = 0; i < 63; i++)
            {
                if ((settings & (1L << i)) != 0)
                    s += (1L << i).ToStringInvariant() + SettingsSplittingChar;
            }

            if (s == "" && setonfornoflags>=0)
                s = standardoptions[setonfornoflags].Tag;

            if (ItemCount == 0)     // if not created, create..
                Create(s);

            Tag = tag;
            PositionBelow(ctr);
            Show(parent);
        }

        // given an enum list, and a set of bools indicating if each is set or not, show
        public void Show(Type ofenum, bool[] ctrlset, Control ctr, Form parent, Object tag = null)      
        {
            string settings = "";

            foreach (var v in Enum.GetValues(ofenum))
            {
                if (ctrlset[(int)v])
                    settings += v.ToString() + SettingsSplittingChar;
            }

            if (ItemCount == 0)     // if not created, create..
                Create(settings);

            Tag = tag;
            PositionBelow(ctr);
            Show(parent);
        }

        public void Show(string settings, Control ctr, Form parent, Object tag = null)         // quick form version
        {
            if (ItemCount == 0)     // if not created, create..
                Create(settings);
            Tag = tag;
            PositionBelow(ctr);
            Show(parent);
        }

        public void Show(string settings, Point point, Form parent, Object tag = null)         // quick form version
        {
            if (ItemCount == 0)     // if not created, create..
                Create(settings);
            Tag = tag;
            SetLocation = point;
            Show(parent);
        }

        #region Implementation

        private void SetGroupOptions()
        {
            string list = GetChecked(groupoptions.Count);       // using All or None.. on items beyond reserved entries
                                                                     //            System.Diagnostics.Debug.WriteLine("Checked" + list);
            int pos = 0;
            foreach (var eo in groupoptions)
            {
                if (eo.Tag == "All")
                {
                    SetChecked(pos, list.Equals("All"));
                }
                else if (eo.Tag == "None")
                {
                    SetChecked(pos, list.Equals("None"));
                }
                else if (list.MatchesAllItemsInList(eo.Tag, SettingsSplittingChar))        // exactly, tick
                {
                    //System.Diagnostics.Debug.WriteLine("Checking T " + eo.Tag + " vs " + list);
                    SetChecked(pos,true);
                }
                else if (list.ContainsAllItemsInList(eo.Tag, SettingsSplittingChar)) // contains, intermediate
                {
                    //System.Diagnostics.Debug.WriteLine("Checking I " + eo.Tag + " vs " + list + list.Equals(eo.Tag));
                    SetChecked(pos, CheckState.Indeterminate);
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine("Checking F " + eo.Tag + " vs " + list);
                    SetChecked(pos, false);
                }

                pos++;
            }
        }

        protected override void CheckChangedEvent(ExtCheckBox cb, ItemCheckEventArgs e) // we get a chance to operate group options
        {
            if (e.NewValue == CheckState.Checked)       // all or none set all of them
            {
                if (e.Index < groupoptions.Count)
                {
                    bool shift = Control.ModifierKeys.HasFlag(Keys.Control);

                    if (!shift)
                        SetCheckedFromToEnd(groupoptions.Count, false);   // if not shift, we clear all, and apply this tag

                    string tag = groupoptions[e.Index].Tag;
                    if (tag == "All")
                        SetCheckedFromToEnd(groupoptions.Count, true);
                    else if (tag == "None")
                        SetCheckedFromToEnd(groupoptions.Count, false);
                    else
                        SetChecked(tag);
                }
            }
            else
            {
                if (e.Index < groupoptions.Count)        // off on this clears the entries of it only
                {
                    string tag = groupoptions[e.Index].Tag;
                    if (tag != "All" && tag != "None ")
                        SetChecked(tag, false);
                }
            }

            SetGroupOptions();
        }

        protected override void SaveSettingsEvent()     // override to handle group returns
        {
            //System.Diagnostics.Debug.WriteLine("SF Deactivate save settings");
            SaveSettings?.Invoke(GetChecked(groupoptions.Count, AllOrNoneBack), Tag);
        }

        private List<Options> groupoptions = new List<Options>();
        private List<Options> standardoptions = new List<Options>();

        #endregion

    }
}
