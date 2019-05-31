﻿/*
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
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;

namespace ExtendedControls
{
    public class ThemeToolStripRenderer : ToolStripProfessionalRenderer//ToolStripSystemRenderer
    {
        public ToolStripCustomColourTable colortable;

        public ThemeToolStripRenderer() : base(new ToolStripCustomColourTable())
        {
            this.colortable = (ToolStripCustomColourTable)ColorTable;
            this.RoundedEdges = true;
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)            // called to determine text colour..
        {
            if (e.Item.Selected || e.Item.Pressed)
                e.TextColor = colortable.colMenuSelectedText;
            else
                e.TextColor = colortable.colMenuText;

            base.OnRenderItemText(e);
        }
    }

    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    public class ToolStripComboBoxCustom : System.Windows.Forms.ToolStripControlHost
    {
        public ExtComboBox ComboBox { get { return (ExtComboBox)base.Control; } }

        public ToolStripComboBoxCustom() : base(new ExtComboBox())
        {
        }

        public Color MouseOverBackgroundColor { get { return ComboBox.MouseOverBackgroundColor; } set { ComboBox.MouseOverBackgroundColor = value; } }
        public Color BorderColor { get { return ComboBox.BorderColor; } set { ComboBox.BorderColor = value; } }
        public Color DropDownBackgroundColor { get { return ComboBox.DropDownBackgroundColor; } set { ComboBox.DropDownBackgroundColor = value; } }
        public Color ScrollBarColor { get { return ComboBox.ScrollBarColor; } set { ComboBox.ScrollBarColor = value; } }
        public Color ScrollBarButtonColor { get { return ComboBox.ScrollBarButtonColor; } set { ComboBox.ScrollBarButtonColor = value; } }

        public FlatStyle FlatStyle { get { return ComboBox.FlatStyle; } set { ComboBox.FlatStyle = value; } }
        public float ButtonColorScaling { get { return ComboBox.ButtonColorScaling; } set { ComboBox.ButtonColorScaling = value; } }
        public int SelectedIndex { get { return ComboBox.SelectedIndex; } set { ComboBox.SelectedIndex = value; } }
        public ExtComboBox.ObjectCollection Items { get { return ComboBox.Items; } set { ComboBox.Items = value; } }
        public object DataSource { get { return ComboBox.DataSource; } set { ComboBox.DataSource = value; } }
        public string DisplayMember { get { return ComboBox.DisplayMember; } set { ComboBox.DisplayMember = value; } }
        public string ValueMember { get { return ComboBox.ValueMember; } set { ComboBox.ValueMember = value; } }
        public object SelectedItem { get { return ComboBox.SelectedItem; } set { ComboBox.SelectedItem = value; } }
        public object SelectedValue { get { return ComboBox.SelectedValue; } }

        public event EventHandler SelectedIndexChanged { add { ComboBox.SelectedIndexChanged += value; } remove { ComboBox.SelectedIndexChanged -= value; } }
    }
}
