using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace CodeDesigner
{
    public partial class AssemblerControl : UserControl
    {
        public string SourcePath { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public MipsSource SourceMips { get; set; }
        public ImageList Images { get; set; }
        public AutocompleteMenu AutoCompleteMenu { get; set; }
        public List<AutocompleteItem> MenuRegisters { get; set; }
        public List<AutocompleteItem> MenuInstructions { get; set; }
        public List<AutocompleteItem> MenuCodeDesignerSyntax { get; set; }
        public List<AutocompleteItem> MenuLabels { get; set; }

        public AssemblerControl()
        {
            InitializeComponent();
        }

        public void ImagesFromFolder(string pathImages)
        {
            Images = new ImageList();

            DirectoryInfo directory = new DirectoryInfo(pathImages);
            FileInfo[] fileinfo = directory.GetFiles("*.png");

            foreach (FileInfo info in fileinfo)
            {
                Images.Images.Add(Path.GetFileNameWithoutExtension(info.FullName), Image.FromFile(info.FullName));
            }
        }

        public void LoadSource()
        {
            ImagesFromFolder("images/autocomplete");
            AutoCompleteMenu = new AutocompleteMenu(fstSource);
            AutoCompleteMenu.ImageList = Images;
            AutoCompleteMenu.BackColor = Color.LightGray;
            MenuRegisters = AutoCompleteUpdate(SourceMips.Mips.Registers.Select(x => x.Name), 0);
            MenuInstructions = AutoCompleteUpdate(SourceMips.Mips.Instructions.Select(x => x.Name), 1);
            MenuCodeDesignerSyntax = AutoCompleteUpdate(MipsSource.CodeDesignerSyntax, 2);
            UpdateAutoComplete();

            if (SourcePath != string.Empty)
            {
                Source = File.ReadAllText(SourcePath);
            }
            if (Source != string.Empty)
            {
                fstSource.Text = Source;
            }
            fstSource.AppendText(" ");
        }
        
        private void fstConsole_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(Theme.ExceptionStyle);
            e.ChangedRange.SetStyle(Theme.ExceptionStyle, Theme.ExceptionPattern, RegexOptions.IgnoreCase);
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
            
        }

        public void Save()
        {
            File.WriteAllText(SourcePath, fstSource.Text);
        }

        public void Run(List<Label> labels)
        {

            SourceMips.Source = fstSource.Text;
            rtCode.Text = SourceMips.Parse(labels);

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
            MenuLabels = AutoCompleteUpdate(SourceMips.Labels.Select(x => x.Text), 3);
            UpdateAutoComplete();          
        }

        public void UpdateAutoComplete()
        {
            var items = new List<AutocompleteItem>();
            items.AddRange(MenuRegisters);
            items.AddRange(MenuInstructions);
            items.AddRange(MenuCodeDesignerSyntax);

            if(MenuLabels != null)
                items.AddRange(MenuLabels);

            AutoCompleteMenu.Items.SetAutocompleteItems(items);
        }

        public List<AutocompleteItem> AutoCompleteUpdate(IEnumerable<string> textItems, int imageIndex)
        {
            var items = new List<AutocompleteItem>();
            foreach(var text in textItems)
            {
                items.Add(new AutocompleteItem()
                {
                    Text = text.ToLower(),
                    ImageIndex = imageIndex
                });
            }
            return items;
        }
    }
}
