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
    public partial class TestSplitter : Form
    {
        ThemeStandard theme;

        public TestSplitter()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontSize = 8.25f;
            theme.WindowsFrame = true;

            InitializeComponent();

            string splitctrl = "H(0.50, U'0,-1', U'1,-1')";       // default is a splitter without any selected panels

            SplitContainer sp = ControlHelpersStaticFunc.SplitterTreeMakeFromCtrlString(new BaseUtils.StringParser(splitctrl), MakeSplitContainer, MakeNode, 0);

            panelPlayfield.Controls.Add(sp);

            //theme.ApplyStd(this);

        }

        private SplitContainer MakeSplitContainer(Orientation ori, int lv)
        {
            System.Diagnostics.Debug.WriteLine("Make SC " + ori + " " + lv);
            SplitContainer sc = new SplitContainer() { Orientation = ori, Width = 1000, Height = 1000 };    // set width big to provide some res to splitter dist
            sc.Dock = DockStyle.Fill;
            sc.FixedPanel = FixedPanel.None;    // indicate to scale splitter distance on resize of control
            sc.Name = "SC-" + lv;
            sc.Controls[0].Name = lv + "-P1";       // names used for debugging this complicated beast!
            sc.Controls[1].Name = lv + "-P2";
            return sc;
        }

        private Control MakeNode(string s)
        {
            System.Diagnostics.Debug.WriteLine("Make node " + s);

            ExtendedControls.TabStrip tabstrip = new ExtendedControls.TabStrip();
            tabstrip.Dock = DockStyle.Fill;
            tabstrip.StripMode = ExtendedControls.TabStrip.StripModeType.StripTop;
//            tabstrip.StripMode = ExtendedControls.TabStrip.StripModeType.ListSelection;
            tabstrip.BackColor = Color.AliceBlue;
            tabstrip.ImageList = new Bitmap[] {
                DialogTest.Properties.Resources.galaxy_red,
                DialogTest.Properties.Resources.galaxy_gray,
                DialogTest.Properties.Resources.galaxy_gray,
                DialogTest.Properties.Resources.galaxy_white,
                DialogTest.Properties.Resources.galaxy_gray,
                DialogTest.Properties.Resources.galaxy_white,
                                            };

            tabstrip.TextList = new string[] { "icon 0", "icon 1",
                "icon 2", "icon 3",
                "icon 4", "icon 5",
            };

            tabstrip.Name = s;

            tabstrip.OnCreateTab += OnCreateTab;
            tabstrip.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("Command pop out" + t + " " + i);


            return tabstrip;
        }


        private Control OnCreateTab(ExtendedControls.TabStrip t, int no)
        {
            UserControl uc = new UserControl();
            uc.BackColor = Color.Cyan;
            uc.AutoScaleMode = AutoScaleMode.Font;

            DLabel lb = new DLabel();
            lb.Location = new Point(10, 10);
            lb.Size = new Size(200, 24);
            lb.Text = t.Name + " User Control " + (no + 0);

            ExtButton bb = new ExtButton();
            bb.Location = new Point(10, 40);
            bb.Size = new Size(40, 40);
            bb.Text = "BUT";
            bb.Name = "UC " + no + " BUT";
            uc.Controls.Add(bb);

            uc.Name = "UC " + no + "!.";
            uc.Dock = DockStyle.Fill;
            uc.Controls.Add(lb);
            t.SetControlText("CT<" + uc.Name + ">");

            theme.ApplyStd(uc);
            return uc;
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            theme.FontSize = 12;
            theme.ApplyStd(this);
        }
        private void extButton2_Click(object sender, EventArgs e)
        {
            theme.FontSize = 16;
            theme.ApplyStd(this);
        }
    }

    class DLabel : Label
    {
        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            System.Diagnostics.Debug.WriteLine("Scaling " + GetType().Name + " " + Name + " " + this.GetHeirarchy() + " " + Location + Size + " factor " + factor + Font);
            base.ScaleControl(factor, specified);
            System.Diagnostics.Debug.WriteLine(".. to " + Location + Size);
        }
    }
}
