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
        static public Image ImageNone { get { return Properties.Resources.None; } }
        static public Image ImageAll { get { return Properties.Resources.All; } }
        static public Image ImageDisabled { get { return Properties.Resources.Disabled; } }

        public void AddAllNone(int checkmap = 1)
        {
            AddGroupItemAtTop(None, "None".Tx(), ImageNone, checkmap: checkmap);
            AddGroupItemAtTop(All, "All".Tx(), ImageAll, checkmap: checkmap);       // displayed, translate
        }

        // list of options which do not participate in the all/none selection. Include the separation character at the end
        public string NoneAllIgnore { get; set; } = "";

        public const string Disabled = "Disabled";
        public void AddDisabled(string differenttext = null, int checkmap = 1)
        {
            AddGroupItemAtTop(Disabled, differenttext.HasChars() ? differenttext : "Disabled".Tx(), ImageDisabled, exclusive: All, checkmap:checkmap);
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

            // an exclusive group option overrides all other settings and returns its own tag, if checked
            foreach (var eo in GroupOptions(checkbox).Where(x=>x.ExclusiveGroupOption))
            {
                if (IsChecked(new string[] { eo.Tag },checkbox))    // if this tag and checked, list is this
                    list = eo.Tag;
            }

            if (list == null)                                           // no exclusive options, so
                list = base.GetChecked(allornone, tagsignorelist, checkbox);  // get the set options from the standard grouping, using All/None

            return list;
        }

        public override Rectangle Render(Point preferedxy, Size? fixedsize = null)
        {
            var rect = base.Render(preferedxy, fixedsize);

            int maxcheckboxes = MaxRadioColumns();
            for (int i = 0; i < maxcheckboxes; i++)
                SetGroupOptions(i);

            return rect;
        }

        #region Implementation

        protected override void CheckChangedEvent(int checkbox, ImageElement.CheckBox cb, ItemCheckEventArgs e) // we get a chance to operate group options
        {
            var exclusivegroupoptions = GroupOptions(checkbox).Where(y => y.ExclusiveGroupOption);      // list of exclusive group options (ie. disabled button)
            
            var it = ItemList[e.Index];

            if (it.Group)
            {
                string taglist = it.Tag;

                // Group button, if on
                if (e.NewValue == CheckState.Checked)
                {
                    bool shift = Control.ModifierKeys.HasFlag(Keys.Control);

                    if (!shift)         // shift modifies behaviour, normal is to clear all group items
                    {
                        //System.Diagnostics.Debug.WriteLine($"Group checked {checkbox}: {taglist} clear all");
                        Set(0, CheckState.Unchecked, -1, NoneAllIgnore, checkbox, ignoregroup:true);        // set all non group items unchecked except NoneAllIgnore ones
                    }

                    // all exclusive group options, look thru and see if its got the same tag list, if so, set it, else clear the tick
                    // so for example, if the tag = disabled, then all groups with tag=disabled would be set on, all other options not
                    // any other groups will be turned off by SetGroupOptions
                    foreach (var eo in exclusivegroupoptions)                    
                    {
                        Set(eo.Tag, taglist == eo.Tag, checkbox);   
                    }

                    if (taglist == All)                                     // All sets all except the ignore list
                    {
                        //System.Diagnostics.Debug.WriteLine($"Group checked {checkbox}: {taglist} set all");
                        Set(0, CheckState.Checked, -1, NoneAllIgnore, checkbox, ignoregroup: true);        // set all non group items checked except NoneAllIgnore
                    }
                    else if (taglist == None || it.ExclusiveGroupOption)            // None clears all except the ignore list, or if the group is an exclusive option, clear all
                    {
                        //System.Diagnostics.Debug.WriteLine($"Group checked {checkbox}: {taglist} unset all");
                        Set(0, CheckState.Unchecked, -1, NoneAllIgnore, checkbox, ignoregroup: true);        // set all non group items unchecked except NoneAllIgnore
                    }
                    else
                    {
                        //System.Diagnostics.Debug.WriteLine($"Group checked {checkbox}: {taglist} set ");
                        Set(taglist, true, checkbox);                                       // else set items in the tag list
                    }
                }
                else
                {
                    if (it.ExclusiveGroupOption)                       // clicking on exclusive option does not turn it off, so turn it back on
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
                    // uncheck all exclusive group items - this is the only way to turn them off
                    foreach (var eo in exclusivegroupoptions)      
                        Set(eo.Tag, false, checkbox);
                }
            }

            SetGroupOptions(checkbox);      // make sure group options ticks are set right
        }

        // This sets the group options ticks of a checkbox set
        private void SetGroupOptions(int checkbox)
        {
            // returning All or None or list of selected, on non.. on non group items, and ignoring those which do not contribute to all/none
            string list = GetChecked(true, NoneAllIgnore, checkbox);

            System.Diagnostics.Debug.WriteLine($"Set Group Options on {checkbox} current set is {list}");

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


