using ExtendedControls;
using ExtendedForms;
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
    public partial class TestConfigurableForms : Form
    {
        ThemeList theme;

        public TestConfigurableForms()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
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
            f.Add(new ConfigurableEntryList.Entry(butl, "left", "", new Point(20, 64), new Size(32, 32), null));
            var butr = new ExtendedControls.ExtButton();
            butr.Image = Properties.Resources.RightArrow;
            f.Add(new ConfigurableEntryList.Entry(butr, "right", "", new Point(width - 20 - 32, 64), new Size(32, 32), null));

            f.Add(new ConfigurableEntryList.Entry("olabel", typeof(Label), "Offer", new Point(20, 30), new Size(width - 40, 20), null, 1.5f, ContentAlignment.MiddleCenter));

            f.Add(new ConfigurableEntryList.Entry("offer", typeof(Label), "0/0", new Point(width / 2 - 12, 50), new Size(width / 2 - 20, 20), null, 1.2f, ContentAlignment.MiddleLeft));

            var bar = new PictureBox();
            bar.SizeMode = PictureBoxSizeMode.StretchImage;
            bar.Image = Properties.Resources.TraderBar;
            f.Add(new ConfigurableEntryList.Entry(bar, "bar", "", new Point(width / 2 - 32, 70), new Size(64, 16), null));

            f.Add(new ConfigurableEntryList.Entry("receive", typeof(Label), "0", new Point(width / 2 - 12, 90), new Size(width / 2 - 20, 20), null, 1.2f, ContentAlignment.MiddleLeft));

            f.Add(new ConfigurableEntryList.Entry("rlabel", typeof(Label), "Receive", new Point(20, 110), new Size(width - 40, 20), null, 1.5f, ContentAlignment.MiddleCenter));

            var panelbox = new GroupBox() { ForeColor = Color.Red };
            f.Add(new ConfigurableEntryList.Entry(panelbox, "panel", "g1", new Point(10, 150), new Size(width - 10, 100), "") { Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left });

            f.AddOK(new Point(width - 100, 270), anchor: AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);
            f.AddCancel(new Point(20, 270), anchor: AnchorStyles.Right | AnchorStyles.Bottom);

            f.Trigger += (a, b, c) => { System.Diagnostics.Debug.WriteLine("Ret " + b); if ( b == "OK" || b == "Close" ) f.ReturnResult(DialogResult.OK); };

            f.RightMargin = 20;
            f.AllowResize = true;
            f.AllowSpaceForScrollBar = false;

            Theme.Current.FontSize = 12;

            f.ShowDialogCentred(this, this.Icon, "Trader",closeicon:true, minsize: new Size(800, 500), maxsize:new Size(1000,600));

        }

        
        void CF(float s, bool wf, bool allowresize = false, Size? minsize = null, Size? maxsize = null, Size? reqsize = null)
        {
            Theme.Current.FontSize = s;
            Theme.Current.WindowsFrame = wf;

            ExtendedControls.ConfigurableForm f = new ExtendedControls.ConfigurableForm();

            int width = 430;
            int ctrlleft = 150;

            int initialvalue = 200;
            Form parent = this;

            f.Add(new ConfigurableEntryList.Entry("L", typeof(Label), "Jump to:", new Point(10, 40), new Size(140, 24), ""));

            var el = new ConfigurableEntryList.Entry("Entry", initialvalue, new Point(ctrlleft, 40), new Size(width - ctrlleft - 20, 24), "Enter number to jump to or near to") { NumberBoxLongMinimum = 0, NumberBoxFormat = "0", Anchor = AnchorStyles.Right };
            f.Add(el);

            f.Add(new ConfigurableEntryList.Entry("OK", typeof(ExtendedControls.ExtButton), "OK", new Point(width - 100, 70), new Size(80, 24), "Press to Accept") { Anchor = AnchorStyles.Right }) ;
            f.Add(new ConfigurableEntryList.Entry("Cancel", typeof(ExtendedControls.ExtButton), "Cancel", new Point(width - 200, 70), new Size(80, 24), "Press to Cancel") { Anchor = AnchorStyles.Right }) ;

            f.Trigger += (dialogname, controlname, tag) =>
            {
                System.Diagnostics.Debug.WriteLine($"Value is {initialvalue}");
                if (controlname == "OK" || controlname == "Entry:Return")
                {
                    long? v3 = f.GetInt("Entry");
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
                else if ( controlname == "Resize")
                {
                    System.Diagnostics.Debug.WriteLine($"Resize to {f.Bounds}");
                }
            };

            f.AllowResize = allowresize;
            DialogResult res = f.ShowDialogCentred(parent, parent.Icon, "Jump to Entry", closeicon:true,minsize:minsize,maxsize:maxsize,requestedsize:reqsize);

            if (res == DialogResult.OK)
            {
                int target = (int)f.GetLong("Entry").Value;
                System.Diagnostics.Debug.WriteLine($"Value is {initialvalue}");
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

        private void extButton33_Click(object sender, EventArgs e)
        {
            CF(8, false, true);
        }


        private void extButton34_Click(object sender, EventArgs e)
        {
            CF(8, false, true, new Size(100, 100));
        }

        private void extButton35_Click(object sender, EventArgs e)
        {
            CF(8, false, true, new Size(100, 100), new Size(500, 500));

        }

        private void extButton36_Click(object sender, EventArgs e)
        {
            CF(8, false, true, new Size(400, 100), new Size(800, 800), new Size(500, 250));
        }

        private void extButton37_Click(object sender, EventArgs e)
        {
            CF(8, false, true, new Size(100, 100), new Size(800, 800), new Size(-150,-150));

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
            cfg.Add(new ConfigurableEntryList.Entry(wikibutton, "Wiki", null, new Point(0, 0), new Size(24, 24), null));

            ExtButton videobutton = new ExtButton();
            videobutton.Image = Properties.Resources.CursorToTop;
            cfg.Add(new ConfigurableEntryList.Entry(videobutton, "Video", null, new Point(24, 0), new Size(24, 24), null));

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



        private void extButton38_Click(object sender, EventArgs e)
        {
            ExtendedControls.ConfigurableForm f = new ExtendedControls.ConfigurableForm();

            int width = 430;
            Form parent = this;

            f.Add(new ConfigurableEntryList.Entry("add", typeof(ExtButton), "Add+", new Point(10, 40), new Size(140, 24), ""));

            f.Add(new ConfigurableEntryList.Entry("OK", typeof(ExtendedControls.ExtButton), "OK", new Point(width - 100, 70), new Size(80, 24), "Press to Accept"));

            f.InstallStandardTriggers();

            int newpos = 70;
            f.Trigger += (formname, ctrlname, tag) =>
            {
                if (ctrlname == "add")
                {
                    f.MoveControls(newpos-15, 30);      // the -15 just ensures any rounding won't affect picking ok
                    f.Add(new ConfigurableEntryList.Entry("newc", typeof(ExtButton), "NB", new Point(10, newpos), new Size(140, 24), ""));
                    f.UpdateEntries();
                    newpos += 30;
                }
            };

            DialogResult res = f.ShowDialogCentred(parent, parent.Icon, "Dynamic", closeicon: true);

            if (res == DialogResult.OK)
            {

            }

        }

        private void extButton39_Click(object sender, EventArgs e)
        {
            FillLarge(100, 500,true);
        }
        private void extButton40_Click(object sender, EventArgs e)
        {
            FillLarge(1000, 4000, true);
        }
        private void extButton41_Click(object sender, EventArgs e)
        {
            FillLarge(200, 600, true);
        }
        private void FillLarge(int number, int vdepth, bool resize)
        { 
            bool[] state = new bool[number];
            string[] names = new string[number];
            for (int i = 0; i < number; i++)
                names[i] = "S_" + i;

            ExtendedControls.ConfigurableForm f = new ExtendedControls.ConfigurableForm();

            Form parent = this;

            f.Add(new ConfigurableEntryList.Entry("add", typeof(ExtButton), "But", new Point(10, 40), new Size(140, 24), "") { PlacedInPanel = ConfigurableEntryList.Entry.PanelType.Top });
            f.AddOK(new Point(10, 10), paneltype: ConfigurableEntryList.Entry.PanelType.Bottom);
            f.AddBools(names,names, state, 4, 24, vdepth, 4, 150, "B_");
            f.InstallStandardTriggers();

            f.Trigger += (formname, ctrlname, tag) =>
            {
            };

            f.TopPanelHeight = 100;
            f.BottomPanelHeight = 80;
            f.AllowResize = resize;
            f.BorderMargin = 5;

            //Theme.Current.FontSize = 8f;
            //Theme.Current.FontName = "MS Sans Serif";
            //Theme.Current.WindowsFrame = true;

            DialogResult res = f.ShowDialogCentred(parent, parent.Icon, "Big List", closeicon: true);

            if (res == DialogResult.OK)
            {

            }


        }

        private void extButton6_Click(object sender, EventArgs e)
        {
            ExtendedControls.ConfigurableForm f = new ExtendedControls.ConfigurableForm();

            int width = 430;
            int ctrlleft = 150;
            int initialvalue = 20;

            f.Add(new ConfigurableEntryList.Entry("L", typeof(Label), "Jump to:", new Point(10, 40), new Size(140, 24), ""));
            f.Add(new ConfigurableEntryList.Entry("Entry", typeof(ExtendedControls.NumberBoxLong), initialvalue.ToString(), new Point(ctrlleft, 40), new Size(width - ctrlleft - 20, 24), "Enter number to jump to or near to") { NumberBoxDoubleMinimum = 0, NumberBoxFormat = "0", Anchor = AnchorStyles.Right });

            f.Add(new ConfigurableEntryList.Entry("OK", typeof(ExtendedControls.ExtButton), "OK", new Point(width - 100, 10), new Size(80, 24), "Press to Accept") { Anchor = AnchorStyles.Right, PlacedInPanel = ConfigurableEntryList.Entry.PanelType.Bottom });
            f.Add(new ConfigurableEntryList.Entry("Cancel", typeof(ExtendedControls.ExtButton), "Cancel", new Point(width - 200, 10), new Size(80, 24), "Press to Cancel") { Anchor = AnchorStyles.Right, PlacedInPanel = ConfigurableEntryList.Entry.PanelType.Bottom });

            Theme.Current.FontSize = 12;

            f.TopPanelHeight = 40;
            f.BottomPanelHeight = 120;

            f.ShowDialogCentred(this, this.Icon, "Test", closeicon: true, minsize: new Size(800, 500), maxsize: new Size(1000, 600));

        }

        private void extButton7_Click(object sender, EventArgs e)
        {
            ExtendedControls.ConfigurableForm f = new ExtendedControls.ConfigurableForm();

            int width = 430;
            int ctrlleft = 150;
            int initialvalue = 20;

            f.Add(new ConfigurableEntryList.Entry("P", typeof(ExtPanelVertScrollWithBar), "", new Point(10, 40), new Size(500,900), ""));

            for (int i = 40; i < 1000; i += 100)
            {
                f.Add(new ConfigurableEntryList.Entry($"L{i}", typeof(Label), $"label {i}:", new Point(10, i), new Size(140, 24), "") { InPanel = "P" });
                f.Add(new ConfigurableEntryList.Entry($"Entry{i}", typeof(ExtendedControls.NumberBoxLong), initialvalue.ToString(), new Point(ctrlleft, 40), new Size(width - ctrlleft - 20, 24), "Enter number to jump to or near to") { NumberBoxDoubleMinimum = 0, NumberBoxFormat = "0", Anchor = AnchorStyles.Right, InPanel = "P" });
            }

//            f.Add(new ConfigurableEntryList.Entry("OK", typeof(ExtendedControls.ExtButton), "OK", new Point(width - 100, 10), new Size(80, 24), "Press to Accept") { Anchor = AnchorStyles.Right});
  //          f.Add(new ConfigurableEntryList.Entry("Cancel", typeof(ExtendedControls.ExtButton), "Cancel", new Point(width - 200, 10), new Size(80, 24), "Press to Cancel") { Anchor = AnchorStyles.Right});

            Theme.Current.FontSize = 12;

            //f.AllowResize = true;


            f.Trigger += (formname, ctrlname, tag) =>
            {
                if (ctrlname == "Close")
                {
                    f.ReturnResult(DialogResult.Cancel);
                }
            };

            f.ShowDialogCentred(this, this.Icon, "Test", closeicon: true);
        }
    }
}

