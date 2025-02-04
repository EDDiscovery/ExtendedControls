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
    public partial class TestTheme : Form
    {
        ThemeList stdthemes;

        public TestTheme()
        {
            InitializeComponent();
            stdthemes = new ThemeList();
            stdthemes.LoadBaseThemes();
            stdthemes.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;
            Theme.Current.ApplyStd(this);
            labelName.Text = Theme.Current.Name;

            for (int i = 0; i < 20; i++)
            {
                dataGridView.Rows.Add(new object[] { $"{i}", "two", "three"});
            }

            extComboBox1.Items.AddRange(new string[] { "one", "two", "three" });
            extListBox1.Items.AddRange(new string[] { "one", "two", "three", "four","five" });
            extRichTextBox1.Text = "Hello\r\nThere!\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7";
        }

        private void extButtonEdit_Click(object sender, EventArgs e)
        {
            ThemeEditor te = new ThemeEditor();
            te.InitForm(Theme.Current);

            var res = te.ShowDialog(this);

            if ( res == DialogResult.OK )
            {
                Theme.Current = te.Theme;
                Theme.Current.ApplyStd(this);
                labelName.Text = Theme.Current.Name;
            }
        }

        private void extButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = @"c:\code";
            dlg.FileName = "example";
            dlg.DefaultExt = ".theme";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Theme.Current.SaveFile(dlg.FileName);
            }

        }

        private void extButtonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"c:\code";
            dlg.FileName = "example";
            dlg.DefaultExt = ".theme";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Theme set = new Theme();
                if ( set.LoadFile(dlg.FileName, System.IO.Path.GetFileNameWithoutExtension(dlg.FileName)))
                {
                    Theme.Current = set;
                    Theme.Current.ApplyStd(this);
                    labelName.Text = Theme.Current.Name;

                }
            }

        }

        private void extButtonVerdana_Click(object sender, EventArgs e)
        {
            Theme.Current = stdthemes.FindTheme("Elite Verdana");
            Theme.Current.ApplyStd(this);
            labelName.Text = Theme.Current.Name;

        }

        private void extButtonEuroCaps_Click(object sender, EventArgs e)
        {
            Theme.Current = stdthemes.FindTheme("Elite EuroCaps");
            Theme.Current.ApplyStd(this);
            labelName.Text = Theme.Current.Name;

        }

        private void extButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void extButtonSystem_Click(object sender, EventArgs e)
        {
            Theme.Current = stdthemes.FindTheme("Windows Default");
            Theme.Current.ApplyStd(this);
            labelName.Text = Theme.Current.Name;

        }
    }
}
