using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CodeDesigner
{
    public partial class BMSEditorControl : UserControl
    {

        public string SourcePath { get; set; }
        public string Source { get; set; }
        public string ArchivePath { get; set; }
        public string OutputPath { get; set; }

        public BMSEditorControl()
        {
            InitializeComponent();
        }

        public void LoadSource()
        {
            if (SourcePath != string.Empty)
            {
                Source = System.IO.File.ReadAllText(SourcePath);
            }
            if (Source != string.Empty)
            {
                fstSource.Text = Source;
            }
        }

        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Range range = (sender as FastColoredTextBox).VisibleRange;

            range.ClearStyle(Theme.CommentStyle);
            range.SetStyle(Theme.CommentStyle, @"//.*$", RegexOptions.Multiline);
            range.SetStyle(Theme.CommentStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            range.SetStyle(Theme.CommentStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline |
                        RegexOptions.RightToLeft);

            e.ChangedRange.ClearStyle(Theme.LabelStyle);
            e.ChangedRange.SetStyle(Theme.LabelStyle, Theme.LabelPattern, RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(Theme.LabelStyle, Theme.TargetPattern, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.StringStyle);
            e.ChangedRange.SetStyle(Theme.StringStyle, "long|double|integer|float|string|char", RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle1);
            e.ChangedRange.SetStyle(Theme.RegisterStyle1, "goto|for|if|else|else if|while|end", RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle2);
            e.ChangedRange.SetStyle(Theme.RegisterStyle2, "math", RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle3);
            e.ChangedRange.SetStyle(Theme.RegisterStyle3, "get|set|offset", RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle4);
            e.ChangedRange.SetStyle(Theme.RegisterStyle4, "0x[0-9a-f]{1,}", RegexOptions.IgnoreCase);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            fstConsole.Text = "Please select an archive to continue.\n";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ArchivePath = openFileDialog1.FileName;
                fstConsole.Text += "Please select the output path to contine.\n";
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    OutputPath = folderBrowserDialog1.SelectedPath;
                    var cmd = new Process();
                    cmd.StartInfo.FileName = @"quickbms.exe";
                    cmd.StartInfo.Arguments = $"{SourcePath} {ArchivePath} \"{OutputPath}\"";
                    cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    cmd.StartInfo.CreateNoWindow = false;
                    cmd.Start();

                    fstConsole.Text += $"The archive {System.IO.Path.GetFileName(ArchivePath)} was unpacked and the contents have been exported to {OutputPath}\n";
                    Console.WriteLine();
                }
            }
        }

        private void tsBtnHistory_Click(object sender, EventArgs e)
        {
            fstConsole.Text = "Please select an archive to continue.\n";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ArchivePath = openFileDialog1.FileName;
                fstConsole.Text += "Please select the Input path to contine.\n";
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    OutputPath = folderBrowserDialog1.SelectedPath;
                    var cmd = new Process();
                    cmd.StartInfo.FileName = @"quickbms.exe";
                    cmd.StartInfo.Arguments = $"-w -r {SourcePath} {ArchivePath} \"{OutputPath}\"";
                    cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    cmd.StartInfo.CreateNoWindow = false;
                    cmd.Start();

                    fstConsole.Text += $"The archive {System.IO.Path.GetFileName(OutputPath)} has been packed and the contents have been imported into {ArchivePath}\n";
                }
            }
        }
    }
}
