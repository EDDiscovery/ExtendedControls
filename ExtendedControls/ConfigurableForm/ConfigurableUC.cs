/*
 * Copyright 2017-2024 EDDiscovery development team
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
    public class ConfigurableUC : UserControl, IConfigurableDialog
    {
        #region Properties

        public event Action<string, string, Object, Object, Object> TriggerAdv;

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

            Entries.UITrigger += (d, c, v1, v2, ct) => TriggerAdv?.Invoke(d, c, v1, v2, ct);
        }

        public void UpdateEntries()
        {
            Entries.CreateEntries(contentpanel, null, null, this.FindToolTipControl(), this.FindForm().CurrentAutoScaleFactor(),
                                                Theme.Current?.GetScaledFont(1.0f), -vertscrollpanel.ScrollValue);
            contentpanel.Recalcuate();  // don't move scroll, but recalc area to scroll

            foreach (var ent in Entries)
            {
                if (ent.Control is ExtPanelVertScrollWithBar)
                {
                    ((ExtPanelVertScrollWithBar)ent.Control).Recalcuate();      // for this panel, we need to recalc scroll area manually
                }
            }
        }

        // call after themeing to apply some overrides
        public void Themed()
        {
            foreach (var ent in Entries)
            {
                if (ent.Control != null)     // in case not made due to description error
                {
                    ent.Location = ent.Control.Location;
                    ent.Size = ent.Control.Size;
                    if (ent.MinimumSize == Size.Empty)
                        ent.MinimumSize = ent.Size;
                    if (ent.BackColor.HasValue)
                        ent.Control.BackColor = ent.BackColor.Value;
                    if (ent.Control is ExtPanelVertScrollWithBar)
                    {
                        ((ExtPanelVertScrollWithBar)ent.Control).Recalcuate();      // for this panel, we need to recalc scroll area manually
                    }
                }

                //System.Diagnostics.Debug.WriteLine($"ConfigUC theme {ent.Name} {ent.Control.Bounds}");
            }

            contentpanel.Recalcuate();
            initialscrollpanelsize = contentpanel.Size;
            resizepositionon = true;
        }

        private bool resizepositionon = false;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (contentpanel != null && !Entries.DisableTriggers && resizepositionon)
            {
                int widthdelta = contentpanel.Width - initialscrollpanelsize.Width;
                int heightdelta = contentpanel.Height - initialscrollpanelsize.Height;

                foreach (var ent in Entries)
                {
                    ent.Control.ApplyAnchor(ent.Anchor, ent.Location, ent.Size, ent.MinimumSize, widthdelta, heightdelta);

                    if (ent.Control is ExtPanelVertScrollWithBar)
                    {
                        ((ExtPanelVertScrollWithBar)ent.Control).Recalcuate();      // for this panel, we need to recalc scroll area manually
                    }
                }

                //System.Diagnostics.Debug.WriteLine($"ConfigurableUC On Resize scroll panel pos {vertscrollpanel.ScrollValue}");
                int pos = contentpanel.BeingPosition();
                contentpanel.FinishedPosition(pos);
                Entries.SendTrigger(Entries.Name, "Resize:" + Size.Width.ToStringInvariant() + "," + Size.Height.ToStringInvariant(), Size);
            }
        }

        public void Show(IWin32Window window)
        {
            throw new NotImplementedException();
        }

        // no action, not modal
        public void ReturnResult(DialogResult result)
        {
        }

        // add a string definition dynamically add to list.  errmsg if something is wrong
        public string Add(string instr)       // add a string definition dynamically add to list.  errmsg if something is wrong
        {
            return Entries.Add(instr);
        }
        // remove a control from the list, both visually and from entries
        public bool Remove(string controlname)
        {
            return Entries.Remove(controlname);
        }
        // get control by name
        public Control GetControl(string controlname)
        {
            return Entries.GetControl(controlname);
        }
        // get control by name
        public bool Get(string controlname, out string value)
        {
            return Entries.Get(controlname, out value);
        }
        public string Get(string controlname)
        {
            return Entries.Get(controlname, out string value) ? value : null;
        }
        // Return GetValue() by controlname, null if can't get
        public T GetValue<T>(string controlname)
        {
            return Entries.GetValue<T>(controlname);
        }

        // suspend resume
        public bool Suspend(string controlname)
        {
            return Entries.Suspend(controlname);
        }
        public bool Resume(string controlname)
        {
            return Entries.Resume(controlname);
        }

        // Set value of control by string value
        public bool Set(string controlname, string value, bool replaceescapes)
        {
            return Entries.Set(controlname, value, replaceescapes);
        }
        // add text to rich text box at bottom and scroll
        public bool AddText(string controlname, string text)
        {
            return Entries.AddText(controlname, text);
        }
        public void SetCheckedList(IEnumerable<string> controlnames, bool state)
        {
            Entries.SetCheckedList(controlnames, state);
        }
        // radio button this set, to 1 entry, or to N max
        public void RadioButton(string startingcontrolname, string controlhit, int max = 1)
        {
            Entries.RadioButton(startingcontrolname, controlhit, max);
        }
        // null is good
        public string AddSetRows(string controlname, string rowstring)
        {
            return Entries.AddSetRows(controlname, rowstring);
        }
        public string AddSetRows(string controlname, object rowcommands)        // rowcommands is a JArray
        {
            return Entries.AddSetRows(controlname, rowcommands);
        }
        public bool InsertColumn(string controlname, int position, string type, string headertext, int fillsize, string sortmode)
        {
            return Entries.InsertColumn(controlname, position, type, headertext, fillsize, sortmode);
        }

        public bool RemoveColumns(string controlname, int position, int count)
        {
            return Entries.RemoveColumns(controlname, position, count);
        }

        public bool SetRightClickMenu(string controlname, string[] tags, string[] items)
        {
            return Entries.SetRightClickMenu(controlname, tags, items);
        }
        public object GetDGVColumnSettings(string controlname)
        {
            return Entries.GetDGVColumnSettings(controlname);
        }
        public bool SetDGVColumnSettings(string controlname, object settings)
        {
            return Entries.SetDGVColumnSettings(controlname, settings);
        }
        public bool SetDGVColumnSettings(string controlname, string settings)
        {
            return Entries.SetDGVColumnSettings(controlname, settings);
        }
        public bool SetDGVSettings(string controlname,  bool columnreorder, bool percolumnwordwrap, bool allowrowheadervisibilityselection, bool singlerowselect)
        {
            return Entries.SetDGVSettings(controlname, columnreorder, percolumnwordwrap, allowrowheadervisibilityselection, singlerowselect);
        }
        public bool SetWordWrap(string controlname, bool wordwrap)
        {
            return Entries.SetWordWrap(controlname, wordwrap);
        }

        public bool Clear(string controlname)
        {
            return Entries.Clear(controlname);
        }
        public int RemoveRows(string controlname, int start, int count)
        {
            return Entries.RemoveRows(controlname, start, count);
        }
        public void CloseDropDown()
        {
            Entries.CloseDropDown();
        }
        public bool IsAllValid()
        {
            return Entries.IsAllValid();
        }

        public bool SetEnable(string controlname, bool enabled)
        {
            return Entries.Enable(controlname, enabled);
        }

        public bool SetVisible(string controlname, bool visible)
        {
            return Entries.Visible(controlname, visible);
        }

        public bool IsEnabled(string controlname)
        {
            return Entries.Find(controlname)?.Control?.Enabled ?? false;
        }

        public bool IsVisible(string controlname)
        {
            return Entries.Find(controlname)?.Control?.Visible ?? false;
        }

        public bool GetPosition(string controlname, out Rectangle r)    // in design units
        {
            return Entries.GetPosition(controlname, out r, this.FindForm().CurrentAutoScaleFactor());
        }
        public bool SetPosition(string controlname, Point p)    // in design units
        {
            return Entries.SetPosition(controlname, p, this.FindForm().CurrentAutoScaleFactor());
        }
        public bool SetSize(string controlname, Size s)    // in design units
        {
            return Entries.SetSize(controlname, s, this.FindForm().CurrentAutoScaleFactor());
        }


        #endregion

        #region Variables

        private ExtPanelVertScroll contentpanel;
        private ExtPanelVertScrollWithBar vertscrollpanel;
        private Size initialscrollpanelsize;

        #endregion
    }
}
