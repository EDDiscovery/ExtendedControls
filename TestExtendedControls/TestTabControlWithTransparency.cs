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
    public partial class TestTabControlWithTransparency : Form
    {
        static List<string> list = new List<string>();
        ThemeList theme;
        int tabno = 0;

        public TestTabControlWithTransparency()
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
            
            tick.Interval = 1000;
            tick.Tick += Tick_Tick;
            tick.Start();

            TransparencyKey = Color.Green;

            dataGridViewColumnHider1.BackgroundColor = TransparencyKey;
            dataGridViewColumnHider1.DefaultCellStyle.BackColor = TransparencyKey;
            dataGridViewColumnHider1.AlternatingRowsDefaultCellStyle.BackColor = TransparencyKey;

            extTabControl1.FlatStyle = FlatStyle.System;
            extTabControl1.TabStyle = new TabStyleAngled();
            extTabControl1.SizeMode = TabSizeMode.Normal;
            extTabControl1.Multiline = true;
        //    extTabControl1.Font = new Font("Ms sans serif", 9);

            for( ; tabno < 15; tabno++ )
            {
                TabPage p = new TabPage();
                p.Text = $"Tab {tabno}";
                extTabControl1.Controls.Add(p);
            }

//            extTabControl1.ForceUpdate();
            System.Diagnostics.Debug.WriteLine($"Item size {extTabControl1.ItemSize} Multiline {extTabControl1.Multiline}");
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

        private void extButtontransparent_Click(object sender, EventArgs e)
        {
            this.BackColor = this.BackColor == TransparencyKey ? Color.Coral : TransparencyKey;
            extTabControl1.Controls[0].BackColor = this.BackColor;
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            TabPage p1 = new TabPage();
            p1.Text = $"Tab {tabno}";
            tabno++;
            extTabControl1.Controls.Add(p1);
            extTabControl1.MinimumTabWidth = extTabControl1.CalculateMinimumTabWidth();
        }

        private void extButton4_Click(object sender, EventArgs e)
        {
            var fnt = new Font("Ms Sans Serif", 12);
            //this.Font = fnt;
            extTabControl1.Font = fnt;
        }

        private void extButton5_Click(object sender, EventArgs e)
        {
            var fnt = new Font("Ms Sans Serif", 16);
            //this.Font = fnt;
            extTabControl1.Font = fnt;
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            var fnt = new Font("Ms Sans Serif", 24);
            //this.Font = fnt;
            extTabControl1.Font = fnt;

        }

        private void extButton6_Click(object sender, EventArgs e)
        {
            extTabControl1.FlatStyle = extTabControl1.FlatStyle == FlatStyle.System ? FlatStyle.Popup : FlatStyle.System;
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            extTabControl1.SizeMode = extTabControl1.SizeMode == TabSizeMode.Normal ? TabSizeMode.Fixed : TabSizeMode.Normal;
            if (extTabControl1.SizeMode == TabSizeMode.Fixed)
                extTabControl1.ItemSize = new Size(64, 32);
            System.Diagnostics.Debug.WriteLine($"Item size {extTabControl1.ItemSize}");
        }

        private void extButton7_Click(object sender, EventArgs e)
        {
            extTabControl1.Multiline = true;

        }

        private void extButton8_Click(object sender, EventArgs e)
        {
            extTabControl1.Multiline = false;

        }

        private void extButton9_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 8;
            Theme.Current.ApplyStd(this);
            System.Diagnostics.Debug.WriteLine($"Tab control themed: Font {extTabControl1.Font}");

        }
        private void extButton10_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 14;
            Theme.Current.ApplyStd(this);
            System.Diagnostics.Debug.WriteLine($"Tab control themed: Font {extTabControl1.Font}");

        }

    }

}
