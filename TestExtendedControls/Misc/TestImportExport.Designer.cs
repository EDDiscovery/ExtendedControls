namespace TestExtendedControls
{
    partial class TestImportExport
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.extButtonImportJson = new ExtendedControls.ExtButton();
            this.extButtonExportMin = new ExtendedControls.ExtButton();
            this.extButtonExportDTUTC = new ExtendedControls.ExtButton();
            this.extButtonExportDT = new ExtendedControls.ExtButton();
            this.extButtonExport = new ExtendedControls.ExtButton();
            this.extButtonImportSaveAs = new ExtendedControls.ExtButton();
            this.SuspendLayout();
            // 
            // extButtonImportJson
            // 
            this.extButtonImportJson.Location = new System.Drawing.Point(31, 71);
            this.extButtonImportJson.Name = "extButtonImportJson";
            this.extButtonImportJson.Size = new System.Drawing.Size(75, 23);
            this.extButtonImportJson.TabIndex = 0;
            this.extButtonImportJson.Text = "Import JSON";
            this.extButtonImportJson.UseVisualStyleBackColor = true;
            this.extButtonImportJson.Click += new System.EventHandler(this.extButtonImportJson_Click);
            // 
            // extButtonExportMin
            // 
            this.extButtonExportMin.Location = new System.Drawing.Point(291, 28);
            this.extButtonExportMin.Name = "extButtonExportMin";
            this.extButtonExportMin.Size = new System.Drawing.Size(75, 23);
            this.extButtonExportMin.TabIndex = 0;
            this.extButtonExportMin.Text = "Export Min";
            this.extButtonExportMin.UseVisualStyleBackColor = true;
            this.extButtonExportMin.Click += new System.EventHandler(this.extButtonExportMin_Click);
            // 
            // extButtonExportDTUTC
            // 
            this.extButtonExportDTUTC.Location = new System.Drawing.Point(193, 28);
            this.extButtonExportDTUTC.Name = "extButtonExportDTUTC";
            this.extButtonExportDTUTC.Size = new System.Drawing.Size(92, 23);
            this.extButtonExportDTUTC.TabIndex = 0;
            this.extButtonExportDTUTC.Text = "Export DT UTC";
            this.extButtonExportDTUTC.UseVisualStyleBackColor = true;
            this.extButtonExportDTUTC.Click += new System.EventHandler(this.extButtonExportDTUTC_Click);
            // 
            // extButtonExportDT
            // 
            this.extButtonExportDT.Location = new System.Drawing.Point(112, 28);
            this.extButtonExportDT.Name = "extButtonExportDT";
            this.extButtonExportDT.Size = new System.Drawing.Size(75, 23);
            this.extButtonExportDT.TabIndex = 0;
            this.extButtonExportDT.Text = "Export DT";
            this.extButtonExportDT.UseVisualStyleBackColor = true;
            this.extButtonExportDT.Click += new System.EventHandler(this.extButtonExportDT_Click);
            // 
            // extButtonExport
            // 
            this.extButtonExport.Location = new System.Drawing.Point(31, 28);
            this.extButtonExport.Name = "extButtonExport";
            this.extButtonExport.Size = new System.Drawing.Size(75, 23);
            this.extButtonExport.TabIndex = 0;
            this.extButtonExport.Text = "Export";
            this.extButtonExport.UseVisualStyleBackColor = true;
            this.extButtonExport.Click += new System.EventHandler(this.extButtonExport_Click);
            // 
            // extButtonImportSaveAs
            // 
            this.extButtonImportSaveAs.Location = new System.Drawing.Point(112, 71);
            this.extButtonImportSaveAs.Name = "extButtonImportSaveAs";
            this.extButtonImportSaveAs.Size = new System.Drawing.Size(75, 23);
            this.extButtonImportSaveAs.TabIndex = 0;
            this.extButtonImportSaveAs.Text = "Import SaveAs";
            this.extButtonImportSaveAs.UseVisualStyleBackColor = true;
            this.extButtonImportSaveAs.Click += new System.EventHandler(this.extButtonImportSaveAs_Click);
            // 
            // TestImportExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 509);
            this.Controls.Add(this.extButtonImportSaveAs);
            this.Controls.Add(this.extButtonImportJson);
            this.Controls.Add(this.extButtonExportMin);
            this.Controls.Add(this.extButtonExportDTUTC);
            this.Controls.Add(this.extButtonExportDT);
            this.Controls.Add(this.extButtonExport);
            this.Name = "TestImportExport";
            this.Text = "TestImportExport";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private ExtendedControls.ExtButton extButtonExport;
        private ExtendedControls.ExtButton extButtonImportJson;
        private ExtendedControls.ExtButton extButtonExportMin;
        private ExtendedControls.ExtButton extButtonExportDT;
        private ExtendedControls.ExtButton extButtonExportDTUTC;
        private ExtendedControls.ExtButton extButtonImportSaveAs;
    }
}