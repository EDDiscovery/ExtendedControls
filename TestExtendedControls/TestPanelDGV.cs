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

namespace DialogTest
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

            for ( int i = 0; i < 100; i++ )
            {
                DataGridViewRow row = dataGridView1.RowTemplate.Clone() as DataGridViewRow;
                row.CreateCells(dataGridView1, i.ToString(), "2", "3");
                dataGridView1.Rows.Add(row);
            }

            System.Diagnostics.Debug.WriteLine("HW {0} ", dataGridView1.RowHeadersWidth);
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                System.Diagnostics.Debug.WriteLine("Col {0} w {1} ", i, dataGridView1.Columns[i].Width);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] prevsizes = new int[] { 100, 200, 300 };

            int totalsizes = 0;
            foreach (var x in prevsizes)
                totalsizes += x;

            dataGridView1.SuspendLayout();
            dataGridView1.RowHeadersWidth = 50;

            for (int i = 0; i < prevsizes.Length; i++)
            {
                float f = 100f * (float)prevsizes[i] / totalsizes;
                dataGridView1.Columns[i].FillWeight = f;
            }
            dataGridView1.ResumeLayout();

            System.Diagnostics.Debug.WriteLine("HW {0} ", dataGridView1.RowHeadersWidth);
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                System.Diagnostics.Debug.WriteLine("Col {0} w {1} ", i, dataGridView1.Columns[i].Width);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.SuspendLayout();
            dataGridView1.RowHeadersWidth = 75;
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 400;
            dataGridView1.Columns[2].Width = dataGridView1.Width - dataGridView1.RowHeadersWidth - 200 - 400;

            System.Diagnostics.Debug.WriteLine("HW {0} ", dataGridView1.RowHeadersWidth);
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                System.Diagnostics.Debug.WriteLine("Col {0} w {1} ", i, dataGridView1.Columns[i].Width);

            dataGridView1.ResumeLayout();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            RescaleColumns(dataGridView1);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("HW {0} ", dataGridView1.RowHeadersWidth);
            int wset = dataGridView1.RowHeadersWidth;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                wset += dataGridView1.Columns[i].Width;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("Col {0} w {1} {2}", i, dataGridView1.Columns[i].Width, (float)dataGridView1.Columns[i].Width/wset);
            }

            System.Diagnostics.Debug.WriteLine("{0} vs {1}", dataGridView1.Width, wset);
        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
           // RescaleColumns2(dataGridView1);
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

        // experimental functions ..

        private void RescaleColumns(DataGridView v)
        {
            int colwidth = v.RowHeadersWidth;
            for (int i = 0; i < v.ColumnCount; i++)
                colwidth += v.Columns[i].Width;
            int curwidth = v.Width;
            System.Diagnostics.Debug.WriteLine("{0} vs {1}", colwidth, curwidth);
            float scalingfactor = (float)curwidth / (float)colwidth;

            v.RowHeadersWidth = Math.Max(10, (int)(v.RowHeadersWidth * scalingfactor));
            int wset = v.RowHeadersWidth;

            for (int i = 0; i < v.ColumnCount - 1; i++)
            {
                v.Columns[i].Width = (int)(v.Columns[i].Width * scalingfactor);
                wset += v.Columns[i].Width;
            }

            v.Columns[v.ColumnCount - 1].Width = curwidth - wset;

            System.Diagnostics.Debug.WriteLine("HW {0} ", dataGridView1.RowHeadersWidth);
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                System.Diagnostics.Debug.WriteLine("Col {0} w {1} ", i, dataGridView1.Columns[i].Width);
        }

        private void RescaleColumns2(DataGridView v)
        {
            int widthleft = v.Width;

            for (int i = 0; i < v.ColumnCount; i++)
            {
                v.Columns[i].Width = (int)(v.Width * (float)v.Columns[i].FillWeight / 100.0f);
                widthleft -= v.Columns[i].Width;
            }

            v.RowHeadersWidth = Math.Max(widthleft, 10);

            System.Diagnostics.Debug.WriteLine("HW {0} ", dataGridView1.RowHeadersWidth);
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                System.Diagnostics.Debug.WriteLine("Col {0} w {1} ", i, dataGridView1.Columns[i].Width);
        }


    }
}
