﻿/*
 * Copyright 2016-2025 EDDiscovery development team
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace ExtendedControls
{
    public class ExtComboBox : Control, IThemeable
    {
        // ForeColor = text,
        // BackColor = control background colour 1
        public Color BackColor2 { get { return backColor2; } set { backColor2 = value; Invalidate(); } }
        public Color ControlBackground { get { return controlbackcolor; } set { controlbackcolor = value; Invalidate(true); } } // colour of unfilled control area if border is on or button

        public float MouseOverScalingColor { get; set; } = 1.3F;
        public Color BorderColor { get; set; } = Color.White;
        public float GradientDirection { get; set; } = 90F;
        [System.ComponentModel.BrowsableAttribute(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DropDownTheme DropDownTheme { get; set; } = new DropDownTheme();
        public bool DisableBackgroundDisabledShadingGradient { get; set; } = false;     // set, non system only, stop scaling for disabled state (useful for transparency)
        public float DisabledScaling { get; set; } = 0.5F;      // when disabled, scale down colours
        public FlatStyle FlatStyle { get; set; } = FlatStyle.System;
        
        public int SelectedIndex { get { return cbsystem.SelectedIndex; } set { cbsystem.SelectedIndex = value; base.Text = cbsystem.Text; Invalidate(); } }

        public ObjectCollection Items { get { return _items; } set { _items.Clear(); _items.AddRange(value.ToArray()); } }

        public override AnchorStyles Anchor { get { return base.Anchor; } set { base.Anchor = value; cbsystem.Anchor = value; } }
        public override DockStyle Dock { get { return base.Dock; } set { base.Dock = value; cbsystem.Dock = value; } }
        public override Font Font { get { return base.Font; } set { base.Font = value; cbsystem.Font = value; } }
        public override string Text { get { return base.Text; } set { base.Text = value; cbsystem.Text = value; Invalidate();  } }
        public System.Drawing.ContentAlignment TextAlign { get; set; } = System.Drawing.ContentAlignment.MiddleLeft;      // ONLY for non system combo boxes

        // BEWARE SET value/display before DATA SOURCE
        public object DataSource { get { return cbsystem.DataSource; } set { cbsystem.DataSource = value; } }
        public string ValueMember { get { return cbsystem.ValueMember; } set { cbsystem.ValueMember = value; } }
        public string DisplayMember { get { return cbsystem.DisplayMember; } set { cbsystem.DisplayMember = value; } }
        public object SelectedItem { get { return cbsystem.SelectedItem; } set { cbsystem.SelectedItem = value; base.Text = cbsystem.Text; Invalidate(); } }
        public object SelectedValue { get { return cbsystem.SelectedValue; } set { cbsystem.SelectedValue = value; } }
        public new Size Size { get { return cbsystem.Size; } set { cbsystem.Size = value; base.Size = value; } }

        public event EventHandler SelectedIndexChanged;

        public ExtComboBox()
        {
            //Text = "";
            this.cbsystem = new ComboBox();
            this.cbsystem.Name = Name + "_SystemComboBox";
            this.cbsystem.Dock = DockStyle.Fill;
            this.cbsystem.SelectedIndexChanged += cbsystem_SelectedIndexChanged;
            this.cbsystem.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbsystem.MouseLeave += cbsystem_MouseLeave;
            this.cbsystem.MouseEnter += cbsystem_MouseEnter;
            this.cbsystem.MouseUp += cbsystem_MouseUp;
            this.cbsystem.Resize += (s,e)=> { this.cbsystem.DropDownWidth = Math.Max( Width * 2,100); };
            this._items = new ObjectCollection(this.cbsystem);
            this.Controls.Add(this.cbsystem);
        }


        public void SetTipDynamically(ToolTip t, string text)// only needed for dynamic changes..
        {
            t.SetToolTip(this, text);
            t.SetToolTip(cbsystem, text);
        }                                       

        public ComboBox GetInternalSystemControl { get { return this.cbsystem; }  }

        bool firstpaint = true;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (firstpaint)
            {
                System.ComponentModel.IContainer ic = this.GetParentContainerComponents();

                ic?.CopyToolTips(this, new Control[] { this, cbsystem });

                firstpaint = false;
            }

            using (Brush highlight = new SolidBrush(controlbackcolor))
            {
                e.Graphics.FillRectangle(highlight, ClientRectangle);
            }


            base.OnPaint(e);

            if (this.FlatStyle != FlatStyle.System)
            {
                int extraborder = 1;
                int texthorzspacing = 1;

                int arrowwidth = Font.ScalePixels(20);

                textBoxBackArea = new Rectangle(ClientRectangle.X + extraborder, ClientRectangle.Y + extraborder,
                                                            ClientRectangle.Width - 2 * extraborder, ClientRectangle.Height - 2 * extraborder);

                topBoxTextArea = new Rectangle(ClientRectangle.X + extraborder + texthorzspacing, ClientRectangle.Y + extraborder,
                                        ClientRectangle.Width - 2 * extraborder - 2 * texthorzspacing - arrowwidth, ClientRectangle.Height - 2 * extraborder);

                arrowRectangleArea = new Rectangle(ClientRectangle.Width - arrowwidth - extraborder, ClientRectangle.Y + extraborder,
                                                    arrowwidth, ClientRectangle.Height - 2 * extraborder);

                topBoxOutline = new Rectangle(ClientRectangle.X, ClientRectangle.Y,
                                                ClientRectangle.Width - 1, ClientRectangle.Height - 1);

                int hoffset = arrowRectangleArea.Width/12 + 2;
                int voffset = arrowRectangleArea.Height / 4;
                arrowpt1 = new Point(arrowRectangleArea.Left + hoffset, arrowRectangleArea.Y + voffset);
                arrowpt2 = new Point(arrowRectangleArea.XCenter(), arrowRectangleArea.Bottom - voffset);
                arrowpt3 = new Point(arrowRectangleArea.Right - hoffset, arrowpt1.Y);

                arrowpt1c = new Point(arrowpt1.X, arrowpt2.Y);
                arrowpt2c = new Point(arrowpt2.X, arrowpt1.Y);
                arrowpt3c = new Point(arrowpt3.X, arrowpt2.Y);

                Brush textb;
                Pen p, p2;

                bool isenabled = Enabled && Items.Count > 0;

                if (isenabled)
                {
                    textb = new SolidBrush(this.ForeColor);
                    p = new Pen(BorderColor);
                    p2 = new Pen(ForeColor);
                    p2.Width = Font.ScaleSize(1.5f);
                }
                else
                {
                    textb = new SolidBrush(ForeColor.Multiply(DisabledScaling));
                    p = new Pen(BorderColor.Multiply(DisabledScaling));
                    p2 = null;
                }

                e.Graphics.DrawRectangle(p, topBoxOutline);

                Color bck1,bck2;

                if (isenabled)
                {
                    bck1 = (mouseover) ? BackColor.Multiply(MouseOverScalingColor) : BackColor;
                    bck2 = (mouseover) ? BackColor2.Multiply(MouseOverScalingColor): BackColor2;
                }
                else
                {
                    bck1 = DisableBackgroundDisabledShadingGradient ? BackColor : BackColor.Multiply(DisabledScaling);
                    bck2 = DisableBackgroundDisabledShadingGradient ? BackColor2 : BackColor2.Multiply(DisabledScaling);
                }

                Brush bbck;

                if (FlatStyle == FlatStyle.Popup && !DisableBackgroundDisabledShadingGradient)
                {
                    bbck = new System.Drawing.Drawing2D.LinearGradientBrush(textBoxBackArea, bck1, bck2, GradientDirection);
                }
                else
                {
                    bbck = new SolidBrush(bck1);
                }

                e.Graphics.FillRectangle(bbck, textBoxBackArea);

                //using (Brush test = new SolidBrush(Color.Red)) e.Graphics.FillRectangle(test, topBoxTextArea); // used to check alignment 

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                if (p2 != null )
                {
                    if (isActivated)
                    {
                        e.Graphics.DrawLine(p2, arrowpt1c, arrowpt2c);            // the arrow!
                        e.Graphics.DrawLine(p2, arrowpt2c, arrowpt3c);
                    }
                    else
                    {
                        e.Graphics.DrawLine(p2, arrowpt1, arrowpt2);            // the arrow!
                        e.Graphics.DrawLine(p2, arrowpt2, arrowpt3);
                    }
                }

                var txalign = Environment.OSVersion.Platform == PlatformID.Win32NT ? RtlTranslateAlignment(TextAlign) : TextAlign;      // MONO Bug cover over

                using (var fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(txalign))
                {
                    fmt.FormatFlags = StringFormatFlags.NoWrap;
                    e.Graphics.DrawString(this.Text, this.Font, textb, topBoxTextArea, fmt);
                }

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                bbck.Dispose();

                textb.Dispose();
                p.Dispose();

                if (p2 != null)
                    p2.Dispose();
            }
        }

        public void Repaint()
        {
            if (this.FlatStyle == FlatStyle.System)
            {
                cbsystem.Visible = true;
                this.Invalidate(true);
            }
            else
            {
                cbsystem.Visible = false;
                this.Invalidate(true);
            }
        }

        private void cbsystem_MouseEnter(object sender, EventArgs e)       // if cbsystem is active, fired.. pass onto our ME handler
        {
            //System.Diagnostics.Debug.WriteLine("CB sys Mouse enter " + _cbsystem.Size + " "  + this.Size);
            base.OnMouseEnter(e);
        }

        private void cbsystem_MouseLeave(object sender, EventArgs e)       // if cbsystem is active, fired.. pass onto our ML handler.
        {
            //System.Diagnostics.Debug.WriteLine("CB sys Mouse leave");
            base.OnMouseLeave(e);
        }

        private void cbsystem_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs eventargs)           // ours is active.  Fired when entered
        {
            //System.Diagnostics.Debug.WriteLine("CBC Enter , visible " + _cbsystem.Visible);
            if (!cbsystem.Visible)
            {
                base.OnMouseEnter(eventargs);
                mouseover = true;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            //System.Diagnostics.Debug.WriteLine("CBC Leave, activated" + isActivated + " visible " + _cbsystem.Visible);

            if (!cbsystem.Visible)
            {
                if (isActivated == false)
                    base.OnMouseLeave(eventargs);

                mouseover = false;
                Invalidate();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            //System.Diagnostics.Debug.WriteLine("Key press " + e.KeyCode + " Focus " + Focused );

            if (this.FlatStyle != FlatStyle.System && this.Items.Count > 0)
            {
                if (SelectedIndex < 0)
                    SelectedIndex = 0;

                if (e.Alt && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
                {
                    Activate();
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                {
                    if (SelectedIndex > 0)
                    {
                        cbsystem.SelectedIndex = SelectedIndex - 1;            // triggers _cbsystem_SelectedIndexChanged
                    }
                }
                else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
                {
                    if (SelectedIndex < this.Items.Count - 1)
                    {
                        cbsystem.SelectedIndex = SelectedIndex + 1;            // triggers _cbsystem_SelectedIndexChanged
                    }
                }
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (this.FlatStyle != FlatStyle.System && (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right))        // grab these nav keys
                return true;
            else
                return base.IsInputKey(keyData);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Activate();
        }

        // call after form show
        public void Activate()
        {
            if (Items.Count == 0 || !Enabled)
                return;

            dropdown = new ExtListBoxForm(this.Name + "_Listbox");

            DropDownTheme.Theme(dropdown.ListBox, ForeColor, BackColor, BorderColor);
            DropDownTheme.Theme(dropdown.ListBox.ScrollBar, BorderColor, this.Font);

            dropdown.SelectedIndex = this.SelectedIndex;
            dropdown.FlatStyle = this.FlatStyle;
            dropdown.Font = this.Font;
            dropdown.Items = this.Items.ToList();

            dropdown.PositionBelow(this);

            dropdown.Activated += customdropdown_Activated;
            dropdown.SelectedIndexChanged += customdropdown_SelectedIndexChanged;
            dropdown.OtherKeyPressed += customdropdown_OtherKeyPressed;
            dropdown.Deactivate += customdropdown_Deactivate;

            dropdown.Show(FindForm());
        }

        private void customdropdown_Deactivate(object sender, EventArgs e)
        {
            isActivated = false;
            this.Invalidate(true);
        }

        private void customdropdown_Activated(object sender, EventArgs e)
        {
            isActivated = true;
            this.Invalidate(true);
        }

        private void customdropdown_SelectedIndexChanged(object sender, EventArgs e, bool key)
        {
            int selectedindex = dropdown.SelectedIndex;
            isActivated = false;            // call has already closed the custom drop down..
            this.Invalidate(true);
            if (cbsystem.SelectedIndex != selectedindex)
                cbsystem.SelectedIndex = selectedindex; // triggers _cbsystem_SelectedIndexChanged, but only if we change the index..
            else
                cbsystem_SelectedIndexChanged(sender, e);      // otherwise, fire it off manually.. this is what the system box does, if the user clicks on it, fires it off
            Focus();

            base.OnMouseLeave(e);    // same as mouse 
        }

        private void customdropdown_OtherKeyPressed(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Escape )
            {
                dropdown.Close();
                isActivated = false;
                this.Invalidate(true);
            }
        }

        private void cbsystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = cbsystem.Text;
            this.Invalidate(true);

            if (this.Enabled && SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, e);
            }

            base.OnMouseLeave(e);    // same as mouse 
        }

        public bool Theme(Theme t, Font fnt)
        {
            ForeColor = t.ComboBoxTextColor;
            ControlBackground = t.ComboBoxBackColor;

            DisabledScaling = t.DisabledScaling;

            if (t.IsButtonSystemStyle)
            {
                FlatStyle = FlatStyle.System;
            }
            else
            {
                BackColor = t.ComboBoxBackColor;
                BackColor2 = t.ComboBoxBackColor2;
                GradientDirection = t.ComboBoxBackAndDropDownGradientDirection;
                BorderColor = t.ComboBoxBorderColor;
                MouseOverScalingColor = t.MouseOverScaling;

                DropDownTheme.SetFromCombo(t);
                FlatStyle = t.ButtonFlatStyle;
            }

            Repaint();            // force a repaint as the individual settings do not by design.

            return false;
        }

        public class ObjectCollection : IList<string>, ICollection<string>
        {
            private IList _explicitCollection;
            private ComboBox _combobox;

            private IList _collection
            {
                get
                {
                    if (_explicitCollection != null)
                    {
                        return _explicitCollection;
                    }
                    else if (_combobox.DisplayMember != null && _combobox.DataSource != null && _combobox.DataSource is ICollection)
                    {
                        return GetMembers((ICollection)_combobox.DataSource, _combobox.DisplayMember).ToList();
                    }
                    else
                    {
                        return _combobox.Items;
                    }
                }
            }

            protected IEnumerable<object> GetMembers(ICollection vals, string displaymember)
            {
                foreach (object val in vals)
                {
                    Type t = val.GetType();
                    PropertyInfo pi = t.GetProperty(displaymember);
                    object dm = pi.GetValue(val, new object[0]);
                    yield return dm;
                }
            }


            public ObjectCollection(ComboBox cb)
            {
                this._combobox = cb;
            }

            public ObjectCollection(IList<string> vals)
            {
                _explicitCollection = vals as IList;
            }


            public ObjectCollection(string[] vals)
            {
                _explicitCollection = vals.ToList() as IList;
            }


            public string this[int index]
            {
                get
                {
                    return _collection[index].ToNullSafeString();
                }

                set
                {
                    _collection[index] = value;
                }
            }

            public int Count
            {
                get
                {
                    return _collection.Count;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return _collection.IsReadOnly;
                }
            }

            public void Add(string item)
            {
                _collection.Add(item);
            }

            public void AddRange(IEnumerable<string> items)
            {
                foreach (var val in items)
                {
                    _collection.Add(val);
                }
            }

            public void Clear()
            {
                _collection.Clear();
            }

            public bool Contains(string item)
            {
                return _collection.Contains(item);
            }

            public void CopyTo(string[] array, int arrayIndex)
            {
                _collection.OfType<object>().Select(v => v.ToNullSafeString()).ToList().CopyTo(array, arrayIndex);
            }

            public IEnumerator<string> GetEnumerator()
            {
                return _collection.OfType<object>().Select(v => v.ToNullSafeString()).GetEnumerator();
            }

            public int IndexOf(string item)
            {
                return _collection.IndexOf(item);
            }

            public void Insert(int index, string item)
            {
                _collection.Insert(index, item);
            }

            public bool Remove(string item)
            {
                _collection.Remove(item);
                return true;
            }

            public void RemoveAt(int index)
            {
                _collection.RemoveAt(index);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public static implicit operator ObjectCollection(List<string> vals)
            {
                return new ObjectCollection(vals);
            }

            public static implicit operator ObjectCollection(string[] vals)
            {
                return new ObjectCollection(vals);
            }
        }

        protected ObjectCollection _items;

        private ComboBox cbsystem;
        private Rectangle topBoxTextArea, arrowRectangleArea, topBoxOutline, textBoxBackArea;
        private Point arrowpt1, arrowpt2, arrowpt3;
        private Point arrowpt1c, arrowpt2c, arrowpt3c;
        private bool isActivated = false;
        private bool mouseover = false;
        private ExtListBoxForm dropdown;
        private Color backColor2 = Color.Red;
        private Color controlbackcolor = SystemColors.Control;
    }
}
