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
    public partial class TestVariousForms : Form
    {
        ThemeStandard theme;

        public TestVariousForms()
        {
            InitializeComponent();
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            //theme.SetThemeByName("Elite EuroCaps");
            theme.SetThemeByName("EDSM");
            theme.FontSize = 12;

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

            f.Add(new ExtendedControls.ConfigurableForm.Entry("olabel", typeof(Label), "Offer", new Point(20, 30), new Size(width-40, 20), null, 1.5f, ContentAlignment.MiddleCenter));

            f.Add(new ExtendedControls.ConfigurableForm.Entry("offer", typeof(Label), "0/0", new Point(width / 2 - 12, 50), new Size(width/2-20, 20), null, 1.2f, ContentAlignment.MiddleLeft));

            var bar = new PictureBox();
            bar.SizeMode = PictureBoxSizeMode.StretchImage;
            bar.Image = Properties.Resources.TraderBar;
            f.Add(new ExtendedControls.ConfigurableForm.Entry(bar, "bar", "", new Point(width / 2 - 32, 70), new Size(64, 16), null));

            f.Add(new ExtendedControls.ConfigurableForm.Entry("receive", typeof(Label), "0", new Point(width / 2 - 12, 90), new Size(width / 2-20, 20), null, 1.2f, ContentAlignment.MiddleLeft));

            f.Add(new ExtendedControls.ConfigurableForm.Entry("rlabel", typeof(Label), "Receive", new Point(20, 110), new Size(width-40, 20), null, 1.5f, ContentAlignment.MiddleCenter));

            f.AddOK(new Point(width - 100, 150));
            f.AddCancel(new Point(20, 150));

            f.Trigger += (a, b, c) => { System.Diagnostics.Debug.WriteLine("Ret " + b); f.ReturnResult(DialogResult.OK); };

            f.RightMargin = 20;

            f.ShowDialogCentred(this, this.Icon, "Trader",closeicon:true);

        }

        
        void CF(float s, bool wf)
        {
            theme.FontSize = s;
            theme.WindowsFrame = wf;

            ExtendedControls.ConfigurableForm f = new ExtendedControls.ConfigurableForm();

            int width = 430;
            int ctrlleft = 150;

            Type t = typeof(DataGridViewDialogs);

            int initialvalue = 200;
            Form parent = this;

            f.Add(new ExtendedControls.ConfigurableForm.Entry("L", typeof(Label), "Jump to:".Tx(t), new Point(10, 40), new Size(140, 24), ""));
            f.Add(new ExtendedControls.ConfigurableForm.Entry("Entry", typeof(ExtendedControls.NumberBoxLong), initialvalue.ToString(), new Point(ctrlleft, 40), new Size(width - ctrlleft - 20, 24), "Enter number to jump to or near to".Tx(t, "EN")) { numberboxdoubleminimum = 0, numberboxformat = "0" });

            f.Add(new ExtendedControls.ConfigurableForm.Entry("OK", typeof(ExtendedControls.ExtButton), "OK".Tx(), new Point(width - 100, 70), new Size(80, 24), "Press to Accept".Tx(t)));
            f.Add(new ExtendedControls.ConfigurableForm.Entry("Cancel", typeof(ExtendedControls.ExtButton), "Cancel".Tx(), new Point(width - 200, 70), new Size(80, 24), "Press to Cancel".Tx(t)));

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
                        ExtendedControls.MessageBoxTheme.Show(parent, "Value is not valid".Tx(t, "VNV"), "Warning".Tx(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (controlname == "Cancel" || controlname == "Close" )
                {
                    f.ReturnResult(DialogResult.Cancel);
                }
            };


            DialogResult res = f.ShowDialogCentred(parent, parent.Icon, "Jump to Entry".Tx(t, "Title"), closeicon:true);

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

            theme.FontSize = s;
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

        private void extButton10_Click(object sender, EventArgs e)
        {
            KeyForm f = new KeyForm();
            f.Init(null, true, " ", "", "", -1, false);
            f.Show(this);
        }

        private void extButton11_Click(object sender, EventArgs e)
        {
            theme.FontSize = 30;
            KeyForm f = new KeyForm();
            f.Init(null, true, " ", "", "", -1, false);
            f.Show(this);

        }

        string infotext = "Help me please\n2I need helpqkqwkqwkqw qwkqwkqw qwkqwkqw\n3qkwqkqw qwkqwjwhe wejhwehwehwe we wjkwjwj w wjh and so it continues over the line maybe\n4qkqkq wqjqwjqw ejwejwe  sdsd \n5 kqkqkwq \n\n7akakaka\n8L";


        private void extButton12_Click(object sender, EventArgs e)
        {
            theme.FontSize = 12;
            MessageBoxTheme.Show(infotext, "Help", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

        }

        private void extButton13_Click(object sender, EventArgs e)
        {
            theme.FontSize = 20;
            MessageBoxTheme.Show(infotext, "Help", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

        }

        private void extButton14_Click(object sender, EventArgs e)
        {
            theme.FontSize = 20;
            MessageBoxTheme.Show(infotext, "Help", MessageBoxButtons.RetryCancel);
        }

        private void extButton15_Click(object sender, EventArgs e)
        {
            theme.FontSize = 20;
            theme.WindowsFrame = true;
            MessageBoxTheme.Show(infotext, "Help", MessageBoxButtons.RetryCancel,MessageBoxIcon.Asterisk);

        }

        private void extButton20_Click(object sender, EventArgs e)
        {
            theme.FontSize = 12;
            MessageBoxTheme.Show(infotext, "Help", MessageBoxButtons.OKCancel, MessageBoxIcon.None);
        }

        private void extButton16_Click(object sender, EventArgs e)
        {
            theme.FontSize = 12;
            theme.WindowsFrame = true;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three" },
                                new string[] { "done", "dtwo", "dthree" },
                                false,
                                new string[] { "t1", "t2", "t3" },
                                true);
        }

        private void extButton17_Click(object sender, EventArgs e)
        {
            theme.FontSize = 20;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three" },
                                new string[] { "done", "dtwo", "dthree" },
                                true,
                                new string[] { "t1", "t2", "t3" });

        }

        private void extButton18_Click(object sender, EventArgs e)
        {
            theme.FontSize = 24;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three" },
                                new string[] { "done", "dtwo", "dthree" },
                                true,
                                new string[] { "t1", "t2", "t3" }, false, 400, 100);

        }

        private void extButton19_Click(object sender, EventArgs e)
        {
            theme.FontSize = 8;
            List<string> ret = PromptMultiLine.ShowDialog(this, "Prompt ML", this.Icon,
                                new string[] { "one", "two", "three" },
                                new string[] { "done", "dtwo", "dthree" },
                                true,
                                new string[] { "t1", "t2", "t3" }, false, 400, 200);


        }

    }
}
