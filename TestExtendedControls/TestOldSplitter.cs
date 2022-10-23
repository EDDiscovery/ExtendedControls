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
    public partial class TestOldSplitter : Form
    {
        public TestOldSplitter()
        {
            InitializeComponent();

            extSplitterResizeParent1.MaxSize = 200;
            extSplitterResizeParent2.MaxSize = 800;
            extSplitterResizeParent3.MaxSize = 800;
            extSplitterResizeParent4.MaxSize = 200;

        }
    }
}
