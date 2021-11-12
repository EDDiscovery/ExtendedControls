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
    public partial class TestPanelDGV : Form
    {
        ThemeStandard theme;

        public TestPanelDGV()
        {
            InitializeComponent();

            theme = new ThemeStandard();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            theme.WindowsFrame = true;


            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            dataGridView.RowHeightChanged += (a,e) => { System.Diagnostics.Debug.WriteLine("DGV Row Height"); };

            dataGridView.Dock = DockStyle.Fill;

            for ( int i = 0; i < 100; i++ )
            {
                DataGridViewRow row = dataGridView.RowTemplate.Clone() as DataGridViewRow;
                row.CreateCells(dataGridView, i.ToString(), (100+i).ToString(), (10000-i).ToString());
                row.Tag = i;
                dataGridView.Rows.Add(row);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Location = new Point(100, 10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int r = 5; r < 10; r++)
                dataGridView.Rows[r].Cells[0].Value= "";       // col 0 sort by tag, knock some out and show it still sorts right
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = "";
            for (int i = 0; i < 10; i++)
                s += $"Row {i}\r\n";
            dataGridView.Rows[5].Cells[1].Value = s;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            dataGridView.Rows[5].Cells[1].Value = "Back";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Col " + e.Column + " Resize" );
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
    }
}
