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
    public partial class TestMisc : Form
    {
        ThemeStandard theme;
        public TestMisc()
        {
            InitializeComponent();
            theme = new ThemeStandard();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            theme.WindowsFrame = true;

            for (int i = 0; i < 100; i++)
                extComboBox1.Items.Add("Item " + i);

            extPanelDropDown1.Items = new List<string>() { "One", "two", "three" };

            extTextBoxAutoComplete1.SetAutoCompletor(AutoList);

            System.Drawing.Imaging.ColorMap colormap = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images    
            colormap.OldColor = Color.White;                                                        // white is defined as the forecolour
            colormap.NewColor = Color.Orange;
            System.Drawing.Imaging.ColorMap colormap2 = new System.Drawing.Imaging.ColorMap();       // any drawn panel with drawn images    
            colormap2.OldColor = Color.FromArgb(222, 222, 222);                                                        // white is defined as the forecolour
            colormap2.NewColor = Color.Orange.Multiply(0.8F);
            foreach (Control c in tableLayoutPanel3.Controls)
            {
                if (c is ExtendedControls.ExtCheckBox)
                    (c as ExtendedControls.ExtCheckBox).SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap, colormap2 });
                else if (c is ExtendedControls.ExtButton)
                    (c as ExtendedControls.ExtButton).SetDrawnBitmapRemapTable(new System.Drawing.Imaging.ColorMap[] { colormap, colormap2 });
            }
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            theme.FontSize = 12;
            theme.ApplyStd(this);

        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            theme.FontSize = 20;
            theme.ApplyStd(this);

        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            this.Font = new Font("Verdana", 20f);
        }

        public static List<string> AutoList(string input, ExtTextBoxAutoComplete t)
        {
            List<string> list = new List<string>();

            list.Add("one");
            list.Add("only");
            list.Add("onynx");
            list.Add("two");
            list.Add("three");
            list.Add("four");
            list.Add("five");
            list.Add("Aone");
            list.Add("Btwo");
            list.Add("Cthree");
            list.Add("Dfour");
            list.Add("Efive");

            List<string> res = (from x in list where x.StartsWith(input, StringComparison.InvariantCultureIgnoreCase) select x).ToList();
            return res;
        }

    }
}
