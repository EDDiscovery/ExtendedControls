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

using BaseUtils;
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

        // Auto Invalidates, style is Flat, Popup (gradient) and System
        public FlatStyle FlatStyle { get { return flatstyle; } set { if (value != flatstyle) SetStyle(value); } }

        // Auto Invalidates attach a tab style class which determines the shape and formatting.
        public TabStyleCustom TabStyle { get { return tabstyle; } set { if (value != tabstyle) SetStyle(null, value); } }

        // change both, or either
        public void SetStyle(FlatStyle? fsstyle, TabStyleCustom tabstylep = null)
        {
            if (tabstylep != null)
            {
                tabstyle = tabstylep;
                Invalidate();
            }

            if (fsstyle.HasValue && fsstyle.Value != FlatStyle)
            {
                flatstyle = fsstyle.Value;

                if (flatstyle == FlatStyle.System)
                    SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, false);
                else
                    SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);

                Invalidate();
            }
        }

        // reordering 
        public bool AllowDragReorder { get; set; } = false;
        // Tab clicked.. reports last tab clicked
        public int LastTabClicked { get; private set; } = -1;
        public void ClearLastTab() { LastTabClicked = -1; }


        public bool TranslateDoChildren => true;

        #endregion

        #region Initialisation and Helpers

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

        // given fonts and the tab text, whats the minimum width? Only needed externally if your in fixed mode
        public SizeF CalculateMinimumTabWidthHeight()
        {
            SizeF min = new Size(16, 12);

            foreach (TabPage p in TabPages)
            {
                //Console.WriteLine("Text is " + p.Text);
                SizeF sz = BitMapHelpers.MeasureStringInBitmap(p.Text, this.Font);

                min = new SizeF(Math.Max(min.Width, sz.Width), Math.Max(min.Height, sz.Height));
            }

            return min;
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

        // You should only need to call this if 1) you defined your own tab control font and 2) you scale the control (say by changing parent font)
        // setting your own TC font stops the OnFontChanged being called for this class, and its at that point this fix needs to be applied
        public void ForceTabUpdate()
        {
            OnFontChanged(null);
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

            if (TabCount > 0)
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

            Rectangle tabrect = GetTabRect(i);      
            if (tabrect.Area() < 1)     // defensive check
                return;

            TabStyle.DrawTab(gr, tabrect, i, selected, tabc1, tabc2, taboutline, Alignment, TabGradientDirection);

            if (this.ImageList != null && this.TabPages[i].ImageIndex >= 0 && this.TabPages[i].ImageIndex < this.ImageList.Images.Count)
                tabimage = this.ImageList.Images[this.TabPages[i].ImageIndex];

            Color tabtextc = (Enabled) ? ((selected) ? TextSelectedColor : TextNotSelectedColor) : TextNotSelectedColor.Multiply(TabDisabledScaling);
            TabStyle.DrawText(gr, tabrect, i, selected, tabtextc, this.TabPages[i].Text, Font, tabimage);

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

        #region Reactors to events

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            //System.Diagnostics.Debug.WriteLine($"Tabcontrol OnFontChange: {Font}  {Font.Height} : Item size {ItemSize} Multiline {Multiline} RowCount {RowCount} {flatstyle}");

            // Put it back into system mode. if we don't put it back into system draw, it seems to never resize the tabs
            if (FlatStyle != FlatStyle.System)
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, false);      // go back to system

            if (SizeMode != TabSizeMode.Fixed)
            {
                SizeF sf = BitMapHelpers.MeasureStringInBitmap("akakAKAKAKyyyyxzzzyyjj0192892", this.Font);
                var size = new Size(100, ((int)sf.Height) + Padding.Y*2);       // set the height only, width is auto calculated. Padding.Y in system mode appears to be height at top
                ItemSize = size;
                //System.Diagnostics.Debug.WriteLine($"TabControl set tab size {size}");

                RecreateHandle();           // only sure way of making the font change work with tab sizing, as all other methods end up calling this
            }

            if (FlatStyle != FlatStyle.System)
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);
        }

        #endregion

        #region Themer

        public bool Theme(Theme t, Font fnt)
        {
            if (t.IsButtonSystemStyle) // not system
            {
                SetStyle(FlatStyle.System, new TabStyleAngled());
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

                SetStyle(t.ButtonFlatStyle, new TabStyleAngled());
            }

            Invalidate();
            return true;
        }

        #endregion

        #region Members
        private FlatStyle flatstyle = FlatStyle.System;
        private TabStyleCustom tabstyle = new TabStyleSquare();     // change for the shape of tabs.
        private int mouseover = -1;                                 // where the mouse if hovering
        private int lasttabclickedinside = -1;                              // LastTabClicked is persistent.. this is wiped by leaving the control
        private Color transparency = Color.Transparent;
        const int topborder = 2;

        #endregion

#if false
//  not needed I think
        private IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
        {
            Message message = Message.Create(this.Handle, msg, wparam, lparam);
            this.WndProc(ref message);
            return message.Result;
        }


        // kept code but not used as of aug 25
        public int MinimumTabWidth { set { SendMessage(0x1300 + 49, IntPtr.Zero, (IntPtr)value); } }
        public void SetPadding(int x,int y) { SendMessage(0x1300 + 43, IntPtr.Zero, (IntPtr)((y<<16) | (x & 0xffff))); }

#endif

    }
}

