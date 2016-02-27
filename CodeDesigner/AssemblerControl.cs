using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System;

namespace CodeDesigner
{
    public partial class AssemblerControl : UserControl
    {
        public string SourcePath { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public MipsSource SourceMips { get; set; }
        public ImageList Images { get; set; }
        public AutocompleteMenu AutoCompleteMenu { get; set; }

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
            AutoCompleteMenu.SelectedColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            AutoCompleteMenu.ImageList = Images;
            AutoCompleteMenu.BackColor = Color.LightGray;
            AutoCompleteMenu.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            LoadLabelsFromProject();        
            UpdateAutoComplete();
            
            if (SourcePath != string.Empty)
            {
                Source = File.ReadAllText(SourcePath);
            }
            if (Source != string.Empty)
            {
                fstSource.Text = Source;
            }
        }

        public void LoadLabelsFromProject()
        {
            var path = SourcePath.Replace($"\\{Path.GetFileName(SourcePath)}", "");
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] fileinfo = directory.GetFiles("*.cdl");
            SourceMips.Labels.Clear();
            foreach (FileInfo info in fileinfo)
            {
                LoadLabelsFromFile(info.FullName);
            }
        }

        public void LoadLabelsFromFile(string cdlpath)
        {
            if (File.Exists(cdlpath))
            {
                var text = File.ReadAllText(cdlpath);
                MatchCollection matches = Regex.Matches(text.Replace("\r", ""), @"(\b[A-Z0-9\.\-\*\(\)\[\]\\\/\=\,\&\?\~ \. \%\^]{3,})[\n ]{0,}([a-f0-9]{8}[ ]{1}[a-fA0-9]{8}[ ]{0,}[\n]{0,1}){1,}[ ]{0,}[\n]{0,1}", RegexOptions.IgnoreCase);
                foreach (Match item in matches)
                {
                    var x = 0;
                    foreach (string stringItem in item.Groups[0].Value.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (Regex.IsMatch(stringItem, Theme.WordPattern))
                        {
                            if (SourceMips.Labels.Where(y => y.Text == $"_{item.Groups[1].Value.Trim().Replace(" ", "_")}[{x}]").Count() == 0)
                            {
                                SourceMips.Labels.Add(new Label()
                                {
                                    Address = Convert.ToInt32(stringItem.Substring(1, 7), 16),
                                    Text = $"_{item.Groups[1].Value.Trim().Replace(" ", "_")}[{x}]"
                                });
                            }
                        }
                        x++;
                    }
                }
            }
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

        public void Run()
        {
            SourceMips.Source = fstSource.Text;
            LoadLabelsFromProject();
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
            UpdateAutoComplete();
        }

        public void UpdateAutoComplete()
        {
            var items = new List<AutocompleteItem>();
            items.AddRange(AutoCompleteUpdate(SourceMips.Mips.Registers.Select(x => x.Name), 0));
            items.AddRange(AutoCompleteInstructions());
            items.AddRange(AutoCompleteSyntax());
            items.AddRange(AutoCompleteUpdate(SourceMips.Labels.Select(x => x.Text), 3));

            AutoCompleteMenu.ShowItemToolTips = true;
            AutoCompleteMenu.Items.SetAutocompleteItems(items);
        }

        public List<AutocompleteItem> AutoCompleteUpdate(IEnumerable<string> textItems, int imageIndex, IEnumerable<string> tooltips = null)
        {
            var items = new List<AutocompleteItem>();

            for (var i = 0; i < textItems.Count(); i++)
            {
                items.Add(new AutocompleteItem()
                {
                    Text = textItems.ElementAt(i),
                    ImageIndex = imageIndex
                });

            }
            return items;
        }

        public List<AutocompleteItem> AutoCompleteInstructions()
        {
            var items = new List<AutocompleteItem>();
            for (var i = 0; i < SourceMips.Mips.Instructions.Count(); i++)
            {
                items.Add(new AutocompleteItem()
                {
                    Text = SourceMips.Mips.Instructions[i].Name.ToLower(),
                    ToolTipTitle = SourceMips.Mips.Instructions[i].Syntax.ToLower(),
                    ToolTipText = SourceMips.Mips.Instructions[i].Info.ToLower(),
                    ImageIndex = 1

                });
            }
            return items;
        }

        public List<AutocompleteItem> AutoCompleteSyntax()
        {
            var items = new List<AutocompleteItem>();
            items.Add(new AutocompleteItem()
            {
                Text = "address",
                ToolTipTitle = "address $hex",
                ToolTipText = "Updates the current address in the assembler with the specified hex string.",
                ImageIndex = 2

            });

            items.Add(new AutocompleteItem()
            {
                Text = "hexcode",
                ToolTipTitle = "hexcode $hex |or| hexcode :label",
                ToolTipText = "uses the hex string supplied for data at the current address.",
                ImageIndex = 2

            });

            items.Add(new AutocompleteItem()
            {
                Text = "print",
                ToolTipTitle = "print \"string\"",
                ToolTipText = "Converts the given string into hexidecimal and places it at the current address.",
                ImageIndex = 2

            });

            items.Add(new AutocompleteItem()
            {
                Text = "setreg",
                ToolTipTitle = "setreg reg, $hex |or| setreg reg, :label",
                ToolTipText = "sets the register given to the hex string supplied.",
                ImageIndex = 2

            });

            items.Add(new AutocompleteItem()
            {
                Text = "call",
                ToolTipTitle = "call $hex(reg1, reg2) |or| call :label(reg1, reg2)",
                ToolTipText = "Passes saved registers into argument registers, then sets a jal and link to the hex string supplied.",
                ImageIndex = 2
            });
            return items;
        }

    }
}
