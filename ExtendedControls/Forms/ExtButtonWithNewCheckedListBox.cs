﻿/*
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
    public class ExtButtonWithNewCheckedListBox : ExtButton
    {
        public Action<string,bool> ValueChanged;        // current value, and if changed

        // call to init, drop down will be activated by OnClick.
        public void Init(IEnumerable<CheckedIconUserControl.Item> standardoptions, 
                            string startsetting,                 // start setting
                            Action<string,bool> settingschanged,    // callback to say settings changed.
                            bool allornoneshown = true, 
                            bool allornonback = false,
                            string disabled = null,            // either "" default, or text to use
                            Size? imagesize = null, Size? screenmargin = null, Size? closeboundaryregion = null, 
                            bool multicolumns = false,
                            IEnumerable<CheckedIconUserControl.Item> groupoptions = null,
                            bool sortitems = false)
        {
            this.list = standardoptions;
            this.glist = groupoptions;
            this.allornone = allornoneshown;
            this.disabled = disabled;
            this.allornoneback = allornonback;
            this.imagesize = imagesize;
            this.screenmargin = screenmargin;
            this.closeboundaryregion = closeboundaryregion;
            this.putsettings = settingschanged;
            this.currentsettings = startsetting;
            this.multicolumns = multicolumns;
            this.sortitems = sortitems;
        }

        // All/None with all items back, and a default closing area
        public void InitAllNoneAllBack(IEnumerable<CheckedIconUserControl.Item> standardoptions,
                             string startsettings, Action<string,bool> settingschanged, bool disabled = true, bool sortitems = false)
        {
            Init(standardoptions, startsettings, settingschanged, true, false, disabled ? "" : null, closeboundaryregion: new System.Drawing.Size(64, 64), multicolumns:true, sortitems:sortitems);
        }


        public char SettingsSplittingChar { get; set; } = ';';      // what char is used for split settings

        public string Get() { return currentsettings; }
        public bool Set(string value) { if (value != currentsettings) { currentsettings = value; return true; } else return false; }

        // individual setting, or All or None if allofnoneback is true
        public bool IsSet(string setting)
        {
            return currentsettings.Contains(setting + SettingsSplittingChar);     // we use ; as the default setting char
        }

        public bool IsNoneSet { get { return currentsettings == "None" + SettingsSplittingChar || currentsettings.Length == 0; } }
        public bool IsAnySet { get { return !IsNoneSet; } }
        public bool IsDisabled { get { return currentsettings == CheckedIconGroupUserControl.Disabled; } }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            DropDown = new CheckedIconNewListBoxForm();

            if (allornone)
                DropDown.UC.AddAllNone();
            if (disabled!=null)
                DropDown.UC.AddDisabled(disabled.HasChars() ? disabled :null);
            DropDown.AllOrNoneBack = allornoneback;
            DropDown.UC.SettingsSplittingChar = SettingsSplittingChar;
            if (glist != null)
                DropDown.UC.Add(glist,forcegroup:true);
            if ( list != null)
                DropDown.UC.Add(list);
            if ( sortitems )
                DropDown.UC.Sort();

            if (imagesize.HasValue)
                DropDown.UC.ImageSize = imagesize.Value;
            if (screenmargin.HasValue)
                DropDown.UC.ScreenMargin = screenmargin.Value;
            if (closeboundaryregion.HasValue)
                DropDown.CloseBoundaryRegion = closeboundaryregion.Value;

            DropDown.UC.MultiColumnSlide = multicolumns;

            DropDown.SaveSettings = (newsetting, o) =>
            {
                bool changed = currentsettings != newsetting;
                currentsettings = newsetting;
                putsettings(newsetting,changed);
                ValueChanged?.Invoke(newsetting,changed);
            };

            DropDown.Show(currentsettings, this, this.FindForm());
        }

        private IEnumerable<CheckedIconUserControl.Item> list;
        private IEnumerable<CheckedIconUserControl.Item> glist;
        private bool allornone;
        private string disabled;
        private Size? imagesize;
        private Size? screenmargin;
        private Size? closeboundaryregion;
        private CheckedIconNewListBoxForm DropDown = null;
        private Action<string,bool> putsettings;
        private bool allornoneback;
        private string currentsettings;
        private bool multicolumns;
        private bool sortitems;

    }
}
