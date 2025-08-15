using BaseUtils;
using ExtendedControls;
using QuickJSON;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TestExtendedControls
{
    public partial class TestTheme : DraggableForm
    {
        ThemeList stdthemes;
        List<string> aclist = new List<string>();

        public class T1
        {
            public int a;
            public int[] array;
        }

        public TestTheme()
        {
            InitializeComponent();

            Theme loadtheme = Theme.LoadFile(@"c:\code\example.theme");
            if (loadtheme != null)
            {
                System.Diagnostics.Debug.WriteLine("Theme loaded from file");
                string jsonout = loadtheme.ToJSON().ToString(true);
                FileHelpers.TryWriteToFile(@"c:\code\loaded.theme", jsonout);

                Theme.Current = loadtheme;
            }
            else
            {
                ThemeList lst = new ThemeList();
                lst.LoadBaseThemes();
                Theme.Current = lst.FindTheme("Elite Verdana Gradiant");
                Theme.Current = lst.FindTheme("Elite Verdana Gradiant Skinny Scroll");

                //Color hgb = Color.FromArgb(255, 10, 40, 10);
                //Color c64 = Color.FromArgb(255, 64, 64, 64);
                //Color elitebutback = Color.FromArgb(255, 32, 32, 32);
                //Theme.Current = new Theme("Elite Verdana", Color.Black,
                //    c64, Color.Orange, Color.FromArgb(255, 96, 96, 96), Theme.ButtonstyleGradient, // button
                //    Color.FromArgb(255, 176, 115, 0), Color.Black,  // grid border
                //    elitebutback, elitebutback, Color.Orange, Color.Orange, hgb, // back/alt fore/alt
                //    Color.DarkOrange, // borderlines
                //    elitebutback, Color.Orange, Color.DarkOrange, // grid slider, arrow, button
                //    Color.Red, Color.White, // travel
                //    elitebutback, Color.Orange, Color.Red, Color.Green, c64, Theme.TextboxborderstyleColor, // text box
                //    elitebutback, Color.Orange, Color.DarkOrange, // text back, arrow, button
                //    Color.Orange, Color.FromArgb(255, 65, 33, 33), c64,// checkbox
                //    Color.Black, Color.Orange, Color.DarkOrange, Color.Yellow,  // menu
                //    Color.Orange,  // label
                //    Color.Black, Color.Orange, Color.FromArgb(255, 130, 71, 0), // group
                //    Color.DarkOrange, // tab control
                //    Color.Black, Color.DarkOrange, Color.Orange, // toolstrips
                //    Color.Orange, // spanel
                //    Color.Green, // overlay
                //    false, 100, "Verdana", 10F, FontStyle.Regular);
            }

            stdthemes = new ThemeList();
            stdthemes.LoadBaseThemes();
           // stdthemes.SetThemeByName("Verdana Grey");

            Theme.Current.WindowsFrame = true;
            Theme.Current.ApplyStd(this);
            labelName.Text = Theme.Current.Name;

            extComboBoxTheme.Items.AddRange(stdthemes.GetThemeNames());

            for (int i = 0; i < 20; i++)
            {
                dataGridView.Rows.Add(new object[] { $"{i}", "two", "three"});
            }

            for (int i = 0; i < 500; i++)
            {
                extComboBox1.Items.Add($"Item {i}");
            }

            for (int i = 0; i < 50; i++)
            {
                extPanelDropDown1.Items.Add($"Item {i}");
            }

            for (int i = 0; i < 50; i++)
            {
                extListBox1.Items.Add($"Item {i}");
            }


            this.extTabControl1.TabStyle = new ExtendedControls.TabStyleAngled();

            extRichTextBox1.Text = "Hello\r\nThere!\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7";

            UpdateLabels(Theme.Current);

            labelData1.Data = new object[] { 10 };

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

            tabStrip1.StripMode = ExtendedControls.TabStrip.StripModeType.StripTop;
            tabStrip1.SetControlText("Ctext1");
            tabStrip1.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("Command pop out" + t + " " + i);
            tabStrip1.OnCreateTab += OnCreateTab;
            tabStrip1.OnPostCreateTab += OnPostCreateTab;
            tabStrip1.HelpAction = (p) => { System.Diagnostics.Debug.WriteLine("Help at " + p); };

            tabStrip2.ImageList = new Bitmap[150];
            tabStrip2.TextList = new string[tabStrip2.ImageList.Length];
                
            for ( int i = 0; i < tabStrip2.ImageList.Length; i++ )
            {
                tabStrip2.ImageList[i] = i % 2 == 0 ? TestExtendedControls.Properties.Resources.galaxy_red : TestExtendedControls.Properties.Resources.galaxy_gray;
                tabStrip2.TextList[i] = $"Item {i}";
            }

            tabStrip2.StripMode = ExtendedControls.TabStrip.StripModeType.StripTop;
            tabStrip2.SetControlText("Ctext2");
            tabStrip2.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("Command pop out" + t + " " + i);
            tabStrip2.OnCreateTab += OnCreateTab;
            tabStrip2.StripMode = TabStrip.StripModeType.ListSelection;
            tabStrip2.HelpAction = (p) => { System.Diagnostics.Debug.WriteLine("Help at " + p); };

            extCheckBox1.Checked = true;
            extCheckBox2.Checked = true;

            aclist.Add("one");
            aclist.Add("only");
            aclist.Add("onynx");
            aclist.Add("two");
            aclist.Add("three");
            aclist.Add("four");
            aclist.Add("five");
            aclist.Add("Aone");
            aclist.Add("Btwo");
            aclist.Add("Cthree");
            aclist.Add("Dfour");
            aclist.Add("Efive");
            for (int i = 0; i < 100; i++)
                aclist.Add($"Item {i}");
            extTextBoxAutoComplete1.SetAutoCompletor(AutoList);

            UserControl uc1 = new UserControl();
            uc1.Dock = DockStyle.Fill;
            uc1.BackColor = Color.Red;
            tabPage3.Controls.Add(uc1);

            extPanelRollUpFlow.FlowDirection = FlowDirection.LeftToRight;

            extPanelVertScrollWithBar1.Recalcuate();

            imageControl1.BackgroundImage = Properties.Resources.FleetCarrier;
            imageControl1.BackColor = Color.Green;
            imageControl1.ImageSize = new Size(Properties.Resources.FleetCarrier.Width, Properties.Resources.FleetCarrier.Height);
            imageControl1.ImageLayout = ImageLayout.Stretch;
            imageControl1.BackgroundImageLayout = ImageLayout.Stretch;
            imageControlScroll1.ImageControlMinimumHeight = imageControl1.ImageSize.Height;

            for (int i = 0; i < 10; i++)
            {
                extPictureBox1.AddTextAutoSize(new Point(0, i*50), new Size(2000, 1000), $"Label {i}", this.Font, Color.Blue, Color.Red, 1.0f);
            }

            extPictureBox1.Render();

            extPanelGradientFill1.ContextMenuStrip = ExtPanelGradientFill.RightClickThemeColorSetSelector((s) =>
            {
                System.Diagnostics.Debug.WriteLine($"Theme color {s}");
                extPanelGradientFill1.ThemeColorSet = s;
                extPanelGradientFill1.Theme(Theme.Current, Theme.Current.GetFont);
            });

            extFlowLayoutPanelTop.ContextMenuStrip = ExtPanelGradientFill.RightClickThemeColorSetSelector((s) =>
            {
                System.Diagnostics.Debug.WriteLine($"Theme color {s}");
                extFlowLayoutPanelTop.ThemeColorSet = s;
                extFlowLayoutPanelTop.Theme(Theme.Current, Theme.Current.GetFont);
            });

            extPanelRollUp1.ContextMenuStrip = ExtPanelGradientFill.RightClickThemeColorSetSelector((s) =>
            {
                System.Diagnostics.Debug.WriteLine($"Theme color {s}");
                extPanelRollUp1.ThemeColorSet = s;
                extPanelRollUp1.Theme(Theme.Current, Theme.Current.GetFont);
            });

            extPanelGradientFill2.ContextMenuStrip = ExtPanelGradientFill.RightClickThemeColorSetSelector((s) =>
            {
                System.Diagnostics.Debug.WriteLine($"Theme color {s}");
                extPanelGradientFill2.ThemeColorSet = s;
                extPanelGradientFill2.Theme(Theme.Current, Theme.Current.GetFont);
            });

            contextMenuStripTabs = ExtPanelGradientFill.RightClickThemeColorSetSelector((s) =>
            {
                System.Diagnostics.Debug.WriteLine($"Theme color {s}");
                extTabControl1.ThemeColorSet = s;
                extTabControl1.Theme(Theme.Current, Theme.Current.GetFont);
            },"Default");

            tabStrip1.ContextMenuStrip = ExtPanelGradientFill.RightClickThemeColorSetSelector((s) =>
            {
                System.Diagnostics.Debug.WriteLine($"Theme color {s}");
                tabStrip1.ThemeColorSet = s;
                tabStrip1.Theme(Theme.Current, Theme.Current.GetFont);
                //tabStrip1.Refresh();
            }, "Default");
        }

        ContextMenuStrip contextMenuStripTabs;

        public void AutoList(string input, ExtTextBoxAutoComplete t, SortedSet<string> set)
        {
            var res = (from x in aclist where x.StartsWith(input, StringComparison.InvariantCultureIgnoreCase) select x).ToList();
            SortedSet<string> ss = new SortedSet<string>();
            foreach (var x in res)
                set.Add(x);

            //  System.Threading.Thread.Sleep(2000);
        }

        private Control OnCreateTab(ExtendedControls.TabStrip t, int no)
        {
            UserControl uc = new UserControl();
            uc.BackColor = Color.Cyan;
            Label lb = new Label();
            lb.Location = new Point(10, 10);
            lb.Size = new Size(200, 20);
            lb.Text = t.Name + " User Control " + (no + 0);
            ExtButton eb = new ExtButton();
            eb.Size = new Size(40, 24);
            eb.Text = "but";
            eb.Location = new Point(10, 30);
            eb.Name = $"UC {no} Button";
            uc.Name = $"UC {no}";
            uc.Dock = DockStyle.Fill;
            uc.Controls.Add(eb);
            uc.Controls.Add(lb);
            t.SetControlText("CT<" + uc.Name + ">");
            return uc;
        }

        private void OnPostCreateTab(ExtendedControls.TabStrip t, Control ctrl, int no)
        {
            Theme.Current.ApplyStd(ctrl);
        }


        private void UpdateLabels(Theme v)
        {
            Font f = v.GetScaledFont(0.8F, 999, true);
            labelUnderline.Font = f;
            f.Dispose();
            f = v.GetScaledFont(1.5F, 999, false,true);
            labelStrikeout.Font = f;
        }

        private void extButtonEdit_Click(object sender, EventArgs e)
        {
            ThemeEditor te = new ThemeEditor();
            te.InitForm(Theme.Current);
            te.ApplyChanges += (o) => {
               o.ApplyStd(this);
                UpdateLabels(o);
            };

            var res = te.ShowDialog(this);

            if (res == DialogResult.OK)
            {
                Theme.Current = te.Theme;
                Theme.Current.ApplyStd(this);
                labelName.Text = Theme.Current.Name;
            }
            else
                Theme.Current.ApplyStd(this);
        }

        private void extButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = @"c:\code";
            dlg.FileName = "example";
            dlg.DefaultExt = ".theme";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Theme.Current.SaveFile(dlg.FileName);
            }

            // demonstrate here, output of JSON output control settings for FromObject.
            var dict = QuickJSON.JToken.GetMemberAttributeSettings(typeof(Theme), "AltFmt", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            JToken jo = JObject.FromObjectWithError(dict, false, new Type[] { typeof(System.Reflection.MemberInfo) }, 10);
            BaseUtils.FileHelpers.TryWriteToFile(@"c:\code\theme.txt", jo.ToString(true));

        }

        private void extButtonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"c:\code";
            dlg.FileName = "example";
            dlg.DefaultExt = ".theme";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Theme set = Theme.LoadFile(dlg.FileName, System.IO.Path.GetFileNameWithoutExtension(dlg.FileName));

                if (set != null)
                {
                    Theme.Current = set;
                    Theme.Current.ApplyStd(this);
                    labelName.Text = Theme.Current.Name;

                }
            }

        }

        private void extButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void extComboBoxTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)extComboBoxTheme.SelectedItem;
            Theme.Current = stdthemes.FindTheme(name);
            Theme.Current.ApplyStd(this);
            labelName.Text = name;


        }

        private void extButton7_Click(object sender, EventArgs e)
        {
            TransparencyKey = Color.Gray;
            this.BackColor = TransparencyKey;
            tabStrip1.PaintTransparentColor = TransparencyKey;
            tabStrip2.PaintTransparentColor = TransparencyKey;
            extTabControl1.PaintTransparentColor = TransparencyKey;
            extFlowLayoutPanelTop.PaintTransparentColor = TransparencyKey;
        }

        private void extButton8_Click(object sender, EventArgs e)
        {
            this.BackColor = Theme.Current.Form;
            extFlowLayoutPanelTop.PaintTransparentColor = Color.Transparent;
            tabStrip1.PaintTransparentColor = Color.Transparent;
            tabStrip2.PaintTransparentColor = Color.Transparent;
            extTabControl1.PaintTransparentColor = Color.Transparent;

        }

        private void extButton9_Click(object sender, EventArgs e)
        {
            extButton10.Visible = !extButton10.Visible;     // flick a flow button on/off
        }

        private void extTabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            contextMenuStripTabs.Show(extTabControl1.PointToScreen(e.Location));
        }

        private void TestTheme_MouseDown(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Click on form");
        }
    }
}
