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

using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    // Enhances the base system with group functionality

    public class CheckedIconGroupUserControl : CheckedIconUserControl
    {
        public void AddAllNone(int checkmap = 1)
        {
            AddGroupItemAtTop(None, "None".TxID(ECIDs.None), Properties.Resources.None, checkmap: checkmap);
            AddGroupItemAtTop(All, "All".TxID(ECIDs.All), Properties.Resources.All, checkmap: checkmap);       // displayed, translate
        }

        // list of options which do not participate in the all/none selection. Include the separation character at the end
        public string NoneAllIgnore { get; set; } = "";

        public const string Disabled = "Disabled";
        public void AddDisabled(string differenttext = null, int checkmap = 1)
        {
            AddGroupItemAtTop(Disabled, differenttext.HasChars() ? differenttext : "Disabled".TxID(ECIDs.Disabled), Properties.Resources.Disabled, exclusive: All, checkmap:checkmap);
        }

        public void AddGroupItem(string tags, string text, Image img = null, object usertag = null, string exclusive = null,
                        int checkmap = 1, string[] checkbuttontooltiptext = null, string icontooltiptext = null, string labeltooltiptext = null)
        {
            Add(tags, text, img, false, exclusive, false, false, usertag, true, checkmap, checkbuttontooltiptext, icontooltiptext, labeltooltiptext);
        }

        public void AddGroupItemAtTop(string tags, string text, Image img = null, object usertag = null, string exclusive = null,
                        int checkmap = 1, string[] checkbuttontooltiptext = null, string icontooltiptext = null, string labeltooltiptext = null)
        {
            Add(tags, text, img, true, exclusive, false, false, usertag, true, checkmap, checkbuttontooltiptext, icontooltiptext, labeltooltiptext);
        }

        // Get checked including any group override settings
        public override string GetChecked(bool allornone, string tagsignorelist = "", int checkbox = 0)
        {
            string list = null;

            foreach (var eo in GroupOptions(checkbox))
            {
                if (eo.Exclusive?.Equals(All) ?? false)        // exclusive option All for group
                {
                    if (IsChecked(new string[] { eo.Tag },checkbox))    // if this tag and checked, list is this
                        list = eo.Tag;
                }
            }

            if (list == null)                                 // no exclusive options, so
                list = base.GetChecked(allornone, tagsignorelist, checkbox);  // get the set options from the standard grouping, using All/None

            return list;
        }

        public override Rectangle Render(Point preferedxy, Size? fixedsize = null)
        {
            var checkboxes = ItemList.Where(x => x.Button == false);
            int maxcheckboxes = checkboxes.Count() > 0 ? checkboxes.Select(x => x.checkbox.Length).Max() : 0;

            var rect = base.Render(preferedxy, fixedsize);

            for (int i = 0; i < maxcheckboxes; i++)
                SetGroupOptions(i);

            return rect;
        }

        #region Implementation

        protected override void CheckChangedEvent(int checkbox, ExtPictureBox.CheckBox cb, ItemCheckEventArgs e) // we get a chance to operate group options
        {
            var groupoptions = GroupOptions(checkbox);
            
            var x = ItemList[e.Index];

            if (x.Group)
            {
                string taglist = x.Tag;
                bool exclusiveoption = x.Exclusive?.Equals(All) ?? false;

                if (e.NewValue == CheckState.Checked)
                {
                    bool shift = Control.ModifierKeys.HasFlag(Keys.Control);

                    if (!shift)
                    {
                        //System.Diagnostics.Debug.WriteLine($"Group checked {checkbox}: {taglist} clear all");
                        Set(0, CheckState.Unchecked, -1, NoneAllIgnore, checkbox, true);        // set all non group items unchecked except NoneAllIgnore ones
                    }

                    foreach (var eo in groupoptions)                    // for all other group options
                    {
                        if (eo.Exclusive?.Equals(All) ?? false)         // if exclusive option ALL
                            Set(eo.Tag, taglist == eo.Tag, checkbox);   // set the tag list given by the group, to either ON if taglist matches the eo taglist, for this checkbox
                    }

                    if (taglist == All)                                     // All sets all except the ignore list
                    {
                        //System.Diagnostics.Debug.WriteLine($"Group checked {checkbox}: {taglist} set all");
                        Set(0, CheckState.Checked, -1, NoneAllIgnore, checkbox, true);        // set all non group items checked except NoneAllIgnore
                    }
                    else if (taglist == None || exclusiveoption)            // None clears all except the ignore list
                    {
                        //System.Diagnostics.Debug.WriteLine($"Group checked {checkbox}: {taglist} unset all");
                        Set(0, CheckState.Unchecked, -1, NoneAllIgnore, checkbox, true);        // set all non group items unchecked except NoneAllIgnore
                    }
                    else
                    {
                        //System.Diagnostics.Debug.WriteLine($"Group checked {checkbox}: {taglist} set ");
                        Set(taglist, true, checkbox);                                       // else set items in the tag list
                    }
                }
                else
                {
                    if (exclusiveoption)                       // clicking on exclusive option does not turn it off
                    {
                        //System.Diagnostics.Debug.WriteLine($"Group unchecked {checkbox} exclusive list");
                        Set(taglist, true, checkbox);
                    }
                }
            }
            else
            {
                if (e.NewValue == CheckState.Checked)       // Normal item, not group, if checked on
                {
                    foreach (var eo in groupoptions)    // check all group options of this checkbox
                    {
                        if (eo.Exclusive?.Equals(All) ?? false) // we have an exclusion which is All
                            Set(eo.Tag, false, checkbox);
                    }
                }
            }

            SetGroupOptions(checkbox);
        }

        // This sets the group options ticks of a checkbox set
        private void SetGroupOptions(int checkbox)
        {
            // returning All or None or list of selected, on non.. on non group items, and ignoring those which do not contribute to all/none
            string list = GetChecked(true, NoneAllIgnore, checkbox);

           // System.Diagnostics.Debug.WriteLine($"Set Group Options on {checkbox} current set is {list}");

            foreach (var eo in GroupOptions(checkbox))
            {
                int pos = (int)eo.label.Tag;

                //System.Diagnostics.Debug.WriteLine($"  Check Group tag {eo.Tag} at {pos} vs {list}");

                if (eo.Tag == All)              
                {
                    //System.Diagnostics.Debug.WriteLine($"   - Group option All");
                    Set(pos, list.Equals(All), checkbox);
                }
                else if (eo.Tag == None)
                {
                    //System.Diagnostics.Debug.WriteLine($"   - Group option None");
                    Set(pos, list.Equals(None), checkbox);
                }
                else if (eo.Tag == list)       // if the tag list of this group matches what is set
                {
                    //System.Diagnostics.Debug.WriteLine($"   - Group option tag list matches list");
                    Set(pos, true, checkbox);  
                }
                else if (list.MatchesAllItemsInList(eo.Tag, SettingsSplittingChar))        // exactly, tick
                {
                    //System.Diagnostics.Debug.WriteLine($"   - Group option list matches all items in list");
                    Set(pos, true, checkbox);
                }
                else if (list.ContainsAllItemsInList(eo.Tag, SettingsSplittingChar)) // contains, intermediate
                {
                    //System.Diagnostics.Debug.WriteLine($"   - Group option list matches some items in list");
                    Set(pos, CheckState.Indeterminate, checkbox:checkbox);
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine($"   - Group option nothing matches turning off");
                    Set(pos, false,checkbox);
                }
            }
        }


        #endregion
    }
}


