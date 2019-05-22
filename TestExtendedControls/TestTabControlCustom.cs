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
            tabControl1.FlatStyle = FlatStyle.Popup;
            tabControl1.TabStyle = new TabStyleAngled();
            tabControl1.AllowDragReorder = true;
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

        int tabstyle = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = !tabControl1.Enabled;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabstyle == 0)
                tabControl1.TabStyle = new ExtendedControls.TabStyleAngled();
            if (tabstyle == 1)
                tabControl1.TabStyle = new ExtendedControls.TabStyleRoundedEdge();
            if (tabstyle == 2)
                tabControl1.TabStyle = new ExtendedControls.TabStyleSquare();

            tabstyle = (tabstyle + 1) % 3;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FlatStyle fs = tabControl1.FlatStyle;
            if (fs == FlatStyle.System)
                tabControl1.FlatStyle = FlatStyle.Popup;
            if (fs == FlatStyle.Popup)
                tabControl1.FlatStyle = FlatStyle.Flat;
            if (fs == FlatStyle.Flat)
                tabControl1.FlatStyle = FlatStyle.System;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Font fnt = tabControl1.Font;

            if (fnt.Name.Equals("Euro Caps"))
                fnt = new Font("Microsoft Sans Serif", 8.0F);
            else
                fnt = new Font("Euro Caps", 12.0F);

            tabControl1.Font = fnt;


        }
    }
}
