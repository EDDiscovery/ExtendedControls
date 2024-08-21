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
    public partial class TestCheckedIconNewListBoxForm : Form
    {
        static List<string> list = new List<string>();
        ThemeList theme;

        string buttonwithdropdown1 = "";

        public TestCheckedIconNewListBoxForm()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            extButtonWithNewCheckedListBox1.Init(new CheckedIconUserControl.Item[] {
                new CheckedIconUserControl.Item("Tag1","Tag1",Properties.Resources.Addtab,"Tag5"),
                new CheckedIconUserControl.Item("Tag2","Tag2",Properties.Resources.BookmarkManager,"Tag5"),
                new CheckedIconUserControl.Item("Tag3","Tag3",Properties.Resources.BookmarkManager),
                new CheckedIconUserControl.Item("Tag4","Tag4",Properties.Resources.BookmarkManager),
                new CheckedIconUserControl.Item("Tag5","Tag5",Properties.Resources.BookmarkManager,"Tag2;Tag1"),
            },
            buttonwithdropdown1, (s, ch) => buttonwithdropdown1 = s,
            groupoptions: new CheckedIconUserControl.Item[] {
                new CheckedIconUserControl.Item("Tag1;Tag2","Option 1&2"),
                new CheckedIconUserControl.Item("Tag3;Tag4","Option 3&4"),
                new CheckedIconUserControl.Item("Special","Special",exclusive:"All")
            });
        }

        void AddText(string s)
        {
            extRichTextBox.AppendText(s + Environment.NewLine);
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();

            for (int i = 0; i < 200; i++)
            {
                frm.UC.Add($"t{i}", $"Text {i}", Properties.Resources.Addtab);
            }
            frm.CloseBoundaryRegion = new Size(64, 64);
            frm.PositionBelow(extButton1);
            frm.SaveSettings += (s, p) => { AddText($"Save {s}"); };
            frm.Show(this);
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();

            for (int i = 0; i < 200; i++)
            {
                frm.UC.Add($"t{i}", $"Text {i}", Properties.Resources.Addtab);
            }
            frm.PositionBelow(extButton2);
            frm.UC.MultipleColumns = true;
            frm.CloseBoundaryRegion = new Size(64, 64);
            frm.SaveSettings += (s, p) => { AddText($"Save {s}"); };
            frm.Show(this);

        }

        string persistent3 = "All";
        private void extButton3_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();

            frm.UC.AddButton("button1", "Button 1");
            frm.UC.Add($"||", $"||");

            for (int i = 0; i < 400; i++)
            {
                frm.UC.Add($"t{i}", $"Text {i}", Properties.Resources.CursorToTop);
            }
            frm.UC.AddAllNone();
            frm.UC.AddGroupItem("t1;t2;t3", "1-3");
            frm.PositionBelow(extButton2);
            frm.UC.MultipleColumns = true;
            frm.CloseBoundaryRegion = new Size(64, 64);
            frm.UC.CheckedChanged += (index, tag, text, ud, ie) => { AddText($"Check {ie.Index}"); };
            frm.UC.ButtonPressed += (index, stag, text, utag, bev) => { AddText($"Button pressed {index} {stag} {text}"); };
            frm.UC.NoneAllIgnore = "||;";
            frm.SaveSettings += (s, p) => { AddText($"Save {s}"); persistent3 = s; };
            frm.UC.Set(persistent3);
            frm.Show(this);

        }

        private void extButton4_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();

            for (int i = 0; i < 600; i++)
            {
                frm.UC.Add($"t{i}", $"Text {i}", Properties.Resources.CursorToTop);
            }
            frm.UC.AddAllNone();
            frm.UC.AddGroupItem("t1;t2;t3", "1-3");
            frm.PositionBelow(extButton2);
            frm.UC.MultipleColumns = true;
            frm.CloseBoundaryRegion = new Size(64, 64);
            frm.SaveSettings += (s, p) => { AddText($"Save {s}"); };
            frm.Show(this);
        }

        string persistent5 = "All";
        private void extButton5_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();

            frm.UC.AddButton("button1", "Button 1");
            frm.UC.AddButton("button2", "Button 2");
            for (int i = 0; i < 400; i++)
            {
                frm.UC.Add($"t{i}", $"Text {i}", Properties.Resources.CursorToTop);
            }
            frm.UC.AddAllNone();
            frm.UC.AddGroupItem("t1;t2;t3", "1-3");
            frm.UC.AddGroupItem("t100;t101;t102", "100-102");
            frm.PositionBelow(extButton2);
            frm.UC.MultipleColumns = true;
            frm.CloseBoundaryRegion = new Size(64, 64);
            frm.UC.CheckedChanged += (index, tag, text, ud, ie) => { AddText($"Check {ie.Index}"); };
            frm.UC.ButtonPressed += (index, stag, text, utag, bev) => { AddText($"Button pressed {index} {stag} {text}"); };
            frm.SaveSettings += (s, p) => { AddText($"Save {s}"); persistent5 = s; };
            frm.UC.Set(persistent5);

            theme.SetThemeByName("Windows Default");
            Theme.Current.FontSize = 8.25f;
            frm.Show(this);

        }

        string persistent6 = "None";
        private void extButton6_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();

            for (int i = 0; i < 1000; i++)
            {
                frm.UC.Add($"t{i}", $"Text {i}", Properties.Resources.CursorToTop);
            }
            frm.UC.AddAllNone();
            frm.UC.AddGroupItem("t1;t2;t3", "1-3");
            frm.PositionBelow(extButton2);
            frm.UC.MultipleColumns = true;
            frm.CloseBoundaryRegion = new Size(64, 64);
            frm.UC.CheckedChanged += (index, tag, text, ud, ie) => { AddText($"Check {ie.Index}"); };
            frm.AllOrNoneBack = true;
            frm.SaveSettings += (s, p) => { AddText($"Save {s}"); persistent6 = s; };
            frm.UC.Set(persistent6);
            Theme.Current.FontSize = 12;
            frm.Show(this);
        }


        string persistent7 = "All";
        private void extButton7_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();

            frm.UC.AddButton("button1", "Button 1");
            frm.UC.AddButton("button2", "Button 2");
            for (int i = 0; i < 400; i++)
            {
                frm.UC.Add($"t{i}", $"Text {i}", Properties.Resources.CursorToTop);
            }
            frm.UC.AddAllNone();
            frm.UC.AddGroupItem("t1;t2;t3", "1-3");
            frm.UC.AddGroupItem("t100;t101;t102", "100-102");
            frm.UC.MultipleColumns = true;
            frm.CloseBoundaryRegion = new Size(64, 64);
            frm.UC.CheckedChanged += (index, tag, text, ud, ie) => { AddText($"Check {ie.Index}"); };
            frm.UC.ButtonPressed += (index, stag, text, utag, bev) => { AddText($"Button pressed {index} {stag} {text}"); };
            frm.SaveSettings += (s, p) => { AddText($"Save {s}"); persistent7 = s; };
            frm.UC.Set(persistent7);
            Theme.Current.FontSize = 16;

            frm.PositionBelow(extButton2);
            frm.Show(this);

        }

        CheckedIconNewListBoxForm frm8 = null;
        string persistent8 = "All";
        private void extButton8_Click(object sender, EventArgs e)
        {
            if (frm8 != null && frm8.DeactivatedWithin(250))
            {
                System.Diagnostics.Debug.WriteLine("Ignoring as just hid");
                return;
            }

            if ( frm8 == null )
            {
                frm8 = new CheckedIconNewListBoxForm();
                frm8.UC.AddButton("button1", "Button 1");
                frm8.UC.AddButton("button2", "Button 2");

                // 420 full
                for (int i = 0; i <440; i++)
                {
                    frm8.UC.Add($"t{i}", $"Text {i}", Properties.Resources.CursorToTop);
                }
                frm8.UC.AddAllNone();
                frm8.UC.AddGroupItem("t1;t2;t3", "1-3");
                frm8.UC.AddGroupItem("t100;t101;t102", "100-102");

                frm8.UC.AddStringListDefinitions("T20-23:t20;t21;t22;t23!T30-T33:t30;t31;t32;t33", "useritem", true, null, '!', ':');
                frm8.UC.AddButton("creategroup", "Create Group", attop: true);
                frm8.UC.AddButton("removegroups", "Remove All Groups", attop: true);

                frm8.UC.ButtonPressed += (index, stag, text, usertag, ea) =>
                {
                    if (ea.Button == MouseButtons.Right)
                    {
                        if (usertag is string) // a marker that this is a user group
                        {
                            frm8.Hide();

                            if (ExtendedControls.MessageBoxTheme.Show($"Confirm removal of " + text, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                frm8.UC.Remove(index);
                                frm8.ForceRedrawOnNextShow();
                            }
                        }
                    }
                    else if (ea.Button == MouseButtons.Left)
                    {
                        if (stag == "creategroup")
                        {
                            frm8.Hide();

                            string promptValue = ExtendedControls.PromptSingleLine.ShowDialog(null, "", "", "Enter name of new group", this.Icon);
                            if (promptValue != null)
                            {
                                string cursettings = frm8.GetChecked();
                                frm8.UC.AddGroupItem(cursettings, promptValue, null, usertag: "useritem");
                                frm8.ForceRedrawOnNextShow();
                            }
                        }
                        else if ( stag == "removegroups")
                        {
                            frm8.Hide();

                            if (ExtendedControls.MessageBoxTheme.Show($"Confirm removal of all groups", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                frm8.UC.RemoveUserTags("useritem");
                                frm8.ForceRedrawOnNextShow();
                            }

                        }
                    }
                };

                frm8.UC.MultipleColumns = true;
                frm8.CloseBoundaryRegion = new Size(64, 64);
                frm8.UC.CheckedChanged += (index, tag, text, ud, ie) => { AddText($"Check {ie.Index}"); };
                frm8.UC.ButtonPressed += (index, stag, text, utag, bev) => { AddText($"Button pressed {index} {stag} {text}"); };
                frm8.SaveSettings += (s, p) => { AddText($"Save {s}"); persistent8 = s; System.Diagnostics.Debug.WriteLine($"Save settings {s} {p}"); };
                frm8.UC.Set(persistent8);
                frm8.CloseOnDeactivate = false;
                frm8.HideOnDeactivate = true;
                frm8.UC.SlideLeft = true;
                frm8.UC.SlideUp = true;
            }

            frm8.UC.Enable("t100", false);
            frm8.UC.Enable("removegroups", frm8.UC.UserTags("useritem").Count()>0);
            frm8.PositionBelow(extButton8);
            frm8.Show(this);

        }

        string persistent9 = "All";
        private void extButton9_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();

            frm.UC.AddButton("button1", "Button 1");
            frm.UC.AddButton("button2", "Button 2");
            for (int i = 0; i < 400; i++)
            {
                frm.UC.Add($"t{i}", $"Text {i}", Properties.Resources.CursorToTop);
            }
            frm.UC.AddAllNone();
            frm.UC.AddGroupItem("t1;t2;t3", "1-3");
            frm.UC.AddGroupItem("t100;t101;t102", "100-102");
            frm.UC.MultipleColumns = true;
            frm.CloseBoundaryRegion = new Size(64, 64);
            frm.UC.CheckedChanged += (index, tag, text, ud, ie) => { AddText($"Check {ie.Index}"); };
            frm.UC.ButtonPressed += (index, stag, text, utag, bev) => { AddText($"Button pressed {index} {stag} {text}"); };
            frm.SaveSettings += (s, p) => { AddText($"Save {s}"); persistent9 = s; };
            frm.UC.Set(persistent9);
            Theme.Current.FontSize = 14;

            //frm.SetLocation = new Point(100, 100);
            frm.UC.SlideLeft = true;      // allow left shift
            frm.PositionBelow(extButton9);
            frm.Show(this);
        }

        // just buttons
        private void extButton10_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();

            for (int i = 0; i < 400; i++)
            {
                frm.UC.AddButton($"t{i}", $"Text {i}", Properties.Resources.CursorToTop);
            }

            frm.UC.MultipleColumns = true;
            frm.CloseBoundaryRegion = new Size(64, 64);
            frm.UC.ButtonPressed += (index, stag, text, utag, bev) => { AddText($"Button pressed {index} {stag} {text}"); };
            frm.UC.SlideLeft = true;      // allow left shift
            frm.PositionBelow(extButton10);
            frm.Show(this);

        }

        private void extButton11_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();

            for (int i = 0; i < 400; i++)
            {
                frm.UC.AddButton($"t{i}", $"Text {i}",null);
            }
            // frm.UC.Add($"t100", $"CHK 100", Properties.Resources.CursorToTop);

            frm.UC.MultipleColumns = true;
            frm.CloseBoundaryRegion = new Size(64, 64);
            frm.UC.ButtonPressed += (index, stag, text, utag, bev) => { AddText($"Button pressed {index} {stag} {text}"); };
            frm.UC.SlideLeft = true;      // allow left shift
            frm.PositionBelow(extButton11);
            frm.Show(this);


        }

        List<CheckedIconUserControl.SubForm> sf = new List<CheckedIconUserControl.SubForm>();
        string b12settings = "";

        private void extButton12_Click(object sender, EventArgs e)
        {
            CheckedIconNewListBoxForm frm = new CheckedIconNewListBoxForm();
            int sfn = 0;

            for (int i = 0; i < 10; i++)
            {
                List<CheckedIconUserControl.Item> subform = new List<CheckedIconUserControl.Item>();

                if (i < 5) // demo user controlled sub menu icon
                {
                    List<CheckedIconUserControl.Item> subsubform = new List<CheckedIconUserControl.Item>();
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 100000 + 1}", $"T{i * 100000 + 1}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 100000 + 2}", $"T{i * 100000 + 2}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 100000 + 3}", $"T{i * 100000 + 3}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 100000 + 4}", $"T{i * 100000 + 4}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"S{i * 100000 + 5}", $"S{i * 100000 + 5}"));
                    subsubform.Add(new CheckedIconUserControl.Item($"S{i * 100000 + 6}", $"S{i * 100000 + 6}"));
                    sf.Add(new CheckedIconUserControl.SubForm { Items = subsubform, Setting = $"", SubmenuIcon = Properties.Resources.RightArrow });

                    subform.Add(new CheckedIconUserControl.Item($"SF1{i * 1000 + 1}", $"SF1{i * 1000 + 1}", button: true, usertag: sf[sfn++]));
                }

                subform.Add(new CheckedIconUserControl.Item($"T{i * 1000 + 1}", $"T{i * 1000 + 1}", button: true));
                subform.Add(new CheckedIconUserControl.Item($"T{i * 1000 + 2}", $"T{i * 1000 + 2}", button: true));
                subform.Add(new CheckedIconUserControl.Item($"T{i * 1000 + 3}", $"T{i * 1000 + 3}", button: true));

                if (i < 8) // demo default submenu icon
                {
                    List<CheckedIconUserControl.Item> subsubform = new List<CheckedIconUserControl.Item>();
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 200000 + 1}", $"T{i * 200000 + 1}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 200000 + 2}", $"T{i * 200000 + 2}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 200000 + 3}", $"T{i * 200000 + 3}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 200000 + 4}", $"T{i * 200000 + 4}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"S{i * 200000 + 5}", $"S{i * 200000 + 5}"));
                    subsubform.Add(new CheckedIconUserControl.Item($"S{i * 200000 + 6}", $"S{i * 200000 + 6}"));
                    sf.Add(new CheckedIconUserControl.SubForm { Items = subsubform, Setting = $"", SubmenuIcon = null });

                    subform.Add(new CheckedIconUserControl.Item($"SF2{i * 2000 + 1}", $"SF2{i * 2000 + 1}", button: true, usertag: sf[sfn++]));

                }

                subform.Add(new CheckedIconUserControl.Item($"T{i * 1000 + 4}", $"T{i * 1000 + 4}", button: true));
                subform.Add(new CheckedIconUserControl.Item($"S{i * 1000 + 5}", $"S{i * 1000 + 5}"));

                if (i < 8)  // demo no sub menu icon
                {
                    List<CheckedIconUserControl.Item> subsubform = new List<CheckedIconUserControl.Item>();
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 300000 + 1}", $"T{i * 300000 + 1}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 300000 + 2}", $"T{i * 300000 + 2}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 300000 + 3}", $"T{i * 300000 + 3}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"T{i * 300000 + 4}", $"T{i * 300000 + 4}", button: true));
                    subsubform.Add(new CheckedIconUserControl.Item($"S{i * 300000 + 5}", $"S{i * 300000 + 5}"));
                    subsubform.Add(new CheckedIconUserControl.Item($"S{i * 300000 + 6}", $"S{i * 300000 + 6}"));
                    sf.Add(new CheckedIconUserControl.SubForm { Items = subsubform, Setting = $"", SubmenuIcon = CheckedIconGroupUserControl.NoSubMenuIcon });

                    subform.Add(new CheckedIconUserControl.Item($"SF3{i * 3000 + 1}", $"SF3{i * 3000 + 1}", button: true, usertag: sf[sfn++]));

                }


                subform.Add(new CheckedIconUserControl.Item($"S{i * 1000 + 6}", $"S{i * 1000 + 6}"));

                sf.Add(new CheckedIconUserControl.SubForm { Items = subform, Setting = $"", Alpha = 50 });

                frm.UC.AddButton($"t{i}", i % 2 == 0 ? $"Sub {i}" : $"Subsform {i}", Properties.Resources.CursorToTop, sf[sfn++]);

                if ( i == 5)
                {
                    for (int j = 0; j < 50; j++)
                    {
                        frm.UC.AddButton($"t{j + 100}", $"But {j + 100}", Properties.Resources.CursorToTop);
                    }
                }
            }


            for (int i = 0; i < 5; i++)
            {
                frm.UC.Add($"t{i}", $"Text {i}", Properties.Resources.CursorToTop);
            }

            theme.SetThemeByName("Elite Verdana Small");

            frm.Name = "TopForm";
            frm.UC.MultipleColumns = true;
            frm.CloseBoundaryRegion = new Size(20, 20);
            frm.UC.SlideLeft = true;      // allow left shift
            frm.OpenSubformAlpha = 75;
            frm.PositionBelow(extButton10);
            frm.UC.ButtonPressed += (index, stag, text, utag, bev) => {
                System.Diagnostics.Debug.WriteLine($">>> Form button pressed {index} {stag} {text}");
                AddText($"*** Form {frm.Name} Button pressed {index} {stag} {text}"); 
                frm.Close(); 
            };
            frm.CloseDownTime = 300;
            frm.SaveSettings += (s, p) => { System.Diagnostics.Debug.WriteLine($">>> Save Settings {frm.Name} {s} {p}"); b12settings = s; };

            frm.Show(b12settings,extButton12,this); ;
        }
    }

}
