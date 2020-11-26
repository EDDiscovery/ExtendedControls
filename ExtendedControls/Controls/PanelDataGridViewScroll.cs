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
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtPanelDataGridViewScroll : Panel      // Must have a DGV and a VScroll added as children by the framework
    {
        public bool VerticalScrollBarDockRight { get; set; } = true;        // true for dock right
        public Padding InternalMargin { get; set; }            // allows spacing around controls

        public int ScrollBarWidth { get { return Font.ScalePixels(24); } }       // if internal

        public void UpdateScroll()      // call if hide/unhide cells - no call back for this
        {
            UpdateScrollBar();
            outlining?.UpdateOutlines();
        }

        public void Suspend()                           // use these for quicker adding in of rows to table
        {
            dgv.RowsAdded -= DGVRowsAdded;              // need to keep row removed in case outlining range goes out 
            dgv.RowStateChanged -= DGVRowStateChanged;
            dgv.SuspendLayout();
        }

        public void Resume()                            
        {
            UpdateScrollBar();
            outlining?.UpdateOutlines();
            dgv.ResumeLayout();
            dgv.RowsAdded += DGVRowsAdded;
            dgv.RowStateChanged += DGVRowStateChanged;
        }

        public void ChangeVisibility(int startrow, int endrow, bool state)       // this efficiently changes the visibility and stops repeated scroll updates
        {
            if (dgv != null)
            {
                dgv.SuspendLayout();

                dgv.RowStateChanged -= DGVRowStateChanged;      // don't cause repeated call backs

                while (startrow <= endrow)
                    dgv.Rows[startrow++].Visible = state;       // set state

                dgv.RowStateChanged += DGVRowStateChanged;
                dgv.ResumeLayout();

                UpdateScrollBar();
                outlining?.UpdateOutlines();
            }
        }

        // on areas identify bits to be on.  its sorted. startrow-endrow identify the whole range - areas outside onareas are off.
        public void ChangeVisibility(int startrow, int endrow, BaseUtils.IntRangeList onareas)
        {
            if (dgv != null)
            {
                dgv.SuspendLayout();

                dgv.RowStateChanged -= DGVRowStateChanged;      // don't cause repeated call backs

                int rowpos = startrow;
                foreach (var r in onareas.Ranges)
                {
                    while (rowpos < r.Start)
                    {
                        //System.Diagnostics.Debug.WriteLine("Row " + rowpos + " Off");
                        dgv.Rows[rowpos++].Visible = false;       // set state
                    }

                    while (rowpos <= r.End)
                    {
                        //System.Diagnostics.Debug.WriteLine("Row " + rowpos + " On");
                        dgv.Rows[rowpos++].Visible = true;       // set state
                    }
                }

                while (rowpos <= endrow)
                {
                    //System.Diagnostics.Debug.WriteLine("Row " + rowpos + " On");
                    dgv.Rows[rowpos++].Visible = true;       // set state
                }

                dgv.RowStateChanged += DGVRowStateChanged;
                dgv.ResumeLayout();

                UpdateScrollBar();
                outlining?.UpdateOutlines();
            }
        }

        public void ApplyOutlining()        // outlining areas have been made, with visibility.. apply.  Presume all is visible, so only do invisible bits
        {
            if ( outlining != null )
            {
                dgv.SuspendLayout();
                dgv.RowStateChanged -= DGVRowStateChanged;      // don't cause repeated call backs

                foreach (ExtPanelDataGridViewScrollOutlining.Outline o in outlining.OutlineSet())
                {
                    if ( !o.expanded )
                    {
                        if (dgv.Rows[o.start].Visible == true || dgv.Rows[o.end].Visible == true)       // if either start/end is visible, its not been done already by a parent
                        {
                            for (int i = o.start; i <= o.end - 1; i++)
                                dgv.Rows[i].Visible = false;
                        }
                    }
                }

                dgv.RowStateChanged += DGVRowStateChanged;
                dgv.ResumeLayout();

                UpdateScrollBar();
                outlining?.UpdateOutlines();
            }
        }

        #region Implementation
        public ExtPanelDataGridViewScroll() : base()
        {
        }

        protected override void OnControlAdded(ControlEventArgs e ) 
        {  // as controls are added, remember them in local variables.
            if (e.Control is DataGridView)
            {
                dgv = e.Control as DataGridView;
                dgv.Scroll += DGVScrolled;                                                      // we hook this in case user uses keys to scroll
                dgv.RowsAdded += DGVRowsAdded;
                dgv.RowsRemoved += DGVRowsRemoved;
                dgv.RowStateChanged += DGVRowStateChanged;
                //dgv.RowHeightChanged += Dgv_RowHeightChanged; // tbd on this one
                dgv.MouseWheel += Wheel;

                outlining?.SetDGV(dgv);    // tell outlining
            }
            else if (e.Control is ExtScrollBar)
            {
                vsc = e.Control as ExtScrollBar;
                vsc.Scroll += new System.Windows.Forms.ScrollEventHandler(ScrollBarMoved);
            }
            else if ( e.Control is ExtPanelDataGridViewScrollOutlining)
            {
                outlining = e.Control as ExtPanelDataGridViewScrollOutlining;
                outlining.MouseWheel += Wheel;
                if (dgv != null)                // and inform outlining of dgv
                    outlining.SetDGV(dgv);
            }
            else
                Debug.Assert(true, "Data view Scroller Panel requires DataGridView and VScrollBarCustom to be added, optionally outlining");
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            int dgvcolumnheaderheight = 0;

            Rectangle area = ClientRectangle;
            area.X += InternalMargin.Left;
            area.Y += InternalMargin.Top;
            area.Width -= InternalMargin.Left + InternalMargin.Right;
            area.Height -= InternalMargin.Top + InternalMargin.Bottom;

            int left = area.X;
            int right = area.Width;

            if ( outlining != null )    // attach to left, allocate area
            {
                outlining.Location = new Point(left, area.Y);
                outlining.Size = new Size(outlining.Width, area.Height);
                left += outlining.Width;
            }

            if ( vsc != null )      // attach to right or left..
            {
                vsc.Size = new Size(ScrollBarWidth, area.Height - dgvcolumnheaderheight);

                if (!VerticalScrollBarDockRight)
                {
                    vsc.Location = new Point(left, area.Y + dgvcolumnheaderheight);
                    left += ScrollBarWidth;
                }
                else
                {
                    right -= ScrollBarWidth;
                    vsc.Location = new Point(right, area.Y + dgvcolumnheaderheight);
                }
            }

            if (dgv != null)                       // finally, put the dgv between left and right
            {
                dgv.Location = new Point(left, area.Y);
                dgv.Size = new Size(right-left, area.Height);
                dgvcolumnheaderheight = dgv.ColumnHeadersHeight;
            }

            UpdateScrollBar();
        }

        private void UpdateScrollBar()
        {
            if (dgv != null && vsc != null) // may not be attached at various design points
            {
                int toprowindex = dgv.FirstDisplayedScrollingRowIndex;                                  // this index ignores invisible rows
                int visibleindex = dgv.Rows.GetNumberOfVisibleRowsAbove(toprowindex);                   // so we translate to visible rows above index
                int totalvisible = dgv.Rows.GetRowCount(DataGridViewElementStates.Visible);             // this gives total visible - this is now the scroll bar range
                int visibleonscreen = dgv.DisplayedRowCount(false);                                     // and the viewport size..
                //System.Diagnostics.Debug.WriteLine(dgv.Name + " FDRow " + toprowindex + " Visible index " + visibleindex + " Total visible " + totalvisible + " On screen " + visibleonscreen);
                vsc.SetValueMaximumLargeChange(visibleindex, totalvisible - 1, visibleonscreen);
            }
        }

        protected void DGVRowsAdded(Object sender, DataGridViewRowsAddedEventArgs e)
        {
            int firstvisible = dgv.FirstDisplayedScrollingRowIndex;

            if (firstvisible >= 0)  // prevents updates while initially generating the dgv
            {
                UpdateScrollBar();
                outlining?.RowAdded(e.RowIndex,e.RowCount);
            }
        }

        protected void DGVRowsRemoved(Object sender, DataGridViewRowsRemovedEventArgs e)
        {
            UpdateScrollBar();
            outlining?.RowRemoved(e.RowIndex,e.RowCount);
        }

        protected virtual void DGVRowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged.Equals(DataGridViewElementStates.Visible))
            {
                UpdateScrollBar();
                outlining?.RowChangedState(e.Row.Index);
            }
        }

        bool ignoredgvscroll = false;   // stops recursion when programatically changing first row pos

        protected void DGVScrolled(Object sender, ScrollEventArgs e)     // occurs if keyboard or we programatically change the position
        {
            if (ignoredgvscroll == false)
            {
                UpdateScrollBar();
                outlining?.UpdateOutlines();
            }
        }

        protected virtual void Wheel(object sender, MouseEventArgs e)   // wheel changed, move vsc
        {
            if (vsc != null)
            {
                if (e.Delta > 0)
                    vsc.ValueLimited--;                 // control takes care of end limits..
                else
                    vsc.ValueLimited++;                 // end is UserLimit, not maximum

                MoveDGVToRow(vsc.Value);
            }
        }

        protected virtual void ScrollBarMoved(object sender, ScrollEventArgs e)     // scroll bar moved, move dgv
        {
            MoveDGVToRow(vsc.Value);
        }

        private void MoveDGVToRow(int rowindex)                   // given row index, find it taking into account visibility
        {
            if (dgv != null )                      
            {
                for (int rowi = 0; rowi < dgv.RowCount; rowi++)
                {
                    if (dgv.Rows[rowi].Visible == true && rowindex-- == 0)
                    {
                        ignoredgvscroll = true; // don't fire the DGVScrolled.. as we can get into a cycle if rows are hidden

                        dgv.SafeFirstDisplayedScrollingRowIndex(rowi);
                        //System.Diagnostics.Debug.WriteLine("Set to " + rowi + " displayed " + dgv.DisplayedRowCount(false));
                        vsc.LargeChange = dgv.DisplayedRowCount(false); // fix nov 20, need to reset as changing row index can change visible rows

                        dgv.Update();
                        vsc.Update();
                        ignoredgvscroll = false;
                        outlining?.UpdateOutlines();
                        return;
                    }
                }
            }
        }

        #endregion

        #region Variables

        DataGridView dgv;
        ExtScrollBar vsc;
        ExtPanelDataGridViewScrollOutlining outlining;

        #endregion
    }
}
