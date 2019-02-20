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

        public bool Add(int rowstart, int rowend)       // add new area, area must be unique
        {
            if (FindEntry(rowstart, rowend) == null)
            {
                OutlineState rup = new OutlineState();
                rup.r = new Outline() { start = rowstart, end = rowend };
                rup.button = new ExtPanelDrawn() { Visible = false, ImageSelected = ExtPanelDrawn.ImageType.Collapse, Padding = new Padding(0), Size = new Size(butsize, butsize), ForeColor = this.ForeColor };
                rup.button.Tag = rup;
                rup.button.Click += Button_Click;
                Controls.Add(rup.button);
                Rollups.Add(rup);
                RollUpChanged();
                Scrolled();
                return true;
            }
            else
                return false;
        }

        public void Add(List<Outline> list)     // applies visibility if parent is dgv scroll
        {
            foreach (var r in list)
            {
                if (FindEntry(r.start, r.end) == null)
                {
                    OutlineState rup = new OutlineState();
                    rup.r = r;
                    rup.button = new ExtPanelDrawn() { Visible = false, ImageSelected = ExtPanelDrawn.ImageType.Collapse, Padding = new Padding(0), Size = new Size(butsize, butsize), ForeColor = this.ForeColor };
                    rup.button.Tag = rup;
                    rup.button.Click += Button_Click;
                    Controls.Add(rup.button);
                    Rollups.Add(rup);
                }
            }

            if (Parent is ExtPanelDataGridViewScroll)   // this implements an efficient visibility change system
            {
                var tuple = (from x in list orderby x.start select new Tuple<int, int, bool>(x.start, x.end- KeepLastEntriesVisibleOnRollUp, x.expanded)).ToList();
                (Parent as ExtPanelDataGridViewScroll).ChangeVisibility(tuple);
            }

            RollUpChanged();
            Scrolled();
        }

        public bool Remove(int rowstart, int rowend)        // remove it
        {
            OutlineState r = FindEntry(rowstart, rowend);
            if (r != null)
            {
                Controls.Remove(r.button);
                r.button.Dispose();
                Rollups.Remove(r);
                RollUpChanged();
                Scrolled();
                Invalidate();   // ensure in case we have gone to zero rollups
                return true;
            }
            else
                return false;
        }

        public void Clear()
        {
            SuspendLayout();
            foreach (var rur in Rollups)
            {
                Controls.Remove(rur.button);
                rur.button.Dispose();
            }

            Rollups.Clear();
            this.Width = MinWidth;

            ResumeLayout();
            Invalidate();
        }

        #endregion

        #region Implementation

        public void SetDGV(DataGridView dgv)    // called to set dgv
        {
            this.dgv = dgv;
        }

        private void RollUpChanged()        // changed list of rollups, resort, work out level of each, reset the outlining size
        {
            Rollups.Sort(delegate (OutlineState first, OutlineState last) { return first.r.start.CompareTo(last.r.start); }); // in start order

            foreach (var r in Rollups)
                r.level = 0;

            int maxlevel = 0;

            for (int i = 0; i < Rollups.Count; i++)
            {
                for (int j = i + 1; j < Rollups.Count; j++)
                {
                    if (Rollups[j].Overlapped(Rollups[i]))
                    {
                        Rollups[j].level = Rollups[i].level + 1;
                        maxlevel = Math.Max(Rollups[j].level, maxlevel);
                    }
                }
            }

            this.Width = Math.Max((butsize + butpadding) * (maxlevel + 1) + butleft, MinWidth);          // resize control appropriately
        }

        public void RowAdded(int row)
        {
            Scrolled();
        }

        public void RowRemoved(int row)
        {
            List<OutlineState> removelist = new List<OutlineState>();

            foreach( var rur in Rollups )     // double check they are not removing a start or end row..
            {
                if (rur.r.start == row || rur.r.end == row)
                    removelist.Add(rur);
            }

            foreach (var r in removelist)       // if so, remove the roll up
                Rollups.Remove(r);

            if (removelist.Count > 0)       // if changed, rework states
                RollUpChanged();

            Scrolled();
        }

        public void RowChangedState(int row)
        {
            if (!processed)       // need some trigger to get the first process done
                Scrolled();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (!processed)       // need some trigger to get the first process done
                Scrolled();
        }

        bool processed = false;

        public void Scrolled()      // something materially changed - work out display and set controls
        {
            if (Rollups.Count > 0)
            {
                int toprow = dgv?.FirstDisplayedScrollingRowIndex ?? -1;

                if (toprow >= 0 )
                {
                    processed = true;
                    int rowsdisplayed = dgv.DisplayedRowCount(true);

                    int botrow = toprow;
                    int rowscounted = 0;
                    for (int i = toprow + 1; i < dgv.Rows.Count; i++)
                    {
                        if (dgv.Rows[i].Displayed)  // from the next one, go thru all, counting off displayed rows, until we have enough
                        {
                            botrow = i;
                            if (++rowscounted == rowsdisplayed)     // we have our total of rows, quit now
                                break;
                        }
                    }

                    //System.Diagnostics.Debug.WriteLine("***** toprow " + toprow + " botrow " + botrow + Environment.StackTrace.StackTrace("Scrolled",3));
  //                  System.Diagnostics.Debug.WriteLine("***** toprow " + toprow + " botrow " + botrow + " rows disp " + rowsdisplayed + " Rowc " + dgv.RowCount);

                    SuspendLayout();

                    for (int i = 0; i < Rollups.Count; i++)
                    {
                        var rur = Rollups[i];
                        bool endvisible = dgv.Rows[rur.r.end].Visible;            // endvisible = not = outer has collapsed the whole list

                        if (endvisible)       // end must be visible, else something else has rolled it up
                        {
                            bool startvisible = dgv.Rows[rur.r.start].Visible;        // startvisible = not = outer collapsed, or this is collapsed

                            bool startonscreen = dgv.Rows[rur.r.start].Displayed;
                            bool endonscreen = dgv.Rows[rur.r.end].Displayed;
                            bool topbotinrange = (toprow >= rur.r.start && toprow <= rur.r.end) || (botrow >= rur.r.start && botrow <= rur.r.end);

                            rur.linedisplay = startonscreen || (endonscreen && startvisible) || (topbotinrange && startvisible);

                            // we display the line if start on screen, if end on screen and start visible (not rolled up), or we are within a greater range and not rolled up
                            rur.linedisplay = startonscreen || (endonscreen && startvisible) || (topbotinrange && startvisible);

                            if (rur.linedisplay)        // if line on screen, calc extent.
                            {
                                rur.ystart = startonscreen ? dgv.GetRowDisplayRectangle(rur.r.start, true).Top+butpadding : dgv.ColumnHeadersHeight;
                                rur.yend = (endonscreen ? dgv.GetRowDisplayRectangle(rur.r.end, true).Top : this.Height);
                            }

                            bool butvis = rur.linedisplay || endonscreen;       // if at end, or line, we have a button
                            rur.button.Visible = butvis;

                            if ( butvis )
                            {
                                int bx = butleft + rur.level * (butsize + butpadding);
                                int by = endonscreen ? dgv.GetRowDisplayRectangle(rur.r.end, true).Top : (rur.r.start == botrow) ? dgv.GetRowDisplayRectangle(rur.r.start, true).Top :(this.Height-butsize);

                                rur.button.Location = new Point(bx,  by);
                                rur.button.ImageSelected = startvisible ? ExtPanelDrawn.ImageType.Collapse : ExtPanelDrawn.ImageType.Expand;
                            }

//                            System.Diagnostics.Debug.WriteLine("Bar " + i + " " + rur.r.start + "-" + rur.r.end + " sv:" + startvisible + " ev:" + endvisible + " sos:" + startonscreen + " eos:" + endonscreen + " topbotinrange:" + topbotinrange + " display:" + rur.linedisplay + " but " + butvis);
                        }
                        else
                        {
                            //System.Diagnostics.Debug.WriteLine("Bar " + i + " invisible");
                            rur.linedisplay = false; // both invisible, means this is collapsed
                            rur.button.Visible = false;
                        }

                    }

                    ResumeLayout();
                    Invalidate();
                }
            }

        }

        protected override void OnPaint(PaintEventArgs pe)      // note you can't control the controls in here, it causes a repaint!  only can paint
        {
            //System.Diagnostics.Debug.WriteLine("Repaint");
            for (int i = 0; i < Rollups.Count; i++)
            {
                var rur = Rollups[i];

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
            ExtPanelDrawn but = sender as ExtPanelDrawn;
            OutlineState rur = but.Tag as OutlineState;

            if (Parent is ExtPanelDataGridViewScroll)   // this implements an efficient visibility change system
            {
                bool state = !dgv.Rows[rur.r.start].Visible;
                (Parent as ExtPanelDataGridViewScroll).ChangeVisibility(rur.r.start, rur.r.end - KeepLastEntriesVisibleOnRollUp, state);
            }
        }

        #endregion

        #region Variables

        DataGridView dgv;
        const int MinWidth = 10;
        const int butpadding = 2;
        const int butleft = 2;
        const int butsize = 16;
        private List<OutlineState> Rollups { get; set; } = new List<OutlineState>();

        private class OutlineState        // do not alter if you get one as a reference via Find
        {
            public Outline r;

            public int level;
            internal ExtPanelDrawn button;
            public bool Overlapped(OutlineState other) { return (r.start >= other.r.start && r.start <= other.r.end) || (r.end >= other.r.start && r.end <= other.r.end); }
            public bool linedisplay;
            public int ystart, yend;
        };

        private OutlineState FindEntry(int rowstart, int rowend) => Rollups.Find(x => x.r.start == rowstart && x.r.end == rowend);

        #endregion
    }
}


          