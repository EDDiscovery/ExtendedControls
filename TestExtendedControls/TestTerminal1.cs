using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExtendedControls
{
    public partial class TestTerminal1 : Form
    {
        ExtendedControls.VT100 proc;

        public TestTerminal1()
        {
            InitializeComponent();
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
         // proc = new VT100Processor(terminal);

            terminal.AddText("Hello and welcome to this terminal program\r\nHow are you?\r\n");
            terminal.VTForeColor = Color.Yellow;
            terminal.AddText("Hello in yellow\r\n");
            this.Text = $"Paint {terminal.ClientRectangle} text {terminal.TextSize} cell {terminal.CellSize}";

            proc = new ExtendedControls.VT100(terminal);
            terminal.CursorFlashes = false;
            terminal.CursorShape = ExtendedControls.Terminal.CursorShapeType.Block;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Text = $"Paint {terminal.ClientRectangle} text {terminal.TextSize} cell {terminal.CellSize}";
        }

        private void buttonVUp_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[{textBoxCursorN.Text}A");
        }

        private void buttonVLeft_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[{textBoxCursorN.Text}D");
        }

        private void buttonVDown_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[{textBoxCursorN.Text}B");
        }

        private void buttonVRight_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[{textBoxCursorN.Text}C");

        }

        private void buttonPLine_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[{textBoxCursorN.Text}F");

        }

        private void buttonNLine_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[{textBoxCursorN.Text}E");
        }

        private void buttonHCol_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[{textBoxCursorN.Text}G");

        }
        private void buttonHome_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001BH");

        }
        private void buttonScrollDown_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[{textBoxCursorN.Text}T");

        }

        private void buttonScrollUp_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[{textBoxCursorN.Text}S");
        }

        private void buttonClearEOS_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[0J");
        }

        private void buttonClearSOS_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[1J");
        }

        private void buttonCLS_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[2J");
        }

        private void buttonClearEOL_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[0K");
        }

        private void buttonClearSOL_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[1K");
        }

        private void buttonClearLine_Click(object sender, EventArgs e)
        {
            proc.Display($"\u001B[2K");
        }

        private void comboBoxForeColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cno = 30 + comboBoxForeColour.SelectedIndex + (comboBoxForeColour.SelectedIndex >= 8 ? 52 : 0);
            proc.Display($"\u001B[{cno}m");
        }

        private void comboBoxBackColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cno = 40 + comboBoxBackColour.SelectedIndex + (comboBoxBackColour.SelectedIndex >= 8 ? 52 : 0);
            proc.Display($"\u001B[{cno}m");

        }

        private void buttonCursoroff_Click(object sender, EventArgs e)
        {
            terminal.ShowCursor = false;

        }

        private void buttonCursorLine_Click(object sender, EventArgs e)
        {
            terminal.CursorShape = ExtendedControls.Terminal.CursorShapeType.LineLeft;
            terminal.ShowCursor = true;
        }

        private void buttonCursorBlock_Click(object sender, EventArgs e)
        {
            terminal.CursorShape = ExtendedControls.Terminal.CursorShapeType.Block;
            terminal.ShowCursor = true;
        }
        private void buttonCursorBlink_Click(object sender, EventArgs e)
        {
            terminal.CursorFlashes = !terminal.CursorFlashes;

        }

        private void buttonVertResize_Click(object sender, EventArgs e)
        {
            this.Size = new Size(Bounds.Width, Bounds.Height+50);
        }

        private void buttonResize_Click(object sender, EventArgs e)
        {
            this.Size = new Size(Bounds.Width + 50, Bounds.Height);
        }

        private void buttonCR_Click(object sender, EventArgs e)
        {
            terminal.CR();
        }

        private void buttonCRLF_Click(object sender, EventArgs e)
        {
            terminal.CRLF();
        }

        private void buttonTextWithBS_Click(object sender, EventArgs e)
        {
            terminal.AddText($"Hello 00\b\bThere");
        }

        private void buttonT1_Click(object sender, EventArgs e)
        {
            int cursory = terminal.CursorPosition.Y;
            terminal.AddText($"Line {cursory} more\r\n");        }

        private void buttonText_Click(object sender, EventArgs e)
        {
            terminal.AddText($"abcd-");

        }

        private void buttonTextBS_Click(object sender, EventArgs e)
        {
            terminal.AddText($"\b");
        }
        private void buttonLFText_Click(object sender, EventArgs e)
        {
            terminal.AddText($"\n");
        }

        private void buttonLF_Click(object sender, EventArgs e)
        {
            terminal.LF();

        }

        private void buttonChar_Click(object sender, EventArgs e)
        {
            terminal.AddText($"a");
        }

        private void buttonCRText_Click(object sender, EventArgs e)
        {
            terminal.AddText($"\r");

        }

        private void buttonDownScroll_Click(object sender, EventArgs e)
        {
            terminal.CursorDown(2, true);
        }

        private void buttonUpScroll_Click(object sender, EventArgs e)
        {
            terminal.CursorUp(2, true);
        }

        StreamReader file;
        Timer playtimer;
        bool pause = false;

        private void buttonFilePlay_Click(object sender, EventArgs e)
        {
            if (file == null)
                file = new StreamReader(@"c:\code\cc3.txt");

            playtimer = new Timer();
            playtimer.Interval = 100;
            playtimer.Tick += Playtimer_Tick;
            playtimer.Start();
        }

        private void Playtimer_Tick(object sender, EventArgs e)
        {
            if (file != null)
            {
                int ch = file.Read();
                if (ch == -1)
                {
                    file.Close();
                    file = null;
                    playtimer.Stop();
                }
                else
                {
                    char c = (char)ch;
                    System.Diagnostics.Debug.WriteLine($"Next char {ch} = {c}");
                    proc.Display(new string(c, 1));
                }
            }
        }

        private void buttonFilePause_Click(object sender, EventArgs e)
        {
            playtimer.Enabled = !playtimer.Enabled;
            buttonFilePause.Text = playtimer.Enabled ? "Pause" : "Play";
        }

        private void buttonFileStep_Click(object sender, EventArgs e)
        {
            Playtimer_Tick(null, null);

        }

        private void buttonFileFast_Click(object sender, EventArgs e)
        {
            playtimer.Interval = playtimer.Interval >= 100 ? 25 : 100;
        }
    }
}
