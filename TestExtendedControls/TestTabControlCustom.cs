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
    public partial class TestTabControlCustom : Form
    {
        ThemeStandard theme;

        public TestTabControlCustom()
        {
            theme = new ThemeStandard();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            theme.WindowsFrame = true;

            InitializeComponent();
            tabControlCustom1.FlatStyle = FlatStyle.Popup;
            tabControlCustom1.TabStyle = new TabStyleAngled();
            tabControlCustom1.AllowDragReorder = true;
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
    }
}
