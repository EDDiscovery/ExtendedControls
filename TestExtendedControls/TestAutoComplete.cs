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
    public partial class TestAutoComplete : Form
    {
        static List<string> list = new List<string>();
        ThemeList theme;

        public TestAutoComplete()
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
            for (int i = 0; i < 100; i++)
                list.Add($"Item {i}");

            autoCompleteTextBox1.SetAutoCompletor(AutoList);
            autoCompleteTextBox1.EndButtonVisible = true;
            autoCompleteTextBox1.ReturnPressed += (box) => { System.Diagnostics.Debug.WriteLine($"Return pressed {autoCompleteTextBox1.Text}"); return true; };
            autoCompleteTextBox2.SetAutoCompletor(AutoList);
            autoCompleteTextBox2.FlatStyle = FlatStyle.Popup;
            autoCompleteTextBox2.AutoCompleteTimeout = 1000;

            extTextBoxAutoComplete1.SetAutoCompletor(AutoList);
            extTextBoxAutoComplete1.EndButtonVisible = true;

            comboBoxCustom1.Items.AddRange(list);

            for (int i = 0; i < 5; i++)
                dataGridViewColumnHider1.Rows.Add("", "");

            Column1.AutoCompleteGenerator = ReturnSystemAutoCompleteListDGV;
            dataGridViewColumnHider1.PreviewKeyDown += DataGridViewColumnHider1_PreviewKeyDown;
            Theme.Current.ApplyStd(this);
            tick.Interval = 1000;
            tick.Tick += Tick_Tick;
            tick.Start();

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

        public static void AutoList(string input, ExtTextBoxAutoComplete t, SortedSet<string> set)
        {
            var res = (from x in list where x.StartsWith(input, StringComparison.InvariantCultureIgnoreCase) select x).ToList();
            SortedSet<string> ss = new SortedSet<string>();
            foreach (var x in res)
                set.Add(x);

          //  System.Threading.Thread.Sleep(2000);
        }

        public static void ReturnSystemAutoCompleteListDGV(string input, Object ctrl, SortedSet<string> set)
        {
            List<string> res = (from x in list where x.StartsWith(input, StringComparison.InvariantCultureIgnoreCase) select x).ToList();
            foreach (var x in res)
                set.Add(x);

          //  System.Threading.Thread.Sleep(2000);
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            autoCompleteTextBox1.ClearOnFirstChar = true;

        }
    }

}
