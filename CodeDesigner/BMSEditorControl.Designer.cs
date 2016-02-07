namespace CodeDesigner
{
    partial class BMSEditorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BMSEditorControl));
            this.fstSource = new FastColoredTextBoxNS.FastColoredTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsBtnHistory = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fstConsole = new FastColoredTextBoxNS.FastColoredTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.fstSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fstConsole)).BeginInit();
            this.SuspendLayout();
            // 
            // fstSource
            // 
            this.fstSource.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fstSource.AutoScrollMinSize = new System.Drawing.Size(29, 36);
            this.fstSource.BackBrush = null;
            this.fstSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fstSource.BookmarkColor = System.Drawing.Color.Azure;
            this.fstSource.CaretColor = System.Drawing.SystemColors.ActiveCaption;
            this.fstSource.CharHeight = 18;
            this.fstSource.CharWidth = 9;
            this.fstSource.CurrentLineColor = System.Drawing.SystemColors.Highlight;
            this.fstSource.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fstSource.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fstSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fstSource.Font = new System.Drawing.Font("Consolas", 12F);
            this.fstSource.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fstSource.IsReplaceMode = false;
            this.fstSource.LineNumberColor = System.Drawing.Color.DimGray;
            this.fstSource.Location = new System.Drawing.Point(0, 0);
            this.fstSource.Margin = new System.Windows.Forms.Padding(4);
            this.fstSource.Name = "fstSource";
            this.fstSource.Paddings = new System.Windows.Forms.Padding(0);
            this.fstSource.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.fstSource.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fstSource.ServiceColors")));
            this.fstSource.Size = new System.Drawing.Size(827, 369);
            this.fstSource.TabIndex = 1;
            this.fstSource.Text = "\r\n";
            this.fstSource.Zoom = 100;
            this.fstSource.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox1_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsBtnHistory});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(827, 36);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.toolStripButton1.Size = new System.Drawing.Size(83, 33);
            this.toolStripButton1.Text = "Export";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsBtnHistory
            // 
            this.tsBtnHistory.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsBtnHistory.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnHistory.Image")));
            this.tsBtnHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnHistory.Name = "tsBtnHistory";
            this.tsBtnHistory.Size = new System.Drawing.Size(83, 33);
            this.tsBtnHistory.Text = "Import";
            this.tsBtnHistory.Click += new System.EventHandler(this.tsBtnHistory_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 36);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fstSource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fstConsole);
            this.splitContainer1.Size = new System.Drawing.Size(827, 480);
            this.splitContainer1.SplitterDistance = 369;
            this.splitContainer1.SplitterWidth = 16;
            this.splitContainer1.TabIndex = 10;
            // 
            // fstConsole
            // 
            this.fstConsole.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fstConsole.AutoScrollMinSize = new System.Drawing.Size(29, 18);
            this.fstConsole.BackBrush = null;
            this.fstConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fstConsole.CharHeight = 18;
            this.fstConsole.CharWidth = 9;
            this.fstConsole.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fstConsole.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fstConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fstConsole.Font = new System.Drawing.Font("Consolas", 12F);
            this.fstConsole.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fstConsole.IsReplaceMode = false;
            this.fstConsole.LineNumberColor = System.Drawing.Color.DimGray;
            this.fstConsole.Location = new System.Drawing.Point(0, 0);
            this.fstConsole.Margin = new System.Windows.Forms.Padding(4);
            this.fstConsole.Name = "fstConsole";
            this.fstConsole.Paddings = new System.Windows.Forms.Padding(0);
            this.fstConsole.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fstConsole.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fstConsole.ServiceColors")));
            this.fstConsole.Size = new System.Drawing.Size(827, 95);
            this.fstConsole.TabIndex = 6;
            this.fstConsole.Zoom = 100;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // BMSEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BMSEditorControl";
            this.Size = new System.Drawing.Size(827, 516);
            ((System.ComponentModel.ISupportInitialize)(this.fstSource)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fstConsole)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fstSource;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBoxNS.FastColoredTextBox fstConsole;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsBtnHistory;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
