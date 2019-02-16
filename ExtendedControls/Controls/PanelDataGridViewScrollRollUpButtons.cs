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
        };

        public List<RollUpRange> Rollups { get; private set; } = new List<RollUpRange>();

        #region Implementation

        public ExtPanelDataGridViewScrollRollUpButtons() : base()
        {
        }

        public int GetWantedWidth() { return 50; }

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
            Rollups.Add(new RollUpRange() { start = rowstart, end = rowend });
            ExtPanelDrawn pb = new ExtPanelDrawn() { ImageSelected = ExtPanelDrawn.ImageType.Expand, Padding = new Padding(0), Size = new Size(16,16) };
            buttons.Add(pb);
            Controls.Add(pb);
            Invalidate();
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
                        System.Diagnostics.Debug.WriteLine("display " + toprow + " depth " + depth + " rel " + relstart + " " + relend + " | " + rur.start + " - " + rur.end);

                        if ( relstart < 0 )
                        {
                            if ( relend >= depth )
                            {
                                System.Diagnostics.Debug.WriteLine("..both out - display full non end bar");
                                DisplayBar(pe.Graphics, dgv.ColumnHeadersHeight, dgv.Bottom, false, false, i);
                            }
                            else
                            {
                                var rowendrect = dgv.GetRowDisplayRectangle(rur.end, true);
                                DisplayBar(pe.Graphics, dgv.ColumnHeadersHeight, rowendrect.Bottom, false, true, i);
                                System.Diagnostics.Debug.WriteLine("..top out - display top to end bar " + rowendrect.Bottom);
                            }
                        }
                        else
                        {
                            var rowstartrect = dgv.GetRowDisplayRectangle(rur.start, true);

                            if ( relend >= depth )
                            {
                                System.Diagnostics.Debug.WriteLine("..bot out - display " + rowstartrect.Top + " to end");
                                DisplayBar(pe.Graphics, rowstartrect.Top, dgv.Bottom, true, false, i);
                            }
                            else
                            {
                                var rowendrect = dgv.GetRowDisplayRectangle(rur.end, true);
                                System.Diagnostics.Debug.WriteLine("..all in");
                                DisplayBar(pe.Graphics, rowstartrect.Top, rowendrect.Bottom, true, true, i);
                            }
                        }

                        System.Diagnostics.Debug.WriteLine("..");

                    }
                }
            }
        }

        public void DisplayBar(Graphics g, int top, int bot, bool topbut, bool botbut, int butno)
        {
            using (Brush b = new SolidBrush(Color.Red))
            {
                using (Pen p = new Pen(b))
                {
                    Point tp = new Point(5, top);
                    Point bp = new Point(5, bot);
                    g.DrawLine(p, tp, bp );

                    if (topbut)
                    {
                        g.DrawLine(p, tp, new Point(10, tp.Y));
                    }
                    if (botbut)
                    {
                        buttons[butno].Location = new Point(5, bp.Y);
                    }

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
        List<ExtPanelDrawn> buttons = new List<ExtPanelDrawn>();

        #endregion
    }
}
