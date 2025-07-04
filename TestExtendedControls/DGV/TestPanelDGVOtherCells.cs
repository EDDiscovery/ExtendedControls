using BaseUtils;
using ExtendedControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExtendedControls
{
    public partial class TestPanelDGVOtherCells : Form
    {
        ThemeList theme;

        Timer tme = new Timer();

        public TestPanelDGVOtherCells()
        {
            InitializeComponent();

            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            dataGridView.EnableCellHoverOverCallback();
            dataGridView.HoverOverCell += (a, b, c) => HoverOverCell(a, b, c);

            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            //dataGridView.RowHeightChanged += (a, e) => { System.Diagnostics.Debug.WriteLine("DGV Row Height"); };

            dataGridView.Dock = DockStyle.Fill;

            for (int i = 0; i < 202; i++)
            {
                var rw = dataGridView.RowTemplate.Clone() as DataGridViewRow;
                if (i % 2 == 1)
                {
                    DataGridViewTextImageCell c1 = new DataGridViewTextImageCell();
                    c1.Value = "Cell Col1 Row" + i;
                    DataGridViewTextImageCell c2 = new DataGridViewTextImageCell();
                    c2.Value = "Cell Col2 Row" + i;
                    c2.Style.ForeColor = Color.Red;
                    DataGridViewTextImageCell c3 = new DataGridViewTextImageCell();
                    c3.Value = "Cell Col3 Row" + i;
                    c3.Style.ForeColor = Color.Red;
                    c3.Style.Alignment = DataGridViewContentAlignment.TopRight;
                    rw.Cells.Add(c1);
                    rw.Cells.Add(c2);
                    rw.Cells.Add(c3);

                    rw.Cells.Add(new DataGridViewTextBoxCell() { Value = i.ToStringInvariant() });
                }
                else
                {
                    DataGridViewProgressCell p1 = new DataGridViewProgressCell();
                    p1.Value = i / 2;
                    p1.BarForeColor = Color.Orange;
                    p1.Style.ForeColor = Color.Red;
                    p1.BarColorScaling = 0.8f;
                    p1.TextToRightPreferentially = true;
                    p1.BarHeightPercentage = 25;
                    rw.Cells.Add(p1);

                    DataGridViewProgressCell p2 = new DataGridViewProgressCell();
                    p2.Value = 100 - i / 2;
                    p2.BarHeightPercentage = 100 - i / 2;
                    rw.Cells.Add(p2);

                    DataGridViewTextImageCell c3 = new DataGridViewTextImageCell();
                    c3.Value = "TextImage" + i;
                    c3.Style.ForeColor = Color.Red;
                    c3.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    rw.Cells.Add(c3);

                    DataGridViewPictureBoxCell p3 = new DataGridViewPictureBoxCell();
                    p3.PictureBox.AddTextAutoSize(new Point(0, 0), new Size(1000, 1000), "Text1", Font, Color.Red, Color.Blue, 1.0f);
                    p3.PictureBox.AddTextAutoSize(new Point(0, 70), new Size(1000, 1000), "Text2", Font, Color.Red, Color.Blue, 1.0f);
                    p3.PictureBox.AddImage(new Rectangle(0, 20, 96, 96), Properties.Resources.CaptainsLog);
                    p3.PictureBox.Render();
                    rw.Cells.Add(p3);

                    if (i == 0)
                    {
                        rw.MinimumHeight = 120;
                        p3.Alignment = ContentAlignment.TopCenter;
                    }
                }

                dataGridView.Rows.Add(rw);
            }

            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            tme.Interval = 1000;
            tme.Tick += Tme_Tick;
            tme.Start();
        }


        int tickc = 0;
        private void Tme_Tick(object sender, EventArgs e)
        {
            DataGridViewPictureBoxCell p3 = dataGridView.Rows[0].Cells[3] as DataGridViewPictureBoxCell;
            p3.PictureBox.ClearImageList();
            p3.PictureBox.AddTextAutoSize(new Point(0, 0), new Size(1000, 1000), "Count" + tickc, Font, Color.Red, Color.Blue, 1.0f);
            p3.PictureBox.AddTextAutoSize(new Point(0, 70), new Size(1000, 1000), "Text2", Font, Color.Red, Color.Blue, 1.0f);
            p3.PictureBox.AddImage(new Rectangle(0, 20, 32, 32), Properties.Resources.Calendar);
            p3.PictureBox.Render();
            dataGridView.InvalidateCell(p3);

            tickc++;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Location = new Point(100, 10);
        }


        private void dataGridView1_Resize(object sender, EventArgs e)
        {
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 0)
            {
                int tl = (int)dataGridView.Rows[e.RowIndex1].Tag;
                int tr = (int)dataGridView.Rows[e.RowIndex2].Tag;
                e.SortResult = tl.CompareTo(tr);
                e.Handled = true;
            }
            else
                e.SortDataGridViewColumnNumeric();
        }

        private void buttonClearRows_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        void HoverOverCell(DataGridViewCell cell, Rectangle area, Point screenpos)
        {
            ulong curtime = (ulong)Environment.TickCount;
            if (cell.ColumnIndex == 3 && (lastpopoutcelltime - curtime > 500 || lastpopoutcell != cell))
            {
                if (popupform != null)
                    popupform.Close();

                popupform = new PopUpForm(screenpos,new Size(500,500),2000);

                var imgctrl = new ImageControl();
                imgctrl.SetDrawImage(Properties.Resources.CaptainsLog, new Rectangle(0, 0, 300,300));
                imgctrl.Bounds = new Rectangle(50,50,300,300);
                popupform.Controls.Add(imgctrl);

                popupform.Show(this);
                lastpopoutcell = cell;
            }
        }


        PopUpForm popupform = null;
        DataGridViewCell lastpopoutcell = null;
        ulong lastpopoutcelltime = 0;

        private void dataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

  
}
