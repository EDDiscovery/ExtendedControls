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
        ThemeList theme;

        public TestDateTime()
        {
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            Theme.Current.FontName = "Microsoft Sans Serif";
            Theme.Current.FontSize = 8.25f;
            Theme.Current.WindowsFrame = true;

            InitializeComponent();

            Theme.Current.ApplyStd(this);


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
