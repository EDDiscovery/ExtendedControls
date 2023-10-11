using ExtendedControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExtendedControls
{
    public partial class TestCheckedIconListBoxForm : Form
    {
        ThemeList theme;

        CheckedIconListBoxFormGroup fgroup;

        public TestCheckedIconListBoxForm()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            Theme.Current.FontSize = 12;

            fgroup = new CheckedIconListBoxFormGroup();

            var imglist = new Image[] { Properties.Resources.edlogo24, Properties.Resources.Logo8bpp48, Properties.Resources.galaxy_white, Properties.Resources.Logo8bpp48rot, Properties.Resources.galaxy_red, };

            //fgroup.AddAllNone();
            //fgroup.AddGroupOption("T1;T2", "T1+2");

            for (int i = 0; i < 250; i++)
            {
                fgroup.AddStandardOption("T" + i.ToString(), "Tx" + i.ToString(), imglist[i % imglist.Length]);
            }

       //     fgroup.SortStandardOptions();

         //   fgroup.AddStandardOptionAtTop("B1", "Button1", button: true);

           // fgroup.AddGroupOption("T1;T2;T3;T4;", "G1-4");
          //  fgroup.AddGroupOption("T5;T6;T7;T8;", "G5-8");
            fgroup.CheckedChanged += F_CheckedChanged;
            fgroup.SaveSettings += F_SaveSettings;
            fgroup.ButtonPressed += F_ButtonPressed;
            fgroup.HideOnDeactivate = true;
            fgroup.CloseOnDeactivate = false;
            fgroup.MultipleColumnsAllowed = true;
            fgroup.MultipleColumnsFitToScreen = true;
            fgroup.BorderStyle = BorderStyle.Fixed3D;
            //  fgroup.ImageSize = new Size(24, 24);

            extButtonWithCheckedIconListBoxGroup1.Init(new List<CheckedIconListBoxFormGroup.StandardOption>
            {
                new CheckedIconListBoxFormGroup.StandardOption("Tag1","Tag1",Properties.Resources.Addtab,"Tag5"),
                new CheckedIconListBoxFormGroup.StandardOption("Tag2","Tag2",Properties.Resources.BookmarkManager,"Tag5"),
                new CheckedIconListBoxFormGroup.StandardOption("Tag3","Tag3",Properties.Resources.BookmarkManager),
                new CheckedIconListBoxFormGroup.StandardOption("Tag4","Tag4",Properties.Resources.BookmarkManager),
                new CheckedIconListBoxFormGroup.StandardOption("Tag5","Tag5",Properties.Resources.BookmarkManager,"Tag2;Tag1"),
            }, groupoptions, (s) => groupoptions = s,
            groupoptions:new List<CheckedIconListBoxFormGroup.GroupOption>
            {
                new CheckedIconListBoxFormGroup.GroupOption("Tag1;Tag2","Option 1&2"),
                new CheckedIconListBoxFormGroup.GroupOption("Tag3;Tag4","Option 3&4")
            });
        }

        string groupoptions = "";

        void Log(string s)
        {
            extRichTextBox1.Text += s + Environment.NewLine;
            extRichTextBox1.Select(extRichTextBox1.Text.Length, extRichTextBox1.Text.Length);
        }

        private void F_CheckedChanged(object sender, ItemCheckEventArgs e, Object tag)
        {
            var s = sender as CheckedIconListBoxForm;
            extRichTextBox1.AppendText("From " + sender.GetType().Name + " " + e.Index + " c " + e.CurrentValue + " new " + e.NewValue + " " + s.GetChecked() + Environment.NewLine);
        }

        private void F_SaveSettings(string s, Object tag)
        {
            extRichTextBox1.AppendText("Save settings " + s + Environment.NewLine);
        }
        private void F_ButtonPressed(int index, string stag, string text, Object tag, MouseEventArgs e)
        {
            extRichTextBox1.AppendText($"Button {stag} {text} with {e.Button}" + Environment.NewLine);
        }

        // icon list box

        private void extButton1_Click(object sender, EventArgs e)
        {
            Log("Button1");
            CheckedIconListBoxForm f = new CheckedIconListBoxForm();

            var imglist = new Image[] { Properties.Resources.edlogo24, Properties.Resources.Logo8bpp48, Properties.Resources.galaxy_white, Properties.Resources.Logo8bpp48rot, Properties.Resources.galaxy_red, };

            for (int i = 0; i < 10; i++)
                f.AddItem("T" + i.ToString(), "Tx" + i.ToString(), imglist[i % imglist.Length], false, (i == 0) ? "T1;T2" : (i == 1) ? "T0;T2" : (i == 2) ? "T0;T1" : null, i <= 2);

            f.AddItem(null, "Press Button", imglist[0], button: true);

            f.PositionBelow(extButton1);
            f.SetChecked("T1;T4");
            f.SaveSettings += F_SaveSettings;
            f.CheckBoxColor = Color.Orange;
            f.CheckBoxInnerColor = Color.Yellow;
            f.CheckColor = Color.White;
            f.FlatStyle = FlatStyle.Popup;
            //f.ImageSize = new Size(32, 32);
            f.SliderColor = Color.Green;
            f.ThumbButtonColor = Color.Red;
            f.ArrowButtonColor = Color.Yellow;
            f.Font = new Font("Euro Caps", 16);
            f.CloseBoundaryRegion = new Size(32, 32);
            f.CheckedChanged += F_CheckedChanged;
            f.SaveSettings += (s, p) => { System.Diagnostics.Debug.WriteLine("Save button1"); };
            f.ButtonPressed += F_ButtonPressed;
            f.Show(this);
        }


        private void extButton3_Click(object sender, EventArgs e)
        {
            CheckedIconListBoxForm f = new CheckedIconListBoxForm();

            var imglist = new Image[] { Properties.Resources.edlogo24, Properties.Resources.Logo8bpp48, Properties.Resources.galaxy_white, Properties.Resources.Logo8bpp48rot, Properties.Resources.galaxy_red, };

            for (int i = 0; i < 20; i++)
                f.AddItem("T" + i.ToString(), "Tx" + i.ToString(), imglist[i % imglist.Length]);

            f.PositionBelow(extButton1);
            f.SetChecked("Two;Four");
            f.CheckedChanged += F_CheckedChanged;
            f.SaveSettings += F_SaveSettings;
            f.CheckBoxColor = Color.Orange;
            f.CheckBoxInnerColor = Color.Yellow;
            f.CheckColor = Color.White;
            f.FlatStyle = FlatStyle.System;
            //f.ImageSize = new Size(32, 32);
            f.Show(this);

        }

        private void extButton4_Click(object sender, EventArgs e)
        {
            CheckedIconListBoxForm f = new CheckedIconListBoxForm();

            var imglist = new Image[] { Properties.Resources.edlogo24, Properties.Resources.Logo8bpp48, Properties.Resources.galaxy_white, Properties.Resources.Logo8bpp48rot, Properties.Resources.galaxy_red, };
            for (int i = 0; i < 20; i++)
                f.AddItem("T" + i.ToString(), "Tx" + i.ToString(), imglist[i % imglist.Length]);

            f.PositionBelow(extButton1);
            f.SetChecked("Two;Four");
            f.CheckedChanged += F_CheckedChanged;
            f.SaveSettings += F_SaveSettings;
            f.Font = new Font("Euro Caps", 16);
            Theme.Current.ApplyStd(f);
            f.Show(this);

        }

        CheckedIconListBoxFormGroup MakeSelForm(float size, bool group)
        {
            CheckedIconListBoxFormGroup f = new CheckedIconListBoxFormGroup();

            var imglist = new Image[] { Properties.Resources.edlogo24, Properties.Resources.Logo8bpp48, Properties.Resources.galaxy_white, Properties.Resources.Logo8bpp48rot, Properties.Resources.galaxy_red, };

            f.AddAllNone();
            f.AddGroupOption("T1;T2", "T1+2");

            for (int i = 0; i <10; i++)
            {
                f.AddStandardOption("T" + i.ToString(), "Tx" + i.ToString(), imglist[i % imglist.Length]);
            }

            f.SortStandardOptions();

            f.AddStandardOptionAtTop("B1", "Button1", button: true);

            if (group)
            {
                f.AddGroupOption("T1;T2;T3;T4;", "G1-4");
                f.AddGroupOption("T5;T6;T7;T8;", "G5-8");
            }

            f.CheckedChanged += F_CheckedChanged;
            f.SaveSettings += F_SaveSettings;
            f.ButtonPressed += F_ButtonPressed;
            Theme.Current.FontSize = size;
            f.Create("All", true);

            return f;
        }

        private void CLBSF(float size, bool group)
        {
            Stopwatch sw = new Stopwatch(); sw.Start();
            CheckedIconListBoxFormGroup f = MakeSelForm(size, group);
            f.PositionBelow(extButton5);
            f.Show(this);
            System.Diagnostics.Debug.WriteLine("CLBSF in " + sw.ElapsedMilliseconds);

        }

        private void extButton5_Click(object sender, EventArgs e)
        {
            CLBSF(12, true);
        }

        private void extButton6_Click(object sender, EventArgs e)
        {
            CLBSF(12, false);
        }

        private void extButton7_Click(object sender, EventArgs e)
        {
            CLBSF(20, false);
        }

        private void extButton8_Click(object sender, EventArgs e)
        {
            CLBSF(30, true);
        }

        CheckedIconListBoxForm showhidelistbox1 = null;
        private void extButton2_Click(object sender, EventArgs e)
        {
            if ( showhidelistbox1 != null && showhidelistbox1.DeactivatedWithin(250) )
            {
                System.Diagnostics.Debug.WriteLine("Button 2 ignoring as just hid");
                return;
            }

            if (showhidelistbox1 == null)
            {
                System.Diagnostics.Debug.WriteLine("Button 2 create");
                showhidelistbox1 = new CheckedIconListBoxForm();

                var imglist = new Image[] { Properties.Resources.edlogo24, Properties.Resources.Logo8bpp48, Properties.Resources.galaxy_white, Properties.Resources.Logo8bpp48rot, Properties.Resources.galaxy_red, };

                for (int i = 0; i < 200; i++)
                    showhidelistbox1.AddItem("T" + i.ToString(), "Tx" + i.ToString(), imglist[i % imglist.Length]);
                showhidelistbox1.CheckedChanged += F_CheckedChanged;
                showhidelistbox1.SaveSettings += F_SaveSettings;
                showhidelistbox1.CheckBoxColor = Color.Orange;
                showhidelistbox1.CheckBoxInnerColor = Color.Yellow;
                showhidelistbox1.CheckColor = Color.White;
                showhidelistbox1.FlatStyle = FlatStyle.Popup;
                //showhide.ImageSize = new Size(32, 32);
                showhidelistbox1.SliderColor = Color.Green;
                showhidelistbox1.ThumbButtonColor = Color.Red;
                showhidelistbox1.ArrowButtonColor = Color.Yellow;
                showhidelistbox1.SetChecked("Two;Four");
                showhidelistbox1.Font = new Font("Euro Caps", 10);
                showhidelistbox1.CloseOnDeactivate = false;
                showhidelistbox1.HideOnDeactivate = true;
                showhidelistbox1.CloseBoundaryRegion = new Size(32, 32);
                showhidelistbox1.SaveSettings += (s, p) => { System.Diagnostics.Debug.WriteLine("Save show hide list box 1"); };

            }

            System.Diagnostics.Debug.WriteLine("Button 2 position and show");
            showhidelistbox1.PositionBelow(extButton2);
            showhidelistbox1.Show(this);

        }

        CheckedIconListBoxFormGroup showhideselform1 = null;

        private void extButton9_Click(object sender, EventArgs e)
        {
            if (showhideselform1 == null)
            {
                showhideselform1 = MakeSelForm(12, true);
                showhideselform1.CloseOnDeactivate = false;
                showhideselform1.HideOnDeactivate = true;
            }

            showhideselform1.PositionBelow(extButton9);
            showhideselform1.Show(this);
        }

        private void extButton10_Click(object sender, EventArgs e)
        {
            DropDown(true);
        }

        private void extButton11_Click(object sender, EventArgs e)
        {
            DropDown(false);
        }

        private void DropDown(bool themeit)
        { 
            var dropdown = new ExtListBoxForm("", true);

            Image[] imagelist = new Image[] {
                Properties.Resources.galaxy_black,
                Properties.Resources.galaxy_gray,
                Properties.Resources.galaxy_red,
                Properties.Resources.galaxy_white,
            };

            string[] textlist = new string[] { "256", "192", "128", "96"};

            dropdown.Items = textlist.ToList();
            dropdown.ImageItems = imagelist.ToList();
            dropdown.FlatStyle = FlatStyle.Popup;
            dropdown.PositionBelow(extButton11);

            if (themeit)
            {
                Theme.Current.ApplyStd(dropdown);
            }
            else
            { 
                var stdtheme = new ThemeList();
                stdtheme.LoadBaseThemes();
                stdtheme.SetThemeByName("Windows Default");
                Theme.Current.ApplyStd(dropdown,true);
            }


            dropdown.Show(this.FindForm());


        }

        private void extButton12_Click(object sender, EventArgs e)
        {
            if (fgroup.Visible == true)
            {
                fgroup.Hide();
            }
            else 
            {
                fgroup.Show("All", extButton12, this);     // use the quick helper. 
            }

        }

        private void extButton13_Click(object sender, EventArgs e)
        {
            fgroup.Clear();
        }

        int bi = 0;
        private void extButton14_Click(object sender, EventArgs e)
        {
            fgroup.AddStandardOptionAtTop("B2", $"Button {bi}", button: true);
            fgroup.AddGroupOption("G2", $"Group {bi}");
            bi++;
        }
    }
}
