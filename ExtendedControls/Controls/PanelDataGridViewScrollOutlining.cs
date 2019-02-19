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
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtPanelDataGridViewScrollOutlining : Panel      
    {
        public class RollUpRange        // do not alter if you get one as a reference via Find
        {
            public int start;
            public int end;
            public int level;
            internal ExtPanelDrawn button;
            public bool Overlapped(RollUpRange other) { return (start >= other.start && start <= other.end) || (end >= other.start && end <= other.end); }
            public bool display;
            public int ystart, yend;
        };

        #region Interface

        public ExtPanelDataGridViewScrollOutlining() : base()
        {
        }

        public RollUpRange Find(int rowstart, int rowend) => Rollups.Find(x => x.start == rowstart && x.end == rowend);

        public bool Add(int rowstart, int rowend)       // add new area, area must be unique
        {
            if (Find(rowstart, rowend) == null)
            {
                RollUpRange r = new RollUpRange() { start = rowstart, end = rowend };
                r.button = new ExtPanelDrawn() { Visible = false, ImageSelected = ExtPanelDrawn.ImageType.Collapse, Padding = new Padding(0), Size = new Size(butsize, butsize), ForeColor = this.ForeColor };
                r.button.Tag = r;
                r.button.Click += Button_Click;
                Controls.Add(r.button);
                Rollups.Add(r);
                RollUpChanged();
                Scrolled();
                return true;
            }
            else
                return false;
        }


        public bool Remove(int rowstart, int rowend)        // remove it
        {
            RollUpRange r = Find(rowstart, rowend);
            if (r != null)
            {
                Controls.Remove(r.button);
                r.button.Dispose();
                Rollups.Remove(r);
                RollUpChanged();
                Scrolled();
                return true;
            }
            else
                return false;
        }

        public bool State( RollUpRange r)       // what is its state
        {
            return dgv.Rows[r.start].Visible;
        }

        public bool SetState( RollUpRange rur , bool state )        // and change its state
        {
            if (rur != null)
            {
                if (Parent is ExtPanelDataGridViewScroll)   // this implements an efficient visibility change system
                {
                    (Parent as ExtPanelDataGridViewScroll).ChangeVisibility(rur.start, rur.end - 1, state);
                }
                else
                {
                    processed = true;
                    for (int r = rur.start; r < rur.end; r++)
                    {
                        dgv.Rows[r].Visible = state;
                    }
                    processed = false;

                    Scrolled();
                }
                return true;
            }
            else
                return false;
        }

        #endregion

        #region Implementation

        public void SetDGV(DataGridView dgv)    // called to set dgv
        {
            this.dgv = dgv;
        }

        private void RollUpChanged()        // changed list of rollups, resort, work out level of each, reset the outlining size
        {
            Rollups.Sort(delegate (RollUpRange first, RollUpRange last) { return first.start.CompareTo(last.start); }); // in start order

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

            this.Width = Math.Max((butsize + butpadding) * (maxlevel + 1) + butleft,10);          // resize control appropriately
        }

        public void RowAdded(int row)
        {
            Scrolled();
        }

        public void RowRemoved(int row)
        {
            List<RollUpRange> removelist = new List<RollUpRange>();

            foreach( var r in Rollups )     // double check they are not removing a start or end row..
            {
                if (r.start == row || r.end == row)
                    removelist.Add(r);
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
            int toprow = dgv?.FirstDisplayedScrollingRowIndex ?? -1;

            if (toprow >= 0)
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

                SuspendLayout();

                for (int i = 0; i < Rollups.Count; i++)
                {
                    var rur = Rollups[i];
                    bool endvisible = dgv.Rows[rur.end].Visible;            // endvisible = not = outer has collapsed the whole list

                    if ( endvisible )       // end must be visible, else something else has rolled it up
                    {
                        bool startvisible = dgv.Rows[rur.start].Visible;        // startvisible = not = outer collapsed, or this is collapsed

                        bool startonscreen = dgv.Rows[rur.start].Displayed;
                        bool endonscreen = dgv.Rows[rur.end].Displayed;
                        bool topbotinrange = (toprow >= rur.start && toprow <= rur.end) || (botrow >= rur.start && botrow <= rur.end);

                        // we display the line if start on screen, if end on screen and start visible, or we are within a greater range.
                        rur.display = startonscreen || ( endonscreen && startvisible) || (topbotinrange && startvisible);
                        rur.button.Visible = endonscreen;

                        if (endonscreen)
                        {
                            rur.button.Location = new Point(butleft + rur.level * (butsize + butpadding), dgv.GetRowDisplayRectangle(rur.end, true).Bottom - butsize);
                            rur.button.ImageSelected = startvisible ? ExtPanelDrawn.ImageType.Collapse : ExtPanelDrawn.ImageType.Expand;
                        }

                        if (rur.display)
                        {   
                            rur.yend = endonscreen ? dgv.GetRowDisplayRectangle(rur.end, true).Bottom - butsize : this.Height;
                            rur.ystart = startonscreen ? dgv.GetRowDisplayRectangle(rur.start, true).Top : dgv.ColumnHeadersHeight;
                        }

                        //System.Diagnostics.Debug.WriteLine("Bar " + i + " " + rur.start + "-" + rur.end + " sv:" + startvisible + " ev:" + endvisible + " sos:" + startonscreen + " eos:" + endonscreen + " topbotinrange:" + topbotinrange + " display:" + rur.display);
                    }
                    else
                    {
                        //System.Diagnostics.Debug.WriteLine("Bar " + i + " invisible");
                        rur.display = false; // both invisible, means this is collapsed
                        rur.button.Visible = false;
                    }

                }

                ResumeLayout();
                Invalidate();
            }

        }

        protected override void OnPaint(PaintEventArgs pe)      // note you can't control the controls in here, it causes a repaint!  only can paint
        {
            //System.Diagnostics.Debug.WriteLine("Repaint");
            for (int i = 0; i < Rollups.Count; i++)
            {
                var rur = Rollups[i];

                if ( rur.display )
                {
                    bool startonscreen = dgv.Rows[rur.start].Displayed;
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
            RollUpRange rur = but.Tag as RollUpRange;

            SetState(rur, !State(rur));
        }

        #endregion

        #region Variables

        DataGridView dgv;
        const int butpadding = 2;
        const int butleft = 2;
        const int butsize = 16;
        private List<RollUpRange> Rollups { get; set; } = new List<RollUpRange>();

        #endregion
    }
}


          