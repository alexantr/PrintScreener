namespace PrintScreener
{
    partial class SelectArea
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectArea));
            panelDrag = new Panel();
            labelSize = new Label();
            buttonSelect = new Button();
            panelDrag.SuspendLayout();
            SuspendLayout();
            // 
            // panelDrag
            // 
            panelDrag.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDrag.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelDrag.Controls.Add(labelSize);
            panelDrag.Controls.Add(buttonSelect);
            panelDrag.Location = new Point(10, 10);
            panelDrag.Margin = new Padding(0);
            panelDrag.Name = "panelDrag";
            panelDrag.Size = new Size(620, 460);
            panelDrag.TabIndex = 0;
            panelDrag.MouseDown += DragControlsMouseDown;
            // 
            // labelSize
            // 
            labelSize.AutoSize = true;
            labelSize.Location = new Point(0, 34);
            labelSize.Name = "labelSize";
            labelSize.Size = new Size(41, 30);
            labelSize.TabIndex = 1;
            labelSize.Text = "area";
            labelSize.UseCompatibleTextRendering = true;
            labelSize.MouseDown += DragControlsMouseDown;
            // 
            // buttonSelect
            // 
            buttonSelect.BackColor = Color.White;
            buttonSelect.FlatStyle = FlatStyle.Flat;
            buttonSelect.Location = new Point(0, 0);
            buttonSelect.Margin = new Padding(0);
            buttonSelect.Name = "buttonSelect";
            buttonSelect.Size = new Size(100, 34);
            buttonSelect.TabIndex = 0;
            buttonSelect.Text = "Confirm";
            buttonSelect.UseVisualStyleBackColor = false;
            buttonSelect.Click += SelectBtnClick;
            // 
            // SelectArea
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Magenta;
            ClientSize = new Size(640, 480);
            Controls.Add(panelDrag);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(20, 20);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(100, 100);
            Name = "SelectArea";
            Opacity = 0.6D;
            StartPosition = FormStartPosition.Manual;
            Text = "Select area";
            TopMost = true;
            FormClosing += SelectArea_FormClosing;
            Load += SelectArea_Load;
            Move += SelectArea_Move;
            Resize += SelectArea_Resize;
            panelDrag.ResumeLayout(false);
            panelDrag.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelDrag;
        private Button buttonSelect;
        private Label labelSize;
    }
}