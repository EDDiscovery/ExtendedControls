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
    public partial class TestTabControlCustom : Form
    {
        ThemeList themelist;

        int tabpageno = 0;
        public TestTabControlCustom()
        {
            themelist = new ThemeList();
            themelist.LoadBaseThemes();
            themelist.SetThemeByName("Elite Verdana Small Gradiant Skinny Scroll");
            Theme.Current.WindowsFrame = true;

            InitializeComponent();
            tabControl1.FlatStyle = FlatStyle.Popup;
            tabControl1.TabStyle = new TabStyleAngled();
            tabControl1.AllowDragReorder = true;
            tabControl1.ThemeColorSet = 0;
            tabControl1.Padding = new Point(8, 8);      // height is immaterial

            Theme.Current.ApplyStd(this);

            while (tabpageno < 9)
                MakeTabPage();
            

        }

        void MakeTabPage()
        {
            TabPage p = new TabPage();
            Label l = new Label() { Location = new Point(3, 3), Text = $"Tab Page {tabpageno}", AutoSize = true};
            p.Controls.Add(l);
            Theme.Current.ApplyStd(p);
            tabControl1.TabPages.Add(p);
            p.Text = $"Tab page {tabpageno}";
            tabpageno++;
        }
        private void extButtonT12_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            Theme.Current.ApplyStd(this);
            System.Diagnostics.Debug.WriteLine("Test Applied Font " + this.Font + " " + this.Font.Height);

        }

        private void extButtonT20_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 20;
            Theme.Current.ApplyStd(this);
            System.Diagnostics.Debug.WriteLine("Test Applied Font " + this.Font + " " + this.Font.Height);
        }

        private void extButtonT85_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 8.5f;
            Theme.Current.ApplyStd(this);
            System.Diagnostics.Debug.WriteLine("Test Applied Font " + this.Font + " " + this.Font.Height);

        }

        private void extButtonT85Std_Click(object sender, EventArgs e)
        {
            themelist.SetThemeByName("Windows Default");
            Theme.Current.ApplyStd(this);
            System.Diagnostics.Debug.WriteLine("Test Applied Font " + this.Font + " " + this.Font.Height);
        }

        int tabstyle = 0;

        private void buttonEnable_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = !tabControl1.Enabled;
        }

        private void buttonTabStyle_Click(object sender, EventArgs e)
        {
            if (tabstyle == 0)
                tabControl1.TabStyle = new ExtendedControls.TabStyleAngled();
            if (tabstyle == 1)
                tabControl1.TabStyle = new ExtendedControls.TabStyleRoundedEdge();
            if (tabstyle == 2)
                tabControl1.TabStyle = new ExtendedControls.TabStyleSquare();

            tabstyle = (tabstyle + 1) % 3;

        }

        private void buttonFlatStyle_Click(object sender, EventArgs e)
        {
            FlatStyle fs = tabControl1.FlatStyle;
            if (fs == FlatStyle.System)
                tabControl1.FlatStyle = FlatStyle.Popup;
            if (fs == FlatStyle.Popup)
                tabControl1.FlatStyle = FlatStyle.Flat;
            if (fs == FlatStyle.Flat)
                tabControl1.FlatStyle = FlatStyle.System;

        }
        private void extButtonFormFont_Click(object sender, EventArgs e)
        {
            Font fnt = this.Font;

            if (fnt.Name.Equals("Arial"))
                fnt = new Font("Microsoft Sans Serif", 8.0F);
            else
                fnt = new Font("Arial", 12.0F);

            this.Font = fnt;

        }

        private void buttonTCFont_Click(object sender, EventArgs e)
        {
            Font fnt;
            fnt = new Font("Arial", 14.0F);
            tabControl1.Font = fnt;
        }

        private void buttonTC2Font_Click(object sender, EventArgs e)
        {
            Font fnt;
            fnt = new Font("Microsoft Sans Serif", 8.0F);
            tabControl1.Font = fnt;

        }

        private void extButtonAddTab_Click(object sender, EventArgs e)
        {
            MakeTabPage();
        }

        private void extButtonRemoveTab_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.RemoveAt(0);
        }


    }
}
