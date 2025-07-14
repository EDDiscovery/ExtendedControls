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

namespace ExtendedControls
{
    public class ExtTabControl : TabControl, IThemeable, ITranslatableControl
    {
        #region Properties

        // BackColor is whole window control
        // ForeColor is not used

        // Invalidate to repaint - only useful if not system Flatstyle
        public Color TabControlBorderColor { get; set; } = Color.LightGray;     // bright colour
        public Color TabControlBorderColor2 { get; set; } = Color.DarkGray;     // darker colour

        public Color TabNotSelectedBorderColor { get; set; } = Color.Gray;     //Unselected tabs are outlined in this

        public Color TabNotSelectedColor { get; set; } = Color.LightGray;        // tabs are filled with this
        public Color TabNotSelectedColor2 { get; set; } = Color.Gray;            // tabs are filled with this
        public Color TextNotSelectedColor { get; set; } = SystemColors.ControlText;
        public float TabGradientDirection { get; set; } = 90F;

        public Color TabSelectedColor { get; set; } = Color.LightGray;
        public Color TabSelectedColor2 { get; set; } = Color.Gray;
        public Color TextSelectedColor { get; set; } = SystemColors.ControlText;             // text is painted in this..

        public Color TabMouseOverColor { get; set; } = Color.White;
        public Color TabMouseOverColor2 { get; set; } = Color.White;

        public float TabDisabledScaling { get; set; } = 0.5F;                   // how much darker if not selected.

        // ThemeColors only used if ThemeColourSet>=0.  
        public Color[] ThemeColors { get; set; } = new Color[4] { SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control };
        // -1 = system, 0 use tabstrip theme colours, else use panel set 1,2,3,4.. 
        public int ThemeColorSet { get; set; } = -1;
        public float TabBackgroundGradientDirection { get; set; } = 0F;

        // if you set this to a colour, the tab strip background becomes that colour, for transparency purposes.
        // If you set it to Color.Transparent, it goes to normal painting
        public Color PaintTransparentColor { get { return transparency; } set { transparency = value; Invalidate(); } }

        private Color transparency = Color.Transparent;

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

        public bool TranslateDoChildren => true;

        public void ClearLastTab() { LastTabClicked = -1; }

        #endregion

        #region Initialisation
        public ExtTabControl() : base()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
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
                if (mouseover != currentmouseover && FlatStyle != FlatStyle.System)
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

                if (FlatStyle != FlatStyle.System)
                    Invalidate();
            }
        }

        #endregion

        #region CustomPainting

        // we always draw now the tab background.  If you want it transparent, set the tab back colours to the transparent key

        const int topborder = 2;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent == null || Width < 1 || Height < 1)
                return;

            Rectangle tabarea = new Rectangle(0, 0, Width, DisplayRectangle.Y - topborder);   // less 2 for border area

            if (PaintTransparentColor != Color.Transparent) // if in transparent mode, ensure transparency of the top strip                                 
                e.Graphics.DrawFilledRectangle(ClientRectangle, PaintTransparentColor);
            else if (ThemeColorSet < 0)
            {
                using (Brush br = new SolidBrush(BackColor))
                    e.Graphics.FillRectangle(br, tabarea);
            }
            else
            {
                e.Graphics.DrawMultiColouredRectangles(ClientRectangle, ThemeColors, TabBackgroundGradientDirection);
            }

            DrawBorder(e.Graphics);

            if (TabCount > 0 )
            {
                for (int i = TabCount - 1; i >= 0; i--)
                {
                    if (i != SelectedIndex)
                        DrawTab(i, e.Graphics, false, mouseover == i);
                }

                // seen instances of SelectedIndex being set BEFORE tab up, meaning selected index is out of range
                if (SelectedIndex >= 0 && SelectedIndex < TabCount)      // and if its selected, we did not draw it     -- seen it above TabCount.. protect
                    DrawTab(SelectedIndex, e.Graphics, true, false);     // we paint the selected one last, in case it overwrites the other ones.
            }
        }

        private void DrawTab(int i, Graphics gr, bool selected, bool mouseover)
        {
            if (TabStyle == null)
                throw new ArgumentNullException("Custom style not attached");

            Color tabc1 = Enabled ? (selected ? TabSelectedColor : mouseover ? TabMouseOverColor : TabNotSelectedColor) : TabNotSelectedColor.Multiply(TabDisabledScaling);
            Color tabc2 = Enabled ? (selected ? TabSelectedColor2 : mouseover ? TabMouseOverColor2 : TabNotSelectedColor2) : TabNotSelectedColor.Multiply(TabDisabledScaling);
            Color taboutline = (selected) ? TabControlBorderColor : TabNotSelectedBorderColor;

            Image tabimage = null;

            TabStyle.DrawTab(gr, GetTabRect(i), i, selected, tabc1, tabc2, taboutline, Alignment, TabGradientDirection);

            if (this.ImageList != null && this.TabPages[i].ImageIndex >= 0 && this.TabPages[i].ImageIndex < this.ImageList.Images.Count)
                tabimage = this.ImageList.Images[this.TabPages[i].ImageIndex];

            Color tabtextc = (Enabled) ? ((selected) ? TextSelectedColor : TextNotSelectedColor) : TextNotSelectedColor.Multiply(TabDisabledScaling);
            TabStyle.DrawText(gr, GetTabRect(i), i, selected, tabtextc, this.TabPages[i].Text, Font , tabimage);

            gr.SmoothingMode = SmoothingMode.Default;
        }

        private void DrawBorder(Graphics gr)
        {
            Pen outerrectangle = new Pen(TabControlBorderColor, 1.0F);    // based on 4 width border, bright colour
            Pen innerrectangle = new Pen(TabControlBorderColor2, 1.0F);     // just a touch brighter, darker colour
            Pen widefill = new Pen(TabControlBorderColor, 2.0F);
            
            int borderheight = ClientRectangle.Height - DisplayRectangle.Y + topborder;         // area of boarder in height
            int borderwidth = (ClientRectangle.Width - DisplayRectangle.Width) / 2;     // should be 4

            // remember to draw a rectangle it needs to be width-1 
            
            // outer rectangle, from left to 1 pixel past the displayrectangle. 1 wide
            var b1 = new Rectangle(0, DisplayRectangle.Y - topborder, ClientRectangle.Width - 1 - borderwidth + 2, borderheight - 1);

            //System.Diagnostics.Debug.WriteLine($"Tabcontrol {borderheight} {borderwidth} {b1}");

            gr.DrawRectangle(outerrectangle, b1);

            // right line
            gr.DrawLine(widefill, b1.Right + 2, b1.Y, b1.Right + 2, b1.Bottom + 1);
            // inner rectangle - inside the outer rectangle
            b1.Inflate(-1, -1);
            gr.DrawRectangle(innerrectangle, b1);
            // fill in the left vert bar
            gr.DrawLine(widefill, b1.X + 2, b1.Y + 1, b1.X + 2, b1.Y + b1.Height);
            // fill in bottom horz bar
            gr.DrawLine(widefill, b1.X + 3, b1.Y + b1.Height - 1, b1.X + b1.Width, b1.Y + b1.Height - 1);

            innerrectangle.Dispose();
            widefill.Dispose();
            outerrectangle.Dispose();
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

            bool systemmode = FlatStyle == FlatStyle.System;

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

            Invalidate();
            //System.Diagnostics.Debug.WriteLine($"Tabcontrol Force Update Finish: flat {flatstyle} size {SizeMode} Multiline {Multiline} Font {Font}");
        }

        private IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
        {
            Message message = Message.Create(this.Handle, msg, wparam, lparam);
            this.WndProc(ref message);
            return message.Result;
        }

        #endregion

        #region Helpers

        public bool Theme(Theme t, Font fnt)
        {
            AutoForceUpdate = false;        // make it slightly better

            if ( t.IsButtonSystemStyle) // not system
            {
                FlatStyle = FlatStyle.System;
            }
            else
            {

                TabControlBorderColor = t.TabControlBorder;
                TabControlBorderColor2 = t.IsTextBoxBorderNone ? t.TabControlBorder2 : t.TabControlBorder;

                TabNotSelectedColor = t.TabControlButtonBack;
                TabNotSelectedColor2 = t.IsButtonFlatStyle ? TabNotSelectedColor : t.TabControlButtonBack2;
                TabGradientDirection = t.TabControlTabGradientDirection;

                TextSelectedColor = t.TabControlText;
                TextNotSelectedColor = t.TabControlText.Multiply(0.8F);

                TabNotSelectedBorderColor = t.TabControlBorder.Multiply(t.DisabledScaling);

                TabSelectedColor = TabNotSelectedColor.Multiply(t.MouseSelectedScaling);
                TabSelectedColor2 = TabNotSelectedColor2.Multiply(t.MouseSelectedScaling);

                TabMouseOverColor = TabNotSelectedColor.Multiply(t.MouseOverScaling);
                TabMouseOverColor2 = TabNotSelectedColor2.Multiply(t.MouseOverScaling);

                TabDisabledScaling = t.DisabledScaling;
                SetStyle(t.ButtonFlatStyle, new TabStyleAngled());

                if (ThemeColorSet > 0)
                {
                    ThemeColors = t.GetPanelSet(ThemeColorSet);
                    TabBackgroundGradientDirection = t.GetPanelDirection(ThemeColorSet);
                }
                else
                {
                    ThemeColors = t.TabControlBack;
                    TabBackgroundGradientDirection = t.TabControlBackGradientDirection;
                }
            }

            ForceUpdate();
            return true;
        }
        #endregion

        #region Members
        private FlatStyle flatstyle = FlatStyle.System;
        private TabStyleCustom tabstyle = new TabStyleSquare();     // change for the shape of tabs.
        private int mouseover = -1;                                 // where the mouse if hovering
        int lasttabclickedinside = -1;                              // LastTabClicked is persistent.. this is wiped by leaving the control

        #endregion
    }
}

