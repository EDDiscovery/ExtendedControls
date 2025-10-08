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
    public partial class TestDrakeDockingPads : Form
    {
        public TestDrakeDockingPads()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            fleetCarrierDockingPads1.SelectedIndex = 13;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fleetCarrierDockingPads1.SelectedIndex = 1;

        }

        private void button0_Click(object sender, EventArgs e)
        {
            fleetCarrierDockingPads1.SelectedIndex = 0;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            fleetCarrierDockingPads1.SelectedIndex = 11;

        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            fleetCarrierDockingPads1.SelectedIndex = (fleetCarrierDockingPads1.SelectedIndex + 1) % 33;
        }

        private void buttonFlip_Click(object sender, EventArgs e)
        {
            fleetCarrierDockingPads1.SetOrientation(!fleetCarrierDockingPads1.IsVertical);
        }
    }
}
