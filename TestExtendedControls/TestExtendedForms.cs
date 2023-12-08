using AudioExtensions;
using BaseUtils;
using ExtendedAudioForms;
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
    public partial class TestExtendedForms : Form
    {
        ThemeList theme;

        public TestExtendedForms()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.FontSize = 12;

            BaseUtils.Translator.Instance.AddExcludedControls(new Type[]
            {   typeof(ExtendedControls.ExtComboBox), typeof(ExtendedControls.NumberBoxDouble),typeof(ExtendedControls.NumberBoxFloat),typeof(ExtendedControls.NumberBoxLong),
                typeof(ExtendedControls.ExtScrollBar),typeof(ExtendedControls.ExtStatusStrip),typeof(ExtendedControls.ExtRichTextBox),typeof(ExtendedControls.ExtTextBox),
                typeof(ExtendedControls.ExtTextBoxAutoComplete),typeof(ExtendedControls.ExtDateTimePicker),typeof(ExtendedControls.ExtNumericUpDown) });

        }


        // icon list box

        private void extButton1_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            AudioDriverCSCore ad = new AudioDriverCSCore();
            AudioDeviceConfigure f = new AudioDeviceConfigure();
            f.Init(ad);
            f.ShowDialog(this);
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 20;
            AudioDriverCSCore ad = new AudioDriverCSCore();
            AudioDeviceConfigure f = new AudioDeviceConfigure();
            f.Init(ad);
            f.ShowDialog(this);
        }

        private void extButton4_Click(object sender, EventArgs e)
        {
            SC(12,true);
        }

        private void SC(float size, bool mode)
        { 
            Theme.Current.FontSize = size;
            AudioDriverCSCore ad = new AudioDriverCSCore();
            AudioQueue q = new AudioQueue(ad);
            WindowsSpeechEngine wse = new WindowsSpeechEngine();
            SpeechSynthesizer ss = new SpeechSynthesizer(wse);

            SpeechConfigure c = new SpeechConfigure();

            Variables ef = new Variables();

            c.Init(false,q, ss, "Caption title", this.Icon, mode ? "Text to do" : null,
                    true, true, AudioQueue.Priority.High,
                    "sn", "en", "Sheila", "100", "Default", ef);

            c.ShowDialog(this);

        }

        private void extButton5_Click(object sender, EventArgs e)
        {
            SC(20,false);
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            Theme.Current.WindowsFrame = true;
        }

        private void extButton6_Click(object sender, EventArgs e)
        {
            WC(12);

        }

        private void WC(float size)
        {
            Theme.Current.FontSize = size;
            AudioDriverCSCore ad = new AudioDriverCSCore();
            AudioQueue q = new AudioQueue(ad);

            WaveConfigureDialog c = new WaveConfigureDialog();

            Variables ef = new Variables();

            c.Init(false, q, "Caption title", this.Icon,
                    @"c:\",
                    true, AudioQueue.Priority.High,
                    "sn", "en", "100", ef);

            c.ShowDialog(this);

        }

        private void extButton7_Click(object sender, EventArgs e)
        {
            WC(20);
        }

        private void extButton8_Click(object sender, EventArgs e)
        {
            VAR(12,true);
        }

        private void VAR(float s, bool pop, bool ops = false)
        {
            Theme.Current.FontSize = s;
            VariablesForm f = new VariablesForm();

            Variables v = new Variables();
            if (pop)
            {
                v["Fred"] = "F1";
                v["George"] = "F1";
            }
            f.Init("Var test", this.Icon, v, showrunatrefreshcheckbox:true, allowadd:ops, allownoexpand:ops);
            f.ShowDialog();
        }

        private void extButton9_Click(object sender, EventArgs e)
        {
            VAR(20, true);

        }

        private void extButton10_Click(object sender, EventArgs e)
        {
            VAR(20, false);
        }

        private void extButton11_Click(object sender, EventArgs e)
        {
            VAR(24, true, true);

        }

        private void CForm(float size, bool filter)
        {
            Theme.Current.FontSize = size;
            ConditionFilterForm f = new ConditionFilterForm();

            if ( filter)
            {
                List<string> events = new List<string>() { "e1", "e2", "e3" };
                ConditionLists cl = new ConditionLists();
                cl.Add(new Condition("e1", "func1", new Variables() { ["one"] = "one" },
                        new List<ConditionEntry>
                        {
                                new ConditionEntry("IsPlanet",ConditionEntry.MatchType.IsTrue,""),      // both passes
                                new ConditionEntry("IsSmall",ConditionEntry.MatchType.IsFalse,""),
                        },
                        Condition.LogicalCondition.And,    // inner
                        Condition.LogicalCondition.Or
                  ));

                f.InitFilter("Filter", this.Icon, events, cl);
            }
            else
            {
                ConditionLists cl = new ConditionLists();
                cl.Add(new Condition("e", "f", new Variables(),
                        new List<ConditionEntry>
                        {
                                new ConditionEntry("IsPlanet",ConditionEntry.MatchType.IsTrue,""),      // both passes
                                new ConditionEntry("IsSmall",ConditionEntry.MatchType.IsFalse,""),
                        },
                        Condition.LogicalCondition.And,    // inner
                        Condition.LogicalCondition.Or
                    ));

                cl.Add(new Condition("e", "f", new Variables(),
                        new List<ConditionEntry>
                        {
                                new ConditionEntry("Other[Iter1].outerrad-Outer[Iter1].innerrad",ConditionEntry.MatchType.NumericGreaterEqual,"400"),        // does pass on Other[2]
                        },
                        Condition.LogicalCondition.Or,
                        Condition.LogicalCondition.And
                    ));

                List<TypeHelpers.PropertyNameInfo> vars = new List<TypeHelpers.PropertyNameInfo>();
                vars.Add(new TypeHelpers.PropertyNameInfo("Pone", "Para one wkwkw wkwkw long explanation of text here to a very long length to see what the darn this does and does again and again and again until the very end" +
                    " here to a very long length to see what the darn this does and does again and again and again until the very end" +
                    " here to a very long length to see what the darn this does and does again and again and again until the very end" +
                    " here to a very long length to see what the darn this does and does again and again and again until the very end" 
                    , ConditionEntry.MatchType.NumericEquals));
                vars.Add(new TypeHelpers.PropertyNameInfo("Ptwo", "Para two", ConditionEntry.MatchType.NumericEquals));
                vars.Add(new TypeHelpers.PropertyNameInfo("Pthree", "Para three", ConditionEntry.MatchType.NumericEquals));
                vars.Add(new TypeHelpers.PropertyNameInfo("Sone", "Para one", ConditionEntry.MatchType.Equals));
                vars.Add(new TypeHelpers.PropertyNameInfo("Stwo", "Para two", ConditionEntry.MatchType.Equals));

               f.AutoCompleteOnMatch = true;
                f.VariableNames = vars;
                f.InitCondition("Condition", this.Icon, cl);
            }

            f.ShowDialog();
        }

        private void extButton12_Click(object sender, EventArgs e)
        {
            CForm(8, false);

        }

        private void extButton13_Click(object sender, EventArgs e)
        {
            CForm(8, true);

        }
    }
}
