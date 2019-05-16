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
            theme.FontSize = 12;
            theme.WindowsFrame = false;
            ExtendedControls.ThemeableFormsInstance.Instance = theme;

        }

        ConditionLists eventscond;

        private void buttonEvents(object sender, EventArgs e)
        {
            EV(12);
        }

        private void EV(float s)
        { 
            ConditionFilterForm frm = new ConditionFilterForm();
            frm.VariableNamesEvents += (evname) =>
            {
                List<BaseUtils.TypeHelpers.PropertyNameInfo> list = new List<BaseUtils.TypeHelpers.PropertyNameInfo>();
                list.Add(new BaseUtils.TypeHelpers.PropertyNameInfo("one:" + evname, "help on one"));
                list.Add(new BaseUtils.TypeHelpers.PropertyNameInfo("two:" + evname, "help on two"));
                return list;
            };

            frm.VariableNames = new List<BaseUtils.TypeHelpers.PropertyNameInfo>();
            frm.VariableNames.Add(new BaseUtils.TypeHelpers.PropertyNameInfo("defone", "String", ConditionEntry.MatchType.Contains));
            frm.VariableNames.Add(new BaseUtils.TypeHelpers.PropertyNameInfo("deftwo", "Number", ConditionEntry.MatchType.NumericEquals));
            frm.VariableNames.Add(new BaseUtils.TypeHelpers.PropertyNameInfo("defthree", "help!"));

            List<string> events = new List<string>() { "eone", "etwo" };

            theme.FontSize = s;
            frm.InitFilter("Name", this.Icon, events);

            if (frm.ShowDialog() == DialogResult.OK)
                eventscond = frm.Result;
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            EV(20);

        }

        private void buttonCondition(object sender, EventArgs e)
        {
            CD(12);
        }

        private void CD(float s)
        { 
            ConditionFilterForm frm = new ConditionFilterForm();

            List<ConditionEntry> ces = new List<ConditionEntry>() { new ConditionEntry("Fred", ConditionEntry.MatchType.NumericGreaterEqual, "20") };

            Condition conds = new Condition("", "", "", ces);

            frm.VariableNames = new List<BaseUtils.TypeHelpers.PropertyNameInfo>();
            frm.VariableNames.Add(new BaseUtils.TypeHelpers.PropertyNameInfo("defone", "String", ConditionEntry.MatchType.Contains));
            frm.VariableNames.Add(new BaseUtils.TypeHelpers.PropertyNameInfo("deftwo", "Number", ConditionEntry.MatchType.NumericEquals));

            theme.FontSize = s;
            frm.InitCondition("Name", this.Icon, conds);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                var list = frm.Result;
                if (list.Count == 1)
                    conds = list.Get(0);
            }
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            CD(20);

        }


        ConditionLists clist2;

        private void buttonExt3_Click(object sender, EventArgs e)
        {
            CL(12);
        }


        private void CL(float s)
        {
            ConditionFilterForm frm = new ConditionFilterForm();

            theme.FontSize = s;
            frm.InitCondition("Name", this.Icon, clist2);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                clist2 = frm.Result;
            }

        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            CL(20);

        }
    }
}
