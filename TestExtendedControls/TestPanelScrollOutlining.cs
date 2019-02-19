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
    public partial class TestPanelScrollOutlining : Form
    {
        public TestPanelScrollOutlining()
        {
            InitializeComponent();
            for( int i = 0; i < 100; i++ )
            {
                DataGridViewRow row = dataGridView1.RowTemplate.Clone() as DataGridViewRow;
                row.CreateCells(dataGridView1, i.ToString(), "2", "3");
                dataGridView1.Rows.Add(row);

                DataGridViewRow row2 = dataGridView1.RowTemplate.Clone() as DataGridViewRow;
                row2.CreateCells(dataGridView1, "R2-" + i.ToString(), "2", "3");
                dataGridView2.Rows.Add(row2);
            }

            Outlining1.ForeColor = Color.Black;
            Outlining1.Add(5, 15);
            Outlining1.Add(6, 8);
            System.Diagnostics.Debug.Assert(Outlining1.Add(6, 8)==false); // should fail
            Outlining1.Add(8, 12);
            Outlining1.Add(50, 70);
            Outlining1.Add(40, 80);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Outlining1.Remove(6, 8);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 6; i < 8; i++)
                dataGridView2.Rows.RemoveAt(i);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(6);
        }
    }
}
