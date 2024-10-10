﻿/*
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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ConfigurableUC : UserControl, IConfigurableDialog
    {
        #region Properties

        public event Action<string, string, Object> Trigger { add { Entries.Trigger += value; } remove { Entries.Trigger -= value; } }

        // use in the trigger handler to swallow the return. Normally its not swallowed.
        public bool SwallowReturn { get { return Entries.SwallowReturn; } set { Entries.SwallowReturn = true; } }

        public ConfigurableEntryList Entries { get; private set; } = new ConfigurableEntryList();

        public BorderStyle PanelBorderStyle { get; set; } = BorderStyle.FixedSingle;
        public DialogResult DialogResult { get; set; }      // Just for the Interface - not used in this non modal UC

        #endregion

        #region Implementation
        public void Init(string lname, Object callertag)
        {
            Entries.Name = lname;
            Entries.CallerTag = callertag;

            contentpanel = new ExtPanelVertScroll() { Name = "ContentPanel" };

            vertscrollpanel = new ExtPanelVertScrollWithBar() { Name = "VScrollPanel", BorderStyle = PanelBorderStyle, Margin = new Padding(0), Padding = new Padding(0) };
            vertscrollpanel.Controls.Add(contentpanel);
            vertscrollpanel.HideScrollBar = true;
            vertscrollpanel.Dock = DockStyle.Fill;
            Controls.Add(vertscrollpanel);

            Entries.CreateEntries(contentpanel, null, null, this.FindToolTipControl());
        }

        public void UpdateEntries()
        {
            Entries.CreateEntries(contentpanel, null, null, this.FindToolTipControl(), this.FindForm().CurrentAutoScaleFactor(), -vertscrollpanel.ScrollValue);
            contentpanel.Recalcuate();  // don't move scroll, but recalc area to scroll
        }

        // call after themeing to apply some overrides
        public void Themed()
        {
            foreach (var ent in Entries)
            {
                ent.Location = ent.Control.Location;
                ent.Size = ent.Control.Size;
                if (ent.MinimumSize == Size.Empty)
                    ent.MinimumSize = ent.Size;
                if (ent.BackColor.HasValue)
                    ent.Control.BackColor = ent.BackColor.Value;
            }

            contentpanel.Recalcuate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if ( contentpanel != null )
            {
// TBD ANCHORING? as per CF Resize

                //System.Diagnostics.Debug.WriteLine($"ConfigurableUC On Resize scroll panel pos {vertscrollpanel.ScrollValue}");
                int pos = contentpanel.BeingPosition();
                contentpanel.FinishedPosition(pos);
                Entries.SendTrigger("Resize");
            }
        }

        // no action, not modal
        public void ReturnResult(DialogResult result)
        {
        }

        // Set value of control by string value
        public bool Set(string controlname, string value)
        {
            return Entries.Set(controlname, value);
        }

        // get control by name
        public Control GetControl(string controlname)
        {
            return Entries.GetControl(controlname);
        }

        // get control by name
        public string Get(string controlname)
        {
            return Entries.Get(controlname);
        }

        // Return GetValue() by controlname, null if can't get
        public T GetValue<T>(string controlname)
        {
            return Entries.GetValue<T>(controlname);
        }

        public void Show(IWin32Window window)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Variables

        private ExtPanelVertScroll contentpanel;
        private ExtPanelVertScrollWithBar vertscrollpanel;

        #endregion
    }
}
