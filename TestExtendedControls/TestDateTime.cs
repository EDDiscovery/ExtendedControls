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
    public partial class TestDateTime : Form
    {
        ThemeStandard theme;

        public TestDateTime()
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


            extDateTimePicker1.Format = DateTimePickerFormat.Custom;
            extDateTimePicker1.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            extDateTimePicker1.BackColor = Color.Blue;
            extDateTimePicker1.SelectedColor = Color.DarkBlue;
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            CalendarForm frm = new CalendarForm();
            frm.Value = new DateTime(2021, 4, 10);
            //  frm.CloseOnChange = true;
            frm.Selected += (s, ev) => { System.Diagnostics.Debug.WriteLine("Date Sel " + ev); };
            frm.Show();
        }
    }
}
