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
    public class CheckedListBoxForm : Form
    {
        private CheckedListBox clb;
        public CheckedListBox.ObjectCollection Items { get { return clb.Items; } }
        public bool IsOpen { get; set; } = false;

        public event ItemCheckEventHandler CheckedChanged;

        private Point SetPosition { get; set; }
        private Size SetSize { get; set; }
        private bool ignorechangeevent = false;

        public CheckedListBoxForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            AutoSize = false;
            Padding = new Padding(0);
            clb = new CheckedListBox();
            clb.Visible = true;
            clb.CheckOnClick = true;
            clb.BackColor = Color.Orange;
            clb.ItemCheck += ItemCheck;
            Controls.Add(clb);
            IsOpen = true;
        }

        public void PositionSize( Point p , Size s )
        {
            SetPosition =p;
            SetSize = s;
        }

        public void PositionBelow( Control b , Size s )
        {
            Point p = b.PointToScreen(new Point(0, b.Size.Height));
            SetPosition = p;
            SetSize = s;
        }

        public void SetColour(Color backcolour, Color textc)
        {
            BackColor = backcolour;
            clb.BackColor = backcolour;
            clb.ForeColor = textc;
        }

        public void SetFont(Font fnt)
        {
            clb.Font = fnt;
        }

        public void SetChecked(string value, int ignore = 0)        // using ; as the separator
        {
            SetChecked(value.SplitNoEmptyStrings(';'));
        }

        public void SetChecked(List<string> list, int ignore = 0)   // null allowed
        {
            if ( list != null )
                SetChecked(list.ToArray(), ignore);
        }

        public void SetChecked(string[] list, int ignore = 0)       // empty array allowed
        {
            ignorechangeevent = true;

            bool all = list.Length == 1 && list[0].Equals("All");

            for (int i = ignore; i < clb.Items.Count; i++)
            {
                if (list.Contains(clb.Items[i]) || all)
                    clb.SetItemChecked(i, true);
            }
            ignorechangeevent = false;
        }

        public void SetChecked(bool c, int ignore = 0, int count = 0)
        {
            ignorechangeevent = true;
            if (count == 0)
                count = clb.Items.Count - ignore;

            for (int i = ignore; count-- > 0; i++)
                clb.SetItemChecked(i, c);
            ignorechangeevent = false;
        }

        public string GetChecked(int ignore = 0, bool allornone = true)            // semicolon list of options with trailing ;, or All, or None if selected
        {
            string ret = "";

            int total = 0;
            for (int i = ignore; i < clb.Items.Count; i++)
            {
                if (clb.GetItemCheckState(i) == CheckState.Checked)
                {
                    ret += clb.Items[i] + ";";
                    total++;
                }
            }

            if (allornone)
            {
                if (total == clb.Items.Count - ignore)
                    ret = "All";
                if (ret.Length == 0)
                    ret = "None";
            }

            return ret;
        }

        public List<string> GetCheckedList(int ignore = 0, bool allornone = true)
        {
            return GetChecked(ignore,allornone).SplitNoEmptyStartFinish(';').ToList();
        }

        public string[] GetCheckedArray(int ignore = 0, bool allornone = true)
        {
            return GetChecked(ignore,allornone).SplitNoEmptyStartFinish(';');
        }

        protected void ItemCheck(Object sender , ItemCheckEventArgs e )
        {
            if (CheckedChanged != null && !ignorechangeevent)
                CheckedChanged(sender, e);
        }

        protected override void OnShown(EventArgs e)
        {
            Location = SetPosition;
            Size = SetSize;
            clb.Size = SetSize;
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Close();
            IsOpen = false;
        }
    }
}
