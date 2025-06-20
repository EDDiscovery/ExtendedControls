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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtTreeView : Panel, IThemeable
    {
        public class TreeViewBack : TreeView
        {
            public IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
            {
                Message message = Message.Create(this.Handle, msg, wparam, lparam);
                this.WndProc(ref message);
                return message.Result;
            }
        }

        // BackColor is the colour of the panel. 
        // if BorderColor is set, BackColor gets shown, with BorderColor on top.
        // BorderStyle is also applied by windows around the control, set to None for BorderColor only

        public Color TreeViewForeColor { get { return TreeView.ForeColor; } set { TreeView.ForeColor = value; } }
        public Color TreeViewBackColor { get { return TreeView.BackColor; } set { TreeView.BackColor = value; } }
        public Color BorderColor { get; set; } = Color.Transparent;
        public Color BorderColor2 { get; set; } = Color.Transparent;
        public bool ShowLineCount { get; set; } = false;                // count lines
        public bool HideScrollBar { get; set; } = true;                   // hide if no scroll needed

        public FlatStyle ScrollBarFlatStyle { get { return ScrollBar.FlatStyle; } set { ScrollBar.FlatStyle = value; } }
        public Color ScrollBarBackColor { get { return ScrollBar.BackColor; } set { ScrollBar.BackColor = value; } }
        public Color ScrollBarSliderColor { get { return ScrollBar.SliderColor; } set { ScrollBar.SliderColor = value; } }
        public Color ScrollBarBorderColor { get { return ScrollBar.BorderColor; } set { ScrollBar.BorderColor = value; } }
        public Color ScrollBarThumbBorderColor { get { return ScrollBar.ThumbBorderColor; } set { ScrollBar.ThumbBorderColor = value; } }
        public Color ScrollBarArrowBorderColor { get { return ScrollBar.ArrowBorderColor; } set { ScrollBar.ArrowBorderColor = value; } }
        public Color ScrollBarArrowButtonColor { get { return ScrollBar.ArrowButtonColor; } set { ScrollBar.ArrowButtonColor = value; } }
        public Color ScrollBarThumbButtonColor { get { return ScrollBar.ThumbButtonColor; } set { ScrollBar.ThumbButtonColor = value; } }
        public Color ScrollBarMouseOverButtonColor { get { return ScrollBar.MouseOverButtonColor; } set { ScrollBar.MouseOverButtonColor = value; } }
        public Color ScrollBarMousePressedButtonColor { get { return ScrollBar.MousePressedButtonColor; } set { ScrollBar.MousePressedButtonColor = value; } }
        public Color ScrollBarForeColor { get { return ScrollBar.ForeColor; } set { ScrollBar.ForeColor = value; } }
        public int ScrollBarWidth { get { return scrollbarwidth; } set { scrollbarwidth = value; Invalidate(); } }

        public TreeNodeCollection Nodes {get { return TreeView.Nodes; } }
        public bool ShowLines { get {return TreeView.ShowLines; } set { TreeView.ShowLines = value; } }
        public bool ShowRootLines { get {return TreeView.ShowRootLines; } set { TreeView.ShowRootLines = value; } }
        public bool ShowPlusMinus {get {return TreeView.ShowPlusMinus; } set { TreeView.ShowPlusMinus = value;} }
        
        public ExtTreeView() : base()
        {
            TreeView = new TreeViewBack();
            ScrollBar = new ExtScrollBar();
            Controls.Add(TreeView);
            Controls.Add(ScrollBar);
            TreeView.Scrollable = true;                     // TreeView has to be scrollable to scroll at all, but this shows the scroll bar
                                                            // just make sure the themed scroll bar is on top of the default Windows one
            TreeView.BorderStyle = BorderStyle.None;
            TreeView.BackColor = BackColor;
            TreeView.ForeColor = ForeColor;
            TreeView.MouseUp += TreeView_MouseUp;
            TreeView.MouseDown += TreeView_MouseDown;
            TreeView.MouseMove += TreeView_MouseMove;
            TreeView.MouseEnter += TreeView_MouseEnter;
            TreeView.MouseLeave += TreeView_MouseLeave;
            TreeView.AfterCollapse += TreeView_ExpandChanged;
            TreeView.AfterExpand += TreeView_ExpandChanged;
            TreeView.MouseWheel += new MouseEventHandler(MWheel);        // need to keep our scrollbar in sync with the Windows one, need to catch the mouse wheel
            ScrollBar.Scroll += new ScrollEventHandler(OnScrollBarChanged);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!BorderColor.IsFullyTransparent())
            {
                e.Graphics.SmoothingMode = SmoothingMode.Default;

                GraphicsPath g1 = DrawingHelpersStaticFunc.RectCutCorners(1, 1, ClientRectangle.Width - 2, ClientRectangle.Height - 1, 1, 1);
                using (Pen pc1 = new Pen(BorderColor, 1.0F))
                    e.Graphics.DrawPath(pc1, g1);

                GraphicsPath g2 = DrawingHelpersStaticFunc.RectCutCorners(0, 0, ClientRectangle.Width, ClientRectangle.Height - 1, 2, 2);
                using (Pen pc2 = new Pen(BorderColor2, 1.0F))
                    e.Graphics.DrawPath(pc2, g2);
            }
        }
        
        bool visibleonlayout = false;
        int totalNodeCount = 0;
        int nodesToFirstVisible = 0;
        bool pastTopVis = false;

        void CountNodes(TreeNodeCollection tnc)
        {
            foreach(TreeNode tn in tnc)
            {
                totalNodeCount++;
                if (!pastTopVis) nodesToFirstVisible++;
                pastTopVis = pastTopVis || tn == TreeView.TopNode;
                if (tn.IsExpanded) CountNodes(tn.Nodes);
            }
        }

        void UpdateNodeCounts()
        {
            totalNodeCount = 0;
            nodesToFirstVisible = 0;
            pastTopVis = false;
            CountNodes(Nodes);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            int bordersize = (!BorderColor.IsFullyTransparent()) ? 3 : 0;

            int treeviewclienth = ClientRectangle.Height - bordersize * 2;
            TreeView.Height = treeviewclienth;       // Set TreeView height so we can use VisibleCount
            
            SetScrollBarParameters();

            visibleonlayout = ScrollBar.IsScrollBarOn || DesignMode || !HideScrollBar;  // Hide must be on, or in design mode, or scroll bar is on due to values

            TreeView.Location = new Point(bordersize, bordersize);
            TreeView.Size = new Size(ClientRectangle.Width - bordersize * 2, treeviewclienth);

            ScrollBar.Location = new Point(ClientRectangle.Width - scrollbarwidth - bordersize, bordersize);
            ScrollBar.Size = new Size(scrollbarwidth, treeviewclienth);
            // we're letting the TreeView scroll itself, just hiding the default scroll bar under the themeable custom one when it's needed
            if (visibleonlayout) 
                ScrollBar.BringToFront();
            else 
                TreeView.BringToFront();
        }

        void SetScrollBarParameters()
        {
            UpdateNodeCounts();

            ScrollBar.SetValueMaximumLargeChange(nodesToFirstVisible - 1, totalNodeCount, TreeView.VisibleCount);
        }

        void UpdateScrollBar()
        {
            SetScrollBarParameters();

            if (ScrollBar.IsScrollBarOn != visibleonlayout)     // need to relayout if scroll bars pop on
                PerformLayout();
        }

        protected virtual void OnScrollBarChanged(object sender, ScrollEventArgs e)
        {
            ScrollToBar();
        }

        protected virtual void MWheel(object sender, MouseEventArgs e)
        {
            SetScrollBarParameters();
        }

        private void ScrollToBar()              // from the scrollbar, scroll first line to value
        {
            UpdateNodeCounts();

            int scrollvalue = ScrollBar.Value;
            int delta = scrollvalue - nodesToFirstVisible;

            //Console.WriteLine("Scroll Bar:" + scrollvalue + " FVL: " + firstVisibleLine + " delta " + delta);
            if (delta != 0)
            {
                TreeNode nextTop = TreeView.TopNode;
                for (int i = 0; i < Math.Abs(delta); i++)
                {
                    if (delta < 0)
                    {
                        if (nextTop.PrevVisibleNode == null) break;     //we're at the top already
                        nextTop = nextTop.PrevVisibleNode;
                    }
                    else
                    {
                        if (nextTop.NextVisibleNode == null) break;     //we're at the bottom already
                        nextTop = nextTop.NextVisibleNode;
                    }
                }
                TreeView.TopNode = nextTop;
            }
        }

        protected override void OnGotFocus(EventArgs e)             // Focus on us is given to the TreeView.
        {
            base.OnGotFocus(e);
            TreeView.Focus();
        }

        private void TreeView_ExpandChanged(Object sender, TreeViewEventArgs e)
        {
            UpdateScrollBar();
        }

        private void TreeView_MouseLeave(object sender, EventArgs e)             // using the treeview mouse actions, pass thru to ours so registered handlers work
        {
            base.OnMouseLeave(e);
        }

        private void TreeView_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        private void TreeView_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        private void TreeView_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        public bool Theme(Theme t, Font fnt)
        {
            ForeColor = t.TextBlockForeColor;
            BackColor = t.TextBlockBackColor;
            BorderColor = t.TextBlockBorderColor;
            BorderColor2 = t.TextBlockBorderColor2;
            // theme sets the scroll bar width, but we never use skinny, as its not wide enough to mask the tree view scroll bar
            int newwidth = ExtendedControls.Theme.ScrollBarWidth(t.GetFont, false);  
            if (newwidth != ScrollBarWidth)
            {
                scrollbarwidth = newwidth;
                PerformLayout();
            }
            return true;    // allow Scroll Bar to theme itself
        }

        private TreeView TreeView;                 // Use these with caution.
        private ExtScrollBar ScrollBar;
        private int scrollbarwidth = 48;
    }
}
