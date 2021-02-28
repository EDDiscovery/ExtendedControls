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

namespace TestExtendedControls
{
    public partial class TestTextBoxes : Form
    {
        ThemeStandard theme;

        public TestTextBoxes()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            //theme.SetThemeByName("Elite EuroCaps");
            theme.SetThemeByName("Elite Verdana");
            //theme.FontName = "Microsoft Sans Serif";
            //theme.FontName = "Arial";
            //theme.FontName = "Euro Caps";
            //theme.FontSize = 20f;
          //  theme.WindowsFrame = true;

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

            string tx = "";
            for (int i = 0; i < 200; i++)
                tx = tx.AppendPrePad(i.ToStringInvariant("0000") + ":" + " Here is some text", Environment.NewLine);
            extRichTextBox1.Text = tx;

            extComboBoxFontSize.Items = new string[] { "8", "10", "12", "14", "16", "18", "20", "22", "24" };
            extComboBoxFont.Items = new string[] { "Arial","MS Sans Serif","Euro Caps" };
        }

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

        private void TestTextBoxes_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            using (Pen p = new Pen(Color.Green))
            {
                for (int i = 0; i < 400; i += 2)
                {
                    e.Graphics.DrawLine(p, new Point(extRichTextBox1.Left - 10 - i % 8, extRichTextBox1.Top + i),
                                        new Point(extRichTextBox1.Left - 2, extRichTextBox1.Top + i));

                    if (i % 8 == 0)
                        e.Graphics.DrawLine(p, new Point(extRichTextBox1.Left - 20, extRichTextBox1.Top + i),
                                            new Point(extRichTextBox1.Left - 18, extRichTextBox1.Top + i));
                    if (i % 16 == 0)
                        e.Graphics.DrawLine(p, new Point(extRichTextBox1.Left - 24, extRichTextBox1.Top + i),
                                            new Point(extRichTextBox1.Left - 22, extRichTextBox1.Top + i));
                }

            }
        }

        string font = "Arial";
        int fontsize = 8;

        private void extComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fontsize = ((string)extComboBoxFontSize.SelectedItem).InvariantParseInt(10);
            extRichTextBox1.Font = new Font(font, fontsize);
        }

        private void extComboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            font = (string)extComboBoxFont.SelectedItem;
            extRichTextBox1.Font = new Font(font, fontsize);

        }
    }
}
