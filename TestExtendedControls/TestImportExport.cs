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
    public partial class TestImportExport : Form
    {

        ThemeList theme;

        public TestImportExport()
        {
            InitializeComponent();
            //theme = new ThemeList();
            //theme.LoadBaseThemes();
            //theme.SetThemeByName("Elite Verdana");
            //Theme.Current.WindowsFrame = true;

            BaseUtils.Translator.Instance.AddExcludedControls(new Type[]
             {   typeof(ExtendedControls.ExtComboBox), typeof(ExtendedControls.NumberBoxDouble),typeof(ExtendedControls.NumberBoxFloat),typeof(ExtendedControls.NumberBoxLong),
                typeof(ExtendedControls.ExtScrollBar),typeof(ExtendedControls.ExtStatusStrip),typeof(ExtendedControls.ExtRichTextBox),typeof(ExtendedControls.ExtTextBox),
                typeof(ExtendedControls.ExtTextBoxAutoComplete),typeof(ExtendedControls.ExtDateTimePicker),typeof(ExtendedControls.ExtNumericUpDown),
                typeof(ExtendedControls.MultiPipControl)});

        }

        private void extButtonExport_Click(object sender, EventArgs e)
        {
            ImportExportForm frm = new ImportExportForm();
            frm.Export(new string[] { "Export Current View" }, new ImportExportForm.ShowFlags[] { ImportExportForm.ShowFlags.ShowCSVOpenInclude });

            if (frm.ShowDialog(this.FindForm()) == DialogResult.OK)
            {

            }
        }

        private void extButtonExportDT_Click(object sender, EventArgs e)
        {
            ImportExportForm frm = new ImportExportForm();
            frm.Export(new string[] { "Export Current View" }, new ImportExportForm.ShowFlags[] { ImportExportForm.ShowFlags.ShowDateTimeCSVOpenInclude });

            if (frm.ShowDialog(this.FindForm()) == DialogResult.OK)
            {

            }


        }

        private void extButtonExportDTUTC_Click(object sender, EventArgs e)
        {
            ImportExportForm frm = new ImportExportForm();
            frm.Export(new string[] { "Export Current View" }, new ImportExportForm.ShowFlags[] { ImportExportForm.ShowFlags.ShowDateTimeUTCCSVOpenInclude });

            if (frm.ShowDialog(this.FindForm()) == DialogResult.OK)
            {

            }

        }

        private void extButtonExportMin_Click(object sender, EventArgs e)
        {

            ImportExportForm frm = new ImportExportForm();
            frm.Export(new string[] { "Export Current View" }, new ImportExportForm.ShowFlags[] { ImportExportForm.ShowFlags.None });

            if (frm.ShowDialog(this.FindForm()) == DialogResult.OK)
            {

            }

        }

        private void extButtonImportJson_Click(object sender, EventArgs e)
        {
            ImportExportForm frm = new ImportExportForm();

            frm.Import(new string[] { "SLEF - Loadout from EDSY or journal loadout event" },
               new ImportExportForm.ShowFlags[] { ImportExportForm.ShowFlags.ShowPaste },
               new string[] { "JSON|*.json" }
              );

            if (frm.ShowDialog(FindForm()) == DialogResult.OK)
            {

            }

        }

        private void extButtonImportSaveAs_Click(object sender, EventArgs e)
        {
            ImportExportForm frm = new ImportExportForm();

            frm.Import(new string[] { "SLEF - Loadout from EDSY or journal loadout event" },
               new ImportExportForm.ShowFlags[] { ImportExportForm.ShowFlags.ShowPaste | ImportExportForm.ShowFlags.ShowImportSaveas},
               new string[] { "JSON|*.json" }
              );

            if (frm.ShowDialog(FindForm()) == DialogResult.OK)
            {

            }

        }
    }
}