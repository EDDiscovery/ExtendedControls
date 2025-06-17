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
    public partial class TestSplitterContainer : Form
    {
        ThemeList theme;

        public TestSplitterContainer()
        {
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            Theme.Current.FontSize = 8.25f;
            Theme.Current.WindowsFrame = true;

            InitializeComponent();

            string splitctrl = "H(0.50, U'0,-1', U'1,-1')";       // default is a splitter without any selected panels

            SplitContainer sp = ControlHelpersStaticFunc.SplitterTreeMakeFromCtrlString(new BaseUtils.StringParser(splitctrl), MakeSplitContainer, MakeNode, 0);

            tabPage1.Controls.Add(sp);

            //Theme.Current.ApplyStd(this);

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
                TestExtendedControls.Properties.Resources.galaxy_red,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
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

            Theme.Current.ApplyStd(uc);
            return uc;
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            Theme.Current.ApplyStd(this);
        }
        private void extButton2_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 16;
            Theme.Current.ApplyStd(this);
        }

        private void extButton2_Click_1(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 14;
            Theme.Current.ApplyStd(this);

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
