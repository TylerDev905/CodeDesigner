using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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
            var type= tabControlEx1.SelectedTab.Controls[0].GetType();
            if(type == typeof(AssemblerControl))
            {
                var asm = (AssemblerControl)tabControlEx1.SelectedTab.Controls[0];
                asm.Run();
            }
            
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
                    Text = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName),
                    Tag = openFileDialog.FileName,
                };

                var ext= System.IO.Path.GetExtension(openFileDialog.FileName);

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
                        break;

                    case ".asm":
                        var controlAsm = new AssemblerControl()
                        {
                            SourcePath = openFileDialog.FileName,
                            SourceMips = mipsSource,
                            Dock = DockStyle.Fill
                        };
                        controlAsm.LoadSource();
                        tab.Controls.Add(controlAsm);
                        tabControlEx1.Controls.Add(tab);
                        break;

                    case ".txt":
                        var disassembler = (DisassemblerControl)tabControlEx1.SelectedTab.Controls[0];
                        disassembler.LoadLabelsFromFile(openFileDialog.FileName);
                        break;
                }
            } 
        }
    }
}



