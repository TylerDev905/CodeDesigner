using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeDesigner
{
    public class MipsSource
    {
        public int AddressCounter { get; set; }
        public int SyntaxItemIndex { get; set; }
        public List<ISyntax> SyntaxItems { get; set; } = new List<ISyntax>();
        public List<Label> Labels { get; set; } = new List<Label>();
        public string Source { get; set; } = string.Empty;
        public string AssembledCode { get; set; }
        public Mips32 Mips { get; set; } = new Mips32();
        public List<string> Logs { get; set; } = new List<string>();
        public bool Exit { get; set; }
        public List<string> Commands = new List<string> { "hexcode", "setreg", "call", "print" };
        public string LabelPattern = @"([_.a-z0-9]{3,15}):";
        public string TargetPattern = @":([_.a-z0-9]{3,15})";

        public MipsSource(string source)
        {
            AddressCounter = 0;
            SyntaxItemIndex = 0;
            Source = source;
        }

        public MipsSource(int addressCounter, int syntaxitemIndex, string source)
        {
            AddressCounter = addressCounter;
            SyntaxItemIndex = syntaxitemIndex;
            Source = source;
        }
        
        public void Parse()
        {
            Logs.Add("Analyzing begin...");
            var source = Source
                .Replace("\t", "")
                .Replace("\r", "")
                .Replace("\n", "  ");

            var syntaxItems = source.Split(new string[] { "  " }, StringSplitOptions.None).ToList();
            Exit = false;
            for (SyntaxItemIndex = 0; SyntaxItemIndex < syntaxItems.Count();)
            {
                if (!IsComment(syntaxItems))
                    if (!Exit)
                    {
                        if (!IsBlankLine(syntaxItems[SyntaxItemIndex]))
                            if (!IsLabel(syntaxItems[SyntaxItemIndex]))
                                if (!IsAddressUpdate(syntaxItems[SyntaxItemIndex]))
                                    if (!IsTargetLabel(syntaxItems[SyntaxItemIndex]))
                                        if (!IsCommand(syntaxItems[SyntaxItemIndex]))
                                            IsOperation(syntaxItems[SyntaxItemIndex]);                    
                    }
                    else
                        break;
            }
            foreach (var log in Logs)
                Console.WriteLine(log);

        }

        public string ToCode()
        {
            var result = string.Empty;
            Console.WriteLine("\nAssembling...");
            
            for (var i = 0; i < SyntaxItems.Count(); i++)
            {
                var address = string.Empty;
                var data = string.Empty;

                if (SyntaxItems[i].GetType() != typeof(Label) && SyntaxItems[i].AddressCounter != null)
                    address = Helper.ZeroPad(Convert.ToString((int)SyntaxItems[i].AddressCounter, 16), 8);

                if (SyntaxItems[i].GetType() == typeof(Operation))
                    data = ((Operation)SyntaxItems[i]).Data;

                if (SyntaxItems[i].GetType() == typeof(TargetLabel))
                {
                    var target = ((TargetLabel)SyntaxItems[i]);
                    if (target.IsOperation) {
                        var label = Labels.Single(x => x.Text == target.Text);
                        data = Mips.Assemble(target.Line, (int)target.AddressCounter, (int)label.AddressCounter, target.Text);
                    }
                }

                if (address != string.Empty && data != string.Empty)
                    result += $"{address} {data}\n";
            }
            return result;
        }

        public interface ISyntax
        {
            int LineNumber { get; set; }
            int ByteLength { get; set; }
            int? AddressCounter { get; set; }
        }

        public class BlankLine : ISyntax
        {
            public int LineNumber { get; set; }
            public int ByteLength { get; set; } = 0;
            public int? AddressCounter { get; set; }
        }

        public bool IsBlankLine(string line)
        {
            if(line == string.Empty)
            {
                SyntaxItems.Add(new BlankLine() {
                    LineNumber = SyntaxItemIndex
                });
                SyntaxItemIndex++;
                return true;
            }
            return false;
        }

        public static class CommentIndicators
        {
            public static string MultiLineStart = "/*";
            public static string MultiLineEnd = "*/";
            public static string SingleLine = "//";
        }

        public class Comment : ISyntax
        {
            public int LineNumber { get; set; }
            public int LinePosition { get; set; }
            public int LineSpan { get; set; }
            public string Text { get; set; }
            public int ByteLength { get; set; } = 0;
            public int? AddressCounter { get; set; }
        }

        public bool IsComment(List<string> lines)
        {
            var commentFound = false;
            var commentBuffer = string.Empty;
            var lineSpan = 0;

            if (lines[SyntaxItemIndex].Contains(CommentIndicators.MultiLineStart))
            {
                if (lines[SyntaxItemIndex].Contains(CommentIndicators.MultiLineEnd))
                {
                    var startIndex = lines[SyntaxItemIndex].IndexOf(CommentIndicators.MultiLineStart);
                    var endIndex = lines[SyntaxItemIndex].IndexOf(CommentIndicators.MultiLineEnd);
                    commentBuffer += lines[SyntaxItemIndex].Substring(startIndex, (endIndex - startIndex + 2));
                    lines[SyntaxItemIndex] = lines[SyntaxItemIndex].Replace(commentBuffer, "").Trim(' ');
                    commentFound = true;
                }

                var itemCount = lines.Count();
                var itemIndex = SyntaxItemIndex;

                try {
                    while (commentFound == false || itemIndex < itemCount - 1)
                    {
                        if (lines[itemIndex].Contains(CommentIndicators.MultiLineEnd)) {
                            commentFound = true;
                            commentBuffer += lines[itemIndex];
                            break;
                        }
                        else
                            commentBuffer += lines[itemIndex];

                        itemIndex++;
                        lineSpan++;
                    }
                    lineSpan++;

                }
                catch (ArgumentException e)
                {
                    Logs.Add($"Line {SyntaxItemIndex + 1}: Exception thrown - The multi line comment is missing its closing indicator");
                    Exit = true;
                }

            }

            if (lines[SyntaxItemIndex].Contains(CommentIndicators.SingleLine)) {
                if (lines[SyntaxItemIndex].StartsWith(CommentIndicators.SingleLine))
                {
                    commentBuffer = lines[SyntaxItemIndex];
                    lineSpan = 1;
                }
                else
                {
                    var startIndex = lines[SyntaxItemIndex].IndexOf(CommentIndicators.SingleLine);
                    commentBuffer += lines[SyntaxItemIndex].Substring(startIndex, lines[SyntaxItemIndex].Length - startIndex);
                    lines[SyntaxItemIndex] = lines[SyntaxItemIndex].Replace(commentBuffer, "").Trim(' ');
                }
                commentFound = true;
            }

            if (commentFound) {

                SyntaxItems.Add(new Comment() {
                    Text = commentBuffer,
                    LineSpan = lineSpan,
                    LineNumber = SyntaxItemIndex
                });

                SyntaxItemIndex += lineSpan;
                return true;
            }
            return false;
        }


        public class Operation : ISyntax
        {
            public int LineNumber { get; set; }
            public string Data { get; set; }
            public int ByteLength { get; set; } = 4;
            public int? AddressCounter { get; set; }
        }

        public bool IsOperation(string item)
        {
            try { 
                var operation = new Operation()
                {
                    LineNumber = SyntaxItemIndex,
                    AddressCounter = AddressCounter,
                    Data = Mips.Assemble(item)
                };
                SyntaxItems.Add(operation);
                AddressCounter += operation.ByteLength;
                SyntaxItemIndex++;
                return true;
            }
            catch (ArgumentException e)
            {
                var types = new string[] { "Branch", "Code", "Register", "Integer", "Immediate", "Call" };
                var found = false;
                foreach (var type in types)
                    if (e.StackTrace.Contains($"Format{type}"))
                    {
                        Logs.Add($"Line {SyntaxItemIndex + 1}: Exception thrown - Operation argument of type {type} is incorrectly typed");
                        found = true;
                    }
                if(!found)
                    Logs.Add($"Line {SyntaxItemIndex + 1}: Exception thrown - The Operation cannot be parsed");
                SyntaxItemIndex++;
                return false;
            }
        }

        public class AddressUpdate : ISyntax
        {
            public int LineNumber { get; set; }
            public int NewAddress { get; set; }
            public int ByteLength { get; set; } = 0;
            public int? AddressCounter { get; set; }
        }

        public bool IsAddressUpdate(string item)
        {
            if (item.Contains("address"))
            {
                try {
                    var newAddress = Convert.ToInt32(CodeDesigner.Parse.WithRegex(item, "([a-f0-9]{8})"), 16);
                    AddressCounter = newAddress;
                    SyntaxItems.Add(new AddressUpdate()
                    {
                        LineNumber = SyntaxItemIndex,
                        NewAddress = newAddress
                    });
                    AddressCounter = newAddress;
                }
                catch(Exception e)
                {
                    Logs.Add($"Line {SyntaxItemIndex + 1}: Exception thrown - The address update cannot be parsed");
                }
                SyntaxItemIndex++;
                return true;
            }
            return false;
        }

        public class Command : ISyntax
        {
            public int LineNumber { get; set; }
            public int ByteLength { get; set; }
            public List<string> Data { get; set; }
            public int? AddressCounter { get; set; }
        }

        public bool IsCommand(string item)
        {
            return false;
        }

        public class Label : ISyntax
        {
            public int LineNumber { get; set; }
            public string Text { get; set; }
            public int? AddressCounter { get; set; }
            public int ByteLength { get; set; } = 0;
        }

        public bool IsLabel(string item)
        {
            if (item.Contains(":") && Regex.IsMatch(item, LabelPattern, RegexOptions.IgnoreCase))
            {
                var text = CodeDesigner.Parse.WithRegex(item, LabelPattern);
                var label = new Label()
                {
                    LineNumber = SyntaxItemIndex,
                    AddressCounter = AddressCounter,
                    Text = text
                };

                SyntaxItems.Add(label);
                Labels.Add(label);
                Logs.Add(($"Line {SyntaxItemIndex + 1}: Found Label [ {text} ] - 0x{Helper.ZeroPad(Convert.ToString(AddressCounter, 16), 8)}"));
                SyntaxItemIndex++;
                return true;
            }
            return false;
        }

        public class TargetLabel : ISyntax
        {
            public int LineNumber { get; set; }
            public string Label { get; set; }
            public string Line { get; set; }
            public bool IsOperation { get; set; }
            public int ByteLength { get; set; } = 0;
            public int? AddressCounter { get; set; }
            public string Text { get; set; }
        }

        public bool IsTargetLabel(string item)
        {
            if (item.Contains(":") && Regex.IsMatch(item, TargetPattern, RegexOptions.IgnoreCase))
            {
                var text = CodeDesigner.Parse.WithRegex(item, TargetPattern);
                SyntaxItems.Add(new TargetLabel()
                {
                    LineNumber = SyntaxItemIndex,
                    Label = CodeDesigner.Parse.WithRegex(item, TargetPattern),
                    Line = item,
                    IsOperation = Commands.Where(x => item.Contains(x)).Count() == 0 ? true : false,
                    Text = text,
                    AddressCounter = AddressCounter
                });
                Logs.Add(($"Line {SyntaxItemIndex + 1}: Found Target Label [ {text} ] - 0x{Helper.ZeroPad(Convert.ToString(AddressCounter, 16), 8)}"));
                AddressCounter += 4; 

                SyntaxItemIndex++;
                return true;
            }
            return false;
        }
    }

}
