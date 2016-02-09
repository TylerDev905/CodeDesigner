using System;
using System.Collections.Generic;
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
            webBrowser1.Navigate($"{Environment.CurrentDirectory}\\index.html");
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var type = tabControlEx1.SelectedTab.Controls[0].GetType();
            if (type == typeof(AssemblerControl))
            {
                var assembler = (AssemblerControl)tabControlEx1.SelectedTab.Controls[0];
                assembler.Save();
                assembler.Run(GetAllLabels());
            }
        }

        private List<Label> GetAllLabels()
        {
            var type = tabControlEx1.SelectedTab.Controls[0].GetType();
            var labels = new List<Label>();

            foreach (TabPage control in tabControlEx1.Controls)
            {     
                if (control.Controls[0].GetType() == typeof(DisassemblerControl) && type == typeof(AssemblerControl))
                {
                    var temp = (DisassemblerControl)control.Controls[0];
                    labels.AddRange(temp.Labels);
                }
            }
            return labels;
        }

        private void tsmOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileOpen(openFileDialog.FileName);
            } 
        }

        public void FileOpen(string filepath)
        {
            var tab = new TabPage()
            {
                Text = Path.GetFileNameWithoutExtension(filepath),
                Tag = filepath,
            };

            var ext = Path.GetExtension(filepath);

            switch (ext)
            {
                case ".bin":
                    var controlBin = new DisassemblerControl()
                    {
                        MemoryDumpPath = filepath,
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
                        SourcePath = filepath,
                        SourceMips = mipsSource,
                        Dock = DockStyle.Fill
                    };
                    controlAsm.LoadSource();
                    tab.Controls.Add(controlAsm);
                    tabControlEx1.Controls.Add(tab);
                    tabControlEx1.SelectedTab = tab;
                    break;

                case ".bms":
                    var bms = new BMSEditorControl()
                    {
                        SourcePath = filepath,
                        Dock = DockStyle.Fill
                    };
                    bms.LoadSource();
                    tab.Controls.Add(bms);
                    tabControlEx1.Controls.Add(tab);
                    tabControlEx1.SelectedTab = tab;
                    break;

                case ".txt":
                    var txt = new FastColoredTextBoxNS.FastColoredTextBox()
                    {
                        Tag = filepath,
                        Dock = DockStyle.Fill
                    };
                    tab.Controls.Add(txt);
                    tabControlEx1.Controls.Add(tab);
                    tabControlEx1.SelectedTab = tab;
                    break;

                default:
                    MessageBox.Show("Incorrect file format.");
                    break;
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var type = tabControlEx1.SelectedTab.Controls[0].GetType();

            if (type == typeof(AssemblerControl))
                ((AssemblerControl)tabControlEx1.SelectedTab.Controls[0]).Save();
            if (type == typeof(DisassemblerControl))
               ((DisassemblerControl)tabControlEx1.SelectedTab.Controls[0]).Save();
            if (type == typeof(BMSEditorControl))
                ((BMSEditorControl)tabControlEx1.SelectedTab.Controls[0]).Save();
            if (type == typeof(FastColoredTextBoxNS.FastColoredTextBox)) {
                var textbox = ((FastColoredTextBoxNS.FastColoredTextBox)tabControlEx1.SelectedTab.Controls[0]);
                File.WriteAllText(textbox.Tag.ToString(), textbox.Text);
            }
        }

        private void sourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Environment.CurrentDirectory + "\\projects\\lib\\new.cds", @"/*Code designer source*/");

            var tab = new TabPage(){ Text = "new.cds" };
            var controlAsm = new AssemblerControl()
            {
                SourcePath = Environment.CurrentDirectory + "\\projects\\lib\\new.cds",
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new FormAbout();
            about.ShowDialog();
            about.Dispose();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            projectTree1.Setup(Environment.CurrentDirectory);
            projectTree1.InitializeComponent();
            projectTree1.contextMenu.ItemClicked += ContextMenu_ItemClicked;
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Open":
                    FileOpen($"{projectTree1.SelectedNode.Name}");
                    break;
                case "Cut":
                    projectTree1.OnCut();
                    break;
                case "Copy":
                    projectTree1.OnCopy();
                    break;
                case "Paste":
                    projectTree1.OnPaste();
                    break;
                case "Delete":
                    projectTree1.OnDelete();
                    break;
            }
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var i = 1;
            while (true)
            {
                projectTree1.pathProject = Environment.CurrentDirectory + "\\projects";

                if (Directory.Exists(projectTree1.pathProject + "\\project" + i.ToString()) != true)
                {
                    Directory.CreateDirectory(projectTree1.pathProject + "\\project" + i.ToString());
                    break;
                }
                else
                {
                    i++;
                }
            }

        }
    }
}



