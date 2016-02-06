using System;
using System.IO;
using System.Windows.Forms;

namespace CodeDesigner
{
    public partial class FormMain : Form
    {
        public MipsSource mipsSource { get; set; } = new MipsSource("");

        public FormMain()
        {
            InitializeComponent();
            webBrowser1.Navigate("http://gamehacking.org/vb");
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var type = tabControlEx1.SelectedTab.Controls[0].GetType();
            if(type == typeof(AssemblerControl))
                ((AssemblerControl)tabControlEx1.SelectedTab.Controls[0]).Run();
        }

        private void assemblerControl1_Load(object sender, EventArgs e)
        {
            
        }

        private void tsmOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var tab = new TabPage()
                {
                    Text = Path.GetFileNameWithoutExtension(openFileDialog.FileName),
                    Tag = openFileDialog.FileName,
                };

                var ext = Path.GetExtension(openFileDialog.FileName);

                switch (ext)
                {
                    case ".bin":
                        var controlBin = new DisassemblerControl()
                        {
                            MemoryDumpPath = openFileDialog.FileName,
                            mips = mipsSource.Mips,
                            Dock = DockStyle.Fill
                        };
                        controlBin.LoadMemoryDump();
                        controlBin.Start();
                        tab.Controls.Add(controlBin);
                        tabControlEx1.Controls.Add(tab);
                        tabControlEx1.SelectedTab = tab;
                        break;

                    case ".cds":
                        var controlAsm = new AssemblerControl()
                        {
                            SourcePath = openFileDialog.FileName,
                            SourceMips = mipsSource,
                            Dock = DockStyle.Fill
                        };
                        controlAsm.LoadSource();
                        tab.Controls.Add(controlAsm);
                        tabControlEx1.Controls.Add(tab);
                        tabControlEx1.SelectedTab = tab;
                        break;
                }
            } 
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var type = tabControlEx1.SelectedTab.Controls[0].GetType();

            if (type == typeof(AssemblerControl))
                ((AssemblerControl)tabControlEx1.SelectedTab.Controls[0]).Save();

            if (type == typeof(DisassemblerControl))
               ((DisassemblerControl)tabControlEx1.SelectedTab.Controls[0]).Save();
        }

        private void sourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText("new.cds", @"/*Code designer source*/");

            var tab = new TabPage()
            {
                Text = "new.cds",
                Tag = openFileDialog.FileName,
            };

            var controlAsm = new AssemblerControl()
            {
                SourcePath = "new.cds",
                SourceMips = mipsSource,
                Dock = DockStyle.Fill
            };
            controlAsm.LoadSource();
            tab.Controls.Add(controlAsm);
            tabControlEx1.Controls.Add(tab);
            tabControlEx1.SelectedTab = tab;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var type = tabControlEx1.SelectedTab.Controls[0].GetType();

                if (type == typeof(AssemblerControl))
                {
                    tabControlEx1.SelectedTab.Text = Path.GetFileName(saveFileDialog1.FileName);
                    var assembler = (AssemblerControl)tabControlEx1.SelectedTab.Controls[0];
                    assembler.SourcePath = saveFileDialog1.FileName;
                    assembler.Save();
                }

                if (type == typeof(DisassemblerControl))
                {
                    tabControlEx1.SelectedTab.Text = Path.GetFileName(saveFileDialog1.FileName);
                    var disassembler = (DisassemblerControl)tabControlEx1.SelectedTab.Controls[0];
                    disassembler.MemoryDumpPath = saveFileDialog1.FileName;
                    disassembler.LabelsPath = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName) + ".txt";
                    disassembler.HistoryPath = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName) + ".cdh";
                    disassembler.Save();
                }
            }
        }

        private void binToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllBytes("new.bin", new byte[33554432]);

            var tab = new TabPage()
            {
                Text = "new.bin",
                Tag = openFileDialog.FileName,
            };

            var controlBin = new DisassemblerControl()
            {
                MemoryDumpPath = "new.bin",
                mips = mipsSource.Mips,
                Dock = DockStyle.Fill
            };
            controlBin.LoadMemoryDump();
            controlBin.Start();
            tab.Controls.Add(controlBin);
            tabControlEx1.Controls.Add(tab);
            tabControlEx1.SelectedTab = tab;
        }

 
    }
}



