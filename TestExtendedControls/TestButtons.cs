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
    public partial class TestButtons : Form
    {
        ThemeStandard theme;

        public TestButtons()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontName = "Microsoft Sans Serif";
            theme.FontSize = 8.25f;
            theme.WindowsFrame = true;

            InitializeComponent();

            CompositeButton b = CompositeButton.QuickInit(Properties.Resources.Selector,
                                                           "Quick Init", new Font("Verdana", 8.25f), Color.Yellow, Color.Transparent,
                                                           Properties.Resources.edsm32x32, new Size(32, 32),
                                                           new Image[] { Properties.Resources.edlogo24, Properties.Resources.galaxy_black }, new Size(24, 24),
                                                           3,6,
                                                           (o, p) => { System.Diagnostics.Debug.WriteLine("CB " + o + " " + p); });
            this.Controls.Add(b);
            b.Location = new Point(250, 10);
            
            System.Diagnostics.Debug.WriteLine("Size needed " + b.FindMaxSubControlArea(0, 6));
            b.Size = new Size(128,b.FindMaxSubControlArea(0, 6).Height);

            //   theme.ApplyStd(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
//            compositeButton1.Size = new Size(128, 128);
        }

        private void button2_Click(object sender, EventArgs e)
        {
  //          compositeButton1.Size = new Size(400, 400);
        }

        private void extButton5_Click(object sender, EventArgs e)
        {
            if (groupBox1.FlatStyle == FlatStyle.System)
                groupBox1.FlatStyle = FlatStyle.Flat;
            else if (groupBox1.FlatStyle == FlatStyle.Flat)
                groupBox1.FlatStyle = FlatStyle.Popup;
            else if (groupBox1.FlatStyle == FlatStyle.Popup)
                groupBox1.FlatStyle = FlatStyle.Standard;
            else if (groupBox1.FlatStyle == FlatStyle.Standard)
                groupBox1.FlatStyle = FlatStyle.System;

            groupBox1.Invalidate();
        }

        private void extButton6_Click(object sender, EventArgs e)
        {
            groupBox1.BackColor = Color.DarkBlue;
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("Euro Caps", 12F);
        }

        private void upDown1_Selected(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Up/Down " + e.Delta);
        }
    }
}
