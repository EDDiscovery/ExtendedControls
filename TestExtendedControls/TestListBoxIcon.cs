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

namespace DialogTest
{
    public partial class TestListBoxIcon : Form
    {
        ThemeStandard theme;

        public TestListBoxIcon()
        {
            InitializeComponent();
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontSize = 12;
        }

        private void F_CheckedChanged(object sender, ItemCheckEventArgs e, Object tag)
        {
            var s = sender as CheckedIconListBoxForm;
            extRichTextBox1.Text += "From " + sender.GetType().Name + " " + e.Index + " c " + e.CurrentValue + " new " + e.NewValue + " " + s.GetChecked() + Environment.NewLine;
            
            extRichTextBox1.Select(extRichTextBox1.Text.Length, extRichTextBox1.Text.Length);
        }

        private void F_SaveSettings(string s, Object tag)
        {
            extRichTextBox1.Text += "Save settings " + s + Environment.NewLine;
            extRichTextBox1.Select(extRichTextBox1.Text.Length, extRichTextBox1.Text.Length);
        }

        // icon list box

        private void extButton1_Click(object sender, EventArgs e)
        {
            CheckedIconListBoxForm f = new CheckedIconListBoxForm();

            var imglist = new Image[] { Properties.Resources.edlogo24, Properties.Resources.Logo8bpp48, Properties.Resources.galaxy_white, Properties.Resources.Logo8bpp48rot, Properties.Resources.galaxy_red, };

            for (int i = 0; i < 200; i++)
                f.AddItem("T" + i.ToString(), "Tx" + i.ToString(), imglist[i % imglist.Length]);

            f.PositionBelow(extButton1);
            f.SetChecked("Two;Four");
            f.CheckedChanged += F_CheckedChanged;
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
            theme.ApplyStd(f);
            f.Show(this);

        }

        CheckedIconListBoxFormGroup MakeSelForm(float size, bool group)
        {
            CheckedIconListBoxFormGroup f = new CheckedIconListBoxFormGroup();

            var imglist = new Image[] { Properties.Resources.edlogo24, Properties.Resources.Logo8bpp48, Properties.Resources.galaxy_white, Properties.Resources.Logo8bpp48rot, Properties.Resources.galaxy_red, };

            f.AddAllNone();
            for (int i = 0; i <250; i++)
            {
                f.AddStandardOption("T" + i.ToString(), "Tx" + i.ToString(), imglist[i % imglist.Length]);
            }

            if (group)
            {
                f.AddGroupOption("T1;T2;T3;T4;", "G1-4");
                f.AddGroupOption("T5;T6;T7;T8;", "G5-8");
            }
            f.CheckedChanged += F_CheckedChanged;
            f.SaveSettings += F_SaveSettings;
            theme.FontSize = size;
            f.Create("", true);

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
            if (showhidelistbox1 == null)
            {
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
                System.Diagnostics.Debug.WriteLine("Make");
            }
            System.Diagnostics.Debug.WriteLine("Position");

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
                theme.ApplyStd(dropdown);
            }
            else
            { 
                var stdtheme = new ThemeStandard();
                stdtheme.LoadBaseThemes();
                stdtheme.SetThemeByName("Windows Default");

                stdtheme.ApplyStd(dropdown,true);
            }


            dropdown.Show(this.FindForm());


        }
    }
}
