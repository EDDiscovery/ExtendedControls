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
            //theme.SetThemeByName("Elite EuroCaps");
            theme.SetThemeByName("EDSM");
            Theme.Current.FontSize = 12;

           // extButton1_Click(null, null);
        }


        // icon list box

        private void extButton1_Click(object sender, EventArgs e)
        {
            ExtendedControls.ConfigurableForm f = new ExtendedControls.ConfigurableForm();

            int width = 400;

            var butl = new ExtendedControls.ExtButton();
            butl.Image = Properties.Resources.LeftArrow;
            f.Add(new ExtendedControls.ConfigurableForm.Entry(butl, "left", "", new Point(20, 64), new Size(32, 32), null));
            var butr = new ExtendedControls.ExtButton();
            butr.Image = Properties.Resources.RightArrow;
            f.Add(new ExtendedControls.ConfigurableForm.Entry(butr, "right", "", new Point(width - 20 - 32, 64), new Size(32, 32), null));

            f.Add(new ExtendedControls.ConfigurableForm.Entry("olabel", typeof(Label), "Offer", new Point(20, 30), new Size(width - 40, 20), null, 1.5f, ContentAlignment.MiddleCenter));

            f.Add(new ExtendedControls.ConfigurableForm.Entry("offer", typeof(Label), "0/0", new Point(width / 2 - 12, 50), new Size(width / 2 - 20, 20), null, 1.2f, ContentAlignment.MiddleLeft));

            var bar = new PictureBox();
            bar.SizeMode = PictureBoxSizeMode.StretchImage;
            bar.Image = Properties.Resources.TraderBar;
            f.Add(new ExtendedControls.ConfigurableForm.Entry(bar, "bar", "", new Point(width / 2 - 32, 70), new Size(64, 16), null));

            f.Add(new ExtendedControls.ConfigurableForm.Entry("receive", typeof(Label), "0", new Point(width / 2 - 12, 90), new Size(width / 2 - 20, 20), null, 1.2f, ContentAlignment.MiddleLeft));

            f.Add(new ExtendedControls.ConfigurableForm.Entry("rlabel", typeof(Label), "Receive", new Point(20, 110), new Size(width - 40, 20), null, 1.5f, ContentAlignment.MiddleCenter));

            var panelbox = new GroupBox() { ForeColor = Color.Red };
            f.Add(new ExtendedControls.ConfigurableForm.Entry(panelbox, "panel", "g1", new Point(10, 150), new Size(width - 10, 100), "") { anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left });

            f.AddOK(new Point(width - 100, 270), anchor: AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);
            f.AddCancel(new Point(20, 270), anchor: AnchorStyles.Right | AnchorStyles.Bottom);

            f.Trigger += (a, b, c) => { System.Diagnostics.Debug.WriteLine("Ret " + b); if ( b == "OK" || b == "Close" ) f.ReturnResult(DialogResult.OK); };

            f.RightMargin = 20;
            f.AllowResize = true;

            Theme.Current.FontSize = 12;

            f.ShowDialogCentred(this, this.Icon, "Trader",closeicon:true, minsize: new Size(800, 500), maxsize:new Size(1000,600));

        }

        
        void CF(float s, bool wf)
        {
            Theme.Current.FontSize = s;
            Theme.Current.WindowsFrame = wf;

            ExtendedControls.ConfigurableForm f = new ExtendedControls.ConfigurableForm();

            int width = 430;
            int ctrlleft = 150;

            int initialvalue = 200;
            Form parent = this;

            f.Add(new ExtendedControls.ConfigurableForm.Entry("L", typeof(Label), "Jump to:", new Point(10, 40), new Size(140, 24), ""));
            f.Add(new ExtendedControls.ConfigurableForm.Entry("Entry", typeof(ExtendedControls.NumberBoxLong), initialvalue.ToString(), new Point(ctrlleft, 40), new Size(width - ctrlleft - 20, 24), "Enter number to jump to or near to") { numberboxdoubleminimum = 0, numberboxformat = "0" });

            f.Add(new ExtendedControls.ConfigurableForm.Entry("OK", typeof(ExtendedControls.ExtButton), "OK", new Point(width - 100, 70), new Size(80, 24), "Press to Accept"));
            f.Add(new ExtendedControls.ConfigurableForm.Entry("Cancel", typeof(ExtendedControls.ExtButton), "Cancel", new Point(width - 200, 70), new Size(80, 24), "Press to Cancel"));

            f.Trigger += (dialogname, controlname, tag) =>
            {
                if (controlname == "OK" || controlname == "Entry:Return")
                {
                    long? v3 = f.GetLong("Entry");
                    if (v3.HasValue)
                    {
                        f.ReturnResult(DialogResult.OK);
                    }
                    else
                        ExtendedControls.MessageBoxTheme.Show(parent, "Value is not valid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (controlname == "Cancel" || controlname == "Close" )
                {
                    f.ReturnResult(DialogResult.Cancel);
                }
            };

            //f.AllowResize = true;
            DialogResult res = f.ShowDialogCentred(parent, parent.Icon, "Jump to Entry", closeicon:true);

            if (res == DialogResult.OK)
            {
                int target = (int)f.GetLong("Entry").Value;

            }
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            CF(20,false);
        }

        private void extButton4_Click(object sender, EventArgs e)
        {
            CF(30,false);
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            CF(30,true);

        }

        private void extButton5_Click(object sender, EventArgs e)
        {
            CF(8, true);
        }

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
            f.Init(null, true, " ", "", "EliteDangerous64", -1, false,new List<string> { "AddOne" },addp);
            f.Show(this);
        }

        private void extButton11_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 30;
            KeyForm f = new KeyForm();
            f.Init(null, true, " ", "", "", -1, false);
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

        private void extButton19_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 8;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three" },
                                new string[] { "done", "dtwo", "dthree" },
                                true,
                                new string[] { "t1", "t2", "t3" }, false, 400, 200);


        }

        private void extButton21_Click(object sender, EventArgs e)
        {
            ConfigurableForm cfg = new ExtendedControls.ConfigurableForm();
            cfg.AllowSpaceForScrollBar = false;
            cfg.RightMargin = cfg.BottomMargin = 0;
            cfg.ForceNoWindowsBorder = true;
            cfg.AllowSpaceForCloseButton = true;
            cfg.BorderMargin = 0;

            ExtButton wikibutton = new ExtButton();
            wikibutton.Image = Properties.Resources.CursorToTop;
            cfg.Add(new ConfigurableForm.Entry(wikibutton, "Wiki", null, new Point(0, 0), new Size(24, 24), null));

            ExtButton videobutton = new ExtButton();
            videobutton.Image = Properties.Resources.CursorToTop;
            cfg.Add(new ConfigurableForm.Entry(videobutton, "Video", null, new Point(24, 0), new Size(24, 24), null));

            cfg.Trigger += (string logicalname, string ctrlname, object callertag) =>
            {
                if (ctrlname == "Close")
                    cfg.ReturnResult(DialogResult.Cancel);
                else if (ctrlname == "Wiki")
                    cfg.ReturnResult(DialogResult.OK);
                else if (ctrlname == "Video")
                    cfg.ReturnResult(DialogResult.Yes);
            };

            Theme.Current.WindowsFrame = false;
            Theme.Current.FontSize = sender is float ? (float)sender : 8.5f;

            DialogResult res = cfg.ShowDialog(this, new Point(500,500), this.Icon, "", closeicon: true);
        }

        private void extButton22_Click(object sender, EventArgs e)
        {
            extButton21_Click(12.0f, e);
        }

        private void extButton23_Click(object sender, EventArgs e)
        {
            extButton21_Click(16.0f, e);

        }

        private void extButton24_Click(object sender, EventArgs e)
        {
            extButton21_Click(10.0f, e);

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
            MessageBoxTheme.Show("A", "A", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
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

