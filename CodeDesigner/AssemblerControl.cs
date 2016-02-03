using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;

namespace CodeDesigner
{
    public partial class AssemblerControl : UserControl
    {
        public string SourcePath { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public MipsSource SourceMips { get; set; }

        public AssemblerControl()
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
        
        private void fstConsole_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(Theme.ExceptionStyle);
            e.ChangedRange.SetStyle(Theme.ExceptionStyle, Theme.ExceptionPattern, RegexOptions.IgnoreCase);
        }

        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(Theme.LabelStyle);
            e.ChangedRange.SetStyle(Theme.LabelStyle, Theme.LabelPattern, RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(Theme.LabelStyle, Theme.TargetPattern, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.HexStyle);
            e.ChangedRange.SetStyle(Theme.HexStyle, Theme.WordPattern, RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(Theme.HexStyle, Theme.HalfWordPattern + Theme.NothingAheadPattern, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.StringStyle);
            e.ChangedRange.SetStyle(Theme.StringStyle, Theme.QuotesPattern, RegexOptions.IgnoreCase);
            
            e.ChangedRange.ClearStyle(Theme.RegisterStyle1);
            e.ChangedRange.SetStyle(Theme.RegisterStyle1, Theme.RegisterPattern1, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle2);
            e.ChangedRange.SetStyle(Theme.RegisterStyle2, Theme.RegisterPattern2, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle3);
            e.ChangedRange.SetStyle(Theme.RegisterStyle3, Theme.RegisterPattern3, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle4);
            e.ChangedRange.SetStyle(Theme.RegisterStyle4, Theme.RegisterPattern4, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle5);
            e.ChangedRange.SetStyle(Theme.RegisterStyle5, Theme.RegisterPattern5, RegexOptions.IgnoreCase);

            e.ChangedRange.ClearStyle(Theme.RegisterStyle6);
            e.ChangedRange.SetStyle(Theme.RegisterStyle6, Theme.RegisterPattern6, RegexOptions.IgnoreCase);

            Range range = (sender as FastColoredTextBox).VisibleRange;

            range.ClearStyle(Theme.CommentStyle);
            range.SetStyle(Theme.CommentStyle, @"//.*$", RegexOptions.Multiline);
            range.SetStyle(Theme.CommentStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            range.SetStyle(Theme.CommentStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline |
                        RegexOptions.RightToLeft);

        }

        public void Run()
        {
            SourceMips.Source = fstSource.Text;
            rtCode.Text = SourceMips.Parse();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            fstConsole.Text = string.Empty;

            foreach (var log in SourceMips.Logs.OrderBy(x => x.LineNumber))
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
