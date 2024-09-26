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
                configurableUC1.Add(new ConfigurableUC.Entry("B" + i, typeof(ExtButton), i.ToString(), new Point(100, i), new Size(50, 24), "N1"));
            }
            configurableUC1.Init("Has caption", "UC1", null, true);
            configurableUC1.AddEntries();
            configurableUC1.Trigger += ConfigurableUC1_Trigger;
            Theme.Current.ApplyStd(this);
//            configurableUC1.AutoScaleMode = AutoScaleMode.
            configurableUC1.Themed();
        }

        private void ConfigurableUC1_Trigger(string arg1, string arg2, object arg3)
        {
            extRichTextBox1.Text += $"Trig {arg1} {arg2}" + Environment.NewLine;
            extRichTextBox1.Select(extRichTextBox1.Text.Length, extRichTextBox1.Text.Length);
            extRichTextBox1.ScrollToCaret();
        }

        private void extButtonResize_Click(object sender, EventArgs e)
        {
            this.Bounds = new Rectangle(100, 100, 1000, 1000);
        }
    }

}
