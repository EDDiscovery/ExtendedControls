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
    // Represents a check list, with optional group options, and standard options
    // you can either pre-configure the standard options or pass them thru the Filter function which presents the menu

    public class CheckedIconListBoxFilterForm
    {
        public Action<string,Object> SaveBack;         // called to save back value
        public Action<CheckedIconListBoxFilterForm, Object> Changed;       // called after save back to say fully changed.
        public bool AllOrNoneBack { get; set; } = true;            // use to control if ALL or NOne is reported, else its all entries or empty list

        private ExtendedControls.CheckedIconListBoxForm cc;
        private Object tagback;

        private class Options
        {
            public string Name;
            public string Itemlist;
            public Image Image;
        }

        private List<Options> groupoptions = new List<Options>();
        private List<Options> standardoptions = new List<Options>();

        private int ReservedEntries { get { return 2 + groupoptions.Count(); } }

        public void AddGroupOption(string name, string items, Image img = null)      // group option
        {
//            System.Diagnostics.Debug.WriteLine("Add group " + name + "=" + items);
            groupoptions.Add(new Options() { Name = name, Itemlist = items, Image = img });
        }

        public void AddStandardOption(string name, Image img = null)                // standard option
        {
            standardoptions.Add(new Options() { Name = name, Image = img });
        }

        // present below control
        public void Filter(string settings, Control ctr, Form parent, List<string> list = null, List<Image> images = null, Object tag = null)
        {
            Filter(settings, ctr.PointToScreen(new Point(0, ctr.Size.Height)), new Size(ctr.Width * 3, 600), parent, list, images);
        }

        public void Filter(string settings, Point p, Size s, Form parent, List<string> list = null, List<Image> images = null, Object tag = null)
        {
            if (cc == null)
            {
                cc = new ExtendedControls.CheckedIconListBoxForm();

                cc.AddItem("All".Tx());       // displayed, translate
                cc.AddImageItem(Properties.Resources.All);

                cc.AddItem("None".Tx());
                cc.AddImageItem(Properties.Resources.None);

                foreach (var x in groupoptions)
                {
                    cc.AddItem(x.Name);
                    if (x.Image != null)
                        cc.AddImageItem(x.Image);
                }

                foreach (var x in standardoptions)
                {
                    cc.AddItem(x.Name);
                    if (x.Image != null)
                        cc.AddImageItem(x.Image);
                }

                if (list != null)
                    cc.AddItems(list.ToArray());

                if (images != null)
                    cc.AddImageItems(images);

                cc.SetChecked(settings);

                SetFilterSet();

                cc.FormClosed += FilterClosed;
                cc.CheckedChanged += CheckChanged;
                cc.PositionSize(p, s);
                cc.LargeChange = cc.ItemCount * Properties.Resources.All.Height / 40;   // 40 ish scroll movements

                ThemeableFormsInstance.Instance.ApplyToControls(cc, applytothis: true);

                tagback = tag;

                cc.Show(parent);
            }
            else
                cc.Close();
        }

        private void SetFilterSet()
        {
            string list = cc.GetChecked(ReservedEntries);       // using All or None.. on items beyond reserved entries
                                                                //            System.Diagnostics.Debug.WriteLine("Checked" + list);
            cc.SetChecked(list.Equals("All"), 0, 1);
            cc.SetChecked(list.Equals("None"), 1, 1);

            int p = 2;
            foreach (var eo in groupoptions)
            {
                //System.Diagnostics.Debug.WriteLine("Filter check for " + eo.Itemlist);
                cc.SetChecked(list.Equals(eo.Itemlist), p++, 1);
            }
        }

        private void CheckChanged(Object sender, ItemCheckEventArgs e)          // called after check changed for the new system
        {
            if (e.NewValue == CheckState.Checked)       // all or none set all of them
            {
                if (e.Index <= 1)
                {
                    cc.SetChecked(e.Index == 0, ReservedEntries);
                }
                else if (e.Index < ReservedEntries)
                {
                    cc.SetChecked(false, ReservedEntries);
                    cc.SetChecked(groupoptions[e.Index - 2].Itemlist);
                }
            }

            SetFilterSet();
        }

        private void FilterClosed(Object sender, FormClosedEventArgs e)
        {
            SaveBack?.Invoke(cc.GetChecked(ReservedEntries,AllOrNoneBack),tagback);
            cc = null;
            Changed?.Invoke(this, tagback);
        }
    }
}
