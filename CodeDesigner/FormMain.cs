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

        public MipsSource mipsSource { get; set; }

        public FormMain()
        {
            var source = System.IO.File.ReadAllText(@"C:\Users\Tyler\Desktop\test.txt");
            mipsSource = new MipsSource(source);
            InitializeComponent();
            fastColoredTextBox1.Text = source;
        }

        private Style CommentStyle = new TextStyle(Brushes.Green, null, FontStyle.Bold);
        private Style CommandsStyle = new TextStyle(Brushes.DodgerBlue, null, FontStyle.Bold);
        private Style OperatorStyle = new TextStyle(Brushes.IndianRed, null, FontStyle.Bold);
        private Style LabelsStyle = new TextStyle(Brushes.PaleGreen, null, FontStyle.Bold);
        private Style RegisterStyle1 = new TextStyle(Brushes.Goldenrod, null, FontStyle.Bold);
        private Style RegisterStyle2 = new TextStyle(Brushes.Orchid, null, FontStyle.Bold);
        private Style RegisterStyle3 = new TextStyle(Brushes.MediumPurple, null, FontStyle.Bold);
        private Style RegisterStyle4 = new TextStyle(Brushes.Firebrick, null, FontStyle.Bold);


        private void fstConsole_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(RegisterStyle4);
            e.ChangedRange.SetStyle(RegisterStyle4, "Line [0-9]{1,}: Exception thrown - [a-z0-9 ,!.]{1,}", RegexOptions.IgnoreCase);
        }

        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(LabelsStyle);
            e.ChangedRange.SetStyle(LabelsStyle, MipsSource.LabelPattern, RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(LabelsStyle, MipsSource.TargetPattern, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(CommandsStyle);
            e.ChangedRange.SetStyle(CommandsStyle, MipsSource.WordPattern, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(OperatorStyle);
            e.ChangedRange.SetStyle(OperatorStyle, MipsSource.BetweenQuotes, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(RegisterStyle3);
            e.ChangedRange.SetStyle(RegisterStyle3, String.Join("|", mipsSource.Mips.Registers.Where(x => x.MemoryID == MemoryMap.EE && x.Name.Contains("a") && x.Name.Contains("ra") == false).Select(x => x.Name)), RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(RegisterStyle1);
            e.ChangedRange.SetStyle(RegisterStyle1, String.Join("|", mipsSource.Mips.Registers.Where(x => x.MemoryID == MemoryMap.EE && x.Name.Contains("s")).Select(x => x.Name)), RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(RegisterStyle2);
            e.ChangedRange.SetStyle(RegisterStyle2, String.Join("|", mipsSource.Mips.Registers.Where(x => x.MemoryID == MemoryMap.EE && x.Name.Contains("v")).Select(x => x.Name)), RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(RegisterStyle4);
            e.ChangedRange.SetStyle(RegisterStyle4, String.Join("|", mipsSource.Mips.Registers.Where(x => x.MemoryID == MemoryMap.EE && x.Name.Contains("t")).Select(x => x.Name)), RegexOptions.IgnoreCase);


            Range range = (sender as FastColoredTextBox).VisibleRange;

            range.ClearStyle(CommentStyle);
            range.SetStyle(CommentStyle, @"//.*$", RegexOptions.Multiline);
            range.SetStyle(CommentStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            range.SetStyle(CommentStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline |
                        RegexOptions.RightToLeft);

        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mipsSource.Source = fastColoredTextBox1.Text;
            rtCode.Text = mipsSource.Parse();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            fstConsole.Text = "Assembling has started" + "\r\n";
            
            foreach (var log in mipsSource.Logs.OrderBy(x => x.LineNumber))
            {
                fstConsole.Text += log.Message + "\r\n";
                rtCode.Text = string.Empty;
            }
            stopwatch.Stop();
            
            fstConsole.Text += "The assembler took a total time of " + stopwatch.ElapsedMilliseconds.ToString() + " milliseconds to complete";
            if (fstConsole.VerticalScroll.Maximum != 100)
            {
                fstConsole.Focus();
                fstConsole.VerticalScroll.Value = fstConsole.VerticalScroll.Maximum;
                fstConsole.UpdateScrollbars();
            }
        }
    }
}



