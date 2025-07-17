using ExtendedControls;
using ExtendedForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static BaseUtils.EnhancedSendKeysParser;

namespace TestExtendedControls
{
    public partial class TestVariousForms : Form
    {
        ThemeList theme;

        public TestVariousForms()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("EDSM");
            Theme.Current.FontSize = 12;

           // extButton1_Click(null, null);
        }


        // icon list box

        private void extButton6_Click(object sender, EventArgs e)
        {
            INFO(16,12);
        }

        private void INFO(float s,float isf)
        { 
            InfoForm i = new InfoForm();
            string text = "Info text" + Environment.NewLine;
            for (int tl = 0; tl < 20; tl++)
                text += "Info line " + tl.ToString() + Environment.NewLine;

            Theme.Current.FontSize = s;
            i.Info("Example info form", this.Icon, text, new int[] { 0, 200, 300 }, isf);
            i.Show(this);
        }

        private void extButton7_Click(object sender, EventArgs e)
        {
            INFO(12,12);
        }

        private void extButton8_Click(object sender, EventArgs e)
        {
            INFO(20,12);
        }

        private void extButton9_Click(object sender, EventArgs e)
        {
            INFO(30,12);
        }

        class AddKeyParser : BaseUtils.EnhancedSendKeysParser.IAdditionalKeyParser
        {
            public Tuple<string, string> Parse(ref string s)
            {
                if (s.StartsWith("AddOne"))
                {
                    s = s.Substring(6);
                    return new Tuple<string, string>(s, null);
                }
                else
                    return null;
            }
        }

        private void extButton10_Click(object sender, EventArgs e)
        {
            AddKeyParser addp = new AddKeyParser();

            {
                Queue<SKEvent> events = new Queue<SKEvent>();
                string err = BaseUtils.EnhancedSendKeysParser.ParseKeys(events, "[1,2,3]#1Shift", 200, 201, 202, addp);
            }
            {
                Queue<SKEvent> events = new Queue<SKEvent>();
                string err = BaseUtils.EnhancedSendKeysParser.ParseKeys(events, "[1,2,3]#1Shift+Insert", 200, 201, 202, addp);
            }
            {
                Queue<SKEvent> events = new Queue<SKEvent>();
                string err = BaseUtils.EnhancedSendKeysParser.ParseKeys(events, "[1,2,3]#1Insert", 200, 201, 202, addp);
            }
            {
                Queue<SKEvent> events = new Queue<SKEvent>();
                string err = BaseUtils.EnhancedSendKeysParser.ParseKeys(events, "[1,2,3]#1^Insert", 200, 201, 202, addp);
            }
            {
                Queue<SKEvent> events = new Queue<SKEvent>();
                string err = BaseUtils.EnhancedSendKeysParser.ParseKeys(events, "[1,2,3]#1^Shift+Insert", 200, 201, 202, addp);
            }
            {
                Queue<SKEvent> events = new Queue<SKEvent>();
                string err = BaseUtils.EnhancedSendKeysParser.ParseKeys(events, "[1,2,3]#1^Shift", 200, 201, 202, addp);
            }
            //     err = BaseUtils.EnhancedSendKeysParser.ParseKeys(events, "[1,2,3]#Insert", 200, 201, 202, addp);

            KeyForm f = new KeyForm();
            f.Init(null, true, " ", "", "EliteDangerous64",  false,new List<string> { "AddOne" },addp);
            f.Show(this);
        }

        private void extButton11_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 30;
            KeyForm f = new KeyForm();
            f.Init(null, true, " ", "", "", false);
            f.Show(this);

        }

        string infotext = "Help me please\n2I need helpqkqwkqwkqw qwkqwkqw qwkqwkqw\n3qkwqkqw qwkqwjwhe wejhwehwehwe we wjkwjwj w wjh and so it continues over the line maybe\n4qkqkq wqjqwjqw ejwejwe  sdsd \n5 kqkqkwq \n\n7akakaka\n8L";


        private void extButton12_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            Theme.Current.WindowsFrame = false;
            MessageBoxTheme.Show(infotext, "Help", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

        }

        private void extButton13_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 20;
            Theme.Current.WindowsFrame = false;
            MessageBoxTheme.Show(infotext, "Help", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

        }

        private void extButton14_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 20;
            Theme.Current.WindowsFrame = false;
            MessageBoxTheme.Show(infotext, "Help", MessageBoxButtons.RetryCancel);
        }

        private void extButton15_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 20;
            Theme.Current.WindowsFrame = true;
            MessageBoxTheme.Show(infotext, "Help", MessageBoxButtons.RetryCancel,MessageBoxIcon.Asterisk);

        }

        private void extButton20_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            Theme.Current.WindowsFrame = false;
            MessageBoxTheme.Show(infotext, "Help", MessageBoxButtons.OKCancel, MessageBoxIcon.None);
        }

        private void extButton16_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            Theme.Current.WindowsFrame = true;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three" },
                                new string[] { "done", "dtwo", "dthree" },
                                false,
                                new string[] { "t1", "t2", "t3" },
                                true);
        }

        private void extButton17_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 20;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three" },
                                new string[] { "done", "dtwo", "dthree" },
                                true,
                                new string[] { "t1", "t2", "t3" });

        }

        private void extButton18_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 24;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three" },
                                new string[] { "done", "dtwo", "dthree" },
                                true,
                                new string[] { "t1", "t2", "t3" }, false, 400, 100);

        }

        private void extButton42_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" },
                                new string[] { "done", "dtwo", "dthree", "dfour", "dfive", "dsix", "dseven", "deight", "dnine", "dten" },
                                true,
                                new string[] { "t1", "t2", "t3" }, false, 400, 100, -1);


        }
        private void extButton43_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 16;
            Theme.Current.WindowsFrame = false;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" },
                                new string[] { "done", "dtwo", "dthree", "dfour", "dfive", "dsix", "dseven", "deight", "dnine", "dten" },
                                new int[] {32,32,80,32,80, 120,50,40,32,32},
                                new string[] { "t1", "t2", "t3" }, 
                                true,false, 400, -1, 8);

        }


        private void extButton19_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 8;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three" },
                                new string[] { "done", "dtwo", "dthree" },
                                true,
                                new string[] { "t1", "t2", "t3" }, false, 400, 200);


        }

        private void extButton25_Click(object sender, EventArgs e)
        {
            Theme.Current.WindowsFrame = false;
            MessageBoxTheme.Show("Test text", "wkwkwk");
        }

        private void extButton26_Click(object sender, EventArgs e)
        {
            Theme.Current.WindowsFrame = false;
            MessageBoxTheme.Show("Test text\r\nwith a line", "wkwkwk");
   
        }

        private void extButton27_Click(object sender, EventArgs e)
        {
            Theme.Current.WindowsFrame = false;
            MessageBoxTheme.Show("Test text qwkqkqw qwkqwkqw qwkqwkqw qwkqwkqw wjkqwkqwkqw qwkqwkqw qwkqwkqw  qwkqk \r\nSTART qkqwkqwkqw qwkqwkqwqw qwkqwkqw end", "kw wkwkw wkwkw wkwk wank", MessageBoxButtons.OKCancel);
        }

        private void extButton28_Click(object sender, EventArgs e)
        {
            Theme.Current.WindowsFrame = false;
            MessageBoxTheme.Show("Test text", "Very long title wkwkwk wkwkwkw wkwkw wkwkw wkwkw wkwkw wkwkw wkwk wank", MessageBoxButtons.OKCancel);
        }

        private void extButton29_Click(object sender, EventArgs e)
        {
            Theme.Current.WindowsFrame = false;
            MessageBoxTheme.Show("Test text qwkqkqw qwkqwkqw qwkqwkqw qwkqwkqw wjkqwkqwkqw qwkqwkqw qwkqwkqw  qwkqk \r\nSTART qkqwkqwkqw qwkqwkqwqw qwkqwkqw end", "kw wkwkw wkwkw wkwk wank", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
        }

        private void extButton30_Click(object sender, EventArgs e)
        {
            Theme.Current.WindowsFrame = false;
            Theme.Current.FontName = "Verdana";

            for (int fontsize = 8; fontsize < 20; fontsize++)
            {
                Theme.Current.FontSize = fontsize;
                MessageBoxTheme.Show("No new Release Found", "Warning", MessageBoxButtons.OK);
            }
        }

        private void extButton31_Click(object sender, EventArgs e)
        {
            Theme.Current.WindowsFrame = false;
            MessageBoxTheme.Show("A", "A", MessageBoxButtons.OKCancel);
        }

        private void extButton32_Click(object sender, EventArgs e)
        {
            Theme.Current.WindowsFrame = true;
            MessageBoxTheme.Show("A", "A", MessageBoxButtons.OK, MessageBoxIcon.Hand);

        }


    }
}

