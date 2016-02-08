using System;
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
        public string InputPath { get; set; }

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

            e.ChangedRange.ClearStyle(Theme.LabelStyle);
            e.ChangedRange.SetStyle(Theme.LabelStyle, Theme.LabelPattern, RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(Theme.LabelStyle, Theme.TargetPattern, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.StringStyle);
            e.ChangedRange.SetStyle(Theme.StringStyle, Theme.BMSTypes, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle1);
            e.ChangedRange.SetStyle(Theme.RegisterStyle1, Theme.BMSStatements, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle2);
            e.ChangedRange.SetStyle(Theme.RegisterStyle2, Theme.BMSSpecial1, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle3);
            e.ChangedRange.SetStyle(Theme.RegisterStyle3, Theme.BMSSpecial2, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle4);
            e.ChangedRange.SetStyle(Theme.RegisterStyle4, Theme.BMSHex, RegexOptions.IgnoreCase);

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
                }
                else
                {
                    fstConsole.Text = string.Empty;
                }
            }
            else
            {
                fstConsole.Text = string.Empty;
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
                    InputPath = folderBrowserDialog1.SelectedPath;
                    var cmd = new Process();
                    cmd.StartInfo.FileName = @"quickbms.exe";
                    cmd.StartInfo.Arguments = $"-w -r {SourcePath} {ArchivePath} \"{InputPath}\"";
                    cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    cmd.StartInfo.CreateNoWindow = false;
                    cmd.Start();
                    fstConsole.Text += $"The archive {System.IO.Path.GetFileName(InputPath)} has been packed and the contents have been imported into {ArchivePath}\n";
                }
                else
                {
                    fstConsole.Text = string.Empty;
                }
            }
            else
            {
                fstConsole.Text = string.Empty;
            }
        }
    }
}
