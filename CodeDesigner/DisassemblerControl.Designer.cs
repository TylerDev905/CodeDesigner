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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewDisassembler = new CodeDesigner.DisassemblerGrid();
            this.labelStringView = new CodeDesigner.LabelStringView();
            this.ColumnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisassembler)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDisassembler
            // 
            this.dataGridViewDisassembler.AllowUserToAddRows = false;
            this.dataGridViewDisassembler.AllowUserToDeleteRows = false;
            this.dataGridViewDisassembler.AllowUserToResizeRows = false;
            this.dataGridViewDisassembler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDisassembler.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridViewDisassembler.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewDisassembler.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDisassembler.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDisassembler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDisassembler.ColumnHeadersVisible = false;
            this.dataGridViewDisassembler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAddress,
            this.ColumnData,
            this.ColumnOperation,
            this.ColumnLabel,
            this.ColumnComment});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDisassembler.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewDisassembler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDisassembler.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridViewDisassembler.Location = new System.Drawing.Point(0, 18);
            this.dataGridViewDisassembler.Name = "dataGridViewDisassembler";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 11.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDisassembler.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewDisassembler.RowHeadersVisible = false;
            this.dataGridViewDisassembler.RowHeadersWidth = 5;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Control;
            this.dataGridViewDisassembler.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewDisassembler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDisassembler.Size = new System.Drawing.Size(966, 349);
            this.dataGridViewDisassembler.TabIndex = 7;
            // 
            // labelStringView
            // 
            this.labelStringView.BackColor = System.Drawing.SystemColors.ControlLight;
            this.labelStringView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelStringView.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelStringView.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStringView.ForeColor = System.Drawing.Color.Black;
            this.labelStringView.Location = new System.Drawing.Point(0, 0);
            this.labelStringView.Name = "labelStringView";
            this.labelStringView.Size = new System.Drawing.Size(966, 18);
            this.labelStringView.TabIndex = 6;
            this.labelStringView.Text = "623546563425436252436534";
            this.labelStringView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.ColumnOperation.Width = 300;
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
            // DisassemblerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewDisassembler);
            this.Controls.Add(this.labelStringView);
            this.Name = "DisassemblerControl";
            this.Size = new System.Drawing.Size(966, 367);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisassembler)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private LabelStringView labelStringView;
        private DisassemblerGrid dataGridViewDisassembler;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComment;
    }
}
