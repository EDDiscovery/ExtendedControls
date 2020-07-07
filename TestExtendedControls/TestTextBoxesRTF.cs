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
    public partial class TestTextBoxesRTF : Form
    {
        ThemeStandard theme;

        // proving you can't control RTF for scroll bar purposes.

        public TestTextBoxesRTF()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            //theme.SetThemeByName("Elite EuroCaps");
            theme.SetThemeByName("Elite Verdana");
            //theme.FontName = "Microsoft Sans Serif";
            //theme.FontName = "Arial";
            //theme.FontName = "Euro Caps";
            theme.FontSize = 14f;
            theme.WindowsFrame = true;

            InitializeComponent();

            string tx = "";
            for (int i = 0; i < 20; i++)
                tx = tx.AppendPrePad(i.ToStringInvariant("0000") + ":" + " Here is some text", Environment.NewLine);
            extRichTextBox1.Text = tx;

            var x = Properties.Resources.EDD_License;
            extRichTextBox1.Rtf = x;

            theme.ApplyStd(this);


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
    }
}
