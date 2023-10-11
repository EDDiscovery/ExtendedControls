/*
 * Copyright © 2023-2023 EDDiscovery development team
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

namespace ExtendedControls
{
    /// <summary>
    /// A button, with a drop down list box form group
    /// When a dropdown is complete, ValueChanged will be called always indicating current value and if changed
    /// </summary>
    public class ExtButtonWithCheckedIconListBoxGroup : ExtButton
    {
        public Action<string,bool> ValueChanged;        // current value, and if changed

        // call to init, drop down will be activated by OnClick.
        public void Init(IEnumerable<CheckedIconListBoxFormGroup.StandardOption> standardoptions, 
                            string currentsettings, Action<string> settingschanged,
                            bool allornoneshown = true, 
                            bool allornonback = false,
                            Size? imagesize = null, Size? screenmargin = null, Size? closeboundaryregion = null,
                            IEnumerable<CheckedIconListBoxFormGroup.GroupOption> groupoptions = null)
        {
            this.list = standardoptions;
            this.glist = groupoptions;
            this.allornone = allornoneshown;
            this.allornoneback = allornonback;
            this.imagesize = imagesize;
            this.screenmargin = screenmargin;
            this.closeboundaryregion = closeboundaryregion;
            this.putsettings = settingschanged;
            this.currentsettings = currentsettings;
        }

        public char SettingsSplittingChar { get; set; } = ';';      // what char is used for split settings

        // individual setting, or All or None if allofnoneback is true
        public bool IsSet(string setting)
        {
            return currentsettings.Contains(setting + SettingsSplittingChar);     // we use ; as the setting char
        }

        public bool IsNoneSet()
        {
            return currentsettings == "None" || currentsettings.Length == 0;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            DropDown = new CheckedIconListBoxFormGroup();

            if (allornone)
                DropDown.AddAllNone();
            DropDown.AllOrNoneBack = allornoneback;
            DropDown.SettingsSplittingChar = SettingsSplittingChar;
            if (glist != null)
                DropDown.AddGroupOptions(glist);
            if ( list != null)
                DropDown.AddStandardOptions(list);
            if (imagesize.HasValue)
                DropDown.ImageSize = imagesize.Value;
            if (screenmargin.HasValue)
                DropDown.ScreenMargin = screenmargin.Value;
            if (closeboundaryregion.HasValue)
                DropDown.CloseBoundaryRegion = closeboundaryregion.Value;
          
            DropDown.SaveSettings = (newsetting, o) =>
            {
                bool changed = currentsettings != newsetting;
                currentsettings = newsetting;
                putsettings(newsetting);
                ValueChanged?.Invoke(newsetting,changed);
            };

            DropDown.Show(currentsettings, this, this.FindForm());
        }

        private IEnumerable<CheckedIconListBoxFormGroup.StandardOption> list;
        private IEnumerable<CheckedIconListBoxFormGroup.GroupOption> glist;
        private bool allornone;
        private Size? imagesize;
        private Size? screenmargin;
        private Size? closeboundaryregion;
        private CheckedIconListBoxFormGroup DropDown = null;
        private Action<string> putsettings;
        private bool allornoneback;
        private string currentsettings;

    }
}
