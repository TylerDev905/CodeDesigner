using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CodeDesigner
{

    public partial class DisassemblerControl : UserControl
    {
        public string MemoryDumpPath { get; set; } = @"C:\Users\Tyler\Desktop\pcsx2  fragment\dump.bin";
        public byte[] MemoryDump { get; set; }
        public int MemoryDumpSize { get; set; } = 33554432;
        public int PageStart { get; set; } = 0;
        public int PageEnd{ get; set; } = 100;
        public bool IsInsert { get; set; } = false;
        public Mips32 mips { get; set; }

        public DisassemblerControl()
        {
            InitializeComponent();
            dataGridViewDisassembler.KeyDown += new KeyEventHandler(OnKeyPressedDown);
            dataGridViewDisassembler.CellPainting += new DataGridViewCellPaintingEventHandler(OnCellPainting);
            dataGridViewDisassembler.SelectionChanged += new EventHandler(OnSelected);
            dataGridViewDisassembler.KeyUp += new KeyEventHandler(OnKeyPressed);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        
        public void LoadMemoryDump()
        {
            if(MemoryDumpPath != string.Empty)
            {
                if (System.IO.File.Exists(MemoryDumpPath))
                    MemoryDump = System.IO.File.ReadAllBytes(MemoryDumpPath);
            }
            else
                MemoryDump = new byte[MemoryDumpSize];
        }
        
        public void Start()
        {
            var memoryIndex = PageStart;
            while(memoryIndex < PageStart + PageEnd)
            {
                AddDisassembledRow(AddressType.Operation, memoryIndex);
                memoryIndex += 4;
            }
        }

        public void OnSelected(object sender, EventArgs e)
        {
            UpdateStringView();
        }

        public void OnKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    JumpToAddres();
                    break;
            }
        }

        private void JumpToAddres()
        {
            if (Regex.IsMatch(dataGridViewDisassembler.SelectedRows[0].Cells[2].Value.ToString(), Theme.WordPattern))
                GoToAddress(dataGridViewDisassembler.SelectedRows[0].Cells[2].Value.ToString());

            if (Regex.IsMatch(dataGridViewDisassembler.SelectedRows[0].Cells[1].Value.ToString(), Theme.WordPattern))
                GoToAddress(dataGridViewDisassembler.SelectedRows[0].Cells[1].Value.ToString());
        }

        private void GoToAddress(string address)
        {
            var addressInt = Convert.ToInt32(Parse.WithRegex(address, Theme.WordPattern), 16);
            if (addressInt < MemoryDump.Length)
            {
                IsInsert = false;
                dataGridViewDisassembler.Rows.Clear();
                PageStart = addressInt;
                Start();
                dataGridViewDisassembler.Rows[0].Selected = true;
            }
        }

        public void OnKeyPressedDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    IsInsert = true;
                    CursorUp();
                    UpdateStringView();
                    break;
                case Keys.Down:
                    IsInsert = false;
                    CursorDown();
                    UpdateStringView();
                    break;
            }
        }

        public void OnCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(this.dataGridViewDisassembler.Columns["ColumnOperation"].Index == e.ColumnIndex && e.RowIndex >= 0 && this.dataGridViewDisassembler.SelectedRows.Contains(this.dataGridViewDisassembler.Rows[e.RowIndex]) == false)
            {
                var rectangle = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                using (Brush gridBrush = new SolidBrush(this.dataGridViewDisassembler.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    e.Graphics.FillRectangle(backColorBrush, rectangle);

                    var value = e.Value.ToString().Replace(",", " ,")
                                .Replace("(", " ( ")
                                .Replace(")", " )")
                                .Replace("$", "$ ")
                                .Replace("[", "[ ");

                    var args = value.ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    var x = e.CellBounds.X + 2;
                    var y = e.CellBounds.Y + 2;

                    foreach (var arg in args)
                    {
                        if (x.Equals(e.CellBounds.X + 2))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.Azure, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width + 5;
                        }

                        else if (Regex.IsMatch(arg, Theme.RegisterPattern6))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.MediumPurple, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width - 5;
                        }

                        else if(Regex.IsMatch(arg, Theme.HalfWordPattern) || Regex.IsMatch(arg, Theme.WordPattern))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.OldLace, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width - 5;
                        }
                        
                        else if (Regex.IsMatch(arg, Theme.RegisterPattern1))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.Goldenrod, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width - 5;
                        }

                        else if (Regex.IsMatch(arg, Theme.RegisterPattern2))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.YellowGreen, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width - 5;
                        }

                        else if (Regex.IsMatch(arg, Theme.RegisterPattern3))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.CornflowerBlue, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width - 5;
                        }

                        else if (Regex.IsMatch(arg, Theme.RegisterPattern4))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.OrangeRed, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width - 5;
                        }

                        else if (Regex.IsMatch(arg, Theme.RegisterPattern5))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.DeepSkyBlue, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width - 5;
                        }
                        
                        else if (arg.Contains(")") || arg.Contains(","))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.Azure, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width + 5;
                        }

                        else if (arg.Contains(",") || arg.Contains("(") || arg.Contains("$"))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.Azure, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width - 5;
                        }
                    }

                    e.Handled = true;
                }
            }
        }
        
        public void UpdateStringView()
        {
            if (dataGridViewDisassembler.SelectedRows.Count > 0)
            {
                var bytes = new List<byte>();
                var address = Convert.ToInt32(dataGridViewDisassembler.SelectedRows[0].Cells[0].Value.ToString(), 16);
                for (int i = address; i < address + 250; i++)
                    bytes.Add(MemoryDump[i] > 32 && MemoryDump[i] < 177 ? MemoryDump[i] : Convert.ToByte(46));

                labelStringView.Text = Encoding.ASCII.GetString(bytes.ToArray());
                labelStringView.Update();
            }
        }

        public void CursorUp()
        {
            if (PageStart >= 4)
            {
                PageStart -= 4;
                AddDisassembledRow(AddressType.Operation, PageStart);
            }
        }

        public void CursorDown()
        {
            if (PageEnd < MemoryDump.Length)
            {
                PageEnd += 4;
                AddDisassembledRow(AddressType.Operation, PageEnd);
            }
        }

        public enum AddressType
        {
            Byte,
            Halfword,
            Word,
            Operation
        }

        public void AddDisassembledRow(AddressType type, int x)
        {
            for (var i = x + 3; i < x + 4; i += 4)
            {
                var byte1 = i - 3;
                var byte2 = i - 2;
                var byte3 = i - 1;

                var address = Convert.ToString((byte1), 16).PadLeft(8, '0');
                var word = ByteToText(MemoryDump[i]) + ByteToText(MemoryDump[byte3]) + ByteToText(MemoryDump[byte2]) + ByteToText(MemoryDump[byte1]);
                
                switch (type)
                {
                    case AddressType.Byte:
                        AddRow(ToAddress(byte1), "------" + ByteToText(MemoryDump[byte1]), $".byte[ {ByteToAscci(MemoryDump[i])} ]");
                        AddRow(ToAddress(byte2), "----" + ByteToText(MemoryDump[byte2]) + "--", $".byte[ {ByteToAscci(MemoryDump[byte3])} ]");
                        AddRow(ToAddress(byte3), "--" + ByteToText(MemoryDump[byte3]) + "----", $".byte[ {ByteToAscci(MemoryDump[byte2])} ]");
                        AddRow(ToAddress(i), ByteToText(MemoryDump[i]) + "------", $".byte[ {ByteToAscci(MemoryDump[byte1])} ]");
                        break;
                    case AddressType.Halfword:
                        AddRow(ToAddress(byte1), "----" + ByteToText(MemoryDump[byte2]) + ByteToText(MemoryDump[byte1]), $".halfword");
                        AddRow(ToAddress(byte3), ByteToText(MemoryDump[i]) + ByteToText(MemoryDump[byte3]) + "----", $".halfword");
                        break;
                    case AddressType.Word:
                        AddRow(ToAddress(byte1), word, ".word");
                        break;
                    case AddressType.Operation:
                        AddRow(ToAddress(byte1), word, mips.Disassemble(word));
                        break;
                }
            }
        }

        public void AddRow(string address, string data, string disassembled)
        {
            var row = new DataGridViewRow();
            row.CreateCells(this.dataGridViewDisassembler);
            row.Cells[0].Value = address;
            row.Cells[1].Value = data;
            row.Cells[2].Value = disassembled;
            row.Cells[3].Value = "";
            row.Cells[4].Value = "";
            var cursor = dataGridViewDisassembler.SelectedRows.Count == 0 ? MemoryDumpSize : dataGridViewDisassembler.SelectedRows[0].Index;

            if (IsInsert)
                this.dataGridViewDisassembler.Rows.Insert(0, row);
            else
                this.dataGridViewDisassembler.Rows.Add(row);
        }

        private string ByteToText(byte byteData) => Convert.ToString(Convert.ToInt32(byteData), 16).PadLeft(2, '0');

        private string ByteToAscci(byte byteData) => Encoding.ASCII.GetString(new byte[] { byteData > 31 && byteData < 177 ? byteData : Convert.ToByte(46) });

        private string ToAddress(int i) => Convert.ToString(i, 16).PadLeft(8, '0');

        private void tsBtnAddress_Click(object sender, EventArgs e)
        {
            if(Regex.IsMatch(tsTbAddress.Text, Theme.WordPattern))
            {
                IsInsert = false;
                dataGridViewDisassembler.Rows.Clear();
                var address = Convert.ToInt32(tsTbAddress.Text, 16);
                PageStart = address;
                Start();
                dataGridViewDisassembler.Rows[0].Selected = true;
            }
        }

        private void tsBtnStrings_Click(object sender, EventArgs e)
        {
            var formStringsDump = new FormStrings()
            {
                MemoryDump = MemoryDump
            };

            formStringsDump.ShowDialog();

            if (formStringsDump.Address != 0)
            {
                IsInsert = false;
                dataGridViewDisassembler.Rows.Clear();
                PageStart = formStringsDump.Address;
                Start();
                dataGridViewDisassembler.Rows[0].Selected = true;
            }
        }
    }
}
