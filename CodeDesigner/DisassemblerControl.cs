using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace CodeDesigner
{

    public partial class DisassemblerControl : UserControl
    {
        public string MemoryDumpPath { get; set; } = @"C:\Users\Tyler\Desktop\pcsx2  fragment\dump.bin";
        public byte[] MemoryDump { get; set; }
        public int MemorySize { get; set; } = 33554432;
        public int MemoryStart { get; set; } = 0;
        public int MemoryIndex { get; set; } = 0;
        public Mips32 mips { get; set; }

        public DisassemblerControl()
        {
            InitializeComponent();
        }

        public void LoadMemoryDump()
        {
            if(MemoryDumpPath != string.Empty)
            {
                if (System.IO.File.Exists(MemoryDumpPath))
                    MemoryDump = System.IO.File.ReadAllBytes(MemoryDumpPath);
            }
            else
                MemoryDump = new byte[MemorySize];
        }

        private string ByteToText(byte byteData)
        {
            return Convert.ToString(Convert.ToInt32(byteData), 16).PadLeft(2, '0');
        }

        private string ByteToAscci(byte byteData)
        {
            return Encoding.ASCII.GetString(new byte[] { byteData > 31 && byteData < 177 ? byteData : Convert.ToByte(46) });
        }

        private string ToAddress(int i)
        {
            return Convert.ToString(i, 16).PadLeft(8, '0');
        }

        public void Start()
        {

            LoadMemoryDump();
            mips = new Mips32(); 
            MemoryStart = 3;
            MemorySize = 200;
            this.labelStringView.Text = "";

            for (var i = 3; i < MemorySize; i += 4)
            {
                var address = Convert.ToString((i - 3), 16).PadLeft(8, '0');
                var word = ByteToText(MemoryDump[i]) + ByteToText(MemoryDump[i-1]) + ByteToText(MemoryDump[i-2]) + ByteToText(MemoryDump[i-3]);
                
                var operation = mips.Disassemble(word);

                //AddRow(false, ToAddress(i - 3), "------" + ByteToText(MemoryDump[i - 3]), $".byte[ {ByteToAscci(MemoryDump[i])} ]");
                //AddRow(false, ToAddress(i - 2), "----" + ByteToText(MemoryDump[i - 2]) + "--", $".byte[ {ByteToAscci(MemoryDump[i - 1])} ]");
                //AddRow(false, ToAddress(i - 1), "--" + ByteToText(MemoryDump[i - 1]) + "----", $".byte[ {ByteToAscci(MemoryDump[i - 2])} ]");
                //AddRow(false, ToAddress(i), ByteToText(MemoryDump[i]) + "------", $".byte[ {ByteToAscci(MemoryDump[i - 3])} ]");

                //AddRow(false, ToAddress(i - 3), "----" + ByteToText(MemoryDump[i - 2]) + ByteToText(MemoryDump[i - 3]), $".halfword");
                //AddRow(false, ToAddress(i - 1), ByteToText(MemoryDump[i]) + ByteToText(MemoryDump[i - 1]) + "----", $".halfword");

                //AddRow(false, ToAddress(i - 3), word, ".word");
                AddRow(false, ToAddress(i - 3), word, operation);

                this.labelStringView.Text += ByteToAscci(MemoryDump[i]) + ByteToAscci(MemoryDump[i - 1]) + ByteToAscci(MemoryDump[i - 2]) + ByteToAscci(MemoryDump[i - 3]);

                Console.WriteLine("");
            }
        }

        public void AddRow(Boolean isTop, string address, string data, string disassembled)
        {
            var row = new DataGridViewRow();
            row.CreateCells(this.dataGridViewDisassembler);
            row.Cells[0].Value = address;
            row.Cells[1].Value = data;
            row.Cells[2].Value = disassembled;
            row.Cells[3].Value = "";
            row.Cells[4].Value = "";
            if (isTop)
            {
                this.dataGridViewDisassembler.Rows.Insert(0, row);
            }
            else
            {
                this.dataGridViewDisassembler.Rows.Add(row);
            }
        }

    }
}
