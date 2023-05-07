namespace RemapKeyboard
{
    partial class frmMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            lblPressedKeys = new Label();
            bttCopyKeysToClipboard = new Button();
            chkAutoRefresh = new CheckBox();
            notifyIcon = new NotifyIcon(components);
            contextMenuStripIcon = new ContextMenuStrip(components);
            showToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            label2 = new Label();
            contextMenuStripIcon.SuspendLayout();
            SuspendLayout();
            // 
            // lblPressedKeys
            // 
            lblPressedKeys.Dock = DockStyle.Fill;
            lblPressedKeys.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblPressedKeys.Location = new Point(0, 0);
            lblPressedKeys.Name = "lblPressedKeys";
            lblPressedKeys.Size = new Size(429, 166);
            lblPressedKeys.TabIndex = 1;
            lblPressedKeys.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // bttCopyKeysToClipboard
            // 
            bttCopyKeysToClipboard.Location = new Point(338, 143);
            bttCopyKeysToClipboard.Name = "bttCopyKeysToClipboard";
            bttCopyKeysToClipboard.Size = new Size(91, 23);
            bttCopyKeysToClipboard.TabIndex = 2;
            bttCopyKeysToClipboard.Text = "Copy keys";
            bttCopyKeysToClipboard.UseVisualStyleBackColor = true;
            bttCopyKeysToClipboard.Click += bttCopyKeysToClipboard_Click;
            // 
            // chkAutoRefresh
            // 
            chkAutoRefresh.AutoSize = true;
            chkAutoRefresh.Location = new Point(338, 118);
            chkAutoRefresh.Name = "chkAutoRefresh";
            chkAutoRefresh.Size = new Size(91, 19);
            chkAutoRefresh.TabIndex = 3;
            chkAutoRefresh.Text = "Auto refresh";
            chkAutoRefresh.UseVisualStyleBackColor = true;
            chkAutoRefresh.CheckedChanged += chkAutoRefresh_CheckedChanged;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuStripIcon;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Keyboard Remap";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // contextMenuStripIcon
            // 
            contextMenuStripIcon.Items.AddRange(new ToolStripItem[] { showToolStripMenuItem, toolStripMenuItem1, exitToolStripMenuItem });
            contextMenuStripIcon.Name = "contextMenuStripIcon";
            contextMenuStripIcon.Size = new Size(104, 54);
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(103, 22);
            showToolStripMenuItem.Text = "&Show";
            showToolStripMenuItem.Click += showToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(103, 22);
            exitToolStripMenuItem.Text = "&Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 132);
            label1.Name = "label1";
            label1.Size = new Size(220, 15);
            label1.TabIndex = 5;
            label1.Text = "Desenvolvido por Leonardo Assumpção.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 147);
            label2.Name = "label2";
            label2.Size = new Size(198, 15);
            label2.TabIndex = 6;
            label2.Text = "leonardo_assumpcao@outlook.com";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(429, 166);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(chkAutoRefresh);
            Controls.Add(bttCopyKeysToClipboard);
            Controls.Add(lblPressedKeys);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Keyboard Remap";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            KeyDown += frmMain_KeyDown;
            KeyUp += frmMain_KeyUp;
            Resize += frmMain_Resize;
            contextMenuStripIcon.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPressedKeys;
        private Button bttCopyKeysToClipboard;
        private CheckBox chkAutoRefresh;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStripIcon;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Label label1;
        private Label label2;
    }
}