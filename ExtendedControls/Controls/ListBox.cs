/*
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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtListBox : Control, IThemeable
    {
        // BackColor paints the whole control - set Transparent if you don't want this. (but its a fake transparent note).
        // Text is in ForeColor
        public Color SelectionBackColor { get; set; } = Color.Gray;     // the area actually painted (Not system)
        public Color SelectionBackColor2 { get; set; } = Color.Gray;     // the area actually painted (Not system)
        public float BackGradientDirection { get { return gradientdirection; } set { gradientdirection = value; Invalidate(); } }
        public Color BorderColor { get; set; } = Color.Red;             // not system
        public Color SelectionColor { get; set; } = Color.Silver;       // solid selection bar
        public Color ItemSeperatorColor { get; set; } = Color.Red;

        public ExtScrollBar ScrollBar { get; set; }

        public FlatStyle FlatStyle { get { return flatstyle; } set { SetFlatStyle(value); } }

        // in all styles

        public List<string> Items { get { return items; } set           // allow items to be changed
            { items = value;
                lbsys.Items.Clear();
                lbsys.Items.AddRange(value.ToArray());
                Invalidate(true); Update();
            } }

        public List<Image> ImageItems { get { return imageitems; } set { imageitems = value; } }

        public int[] ItemSeperators { get; set; } = null;     // set to array giving index of each separator

        // in non standard styles

        public bool FitToItemsHeight { get; set; } = true;              // if set, move the border to integer of item height.
        public bool FitImagesToItemHeight { get; set; } = false;        // if set images scaled to fit within item height

        public int ScrollBarWidth { get { return Font.ScaleScrollbar(); } }

        // All modes
        public int SelectedIndex { get { return selectedindex; } set { if (value != selectedindex) { selectedindex = value; Invalidate(); } } }

        public delegate void OnSelectedIndexChanged(object sender, EventArgs e, bool key);
        public event OnSelectedIndexChanged SelectedIndexChanged;

        public delegate void OnKeyPressed(object sender, KeyPressEventArgs e);
        public event OnKeyPressed KeyPressed;

        public delegate void OnAnyOtherKeyPressed(object sender, KeyEventArgs e);
        public event OnAnyOtherKeyPressed OtherKeyPressed;

        public ExtListBox() : base()
        {
            items = new List<string>();
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            ScrollBar = new ExtScrollBar();
            ScrollBar.SmallChange = 1;
            ScrollBar.LargeChange = 1;
            Controls.Add(ScrollBar);
            ScrollBar.Visible = false;
            ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(vScroll);

            lbsys = new ListBox();
            this.Controls.Add(lbsys);
            lbsys.SelectedIndexChanged += lbsys_SelectedIndexChanged;
            lbsys.DrawItem += Lbsys_DrawItem;
            lbsys.DrawMode = DrawMode.OwnerDrawFixed;
        }

        public void SetFlatStyle( FlatStyle v)
        {
            flatstyle = v;
            lbsys.Visible = (flatstyle == FlatStyle.System);
            this.Invalidate();
        }

        // Measure width and height of line, maximum to items and imageitems.
        public Size MeasureItems(Graphics g)
        {
            using (StringFormat fmt = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap })
            {
                Size p = g.MeasureItems(Font, Items.ToArray(), fmt);
                if (imageitems != null)
                {
                    int maxwidth = imageitems.Max(x => x.Width);
                    p.Width += FitImagesToItemHeight ? (int)(Font.GetHeight() + 2) : maxwidth;
                }
                return p;
            }
        }

        #region Implementation
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (lbsys != null && flatstyle == FlatStyle.System && this.Width > 0)
            {
                lbsys.Width = this.Width;
                lbsys.Height = this.Height;
                lbsys.ItemHeight = (int)Font.GetHeight() + 2;
            }
        }

        private void CalculateLayout()
        {
            bordersize = 0;

            if (FlatStyle != FlatStyle.System && !BorderColor.IsFullyTransparent())
                bordersize = 2;

            int items = (Items != null) ? Items.Count() : 0;
            itemslayoutestimatedon = items;

            fontusedforestimate = Font;

            itemheight = (int)Font.GetHeight() + 2;
            lbsys.ItemHeight = itemheight;

            displayableitems = (ClientRectangle.Height-bordersize*2) / itemheight;            // number of items to display

            if (items > 0 && displayableitems > items)
                displayableitems = items;

            mainarea = new Rectangle(bordersize, bordersize, 
                            ClientRectangle.Width - bordersize * 2, 
                            (FitToItemsHeight) ? (displayableitems * itemheight) : (ClientRectangle.Height - bordersize*2));
            borderrect = mainarea;
            borderrect.Inflate(bordersize,bordersize);
            borderrect.Width--; borderrect.Height--;        // adjust to rect not area.

            //System.Diagnostics.Debug.WriteLine("List box" + mainarea + " " + items + "  " + displayableitems);

            if ( items > displayableitems )
            {
                ScrollBar.Location = new Point(mainarea.Right - ScrollBarWidth, mainarea.Y);
                mainarea.Width -= ScrollBarWidth;
                ScrollBar.Size = new Size(ScrollBarWidth, mainarea.Height);
                ScrollBar.Minimum = 0;
                ScrollBar.Maximum = Items.Count - displayableitems;
                ScrollBar.Visible = true;
                ScrollBar.FlatStyle = FlatStyle;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.FlatStyle == FlatStyle.System)
                return;

            //System.Diagnostics.Debug.WriteLine("Updated list control H=" + itemheight);

            if (Items != null && itemslayoutestimatedon != Items.Count() || Font != fontusedforestimate )  // item count changed, rework it out.
                CalculateLayout();

            if (firstindex < 0)                                           // if invalid (at start)
            {
                if (SelectedIndex == -1 || Items == null)                  // screen out null..
                    firstindex = 0;
                else
                {
                    firstindex = SelectedIndex;

                    if (firstindex + displayableitems > Items.Count)        // no point leaving the display half populated
                    {
                        firstindex = Items.Count - displayableitems;        // go back..
                        if (firstindex < 0)                                 // if too far (because displayable items > list size)
                            firstindex = 0;
                    }
                }

                ScrollBar.Value = firstindex;
            }

            focusindex = (focusindex >= 0) ? focusindex : ((SelectedIndex > 0) ? SelectedIndex : 0);

            using (Pen p = new Pen(this.BorderColor))
            {
                for (int i = 0; i < bordersize; i++)
                {
                    var brect = new Rectangle(borderrect.Left + i, borderrect.Top + i, borderrect.Width - i * 2, borderrect.Height - i * 2);
                    e.Graphics.DrawRectangle(p, borderrect);
                }
            }

            if (!this.SelectionBackColor.IsFullyTransparent())
            {
                Brush backb;
                if (FlatStyle == FlatStyle.Popup)
                {
                    backb = new System.Drawing.Drawing2D.LinearGradientBrush(mainarea, SelectionBackColor, SelectionBackColor2, BackGradientDirection);
                    //System.Diagnostics.Debug.WriteLine($"Listbox draw slider at {BackGradientDirection}");
                }
                else
                    backb = new SolidBrush(SelectionBackColor);

                e.Graphics.FillRectangle(backb, mainarea);
                backb.Dispose();
            }

            if (Items != null && Items.Count > 0)
            {
                Rectangle totalarea = mainarea;     // total width area
                totalarea.Height = itemheight;
                Rectangle textarea = totalarea;     // where we draw text
                Rectangle imagearea = totalarea;

                if ( imageitems != null )           // if we have images, allocate space between the 
                {
                    if (FitImagesToItemHeight)
                    {
                        imagearea = new Rectangle(imagearea.X, imagearea.Y, itemheight - 1, itemheight - 1);
                        textarea.X += imagearea.Width + 1;
                    }
                    else
                    {
                        int maxwidth = imageitems.Max(x => x.Width);
                        textarea.X += maxwidth;
                        imagearea.Width = maxwidth;
                    }
                }

                int offset = 0;

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (StringFormat f = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap })
                using (Brush textb = new SolidBrush(this.ForeColor))
                using (Brush highlight = new SolidBrush(SelectionColor))
                {
//                    System.Diagnostics.Debug.WriteLine("Draw LB in " + Font );

                    foreach (string s in Items)
                    {   // if not fitting to items height, 
                        if (offset >= firstindex && offset < firstindex + displayableitems + (FitToItemsHeight ? 0 : 1))
                        {
                            if (offset == focusindex)
                            {
                                e.Graphics.FillRectangle(highlight, totalarea);
                            }

                            if (imageitems != null && offset < imageitems.Count)
                            {
                                e.Graphics.DrawImage(imageitems[offset], imagearea);
                                //System.Diagnostics.Debug.WriteLine(offset + " Image is " + imagearea);
                            }

                            e.Graphics.DrawString(s, this.Font, textb, textarea, f);

                            if (ItemSeperators != null && Array.IndexOf(ItemSeperators, offset) >= 0)
                            {
                                using (Pen p = new Pen(ItemSeperatorColor))
                                {
                                    e.Graphics.DrawLine(p, new Point(textarea.Left, textarea.Top), new Point(textarea.Right, textarea.Top));
                                }
                            }

                            totalarea.Y += itemheight;
                            textarea.Y = imagearea.Y = totalarea.Y;
                        }

                        offset++;
                    }
                }

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            }
        }

        private void Lbsys_DrawItem(object sender, DrawItemEventArgs e) // for system draw with ICON
        {
            e.DrawBackground();

            using (Brush textb = new SolidBrush(this.ForeColor))
            {
                Rectangle textarea = e.Bounds;
                Rectangle imagearea = e.Bounds;

                if (imageitems != null)
                {
                    if (FitImagesToItemHeight)
                    {
                        imagearea = new Rectangle(imagearea.X, imagearea.Y, textarea.Height - 1,textarea.Height - 1);
                        textarea.X += imagearea.Width + 1;
                    }
                    else
                    {
                        int maxwidth = imageitems.Max(x => x.Width);
                        textarea.X += maxwidth;
                        imagearea.Width = maxwidth;
                    }

                    if (e.Index < imageitems.Count)
                    {
                        e.Graphics.DrawImage(imageitems[e.Index], imagearea);
                    }
                }

                using (StringFormat f = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap })
                {
                    e.Graphics.DrawString(items[e.Index], e.Font, textb, textarea, f);
                }

                if (ItemSeperators != null && Array.IndexOf(ItemSeperators, e.Index) >= 0)
                {
                    using (Pen p = new Pen(ItemSeperatorColor))
                    {
                        e.Graphics.DrawLine(p, new Point(textarea.Left, textarea.Top), new Point(textarea.Right, textarea.Top));
                    }
                }
            }
            e.DrawFocusRectangle();
        }

        protected void vScroll(object sender, ScrollEventArgs e)
        {
            if (firstindex != e.NewValue)
            {
                firstindex = e.NewValue;
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int items = (Items != null) ? Items.Count() : 0;

            if (items > 0 && itemheight > 0 )       // if any items and we have done a calc layout.. just to check
            {
                int index = firstindex + e.Location.Y / itemheight;

                if (index >= items)                 // due to the few pixels for border.  we let them have this
                    index = items - 1;

                SelectedIndex = index;
                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(this, new EventArgs(), false);
            }
        }

        protected void ScrollUpOne()
        {
            if (firstindex > 0)
            {
                firstindex--;
                ScrollBar.Value = firstindex;
                Invalidate();
            }
        }
        protected void ScrollDownOne()
        {
            if (Items != null && firstindex < Items.Count() - displayableitems)
            {
                firstindex++;
                ScrollBar.Value = firstindex;
                Invalidate();
            }
        }

        protected void FocusUpOne()
        {
            if (focusindex > 0)
            {
                focusindex--;
                Invalidate();
                if (focusindex < firstindex)
                    ScrollUpOne();
            }
        }

        protected void FocusDownOne()
        {
            if (Items != null && focusindex < Items.Count()-1)
            {
                focusindex++;
                Invalidate();
                if (focusindex >= firstindex + displayableitems)
                    ScrollDownOne();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (flatstyle == FlatStyle.System)
                return;

            if (e.Delta > 0)
                ScrollUpOne();
            else
                ScrollDownOne();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (flatstyle == FlatStyle.System)
                return;
            else if (itemheight > 0)  // may not have been set yet
            {
                int y = e.Location.Y;
                int index = (y / itemheight) + firstindex;
                focusindex = index;
                Invalidate();
            }
        }
        
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)        // grab these nav keys
                return true;
            else
                return base.IsInputKey(keyData);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (KeyPressed != null)
               KeyPressed(this, e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            KeyDownAction(e);
        }

        public void KeyDownAction(KeyEventArgs e)
        { 
            if (flatstyle != FlatStyle.System)
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) || (e.Alt && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)))
                {
                    SelectedIndex = focusindex;
                    SelectedIndexChanged?.Invoke(this, new EventArgs(), true);
                }
                else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
                    FocusDownOne();
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                    FocusUpOne();
            }

            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                OtherKeyPressed?.Invoke(this, e);
            }
        }

        private void lbsys_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndex = lbsys.SelectedIndex;
            SelectedIndexChanged?.Invoke(this, new EventArgs(), true);
        }

        public void Repaint()
        {
            this.Invalidate(true);
        }

        public bool Theme(Theme t, Font fnt)
        {
            ForeColor = t.ListBoxTextColor;
            ItemSeperatorColor = t.ListBoxBorderColor;

            if (t.IsButtonSystemStyle)
            {
                FlatStyle = FlatStyle.System;
            }
            else
            {
                BackColor = t.Form;
                SelectionBackColor = t.ListBoxBackColor;
                SelectionBackColor2 = t.ListBoxBackColor2;
                SelectionColor = t.ListBoxBackColor.Multiply(t.MouseSelectedScaling);
                BorderColor = t.ListBoxBorderColor;
                FlatStyle = t.ButtonFlatStyle;
                BackGradientDirection = t.ListBoxBackGradientDirection;

                ScrollBar.ForeColor = t.ListBoxScrollArrow;
                ScrollBar.BackColor = t.Form;
                ScrollBar.BorderColor = ScrollBar.ThumbBorderColor = ScrollBar.ArrowBorderColor = t.ListBoxBorderColor;
                ScrollBar.SliderColor = t.ListBoxSliderBack;
                ScrollBar.SliderColor2 = t.IsButtonGradientStyle ? t.ListBoxSliderBack2 : t.ListBoxSliderBack;
                ScrollBar.SliderDrawAngle = t.ListBoxSliderGradientDirection;
                ScrollBar.ArrowButtonColor = t.ListBoxScrollArrowBack;
                ScrollBar.ArrowButtonColor2 = t.IsButtonGradientStyle ? t.ListBoxScrollArrowBack2 : t.ListBoxScrollArrowBack;
                ScrollBar.ThumbButtonColor = t.ListBoxScrollButtonBack;
                ScrollBar.ThumbButtonColor2 = t.IsButtonGradientStyle ? t.ListBoxScrollButtonBack2 : t.ListBoxScrollButtonBack;
                ScrollBar.ThumbDrawAngle = t.ListBoxScrollButtonGradientDirection;
                ScrollBar.MouseOverButtonColor = ScrollBar.ArrowButtonColor.Multiply(t.MouseOverScaling);
                ScrollBar.MouseOverButtonColor2 = ScrollBar.ArrowButtonColor2.Multiply(t.MouseOverScaling);
                ScrollBar.MousePressedButtonColor = ScrollBar.ArrowButtonColor.Multiply(t.MouseSelectedScaling);
                ScrollBar.MousePressedButtonColor2 = ScrollBar.ArrowButtonColor2.Multiply(t.MouseSelectedScaling);
                ScrollBar.FlatStyle = t.ButtonFlatStyle;

                //System.Diagnostics.Debug.WriteLine($"ListBox {Name} slider {ScrollBar.SliderColor}->{ScrollBar.SliderColor2} : arrow {ScrollBar.ArrowButtonColor}->{ScrollBar.ArrowButtonColor2} : thm {ScrollBar.ThumbButtonColor}->{ScrollBar.ThumbButtonColor2}");
            }

            Repaint();            // force a repaint as the individual settings do not by design.

            return false;
        }

        #endregion

        private Rectangle borderrect, mainarea;
        private int bordersize;
        private int itemslayoutestimatedon = -1;
        private Font fontusedforestimate = null;
        private int displayableitems = -1;
        private int firstindex = -1;
        private int focusindex = -1;
        private int selectedindex = -1;
        private ListBox lbsys;
        private List<string> items;
        private List<Image> imageitems;
        private int itemheight;     // autoset
        private FlatStyle flatstyle = FlatStyle.System;
        private float gradientdirection = 90F;

    }
}



