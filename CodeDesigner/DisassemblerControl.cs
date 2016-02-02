using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace CodeDesigner
{
    public partial class DisassemblerControl : UserControl
    {
        public string MemoryDumpPath { get; set; } = string.Empty;
        public byte[] MemoryDump { get; set; }
        public int MemoryDumpSize { get; set; } = 33554432;
        public int PageStart { get; set; } = 0;
        public int PageEnd { get; set; } = 175;
        public AddRowType RowType { get; set; } = AddRowType.Down;
        public Mips32 mips { get; set; }
        public List<Label> Labels { get; set; } = new List<Label>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<string> History { get; set; } = new List<string>();
        public List<StringMatch> Strings { get; set; } = new List<StringMatch>();
        public int StringAddress { get; set; }
        public int StringOffset { get; set; }

        private string ByteToText(byte byteData) => Convert.ToString(Convert.ToInt32(byteData), 16).PadLeft(2, '0');

        private string ByteToAscii(byte byteData) => Encoding.ASCII.GetString(new byte[] { byteData > 31 && byteData < 177 ? byteData : Convert.ToByte(46) });

        private string ToAddress(int i) => Convert.ToString(i, 16).PadLeft(8, '0');

        public class StringMatch
        {
            public int Address { get; set; }
            public int Offset { get; set; }
            public string Item { get; set; }
        }

        public enum AddressType
        {
            Byte,
            Halfword,
            Word,
            Operation,
            Float,
            Double,
            Quad
        }

        public enum AddRowType
        {
            Up,
            Insert,
            Down
        }

        public DisassemblerControl()
        {
            InitializeComponent();
            dataGridViewDisassembler.KeyDown += new KeyEventHandler(OnKeyPressedDown);
            dataGridViewDisassembler.CellPainting += new DataGridViewCellPaintingEventHandler(OnCellPainting);
            dataGridViewDisassembler.SelectionChanged += new EventHandler(OnSelected);
            dataGridViewDisassembler.KeyUp += new KeyEventHandler(OnKeyPressed);
            dataGridViewDisassembler.CellEndEdit += new DataGridViewCellEventHandler(OnCellEdit);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        
        public void LoadMemoryDump()
        {
            if (MemoryDumpPath != string.Empty)
            {
                if (File.Exists(MemoryDumpPath))
                {
                    MemoryDump = File.ReadAllBytes(MemoryDumpPath);
                    LoadLabelsFromFile(Path.GetFileNameWithoutExtension(MemoryDumpPath) + ".txt");
                }
            }
            else
                MemoryDump = new byte[MemoryDumpSize];
        }

        public void Start()
        {
            RowType = AddRowType.Down;

            var memoryIndex = PageStart;
            while (memoryIndex < PageStart + PageEnd)
            {
                AddDisassembledRow(AddressType.Operation, memoryIndex);
                memoryIndex += 4;
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
        
        public void OnSelected(object sender, EventArgs e)
        {
            UpdateStringView();
        }

        public void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var address = Convert.ToInt32(dataGridViewDisassembler.Rows[dataGridViewDisassembler.SelectedRows[0].Index].Cells[0].Value.ToString(), 16);
            switch (e.KeyCode)
            {
                case Keys.Right:
                    JumpToAddres();
                    break;
                case Keys.F9:
                    RowType = AddRowType.Insert;
                    RemoveDisassembledRow();
                    AddDisassembledRow(AddressType.Word, address);
                    break;
                case Keys.F10:
                    RowType = AddRowType.Insert;
                    RemoveDisassembledRow();
                    AddDisassembledRow(AddressType.Halfword, address);
                    break;
                case Keys.F11:
                    RowType = AddRowType.Insert;
                    RemoveDisassembledRow();
                    AddDisassembledRow(AddressType.Operation, address);
                    break;
                case Keys.F12:
                    RowType = AddRowType.Insert;
                    RemoveDisassembledRow();
                    AddDisassembledRow(AddressType.Byte, address);
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
                History.Add(Convert.ToString(addressInt, 16).PadLeft(8, '0') + " " + DateTime.Now.ToString("yyyy/dd/mm hh:mm:ss"));
                RowType = AddRowType.Down;
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
                    RowType = AddRowType.Up;
                    CursorUp();
                    UpdateStringView();
                    break;

                case Keys.Down:
                    RowType = AddRowType.Down;
                    CursorDown();
                    UpdateStringView();
                    break;
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
        
        public void OnCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (this.dataGridViewDisassembler.Columns["ColumnOperation"].Index == e.ColumnIndex && e.RowIndex >= 0 && this.dataGridViewDisassembler.SelectedRows.Contains(this.dataGridViewDisassembler.Rows[e.RowIndex]) == false)
            {
                var rectangle = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                using (Brush gridBrush = new SolidBrush(this.dataGridViewDisassembler.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    e.Graphics.FillRectangle(backColorBrush, rectangle);

                    var value = e.Value.ToString().Replace(",", " ,")
                                .Replace("(", " ( ")
                                .Replace(")", " )")
                                .Replace("$", "$ ")
                                .Replace("[", "[ ")
                                .Replace("]", " ]");

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

                        else if (Regex.IsMatch(arg, Theme.HalfWordPattern) || Regex.IsMatch(arg, Theme.WordPattern))
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

                        else if (arg.Contains("]"))
                        {
                            x += 10;
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.Azure, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width;
                        }
                        else if (arg.Contains(",") || arg.Contains("(") || arg.Contains("$"))
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.Azure, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width - 5;
                        }
                        else
                        {
                            e.Graphics.DrawString(arg, e.CellStyle.Font, Brushes.Azure, x, y, StringFormat.GenericDefault);
                            x += (int)e.Graphics.MeasureString(arg, e.CellStyle.Font).Width - 5;
                        }
                    }

                    e.Handled = true;
                }
            }
        }

        public void LoadLabelsFromFile(string path)
        {
            var text = string.Empty;
            try
            {
                text = File.ReadAllText(path);
            }
            catch
            {
                text = "";
            }

            MatchCollection matches = Regex.Matches(text.Replace("\r", ""), @"(\b[A-Z0-9\.\-\*\(\)\[\]\\\/\=\,\&\?\~ \. \%\^]{3,})[\n ]{0,}([a-f0-9]{8}[ ]{1}[a-fA0-9]{8}[ ]{0,}[\n]{0,1}){1,}[ ]{0,}[\n]{0,1}", RegexOptions.IgnoreCase);
            foreach (Match item in matches)
            {
                var x = 0;
                foreach (string stringItem in item.Groups[0].Value.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (Regex.IsMatch(stringItem, Theme.WordPattern))
                    {
                        var value = x == 0 ? string.Empty : "_" + x.ToString();
                        Labels.Add(new Label()
                        {
                            Address = Convert.ToInt32(stringItem.Substring(1, 7), 16),
                            Text = $" {item.Groups[1].Value.Trim()}{value}"
                        });
                    }
                    x++;
                }
            }

        }

        public void RemoveDisassembledRow()
        {
            var selected = dataGridViewDisassembler.SelectedRows[0];
            var index = selected.Index;
            var data = selected.Cells[1].Value.ToString();
            var value = selected.Cells[2].Value.ToString();

            if (value.Contains(".byte"))
            {
                if (data.StartsWith("------"))
                    RemoveRows(index, 4);
                else if (data.StartsWith("----"))
                    RemoveRows(index - 1, 4);
                else if (data.StartsWith("--"))
                    RemoveRows(index - 2, 4);
                else if (data.EndsWith("-------"))
                    RemoveRows(index - 3, 4);
            }
            else if (value.Contains(".halfword"))
            {
                if (data.StartsWith("----"))
                    RemoveRows(index, 2);
                else if (data.EndsWith("----"))
                    RemoveRows(index - 1, 2);
            }
            else
            {
                RemoveRows(index, 1);
            }
        }

        public void RemoveRows(int index, int count)
        {
            for(var i = 0; i < count; i++)
                this.dataGridViewDisassembler.Rows.RemoveAt(index);
        }

        public void AddDisassembledRow(AddressType type, int x)
        {
            for (var i = x + 3; i < x + 4; i += 4)
            {
                var byte1 = i - 3;
                var byte2 = i - 2;
                var byte3 = i - 1;

                var label = Labels.FirstOrDefault(z => z.Address == byte1);
                var comment = Comments.FirstOrDefault(z => z.Address == byte1);
                var stringFound = Strings.FirstOrDefault(z => z.Address == byte1);

                if (stringFound != null)
                {
                    StringAddress = stringFound.Address;
                    StringOffset = stringFound.Offset;
                }

                if (byte1 <= StringAddress + StringOffset && StringAddress != 0)
                    type = AddressType.Byte;

                if(byte1 == StringAddress + StringOffset - 4)
                {
                    StringAddress = 0;
                    StringOffset = 0;
                }

                var word = ByteToText(MemoryDump[i]) + ByteToText(MemoryDump[byte3]) + ByteToText(MemoryDump[byte2]) + ByteToText(MemoryDump[byte1]);

                switch (type)
                {
                    case AddressType.Byte:
                        if (RowType == AddRowType.Up)
                        {
                            AddRow(ToAddress(i), ByteToText(MemoryDump[i]) + "------", $".byte[ {ByteToAscii(MemoryDump[i])} ]", label, comment);
                            AddRow(ToAddress(byte3), "--" + ByteToText(MemoryDump[byte3]) + "----", $".byte[ {ByteToAscii(MemoryDump[byte3])} ]", label, comment);
                            AddRow(ToAddress(byte2), "----" + ByteToText(MemoryDump[byte2]) + "--", $".byte[ {ByteToAscii(MemoryDump[byte2])} ]", label, comment);
                            AddRow(ToAddress(byte1), "------" + ByteToText(MemoryDump[byte1]), $".byte[ {ByteToAscii(MemoryDump[byte1])} ]", label, comment);
                        }
                        else
                        {
                            AddRow(ToAddress(byte1), "------" + ByteToText(MemoryDump[byte1]), $".byte[ {ByteToAscii(MemoryDump[byte1])} ]", label, comment);
                            AddRow(ToAddress(byte2), "----" + ByteToText(MemoryDump[byte2]) + "--", $".byte[ {ByteToAscii(MemoryDump[byte2])} ]", label, comment);
                            AddRow(ToAddress(byte3), "--" + ByteToText(MemoryDump[byte3]) + "----", $".byte[ {ByteToAscii(MemoryDump[byte3])} ]", label, comment);
                            AddRow(ToAddress(i), ByteToText(MemoryDump[i]) + "------", $".byte[ {ByteToAscii(MemoryDump[i])} ]", label, comment);
                        }
                        break;

                    case AddressType.Halfword:
                        AddRow(ToAddress(byte1), "----" + ByteToText(MemoryDump[byte2]) + ByteToText(MemoryDump[byte1]), $".halfword", label, comment);
                        AddRow(ToAddress(byte3), ByteToText(MemoryDump[i]) + ByteToText(MemoryDump[byte3]) + "----", $".halfword", label, comment);
                        break;

                    case AddressType.Word:
                        AddRow(ToAddress(byte1), word, ".word", label, comment);
                        break;

                    case AddressType.Operation:
                        AddRow(ToAddress(byte1), word, mips.Disassemble(word), label, comment);
                        break;
                }
            }
        }

        public void AddRow(string address, string data, string disassembled, Label label, Comment comment)
        {
            var row = new DataGridViewRow();
            row.CreateCells(this.dataGridViewDisassembler);
            row.Cells[0].Value = address;
            row.Cells[1].Value = data;
            row.Cells[2].Value = disassembled;

            if (label != null)
                row.Cells[3].Value = label.Text;

            if (comment != null)
                row.Cells[4].Value = comment.Text;

            if (RowType == AddRowType.Up)
                this.dataGridViewDisassembler.Rows.Insert(0, row);

            if(RowType == AddRowType.Insert)
                this.dataGridViewDisassembler.Rows.Insert(dataGridViewDisassembler.SelectedRows[0].Index, row);

            if (RowType == AddRowType.Down)
                this.dataGridViewDisassembler.Rows.Add(row);
        }
        
        private void tsBtnAddress_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(tsTbAddress.Text, Theme.WordPattern))
                GoToAddress(tsTbAddress.Text.PadLeft(8, '0'));
        }

        private void tsBtnStrings_Click(object sender, EventArgs e)
        {
            var formStringsDump = new FormStrings(){ MemoryDump = MemoryDump };
            formStringsDump.ShowDialog();

            if (formStringsDump.Address != 0)
            {
                GoToAddress(Convert.ToString(formStringsDump.Address, 16).PadLeft(8, '0'));
                Strings = formStringsDump.Strings;
            }
            
            formStringsDump.Dispose();
        }

        private void tsBtnSearch_Click(object sender, EventArgs e)
        {

        }

        private void tsBtnHistory_Click(object sender, EventArgs e)
        {
            var formHistory = new FormHistory() { Collection = History };
            formHistory.ShowDialog();
            if (formHistory.Address != string.Empty)
                GoToAddress(formHistory.Address.PadLeft(8, '0'));

            formHistory.Dispose();
        }

        private void tsBtnLabels_Click(object sender, EventArgs e)
        {
            var formSearch = new FormSearch() { Collection = Labels };
            formSearch.ShowDialog();
            if (formSearch.Address != string.Empty)
                GoToAddress(formSearch.Address.PadLeft(8, '0'));

            formSearch.Dispose();
        }

        public void OnCellEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewDisassembler.Columns["ColumnLabel"].Index == e.ColumnIndex)
            {
                if (dataGridViewDisassembler.Rows[e.RowIndex].Cells[3].Value.ToString() != null)
                {
                    Labels.Add(new Label()
                    {
                        Address = Convert.ToInt32(dataGridViewDisassembler.Rows[e.RowIndex].Cells[0].Value.ToString(), 16),
                        Text = dataGridViewDisassembler.Rows[e.RowIndex].Cells[3].Value.ToString()
                    });
                }
            }

            if (this.dataGridViewDisassembler.Columns["ColumnComment"].Index == e.ColumnIndex)
            {
                if (dataGridViewDisassembler.Rows[e.RowIndex].Cells[3].Value.ToString() != null)
                {
                    Comments.Add(new Comment()
                    {
                        Address = Convert.ToInt32(dataGridViewDisassembler.Rows[e.RowIndex].Cells[0].Value.ToString(), 16),
                        Text = dataGridViewDisassembler.Rows[e.RowIndex].Cells[3].Value.ToString()
                    });
                }
            }
        }

    }
}
