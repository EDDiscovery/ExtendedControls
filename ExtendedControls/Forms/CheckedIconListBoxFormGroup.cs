/*
 * Copyright © 2016-2019 EDDiscovery development team
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
 * 
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    // Adds on group control. Must be made using these Add function and then create() is called, before show()

    public class CheckedIconListBoxFormGroup : CheckedIconListBoxForm
    {
        private class Options
        {
            public string Tag;
            public string Text;
            public Image Image;
        }

        private List<Options> groupoptions = new List<Options>();
        private List<Options> standardoptions = new List<Options>();

        public void AddAllNone()
        {
            AddGroupOptionAtTop("None", "None".Tx(), Properties.Resources.None);
            AddGroupOptionAtTop("All", "All".Tx(), Properties.Resources.All);       // displayed, translate
        }

        public void AddGroupOption(string tags, string text, Image img = null)      // group option
        {
            var o = new Options() { Tag = tags, Text = text, Image = img };
            groupoptions.Add(o);
        }

        public void AddGroupOptionAtTop(string tags, string text, Image img = null)      // group option
        {
            var o = new Options() { Tag = tags, Text = text, Image = img };
            groupoptions.Insert(0, o);
        }

        public void AddStandardOption(string tag, string text, Image img = null)                // standard option
        {
            standardoptions.Add(new Options() { Tag = tag, Text = text, Image = img });
        }

        public void AddStandardOption(List<Tuple<string, string, Image>> list)                // standard option
        {
            foreach (var x in list)
                AddStandardOption(x.Item1, x.Item2, x.Item3);
        }

        public void SortStandardOptions()
        {
            standardoptions.Sort(delegate (Options left, Options right)     // in order, oldest first
            {
                return left.Text.CompareTo(right.Text);
            });
        }

        public void Create(string settings, bool applytheme = true)         // create, set settings
        {
            foreach (var x in groupoptions)
                AddItem(x.Tag, x.Text, x.Image);

            foreach (var x in standardoptions)
                AddItem(x.Tag, x.Text, x.Image);

            string[] slist = settings.SplitNoEmptyStartFinish(';');
            if (slist.Length == 1 && slist[0].Equals("All"))
                SetCheckedFromToEnd(groupoptions.Count());
            else
                SetChecked(settings);

            SetGroupOptions();

            LargeChange = ItemCount * Properties.Resources.All.Height / 40;   // 40 ish scroll movements

            if (applytheme)
                ThemeableFormsInstance.Instance?.ApplyStd(this);
        }

        private void SetGroupOptions()
        {
            string list = GetChecked(groupoptions.Count());       // using All or None.. on items beyond reserved entries
                                                                     //            System.Diagnostics.Debug.WriteLine("Checked" + list);
            int p = 0;
            foreach (var eo in groupoptions)
            {
                if (eo.Tag == "All")
                {
                    SetChecked(p, list.Equals("All"));
                }
                else if (eo.Tag == "None")
                {
                    SetChecked(p, list.Equals("None"));
                }
                else if (list.MatchesAllItemsInList(eo.Tag, ';'))        // exactly, tick
                {
                    //System.Diagnostics.Debug.WriteLine("Checking T " + eo.Tag + " vs " + list);
                    SetChecked(p);
                }
                else if (list.ContainsAllItemsInList(eo.Tag, ';')) // contains, intermediate
                {
                    //System.Diagnostics.Debug.WriteLine("Checking I " + eo.Tag + " vs " + list + list.Equals(eo.Tag));
                    SetChecked(p, CheckState.Indeterminate);
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine("Checking F " + eo.Tag + " vs " + list);
                    SetChecked(p, false);
                }

                p++;
            }
        }

        protected override void CheckChangedEvent(ExtCheckBox cb, ItemCheckEventArgs e) // we get a chance to operate group options
        {
            if (e.NewValue == CheckState.Checked)       // all or none set all of them
            {
                if (e.Index < groupoptions.Count())
                {
                    bool shift = Control.ModifierKeys.HasFlag(Keys.Control);

                    if (!shift)
                        SetCheckedFromToEnd(groupoptions.Count(), false);   // if not shift, we clear all, and apply this tag

                    string tag = groupoptions[e.Index].Tag;
                    if (tag == "All")
                        SetCheckedFromToEnd(groupoptions.Count(), true);
                    else if (tag == "None")
                        SetCheckedFromToEnd(groupoptions.Count(), false);
                    else
                        SetChecked(tag);
                }
            }
            else
            {
                if (e.Index < groupoptions.Count())        // off on this clears the entries of it only
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
            System.Diagnostics.Debug.WriteLine("SF Deactivate save settings");
            SaveSettings?.Invoke(GetChecked(groupoptions.Count, AllOrNoneBack), Tag);
        }
    }
}
