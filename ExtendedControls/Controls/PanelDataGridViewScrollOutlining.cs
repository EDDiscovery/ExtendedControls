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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtPanelDataGridViewScrollOutlining : Panel      
    {
        public class Outline
        {
            public int start;
            public int end;
            public bool expanded;

            public Outline() { }
            public Outline(int start, int end, bool expanded = true) { this.start = start; this.end = end; this.expanded = expanded; }
        }

        public int KeepLastEntriesVisibleOnRollUp { get; set; } = 1;

        public IEnumerable OutlineSet() { return (from x in Outlines select x.r); }

        #region Interface

        public ExtPanelDataGridViewScrollOutlining() : base()
        {
        }

        public Outline Find(int rowstart, int rowend)
        {
            OutlineState rur = FindEntry(rowstart, rowend);
            if (rur != null)
                rur.r.expanded = dgv.Rows[rowstart].Visible;

            return rur?.r;
        }

        // Add/remove does not affect DGV - you need to do that yourself
        // areas must be unique
        // no update means you must call UpdateAfterAdd.
        public bool Add(int rowstart, int rowend, bool expandedout = true, bool noupdate = false)       
        {
            if (FindEntry(rowstart, rowend) == null)
            {
                OutlineState rup = new OutlineState();
                rup.r = new Outline() { start = rowstart, end = rowend, expanded = expandedout };
                Outlines.Add(rup);

                if (!noupdate)
                {
                    RollUpListChanged();
                    UpdateOutlines();
                }
                return true;
            }
            else
                return false;
        }

        public void UpdateAfterAdd()
        {
            RollUpListChanged();
            UpdateOutlines();
        }

        public bool Remove(int rowstart, int rowend)        // remove it
        {
            OutlineState r = FindEntry(rowstart, rowend);
            if (r != null)
            {
                if (r.button != null)
                {
                    Controls.Remove(r.button);
                    r.button.Dispose();
                }

                Outlines.Remove(r);

                RollUpListChanged();
                UpdateOutlines();
                Invalidate();   // ensure in case we have gone to zero rollups
                return true;
            }
            else
                return false;
        }

        public void Clear()
        {
            SuspendLayout();
            foreach (var rur in Outlines)
            {
                if (rur.button != null)
                {
                    Controls.Remove(rur.button);
                    rur.button.Dispose();
                }
            }

            Outlines.Clear();
            this.Width = MinWidth;

            ResumeLayout();
            Invalidate();
        }

        #endregion

        #region Implementation

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            UpdateOutlines();
        }

        public void SetDGV(DataGridView dgv)    // called to set dgv
        {
            this.dgv = dgv;
        }

        private void RollUpListChanged()        // changed list of rollups, resort, work out level of each, reset the outlining size
        {
            Outlines.Sort(delegate (OutlineState first, OutlineState last) 
                {
                    int c = first.r.start.CompareTo(last.r.start);      // start order, then in largest range (biggest end)
                    return c != 0 ? c : -first.r.end.CompareTo(last.r.end); // so smaller ones are placed after larger ranges
                });

            SuspendLayout();

            foreach (var r in Outlines)
            {
                if (r.button == null)       // check we have a button, if not, add..
                {
                    r.button = new ExtButtonDrawn() { Visible = false, ImageSelected = ExtButtonDrawn.ImageType.Collapse, Padding = new Padding(0), Size = new Size(butsize, butsize), ForeColor = this.ForeColor };
                    r.button.Tag = r;
                    r.button.Click += Button_Click;
                    Controls.Add(r.button);
                }
                r.level = 0;
            }

            int maxlevel = 0;

            for (int i = 0; i < Outlines.Count; i++)
                Outlines[i].parent = -1;

            for (int i = 0; i < Outlines.Count; i++)
            {
                for (int j = i + 1; j < Outlines.Count; j++)
                {
                    if (Outlines[j].Overlapped(Outlines[i]))
                    {
                        Outlines[j].level = Outlines[i].level + 1;
                        Outlines[j].parent = i;
                        maxlevel = Math.Max(Outlines[j].level, maxlevel);
                    }
                }
            }

            ResumeLayout();
            int width = Math.Max((butsize + butpadding) * (maxlevel + 1) + butleft, MinWidth);          // resize control appropriately

            if (width != this.Width)
                this.Width = width;
        }

        public void UpdateOutlines()      // something materially changed - work out display and set controls
        {
            if (Outlines.Count > 0)
            {
                int toprow = dgv?.SafeFirstDisplayedScrollingRowIndex() ?? -1;

                if (toprow >= 0 )
                {
                    processed = true;
                    int rowsdisplayed = dgv.DisplayedRowCount(true);

                    int botrow = toprow;
                    int rowscounted = 0;
                    for (int i = toprow + 1; i < dgv.Rows.Count; i++)
                    {
                        if (dgv.Rows[i].Displayed)  // from the next one, go thru all, counting off displayed rows, until we have enough, to estimate where bot row is
                        {
                            botrow = i;
                            if (++rowscounted == rowsdisplayed)     // we have our total of rows, quit now
                                break;
                        }
                    }

                    //System.Diagnostics.Debug.WriteLine("***** toprow " + toprow + " botrow " + botrow + Environment.StackTrace.StackTrace("Scrolled",3));
  //                  System.Diagnostics.Debug.WriteLine("***** toprow " + toprow + " botrow " + botrow + " rows disp " + rowsdisplayed + " Rowc " + dgv.RowCount);

                    SuspendLayout();

                    for (int i = 0; i < Outlines.Count; i++)
                    {
                        var rur = Outlines[i];

                        if (rur.button != null)     // we may be doing a delayed add, and get scrolled, so cope with it.. no button is set 
                        {
                            System.Diagnostics.Debug.Assert(i > 0 || rur.parent == -1);     // first must have -1 as parent else we have not called roll up

                            bool completelyhidden = false;
                            for (int p = rur.parent; p != -1; p = Outlines[p].parent)       // go up tree and see if any parents are hidden, if they are, we are off
                            {
                                if (Outlines[p].r.expanded == false)
                                {
                                    completelyhidden = true;
                                    break;
                                }
                            }

                            if (!completelyhidden)    // a child may have hidden out end row, so we don't have anywhere to display the button
                            {
                                completelyhidden = !dgv.Rows[rur.r.end].Visible;        // check its there

                                //   if (rur.r.start >= 24000 && rur.r.start <= 25000)
                                //     if (!completelyhidden ) System.Diagnostics.Debug.WriteLine("Line " + rur.r.end + "is not visible so hiding bar");
                            }

                            if (!completelyhidden)       // end must be visible, else something else has rolled it up
                            {
                                bool startonscreen = dgv.Rows[rur.r.start].Displayed;
                                bool endonscreen = dgv.Rows[rur.r.end].Displayed;
                                bool topbotinrange = (toprow >= rur.r.start && toprow <= rur.r.end) || (botrow >= rur.r.start && botrow <= rur.r.end);

                                rur.linedisplay = startonscreen || (endonscreen && rur.r.expanded) || (topbotinrange && rur.r.expanded);

                                // we display the line if start on screen, if end on screen and start visible (not rolled up), or we are within a greater range and not rolled up
                                rur.linedisplay = startonscreen || (endonscreen && rur.r.expanded) || (topbotinrange && rur.r.expanded);

                                if (rur.linedisplay)        // if line on screen, calc extent.
                                {
                                    rur.ystart = startonscreen ? dgv.GetRowDisplayRectangle(rur.r.start, true).Top + butpadding : dgv.ColumnHeadersHeight;
                                    rur.yend = (endonscreen ? dgv.GetRowDisplayRectangle(rur.r.end, true).Top : this.Height);
                                }

                                bool butvis = rur.linedisplay || endonscreen;       // if at end, or line, we have a button
                                rur.button.Visible = butvis;

                                if (butvis)
                                {
                                    int bx = butleft + rur.level * (butsize + butpadding);
                                    int by = endonscreen ? dgv.GetRowDisplayRectangle(rur.r.end, true).Top : (rur.r.start == botrow) ? dgv.GetRowDisplayRectangle(rur.r.start, true).Top : (this.Height - butsize);

                                    rur.button.Location = new Point(bx, by);
                                    rur.button.ImageSelected = rur.r.expanded ? ExtButtonDrawn.ImageType.Collapse : ExtButtonDrawn.ImageType.Expand;
                                  //  System.Diagnostics.Debug.WriteLine(rur.r.start + "-" + rur.r.end + " button state " + rur.r.expanded);
                                }

                                //   if (rur.r.start >= 24000 && rur.r.start <= 25000)
                                //      System.Diagnostics.Debug.WriteLine("Bar " + i + " " + rur.r.start + "-" + rur.r.end + " sos:" + startonscreen + " eos:" + endonscreen + " topbotinrange:" + topbotinrange + " display:" + rur.linedisplay + " but " + butvis);
                            }
                            else
                            {
                                //     if (rur.r.start >= 24000 && rur.r.start <= 25000)
                                //       System.Diagnostics.Debug.WriteLine("Bar " + i + " " + rur.r.start + "-" + rur.r.end + " invisible parent " + rur.parent);

                                rur.linedisplay = false; // both invisible, means this is collapsed
                                rur.button.Visible = false;
                            }
                        }
                    }

                    ResumeLayout();
                    Invalidate();
                    Update();
                }
            }

        }

        protected override void OnPaint(PaintEventArgs pe)      // note you can't control the controls in here, it causes a repaint!  only can paint
        {
            //System.Diagnostics.Debug.WriteLine("Repaint");
            for (int i = 0; i < Outlines.Count; i++)
            {
                var rur = Outlines[i];

                if ( rur.linedisplay )
                {
                    bool startonscreen = dgv.Rows[rur.r.start].Displayed;
                    int x = butleft + butsize/2 + rur.level * (butsize + 2);

                    using (Brush b = new SolidBrush(this.ForeColor))
                    {
                        using (Pen p = new Pen(b))
                        {
                            Point tp = new Point(x, rur.ystart);
                            Point bp = new Point(x, rur.yend);
                            pe.Graphics.DrawLine(p, tp, bp);

                            if (startonscreen)
                                pe.Graphics.DrawLine(p, tp, new Point(tp.X + butsize / 4, tp.Y));
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ExtButtonDrawn but = sender as ExtButtonDrawn;
            OutlineState rur = but.Tag as OutlineState;

            if (Parent is ExtPanelDataGridViewScroll)   // this implements an efficient visibility change system
            {
                rur.r.expanded = !rur.r.expanded;

                if (rur.r.expanded == false)       // off is easy - hide everything
                {
                    (Parent as ExtPanelDataGridViewScroll).ChangeVisibility(rur.r.start, rur.r.end - KeepLastEntriesVisibleOnRollUp, rur.r.expanded);
                }
                else
                {
                    BaseUtils.IntRangeList irl = new BaseUtils.IntRangeList();
                    irl.Add(rur.r.start, rur.r.end);
                    Vis(Outlines.IndexOf(rur), irl);         // remove visibility of any children marked hidden
                    irl.Sort();
                    (Parent as ExtPanelDataGridViewScroll).ChangeVisibility(rur.r.start, rur.r.end, irl);
               }
            }
        }

        private void Vis(int parent, BaseUtils.IntRangeList irl)
        {
            for (int j = 0; j < Outlines.Count; j++)     // go thru roll ups, looking for parent, and if off, mark areas as removed from vis
            {
                if ( Outlines[j].parent == parent )
                {
                    if ( Outlines[j].r.expanded == false )
                    {
                        irl.Remove(Outlines[j].r.start, Outlines[j].r.end - KeepLastEntriesVisibleOnRollUp);      // remove this range
                    }

                    Vis(j, irl);    // any children..
                }
            }
        }

        public void RowAdded(int row, int count)
        {
            UpdateOutlines();
        }

        public void RowRemoved(int row, int count)
        {
            List<OutlineState> removelist = new List<OutlineState>();

            foreach (var rur in Outlines)     // double check they are not removing a start or end row..
            {
                if ((rur.r.start >= row && rur.r.start < row + count) || (rur.r.end >= row && rur.r.end < row + count) )
                    removelist.Add(rur);
            }

            foreach (var r in removelist)       // if so, remove the roll up
                Outlines.Remove(r);

            if (removelist.Count > 0)       // if changed, rework states
                RollUpListChanged();

            UpdateOutlines();
        }

        public void RowChangedState(int row)
        {
            if (!processed)       // need some trigger to get the first process done
                UpdateOutlines();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (!processed)       // need some trigger to get the first process done
                UpdateOutlines();
        }

        bool processed = false;



        #endregion

        #region Variables

        DataGridView dgv;
        const int MinWidth = 2;
        const int butpadding = 2;
        const int butleft = 2;
        const int butsize = 16;
        private List<OutlineState> Outlines { get; set; } = new List<OutlineState>();

        private class OutlineState        
        {
            public Outline r;

            public int level;
            public int parent;
            internal ExtButtonDrawn button;
            public bool Overlapped(OutlineState other) { return (r.start >= other.r.start && r.start <= other.r.end) || (r.end >= other.r.start && r.end <= other.r.end); }
            public bool linedisplay;
            public int ystart, yend;
        };

        private OutlineState FindEntry(int rowstart, int rowend) => Outlines.Find(x => x.r.start == rowstart && x.r.end == rowend);

        #endregion
    }
}


          