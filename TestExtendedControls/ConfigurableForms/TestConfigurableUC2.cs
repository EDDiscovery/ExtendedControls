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
            err += configurableUC1.Entries.Add("\"P1\",panel,\"\",Dock:Top,0,0,1,200,\"\",\"100,64,1,1\"");


            err += configurableUC1.Entries.Add("\"B1\",button,\"B1\",In:P1,10,10,60,24,\"TipB1\"");
            err += configurableUC1.Entries.Add("\"B3\",button,\"File:c:/code/images/general/mountain.png\",In:P1,80,10,32,32,\"Mountain\"");
            err += configurableUC1.Entries.Add("\"B4\",button,\"File:c:/code/images/general/mountain.png\",In:P1,4,40,48,48,\"Mountain 2\"");
            err += configurableUC1.Entries.Add("\"RB1\",radiobutton,\"RB1\",In:P1,350,10,60,24,\"Radio B1\"");
            err += configurableUC1.Entries.Add("\"RB2\",radiobutton,\"RB2\",In:P1,350,40,60,24,\"Radio B2\"");
            err += configurableUC1.Entries.Add("\"NUD1\",numericupdown,\"20\",In:P1,350,70,120,32,\"Numeric up down\",0,200");
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
            configurableUC1.TriggerAdv += ConfigurableUC1_TriggerAdv;
            Theme.Current.ApplyStd(this);
            configurableUC1.Themed();

            err = configurableUC1.Entries.AddSetRows("DGV", "-2,0,\"HeaderT\",(text,\"R1 col 1 value\",\"text Tooltip\"),(text,\"R1 col 2 value\"),(text,\"R1 col 3 value\");" +
                                                      "-2,(text,\"R2 col 1 value\"),(text,\"R2 col 2 value\")"
                                                        );
            err = configurableUC1.Entries.AddSetRows("DGV", "1,0,\"Header2\"");     // overwrite row 1 header
            err = configurableUC1.Entries.AddSetRows("DGV", "-2,0,\"Header3\"");    // header only on next line

            configurableUC1.SetDGVSettings("DGV", true, true, true, true);

            System.Diagnostics.Debug.Assert(err == null);

            configurableUC1.SetRightClickMenu("DGV", new string[] { "RCT1", "RCT2" }, new string[] { "Right click 1", "Right click 2" });

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

        private void ConfigurableUC1_TriggerAdv(string arg1, string arg2, object arg3, object arg4, object arg5)
        {
            extRichTextBox1.Text += $"Trig `{arg1}` `{arg2}` `{arg3}`" + Environment.NewLine;
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
            configurableUC1.Entries.Add(new ConfigurableEntryList.Entry("BA" + addpos, typeof(ExtButton), "+" + addpos.ToString(), new Point(250, addpos), new Size(99, 24), "N1") { InPanel = "P1"});
            addpos += 40;
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

        private void extButtonRemoveCol_Click(object sender, EventArgs e)
        {
            configurableUC1.RemoveColumns("DGV", 1, 2);
        }

        private void extButtonAddColumn_Click(object sender, EventArgs e)
        {
            configurableUC1.InsertColumn("DGV", 1, "text", "InsertCol", 100, "Alpha");
        }

        private void extButtonSave_Click(object sender, EventArgs e)
        {
            object settings = configurableUC1.GetDGVColumnSettings("DGV");
            JToken tk = settings as JToken;
            BaseUtils.FileHelpers.TryWriteToFile(@"c:\code\colset.json", tk.ToString(true));
        }

        private void extButtonLoad_Click(object sender, EventArgs e)
        {
            string s = BaseUtils.FileHelpers.TryReadAllTextFromFile(@"c:\code\colset.json");
            if ( s != null )
            {
                JToken tk = JToken.Parse(s);
                configurableUC1.SetDGVColumnSettings("DGV", tk);
            }
        }

        bool wordwrap = true;
        private void extButtonToggleWordWrap_Click(object sender, EventArgs e)
        {
            wordwrap = !wordwrap;
            configurableUC1.SetWordWrap("DGV", wordwrap);

        }
    }

}
