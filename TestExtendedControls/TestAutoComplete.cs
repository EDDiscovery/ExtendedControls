﻿using ExtendedControls;
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
        ThemeStandard theme;

        public TestAutoComplete()
        {
            InitializeComponent();
            theme = new ThemeStandard();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            theme.WindowsFrame = true;
            ExtendedControls.ThemeableFormsInstance.Instance = theme;

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

            autoCompleteTextBox1.SetAutoCompletor(AutoList);
            autoCompleteTextBox1.KeyUp += AutoCompleteTextBox1_KeyUp;
            autoCompleteTextBox2.SetAutoCompletor(AutoList);
            autoCompleteTextBox2.FlatStyle = FlatStyle.Popup;
            autoCompleteTextBox2.KeyUp += AutoCompleteTextBox2_KeyUp;

            comboBoxCustom1.Items.AddRange(list);

            for (int i = 0; i < 5; i++)
                dataGridViewColumnHider1.Rows.Add("", "");

            Column1.AutoCompleteGenerator = ReturnSystemAutoCompleteListDGV;
            dataGridViewColumnHider1.PreviewKeyDown += DataGridViewColumnHider1_PreviewKeyDown;
            theme.ApplyStd(this);
        }

        private void DataGridViewColumnHider1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Key down" + e.KeyValue);
        }

        private void AutoCompleteTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Answer " + autoCompleteTextBox2.Text);
        }

        private void AutoCompleteTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Answer " + autoCompleteTextBox1.Text);
        }

        public static List<string> AutoList(string input, ExtTextBoxAutoComplete t)
        {
            List<string> res = (from x in list where x.StartsWith(input, StringComparison.InvariantCultureIgnoreCase) select x).ToList();
            return res;
        }

        public static List<string> ReturnSystemAutoCompleteListDGV(string input, Object ctrl)
        {
            List<string> res = (from x in list where x.StartsWith(input, StringComparison.InvariantCultureIgnoreCase) select x).ToList();
            return res;
        }

    }

}
