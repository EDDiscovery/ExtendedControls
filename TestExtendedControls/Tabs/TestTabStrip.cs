using ExtendedControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestExtendedControls
{
    public partial class TestTabStrip : Form
    {
        ThemeList theme;

        public TestTabStrip()
        {
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana Small Gradiant Skinny Scroll");
            Theme.Current.WindowsFrame = true;
            
            InitializeComponent();
            tabStrip1.ImageList = new Bitmap[] {
                TestExtendedControls.Properties.Resources.galaxy_red, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_gray,TestExtendedControls.Properties.Resources.galaxy,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                                            };
            tabStrip1.TextList = new string[] { "icon 0", "icon 1",
                "icon 2", "icon 3",
                "icon 4", "icon 5",
                "icon 6", "icon 7",
                "icon 8", "icon 9",
                "icon 10", "icon 11",
                "icon 12", "icon 13",
                "icon 14", "icon 15",
                "icon 16", "icon 17",
                "icon 18", "icon 19",
            };

            tabStrip1.EmptyColor = Color.Red;
            tabStrip1.ThemeColorSet = 3;
            tabStrip1.SetControlText("Ctext1");
            tabStrip1.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("Command pop out" + t + " " + i);
            tabStrip1.OnCreateTab += OnCreateTab;

            tabStrip1.HelpAction = (p) => { System.Diagnostics.Debug.WriteLine("Help at " + p); };

            tabStrip2.ImageList = new Bitmap[] {
                TestExtendedControls.Properties.Resources.galaxy_red,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                                            };
            tabStrip2.TextList = new string[] { "icon 0", "icon 1",
                "icon 2", "icon 3",
                "icon 4", "icon 5",
            };

            tabStrip2.SetControlText("Ctext2");
            tabStrip2.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("2 Command pop out" + t + " " + i);
            tabStrip2.OnCreateTab += OnCreateTab;

            tabStrip3.ImageList = new Bitmap[] {
                TestExtendedControls.Properties.Resources.galaxy_red,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                                            };
            tabStrip3.TextList = new string[] { "3icon 0", "3icon 1",
                "3icon 2", "3icon 3",
                "3icon 4", "3icon 5",
            };

            tabStrip3.SetControlText("Ctext3");
            tabStrip3.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("3 Command pop out" + t + " " + i);
            tabStrip3.AllowClose += (t, i, c) => { return MessageBox.Show("Allow close", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes; };
            tabStrip3.OnCreateTab += OnCreateTab;


            tabStrip4.ImageList = new Bitmap[] {
                TestExtendedControls.Properties.Resources.galaxy_red,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                                           };
            tabStrip4.TextList = new string[] { "4icon 0", "4icon 1",
                "4icon 2", "4icon 3",
                "4icon 4", "4icon 5",
            };

            tabStrip4.SetControlText("Ctext4");
            tabStrip4.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("4 Command pop out" + t + " " + i);
            tabStrip4.OnCreateTab += OnCreateTab;

            tabStrip5.ImageList = new Bitmap[] {
                TestExtendedControls.Properties.Resources.CaptainsLog,
                TestExtendedControls.Properties.Resources.BookmarkManager,
                TestExtendedControls.Properties.Resources.CombatPanel,
                TestExtendedControls.Properties.Resources.Commodities,
                TestExtendedControls.Properties.Resources.Compass,
                TestExtendedControls.Properties.Resources.Discoveries,
                                            };
            tabStrip5.TextList = new string[] { 
                "4icon 0", "4icon 1",
                "4icon 2", "4icon 3",
                "4icon 4", "4icon 5",
            };

            tabStrip5.SetControlText("Ctext5");
            tabStrip5.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("3 Command pop out" + t + " " + i);
            tabStrip5.OnCreateTab += OnCreateTab;
            tabStrip5.DropDownFitImagesToItemHeight = true;


            Theme.Current.ApplyStd(this);
        }

        private Control OnCreateTab(ExtendedControls.TabStrip t, int no)
        {
            UserControl uc = new UserControl();
            uc.BackColor = Color.Cyan;
            uc.Name = "UC " + no;

            for ( int i =0; i <= 100; i+=20)
            {
                Label lb = new Label();
                lb.Location = new Point(4, i);
                lb.Size = new Size(200, 20);
                lb.Text = $"{i} {t.Name} Instance {no}";
                uc.Controls.Add(lb);
            }

            t.SetControlText("CT<" + uc.Name + ">");
            return uc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabStrip1.Close();
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            Theme.Current.ApplyStd(this);

        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 20;
            Theme.Current.ApplyStd(this);
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 8.5f;
            Theme.Current.ApplyStd(this);

        }
    }
}
