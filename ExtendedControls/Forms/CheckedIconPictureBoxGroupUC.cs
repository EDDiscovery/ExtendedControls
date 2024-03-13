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
using System.Windows.Forms;

namespace ExtendedControls
{
    // Enhances the base system with group functionality

    public class CheckedIconGroupUserControl : CheckedIconUserControl
    {
        public void AddAllNone()
        {
            AddGroupItemAtTop(None, "None".TxID(ECIDs.None), Properties.Resources.None);
            AddGroupItemAtTop(All, "All".TxID(ECIDs.All), Properties.Resources.All);       // displayed, translate
        }

        // list of options which do not participate in the all/none selection. Include the separation character at the end
        public string NoneAllIgnore { get; set; } = "";

        public const string Disabled = "Disabled";
        public void AddDisabled(string differenttext = null)
        {
            AddGroupItemAtTop(Disabled, differenttext.HasChars() ? differenttext : "Disabled".TxID(ECIDs.Disabled), Properties.Resources.Disabled, exclusive: All);
        }

        public void AddGroupItem(string tags, string text, Image img = null, object usertag = null, string exclusive = null)
        {
            Add(tags, text, img, usertag: usertag, exclusivetags: exclusive, group: true);
        }

        public void AddGroupItemAtTop(string tags, string text, Image img = null, object usertag = null, string exclusive = null)
        {
            Add(tags, text, img, usertag: usertag, exclusivetags: exclusive, group: true, attop: true);
        }

        // Get checked including any group override settings
        public override string GetChecked(bool allornone, string tagsignorelist = "")
        {
            string list = null;

            foreach (var eo in GroupOptions())
            {
                if (eo.Exclusive?.Equals(All) ?? false)        // exclusive option
                {
                    if (IsChecked(new string[] { eo.Tag }))    // if this tagand checked, list is this
                        list = eo.Tag;
                }
            }

            if (list == null)                                 // no exclusive options, so
                list = base.GetChecked(allornone, tagsignorelist);  // get the set options from the standard grouping, using All/None

            return list;
        }
        public override Rectangle Render(Point preferedxy, Size? fixedsize = null)
        {
            SetGroupOptions();
            return base.Render(preferedxy, fixedsize);
        }

        #region Implementation

        protected override void CheckChangedEvent(ExtPictureBox.CheckBox cb, ItemCheckEventArgs e) // we get a chance to operate group options
        {
            var groupoptions = GroupOptions();

            if (e.NewValue == CheckState.Checked)       // all or none set all of them
            {
                if (e.Index < groupoptions.Count)
                {
                    bool shift = Control.ModifierKeys.HasFlag(Keys.Control);

                    if (!shift)
                        SetFromToEnd(groupoptions.Count, false, NoneAllIgnore);   // if not shift, we clear all (excepting the ignore list), and apply this tag

                    string tag = groupoptions[e.Index].Tag;
                    bool exclusiveoption = groupoptions[e.Index].Exclusive?.Equals(All) ?? false;

                    foreach (var eo in groupoptions)
                    {
                        if (eo.Exclusive?.Equals(All) ?? false)         // if exclusive option
                            Set(eo.Tag, tag == eo.Tag);          // its on if we clicked on it, else off
                    }

                    if (tag == All)                                     // All sets all except the ignore list
                    {
                        SetFromToEnd(groupoptions.Count, true, NoneAllIgnore);
                    }
                    else if (tag == None || exclusiveoption)            // None clears all except the ignore list
                    {
                        SetFromToEnd(groupoptions.Count, false, NoneAllIgnore);
                    }
                    else
                        Set(tag);                                       // else set tag
                }
                else
                {
                    foreach (var eo in groupoptions)
                    {
                        if (eo.Exclusive?.Equals(All) ?? false)         // we have set something else, so clear all exclusive options
                            Set(eo.Tag, false);
                    }
                }
            }
            else
            {
                if (e.Index < groupoptions.Count)               // off on this clears the entries of it only
                {
                    string tag = groupoptions[e.Index].Tag;
                    bool exclusiveoption = groupoptions[e.Index].Exclusive?.Equals(All) ?? false;

                    if (exclusiveoption)                       // clicking on exclusive option does not turn it off
                        Set(tag, true);
                }
            }

            SetGroupOptions();
        }

        private void SetGroupOptions()
        {
            // using All or None.. on non group items, and ignoring those which do not contribute to all/none
            string list = GetChecked(true, NoneAllIgnore);
            //System.Diagnostics.Debug.WriteLine($"Set Group Options on {list}");

            int pos = 0;
            foreach (var eo in GroupOptions())
            {
                if (eo.Tag == All)
                {
                    Set(pos, list.Equals(All));
                }
                else if (eo.Tag == None)
                {
                    Set(pos, list.Equals(None));
                }
                else if (eo.Tag == list)       // if an exclusive group option set list equal to this
                {
                    Set(pos, true);
                }
                else if (list.MatchesAllItemsInList(eo.Tag, SettingsSplittingChar))        // exactly, tick
                {
                    //System.Diagnostics.Debug.WriteLine("Checking T " + eo.Tag + " vs " + list);
                    Set(pos, true);
                }
                else if (list.ContainsAllItemsInList(eo.Tag, SettingsSplittingChar)) // contains, intermediate
                {
                    //System.Diagnostics.Debug.WriteLine("Checking I " + eo.Tag + " vs " + list + list.Equals(eo.Tag));
                    Set(pos, CheckState.Indeterminate);
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine("Checking F " + eo.Tag + " vs " + list);
                    Set(pos, false);
                }

                pos++;
            }
        }


        #endregion
    }
}


