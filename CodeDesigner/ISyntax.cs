using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeDesigner
{

    public interface ISyntax
    {
        int LineNumber { get; set; }
    }
    
    public interface IMemorySyntax : ISyntax
    {
        int MemoryAddress { get; set; }
        int ByteLength { get; set; }
    }
    
    public class Command : IMemorySyntax
    {
        public int LineNumber { get; set; }
        public int MemoryAddress { get; set; }
        public int ByteLength { get; set; }
        public List<string> Data { get; set; }
    }

    public class Label : ISyntax
    {
        public int LineNumber { get; set; }
        public string Text { get; set; }
        public int MemoryAddress { get; set; }
    }
    
    public class MipsSource
    {
        public int Address { get; set; }
        public int SyntaxItemIndex { get; set; }
        public List<ISyntax> SyntaxItems { get; set; } = new List<ISyntax>();
        public string Source { get; set; } = string.Empty;
        public Mips32 Mips { get; set; } = new Mips32();
        public List<string>Errors { get; set; } = new List<string>();
        public bool CommentFlag { get; set; }
        public bool Exit { get; set; }

        public MipsSource(string source)
        {
            Address = 0;
            SyntaxItemIndex = 0;
            Source = source;
        }

        public MipsSource(int address, int syntaxitemIndex, string source)
        {
            Address = address;
            SyntaxItemIndex = syntaxitemIndex;
            Source = source;
        }
        
        public void Parse()
        {
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
                                IsOperation(syntaxItems[SyntaxItemIndex]);
                    }
                    else
                        break;
            }
            foreach (var error in Errors)
                Console.WriteLine(error);

        }

        public void PostParse()
        {
            foreach (var item in SyntaxItems)
            {
                //Assemble Operation
                //Assemble Commands
                //Output Code
            }
        }

        public class BlankLine : ISyntax
        {
            public int LineNumber { get; set; }
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
                    while (commentFound == false|| itemIndex <= itemCount)
                    {
                        if (lines[itemIndex].Contains(CommentIndicators.MultiLineEnd)) {
                            commentFound = true;
                            commentBuffer += lines[itemIndex];
                        }
                        else
                            commentBuffer += lines[itemIndex];

                        itemIndex++;
                        lineSpan++;
                    }
                }
                catch (ArgumentException e)
                {
                    Errors.Add($"Line {SyntaxItemIndex}: Exception thrown - The multi line comment is missing its closing indicator");
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

        public class Operation : IMemorySyntax
        {
            public int LineNumber { get; set; }
            public int MemoryAddress { get; set; }
            public int ByteLength { get; set; } = 4;
            public string Data { get; set; }
        }

        public void IsOperation(string item)
        {
            try { 
                var operation = new Operation()
                {
                    LineNumber = SyntaxItemIndex,
                    MemoryAddress = Address,
                    Data = Mips.Assemble(item)
                };
                SyntaxItems.Add(operation);
                Address += operation.ByteLength;
                SyntaxItemIndex++;
            }
            catch (ArgumentException e)
            {
                var types = new string[] { "Branch", "Code", "Register", "Integer", "Immediate", "Call" };
                var found = false;
                foreach (var type in types)
                    if (e.StackTrace.Contains($"Format{type}"))
                    {
                        Errors.Add($"Line {SyntaxItemIndex}: Exception thrown - Operation argument of type {type} is incorrectly typed");
                        found = true;
                    }
                if(!found)
                    Errors.Add($"Line {SyntaxItemIndex}: Exception thrown - Operation is incorrectly formatted");
            }
        }

        public void IsCommand(string item)
        {
            var commands = new string[] { "hexcode", "address", "setreg", "call", "print" };
            
        }

        public bool IsLabel(string item)
        {
            if (item.Contains(":") && Regex.IsMatch(item, @"([a-z0-9]{3,15}):", RegexOptions.IgnoreCase))
            {
                SyntaxItems.Add(new Label()
                {
                    LineNumber = SyntaxItemIndex,
                    MemoryAddress = Address,
                    Text = CodeDesigner.Parse.WithRegex(item, @"([_a-z0-9]{3,15}):")
                });
                SyntaxItemIndex++;
                return true;
            }
            return false;
        }

        public bool IsTargetLabel(string item)
        {
            if (item.Contains(":") && Regex.IsMatch(item, @":([a-z0-9]{3,15})", RegexOptions.IgnoreCase))
            {
                SyntaxItems.Add(new Label()
                {
                    LineNumber = SyntaxItemIndex,
                    MemoryAddress = Address,
                    Text = CodeDesigner.Parse.WithRegex(item, @":([_a-z0-9]{3,15})")
                });
                return true;
            }
            return false;
        }
    }

}
