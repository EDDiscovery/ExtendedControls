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
        ThemeStandard theme;

        public TestConditionUC()
        {
            InitializeComponent();
            theme = new ThemeStandard();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            theme.WindowsFrame = true;
            ExtendedControls.ThemeableFormsInstance.Instance = theme;
        }

        ConditionLists eventscond;

        private void buttonEvents(object sender, EventArgs e)
        {
            ConditionFilterForm frm = new ConditionFilterForm();
            frm.VariableNamesEvents += (evname) => 
                {
                    List<ConditionFilterUC.VariableName> list = new List<ConditionFilterUC.VariableName>();
                    list.Add(new ConditionFilterUC.VariableName("one:" + evname, "help on one"));
                    list.Add(new ConditionFilterUC.VariableName("two:" + evname, "help on two"));
                    return list;
                };

            frm.VariableNames = new List<ConditionFilterUC.VariableName>();
            frm.VariableNames.Add(new ConditionFilterUC.VariableName("defone", "String", ConditionEntry.MatchType.Contains));
            frm.VariableNames.Add(new ConditionFilterUC.VariableName("deftwo", "Number", ConditionEntry.MatchType.NumericEquals));
            frm.VariableNames.Add(new ConditionFilterUC.VariableName("defthree", "help!"));

            List<string> events = new List<string>() { "eone", "etwo" };

            frm.InitFilter("Name", this.Icon, events);

            theme.ApplyToForm(frm);

            if (frm.ShowDialog() == DialogResult.OK)
                eventscond = frm.Result;
        }

        private void buttonCondition(object sender, EventArgs e)
        {
            ConditionFilterForm frm = new ConditionFilterForm();

            List<ConditionEntry> ces = new List<ConditionEntry>() { new ConditionEntry("Fred", ConditionEntry.MatchType.NumericGreaterEqual, "20") };

            Condition conds = new Condition("", "", "", ces);

            frm.VariableNames = new List<ConditionFilterUC.VariableName>();
            frm.VariableNames.Add(new ConditionFilterUC.VariableName("defone", "String", ConditionEntry.MatchType.Contains));
            frm.VariableNames.Add(new ConditionFilterUC.VariableName("deftwo", "Number", ConditionEntry.MatchType.NumericEquals));

            frm.InitCondition("Name", this.Icon, conds);

            theme.ApplyToForm(frm);
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

            frm.InitCondition("Name", this.Icon, clist2);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                clist2 = frm.Result;
            }

        }
    }
}
