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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonOpenFolder = new System.Windows.Forms.Button();
            this.labelPath = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelFormat = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(95, 14);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(246, 21);
            this.textBoxPath.TabIndex = 0;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(347, 13);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.Location = new System.Drawing.Point(428, 13);
            this.buttonOpenFolder.Name = "buttonOpenFolder";
            this.buttonOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenFolder.TabIndex = 2;
            this.buttonOpenFolder.Text = "Open folder";
            this.buttonOpenFolder.UseVisualStyleBackColor = true;
            this.buttonOpenFolder.Click += new System.EventHandler(this.buttonOpenFolder_Click);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(12, 18);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(76, 13);
            this.labelPath.TabIndex = 3;
            this.labelPath.Text = "Output folder:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 44);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(56, 13);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "File name:";
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new System.Drawing.Point(95, 68);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFormat.TabIndex = 5;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(95, 41);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(246, 21);
            this.textBoxName.TabIndex = 6;
            // 
            // labelFormat
            // 
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new System.Drawing.Point(12, 71);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(62, 13);
            this.labelFormat.TabIndex = 7;
            this.labelFormat.Text = "File format:";
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Location = new System.Drawing.Point(95, 96);
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
            this.numericUpDownInterval.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownInterval.TabIndex = 8;
            this.numericUpDownInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "label4";
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Location = new System.Drawing.Point(161, 95);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStartStop.TabIndex = 10;
            this.buttonStartStop.Text = "Start";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(347, 95);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 123);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(491, 126);
            this.richTextBoxLog.TabIndex = 12;
            this.richTextBoxLog.Text = "";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(428, 95);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 261);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownInterval);
            this.Controls.Add(this.labelFormat);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.comboBoxFormat);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.buttonOpenFolder);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxPath);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "PrintScreener";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonOpenFolder;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelFormat;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Button button5;
    }
}

