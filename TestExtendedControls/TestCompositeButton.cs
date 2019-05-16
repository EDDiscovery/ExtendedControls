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
    public partial class TestCompositeButton : Form
    {
        ThemeStandard theme;

        public TestCompositeButton()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontSize = 12;

            InitializeComponent();

            theme.ApplyStd(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            compositeButton1.Size = new Size(128, 128);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            compositeButton1.Size = new Size(400, 400);

        }
    }
}
