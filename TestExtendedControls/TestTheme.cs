using ExtendedControls;
using QuickJSON;
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
            //Theme.Current = new Theme("Std");
            Theme.Current.ApplyStd(this);
            labelName.Text = Theme.Current.Name;

            extComboBoxTheme.Items.AddRange(stdthemes.GetThemeNames());

            for (int i = 0; i < 20; i++)
            {
                dataGridView.Rows.Add(new object[] { $"{i}", "two", "three"});
            }

            extComboBox1.Items.AddRange(new string[] { "one", "two", "three" });
            extListBox1.Items.AddRange(new string[] { "one", "two", "three", "four","five" });
            extRichTextBox1.Text = "Hello\r\nThere!\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7";

            UpdateLabels(Theme.Current);
        }

        private void UpdateLabels(Theme v)
        {
            Font f = v.GetScaledFont(0.8F, 999, true);
            labelUnderline.Font = f;
            f.Dispose();
            f = v.GetScaledFont(1.5F, 999, false,true);
            labelStrikeout.Font = f;
        }

        private void extButtonEdit_Click(object sender, EventArgs e)
        {
            ThemeEditor te = new ThemeEditor();
            te.InitForm(Theme.Current);
            te.ApplyChanges += (o) => {
               o.ApplyStd(this);
                UpdateLabels(o);
            };

            var res = te.ShowDialog(this);

            if (res == DialogResult.OK)
            {
                Theme.Current = te.Theme;
                Theme.Current.ApplyStd(this);
                labelName.Text = Theme.Current.Name;
            }
            else
                Theme.Current.ApplyStd(this);
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

            // demonstrate here, output of JSON output control settings for FromObject.
            var dict = QuickJSON.JToken.GetMemberAttributeSettings(typeof(Theme), "AltFmt", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            JToken jo = JObject.FromObjectWithError(dict, false, new Type[] { typeof(System.Reflection.MemberInfo) }, 10);
            BaseUtils.FileHelpers.TryWriteToFile(@"c:\code\theme.txt", jo.ToString(true));

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

        private void extButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void extComboBoxTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)extComboBoxTheme.SelectedItem;
            Theme.Current = stdthemes.FindTheme(name);
            Theme.Current.ApplyStd(this);
            labelName.Text = name;


        }
    }
}
