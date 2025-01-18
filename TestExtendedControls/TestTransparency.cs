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
    public partial class TestTransparency : Form
    {
        static List<string> list = new List<string>();
        ThemeList theme;

        public TestTransparency()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            list.Add("one");
            list.Add("only");
            list.Add("onynx");
            list.Add("two");
            list.Add("three");
            list.Add("four");
            list.Add("five");
            list.Add("Aone");
            list.Add("Btwo");
            list.Add("Cthree");
            list.Add("Dfour");
            list.Add("Efive");

            for (int i = 0; i < 5; i++)
                dataGridViewColumnHider1.Rows.Add("", "");

            Column1.AutoCompleteGenerator = ReturnSystemAutoCompleteListDGV;
            dataGridViewColumnHider1.PreviewKeyDown += DataGridViewColumnHider1_PreviewKeyDown;
            Theme.Current.ApplyStd(this);
            tick.Interval = 1000;
            tick.Tick += Tick_Tick;
            tick.Start();

            TransparencyKey = Color.Green;
            this.BackColor = TransparencyKey;

            dataGridViewColumnHider1.BackgroundColor = TransparencyKey;
            dataGridViewColumnHider1.DefaultCellStyle.BackColor = TransparencyKey;
            dataGridViewColumnHider1.AlternatingRowsDefaultCellStyle.BackColor = TransparencyKey;

            tabPage1.BackColor = TransparencyKey;
            tabPage3.BackColor = TransparencyKey;
        }

        private void Tick_Tick(object sender, EventArgs e)
        {
            var ac = ActiveControl;
            //System.Diagnostics.Debug.WriteLine($"Focus is {ac.Name} {ac.Text}");
        }

        Timer tick = new Timer();
        private void DataGridViewColumnHider1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("TAC Key down" + e.KeyValue);
        }

        public static void ReturnSystemAutoCompleteListDGV(string input, Object ctrl, SortedSet<string> set)
        {
            List<string> res = (from x in list where x.StartsWith(input, StringComparison.InvariantCultureIgnoreCase) select x).ToList();
            foreach (var x in res)
                set.Add(x);

          //  System.Threading.Thread.Sleep(2000);
        }

        private void extButtonBorder_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle == FormBorderStyle.None ? FormBorderStyle.Sizable : FormBorderStyle.None;
        }
    }

}
