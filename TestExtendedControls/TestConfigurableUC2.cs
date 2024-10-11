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
    public partial class TestConfigurableUC2 : Form
    {
        public TestConfigurableUC2()
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
            //err += configurableUC1.Entries.Add("\"P2\",panel,\"\",Dock:Fill,0,0,1,1,\"\",\"100,64,64,128\"");
            //err += configurableUC1.Entries.Add("\"P1\",panel,\"\",5,5,500,200,\"\",\"blue\"");
            //err += configurableUC1.Entries.Add("\"P1\",panel,\"\",5,5,500,200,\"\"");
            //err += configurableUC1.Entries.Add("\"B1\",button,\"B1\",In:P1,10,10,100,24,\"TipB1\"");

            err += configurableUC1.Entries.Add("\"DGV\",dgv,\"\",Dock:Fill,10,10,10,10,\"TipDGV\",100;(text,\"Column 1\",100),(text,\"Column 2\",200),(text,\"Column 3\",300)");
            err += configurableUC1.Entries.Add("\"P1\",panel,\"\",Dock:Top,0,0,1,100,\"\",\"100,64,128,64\"");


            err += configurableUC1.Entries.Add("\"B1\",button,\"B1\",In:P1,10,10,60,24,\"TipB1\"");
            err += configurableUC1.Entries.Add("\"B3\",button,\"File:c:/code/images/general/mountain.png\",In:P1,80,10,32,32,\"TipB1\"");
            err += configurableUC1.Entries.Add("\"B4\",button,\"File:c:/code/images/general/mountain.png\",In:P1,500,10,48,48,\"TipB1\"");
            err += configurableUC1.Entries.Add("\"B2\",dropdownbutton,\"Resource:TestExtendedControls.Resources.Discoveries.png\",In:P1,200,10,32,32,\"TipDDB1\"," +
                                                                    //  \"T3;T4\",1,1,0,1,28,28;" +
                                                                    "\"T3;T4\",1,1,0,0,32,32;" +
                                                                    "(\"T1\",\"Entry 1\",\"TestExtendedControls.Resources.Addtab.png\",\"exc1\",CheckBox)" +
                                                                    ",(\"T2\",\"Entry 2\",\"TestExtendedControls.Resources.BookmarkManager.png\")" + 
                                                                    ",(\"T3\",\"Entry 3\",\"-\",\"exc2\",CheckBox)" +
                                                                    ",(\"T4\",\"Entry 4\",\"-\",\"exc2\")" +
                                                                    ",(\"B1\",\"Button 1\",,,Button)" +
                                                                    ",(\"T1;T2\",\"Group 1\",\"-\",\"exc2\",Group)" + 
                                                                    ",(\"T3;T4\",\"Group 2\",\"-\",,Group)"
                                                                    );
            //err += configurableUC1.Entries.Add("\"B2\",button,\"B2\",10,105,100,24,\"TipB2\"");
            //err += configurableUC1.Entries.Add("\"B3\",button,\"B3\",10,600,100,24,\"TipB2\"");
            System.Diagnostics.Debug.Assert(err.Length == 0,err);
            configurableUC1.Init("UC1", null);
            configurableUC1.Trigger += ConfigurableUC1_Trigger;
            Theme.Current.ApplyStd(this);
            configurableUC1.Themed();

            err = configurableUC1.Entries.AddSetRows("DGV", "-2,(text,\"R1 col 1 value\",\"text Tooltip\"),(text,\"R1 col 2 value\"),(text,\"R1 col 3 value\");" +
                                                      "-2,(text,\"R2 col 1 value\"),(text,\"R2 col 2 value\")"
                                                        );
            System.Diagnostics.Debug.Assert(err == null);

            JSONFormatter jf = new JSONFormatter();
            jf.Array()
                .Object().V("Row", -1)
                         .Array("Cells")
                                .Object().V("Type", "Text").V("Value", "RJ1 C1").V("ToolTip","Tooltip JSON insert").Close()
                                .Object().V("Type", "Text").V("Value", "RJ1 C2").Close()
                                .Object().V("Type", "Text").V("Value", "RJ1 C3").Close()
                         .Close()
                .Close()
                .Object().V("Row", -2)
                         .Array("Cells")
                                .Object().V("Type", "Text").V("Value", "RI1 C1").Close()
                                .Object().V("Type", "Text").V("Value", "RI1 C2").Close()
                                .Object().V("Type", "Text").V("Value", "RI1 C3").Close()
                         .Close()
            .Close();

            string json = jf.Get();
            var tk = JArray.Parse(json);
            System.Diagnostics.Debug.WriteLine($"Json {tk.ToString(true)}");


            configurableUC1.Entries.AddSetRows("DGV", json);

        }

        private void ConfigurableUC1_Trigger(string arg1, string arg2, object arg3)
        {
            extRichTextBox1.Text += $"Trig {arg1} {arg2}" + Environment.NewLine;
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
