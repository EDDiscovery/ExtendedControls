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
    public partial class TestDockingPads : Form
    {
        public TestDockingPads()
        {
            InitializeComponent();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            dockingPads1.SelectedIndex = 13;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dockingPads1.SelectedIndex = 1;

        }

        private void button0_Click(object sender, EventArgs e)
        {
            dockingPads1.SelectedIndex = 0;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            dockingPads1.SelectedIndex = 11;

        }
    }
}
