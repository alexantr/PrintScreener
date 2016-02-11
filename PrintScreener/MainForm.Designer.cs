namespace PrintScreener
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelPath = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelFormat = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.labelInterval = new System.Windows.Forms.Label();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.checkBoxAutoCapture = new System.Windows.Forms.CheckBox();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelSeconds = new System.Windows.Forms.Label();
            this.checkBoxHideWindow = new System.Windows.Forms.CheckBox();
            this.numericUpDownQuality = new System.Windows.Forms.NumericUpDown();
            this.labelQuality = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.buttonOpenFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.groupBoxLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuality)).BeginInit();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(103, 21);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(237, 22);
            this.textBoxPath.TabIndex = 1;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(346, 20);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(30, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "...";
            this.toolTip.SetToolTip(this.buttonBrowse, "Browse output folder");
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(6, 24);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(79, 13);
            this.labelPath.TabIndex = 0;
            this.labelPath.Text = "Output folder";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(6, 51);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(56, 13);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "File name";
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new System.Drawing.Point(103, 75);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(80, 21);
            this.comboBoxFormat.TabIndex = 6;
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormat_SelectedIndexChanged);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(103, 48);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(237, 22);
            this.textBoxName.TabIndex = 4;
            this.toolTip.SetToolTip(this.textBoxName, "Use %date% and %time% which will be replaced with current date and time");
            // 
            // labelFormat
            // 
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new System.Drawing.Point(6, 78);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(62, 13);
            this.labelFormat.TabIndex = 5;
            this.labelFormat.Text = "File format";
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Location = new System.Drawing.Point(103, 102);
            this.numericUpDownInterval.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numericUpDownInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(60, 22);
            this.numericUpDownInterval.TabIndex = 10;
            this.numericUpDownInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(6, 104);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(89, 13);
            this.labelInterval.TabIndex = 9;
            this.labelInterval.Text = "Capture interval";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLog.Location = new System.Drawing.Point(3, 17);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(376, 70);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            // 
            // checkBoxAutoCapture
            // 
            this.checkBoxAutoCapture.AutoSize = true;
            this.checkBoxAutoCapture.Location = new System.Drawing.Point(223, 242);
            this.checkBoxAutoCapture.Name = "checkBoxAutoCapture";
            this.checkBoxAutoCapture.Size = new System.Drawing.Size(167, 17);
            this.checkBoxAutoCapture.TabIndex = 3;
            this.checkBoxAutoCapture.Text = "Capture PrintScreen button";
            this.checkBoxAutoCapture.UseVisualStyleBackColor = true;
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.richTextBoxLog);
            this.groupBoxLog.Location = new System.Drawing.Point(8, 146);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxLog.Size = new System.Drawing.Size(382, 90);
            this.groupBoxLog.TabIndex = 1;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Log";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(89, 270);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 25);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "Stop";
            this.toolTip.SetToolTip(this.buttonStop, "Stop auto capturing");
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(8, 270);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 25);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Start";
            this.toolTip.SetToolTip(this.buttonStart, "Start auto capturing");
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelSeconds
            // 
            this.labelSeconds.AutoSize = true;
            this.labelSeconds.Location = new System.Drawing.Point(169, 104);
            this.labelSeconds.Name = "labelSeconds";
            this.labelSeconds.Size = new System.Drawing.Size(49, 13);
            this.labelSeconds.TabIndex = 11;
            this.labelSeconds.Text = "seconds";
            // 
            // checkBoxHideWindow
            // 
            this.checkBoxHideWindow.AutoSize = true;
            this.checkBoxHideWindow.Location = new System.Drawing.Point(9, 242);
            this.checkBoxHideWindow.Name = "checkBoxHideWindow";
            this.checkBoxHideWindow.Size = new System.Drawing.Size(165, 17);
            this.checkBoxHideWindow.TabIndex = 2;
            this.checkBoxHideWindow.Text = "Hide window after starting";
            this.toolTip.SetToolTip(this.checkBoxHideWindow, "Hide window when auto capturing is started");
            this.checkBoxHideWindow.UseVisualStyleBackColor = true;
            // 
            // numericUpDownQuality
            // 
            this.numericUpDownQuality.Location = new System.Drawing.Point(280, 75);
            this.numericUpDownQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownQuality.Name = "numericUpDownQuality";
            this.numericUpDownQuality.Size = new System.Drawing.Size(60, 22);
            this.numericUpDownQuality.TabIndex = 8;
            this.numericUpDownQuality.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // labelQuality
            // 
            this.labelQuality.AutoSize = true;
            this.labelQuality.Location = new System.Drawing.Point(205, 78);
            this.labelQuality.Name = "labelQuality";
            this.labelQuality.Size = new System.Drawing.Size(69, 13);
            this.labelQuality.TabIndex = 7;
            this.labelQuality.Text = "JPEG quality";
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.labelPath);
            this.groupBoxSettings.Controls.Add(this.textBoxPath);
            this.groupBoxSettings.Controls.Add(this.buttonBrowse);
            this.groupBoxSettings.Controls.Add(this.labelName);
            this.groupBoxSettings.Controls.Add(this.textBoxName);
            this.groupBoxSettings.Controls.Add(this.labelFormat);
            this.groupBoxSettings.Controls.Add(this.comboBoxFormat);
            this.groupBoxSettings.Controls.Add(this.labelQuality);
            this.groupBoxSettings.Controls.Add(this.numericUpDownQuality);
            this.groupBoxSettings.Controls.Add(this.labelInterval);
            this.groupBoxSettings.Controls.Add(this.numericUpDownInterval);
            this.groupBoxSettings.Controls.Add(this.labelSeconds);
            this.groupBoxSettings.Location = new System.Drawing.Point(8, 8);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(382, 132);
            this.groupBoxSettings.TabIndex = 0;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.Location = new System.Drawing.Point(300, 270);
            this.buttonOpenFolder.Name = "buttonOpenFolder";
            this.buttonOpenFolder.Size = new System.Drawing.Size(90, 25);
            this.buttonOpenFolder.TabIndex = 6;
            this.buttonOpenFolder.Text = "Open folder";
            this.toolTip.SetToolTip(this.buttonOpenFolder, "Open output folder");
            this.buttonOpenFolder.UseVisualStyleBackColor = true;
            this.buttonOpenFolder.Click += new System.EventHandler(this.buttonOpenFolder_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 303);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.checkBoxHideWindow);
            this.Controls.Add(this.checkBoxAutoCapture);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonOpenFolder);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "PrintScreener";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.groupBoxLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuality)).EndInit();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelFormat;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.CheckBox checkBoxAutoCapture;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelSeconds;
        private System.Windows.Forms.CheckBox checkBoxHideWindow;
        private System.Windows.Forms.NumericUpDown numericUpDownQuality;
        private System.Windows.Forms.Label labelQuality;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Button buttonOpenFolder;
    }
}

