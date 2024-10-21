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
using QuickJSON;

namespace TestExtendedControls
{
    public partial class TestConfigurableUC3 : Form
    {
        public TestConfigurableUC3()
        {
            InitializeComponent();
            var theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            string sr = BaseUtils.ResourceHelpers.GetResourceAsString(System.Reflection.Assembly.GetExecutingAssembly(), "EDD_License");

            Image bm = BaseUtils.ResourceHelpers.GetResourceAsImage("TestExtendedControls.Resources.Discoveries.png");

            Image bm2 = Properties.Resources.CaptainsLog;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string err = "";
            err += configurableUC1.Entries.Add("\"SP\",splitter,\"\",Dock:Fill,0,0,1,100,\"\",Horizontal,20,48");


            err += configurableUC1.Entries.Add("\"FP\",flowpanel,\"\",Dock:Top,0,0,1,100,\"\",Horizontal,\"255,128,0,0\"");
            err += configurableUC1.Entries.Add("\"BF1\",button,\"BF1\",In:FP,0,5,60,24,\"TipB1\"");
            err += configurableUC1.Entries.Add("\"BF2\",button,\"BF2\",In:FP,Margin:10,10,10,1,0,0,60,24,\"TipB1\"");
            err += configurableUC1.Entries.Add("\"BF3\",button,\"BF3\",In:FP,0,5,60,24,\"TipB1\"");

            err += configurableUC1.Entries.Add("\"B1\",button,\"B1\",In:SP.1,10,10,60,24,\"TipB1\"");
            err += configurableUC1.Entries.Add("\"B3\",button,\"File:c:/code/images/general/mountain.png\",In:SP.1,80,10,32,32,\"TipB1\"");
            err += configurableUC1.Entries.Add("\"C1\",checkbox,\"CB1\",In:SP.1,200,10,60,24,\"TipC1\",1");
            err += configurableUC1.Entries.Add("\"C2\",checkbox,\"File:c:/code/images/general/mountain.png\",In:SP.1,300,10,32,32,\"TipC2\",1");

            err += configurableUC1.Entries.Add("\"B2\",button,\"B2\",In:SP.2,10,10,60,24,\"TipB2\"");
            err += configurableUC1.Entries.Add("\"NB1\",numberboxdouble,\"1.2\",In:SP.2,10,40,100,24,\"NBD1\"");
            err += configurableUC1.Entries.Add("\"BR\",button,\"BR\",In:SP.2,Anchor:Right,600,10,60,24,\"TipB2\"");
            err += configurableUC1.Entries.Add("\"CB\",combobox,\"one\",In:SP.2,300,10,60,24,\"TipCB\",one, \"two, it\",three");
            err += configurableUC1.Entries.Add("\"PRU\",panelrollup,\"\",In:SP.2,5,100,200,50,\"\",0,\"255,0,128,0\"");
            err += configurableUC1.Entries.Add("\"B10\",button,\"B10\",In:PRU,5,5,60,24,\"TipB10\"");

            err += configurableUC1.Entries.Add("\"RTB\",richtextbox,\"Initial text\r\nHere\",In:SP.2,250,100,200,50,\"Rich text box\"");



            err += configurableUC1.Entries.Add("\"B2\",dropdownbutton,\"Resource:TestExtendedControls.Resources.Discoveries.png\",In:SP.2,200,10,32,32,\"TipDDB1\"," +
                                                                    //  \"T3;T4\",1,1,0,1,28,28;" +
                                                                    "\"T3;T4\",1,1,0,0,32,32;" +
                                                                    "(\"T1\",\"Entry 1\",\"TestExtendedControls.Resources.Addtab.png\",\"exc1\",CheckBox)" +
                                                                    ",(\"T2\",\"Entry 2\",\"TestExtendedControls.Resources.BookmarkManager.png\")" +
                                                                    ",(\"T3\",\"Entry 3\",\"-\",\"exc2\",CheckBox)" +
                                                                    ",(\"T4\",\"Entry 4\",\"-\",\"exc2\")" +
                                                                    ",(\"B1\",\"Button 1\",,,Button)" +
                                                                    ",(\"T1;T2\",\"Group 1\",\"-\",\"exc2\",Group)" +
                                                                    ",(\"T3;T4\",\"Group 2\",\"-\",,Group)");
            System.Diagnostics.Debug.Assert(err.Length == 0,err);

            configurableUC1.Init("UC1", null);
            configurableUC1.TriggerAdv += ConfigurableUC1_TriggerAdv;
            Theme.Current.ApplyStd(this);
            configurableUC1.Themed();

            configurableUC1.Entries.Set("SP", "50.1");
            System.Diagnostics.Debug.WriteLine($"split dist {configurableUC1.Get("SP")}");

        }

        private void ConfigurableUC1_TriggerAdv(string arg1, string arg2, object arg3, object arg4)
        {
            extRichTextBox1.Text += $"Trig `{arg1}` `{arg2}` `{arg3}`" + Environment.NewLine;
            if ( arg2 == "B2")
            {
                configurableUC1.AddText("RTB", "More text\r\n");

            }

            extRichTextBox1.Select(extRichTextBox1.Text.Length, extRichTextBox1.Text.Length);
            extRichTextBox1.ScrollToCaret();
        }

        private void extButtonResize_Click(object sender, EventArgs e)
        {
            this.Bounds = new Rectangle(100, 100, 1000, 1000);
        }

        int addpos = 0;
        private void extButtonAdd_Click(object sender, EventArgs e)
        {
            configurableUC1.Entries.Add(new ConfigurableEntryList.Entry("BA" + addpos, typeof(ExtButton), "+" + addpos.ToString(), new Point(200, addpos), new Size(99, 24), "N1"));
            addpos += 50;
            configurableUC1.UpdateEntries();
        }

        int rwno = 10;
        private void extButtonAddRow_Click(object sender, EventArgs e)
        {
            configurableUC1.Entries.AddSetRows("DGV", $"-2,(text,\"R{rwno++} col 1 value\"),(text,\"RA col 2 value\"),(text,\"RA col 3 value\");"
                                                        );

        }

        private void extButtonChangeRow_Click(object sender, EventArgs e)
        {
            configurableUC1.Entries.AddSetRows("DGV", $"2,(text,\"RU col 1 value\"),(text,\"RU col 2 value\"),(text,\"RU col 3 value\");"
                                                        );

        }

        private void extButtonInsRow_Click(object sender, EventArgs e)
        {
            configurableUC1.Entries.AddSetRows("DGV", $"-1,(text,\"R{rwno++} col 1 value\"),(text,\"RI col 2 value\"),(text,\"RI col 3 value\");"
                                                        );

        }

        private void extButtonChgRowJ_Click(object sender, EventArgs e)
        {
            JSONFormatter jf = new JSONFormatter();
            jf.Array()
                .Object().V("Row", 3)
                         .Array("Cells")
                                .Object().V("Cell",2).V("Type", "Text").V("Value", "RCHG1 C3").V("ToolTip","Tool tip RCHG1").Close()
                         .Close()
            .Close();

            configurableUC1.Entries.AddSetRows("DGV", jf.Get());
        }

        private void extButtonClear_Click(object sender, EventArgs e)
        {
            configurableUC1.Entries.Clear("DGV");
        }

        private void extButtonChgCell_Click(object sender, EventArgs e)
        {
            configurableUC1.Entries.AddSetRows("DGV", $"2,1,(text,\"Change this cell\")");

        }

        private void extButtonRemove12_Click(object sender, EventArgs e)
        {
            configurableUC1.Entries.RemoveRows("DGV", 1,2);
        }
    }

}
