namespace PrintScreener
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            groupBoxOptions = new GroupBox();
            labelSeconds = new Label();
            numericInterval = new NumericUpDown();
            numericQuality = new NumericUpDown();
            comboBoxFormat = new ComboBox();
            textBoxName = new TextBox();
            buttonBrowse = new Button();
            textBoxPath = new TextBox();
            labelInterval = new Label();
            labelQuality = new Label();
            labelFormat = new Label();
            labelName = new Label();
            labelPath = new Label();
            checkBoxMonitorClipboard = new CheckBox();
            groupBoxLog = new GroupBox();
            richTextBoxLog = new RichTextBox();
            checkBoxHideWindow = new CheckBox();
            buttonStart = new Button();
            buttonStop = new Button();
            buttonOpenFolder = new Button();
            toolTip = new ToolTip(components);
            groupBoxOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericInterval).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericQuality).BeginInit();
            groupBoxLog.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxOptions
            // 
            groupBoxOptions.Controls.Add(labelSeconds);
            groupBoxOptions.Controls.Add(numericInterval);
            groupBoxOptions.Controls.Add(numericQuality);
            groupBoxOptions.Controls.Add(comboBoxFormat);
            groupBoxOptions.Controls.Add(textBoxName);
            groupBoxOptions.Controls.Add(buttonBrowse);
            groupBoxOptions.Controls.Add(textBoxPath);
            groupBoxOptions.Controls.Add(labelInterval);
            groupBoxOptions.Controls.Add(labelQuality);
            groupBoxOptions.Controls.Add(labelFormat);
            groupBoxOptions.Controls.Add(labelName);
            groupBoxOptions.Controls.Add(labelPath);
            groupBoxOptions.Location = new Point(12, 12);
            groupBoxOptions.Name = "groupBoxOptions";
            groupBoxOptions.Size = new Size(594, 186);
            groupBoxOptions.TabIndex = 0;
            groupBoxOptions.TabStop = false;
            groupBoxOptions.Text = "Options";
            // 
            // labelSeconds
            // 
            labelSeconds.AutoSize = true;
            labelSeconds.Location = new Point(291, 145);
            labelSeconds.Name = "labelSeconds";
            labelSeconds.Size = new Size(77, 25);
            labelSeconds.TabIndex = 11;
            labelSeconds.Text = "seconds";
            // 
            // numericInterval
            // 
            numericInterval.Location = new Point(195, 143);
            numericInterval.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numericInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericInterval.Name = "numericInterval";
            numericInterval.Size = new Size(90, 31);
            numericInterval.TabIndex = 10;
            toolTip.SetToolTip(numericInterval, "Max 3600 seconds (1 hour)");
            numericInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // numericQuality
            // 
            numericQuality.Location = new Point(444, 106);
            numericQuality.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericQuality.Name = "numericQuality";
            numericQuality.Size = new Size(90, 31);
            numericQuality.TabIndex = 8;
            toolTip.SetToolTip(numericQuality, "From 1 to 100");
            numericQuality.Value = new decimal(new int[] { 80, 0, 0, 0 });
            // 
            // comboBoxFormat
            // 
            comboBoxFormat.FormattingEnabled = true;
            comboBoxFormat.Location = new Point(133, 104);
            comboBoxFormat.Name = "comboBoxFormat";
            comboBoxFormat.Size = new Size(129, 33);
            comboBoxFormat.TabIndex = 6;
            comboBoxFormat.SelectedIndexChanged += FormatIndexChanged;
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(133, 67);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(401, 31);
            textBoxName.TabIndex = 4;
            toolTip.SetToolTip(textBoxName, "Use %date% and %time% which will be replaced with current date and time");
            // 
            // buttonBrowse
            // 
            buttonBrowse.Location = new Point(540, 30);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(48, 34);
            buttonBrowse.TabIndex = 2;
            buttonBrowse.Text = "...";
            toolTip.SetToolTip(buttonBrowse, "Browse folder");
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += BrowseBtnClick;
            // 
            // textBoxPath
            // 
            textBoxPath.Location = new Point(133, 30);
            textBoxPath.Name = "textBoxPath";
            textBoxPath.Size = new Size(401, 31);
            textBoxPath.TabIndex = 1;
            // 
            // labelInterval
            // 
            labelInterval.AutoSize = true;
            labelInterval.Location = new Point(6, 145);
            labelInterval.Name = "labelInterval";
            labelInterval.Size = new Size(183, 25);
            labelInterval.TabIndex = 9;
            labelInterval.Text = "Take screenshot every";
            // 
            // labelQuality
            // 
            labelQuality.AutoSize = true;
            labelQuality.Location = new Point(331, 108);
            labelQuality.Name = "labelQuality";
            labelQuality.Size = new Size(107, 25);
            labelQuality.TabIndex = 7;
            labelQuality.Text = "JPEG quality";
            // 
            // labelFormat
            // 
            labelFormat.AutoSize = true;
            labelFormat.Location = new Point(6, 108);
            labelFormat.Name = "labelFormat";
            labelFormat.Size = new Size(121, 25);
            labelFormat.TabIndex = 5;
            labelFormat.Text = "Image format";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(6, 70);
            labelName.Name = "labelName";
            labelName.Size = new Size(87, 25);
            labelName.TabIndex = 3;
            labelName.Text = "File name";
            // 
            // labelPath
            // 
            labelPath.AutoSize = true;
            labelPath.Location = new Point(6, 35);
            labelPath.Name = "labelPath";
            labelPath.Size = new Size(121, 25);
            labelPath.TabIndex = 0;
            labelPath.Text = "Output folder";
            // 
            // checkBoxMonitorClipboard
            // 
            checkBoxMonitorClipboard.AutoSize = true;
            checkBoxMonitorClipboard.Location = new Point(424, 396);
            checkBoxMonitorClipboard.Name = "checkBoxMonitorClipboard";
            checkBoxMonitorClipboard.Size = new Size(182, 29);
            checkBoxMonitorClipboard.TabIndex = 3;
            checkBoxMonitorClipboard.Text = "Monitor clipboard";
            checkBoxMonitorClipboard.UseVisualStyleBackColor = true;
            // 
            // groupBoxLog
            // 
            groupBoxLog.Controls.Add(richTextBoxLog);
            groupBoxLog.Location = new Point(12, 204);
            groupBoxLog.Name = "groupBoxLog";
            groupBoxLog.Size = new Size(594, 186);
            groupBoxLog.TabIndex = 1;
            groupBoxLog.TabStop = false;
            groupBoxLog.Text = "Log";
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.BorderStyle = BorderStyle.None;
            richTextBoxLog.Location = new Point(6, 30);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.ReadOnly = true;
            richTextBoxLog.Size = new Size(582, 150);
            richTextBoxLog.TabIndex = 0;
            richTextBoxLog.Text = "";
            // 
            // checkBoxHideWindow
            // 
            checkBoxHideWindow.AutoSize = true;
            checkBoxHideWindow.Location = new Point(12, 396);
            checkBoxHideWindow.Name = "checkBoxHideWindow";
            checkBoxHideWindow.Size = new Size(223, 29);
            checkBoxHideWindow.TabIndex = 2;
            checkBoxHideWindow.Text = "Hide window after start";
            toolTip.SetToolTip(checkBoxHideWindow, "Hide window when auto capturing starts");
            checkBoxHideWindow.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(12, 438);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(112, 34);
            buttonStart.TabIndex = 4;
            buttonStart.Text = "Start";
            toolTip.SetToolTip(buttonStart, "Start auto capturing");
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += StartBtnClick;
            // 
            // buttonStop
            // 
            buttonStop.Enabled = false;
            buttonStop.Location = new Point(130, 438);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(112, 34);
            buttonStop.TabIndex = 5;
            buttonStop.Text = "Stop";
            toolTip.SetToolTip(buttonStop, "Stop auto capturing");
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += StopBtnClick;
            // 
            // buttonOpenFolder
            // 
            buttonOpenFolder.Location = new Point(457, 438);
            buttonOpenFolder.Name = "buttonOpenFolder";
            buttonOpenFolder.Size = new Size(149, 34);
            buttonOpenFolder.TabIndex = 6;
            buttonOpenFolder.Text = "Open folder";
            toolTip.SetToolTip(buttonOpenFolder, "Open folder with screenshots");
            buttonOpenFolder.UseVisualStyleBackColor = true;
            buttonOpenFolder.Click += OpenFolderBtnClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(618, 484);
            Controls.Add(checkBoxMonitorClipboard);
            Controls.Add(buttonOpenFolder);
            Controls.Add(buttonStop);
            Controls.Add(buttonStart);
            Controls.Add(checkBoxHideWindow);
            Controls.Add(groupBoxLog);
            Controls.Add(groupBoxOptions);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            Text = "PrintScreener";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            groupBoxOptions.ResumeLayout(false);
            groupBoxOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericInterval).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericQuality).EndInit();
            groupBoxLog.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBoxOptions;
        private GroupBox groupBoxLog;
        private CheckBox checkBoxHideWindow;
        private Button buttonStart;
        private Button buttonStop;
        private Button buttonOpenFolder;
        private CheckBox checkBoxMonitorClipboard;
        private Label labelSeconds;
        private NumericUpDown numericInterval;
        private NumericUpDown numericQuality;
        private ComboBox comboBoxFormat;
        private TextBox textBoxName;
        private Button buttonBrowse;
        private TextBox textBoxPath;
        private Label labelInterval;
        private Label labelQuality;
        private Label labelFormat;
        private Label labelName;
        private Label labelPath;
        private RichTextBox richTextBoxLog;
        private ToolTip toolTip;
    }
}
