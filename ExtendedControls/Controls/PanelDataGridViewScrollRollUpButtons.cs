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
    public class ExtPanelDataGridViewScrollRollUpButtons : Panel      
    {
        public class RollUpRange
        {
            public int start;
            public int end;
            public bool collapsed;
            public int level;
            internal ExtPanelDrawn button;
            public bool Overlapped(RollUpRange other) { return (start >= other.start && start <= other.end) || (end >= other.start && end <= other.end); }
        };

        public List<RollUpRange> Rollups { get; private set; } = new List<RollUpRange>();

        #region Implementation

        public ExtPanelDataGridViewScrollRollUpButtons() : base()
        {
        }

        public void RowAdded(int row)
        {
            Invalidate();
        }
        public void RowRemoved(int row)
        {
            Invalidate();
        }
        public void RowChangedState(int row)
        {
            Invalidate();
        }

        public void Add(int rowstart, int rowend)
        {
            RollUpRange r = new RollUpRange() { start = rowstart, end = rowend };
            r.button = new ExtPanelDrawn() { ImageSelected = ExtPanelDrawn.ImageType.Collapse, Padding = new Padding(0), Size = new Size(butsize,butsize), ForeColor = this.ForeColor };
            r.button.Tag = r;
            r.button.Click += Button_Click;
            Controls.Add(r.button);
            Rollups.Add(r);
            LevelUp();
            Invalidate();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ExtPanelDrawn but = sender as ExtPanelDrawn;
            RollUpRange r = but.Tag as RollUpRange;
            r.collapsed = !r.collapsed;
            Invalidate();
            if ( r.collapsed )
            {

            }
        }

        void LevelUp()
        {
            Rollups.Sort(delegate (RollUpRange first, RollUpRange last) { return first.start.CompareTo(last.start); }); // in start order

            foreach (var r in Rollups)
                r.level = 0;

            int maxlevel = 0;

            for (int i = 0; i < Rollups.Count; i++)
            {
                for (int j = i+1; j < Rollups.Count; j++)
                {
                    if ( Rollups[j].Overlapped(Rollups[i]))
                    {
                        Rollups[j].level = Rollups[i].level + 1;
                        maxlevel = Math.Max(Rollups[j].level, maxlevel);
                    }
                }
            }

            Width = (butsize + 2) * (maxlevel+1);
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            int toprow = dgv?.FirstDisplayedScrollingRowIndex ?? -1;
            if ( toprow >= 0 )
            {
                int depth = dgv.DisplayedRowCount(true);

                for( int i = 0; i < Rollups.Count; i++)
                {
                    var rur = Rollups[i];
                    int relstart = rur.start - toprow;
                    int relend = rur.end - toprow;

                    if ((relstart >= 0 && relstart < depth) || (relend >= 0 && relend < depth))
                    {
                        //if ( dgv.Rows[relstart].Visible && dg)
                        System.Diagnostics.Debug.WriteLine("display " + toprow + " depth " + depth + " rel " + relstart + " " + relend + " | " + rur.start + " - " + rur.end);

                        int barx = butleft + (butsize+2) * rur.level;

                        if (relstart < 0)
                        {
                            if (relend >= depth)
                            {
                                System.Diagnostics.Debug.WriteLine("..both out - display full non end bar");
                                DisplayBar(pe.Graphics, barx, dgv.ColumnHeadersHeight, dgv.Bottom, false, false, i);
                            }
                            else
                            {
                                var rowendrect = dgv.GetRowDisplayRectangle(rur.end, true);
                                DisplayBar(pe.Graphics, barx, dgv.ColumnHeadersHeight, (rowendrect.Top + rowendrect.Bottom) / 2 - 8, false, true, i);
                                System.Diagnostics.Debug.WriteLine("..top out - display top to end bar " + rowendrect.Bottom);
                            }
                        }
                        else
                        {
                            var rowstartrect = dgv.GetRowDisplayRectangle(rur.start, true);

                            if (relend >= depth)
                            {
                                System.Diagnostics.Debug.WriteLine("..bot out - display " + rowstartrect.Top + " to end");
                                DisplayBar(pe.Graphics, barx, rowstartrect.Top, dgv.Bottom, true, false, i);
                            }
                            else
                            {
                                var rowendrect = dgv.GetRowDisplayRectangle(rur.end, true);
                                System.Diagnostics.Debug.WriteLine("..all in");
                                DisplayBar(pe.Graphics, barx, rowstartrect.Top, (rowendrect.Top + rowendrect.Bottom)/2 - 8, true, true, i);    // all in
                            }
                        }

                        System.Diagnostics.Debug.WriteLine("..");

                    }
                    else
                        rur.button.Visible = false;
                }
            }
        }

        public void DisplayBar(Graphics g, int x, int top, int bot, bool topbar, bool botbut, int butno)
        {
            System.Diagnostics.Debug.WriteLine("Bar " + butno + " " + top + " " + bot + " " + topbar + " " + botbut);

            using (Brush b = new SolidBrush(this.ForeColor))
            {
                using (Pen p = new Pen(b))
                {
                    Point tp = new Point(x, top);
                    Point bp = new Point(x, bot);
                    g.DrawLine(p, tp, bp );

                    if (topbar)
                    {
                        g.DrawLine(p, tp, new Point(x+butsize/4, tp.Y));
                    }
                    if (botbut)
                    {
                        Rollups[butno].button.Location = new Point(x-butsize/2, bp.Y);
                        Rollups[butno].button.ImageSelected = Rollups[butno].collapsed ? ExtPanelDrawn.ImageType.Expand : ExtPanelDrawn.ImageType.Collapse;
                    }
                    Rollups[butno].button.Visible = botbut;

                }
            }
        }

        public void SetDGV(DataGridView dgv)
        {
            this.dgv = dgv;
        }

        #endregion

        #region Variables

        DataGridView dgv;
        const int butleft = 8;
        const int butsize = 16;

        #endregion
    }
}
