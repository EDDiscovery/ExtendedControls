using BaseUtils;
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

        ConditionLists eventscond;

        private void buttonEvents(object sender, EventArgs e)
        {
            ConditionFilterForm frm = new ConditionFilterForm();
            List<string> events = new List<string>() { "eone", "etwo" };
            List<string> varfields = new List<string>() { "vone", "vtwo" };

            frm.InitFilter("Name", this.Icon, events, null, varfields, eventscond, (ev, txt) =>
            {
                return "Help on " + ev + ":" + txt;
            });

            if (frm.ShowDialog() == DialogResult.OK)
                eventscond = frm.Result;
        }

        Condition conds;
        private void buttonCondition(object sender, EventArgs e)
        {
            ConditionFilterForm frm = new ConditionFilterForm();
            List<string> varfields = new List<string>() { "vone", "vtwo" , "xone", "xtwo" };
            List<string> varfieldshelp = new List<string>() { "Help for vone\nThis is it", "Help for vtwo\nThis is it", "Help for xone" , "helo for xtwo" };

            frm.InitCondition("Name", this.Icon, varfields, conds, (ev, txt) => 
                    {
                        int indexof = varfields.IndexOf(txt);
                        return indexof >= 0 ? varfieldshelp[indexof] : null;
                    });

            if (frm.ShowDialog() == DialogResult.OK)
            {
                var list = frm.Result;
                if (list.Count == 1)
                    conds = list.Get(0);
            }
        }

        ConditionLists clist2;

        private void buttonExt3_Click(object sender, EventArgs e)
        {
            ConditionFilterForm frm = new ConditionFilterForm();
            List<string> varfields = new List<string>() { "vone", "vtwo" };

            frm.InitCondition("Name", this.Icon, varfields, clist2);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                clist2 = frm.Result;
            }

        }
    }
}
