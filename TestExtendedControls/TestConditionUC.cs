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

namespace TestExtendedControls
{
    public partial class TestConditionUC: Form
    {
        ThemeLoader theme;

        public TestConditionUC()
        {
            InitializeComponent();
            theme = new ThemeLoader();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.FontSize = 12;
            Theme.Current.WindowsFrame = false;
            ExtendedControls.Theme.Current = theme;

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

            Theme.Current.FontSize = s;
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

            List<ConditionEntry> ces = new List<ConditionEntry>()
            {
                new ConditionEntry("Fred", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred2", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred3", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred4", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred5", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred6", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred7", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred8", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred9", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred10", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred11", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred12", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred13", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred14", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred15", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred16", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred17", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred18", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred19", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred20", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred21", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred22", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred23", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred24", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred25", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred26", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred27", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred28", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred29", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                new ConditionEntry("Fred30", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
            };

            Condition conds = new Condition("", "", new Variables(), ces);

            frm.VariableNames = new List<BaseUtils.TypeHelpers.PropertyNameInfo>();
            frm.VariableNames.Add(new BaseUtils.TypeHelpers.PropertyNameInfo("defone", "String", ConditionEntry.MatchType.Contains));
            frm.VariableNames.Add(new BaseUtils.TypeHelpers.PropertyNameInfo("deftwo", "Number", ConditionEntry.MatchType.NumericEquals));

            Theme.Current.FontSize = s;
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


        ConditionLists clist2 = new ConditionLists();

        private void buttonExt3_Click(object sender, EventArgs e)
        {
            CL(12);
        }


        private void CL(float s)
        {
            ConditionFilterForm frm = new ConditionFilterForm();

            if ( clist2.Count == 0 )
            {
                List<ConditionEntry> ces = new List<ConditionEntry>()
                {
                    new ConditionEntry("Fred", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred2", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred3", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred4", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred5", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred6", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred7", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred8", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred9", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred10", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred11", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred12", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred13", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred14", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred15", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred16", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred17", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred18", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred19", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred20", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred21", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred22", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred23", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred24", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred25", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred26", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred27", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred28", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred29", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                    new ConditionEntry("Fred30", ConditionEntry.MatchType.NumericGreaterEqual, "20"),
                };

                clist2.Add(new Condition("e1", "a", new Variables(), ces, ConditionEntry.LogicalCondition.Or, ConditionEntry.LogicalCondition.And));
            }

            Theme.Current.FontSize = s;
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
