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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

/* Idea from : http://www.codeproject.com/Articles/91387/Painting-Your-Own-Tabs-Second-Edition
 * These parts are provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

namespace ExtendedControls
{
    public class ExtTabControl : TabControl, IThemeable
    {
        #region Properties

        // Invalidate to repaint - only useful if not system Flatstyle
        public Color TabControlBorderColor { get; set; } = Color.DarkGray;       //Selected tabs are outlined in this
        public Color TabControlBorderBrightColor { get; set; } = Color.LightGray;

        public Color TabNotSelectedBorderColor { get; set; } = Color.Gray;     //Unselected tabs are outlined in this

        public Color TabNotSelectedColor { get; set; } = Color.Gray;            // tabs are filled with this
        public Color TabSelectedColor { get; set; } = Color.LightGray;
        public Color TabMouseOverColor { get; set; } = Color.White;

        public Color TextSelectedColor { get; set; } = SystemColors.ControlText;             // text is painted in this..
        public Color TextNotSelectedColor { get; set; } = SystemColors.ControlText;

        public float TabColorScaling { get; set; } = 0.5F;                      // gradiant fill..
        public float TabDisabledScaling { get; set; } = 0.5F;                   // how much darker if not selected.

        public Color TabBackgroundColor { get { return tabbackcolor;} set { tabbackcolor = value; Invalidate(); } }

        public int MinimumTabWidth { set { SendMessage(0x1300 + 49, IntPtr.Zero, (IntPtr)value); } }

        // Auto Invalidates
        // style is Flat, Popup (gradient) and System
        public FlatStyle FlatStyle { get { return flatstyle; } set { if ( value != flatstyle ) ChangeFlatStyle(value); } }

        // attach a tab style class which determines the shape and formatting.
        public TabStyleCustom TabStyle { get { return tabstyle; } set { if (value != tabstyle ) ChangeTabStyle(value); } }

        // change both

        public void SetStyle(FlatStyle fsstyle, TabStyleCustom tabstylep)
        {
            if (fsstyle != flatstyle || tabstylep != tabstyle)
            {
                flatstyle = fsstyle;
                tabstyle = tabstylep;
                if (AutoForceUpdate)
                    ForceUpdate();
            }
        }

        // this does the ForceUpdate() call when things change.  But its slow, may be better to do manually
        public bool AutoForceUpdate { get; set; } = true;           

        // reordering 
        public bool AllowDragReorder { get; set; } = false;
        // Tab clicked.. reports last tab clicked
        public int LastTabClicked { get; private set; } = -1;
        public void ClearLastTab() { LastTabClicked = -1; }

        #endregion

        #region Initialisation
        public ExtTabControl() : base()
        {
        }
        
        public TabPage FindTabPageByName(string name)
        {
            foreach (TabPage p in TabPages)
            {
                if (p.Text.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return p;
                }
            }

            return null;
        }

        public int CalculateMinimumTabWidth()                                // given fonts and the tab text, whats the minimum width?
        {
            Graphics gr = Parent.CreateGraphics();

            int minsize = 16;

            foreach (TabPage p in TabPages)
            {
                //Console.WriteLine("Text is " + p.Text);
                SizeF sz = gr.MeasureString(p.Text, this.Font);

                if (sz.Width > minsize)
                    minsize = (int)sz.Width + 1;  // +1 due to float round down..
            }

            gr.Dispose();

            return minsize;
        }

        public int TabAt(Point position)
        {
            int count = TabCount;
            for (int i = 0; i < count; i++)
            {
                if (GetTabRect(i).Contains(position))
                    return i;
            }

            return -1;
        }

        // Force the back bitmap to be repainted
        public void ResetInvalidate()
        {
            backImageControlBitmap?.Dispose();
            backImageControlBitmap = null;
            Invalidate();
        }

        #endregion

        #region Mouse

        protected override void OnMouseDown(MouseEventArgs e)
        {
            LastTabClicked = lasttabclickedinside = TabAt(e.Location);
            base.OnMouseDown(e);
            //System.Diagnostics.Debug.WriteLine($"Tab Clicked on {LastTabClicked}");
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            int currentmouseover = mouseover;

            mouseover = TabAt(e.Location);

            //System.Diagnostics.Debug.WriteLine($"Tab Mouse Move {currentmouseover} to {mouseover}");

            if (e.Button == MouseButtons.Left && lasttabclickedinside != -1 && AllowDragReorder)
            {
                if (lasttabclickedinside != mouseover && mouseover != -1)
                {
                    TabPage r = TabPages[lasttabclickedinside];
                    TabPages[lasttabclickedinside] = TabPages[mouseover];
                    TabPages[mouseover] = r;
                    lasttabclickedinside = LastTabClicked = mouseover;
                    SelectedIndex = mouseover;
                }
            }
            else
            {
                if (mouseover != currentmouseover && flatstyle != FlatStyle.System)
                    Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (mouseover != -1)
            {
                //System.Diagnostics.Debug.WriteLine($"Tab Mouse Leave {mouseover}");
                mouseover = -1;
                lasttabclickedinside = -1;

                if (flatstyle != FlatStyle.System)
                    Invalidate();
            }
        }

        #endregion

        #region CustomPainting

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent == null || Width < 1 || Height < 1)
                return;

            //   System.Diagnostics.Debug.WriteLine($"Paint tab {e.ClipRectangle} {ClientRectangle} {backImageControlBitmap!=null}");

            // if not created, or different size, we need to remake the back image which has the sample of the background below the control

            if (backImageControlBitmap == null || backImageControlBitmap.Width != Width || backImageControlBitmap.Height != Height)  
            {
                backImageControlBitmap?.Dispose();

                backImageControlBitmap = new Bitmap(Width, Height);

                using (Graphics graphics = Graphics.FromImage(backImageControlBitmap))
                {
                    PaintEventArgs eg = new PaintEventArgs(graphics, ClientRectangle);
                    InvokePaintBackground(Parent, eg);  // we force it to paint into our bitmap
                    InvokePaint(Parent, eg);            // to sample the background color, which we paint up onto the screen

                   // System.Diagnostics.Debug.WriteLine($"TabControl Sampled background {Width}x{Height}");
                    // backImageControlBitmap.Save(@"c:\code\backimage.png", ImageFormat.Png);
                }
            }

            // now we make a bitmap to paint on..

            using (var backImageBitmap = new Bitmap(Width, Height))
            {
                using (var backImageGraphics = Graphics.FromImage(backImageBitmap))
                {
                    // if we are painting a color, fill it, else the sample of the background taken above is used as the base colour
                    if (tabbackcolor != Color.Transparent)
                        backImageGraphics.Clear(tabbackcolor);
                    else
                        backImageGraphics.DrawImageUnscaled(backImageControlBitmap, 0, 0);      

                    DrawBorder(backImageGraphics);

                    if (TabCount > 0)
                    {
                        for (int i = TabCount - 1; i >= 0; i--)
                        {
                            if (i != SelectedIndex)
                                DrawTab(i, backImageGraphics, false, mouseover == i);
                        }

                        // seen instances of SelectedIndex being set BEFORE tab up, meaning selected index is out of range
                        if (SelectedIndex >= 0 && SelectedIndex < TabCount)      // and if its selected, we did not draw it     -- seen it above TabCount.. protect
                            DrawTab(SelectedIndex, backImageGraphics, true, false);     // we paint the selected one last, in case it overwrites the other ones.
                    }

                    e.Graphics.DrawImageUnscaled(backImageBitmap, 0, 0);
                }
            }
        }

        private void DrawTab(int i, Graphics gr, bool selected, bool mouseover)
        {
            if (TabStyle == null)
                throw new ArgumentNullException("Custom style not attached");

            Color tabc1 = (Enabled) ? ((selected) ? TabSelectedColor : ((mouseover) ? TabMouseOverColor : TabNotSelectedColor)) : TabNotSelectedColor.Multiply(TabDisabledScaling);
            Color tabc2 = (FlatStyle == FlatStyle.Popup) ? tabc1.Multiply(TabColorScaling) : tabc1;
            Color taboutline = (selected) ? TabControlBorderColor : TabNotSelectedBorderColor;

            Image tabimage = null;

            TabStyle.DrawTab(gr, GetTabRect(i), i, selected, tabc1, tabc2, taboutline, Alignment);

            if (this.ImageList != null && this.TabPages[i].ImageIndex >= 0 && this.TabPages[i].ImageIndex < this.ImageList.Images.Count)
                tabimage = this.ImageList.Images[this.TabPages[i].ImageIndex];

            Color tabtextc = (Enabled) ? ((selected) ? TextSelectedColor : TextNotSelectedColor) : TextNotSelectedColor.Multiply(TabDisabledScaling);
            TabStyle.DrawText(gr, GetTabRect(i), i, selected, tabtextc, this.TabPages[i].Text, Font , tabimage);

            gr.SmoothingMode = SmoothingMode.Default;
        }

        private void DrawBorder(Graphics gr)
        {
            Pen dark = new Pen(TabControlBorderColor, 1.0F);
            Pen bright = new Pen(TabControlBorderBrightColor, 1.0F);
            Pen bright2 = new Pen(TabControlBorderBrightColor, 2.0F);

            var tabcontrolborder = new Rectangle(0, DisplayRectangle.Y - 2, ClientRectangle.Width - 1, DisplayRectangle.Height + 4);

            Rectangle b1 = new Rectangle(tabcontrolborder.X, tabcontrolborder.Y, tabcontrolborder.Width - 2, tabcontrolborder.Height);
            gr.DrawRectangle(dark, b1);

            b1.Inflate(-1, -1);
            gr.DrawRectangle(bright, b1);

            gr.DrawLine(bright2, b1.X + 2, b1.Y + 1, b1.X + 2, b1.Y + b1.Height);
            gr.DrawLine(bright2, b1.X + 3, b1.Y + b1.Height - 1, b1.X + b1.Width, b1.Y + b1.Height - 1);
            gr.DrawLine(bright2, tabcontrolborder.Width, tabcontrolborder.Y, tabcontrolborder.Width, tabcontrolborder.Y + tabcontrolborder.Height + 1);
            bright.Dispose();
            bright2.Dispose();
            dark.Dispose();
        }

        #endregion

        #region ChangeStyles

        private void ChangeTabStyle(TabStyleCustom fs)
        {
            tabstyle = fs;
            if (AutoForceUpdate)
                ForceUpdate();
        }

        private void ChangeFlatStyle(FlatStyle fs)
        {
            // set style back to system mode so that if we are going from flat->system we will force it there.
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, false);
            flatstyle = fs;
            if (AutoForceUpdate)
                ForceUpdate();
        }

        // turns out not needed, keep for reference
        // we intercept this as this occurs during Form Font scaling, and thus themeing, and force an update. The Font changed is not called.
        //protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        //{
        //    System.Diagnostics.Debug.WriteLine($"Tabcontrol ScaleControl.1 : {factor} {specified} = {Bounds} ml {Multiline} Font {Font}");
        //    base.ScaleControl(factor, specified);
        //    System.Diagnostics.Debug.WriteLine($"Tabcontrol ScaleControl.2 : {factor} {specified} = {Bounds} ml {Multiline} Font {Font}");
        //    ForceUpdate();
        //}

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            //System.Diagnostics.Debug.WriteLine($"Tabcontrol Font Change: {Font}  {Font.Height} : Item size {ItemSize} Multiline {Multiline} {flatstyle}");
            if (AutoForceUpdate)
                ForceUpdate();
        }

        public void ForceUpdate()
        {
            if (Parent == null)         // no parent, not attached yet
                return;

            //System.Diagnostics.Debug.WriteLine($"Tabcontrol Force Update In: flat {flatstyle} size {SizeMode} Multiline {Multiline} Font {Font}");

            bool systemmode = flatstyle == FlatStyle.System;

            // Put it back into system mode. if we don't put it back into system draw, it seems to never resize the tabs
            if (!systemmode)
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, false);      // go back to system

            // in anything but fixed, we set the itemsize and the minimum tab width
            if (SizeMode != TabSizeMode.Fixed)
            {
                int minsize = CalculateMinimumTabWidth();           // set the minimum size
                ItemSize = new Size(minsize, Math.Max(16,Font.Height + 4));       // set the item height, and minimum width in anything else but fixed
                MinimumTabWidth = minsize;
            }

            // if in multiline, it goes hairwire if its gets bigger. Clear multiline, set multiline. Winforms recreates the handle with and the sizing resets
            if (Multiline)
            {
                Multiline = false;
                Multiline = true;
            }

            // put it back into user draw if applicable
            if (!systemmode)
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);

            //System.Diagnostics.Debug.WriteLine($"Tabcontrol Force Update Out: flat {flatstyle} size {SizeMode} Multiline {Multiline} Font {Font}");
            ResetBitmap();

            //System.Diagnostics.Debug.WriteLine($"Tabcontrol Force Update Finish: flat {flatstyle} size {SizeMode} Multiline {Multiline} Font {Font}");

        }
        private void ResetBitmap()
        {
            backImageControlBitmap?.Dispose();
            backImageControlBitmap = null;
        }

        private IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
        {
            Message message = Message.Create(this.Handle, msg, wparam, lparam);
            this.WndProc(ref message);
            return message.Result;
        }

        #endregion

        #region Helpers

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                backImageControlBitmap?.Dispose();
            }
        }

        public bool Theme(Theme t, Font fnt)
        {
            AutoForceUpdate = false;        // make it slightly better

            if (t.IsButtonSystemStyle) // not system
            {
                FlatStyle = FlatStyle.System;
            }
            else
            {
                TabControlBorderColor = t.TabcontrolBorder.Multiply(0.6F);
                TabControlBorderBrightColor = t.TabcontrolBorder;
                TabNotSelectedBorderColor = t.TabcontrolBorder.Multiply(0.4F);
                TabNotSelectedColor = t.ButtonBackColor;
                TabSelectedColor = t.ButtonBackColor.Multiply(t.MouseSelectedScaling);
                TabMouseOverColor = t.ButtonBackColor.Multiply(t.MouseOverScaling);
                TextSelectedColor = t.ButtonTextColor;
                TextNotSelectedColor = t.ButtonTextColor.Multiply(0.8F);
                SetStyle(t.ButtonFlatStyle, new TabStyleAngled());
            }
            ForceUpdate();
            return true;
        }
        #endregion

        #region Members
        private Bitmap backImageControlBitmap = null;
        private Color tabbackcolor = Color.Transparent;
        private FlatStyle flatstyle = FlatStyle.System;
        private TabStyleCustom tabstyle = new TabStyleSquare();     // change for the shape of tabs.
        private int mouseover = -1;                                 // where the mouse if hovering
        int lasttabclickedinside = -1;                              // LastTabClicked is persistent.. this is wiped by leaving the control

        #endregion
    }
}

