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
    public partial class TestPanelScrollDGV : Form
    {
        ThemeList theme;

        public TestPanelScrollDGV()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            CreateDGVInPS(50);
            CreateDGV(500);
        }

        void CreateDGV(int vpos)
        {
            DataGridView dgv = new DataGridView();
            dgv.Bounds = new Rectangle(20, vpos, 500, 200);
            dgv.Columns.Add(new DataGridViewTextBoxColumn());
            dgv.Columns.Add(new DataGridViewTextBoxColumn());
            dgv.Columns.Add(new DataGridViewTextBoxColumn());
            extPanelScroll1.Controls.Add(dgv);

            for (int i = 0; i < 30; i++)
            {
                DataGridViewRow row = dgv.RowTemplate.Clone() as DataGridViewRow;
                row.CreateCells(dgv, i.ToString(), "DGV Only", "Three");
                dgv.Rows.Add(row);
            }
        }

        // in a panel scroll, the mouse wheel is ate by the PS and thus does not go to the outer panel
        void CreateDGVInPS(int vpos)
        {
            ExtPanelDataGridViewScroll ps = new ExtPanelDataGridViewScroll();
            ps.Bounds = new Rectangle(20, vpos, 500, 200);

            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.Columns.Add(new DataGridViewTextBoxColumn());
            dgv.Columns.Add(new DataGridViewTextBoxColumn());
            dgv.Columns.Add(new DataGridViewTextBoxColumn());
            dgv.ScrollBars = ScrollBars.None;
            extPanelScroll1.Controls.Add(dgv);

            for (int i = 0; i < 30; i++)
            {
                DataGridViewRow row = dgv.RowTemplate.Clone() as DataGridViewRow;
                row.CreateCells(dgv, i.ToString(), "two", "Three");
                dgv.Rows.Add(row);
            }

            ps.Controls.Add(dgv);
            ExtScrollBar sb = new ExtScrollBar();
            ps.Controls.Add(sb);

            extPanelScroll1.Controls.Add(ps);
        }



        private void extButton1_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            Theme.Current.ApplyStd(this);
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 20;
            Theme.Current.ApplyStd(this);
        }

        class DataGridViewSpecial : DataGridView
        {
            protected override void OnMouseWheel(MouseEventArgs e)
            {
                System.Diagnostics.Debug.WriteLine("Mouse wheel in DGV Special");
                base.OnMouseWheel(e);
            }
        }
    }
}
