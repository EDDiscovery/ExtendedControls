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
    public partial class TestVScroll : Form
    {
        ThemeList theme;

        public TestVScroll()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;
            Theme.Current.ApplyStd(this);
        }

        private void extScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("2 valuechanged custom at " + extScrollBar2.Value);

        }

        private void extScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("1 valuechanged custom at " + extScrollBar2.Value);

        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Vs valuechanged custom at " + vScrollBar1.Value);
        }


        private void extButton1_Click(object sender, EventArgs e)
        {
            if (extScrollBar1.Maximum != 5)
                extScrollBar1.SetValueMaximumLargeChange(0, 5, 10);
            else
                extScrollBar1.SetValueMaximumLargeChange(0, 10, 10);

            if (extScrollBar2.Maximum != 5)
                extScrollBar2.SetValueMaximumLargeChange(0, 5, 10);
            else
                extScrollBar2.SetValueMaximumLargeChange(0, 10, 10);


        }
    }
}
