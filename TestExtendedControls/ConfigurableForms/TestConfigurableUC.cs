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
    public partial class TestConfigurableUC : Form
    {
        public TestConfigurableUC()
        {
            InitializeComponent();
            var theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //configurableUC1.AddLabelAndEntry("L1", new Point(0, 4), new Size(100, 24), new ConfigurableUC.Entry("N1", 10, new Point(100, 4), new Size(50, 24), "N1"));

            for (int i = 0; i < 1400; i += 100)
            {
                configurableUC1.Entries.Add(new ConfigurableEntryList.Entry("B" + i, typeof(ExtButton), i.ToString(), new Point(100, i), new Size(50, 24), "N1"));
            }
            configurableUC1.Init("UC1", null);
            configurableUC1.TriggerAdv += ConfigurableUC1_TriggerAdv;
            Theme.Current.ApplyStd(this);
            configurableUC1.Themed();
        }

        private void ConfigurableUC1_TriggerAdv(string arg1, string arg2, object arg3, object arg4, object arg5)
        {
            extRichTextBox1.Text += $"Trig {arg1} {arg2}" + Environment.NewLine;
            extRichTextBox1.Select(extRichTextBox1.Text.Length, extRichTextBox1.Text.Length);
            extRichTextBox1.ScrollToCaret();
        }

        private void extButtonResize_Click(object sender, EventArgs e)
        {
            this.Bounds = new Rectangle(100, 100, 1000, 1000);
        }

        int addpos = 0;
        private void extButtonAdd_Click(object sender, EventArgs e)
        {
            configurableUC1.Entries.Add(new ConfigurableEntryList.Entry("BA" + addpos, typeof(ExtButton), "+" + addpos.ToString(), new Point(200, addpos), new Size(99, 24), "N1"));
            addpos += 50;
            configurableUC1.UpdateEntries();

        }
    }

}
