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
            Outlining1.Add(5, 22);
            Outlining1.Add(6, 8);
            System.Diagnostics.Debug.Assert(Outlining1.Add(6, 8)==false); // should fail
            Outlining1.Add(8, 12);
            Outlining1.Add(13, 20);
            Outlining1.Add(50, 70);
            Outlining1.Add(40, 80);


            BaseUtils.IntRangeList rl = new BaseUtils.IntRangeList();
            rl.Add(10, 20);
            rl.Add(40, 60);
            rl.Dump();
            rl.Add(20, 30);
            rl.Add(50, 60);     // no action, all inside 4-60
            rl.Add(61, 70);     // merge up
            rl.Dump();      // 10-30, 40-70
            rl.Add(35, 39);
            rl.Dump();      // 10-30, 35-70
            rl.Add(32, 33);
            rl.Sort();
            rl.Dump();      // 10-30, 32-33, 35-70
            rl.Remove(0, 10);
            rl.Dump();      // 11-30, 32-33, 35-70
            rl.Remove(15, 20);
            rl.Sort();
            rl.Dump();      // 11-14, 21-30, 32-33, 35-70
            rl.Remove(21, 30);
            rl.Dump();      // 11-14, 32-33, 35-70
            rl.Add(34, 34);
            rl.Dump();      // 11-14, 32-70
            rl.Remove(50, 60);
            rl.Dump();      // 11-14, 32-49 61-70
            rl.Remove(50, 60);
            rl.Dump();      // 11-14, 32-49 61-70
            rl.Remove(65, 75);
            rl.Dump();      // 11-14, 32-49 61-64
            rl.Remove(60, 62);
            rl.Dump();      // 11-14, 32-49 63-64
            rl.Remove(60, 63);
            rl.Dump();      // 11-14, 32-49 64-64
            rl.Remove(60, 64);
            rl.Dump();      // 11-14, 32-49 
            rl.Remove(32, 64);
            rl.Dump();      // 11-14
            rl.Add(32, 49);
            rl.Remove(30, 64);
            rl.Dump();      // 11-14
            rl.Add(32, 49);
            rl.Add(100, 200);
            rl.Remove(1, 300);
            rl.Dump();      // nothing
            rl.Add(32, 49);
            rl.Add(100, 200);
            rl.Remove(34, 300);
            rl.Dump();      // 32-33
            rl.Add(32, 49);
            rl.Add(60, 70);
            rl.Add(100, 200);
            rl.Remove(34, 150);
            rl.Dump();      // 32-33 151-200
            rl.Remove(32, 200);     // removes both start and end block, which are different
            rl.Dump();      // 0 
            rl.Add(32, 33);
            rl.Add(151, 200);
            rl.Remove(31, 201);        // 0 uses the tidy up routine at the end
            rl.Add(32, 33);
            rl.Add(151, 200);
            rl.Add(31, 201);            // subsume ranges..
            rl.Dump();      // 31-201

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
