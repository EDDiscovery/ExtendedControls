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
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    // puts a drop down autocomplete list on the autocomplete.  Access the TextBox directly

    public class ExtTextBoxAutoCompleteDropDown : Control
    {
        public ExtTextBoxAutoComplete TextBox;

        private Image img = Properties.Resources.ArrowDown;
        private ExtButton DropDown;

        public ExtTextBoxAutoCompleteDropDown() : base()
        {
            TextBox = new ExtTextBoxAutoComplete();

            DropDown = new ExtButton();
            DropDown.Image = img;
            DropDown.Click += DropDown_Click;
            Controls.Add(TextBox);
            Controls.Add(DropDown);
        }

        private void DropDown_Click(object sender, EventArgs e)
        {
            TextBox.Text = "";
            TextBox.ForceAutoComplete("");
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            TextBox.Location = new Point(0, 0);
            int bw = img.Width + 4;
            DropDown.Location = new Point(Width- bw, 0);
            TextBox.Size = new Size(Width - bw, Height);
            DropDown.Size = new Size(bw,TextBox.Height);
        }
    }
}

