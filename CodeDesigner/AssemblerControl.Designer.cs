namespace CodeDesigner
{
    partial class AssemblerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblerControl));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fstSource = new FastColoredTextBoxNS.FastColoredTextBox();
            this.rtCode = new System.Windows.Forms.RichTextBox();
            this.fstConsole = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fstSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fstConsole)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.fstConsole);
            this.splitContainer2.Size = new System.Drawing.Size(887, 457);
            this.splitContainer2.SplitterDistance = 347;
            this.splitContainer2.SplitterWidth = 16;
            this.splitContainer2.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fstSource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtCode);
            this.splitContainer1.Size = new System.Drawing.Size(887, 347);
            this.splitContainer1.SplitterDistance = 665;
            this.splitContainer1.SplitterWidth = 16;
            this.splitContainer1.TabIndex = 4;
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
            this.fstSource.Size = new System.Drawing.Size(665, 347);
            this.fstSource.TabIndex = 0;
            this.fstSource.Text = "\r\n";
            this.fstSource.Zoom = 100;
            this.fstSource.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox1_TextChanged);
            // 
            // rtCode
            // 
            this.rtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtCode.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtCode.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rtCode.Location = new System.Drawing.Point(0, 0);
            this.rtCode.Margin = new System.Windows.Forms.Padding(4);
            this.rtCode.Name = "rtCode";
            this.rtCode.Size = new System.Drawing.Size(206, 347);
            this.rtCode.TabIndex = 2;
            this.rtCode.Text = "";
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
            this.fstConsole.Size = new System.Drawing.Size(887, 94);
            this.fstConsole.TabIndex = 5;
            this.fstConsole.Zoom = 100;
            this.fstConsole.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fstConsole_TextChanged);
            // 
            // AssemblerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AssemblerControl";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.Size = new System.Drawing.Size(897, 457);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fstSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fstConsole)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBoxNS.FastColoredTextBox fstSource;
        private System.Windows.Forms.RichTextBox rtCode;
        private FastColoredTextBoxNS.FastColoredTextBox fstConsole;
    }
}
