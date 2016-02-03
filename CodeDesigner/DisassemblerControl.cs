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
        public int PageSize { get; set; } = 160;
        public int PageEnd { get; set; }
        public AddRowType RowType { get; set; } = AddRowType.Down;
        public Mips32 mips { get; set; }
        public List<Label> Labels { get; set; } = new List<Label>();
        public string LabelsPath { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<string> History { get; set; } = new List<string>();
        public string HistoryPath { get; set; } 
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
            PageEnd = PageSize;
            dgvDisassembler.KeyDown += new KeyEventHandler(OnKeyPressedDown);
            dgvDisassembler.CellPainting += new DataGridViewCellPaintingEventHandler(OnCellPainting);
            dgvDisassembler.SelectionChanged += new EventHandler(OnSelected);
            dgvDisassembler.KeyUp += new KeyEventHandler(OnKeyPressed);
            dgvDisassembler.CellEndEdit += new DataGridViewCellEventHandler(OnCellEdit);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        
        public void LoadMemoryDump()
        {
            if (MemoryDumpPath != string.Empty)
            {
                if (File.Exists(MemoryDumpPath))
                {
                    MemoryDump = File.ReadAllBytes(MemoryDumpPath);

                    LabelsPath = Path.GetFileNameWithoutExtension(MemoryDumpPath) + ".txt";
                    LoadLabels();

                    HistoryPath = Path.GetFileNameWithoutExtension(MemoryDumpPath) + ".cdh";
                    History = LoadHistory();
                }
            }
            else
            {
                MemoryDump = new byte[MemoryDumpSize];
            }
        }

        public void Start()
        {
            RowType = AddRowType.Down;

            var memoryIndex = PageStart;
            while (memoryIndex < PageEnd)
            {
                AddDisassembledRow(AddressType.Operation, memoryIndex);
                memoryIndex += 4;
            }
        }

        public void UpdateStringView()
        {
            if (dgvDisassembler.SelectedRows.Count > 0)
            {
                var bytes = new List<byte>();
                var address = Convert.ToInt32(dgvDisassembler.SelectedRows[0].Cells[0].Value.ToString(), 16);
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
            var address = Convert.ToInt32(dgvDisassembler.Rows[dgvDisassembler.SelectedRows[0].Index].Cells[0].Value.ToString(), 16);
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
            if (Regex.IsMatch(dgvDisassembler.SelectedRows[0].Cells[2].Value.ToString(), Theme.WordPattern))
                GoToAddress(dgvDisassembler.SelectedRows[0].Cells[2].Value.ToString());

            if (Regex.IsMatch(dgvDisassembler.SelectedRows[0].Cells[1].Value.ToString(), Theme.WordPattern))
                GoToAddress(dgvDisassembler.SelectedRows[0].Cells[1].Value.ToString());
        }

        private void GoToAddress(string address)
        {
            var addressInt = Convert.ToInt32(Parse.WithRegex(address, Theme.WordPattern), 16);
            if (addressInt < MemoryDump.Length)
            {
                History.Add(Convert.ToString(addressInt, 16).PadLeft(8, '0') + " " + DateTime.Now.ToString("yyyy/dd/mm hh:mm:ss"));
                RowType = AddRowType.Down;
                dgvDisassembler.Rows.Clear();
                PageStart = addressInt;
                PageEnd = addressInt + PageSize;
                Start();
                dgvDisassembler.Rows[0].Selected = true;
                SaveHistory();
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
                AddDisassembledRow(AddressType.Operation, PageEnd);
                PageEnd += 4;
            }
        }
        
        public void OnCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (this.dgvDisassembler.Columns["ColumnOperation"].Index == e.ColumnIndex && e.RowIndex >= 0 && this.dgvDisassembler.SelectedRows.Contains(this.dgvDisassembler.Rows[e.RowIndex]) == false)
            {
                var rectangle = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                using (Brush gridBrush = new SolidBrush(this.dgvDisassembler.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
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

        public void LoadLabels()
        {

            if (File.Exists(LabelsPath))
            {
                var text = File.ReadAllText(LabelsPath);
                MatchCollection matches = Regex.Matches(text.Replace("\r", ""), @"(\b[A-Z0-9\.\-\*\(\)\[\]\\\/\=\,\&\?\~ \. \%\^]{3,})[\n ]{0,}([a-f0-9]{8}[ ]{1}[a-fA0-9]{8}[ ]{0,}[\n]{0,1}){1,}[ ]{0,}[\n]{0,1}", RegexOptions.IgnoreCase);
                foreach (Match item in matches)
                {
                    var x = 0;
                    foreach (string stringItem in item.Groups[0].Value.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (Regex.IsMatch(stringItem, Theme.WordPattern))
                        {
                            Labels.Add(new Label()
                            {
                                Address = Convert.ToInt32(stringItem.Substring(1, 7), 16),
                                Text = $"_{item.Groups[1].Value.Trim()}"
                            });
                        }
                        x++;
                    }
                }
            }
            else
            {
                File.WriteAllText(LabelsPath, string.Empty);
            }
        }

        public void SaveLabels()
        {
            if (Labels.Count() > 0)
            {
                var result = "";
                var labelTexts = Labels.Select(x => Parse.WithRegex(x.Text, @"_(.{3,})").Trim()).Distinct();

                foreach (var text in labelTexts)
                {
                    result += $"{text}\r\n";
                    foreach (var label in Labels.Where(x => Parse.WithRegex(x.Text, @"_(.{3,})").Trim() == text))
                    {
                        var data = ByteToText(MemoryDump[label.Address + 3]) + ByteToText(MemoryDump[label.Address + 2]) + ByteToText(MemoryDump[label.Address + 1]) + ByteToText(MemoryDump[label.Address]);
                        result += $"2{ToAddress(label.Address).Substring(1,7)} {data}\r\n";
                    }
                    result += "\r\n";
                }
                File.WriteAllText(LabelsPath, result);
            }
        }

        public void RemoveDisassembledRow()
        {
            var selected = dgvDisassembler.SelectedRows[0];
            var index = selected.Index;
            var value = selected.Cells[2].Value.ToString();
            var data = selected.Cells[1].Value.ToString();

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

            RemoveRows(index, 1);
        }

        public void RemoveRows(int index, int count)
        {
            for(var i = 0; i < count; i++)
                this.dgvDisassembler.Rows.RemoveAt(index);
        }

        public void AddDisassembledRow(AddressType type, int address)
        {
            for (var i = address + 3; i < address + 4; i += 4)
            {
                var byte1 = i - 3;
                var byte2 = i - 2;
                var byte3 = i - 1;

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
                
                if (type == AddressType.Byte) {

                    if (RowType == AddRowType.Up)
                    {
                        AddRow(i, ByteToText(MemoryDump[i]) + "------", $".byte[ {ByteToAscii(MemoryDump[i])} ]");
                        AddRow(byte3, "--" + ByteToText(MemoryDump[byte3]) + "----", $".byte[ {ByteToAscii(MemoryDump[byte3])} ]");
                        AddRow(byte2, "----" + ByteToText(MemoryDump[byte2]) + "--", $".byte[ {ByteToAscii(MemoryDump[byte2])} ]");
                        AddRow(byte1, "------" + ByteToText(MemoryDump[byte1]), $".byte[ {ByteToAscii(MemoryDump[byte1])} ]", true);
                    }
                    else
                    {
                        AddRow(byte1, "------" + ByteToText(MemoryDump[byte1]), $".byte[ {ByteToAscii(MemoryDump[byte1])} ]", true);
                        AddRow(byte2, "----" + ByteToText(MemoryDump[byte2]) + "--", $".byte[ {ByteToAscii(MemoryDump[byte2])} ]");
                        AddRow(byte3, "--" + ByteToText(MemoryDump[byte3]) + "----", $".byte[ {ByteToAscii(MemoryDump[byte3])} ]");
                        AddRow(i, ByteToText(MemoryDump[i]) + "------", $".byte[ {ByteToAscii(MemoryDump[i])} ]");
                    }
                }

                if (type == AddressType.Halfword)
                {
                    AddRow(byte1, "----" + ByteToText(MemoryDump[byte2]) + ByteToText(MemoryDump[byte1]), $".halfword", true);
                    AddRow(byte3, ByteToText(MemoryDump[i]) + ByteToText(MemoryDump[byte3]) + "----", $".halfword");
                }

                if (type == AddressType.Word)
                {
                    var word = ByteToText(MemoryDump[i]) + ByteToText(MemoryDump[byte3]) + ByteToText(MemoryDump[byte2]) + ByteToText(MemoryDump[byte1]);
                    AddRow(byte1, word, ".word", true);
                }

                if (type == AddressType.Operation)
                {
                    var word = ByteToText(MemoryDump[i]) + ByteToText(MemoryDump[byte3]) + ByteToText(MemoryDump[byte2]) + ByteToText(MemoryDump[byte1]);
                    AddRow(byte1, word, mips.Disassemble(word), true);
                }
            }
        }

        public void AddRow(int address, string data, string disassembled, bool isAligned = false)
        {
            var row = new DataGridViewRow();
            row.CreateCells(this.dgvDisassembler);
            row.Cells[0].Value = ToAddress(address);
            row.Cells[1].Value = data;
            row.Cells[2].Value = disassembled;

            var label = Labels.FirstOrDefault(l => l.Address == address);
            if (label != null && isAligned)
                row.Cells[3].Value = label.Text;

            var comment = Comments.FirstOrDefault(c => c.Address == address);
            if (comment != null && isAligned)
                row.Cells[4].Value = comment.Text;

            if (RowType == AddRowType.Up)
                this.dgvDisassembler.Rows.Insert(0, row);

            if(RowType == AddRowType.Insert)
                this.dgvDisassembler.Rows.Insert(dgvDisassembler.SelectedRows[0].Index, row);

            if (RowType == AddRowType.Down)
                this.dgvDisassembler.Rows.Add(row);
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
            var formHistory = new FormHistory() { ListBoxItems = History };
            formHistory.ShowDialog();
            if (formHistory.Address != string.Empty)
                GoToAddress(formHistory.Address.PadLeft(8, '0'));

            History = formHistory.ListBoxItems;
            formHistory.Dispose();
        }

        private void tsBtnLabels_Click(object sender, EventArgs e)
        {
            var formLabels = new FormLabels() { Collection = Labels };
            formLabels.ShowDialog();
            if (formLabels.Address != string.Empty)
                GoToAddress(formLabels.Address.PadLeft(8, '0'));

            formLabels.Dispose();
        }

        public void OnCellEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvDisassembler.Columns["ColumnLabel"].Index == e.ColumnIndex)
            {
                if (dgvDisassembler.Rows[e.RowIndex].Cells[3] != null)
                {
                    Labels.Add(new Label()
                    {
                        Address = Convert.ToInt32(dgvDisassembler.Rows[e.RowIndex].Cells[0].Value.ToString(), 16),
                        Text = "_"+ dgvDisassembler.Rows[e.RowIndex].Cells[3].Value.ToString()
                    });
                    SaveLabels();
                }
            }

            if (this.dgvDisassembler.Columns["ColumnComment"].Index == e.ColumnIndex)
            {
                if (dgvDisassembler.Rows[e.RowIndex].Cells[3] != null)
                {
                    Comments.Add(new Comment()
                    {
                        Address = Convert.ToInt32(dgvDisassembler.Rows[e.RowIndex].Cells[0].Value.ToString(), 16),
                        Text = dgvDisassembler.Rows[e.RowIndex].Cells[3].Value.ToString()
                    });
                }
            }
        }

        public void SaveHistory()
        {
            if (History.Count() > 0)
                File.WriteAllText(HistoryPath, String.Join("\r\n", History.ToArray()));
        }

        public List<string> LoadHistory()
        {
            if (File.Exists(HistoryPath))
                return File.ReadAllText(HistoryPath).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            else
            {
                File.WriteAllText(HistoryPath, string.Empty);
                return new List<string>();
            }
        }


    }
}
