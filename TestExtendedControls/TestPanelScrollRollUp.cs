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
    public partial class TestPanelScrollRollUp : Form
    {
        public TestPanelScrollRollUp()
        {
            InitializeComponent();
            for( int i = 0; i < 100; i++ )
            {
                DataGridViewRow row = dataGridView1.RowTemplate.Clone() as DataGridViewRow;
                row.CreateCells(dataGridView1,i.ToString(),"2","3");
                dataGridView1.Rows.Add(row);
            }

            extPanelDataGridViewScrollRollUpButtons1.Add(5, 10);
            extPanelDataGridViewScrollRollUpButtons1.Add(50, 70);

          }

    }
}
