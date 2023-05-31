
namespace TestExtendedControls
{
    partial class TestTerminal1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxBackColour = new System.Windows.Forms.ComboBox();
            this.comboBoxForeColour = new System.Windows.Forms.ComboBox();
            this.textBoxCursorN = new System.Windows.Forms.TextBox();
            this.buttonClearLine = new System.Windows.Forms.Button();
            this.buttonClearSOL = new System.Windows.Forms.Button();
            this.buttonScrollDown = new System.Windows.Forms.Button();
            this.buttonScrollUp = new System.Windows.Forms.Button();
            this.buttonFileFast = new System.Windows.Forms.Button();
            this.buttonFileStep = new System.Windows.Forms.Button();
            this.buttonFilePause = new System.Windows.Forms.Button();
            this.buttonFilePlay = new System.Windows.Forms.Button();
            this.buttonClearEOL = new System.Windows.Forms.Button();
            this.buttonCLS = new System.Windows.Forms.Button();
            this.buttonClearSOS = new System.Windows.Forms.Button();
            this.buttonClearEOS = new System.Windows.Forms.Button();
            this.buttonHome = new System.Windows.Forms.Button();
            this.buttonHCol = new System.Windows.Forms.Button();
            this.buttonVRight = new System.Windows.Forms.Button();
            this.buttonNLine = new System.Windows.Forms.Button();
            this.buttonPLine = new System.Windows.Forms.Button();
            this.buttonVLeft = new System.Windows.Forms.Button();
            this.buttonVDown = new System.Windows.Forms.Button();
            this.buttonVUp = new System.Windows.Forms.Button();
            this.buttonVTTextLF = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonT1 = new System.Windows.Forms.Button();
            this.buttonChar = new System.Windows.Forms.Button();
            this.buttonText = new System.Windows.Forms.Button();
            this.buttonDownScroll = new System.Windows.Forms.Button();
            this.buttonUpScroll = new System.Windows.Forms.Button();
            this.buttonLF = new System.Windows.Forms.Button();
            this.buttonVertResize = new System.Windows.Forms.Button();
            this.buttonCRText = new System.Windows.Forms.Button();
            this.buttonCR = new System.Windows.Forms.Button();
            this.buttonCursorBlink = new System.Windows.Forms.Button();
            this.buttonCursorBlock = new System.Windows.Forms.Button();
            this.buttonCRLF = new System.Windows.Forms.Button();
            this.buttonCursorLine = new System.Windows.Forms.Button();
            this.buttonLFText = new System.Windows.Forms.Button();
            this.buttonTextBS = new System.Windows.Forms.Button();
            this.buttonCursoroff = new System.Windows.Forms.Button();
            this.buttonTextWithBS = new System.Windows.Forms.Button();
            this.buttonResize = new System.Windows.Forms.Button();
            this.terminal = new ExtendedControls.Terminal();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.comboBoxBackColour);
            this.panel2.Controls.Add(this.comboBoxForeColour);
            this.panel2.Controls.Add(this.textBoxCursorN);
            this.panel2.Controls.Add(this.buttonClearLine);
            this.panel2.Controls.Add(this.buttonClearSOL);
            this.panel2.Controls.Add(this.buttonScrollDown);
            this.panel2.Controls.Add(this.buttonScrollUp);
            this.panel2.Controls.Add(this.buttonFileFast);
            this.panel2.Controls.Add(this.buttonFileStep);
            this.panel2.Controls.Add(this.buttonFilePause);
            this.panel2.Controls.Add(this.buttonFilePlay);
            this.panel2.Controls.Add(this.buttonClearEOL);
            this.panel2.Controls.Add(this.buttonCLS);
            this.panel2.Controls.Add(this.buttonClearSOS);
            this.panel2.Controls.Add(this.buttonClearEOS);
            this.panel2.Controls.Add(this.buttonHome);
            this.panel2.Controls.Add(this.buttonHCol);
            this.panel2.Controls.Add(this.buttonVRight);
            this.panel2.Controls.Add(this.buttonNLine);
            this.panel2.Controls.Add(this.buttonPLine);
            this.panel2.Controls.Add(this.buttonVLeft);
            this.panel2.Controls.Add(this.buttonVDown);
            this.panel2.Controls.Add(this.buttonVUp);
            this.panel2.Controls.Add(this.buttonVTTextLF);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 123);
            this.panel2.TabIndex = 4;
            // 
            // comboBoxBackColour
            // 
            this.comboBoxBackColour.FormattingEnabled = true;
            this.comboBoxBackColour.Items.AddRange(new object[] {
            "Black",
            "Red",
            "Green",
            "Yellow",
            "Blue",
            "Magenta",
            "Cyan",
            "White",
            "Gray",
            "Bright Red",
            "Bright Green",
            "Bright Yellow",
            "Bright Blue",
            "Bright Magenta",
            "Bright Cyan",
            "Bright White"});
            this.comboBoxBackColour.Location = new System.Drawing.Point(371, 91);
            this.comboBoxBackColour.Name = "comboBoxBackColour";
            this.comboBoxBackColour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBackColour.TabIndex = 2;
            this.comboBoxBackColour.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackColour_SelectedIndexChanged);
            // 
            // comboBoxForeColour
            // 
            this.comboBoxForeColour.FormattingEnabled = true;
            this.comboBoxForeColour.Items.AddRange(new object[] {
            "Black",
            "Red",
            "Green",
            "Yellow",
            "Blue",
            "Magenta",
            "Cyan",
            "White",
            "Gray",
            "Bright Red",
            "Bright Green",
            "Bright Yellow",
            "Bright Blue",
            "Bright Magenta",
            "Bright Cyan",
            "Bright White"});
            this.comboBoxForeColour.Location = new System.Drawing.Point(177, 91);
            this.comboBoxForeColour.Name = "comboBoxForeColour";
            this.comboBoxForeColour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxForeColour.TabIndex = 2;
            this.comboBoxForeColour.SelectedIndexChanged += new System.EventHandler(this.comboBoxForeColour_SelectedIndexChanged);
            // 
            // textBoxCursorN
            // 
            this.textBoxCursorN.Location = new System.Drawing.Point(299, 34);
            this.textBoxCursorN.Name = "textBoxCursorN";
            this.textBoxCursorN.Size = new System.Drawing.Size(75, 20);
            this.textBoxCursorN.TabIndex = 1;
            // 
            // buttonClearLine
            // 
            this.buttonClearLine.Location = new System.Drawing.Point(660, 60);
            this.buttonClearLine.Name = "buttonClearLine";
            this.buttonClearLine.Size = new System.Drawing.Size(75, 23);
            this.buttonClearLine.TabIndex = 0;
            this.buttonClearLine.Text = "K2 CLine";
            this.buttonClearLine.UseVisualStyleBackColor = true;
            this.buttonClearLine.Click += new System.EventHandler(this.buttonClearLine_Click);
            // 
            // buttonClearSOL
            // 
            this.buttonClearSOL.Location = new System.Drawing.Point(663, 31);
            this.buttonClearSOL.Name = "buttonClearSOL";
            this.buttonClearSOL.Size = new System.Drawing.Size(75, 23);
            this.buttonClearSOL.TabIndex = 0;
            this.buttonClearSOL.Text = "K1 CSOL";
            this.buttonClearSOL.UseVisualStyleBackColor = true;
            this.buttonClearSOL.Click += new System.EventHandler(this.buttonClearSOL_Click);
            // 
            // buttonScrollDown
            // 
            this.buttonScrollDown.Location = new System.Drawing.Point(498, 60);
            this.buttonScrollDown.Name = "buttonScrollDown";
            this.buttonScrollDown.Size = new System.Drawing.Size(75, 23);
            this.buttonScrollDown.TabIndex = 0;
            this.buttonScrollDown.Text = "Scroll Down";
            this.buttonScrollDown.UseVisualStyleBackColor = true;
            this.buttonScrollDown.Click += new System.EventHandler(this.buttonScrollDown_Click);
            // 
            // buttonScrollUp
            // 
            this.buttonScrollUp.Location = new System.Drawing.Point(498, 3);
            this.buttonScrollUp.Name = "buttonScrollUp";
            this.buttonScrollUp.Size = new System.Drawing.Size(75, 23);
            this.buttonScrollUp.TabIndex = 0;
            this.buttonScrollUp.Text = "Scroll Up";
            this.buttonScrollUp.UseVisualStyleBackColor = true;
            this.buttonScrollUp.Click += new System.EventHandler(this.buttonScrollUp_Click);
            // 
            // buttonFileFast
            // 
            this.buttonFileFast.Location = new System.Drawing.Point(744, 89);
            this.buttonFileFast.Name = "buttonFileFast";
            this.buttonFileFast.Size = new System.Drawing.Size(75, 23);
            this.buttonFileFast.TabIndex = 0;
            this.buttonFileFast.Text = "Fast";
            this.buttonFileFast.UseVisualStyleBackColor = true;
            this.buttonFileFast.Click += new System.EventHandler(this.buttonFileFast_Click);
            // 
            // buttonFileStep
            // 
            this.buttonFileStep.Location = new System.Drawing.Point(744, 60);
            this.buttonFileStep.Name = "buttonFileStep";
            this.buttonFileStep.Size = new System.Drawing.Size(75, 23);
            this.buttonFileStep.TabIndex = 0;
            this.buttonFileStep.Text = "Step";
            this.buttonFileStep.UseVisualStyleBackColor = true;
            this.buttonFileStep.Click += new System.EventHandler(this.buttonFileStep_Click);
            // 
            // buttonFilePause
            // 
            this.buttonFilePause.Location = new System.Drawing.Point(744, 31);
            this.buttonFilePause.Name = "buttonFilePause";
            this.buttonFilePause.Size = new System.Drawing.Size(75, 23);
            this.buttonFilePause.TabIndex = 0;
            this.buttonFilePause.Text = "Pause";
            this.buttonFilePause.UseVisualStyleBackColor = true;
            this.buttonFilePause.Click += new System.EventHandler(this.buttonFilePause_Click);
            // 
            // buttonFilePlay
            // 
            this.buttonFilePlay.Location = new System.Drawing.Point(744, 3);
            this.buttonFilePlay.Name = "buttonFilePlay";
            this.buttonFilePlay.Size = new System.Drawing.Size(75, 23);
            this.buttonFilePlay.TabIndex = 0;
            this.buttonFilePlay.Text = "File Play";
            this.buttonFilePlay.UseVisualStyleBackColor = true;
            this.buttonFilePlay.Click += new System.EventHandler(this.buttonFilePlay_Click);
            // 
            // buttonClearEOL
            // 
            this.buttonClearEOL.Location = new System.Drawing.Point(663, 3);
            this.buttonClearEOL.Name = "buttonClearEOL";
            this.buttonClearEOL.Size = new System.Drawing.Size(75, 23);
            this.buttonClearEOL.TabIndex = 0;
            this.buttonClearEOL.Text = "K0 CEOL";
            this.buttonClearEOL.UseVisualStyleBackColor = true;
            this.buttonClearEOL.Click += new System.EventHandler(this.buttonClearEOL_Click);
            // 
            // buttonCLS
            // 
            this.buttonCLS.Location = new System.Drawing.Point(579, 60);
            this.buttonCLS.Name = "buttonCLS";
            this.buttonCLS.Size = new System.Drawing.Size(75, 23);
            this.buttonCLS.TabIndex = 0;
            this.buttonCLS.Text = "J2 CLS";
            this.buttonCLS.UseVisualStyleBackColor = true;
            this.buttonCLS.Click += new System.EventHandler(this.buttonCLS_Click);
            // 
            // buttonClearSOS
            // 
            this.buttonClearSOS.Location = new System.Drawing.Point(579, 31);
            this.buttonClearSOS.Name = "buttonClearSOS";
            this.buttonClearSOS.Size = new System.Drawing.Size(75, 23);
            this.buttonClearSOS.TabIndex = 0;
            this.buttonClearSOS.Text = "J1 ClearSOS";
            this.buttonClearSOS.UseVisualStyleBackColor = true;
            this.buttonClearSOS.Click += new System.EventHandler(this.buttonClearSOS_Click);
            // 
            // buttonClearEOS
            // 
            this.buttonClearEOS.Location = new System.Drawing.Point(579, 3);
            this.buttonClearEOS.Name = "buttonClearEOS";
            this.buttonClearEOS.Size = new System.Drawing.Size(75, 23);
            this.buttonClearEOS.TabIndex = 0;
            this.buttonClearEOS.Text = "J0 ClearEOS";
            this.buttonClearEOS.UseVisualStyleBackColor = true;
            this.buttonClearEOS.Click += new System.EventHandler(this.buttonClearEOS_Click);
            // 
            // buttonHome
            // 
            this.buttonHome.Location = new System.Drawing.Point(498, 31);
            this.buttonHome.Name = "buttonHome";
            this.buttonHome.Size = new System.Drawing.Size(75, 23);
            this.buttonHome.TabIndex = 0;
            this.buttonHome.Text = "Home";
            this.buttonHome.UseVisualStyleBackColor = true;
            this.buttonHome.Click += new System.EventHandler(this.buttonHome_Click);
            // 
            // buttonHCol
            // 
            this.buttonHCol.Location = new System.Drawing.Point(417, 3);
            this.buttonHCol.Name = "buttonHCol";
            this.buttonHCol.Size = new System.Drawing.Size(75, 23);
            this.buttonHCol.TabIndex = 0;
            this.buttonHCol.Text = "H Col";
            this.buttonHCol.UseVisualStyleBackColor = true;
            this.buttonHCol.Click += new System.EventHandler(this.buttonHCol_Click);
            // 
            // buttonVRight
            // 
            this.buttonVRight.Location = new System.Drawing.Point(383, 32);
            this.buttonVRight.Name = "buttonVRight";
            this.buttonVRight.Size = new System.Drawing.Size(75, 23);
            this.buttonVRight.TabIndex = 0;
            this.buttonVRight.Text = "Right";
            this.buttonVRight.UseVisualStyleBackColor = true;
            this.buttonVRight.Click += new System.EventHandler(this.buttonVRight_Click);
            // 
            // buttonNLine
            // 
            this.buttonNLine.Location = new System.Drawing.Point(177, 61);
            this.buttonNLine.Name = "buttonNLine";
            this.buttonNLine.Size = new System.Drawing.Size(75, 23);
            this.buttonNLine.TabIndex = 0;
            this.buttonNLine.Text = "Next Line";
            this.buttonNLine.UseVisualStyleBackColor = true;
            this.buttonNLine.Click += new System.EventHandler(this.buttonNLine_Click);
            // 
            // buttonPLine
            // 
            this.buttonPLine.Location = new System.Drawing.Point(177, 3);
            this.buttonPLine.Name = "buttonPLine";
            this.buttonPLine.Size = new System.Drawing.Size(75, 23);
            this.buttonPLine.TabIndex = 0;
            this.buttonPLine.Text = "Prev Line";
            this.buttonPLine.UseVisualStyleBackColor = true;
            this.buttonPLine.Click += new System.EventHandler(this.buttonPLine_Click);
            // 
            // buttonVLeft
            // 
            this.buttonVLeft.Location = new System.Drawing.Point(217, 32);
            this.buttonVLeft.Name = "buttonVLeft";
            this.buttonVLeft.Size = new System.Drawing.Size(75, 23);
            this.buttonVLeft.TabIndex = 0;
            this.buttonVLeft.Text = "Left";
            this.buttonVLeft.UseVisualStyleBackColor = true;
            this.buttonVLeft.Click += new System.EventHandler(this.buttonVLeft_Click);
            // 
            // buttonVDown
            // 
            this.buttonVDown.Location = new System.Drawing.Point(299, 61);
            this.buttonVDown.Name = "buttonVDown";
            this.buttonVDown.Size = new System.Drawing.Size(75, 23);
            this.buttonVDown.TabIndex = 0;
            this.buttonVDown.Text = "Down";
            this.buttonVDown.UseVisualStyleBackColor = true;
            this.buttonVDown.Click += new System.EventHandler(this.buttonVDown_Click);
            // 
            // buttonVUp
            // 
            this.buttonVUp.Location = new System.Drawing.Point(299, 3);
            this.buttonVUp.Name = "buttonVUp";
            this.buttonVUp.Size = new System.Drawing.Size(75, 23);
            this.buttonVUp.TabIndex = 0;
            this.buttonVUp.Text = "Up";
            this.buttonVUp.UseVisualStyleBackColor = true;
            this.buttonVUp.Click += new System.EventHandler(this.buttonVUp_Click);
            // 
            // buttonVTTextLF
            // 
            this.buttonVTTextLF.Location = new System.Drawing.Point(11, 3);
            this.buttonVTTextLF.Name = "buttonVTTextLF";
            this.buttonVTTextLF.Size = new System.Drawing.Size(75, 23);
            this.buttonVTTextLF.TabIndex = 0;
            this.buttonVTTextLF.Text = "TextLF";
            this.buttonVTTextLF.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonT1);
            this.panel1.Controls.Add(this.buttonChar);
            this.panel1.Controls.Add(this.buttonText);
            this.panel1.Controls.Add(this.buttonDownScroll);
            this.panel1.Controls.Add(this.buttonUpScroll);
            this.panel1.Controls.Add(this.buttonLF);
            this.panel1.Controls.Add(this.buttonVertResize);
            this.panel1.Controls.Add(this.buttonCRText);
            this.panel1.Controls.Add(this.buttonCR);
            this.panel1.Controls.Add(this.buttonCursorBlink);
            this.panel1.Controls.Add(this.buttonCursorBlock);
            this.panel1.Controls.Add(this.buttonCRLF);
            this.panel1.Controls.Add(this.buttonCursorLine);
            this.panel1.Controls.Add(this.buttonLFText);
            this.panel1.Controls.Add(this.buttonTextBS);
            this.panel1.Controls.Add(this.buttonCursoroff);
            this.panel1.Controls.Add(this.buttonTextWithBS);
            this.panel1.Controls.Add(this.buttonResize);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 100);
            this.panel1.TabIndex = 3;
            // 
            // buttonT1
            // 
            this.buttonT1.Location = new System.Drawing.Point(9, 3);
            this.buttonT1.Name = "buttonT1";
            this.buttonT1.Size = new System.Drawing.Size(75, 23);
            this.buttonT1.TabIndex = 0;
            this.buttonT1.Text = "TextLF";
            this.buttonT1.UseVisualStyleBackColor = true;
            this.buttonT1.Click += new System.EventHandler(this.buttonT1_Click);
            // 
            // buttonChar
            // 
            this.buttonChar.Location = new System.Drawing.Point(90, 32);
            this.buttonChar.Name = "buttonChar";
            this.buttonChar.Size = new System.Drawing.Size(75, 23);
            this.buttonChar.TabIndex = 0;
            this.buttonChar.Text = "Char";
            this.buttonChar.UseVisualStyleBackColor = true;
            this.buttonChar.Click += new System.EventHandler(this.buttonChar_Click);
            // 
            // buttonText
            // 
            this.buttonText.Location = new System.Drawing.Point(9, 32);
            this.buttonText.Name = "buttonText";
            this.buttonText.Size = new System.Drawing.Size(75, 23);
            this.buttonText.TabIndex = 0;
            this.buttonText.Text = "Text";
            this.buttonText.UseVisualStyleBackColor = true;
            this.buttonText.Click += new System.EventHandler(this.buttonText_Click);
            // 
            // buttonDownScroll
            // 
            this.buttonDownScroll.Location = new System.Drawing.Point(283, 32);
            this.buttonDownScroll.Name = "buttonDownScroll";
            this.buttonDownScroll.Size = new System.Drawing.Size(75, 23);
            this.buttonDownScroll.TabIndex = 0;
            this.buttonDownScroll.Text = "Down&scroll";
            this.buttonDownScroll.UseVisualStyleBackColor = true;
            this.buttonDownScroll.Click += new System.EventHandler(this.buttonDownScroll_Click);
            // 
            // buttonUpScroll
            // 
            this.buttonUpScroll.Location = new System.Drawing.Point(283, 3);
            this.buttonUpScroll.Name = "buttonUpScroll";
            this.buttonUpScroll.Size = new System.Drawing.Size(75, 23);
            this.buttonUpScroll.TabIndex = 0;
            this.buttonUpScroll.Text = "Up&scroll";
            this.buttonUpScroll.UseVisualStyleBackColor = true;
            this.buttonUpScroll.Click += new System.EventHandler(this.buttonUpScroll_Click);
            // 
            // buttonLF
            // 
            this.buttonLF.Location = new System.Drawing.Point(364, 3);
            this.buttonLF.Name = "buttonLF";
            this.buttonLF.Size = new System.Drawing.Size(75, 23);
            this.buttonLF.TabIndex = 0;
            this.buttonLF.Text = "LF";
            this.buttonLF.UseVisualStyleBackColor = true;
            this.buttonLF.Click += new System.EventHandler(this.buttonLF_Click);
            // 
            // buttonVertResize
            // 
            this.buttonVertResize.Location = new System.Drawing.Point(689, 32);
            this.buttonVertResize.Name = "buttonVertResize";
            this.buttonVertResize.Size = new System.Drawing.Size(75, 23);
            this.buttonVertResize.TabIndex = 0;
            this.buttonVertResize.Text = "Vert Resize";
            this.buttonVertResize.UseVisualStyleBackColor = true;
            this.buttonVertResize.Click += new System.EventHandler(this.buttonVertResize_Click);
            // 
            // buttonCRText
            // 
            this.buttonCRText.Location = new System.Drawing.Point(171, 61);
            this.buttonCRText.Name = "buttonCRText";
            this.buttonCRText.Size = new System.Drawing.Size(75, 23);
            this.buttonCRText.TabIndex = 0;
            this.buttonCRText.Text = "CR";
            this.buttonCRText.UseVisualStyleBackColor = true;
            this.buttonCRText.Click += new System.EventHandler(this.buttonCRText_Click);
            // 
            // buttonCR
            // 
            this.buttonCR.Location = new System.Drawing.Point(364, 32);
            this.buttonCR.Name = "buttonCR";
            this.buttonCR.Size = new System.Drawing.Size(75, 23);
            this.buttonCR.TabIndex = 0;
            this.buttonCR.Text = "CR";
            this.buttonCR.UseVisualStyleBackColor = true;
            this.buttonCR.Click += new System.EventHandler(this.buttonCR_Click);
            // 
            // buttonCursorBlink
            // 
            this.buttonCursorBlink.Location = new System.Drawing.Point(557, 3);
            this.buttonCursorBlink.Name = "buttonCursorBlink";
            this.buttonCursorBlink.Size = new System.Drawing.Size(75, 23);
            this.buttonCursorBlink.TabIndex = 0;
            this.buttonCursorBlink.Text = "Cursor Blink";
            this.buttonCursorBlink.UseVisualStyleBackColor = true;
            this.buttonCursorBlink.Click += new System.EventHandler(this.buttonCursorBlink_Click);
            // 
            // buttonCursorBlock
            // 
            this.buttonCursorBlock.Location = new System.Drawing.Point(476, 61);
            this.buttonCursorBlock.Name = "buttonCursorBlock";
            this.buttonCursorBlock.Size = new System.Drawing.Size(75, 23);
            this.buttonCursorBlock.TabIndex = 0;
            this.buttonCursorBlock.Text = "Cursor Block";
            this.buttonCursorBlock.UseVisualStyleBackColor = true;
            this.buttonCursorBlock.Click += new System.EventHandler(this.buttonCursorBlock_Click);
            // 
            // buttonCRLF
            // 
            this.buttonCRLF.Location = new System.Drawing.Point(364, 61);
            this.buttonCRLF.Name = "buttonCRLF";
            this.buttonCRLF.Size = new System.Drawing.Size(75, 23);
            this.buttonCRLF.TabIndex = 0;
            this.buttonCRLF.Text = "CRLF";
            this.buttonCRLF.UseVisualStyleBackColor = true;
            this.buttonCRLF.Click += new System.EventHandler(this.buttonCRLF_Click);
            // 
            // buttonCursorLine
            // 
            this.buttonCursorLine.Location = new System.Drawing.Point(476, 32);
            this.buttonCursorLine.Name = "buttonCursorLine";
            this.buttonCursorLine.Size = new System.Drawing.Size(75, 23);
            this.buttonCursorLine.TabIndex = 0;
            this.buttonCursorLine.Text = "Cursor Line";
            this.buttonCursorLine.UseVisualStyleBackColor = true;
            this.buttonCursorLine.Click += new System.EventHandler(this.buttonCursorLine_Click);
            // 
            // buttonLFText
            // 
            this.buttonLFText.Location = new System.Drawing.Point(90, 62);
            this.buttonLFText.Name = "buttonLFText";
            this.buttonLFText.Size = new System.Drawing.Size(75, 23);
            this.buttonLFText.TabIndex = 0;
            this.buttonLFText.Text = "LF";
            this.buttonLFText.UseVisualStyleBackColor = true;
            this.buttonLFText.Click += new System.EventHandler(this.buttonLFText_Click);
            // 
            // buttonTextBS
            // 
            this.buttonTextBS.Location = new System.Drawing.Point(9, 62);
            this.buttonTextBS.Name = "buttonTextBS";
            this.buttonTextBS.Size = new System.Drawing.Size(75, 23);
            this.buttonTextBS.TabIndex = 0;
            this.buttonTextBS.Text = "BS";
            this.buttonTextBS.UseVisualStyleBackColor = true;
            this.buttonTextBS.Click += new System.EventHandler(this.buttonTextBS_Click);
            // 
            // buttonCursoroff
            // 
            this.buttonCursoroff.Location = new System.Drawing.Point(476, 3);
            this.buttonCursoroff.Name = "buttonCursoroff";
            this.buttonCursoroff.Size = new System.Drawing.Size(75, 23);
            this.buttonCursoroff.TabIndex = 0;
            this.buttonCursoroff.Text = "Cursor Off";
            this.buttonCursoroff.UseVisualStyleBackColor = true;
            this.buttonCursoroff.Click += new System.EventHandler(this.buttonCursoroff_Click);
            // 
            // buttonTextWithBS
            // 
            this.buttonTextWithBS.Location = new System.Drawing.Point(90, 3);
            this.buttonTextWithBS.Name = "buttonTextWithBS";
            this.buttonTextWithBS.Size = new System.Drawing.Size(75, 23);
            this.buttonTextWithBS.TabIndex = 0;
            this.buttonTextWithBS.Text = "text BS Text";
            this.buttonTextWithBS.UseVisualStyleBackColor = true;
            this.buttonTextWithBS.Click += new System.EventHandler(this.buttonTextWithBS_Click);
            // 
            // buttonResize
            // 
            this.buttonResize.Location = new System.Drawing.Point(689, 3);
            this.buttonResize.Name = "buttonResize";
            this.buttonResize.Size = new System.Drawing.Size(75, 23);
            this.buttonResize.TabIndex = 0;
            this.buttonResize.Text = "Horz Resize";
            this.buttonResize.UseVisualStyleBackColor = true;
            this.buttonResize.Click += new System.EventHandler(this.buttonResize_Click);
            // 
            // terminal
            // 
            this.terminal.BackColor = System.Drawing.Color.Maroon;
            this.terminal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.terminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.terminal.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.terminal.Location = new System.Drawing.Point(0, 223);
            this.terminal.Name = "terminal";
            this.terminal.Size = new System.Drawing.Size(1184, 438);
            this.terminal.TabIndex = 0;
            this.terminal.VTBackColor = System.Drawing.Color.Black;
            this.terminal.VTForeColor = System.Drawing.Color.White;
            // 
            // TestTerminal1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.terminal);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TestTerminal1";
            this.Text = "TestTerminal1";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Terminal terminal;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBoxForeColour;
        private System.Windows.Forms.TextBox textBoxCursorN;
        private System.Windows.Forms.Button buttonClearLine;
        private System.Windows.Forms.Button buttonClearSOL;
        private System.Windows.Forms.Button buttonScrollDown;
        private System.Windows.Forms.Button buttonScrollUp;
        private System.Windows.Forms.Button buttonClearEOL;
        private System.Windows.Forms.Button buttonCLS;
        private System.Windows.Forms.Button buttonClearSOS;
        private System.Windows.Forms.Button buttonClearEOS;
        private System.Windows.Forms.Button buttonHome;
        private System.Windows.Forms.Button buttonHCol;
        private System.Windows.Forms.Button buttonVRight;
        private System.Windows.Forms.Button buttonNLine;
        private System.Windows.Forms.Button buttonPLine;
        private System.Windows.Forms.Button buttonVLeft;
        private System.Windows.Forms.Button buttonVDown;
        private System.Windows.Forms.Button buttonVUp;
        private System.Windows.Forms.Button buttonVTTextLF;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonT1;
        private System.Windows.Forms.Button buttonText;
        private System.Windows.Forms.Button buttonLF;
        private System.Windows.Forms.Button buttonVertResize;
        private System.Windows.Forms.Button buttonCR;
        private System.Windows.Forms.Button buttonCursorBlock;
        private System.Windows.Forms.Button buttonCRLF;
        private System.Windows.Forms.Button buttonCursorLine;
        private System.Windows.Forms.Button buttonTextBS;
        private System.Windows.Forms.Button buttonCursoroff;
        private System.Windows.Forms.Button buttonTextWithBS;
        private System.Windows.Forms.Button buttonResize;
        private System.Windows.Forms.Button buttonLFText;
        private System.Windows.Forms.Button buttonChar;
        private System.Windows.Forms.Button buttonCRText;
        private System.Windows.Forms.ComboBox comboBoxBackColour;
        private System.Windows.Forms.Button buttonCursorBlink;
        private System.Windows.Forms.Button buttonDownScroll;
        private System.Windows.Forms.Button buttonUpScroll;
        private System.Windows.Forms.Button buttonFilePlay;
        private System.Windows.Forms.Button buttonFilePause;
        private System.Windows.Forms.Button buttonFileStep;
        private System.Windows.Forms.Button buttonFileFast;
    }
}