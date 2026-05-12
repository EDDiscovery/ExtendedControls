using AudioExtensions;
using BaseUtils;
using ExtendedAudioForms;
using ExtendedConditionsForms;
using ExtendedControls;
using ExtendedForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExtendedControls
{
    public partial class TestFontForm : Form
    {
        ThemeList theme;

        private PrivateFontCollection PrivateFonts = new PrivateFontCollection();

        public TestFontForm()
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("de");
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.FontSize = 12;

            //FontHandler.AddFileFont(@"c:\code\images\fonts\ZenDots-Regular.ttf");
            //selectedfnt = FontHandler.GetFont("Zen Dots", 12);

            PrivateFonts.AddFontFile(@"c:\code\images\fonts\ZenDots-Regular.ttf");
            selectedfnt = new Font(PrivateFonts.Families[0], 12);

            for (int i = 0; i < 100; i++)
            {
                DataGridViewRow row = dataGridView1.RowTemplate.Clone() as DataGridViewRow;
                row.CreateCells(dataGridView1, i.ToString(), (100 + i).ToString(), (10000 - i).ToString());
                row.Tag = i;
                dataGridView1.Rows.Add(row);
            }

            Set();
        }


        void Set()
        {
            richTextBox1.Font = selectedfnt;
            labelFont.Text = selectedfnt.Name + " " + selectedfnt.Size.ToString();
            dataGridView1.Font = selectedfnt;
            richTextBox1.Clear();
            richTextBox1.Text = "the lazy old dog" + Environment.NewLine + "Jumped over the cow " + richTextBox1.Font.Name;
        }

        Font selectedfnt; 

        private void extButton20_Click(object sender, EventArgs e)
        {
            var frm = new BaseUtils.FontDialog();
            frm.Set(selectedfnt);
            if ( frm.ShowDialog() == DialogResult.OK )
            {
             //   MessageBox.Show($"Font selected {frm.SelectedFont} {frm.SelectedSize} {frm.SelectedStyle}");
                selectedfnt = frm.GetFont();
                Set();
            }

        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            selectedfnt = new Font(PrivateFonts.Families[0], 12);

            richTextBox1.Font = selectedfnt;
            richTextBox1.SelectionFont = selectedfnt;
            richTextBox1.Select(0, 0);
            richTextBox1.Clear();
            richTextBox1.Text = "the lazy old dog" + Environment.NewLine + "Jumped over the cow " + richTextBox1.Font.Name;

//            Set();
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            selectedfnt = new Font("Arial", 12);
            Set();

        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Rich text font {richTextBox1.Font}");
        }
    }
}
