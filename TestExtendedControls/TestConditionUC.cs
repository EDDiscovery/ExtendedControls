using ExtendedConditionsForms;
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
    public partial class TestConditionUC: Form
    {
        public TestConditionUC()
        {
            InitializeComponent();
        }

        private void buttonEvents(object sender, EventArgs e)
        {
            ConditionFilterForm frm = new ConditionFilterForm();
            List<string> events = new List<string>() { "eone", "etwo" };
            List<string> varfields = new List<string>() { "vone", "vtwo" };

            frm.InitFilter("Name", this.Icon, events, null, varfields);
            frm.ShowDialog();
        }

        private void buttonCondition(object sender, EventArgs e)
        {
            ConditionFilterForm frm = new ConditionFilterForm();
            List<string> varfields = new List<string>() { "vone", "vtwo" };

            frm.InitCondition("Name", this.Icon, varfields);
            frm.ShowDialog();

        }
    }
}
