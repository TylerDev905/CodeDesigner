namespace CodeDesigner
{
    partial class DisassemblerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisassemblerControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsTbAddress = new System.Windows.Forms.ToolStripTextBox();
            this.tsBtnAddress = new System.Windows.Forms.ToolStripButton();
            this.tsBtnSearch = new System.Windows.Forms.ToolStripButton();
            this.tsBtnStrings = new System.Windows.Forms.ToolStripButton();
            this.tsBtnLabels = new System.Windows.Forms.ToolStripButton();
            this.tsBtnHistory = new System.Windows.Forms.ToolStripButton();
            this.dgvDisassembler = new CodeDesigner.DisassemblerGrid();
            this.ColumnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelStringView = new CodeDesigner.LabelStringView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisassembler)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsTbAddress,
            this.tsBtnAddress,
            this.tsBtnSearch,
            this.tsBtnStrings,
            this.tsBtnLabels,
            this.tsBtnHistory});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(966, 36);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsTbAddress
            // 
            this.tsTbAddress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsTbAddress.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsTbAddress.MaxLength = 8;
            this.tsTbAddress.Name = "tsTbAddress";
            this.tsTbAddress.Size = new System.Drawing.Size(100, 36);
            this.tsTbAddress.Text = "00000000";
            this.tsTbAddress.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tsBtnAddress
            // 
            this.tsBtnAddress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tsBtnAddress.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnAddress.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsBtnAddress.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tsBtnAddress.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnAddress.Image")));
            this.tsBtnAddress.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAddress.Name = "tsBtnAddress";
            this.tsBtnAddress.Size = new System.Drawing.Size(31, 33);
            this.tsBtnAddress.Text = "GO";
            this.tsBtnAddress.ToolTipText = "Go";
            this.tsBtnAddress.Click += new System.EventHandler(this.tsBtnAddress_Click);
            // 
            // tsBtnSearch
            // 
            this.tsBtnSearch.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsBtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSearch.Image")));
            this.tsBtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSearch.Name = "tsBtnSearch";
            this.tsBtnSearch.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.tsBtnSearch.Size = new System.Drawing.Size(83, 33);
            this.tsBtnSearch.Text = "Search";
            this.tsBtnSearch.Click += new System.EventHandler(this.tsBtnSearch_Click);
            // 
            // tsBtnStrings
            // 
            this.tsBtnStrings.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsBtnStrings.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnStrings.Image")));
            this.tsBtnStrings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnStrings.Name = "tsBtnStrings";
            this.tsBtnStrings.Size = new System.Drawing.Size(92, 33);
            this.tsBtnStrings.Text = "Strings";
            this.tsBtnStrings.Click += new System.EventHandler(this.tsBtnStrings_Click);
            // 
            // tsBtnLabels
            // 
            this.tsBtnLabels.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsBtnLabels.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnLabels.Image")));
            this.tsBtnLabels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnLabels.Name = "tsBtnLabels";
            this.tsBtnLabels.Size = new System.Drawing.Size(83, 33);
            this.tsBtnLabels.Text = "Labels";
            this.tsBtnLabels.Click += new System.EventHandler(this.tsBtnLabels_Click);
            // 
            // tsBtnHistory
            // 
            this.tsBtnHistory.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsBtnHistory.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnHistory.Image")));
            this.tsBtnHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnHistory.Name = "tsBtnHistory";
            this.tsBtnHistory.Size = new System.Drawing.Size(92, 33);
            this.tsBtnHistory.Text = "History";
            this.tsBtnHistory.Click += new System.EventHandler(this.tsBtnHistory_Click);
            // 
            // dgvDisassembler
            // 
            this.dgvDisassembler.AllowUserToAddRows = false;
            this.dgvDisassembler.AllowUserToDeleteRows = false;
            this.dgvDisassembler.AllowUserToResizeRows = false;
            this.dgvDisassembler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDisassembler.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvDisassembler.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDisassembler.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDisassembler.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDisassembler.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDisassembler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisassembler.ColumnHeadersVisible = false;
            this.dgvDisassembler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAddress,
            this.ColumnData,
            this.ColumnOperation,
            this.ColumnLabel,
            this.ColumnComment});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDisassembler.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDisassembler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDisassembler.GridColor = System.Drawing.Color.Black;
            this.dgvDisassembler.Location = new System.Drawing.Point(0, 61);
            this.dgvDisassembler.MultiSelect = false;
            this.dgvDisassembler.Name = "dgvDisassembler";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 11.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDisassembler.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDisassembler.RowHeadersVisible = false;
            this.dgvDisassembler.RowHeadersWidth = 5;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Control;
            this.dgvDisassembler.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDisassembler.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvDisassembler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDisassembler.Size = new System.Drawing.Size(966, 301);
            this.dgvDisassembler.TabIndex = 7;
            // 
            // ColumnAddress
            // 
            this.ColumnAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnAddress.HeaderText = "Address   ";
            this.ColumnAddress.Name = "ColumnAddress";
            this.ColumnAddress.ReadOnly = true;
            this.ColumnAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnAddress.Width = 94;
            // 
            // ColumnData
            // 
            this.ColumnData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnData.HeaderText = "Data      ";
            this.ColumnData.Name = "ColumnData";
            this.ColumnData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnData.Width = 94;
            // 
            // ColumnOperation
            // 
            this.ColumnOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnOperation.HeaderText = "Operation";
            this.ColumnOperation.Name = "ColumnOperation";
            this.ColumnOperation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnOperation.Width = 250;
            // 
            // ColumnLabel
            // 
            this.ColumnLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnLabel.HeaderText = "Label";
            this.ColumnLabel.Name = "ColumnLabel";
            this.ColumnLabel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnLabel.Width = 259;
            // 
            // ColumnComment
            // 
            this.ColumnComment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnComment.HeaderText = "Comment";
            this.ColumnComment.Name = "ColumnComment";
            this.ColumnComment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // labelStringView
            // 
            this.labelStringView.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelStringView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelStringView.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelStringView.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStringView.ForeColor = System.Drawing.Color.Black;
            this.labelStringView.Location = new System.Drawing.Point(0, 0);
            this.labelStringView.MinimumSize = new System.Drawing.Size(0, 25);
            this.labelStringView.Name = "labelStringView";
            this.labelStringView.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelStringView.Size = new System.Drawing.Size(966, 25);
            this.labelStringView.TabIndex = 6;
            this.labelStringView.Text = "HELLO WOLRD";
            this.labelStringView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DisassemblerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvDisassembler);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.labelStringView);
            this.Name = "DisassemblerControl";
            this.Size = new System.Drawing.Size(966, 362);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisassembler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private LabelStringView labelStringView;
        private DisassemblerGrid dgvDisassembler;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox tsTbAddress;
        private System.Windows.Forms.ToolStripButton tsBtnAddress;
        private System.Windows.Forms.ToolStripButton tsBtnSearch;
        private System.Windows.Forms.ToolStripButton tsBtnStrings;
        private System.Windows.Forms.ToolStripButton tsBtnLabels;
        private System.Windows.Forms.ToolStripButton tsBtnHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComment;
    }
}
