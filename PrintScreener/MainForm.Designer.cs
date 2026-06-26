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
            contextMenuOptions = new ContextMenuStrip(components);
            resetOptionsMenuItem = new ToolStripMenuItem();
            resetCounterMenuItem = new ToolStripMenuItem();
            textBoxArea = new TextBox();
            buttonResetArea = new Button();
            labelArea = new Label();
            buttonSelectArea = new Button();
            labelSeconds = new Label();
            numericInterval = new NumericUpDown();
            numericQuality = new NumericUpDown();
            comboBoxType = new ComboBox();
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
            contextMenuLog = new ContextMenuStrip(components);
            clearLogMenuItem = new ToolStripMenuItem();
            saveLogMenuItem = new ToolStripMenuItem();
            richTextBoxLog = new RichTextBox();
            checkBoxHideWindow = new CheckBox();
            buttonStart = new Button();
            buttonStop = new Button();
            buttonOpenFolder = new Button();
            toolTip = new ToolTip(components);
            buttonShot = new Button();
            groupBoxOptions.SuspendLayout();
            contextMenuOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericInterval).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericQuality).BeginInit();
            groupBoxLog.SuspendLayout();
            contextMenuLog.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxOptions
            // 
            groupBoxOptions.ContextMenuStrip = contextMenuOptions;
            groupBoxOptions.Controls.Add(textBoxArea);
            groupBoxOptions.Controls.Add(buttonResetArea);
            groupBoxOptions.Controls.Add(labelArea);
            groupBoxOptions.Controls.Add(buttonSelectArea);
            groupBoxOptions.Controls.Add(labelSeconds);
            groupBoxOptions.Controls.Add(numericInterval);
            groupBoxOptions.Controls.Add(numericQuality);
            groupBoxOptions.Controls.Add(comboBoxType);
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
            groupBoxOptions.Size = new Size(594, 225);
            groupBoxOptions.TabIndex = 0;
            groupBoxOptions.TabStop = false;
            groupBoxOptions.Text = "Options";
            // 
            // contextMenuOptions
            // 
            contextMenuOptions.ImageScalingSize = new Size(24, 24);
            contextMenuOptions.Items.AddRange(new ToolStripItem[] { resetOptionsMenuItem, resetCounterMenuItem });
            contextMenuOptions.Name = "contextMenuOptions";
            contextMenuOptions.Size = new Size(263, 68);
            // 
            // resetOptionsMenuItem
            // 
            resetOptionsMenuItem.Name = "resetOptionsMenuItem";
            resetOptionsMenuItem.Size = new Size(262, 32);
            resetOptionsMenuItem.Text = "Reset options";
            resetOptionsMenuItem.Click += ResetOptionsMenuItem_Click;
            // 
            // resetCounterMenuItem
            // 
            resetCounterMenuItem.Name = "resetCounterMenuItem";
            resetCounterMenuItem.Size = new Size(262, 32);
            resetCounterMenuItem.Text = "Reset counter %num%";
            resetCounterMenuItem.Click += ResetCounterMenuItem_Click;
            // 
            // textBoxArea
            // 
            textBoxArea.BackColor = SystemColors.Control;
            textBoxArea.BorderStyle = BorderStyle.None;
            textBoxArea.Location = new Point(133, 185);
            textBoxArea.Name = "textBoxArea";
            textBoxArea.ReadOnly = true;
            textBoxArea.Size = new Size(219, 24);
            textBoxArea.TabIndex = 13;
            // 
            // buttonResetArea
            // 
            buttonResetArea.Enabled = false;
            buttonResetArea.Location = new Point(476, 180);
            buttonResetArea.Name = "buttonResetArea";
            buttonResetArea.Size = new Size(112, 34);
            buttonResetArea.TabIndex = 15;
            buttonResetArea.Text = "Reset area";
            buttonResetArea.UseVisualStyleBackColor = true;
            buttonResetArea.Click += ResetAreaBtn_Click;
            // 
            // labelArea
            // 
            labelArea.AutoSize = true;
            labelArea.Location = new Point(6, 185);
            labelArea.Name = "labelArea";
            labelArea.Size = new Size(116, 25);
            labelArea.TabIndex = 12;
            labelArea.Text = "Selected area";
            // 
            // buttonSelectArea
            // 
            buttonSelectArea.Location = new Point(358, 180);
            buttonSelectArea.Name = "buttonSelectArea";
            buttonSelectArea.Size = new Size(112, 34);
            buttonSelectArea.TabIndex = 14;
            buttonSelectArea.Text = "Select area";
            buttonSelectArea.UseVisualStyleBackColor = true;
            buttonSelectArea.Click += SelectAreaBtn_Click;
            // 
            // labelSeconds
            // 
            labelSeconds.AutoSize = true;
            labelSeconds.Location = new Point(229, 145);
            labelSeconds.Name = "labelSeconds";
            labelSeconds.Size = new Size(77, 25);
            labelSeconds.TabIndex = 11;
            labelSeconds.Text = "seconds";
            // 
            // numericInterval
            // 
            numericInterval.Location = new Point(133, 143);
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
            // comboBoxType
            // 
            comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxType.FormattingEnabled = true;
            comboBoxType.Location = new Point(133, 104);
            comboBoxType.Name = "comboBoxType";
            comboBoxType.Size = new Size(90, 33);
            comboBoxType.TabIndex = 6;
            comboBoxType.SelectedIndexChanged += TypeBox_SelectedIndexChanged;
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(133, 67);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(401, 31);
            textBoxName.TabIndex = 4;
            toolTip.SetToolTip(textBoxName, "Placeholders %date%, %time% and %num% will be replaced with current date, time and image counter");
            // 
            // buttonBrowse
            // 
            buttonBrowse.Location = new Point(540, 30);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(48, 34);
            buttonBrowse.TabIndex = 2;
            buttonBrowse.Text = "...";
            toolTip.SetToolTip(buttonBrowse, "Select output folder");
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += BrowseBtn_Click;
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
            labelInterval.Size = new Size(70, 25);
            labelInterval.TabIndex = 9;
            labelInterval.Text = "Interval";
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
            labelFormat.Size = new Size(102, 25);
            labelFormat.TabIndex = 5;
            labelFormat.Text = "Image type";
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
            checkBoxMonitorClipboard.Location = new Point(424, 463);
            checkBoxMonitorClipboard.Name = "checkBoxMonitorClipboard";
            checkBoxMonitorClipboard.Size = new Size(182, 29);
            checkBoxMonitorClipboard.TabIndex = 5;
            checkBoxMonitorClipboard.Text = "Monitor clipboard";
            toolTip.SetToolTip(checkBoxMonitorClipboard, "Automatically save images copied to the clipboard");
            checkBoxMonitorClipboard.UseVisualStyleBackColor = true;
            // 
            // groupBoxLog
            // 
            groupBoxLog.ContextMenuStrip = contextMenuLog;
            groupBoxLog.Controls.Add(richTextBoxLog);
            groupBoxLog.Location = new Point(12, 243);
            groupBoxLog.Name = "groupBoxLog";
            groupBoxLog.Size = new Size(594, 214);
            groupBoxLog.TabIndex = 1;
            groupBoxLog.TabStop = false;
            groupBoxLog.Text = "Log";
            // 
            // contextMenuLog
            // 
            contextMenuLog.ImageScalingSize = new Size(24, 24);
            contextMenuLog.Items.AddRange(new ToolStripItem[] { saveLogMenuItem, clearLogMenuItem });
            contextMenuLog.Name = "contextMenuLog";
            contextMenuLog.Size = new Size(187, 68);
            // 
            // clearLogMenuItem
            // 
            clearLogMenuItem.Name = "clearLogMenuItem";
            clearLogMenuItem.Size = new Size(240, 32);
            clearLogMenuItem.Text = "Clear log";
            clearLogMenuItem.Click += ClearLogMenuItem_Click;
            // 
            // saveLogMenuItem
            // 
            saveLogMenuItem.Name = "saveLogMenuItem";
            saveLogMenuItem.Size = new Size(240, 32);
            saveLogMenuItem.Text = "Save log as...";
            saveLogMenuItem.Click += SaveLogMenuItem_Click;
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.BorderStyle = BorderStyle.None;
            richTextBoxLog.ContextMenuStrip = contextMenuLog;
            richTextBoxLog.Location = new Point(6, 30);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.ReadOnly = true;
            richTextBoxLog.Size = new Size(582, 178);
            richTextBoxLog.TabIndex = 0;
            richTextBoxLog.Text = "";
            richTextBoxLog.TextChanged += LogText_TextChanged;
            // 
            // checkBoxHideWindow
            // 
            checkBoxHideWindow.AutoSize = true;
            checkBoxHideWindow.Location = new Point(12, 463);
            checkBoxHideWindow.Name = "checkBoxHideWindow";
            checkBoxHideWindow.Size = new Size(223, 29);
            checkBoxHideWindow.TabIndex = 2;
            checkBoxHideWindow.Text = "Hide window after start";
            toolTip.SetToolTip(checkBoxHideWindow, "If checked, capturing will stop when the window is restored");
            checkBoxHideWindow.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(12, 498);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(112, 34);
            buttonStart.TabIndex = 3;
            buttonStart.Text = "Start";
            toolTip.SetToolTip(buttonStart, "Start capturing with selected interval");
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += StartBtn_Click;
            // 
            // buttonStop
            // 
            buttonStop.Enabled = false;
            buttonStop.Location = new Point(130, 498);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(112, 34);
            buttonStop.TabIndex = 4;
            buttonStop.Text = "Stop";
            toolTip.SetToolTip(buttonStop, "Stop capturing");
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += StopBtn_Click;
            // 
            // buttonOpenFolder
            // 
            buttonOpenFolder.Location = new Point(457, 498);
            buttonOpenFolder.Name = "buttonOpenFolder";
            buttonOpenFolder.Size = new Size(149, 34);
            buttonOpenFolder.TabIndex = 6;
            buttonOpenFolder.Text = "Open folder";
            toolTip.SetToolTip(buttonOpenFolder, "Open output folder");
            buttonOpenFolder.UseVisualStyleBackColor = true;
            buttonOpenFolder.Click += OpenFolderBtn_Click;
            // 
            // buttonShot
            // 
            buttonShot.Location = new Point(248, 498);
            buttonShot.Name = "buttonShot";
            buttonShot.Size = new Size(112, 34);
            buttonShot.TabIndex = 7;
            buttonShot.Text = "Shot";
            toolTip.SetToolTip(buttonShot, "Take single screenshot");
            buttonShot.UseVisualStyleBackColor = true;
            buttonShot.Click += ShotBtn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(618, 544);
            Controls.Add(buttonShot);
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
            contextMenuOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericInterval).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericQuality).EndInit();
            groupBoxLog.ResumeLayout(false);
            contextMenuLog.ResumeLayout(false);
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
        private ComboBox comboBoxType;
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
        private Label labelArea;
        private Button buttonSelectArea;
        private Button buttonResetArea;
        private TextBox textBoxArea;
        private Button buttonShot;
        private ContextMenuStrip contextMenuOptions;
        private ToolStripMenuItem resetOptionsMenuItem;
        private ContextMenuStrip contextMenuLog;
        private ToolStripMenuItem clearLogMenuItem;
        private ToolStripMenuItem resetCounterMenuItem;
        private ToolStripMenuItem saveLogMenuItem;
    }
}
