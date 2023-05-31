/*
 * Copyright © 2023-2023 robby @ github
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedControls
{
    public class VT100
    {
        // https://en.wikipedia.org/wiki/ANSI_escape_code
        // http://braun-home.net/michael/info/misc/VT100_commands.htm
        // https://www.vt100.net/docs/vt100-ug/chapter3.html#S3.3.3

        // C0 control codes
        //^G 	0x07 	BEL Bell    Makes an audible noise.
        //^H    0x08 	BS Backspace   Moves the cursor left (but may "backwards wrap" if cursor is at start of line).
        //^I 	0x09 	HT Tab     Moves the cursor right to next multiple of 8.
        //^J 	0x0A 	LF Line Feed Moves to next line, scrolls the display up if at bottom of the screen.Usually does not move horizontally, though programs should not rely on this.
        //^L 	0x0C 	FF Form Feed Move a printer to top of next page.Usually does not move horizontally, though programs should not rely on this. Effect on video terminals varies.
        //^M 	0x0D 	CR Carriage Return Moves the cursor to column zero.
        //^[    0x1B 	ESC     Escape  Starts all the escape sequences
        // 0x80-0x9F    C1 codes which map to Fe codes (32 codes)
        // ESC 0x40-0x5F Fe codes       - @ A..Z [\]^_     - same as C1 codes
        // ESC 0x60-0x7E Fs sequence    - `a..z{|}~DEL     - individual control sequences
        // ESC 0x30-0x3F Fp sequence    - 0..9:;<=>?       - private control functions
        // ESC 0x20-0x2F nF sequence    - !"#$%&'()*+'-./  - followed by additional bytes in 0x20-02xf, then a byte in 0x30-0x7e


        public VT100(Terminal tr)
        {
            term = tr;
        }

        public bool ReportCursorPosition { get; set; } = false; // set on ESC [ 6 n
        public bool VT52 { get; set; } = true;                  // support VT52 codes
        public bool SupportC1Codes { get; set; } = false;       // standard for 8 bits codes is 0x80-0x9F are equivalent to ESC N. in UTF-8 they are printable, so default disable

        public void Display(string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ch in s)
            {
                switch (state)
                {
                    case TermState.Normal:
                        if ((ch >= 0x20 && ch <= 0x7F) ||      // normal chars
                              ch >= 0xa0 ||
                              (!SupportC1Codes && ch >= 0x80) ||       // if C1 is disabled
                              ch == '\b' || ch == '\n' || ch == '\r')     // normal chars, place in queue
                        {
                            sb.Append(ch);
                        }
                        else if (ch == 0x7)                     // C0
                            Console.Beep(512, 100);
                        else
                        {
                            term.AddTextClear(ref sb);          // emit and clear

                            if (ch == 0x1B)                     // Escape sequence
                                state = TermState.Escape;
                            else if (ch >= 0x80 && ch <= 0x9f)  // C1 codes - if they got here..
                                ProcessFe((char)(ch - 0x40));
                            else if (ch == 0x9)                 // C0
                                term.TabForward(8);
                            else
                                sb.Append(ch);                  // try and print them
                        }
                        break;

                    case TermState.Escape:
                        if (ch >= 0x40 && ch <= 0x5F)           // Fe codes @ A..Z [\]^_
                        {
                            command = "";                       // clear the command and string memory
                            instringescape = false;
                            state = TermState.Normal;           // most codes result in end of sequence so set it
                            ProcessFe(ch);                      // process it, it may result in change to another state below
                        }
                        else if (ch >= 0x60 && ch <= 0x7E)    // fs sequence `a..z{|}~DEL
                        {
                            state = TermState.Normal;
                            ProcessFs(ch);
                        }
                        else if (ch >= 0x30 && ch <= 0x3F)    // fp sequence 0..9:;<=>?
                        {
                            state = TermState.Normal;
                            ProcessFp(ch);
                        }
                        else if (ch >= 0x20 && ch <= 0x2f)    // nF sequence space !"#$%&'()*+'-./
                        {
                            state = TermState.nF;
                            command = new string(ch, 1);      // start nf sequence
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Unknown ESC sequence '{ch}'");
                            state = TermState.Normal;
                        }
                        break;

                    case TermState.CSI:
                        if (ch >= 0x40 && ch <= 0x7F)           // @ A..Z [\]^_   terminates code stream
                        {
                            state = TermState.Normal;           // back to normal
                            CSI(ch);                            // process
                        }
                        else
                            command += ch;
                        break;

                    case TermState.DCS:
                    case TermState.PM:
                    case TermState.OS:
                    case TermState.SOS:
                        if (instringescape)                   // strings..
                        {
                            if (ch == '\\')
                            {
                                // would issue command for termstate
                                System.Diagnostics.Debug.WriteLine($"Issue {state} : `{command}`");
                                state = TermState.Normal;
                            }
                            else
                                instringescape = false;
                        }
                        else if (ch == 0x1b)
                            instringescape = true;
                        else
                            command += ch;
                        break;

                    case TermState.nF:                      // nF sequence space !"#$%&'()*+'-./  repeat followed by 0x30-0x7E
                        command += ch;
                        if ( ch >= 0x30&&ch<=0x7E)          // if terminator, process
                        {
                            ProcessnF();
                            state = TermState.Normal;
                        }
                        break;
                }

            }

            if (sb.Length > 0)
                term.AddTextClear(ref sb);
        }

        private void CSI(char ch)       // ESC [ parameters;parameters C sequence
        {
            int? value = command.InvariantParseIntNull();   // most use a single value, so decode it

            switch (ch)
            {
                case 'A': //Moves the active position upward without altering the column position. The number of lines moved is determined by the parameter. A parameter value of zero or one moves the active position one line upward. A parameter value of n moves the active position n lines upward. If an attempt is made to move the cursor above the top margin, the cursor stops at the top margin. 
                    term.CursorUp(Math.Max(1, value ?? 1));
                    break;
                case 'B':   // The CUD sequence moves the active position downward without altering the column position. The number of lines moved is determined by the parameter. If the parameter value is zero or one, the active position is moved one line downward. If the parameter value is n, the active position is moved n lines downward. In an attempt is made to move the cursor below the bottom margin, the cursor stops at the bottom margin
                    term.CursorDown(Math.Max(1, value ?? 1));
                    break;
                case 'C':   // The CUF sequence moves the active position to the right. The distance moved is determined by the parameter. A parameter value of zero or one moves the active position one position to the right. A parameter value of n moves the active position n positions to the right. If an attempt is made to move the cursor to the right of the right margin, the cursor stops at the right margin
                    term.CursorForward(Math.Max(1, value ?? 1));
                    break;
                case 'D': // The CUB sequence moves the active position to the left. The distance moved is determined by the parameter. If the parameter value is zero or one, the active position is moved one position to the left. If the parameter value is n, the active position is moved n positions to the left. If an attempt is made to move the cursor to the left of the left margin, the cursor stops at the left margin. 
                    term.CursorBack(Math.Max(1, value ?? 1));
                    break;
                case 'E':
                    term.CursorNextLine(Math.Max(1, value ?? 1));
                    break;
                case 'F':
                    term.CursorPreviousLine(Math.Max(1, value ?? 1));
                    break;
                case 'G':
                    term.CursorPosition = new System.Drawing.Point((value ?? 1) - 1, term.CursorPosition.Y);
                    break;

                // The CUP sequence moves the active position to the position specified by the parameters. This sequence has two parameter values,
                // the first specifying the line position and the second specifying the column position.
                // A parameter value of zero or one for the first or second parameter moves the active position to the first line or column in the display,
                // respectively. The default condition with no parameters present is equivalent to a cursor to home action.
                // In the VT100, this control behaves identically with its format effector counterpart, HVP

                case 'H': // CUP
                case 'f': // HVP
                    {
                        int p1 = command.IndexOf(';');
                        if (p1 >= 0)
                        {
                            term.CursorPosition = new System.Drawing.Point(
                                command.Substring(p1 + 1).InvariantParseInt(1) - 1,     // second is x
                                command.Substring(0, p1).InvariantParseInt(1) - 1      // first is y, if no text, default is 1
                                );
                        }
                        else
                            System.Diagnostics.Debug.WriteLine($"CSI H missing ; '{command}'");

                        break;
                    }
                case 'J': //ED – Erase In Display
                    if (value == 0)
                        term.ClearCursorToScreenEnd();
                    else if (value == 1)
                        term.ClearCursorToScreenStart();
                    else if (value == 2 || value == 3)
                    {
                        term.ClearScreen();
                        // docs say cursor does not move term.CursorPosition = new System.Drawing.Point(0, 0);
                    }
                    else
                        System.Diagnostics.Debug.WriteLine($"Unknown J code {value}");
                    break;
                case 'K': // EL – Erase In Line
                    if (value == 0)
                        term.ClearCursorToLineEnd();
                    else if (value == 1)
                        term.ClearCursorToLineStart();
                    else if (value == 2)
                        term.ClearLine();
                    else
                        System.Diagnostics.Debug.WriteLine($"Unknown K code {value}");
                    break;
                case 'S':
                    term.ScrollUp(value ?? 1);
                    break;
                case 'T':
                    term.ScrollDown(value ?? 1);
                    break;
                case 'm':
                    while (true)
                    {
                        int p1 = command.IndexOf(';');
                        int cmd = command.Substring(0, p1 >= 0 ? p1 : command.Length).InvariantParseInt(0);     // def 0 if not there
                        switch (cmd)
                        {
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                            case 36:
                            case 37:
                                term.VTForeColor = colours[cmd - 30];
                                break;
                            case 90:
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                            case 97:
                                term.VTForeColor = colours[cmd - 90 + 8];
                                break;
                            case 40:
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                            case 46:
                            case 47:
                                term.VTBackColor = colours[cmd - 40];
                                break;
                            case 100:
                            case 101:
                            case 102:
                            case 103:
                            case 104:
                            case 105:
                            case 106:
                            case 107:
                                term.VTBackColor = colours[cmd - 100 + 8];
                                break;
                            case 0: // off
                            case 1: // bold
                            case 4: // underscore
                            case 5: // blink
                            case 7: // neg image
                            default:
                                System.Diagnostics.Debug.WriteLine($"Unknown/Unimplemented SGR {cmd}");
                                break;
                        }

                        if (p1 >= 0)                                    // if semi there, cut and loop
                            command = command.Substring(p1 + 1);        // cut
                        else
                            break;
                    }
                    break;
                case 'R':       // CPR – Cursor Position Report – VT100 to Host
                case 'c':       // DA  – Device Attributes
                case 'i':       // unimplemented
                case 'g':       // TBC – Tabulation Clear
                case 'h':       // SM – Set Mode
                case 'l':       // RM – Reset Mode
                case 'r':       // DECSTBM – Set Top and Bottom Margins (DEC Private)
                case 'q':       // DECLL – Load LEDS (DEC Private)
                case 'x':       // DECREPTPARM – Report Terminal Parameters
                case 'y':       // DECTST – Invoke Confidence Test
                    System.Diagnostics.Debug.WriteLine($"Unsupported {ch} code {value}");
                    break;
                case 'n':       // DSR – Device Status Report
                    if (value == 6)
                        ReportCursorPosition = true;
                    else
                        System.Diagnostics.Debug.WriteLine($"Unsupported CSI n code {value}");
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine($"Unknown CSI sequence '{ch}' '{command}'");
                    break;
            }
        }

        private void ProcessFe(char ch)                 // ESC @ A..Z [\]^_ or C1 codes shifted to same range
        {
            switch (ch)
            {
                case 'A':       // cursor up VT52
                    if (VT52)
                        term.CursorUp(1);
                    break;
                case 'B':       // cursor down VT52
                    if (VT52)
                        term.CursorDown(1);
                    break;
                case 'C':       // cursor right VT52
                    if (VT52)
                        term.CursorForward(1);
                    break;
                case 'D':       // cursor left VT52
                    if (VT52)
                        term.CursorBack(1);
                    break;
                case 'E': // ESC NEL VT100 This sequence causes the active position to move to the first position on the next line downward. If the active position is at the bottom margin, a scroll up is performed.
                    if (VT52)
                        term.CursorNextLine(1,true);
                    break;
                case 'H':       // cursor home VT52
                    if (VT52)
                        term.CursorPosition = new System.Drawing.Point(0, 0);
                    else
                        System.Diagnostics.Debug.WriteLine($"Unimplemented Set Tab");
                    break;
                case 'I':       // Reverse line feed vt52
                    if (VT52)
                        term.CursorUp(1, true);
                    break;
                case 'J':       // vt52
                    if (VT52)
                        term.ClearCursorToScreenEnd();
                    break;
                case 'K':       // vt52
                    if (VT52)
                        term.ClearCursorToLineEnd();
                    break;
                case 'P':
                    state = TermState.DCS;
                    break;
                case '[':
                    state = TermState.CSI;
                    break;
                case '\\':      // ignore esc \, as its an ST end, used only after P[]
                    break;
                case ']':
                    state = TermState.OS;
                    break;
                case 'X':
                    state = TermState.SOS;
                    break;
                case '^':
                    state = TermState.PM;      
                    break;
                case '_':
                    state = TermState.APC;
                    break;
                case 'M': // ESC RI Move the active position to the same horizontal position on the preceding line. If the active position is at the top margin, a scroll down is performed.
                    if (VT52)
                        term.CursorUp(1, true);
                    break;

                case 'N': // SS2 ignored
                case 'O': // SS3 ignored
                case 'F': // Enter Graphics Mode Vt52
                case 'G': // Leave Graphics Mode Vt52
                case 'Y': // Direct Cursor Address ESC Y line column 	 
                case 'Z': // DECID – Identify Terminal (DEC Private)
                default:
                    System.Diagnostics.Debug.WriteLine($"Unimplemented Fe '{ch}'");
                    break;
            }
        }

        private void ProcessFs(char ch) // fs sequence ESC 1a..z{|}~DEL
        {
            switch (ch)
            {
                case 'c': // Reset the VT100 to its initial state, i.e., the state it has after it is powered on. This also causes the execution of the power-up self-test and signal INIT H to be asserted briefly.
                default:
                    System.Diagnostics.Debug.WriteLine($"Not processed Fs '{ch}'");
                    break;
            }
        }

        private void ProcessFp(char ch) // fp sequence ESC 0..9:;<=>?
        {
            switch( ch)
            {
                case '=': //DECKPAM – Keypad Application Mode (DEC Private)
                case '>': //DECKPNM – Keypad Numeric Mode(DEC Private)
                case '<': //Enter ANSI Mode
                case '7': //DECSC – Save Cursor (DEC Private)
                case '8': //DECRC – Restore Cursor (DEC Private)
                default:
                    System.Diagnostics.Debug.WriteLine($"Unimplemented Fp '{ch}'");
                    break;
            }
        }

        private void ProcessnF()
        {
            if (command == "#8")   // DECALN – Screen Alignment Display (DEC Private)
            {
            }
            else if (command == "#3")   // DECDHL – Double Height Line (DEC Private) top half
            {
            }
            else if (command == "#4")   // DECDHL – Double Height Line (DEC Private) Bottom half
            {
            }
            else if (command == "#5")   // DECSWL – Single-width Line (DEC Private)
            {
            }
            else if (command == "#6")   // DECDWL – Double-Width Line (DEC Private)
            {
            }

            //ESC SP F  Announce code structure 6
            //ESC SP G Announce code structure 7
            //ESC(A     ESC) A United Kingdom Set
            //ESC(B     ESC) B ASCII Set
            //ESC(0     ESC) 0     Special Graphics
            //ESC(1     ESC) 1     Alternate Character ROM Standard Character Set
            //ESC(2     ESC) 2     Alternate Character ROM Special Graphics

            System.Diagnostics.Debug.WriteLine($"Unimplemented nf sequence '{command}'");
        }

        private Terminal term;
        private TermState state = TermState.Normal;
        enum TermState
        {
            Normal,
            Escape,         // Escape seen
            CSI,            // Escape [     terminated by 0x40-0x7e
            nF,             // Escape 0x20-0x2F.. 0x30-0x7E
            DCS,            // device control string, terminated by ST (ESC\)
            OS,             // OS command, terminated by ST (ESC\)
            SOS,            // Start of string, terminated by ST (ESC\)
            PM,             // Privacy message, terminated by ST (ESC\)
            APC,            // APC message, terminated by ST (ESC\)
        }

        private bool instringescape;
        private string command;

        static System.Drawing.Color[] colours = new System.Drawing.Color[]
        {
            System.Drawing.Color.Black,
            System.Drawing.Color.Red,
            System.Drawing.Color.Green,
            System.Drawing.Color.Yellow,

            System.Drawing.Color.Blue,
            System.Drawing.Color.Magenta,
            System.Drawing.Color.Cyan,
            System.Drawing.Color.FromArgb(255,170,170,170),

            System.Drawing.Color.FromArgb(255,85,85,85),
            System.Drawing.Color.FromArgb(255,255,85,85),
            System.Drawing.Color.FromArgb(255,85,255,85),
            System.Drawing.Color.FromArgb(255,255,255,85),

            System.Drawing.Color.FromArgb(255,85,85,255),
            System.Drawing.Color.FromArgb(255,255,85,255),
            System.Drawing.Color.FromArgb(255,85,255,255),
            System.Drawing.Color.FromArgb(255,255,255,255),
        };


    }
}
