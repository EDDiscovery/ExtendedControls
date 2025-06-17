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
    public partial class TestTreeView : Form
    {
        ThemeList theme;

        public TestTreeView()
        {
            InitializeComponent();

            AddTreeList("Top", "@", new string[] { DateTime.Now.ToString() }, 'Y');

            for (int i = 0; i < 50; i++)
            {
                AddTreeList($"N{i}", $"{i}", new string[] { $"one-{i}", "two", "three", "four" }, i < 10 ? 'N' : 'Y');

            }

            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            Theme.Current.Apply(this);
        }

        TreeNode AddTreeList(string parentid, string parenttext, string[] children, char ce)
        {
            TreeNode[] parents = extTreeView1.Nodes.Find(parentid, false);                     // find parent id in tree
            TreeNode pnode = (parents.Length == 0) ? (extTreeView1.Nodes.Add(parentid, parenttext)) : parents[0];  // if not found, add it, else get it

            if (pnode.Nodes.Count > 0)          // defend against nodes coming and going so we don't show bad data from previous searches
            {
                if (pnode.Nodes.Count != children.Length)   // different length
                {
                    pnode.Nodes.Clear();
                }
                else
                {
                    int exno = 0;
                    foreach (TreeNode cn in pnode.Nodes)        // check IDs are in the same order
                    {
                        string childid = parentid + "-" + (exno++).ToString();       // make up a child id
                        if (cn.Name != childid)
                        {
                            pnode.Nodes.Clear();
                            break;
                        }
                    }
                }
            }

            int eno = 0;
            foreach (var childtext in children)
            {
                string childid = parentid + "-" + (eno++).ToString();       // make up a child id
                TreeNode[] childs = pnode.Nodes.Find(childid, false);       // find it..
                if (childs.Length > 0)                                      // found
                    childs[0].Text = childtext;                             // reset text
                else
                    pnode.Nodes.Add(childid, childtext);                    // else set the text
            }

            if (ce == 'Y')
                pnode.Expand();

            return pnode;
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            Theme.Current.SkinnyScrollBars = true;
            Theme.Current.Apply(this);
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            Theme.Current.SkinnyScrollBars = false;
            Theme.Current.Apply(this);

        }
    }

}
