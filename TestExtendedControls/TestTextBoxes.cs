using EDDiscovery2;
using ExtendedControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogTest
{
    public partial class TestTextBoxes : Form
    {
        ThemeStandard theme;

        public TestTextBoxes()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontName = "Microsoft Sans Serif";
            theme.FontSize = 8.25f;
            theme.WindowsFrame = true;

            InitializeComponent();

            theme.ApplyStd(this);

            textBoxDouble1.FormatCulture = CultureInfo.GetCultureInfo("en-gb");
            textBoxDouble2.FormatCulture = CultureInfo.GetCultureInfo("fr");
            numberBoxLong1.FormatCulture = CultureInfo.GetCultureInfo("en-gb");

            numberBoxLong1.Minimum = 1000;
            numberBoxLong1.Maximum = 2000;
            numberBoxLong1.ValueNoChange = 1100;

            numberBoxLong2.Minimum = 1000;
            numberBoxLong2.Maximum = 2000;
            numberBoxLong2.ValueNoChange = 1400;
            numberBoxLong2.SetComparitor(numberBoxLong1, 2);

            textBoxDouble1.Minimum = -20.0;
            textBoxDouble1.Maximum = 20.0;
            textBoxDouble1.ValueNoChange = 1.1;

            textBoxDouble2.Minimum = 10.0;
            textBoxDouble2.Maximum = 20.0;
            textBoxDouble2.ValueNoChange = 12.1;


            extRichTextBox1.HideScrollBar = true;

            extNumericUpDown1.AutoSize = true;
            extNumericUpDown1.Minimum = -100;

            commanders = new List<EDDiscovery2.EDCommander>();
            commanders.Add(new EDCommander(-1, "Hidden log", ""));
            commanders.Add(new EDCommander(1, "Robby1", ""));
            commanders.Add(new EDCommander(2, "Robby2", ""));
            commanders.Add(new EDCommander(3, "Robby3", ""));
            commanders.Add(new EDCommander(4, "Robby4", ""));
            commanders.Add(new EDCommander(6, "Robby6", ""));
            commanders.Add(new EDCommander(7, "Robby7", ""));
            commanders.Add(new EDCommander(8, "Robby8", ""));
            commanders.Add(new EDCommander(9, "Robby9", ""));
            commanders.Add(new EDCommander(10, "Robby10", ""));
            commanders.Add(new EDCommander(11, "Robby11", ""));

            extComboBox1.DataSource = commanders;
            extComboBox1.DisplayMember = "Name";
            extComboBox1.ValueMember = "Nr";
            extComboBox1.FlatStyle = FlatStyle.Popup;
            extComboBox1.Repaint();

            commanders2 = new List<EDDiscovery2.EDCommander>();
            commanders2.Add(new EDCommander(-1, "2Hidden log", ""));
            commanders2.Add(new EDCommander(1, "2Robby1", ""));
            commanders2.Add(new EDCommander(2, "2Robby2", ""));
            commanders2.Add(new EDCommander(3, "2Robby3", ""));
            commanders2.Add(new EDCommander(4, "2Robby4", ""));
            commanders2.Add(new EDCommander(6, "2Robby6", ""));
            commanders2.Add(new EDCommander(7, "2Robby7", ""));
            commanders2.Add(new EDCommander(8, "2Robby8", ""));
            commanders2.Add(new EDCommander(9, "2Robby9", ""));
            commanders2.Add(new EDCommander(10, "2Robby10", ""));
            commanders2.Add(new EDCommander(11, "2Robby11", ""));


            extComboBox2.DataSource = commanders2;
            extComboBox2.DisplayMember = "Name";
            extComboBox2.ValueMember = "Nr";
            extComboBox2.FlatStyle = FlatStyle.System;
            extComboBox2.Repaint();



        }

        List<EDDiscovery2.EDCommander> commanders = null;
        List<EDDiscovery2.EDCommander> commanders2 = null;


        private void numberBoxLong1_ValueChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("1. Long is " + numberBoxLong1.Value);
        }

        private void numberBoxLong2_ValueChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("2. Long is " + numberBoxLong2.Value);

        }

        int count = 0;

        private void extButton1_Click(object sender, EventArgs e)
        {
            if ((count % 10) == 5)
            {
                extRichTextBox1.AppendText("Add this very long wrapping line on " + count + Environment.NewLine, (count % 2 == 0) ? Color.Red : Color.Blue);
                count++;
            }
            else
                extRichTextBox1.AppendText("Add this on " + count + Environment.NewLine, (count % 2 == 0) ? Color.Red : Color.Blue);
            count++;

        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            extRichTextBox1.Clear();

        }

        private void extNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Value changed Custom numeric " + extNumericUpDown1.Value);
        }
    }
}
