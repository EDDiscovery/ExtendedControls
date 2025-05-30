﻿/*
 * Copyright 2016 - 2025 EDDiscovery development team
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
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExtendedControls
{
    public partial class TabStrip : UserControl, IThemeable
    {
        public enum StripModeType { StripTop, StripBottom, ListSelection, StripTopOpen };
        public StripModeType StripMode { get { return stripmode; } set { ChangeStripMode(value); } }
        public Image EmptyPanelIcon { get; set; } = Properties.Resources.Stop;
        public Image[] ImageList { get; set; }     // images
        public int[] ListSelectionItemSeparators { get; set; }   // any separators for ListSelection
        public string[] TextList { get; set; }       // text associated - tooltips or text on list selection
        public object[] TagList { get; set; }      // tags for them..
        public bool ShowPopOut { get; set; }= true; // Pop out icon show
        public Color SelectedBackColor { get; set; } = Color.Transparent;   // if set, show selected with a back colour
        public override Color ForeColor { get => base.ForeColor; set { base.ForeColor = value; labelControlText.ForeColor = value; labelTitle.ForeColor = value; } }

        // ThemeColors only used if ThemeColourSet>=0.  
        public Color[] ThemeColors { get { return panelStrip.ThemeColors; } set { panelStrip.ThemeColors = value; } }
        // -1 = system, 0 use tabstrip theme colours, else use panel set 1,2,3,4.. 
        public int ThemeColorSet { get; set; } = 0;

        // if you set this, when empty, a panel will appear with the color selected
        public Color EmptyColor { get { return emptypanelcolor; } set { emptypanelcolor = value; Invalidate(); } }
        public float EmptyColorScaling { get; set; } = 0.5F;

        // only if using ListSelection with a drop down box
        public Color DropDownSelectionBackgroundColor { get; set; } = Color.Gray;
        public Color DropDownSelectionBackgroundColor2 { get; set; } = Color.Gray;      // background
        public float DropDownGradient { get; set; } = 90F;
        public Color DropDownSelectionColor { get; set; } = Color.Green;       // selection bar
        public Color DropDownSliderColor { get; set; } = Color.Green;
        public Color DropDownSliderArrowColor { get; set; } = Color.Cyan;
        public Color DropDownBorderColor { get; set; } = Color.Green;
        public Color DropDownSliderButtonColor { get; set; } = Color.Blue;
        public Color DropDownMouseOverSliderButtonColor { get; set; } = Color.Red;
        public Color PressedDropDownSliderButtonColor { get; set; } = Color.DarkCyan;

        // if you set this to a colour, the tab strip background becomes that colour, for transparency purposes.
        // If you set it to Color.Transparent, it goes to normal
        public Color PaintTransparentColor { get { return panelStrip.PaintTransparentColor; } set { panelStrip.PaintTransparentColor = value; Invalidate(); } }

        public bool DropDownFitImagesToItemHeight { get; set; } = false;

        // If non null, a help icon ? appears on the right. When clicked, you get a callback.  P is the lower bottom of the ? icon in screen co-ords

        public Action<Point> HelpAction { get; set; }  = null;

        public static Image HelpIcon { get { return global::ExtendedControls.Properties.Resources.help; } }

        // Selected
        public int SelectedIndex { get { return selectedindex; } set { ChangePanel(value); } }
        public Control CurrentControl;

        // events
        public Func<TabStrip, int, Control,bool> AllowClose;     // called if a panel is being closed, true to allow it
        public Action<TabStrip, Control> OnRemoving;        // called due to ChangePanel or Close
        public Func<TabStrip, int,Control> OnCreateTab;     // called due to  Create or due to ChangePanel
        public Action<TabStrip, Control, int> OnPostCreateTab;  // called due to ChangePanel 
        public Action<TabStrip, int> OnPopOut;              // when the popout button clicked
        public Action<TabStrip> OnTitleClick;               // when the title is clicked
        public Action<TabStrip> OnControlTextClick;         // when the control text is clicked

        public TabStrip()
        {
            InitializeComponent();
            labelControlText.Text = "";
            labelControlText.MouseDown += (s1, e1) => OnControlTextClick?.Invoke(this);
            labelTitle.Text = "?";
            labelTitle.MouseDown += (s1, e1) => OnTitleClick?.Invoke(this);

            autofadeinouttimer.Tick += AutoFadeInOutTick;
            autorepeat.Interval = 200;
            autorepeat.Tick += Autorepeat_Tick;
            pimageSelectedIcon.BackgroundImage = EmptyPanelIcon;
            pimageSelectedIcon.BackgroundImageLayout = ImageLayout.Stretch;
            pimagePopOutIcon.Visible = pimageListSelection.Visible = false;
            extButtonDrawnHelp.Visible = false;

        //    SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        #region Public interface

        public void Toggle()
        {
            if (selectedindex != -1)
                ChangePanel((selectedindex + 1) % ImageList.Length);
        }

        public bool ChangePanel(int i)     // with bounds checking
        {
            if (i >= 0 && i < ImageList.Length)
            {
                if ( Close() )
                {
                    Create(i);
                    PostCreate();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool Close()     // close down
        {
            if (CurrentControl != null)
            {
                if (AllowClose?.Invoke(this, selectedindex, CurrentControl) ?? true)
                {
                    OnRemoving?.Invoke(this, CurrentControl);
                    this.Controls.Remove(CurrentControl);
                    CurrentControl.Dispose();
                    CurrentControl = null;
                    selectedindex = -1;
                    labelControlText.Text = "";
                    labelTitle.Text = "?";
                    pimageSelectedIcon.BackgroundImage = EmptyPanelIcon;
                    extButtonDrawnHelp.Visible = false;
                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }

        public void Create(int i)       // create.. only if already closed
        {
            if (OnCreateTab != null)
            {
                //System.Diagnostics.Debug.WriteLine("Panel " + i + " make");
                CurrentControl = OnCreateTab(this, i);      // TAB should just create..

                if (CurrentControl != null)
                {
                    selectedindex = i;

                    CurrentControl.Dock = DockStyle.Fill;

                    AddControlToView(CurrentControl);

                    //System.Diagnostics.Debug.WriteLine("Panel " + i + " post create " + CurrentControl.Name);

                    if (i < ImageList.Length)
                        pimageSelectedIcon.BackgroundImage = ImageList[i];   // paranoia..

                    labelTitle.Text = CurrentControl.Name;

                    extButtonDrawnHelp.Visible = HelpAction != null;    // visible if help is defined and tab is created
                }
            }

            Display();
        }

        public void PostCreate()        // ask for post create phase
        {
            if (CurrentControl != null && OnPostCreateTab != null )
            {
                OnPostCreateTab(this, CurrentControl, selectedindex);       // now tab is in control set, give it a chance to configure itself and set its name
            }
        }

        public void SetControlText(string t)
        {
            labelControlText.Text = t;
            Display();
        }

        #endregion

        #region Common 

        // paint the UC

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (EmptyColor != Color.Empty && CurrentControl == null)       // this seems the best way to display the non used
            {
                Rectangle area = panelStrip.Dock == DockStyle.Top ? new Rectangle(0, panelStrip.Height, ClientRectangle.Width, ClientRectangle.Height - panelStrip.Height) : new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height - panelStrip.Height);

                using (Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(area, EmptyColor, EmptyColor.Multiply(EmptyColorScaling), DropDownGradient))      // reuse drop down gradient
                {
                    e.Graphics.FillRectangle(b, area);
                }
            }
        }

        private void AddControlToView(Control c)
        {
            SuspendLayout();

            Controls.Clear();

            if (StripMode != StripModeType.StripBottom)
            {
                Controls.Add(c);
                Controls.Add(panelStrip);
            }
            else
            {
                Controls.Add(panelStrip);
                Controls.Add(c);
            }

            ResumeLayout();
        }

        private void TabStrip_Layout(object sender, LayoutEventArgs e)
        {
            if ((StripMode == StripModeType.StripTop || StripMode == StripModeType.StripTopOpen) && panelStrip.Dock != DockStyle.Top)
            {
                panelStrip.Dock = DockStyle.Top;
            }
        }

        private void TabStrip_Resize(object sender, EventArgs e)
        {
            tabdisplaystart = 0;        // because we will display a different set next time
        }

        private void panelPopOut_Click(object sender, EventArgs e)
        {
            if (OnPopOut != null && selectedindex >= 0)
                OnPopOut(this, selectedindex);
        }

        private void ChangeStripMode(StripModeType mt)
        {
            stripmode = mt;
            tdm = stripmode == StripModeType.StripTopOpen ? TabDisplayMode.ExpandedFixed : TabDisplayMode.Compressed;
            Display();
        }

        #endregion

        #region Show hide info control

        private void MouseEnterPanelObjects(object sender, EventArgs e)
        {
            autofadeinouttimer.Stop();

            if (tdm == TabDisplayMode.Compressed )      // if in compressed..
            {
                autofadetabmode = TabDisplayMode.Expanded;
                autofadeinouttimer.Interval = 350;
                autofadeinouttimer.Start();
                //System.Diagnostics.Debug.WriteLine("{0} {1} Fade in", Environment.TickCount, Name);
            }
        }

        private void MouseLeavePanelObjects(object sender, EventArgs e)     // get this when leaving a panel and going to the icons.. so fade out slowly so it can be cancelled
        {
            autofadeinouttimer.Stop();

            if (tdm == TabDisplayMode.Expanded )      // if in expanded
            {
                autofadetabmode = TabDisplayMode.Compressed;
                autofadeinouttimer.Interval = 750;
                autofadeinouttimer.Start();
                //System.Diagnostics.Debug.WriteLine("{0} {1} Fade out", Environment.TickCount, Name);
            }
        }

        void AutoFadeInOutTick(object sender, EventArgs e)            // hiding
        {
            autofadeinouttimer.Stop();

            //System.Diagnostics.Debug.WriteLine("{0} {1} Fade {2}" , Environment.TickCount, Name, tobevisible);

            if (tdm != autofadetabmode )
            {
                tdm = autofadetabmode;
                Display();
            }
        }

        #endregion

        #region Implementation - As Tab strip

        void Display()
        {
            if (ImageList == null)
                return;

            //System.Diagnostics.Debug.WriteLine("Mode " + tdm);

            if (StripMode != StripModeType.ListSelection && imagepanels == null && ImageList != null)  // on first entry..
            {
                imagepanels = new ExtPanelNoChildThemed[ImageList.Length];

                for (int inp = 0; inp < imagepanels.Length; inp++)
                {
                    imagepanels[inp] = new ExtPanelNoChildThemed()
                    {
                        BackgroundImage = ImageList[inp],
                        Tag = inp,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Visible = false,
                        Size = pimageSelectedIcon.Size,
                       
                    };

                    imagepanels[inp].Click += TabIconClicked;
                    imagepanels[inp].MouseEnter += MouseEnterPanelObjects;
                    imagepanels[inp].MouseLeave += MouseLeavePanelObjects;

                    if (ShowPopOut)
                        imagepanels[inp].ContextMenuStrip = contextMenuStrip1;

                    if (TextList != null)
                    {
                        toolTip1.SetToolTip(imagepanels[inp], TextList[inp]);
                        toolTip1.ShowAlways = true;      // if not, it never appears
                    }

                    panelStrip.Controls.Add(imagepanels[inp]);
                }
            }

            int tabno = 0;

            bool showselectionicon = true;      // set up for compressed mode..
            bool showtext = true;
            bool showpopouticon = false;
            bool showlistselection = false;

            int tabfieldspacing = Font.ScalePixels(8);

            int xpos = pimageSelectedIcon.Width + tabfieldspacing;       // start here
            bool arrowson = false;

            if (StripMode == StripModeType.ListSelection)               // in list mode
            {
                if (tdm != TabDisplayMode.Compressed)                   // values are Compressed.. Expanded.. ExpandedInList
                {
                    if (ShowPopOut)
                    {
                        showselectionicon = false;                      // swap what is shown..
                        showpopouticon = true;
                    }

                    showlistselection = true;
                    xpos += pimageListSelection.Width + tabfieldspacing; // space on for allowing panel selector
                }
            }
            else if (tdm != TabDisplayMode.Compressed)      // show icons..
            {
                for (; tabno < tabdisplaystart; tabno++)
                    imagepanels[tabno].Visible = false;

                // don't trust extButtonDrawnHelp.Visible - we all know that does not get set until its redrawn.  use another decision to decide on width
                int stoppoint = DisplayRectangle.Width - Spacing - (CurrentControl!=null && HelpAction!=null ? extButtonDrawnHelp.Width : 0); // stop here

                int spaceforarrowsandoneicon = panelArrowLeft.Width + Spacing + imagepanels[0].Width + Spacing + panelArrowRight.Width;

                if (xpos + spaceforarrowsandoneicon > stoppoint || tdm == TabDisplayMode.ExpandedFixed)   // no space at all or fixed open
                {
                    xpos = 0;                                       // turn off titles, use all the space
                    showselectionicon = false;
                }

                int tabtotalwidth = 0;
                for (int ip = 0; ip < imagepanels.Length; ip++)
                {
                    tabtotalwidth += imagepanels[ip].Width + Spacing * 2;       // do it now due to the internal scaling due to fonts
                                                                                // System.Diagnostics.Debug.WriteLine("Image panel size " + ip + " w " + imagepanels[ip].Width + " width " + Images[ip].Width);
                }

                tabtotalwidth -= Spacing * 2;           // don't count last spacing.

                if (xpos + tabtotalwidth > stoppoint)     // if total tab width (icon space icon..) too big
                {
                    panelArrowLeft.Location = new Point(xpos, 4);
                    xpos += panelArrowLeft.Width + Spacing; // move over allowing space for left and spacing
                    stoppoint -= panelArrowRight.Width + Spacing;     // allow space for right arrow plus padding
                    arrowson = true;
                }

                tabsvisibleonscreen = 0;
                for (; tabno < imagepanels.Length && xpos < stoppoint - ImageList[tabno].Width; tabno++)
                {                                           // if its soo tight, may display nothing, thats okay
                    imagepanels[tabno].Location = new Point(xpos, 3);
                    xpos += imagepanels[tabno].Width + Spacing * 2;
                    imagepanels[tabno].Visible = true;
                    tabsvisibleonscreen++;

                    if (SelectedBackColor != Color.Transparent)
                        imagepanels[tabno].BackColor = (tabno == selectedindex) ? SelectedBackColor : Color.Transparent;

                    //System.Diagnostics.Debug.WriteLine("Tab " + tabno + " Col " + imagepanels[tabno].BackColor);
                }

                if (arrowson)
                    panelArrowRight.Location = new Point(xpos, 4);

                if (tdm == TabDisplayMode.ExpandedFixed)
                {
                    showtext = true;
                }
                else
                {
                    if (ShowPopOut && showselectionicon )
                    {
                        showselectionicon = false;
                        showpopouticon = true;
                    }

                    showtext = false;
                }
            }

            if (imagepanels != null)
            {
                for (; tabno < imagepanels.Length; tabno++)
                    imagepanels[tabno].Visible = false;
            }

            panelStrip.SuspendLayout(); // important to suspend layout otherwise we get popping

            //System.Diagnostics.Debug.WriteLine(this.Name + " seli" + showselectionicon + " showp " + showpopouticon + " text" + showtext + " lists " + showlistselection);
            pimageListSelection.Size = pimagePopOutIcon.Size = pimageSelectedIcon.Size; // duplicate size across so panelstrip does not resize

            pimageSelectedIcon.Visible = showselectionicon;

            pimagePopOutIcon.Location = pimageSelectedIcon.Location;     // same position, mutually exclusive
            pimagePopOutIcon.Visible = showpopouticon;

            pimageListSelection.Location = new Point(pimageSelectedIcon.Right + tabfieldspacing, pimageSelectedIcon.Top);
            pimageListSelection.Visible = showlistselection;

            labelTitle.Location = new Point(xpos + tabfieldspacing, labelTitle.Top);
            labelTitle.Visible = showtext;

            labelControlText.Location = new Point(labelTitle.Right + tabfieldspacing, labelTitle.Top);
            labelControlText.Visible = showtext;
            //System.Diagnostics.Debug.WriteLine("Panel " + panelStrip.Size + " " + pimageListSelection.Size + " " + pimagePopOutIcon.Size + " " + panelStrip.AutoSize);

            panelArrowRight.Visible = panelArrowLeft.Visible = arrowson;

            panelStrip.ResumeLayout();
        }

        public void TabIconClicked(object sender, EventArgs e)
        {
            int i = (int)(((Control)sender).Tag);
            ChangePanel(i);
        }

        private void panelArrowLeft_MouseDown(object sender, MouseEventArgs e)
        {
            autorepeatdir = -1;
            ClickArrow();
        }

        private void panelArrowLeft_MouseUp(object sender, MouseEventArgs e)
        {
            autorepeat.Stop();
        }

        private void panelArrowRight_MouseDown(object sender, MouseEventArgs e)
        {
            autorepeatdir = 1;
            ClickArrow();
        }

        private void panelArrowRight_MouseUp(object sender, MouseEventArgs e)
        {
            autorepeat.Stop();
        }

        private void ClickArrow()
        {
            autorepeat.Stop();

            int newpos = tabdisplaystart + autorepeatdir;
            if ( newpos >= 0 && newpos <= imagepanels.Length - tabsvisibleonscreen)
            {
                tabdisplaystart = newpos;
                Display();
                autorepeat.Start();
            }
        }

        private void Autorepeat_Tick(object sender, EventArgs e)
        {
            ClickArrow();
        }

        private void extButtonDrawnHelp_Click(object sender, EventArgs e)
        {
            HelpAction?.Invoke(panelStrip.PointToScreen(new Point(extButtonDrawnHelp.Left, extButtonDrawnHelp.Bottom)));
        }

        #endregion

        #region Implementation - as Pop Out list

        ExtListBoxForm dropdown;

        private void drawnPanelListSelection_Click(object sender, EventArgs e)
        {
            autofadeinouttimer.Stop();      // in case we are in an autofade

            dropdown = new ExtListBoxForm("", true);

            dropdown.ListBox.BackColor = BackColor;
            dropdown.ListBox.ForeColor = ForeColor;
            dropdown.ListBox.SelectionBackColor = this.DropDownSelectionBackgroundColor;
            dropdown.ListBox.SelectionBackColor2 = this.DropDownSelectionBackgroundColor2;
            dropdown.ListBox.SelectionColor = this.DropDownSelectionColor;
            dropdown.ListBox.BackGradientDirection = this.DropDownGradient;
            dropdown.ListBox.BorderColor = this.DropDownBorderColor;
            dropdown.ListBox.ScrollBar.BackColor = dropdown.ListBox.ScrollBar.SliderColor = this.DropDownSliderColor;
            dropdown.ListBox.ScrollBar.ForeColor = this.DropDownSliderArrowColor;    // arrow
            dropdown.ListBox.ScrollBar.ThumbBorderColor = dropdown.ListBox.ScrollBar.ArrowBorderColor =
                                                            dropdown.ListBox.ScrollBar.BorderColor = this.DropDownBorderColor;
            dropdown.ListBox.ScrollBar.ArrowButtonColor = dropdown.ListBox.ScrollBar.ThumbButtonColor = this.DropDownSliderButtonColor;
            dropdown.ListBox.ScrollBar.MouseOverButtonColor = this.DropDownMouseOverSliderButtonColor;
            dropdown.ListBox.ScrollBar.MousePressedButtonColor = this.PressedDropDownSliderButtonColor;

            dropdown.FitImagesToItemHeight = this.DropDownFitImagesToItemHeight;
            dropdown.Font = Font;
            dropdown.Items = TextList.ToList();
            dropdown.ItemSeperators = ListSelectionItemSeparators;
            dropdown.ImageItems = ImageList.ToList();
            dropdown.FlatStyle = FlatStyle.Popup;
            dropdown.PositionBelow(pimageListSelection);

            dropdown.SelectedIndexChanged += (s, ea, key) =>
            {
                tdm = TabDisplayMode.Expanded;              // deactivate drop down.. leave in expanded mode
                ChangePanel(dropdown.SelectedIndex);
            };

            dropdown.Deactivate += (s, ea) =>               // will also be called on selected index because we have auto close on (in constructor)
            {
                tdm = TabDisplayMode.Expanded;              // deactivate drop down.. leave in expanded mode
                MouseLeavePanelObjects(sender, e);          // same as a mouse leave on one of the controls
            };

            dropdown.Show(this.FindForm());
            tdm = TabDisplayMode.ExpandedInList;            // hold display in here during list presentation
        }

        #endregion

        #region Right Click

        private void toolStripMenuItemPopOut_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            ContextMenuStrip cms = (ContextMenuStrip)tsmi.Owner;
            Panel p = (Panel)cms.SourceControl;

            if (OnPopOut != null)
                OnPopOut(this, (int)p.Tag);
        }

        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (tdm == TabDisplayMode.ExpandedContextMenu) // if in context menu, go back to expanded
                tdm = TabDisplayMode.Expanded;

            MouseLeavePanelObjects(sender, e);      // same as a mouse leave on one of the controls
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            if ( tdm == TabDisplayMode.Expanded )       // if in expanded, go to context menu to hold.. if in a mode such as ExpandedFixed, don't change
                tdm = TabDisplayMode.ExpandedContextMenu;
        }

        public bool Theme(Theme t, Font fnt)
        {
            ////System.Diagnostics.Debug.WriteLine("*************** TAB Strip themeing" + myControl.Name + " " + myControl.Tag);

            BackColor = t.Form;
            DropDownSelectionBackgroundColor = t.ComboBoxBackColor;
            DropDownSelectionBackgroundColor2 = t.ComboBoxBackColor2;
            DropDownSelectionColor = t.ComboBoxBackColor.Multiply(t.MouseSelectedScaling);
            DropDownSliderColor = t.ComboBoxDropDownSliderBack;
            DropDownSliderArrowColor = t.ComboBoxScrollArrowBack;
            DropDownBorderColor = t.ComboBoxBorderColor;
            DropDownSliderButtonColor = t.ComboBoxScrollButtonBack;
            DropDownMouseOverSliderButtonColor = DropDownSliderButtonColor.Multiply(t.MouseOverScaling);
            DropDownGradient = t.ComboBoxBackAndDropDownGradientDirection;
            PressedDropDownSliderButtonColor = DropDownSliderButtonColor.Multiply(t.MouseSelectedScaling);

            ForeColor = t.TabStripFore;             // theme our labels
            EmptyColor = t.Form;
            SelectedBackColor = t.TabStripSelected;

            panelStrip.ThemeColorSet = ThemeColorSet;
            if (ThemeColorSet > 0)
            {
                panelStrip.ThemeColors = t.GetPanelSet(ThemeColorSet);
                panelStrip.GradientDirection = t.GetPanelDirection(ThemeColorSet);
            }
            else
            {
                panelStrip.GradientDirection = t.TabStripGradientDirection;
                panelStrip.ThemeColors = t.TabStripBack;
            }

            if (CurrentControl != null)             // we give the CurrentControl only the chance to theme
                t.UpdateControls(CurrentControl, fnt, 100, false);
            
            return false;   // eveything we own is themed in here.. no children
        }

        #endregion

        #region vars

        // internals

        private StripModeType stripmode = StripModeType.StripTop;
        private int selectedindex = -1;

        private enum TabDisplayMode
        {
            Compressed,     // strip/list mode compressed
            Expanded,       // strip/list mode expanded during hover over, will go compressed after mouse exit
            ExpandedFixed,  // open all the time (StripTopOpen)
            ExpandedInList,   // List drop down open, expanded..
            ExpandedContextMenu, // List is expanded, in a context menu
        }

        private TabDisplayMode tdm = TabDisplayMode.Compressed;
        private int tabdisplaystart = 0;    // first tab number displayed
        private int tabsvisibleonscreen = 0;       // number of tabs displayed

        private Timer autofadeinouttimer = new Timer();
        private TabDisplayMode autofadetabmode;

        const int Spacing = 4;      // spacing distance

        private Timer autorepeat = new Timer();
        private int autorepeatdir = 0;

        private Color emptypanelcolor = Color.Empty;         // default empty means use base back color.. ambient property

        private ExtPanelNoChildThemed[] imagepanels;

        #endregion

    }
}
