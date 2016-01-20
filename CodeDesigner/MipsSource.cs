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
        public string Source { get; set; } = string.Empty;
        public List<ILine> ILineCollection { get; set; } = new List<ILine>();
        public int LineNumber { get; set; }
        public int LineCount { get; set; }
        public List<Label> Labels { get; set; } = new List<Label>();

        public string AssembledCode { get; set; }
        public int Address { get; set; }

        public List<string> Logs { get; set; } = new List<string>();
        public bool Exit { get; set; }

        public Mips32 Mips { get; set; } = new Mips32();
        public static List<string> MipsArgTypes = new List<string> { "Branch", "Code", "Register", "Integer", "Immediate", "Call" };
        public static List<string> Commands = new List<string> { "hexcode", "setreg", "call", "print" };

        public static string LabelPattern = @"([_.a-z0-9]{3,15}):";
        public static string TargetPattern = @":([_.a-z0-9]{3,15})";
        public static string WordPattern = "([a-f0-9]{8})";

        public static string MultiLineStart = "/*";
        public static string MultiLineEnd = "*/";
        public static string SingleLine = "//";


        public MipsSource(string source)
        {
            Address = 0;
            LineNumber = 0;
            Source = source;
        }

        public void Parse()
        {
            Exit = false;
            var source = Source
                .Replace("\t", "")
                .Replace("\r", "")
                .Replace("\n", "  ");

            var lines = source.Split(new string[] { "  " }, StringSplitOptions.None).ToList();
            LineCount = lines.Count();

            for (LineNumber = 0; LineNumber < lines.Count();)
            {
                if (!IsComment(lines))
                    if (!Exit)
                    {
                        if (!IsBlankLine(lines[LineNumber]))
                            if (!IsLabel(lines[LineNumber]))
                                if (!IsAddressUpdate(lines[LineNumber]))
                                    if (!IsTargetLabel(lines[LineNumber]))
                                        if (!IsCommand(lines[LineNumber]))
                                            IsOperation(lines[LineNumber]);
                    }
                    else
                        break;
            }
        }

        public string ToCode()
        {
            var result = string.Empty;
            for (var i = 0; i < ILineCollection.Count(); i++)
            {
                var iLine = ILineCollection[i];
                var iLineType = iLine.GetType();

                var address = Helper.ZeroPad(Convert.ToString(iLine.Address, 16), 8);
                var addressData = string.Empty;

                if (iLineType == typeof(Operation))
                    addressData = ((Operation)iLine).AddressData;

                if (iLineType == typeof(TargetLabel))
                    addressData = TargetLabelAssemble(iLine);

                if (iLineType == typeof(Hexcode))
                    addressData = ((Hexcode)iLine).AddressData;

                if (addressData != string.Empty)
                    result += $"{address} {addressData}\r\n";
            }
            return result;
        }

        public string TargetLabelAssemble(ILine iLine)
        {
            var result = string.Empty;
            var target = ((TargetLabel)iLine);
            try
            {
                var label = Labels.Single(x => x.Text == target.Text);
                if (target.IsOperation)
                {
                    result = target.Line;
                    var instruction = Mips.Instructions.Single(x => x.Name.Equals(Helper.ParseInstructionName(result.ToUpper())));
                    var syntaxArgs = Helper.ParseArgs(instruction.Syntax);

                    if (syntaxArgs.Contains(Placeholders.Call))
                        result = result.Replace($":{label.Text}", $"${Helper.ZeroPad(Convert.ToString(label.Address, 16), 8)}");
                    else
                    {
                        var value = 0;
                        if (label.Address > target.Address)
                            value = (label.Address - target.Address);
                        else
                            value = ((target.Address - label.Address) * -1) - 4;

                        var offset = Helper.ZeroPad(Convert.ToString(value, 16), 4);
                        result = result.Replace($":{label.Text}", $"${offset.Substring(offset.Count() - 4, 4)}");
                    }
                    return Mips.Assemble(result);
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Sequence contains more than one matching element"))
                    Logs.Add($"Line {target.LineNumber + 1}: Exception thrown - The label that is being referenced contains more then one definition");
                else
                    Logs.Add($"Line {target.LineNumber + 1}: Exception thrown - The label that is being referenced does not exist");
            }
            return result;
        }

        public interface ILine
        {
            int LineNumber { get; set; }
            int AddressIncrement { get; set; }
            int Address { get; set; }
        }

        public class BlankLine : ILine
        {
            public int LineNumber { get; set; }
            public int AddressIncrement { get; set; } = 0;
            public int Address { get; set; }
        }

        public bool IsBlankLine(string line)
        {
            if (line == string.Empty)
            {
                ILineCollection.Add(new BlankLine() { LineNumber = LineNumber });
                LineNumber++;
                return true;
            }
            return false;
        }

        public class Comment : ILine
        {
            public int LineNumber { get; set; }
            public int LinePosition { get; set; }
            public int LineSpan { get; set; }
            public string Text { get; set; }
            public int AddressIncrement { get; set; } = 0;
            public int Address { get; set; }
        }

        public bool IsComment(List<string> lines)
        {
            var commentFound = false;
            var commentBuffer = string.Empty;
            var lineSpan = 0;

            // Is single line comment?
            if (lines[LineNumber].Contains(SingleLine))
            {
                // Is the comment at the start of the string or beside syntax
                if (lines[LineNumber].StartsWith(SingleLine))
                {
                    commentBuffer = lines[LineNumber];
                    lineSpan = 1;
                }
                else
                {
                    var startIndex = lines[LineNumber].IndexOf(SingleLine);
                    commentBuffer += lines[LineNumber].Substring(startIndex, lines[LineNumber].Length - startIndex);
                    lines[LineNumber] = lines[LineNumber].Replace(commentBuffer, "").Trim(' ');
                }
                commentFound = true;
            }

            // Is multiline comment?
            if (lines[LineNumber].Contains(MultiLineStart))
            {
                // Is the multiline comment finshed on this line?
                if (lines[LineNumber].Contains(MultiLineEnd))
                {
                    var startIndex = lines[LineNumber].IndexOf(MultiLineStart);
                    var endIndex = lines[LineNumber].IndexOf(MultiLineEnd);
                    commentBuffer += lines[LineNumber].Substring(startIndex, (endIndex - startIndex + 2));
                    lines[LineNumber] = lines[LineNumber].Replace(commentBuffer, "").Trim(' ');
                    commentFound = true;
                }
                try
                {
                    // find the end of the comment
                    var tempLineNumber = LineNumber;
                    while (commentFound == false || tempLineNumber < LineCount - 1)
                    {
                        // Is this the end of the comment?
                        if (lines[tempLineNumber].Contains(MultiLineEnd))
                        {
                            commentFound = true;
                            commentBuffer += lines[tempLineNumber];
                            break;
                        }
                        else
                            commentBuffer += lines[tempLineNumber];

                        tempLineNumber++;
                        lineSpan++;
                    }
                    lineSpan++;

                }
                catch
                {
                    Exit = true;
                    Logs.Add($"Line {LineNumber + 1}: Exception thrown - The multi line comment is missing its closing indicator");
                }
            }
            // If a comment was found add it to the ILine collection
            if (commentFound)
            {
                ILineCollection.Add(new Comment()
                {
                    Text = commentBuffer,
                    LineSpan = lineSpan,
                    LineNumber = LineNumber
                });

                LineNumber += lineSpan;
                return true;
            }
            return false;
        }


        public class Operation : ILine
        {
            public int LineNumber { get; set; }
            public string AddressData { get; set; }
            public int AddressIncrement { get; set; } = 4;
            public int Address { get; set; }
        }

        public bool IsOperation(string item)
        {
            try
            {
                var operation = new Operation()
                {
                    LineNumber = LineNumber,
                    Address = Address,
                    AddressData = Mips.Assemble(item)
                };
                ILineCollection.Add(operation);
                Address += operation.AddressIncrement;
                LineNumber++;
                return true;
            }
            catch (ArgumentException e)
            {
                var found = false;
                foreach (var type in MipsArgTypes)
                    if (e.StackTrace.Contains($"Format{type}"))
                    {
                        Logs.Add($"Line {LineNumber + 1}: Exception thrown - Operation argument of type {type} is incorrectly typed");
                        found = true;
                    }
                if (!found)
                    Logs.Add($"Line {LineNumber + 1}: Exception thrown - The Operation cannot be parsed");
                LineNumber++;
                return false;
            }
        }

        public class AddressUpdate : ILine
        {
            public int LineNumber { get; set; }
            public int NewAddress { get; set; }
            public int AddressIncrement { get; set; } = 0;
            public int Address { get; set; }
        }

        public bool IsAddressUpdate(string item)
        {
            if (item.Contains("address"))
            {
                try
                {
                    var newAddress = Convert.ToInt32(CodeDesigner.Parse.WithRegex(item, WordPattern), 16);
                    Address = newAddress;
                    ILineCollection.Add(new AddressUpdate()
                    {
                        LineNumber = LineNumber,
                        NewAddress = newAddress
                    });
                    LineNumber++;
                }
                catch
                {
                    Logs.Add($"Line {LineNumber + 1}: Exception thrown - The address update cannot be parsed");
                }
                return true;
            }
            return false;
        }

        public class Command : ILine
        {
            public int LineNumber { get; set; }
            public int AddressIncrement { get; set; }
            public List<string> AddressData { get; set; }
            public int Address { get; set; }
        }

        public class Hexcode : ILine
        {
            public int LineNumber { get; set; }
            public int AddressIncrement { get; set; } = 4;
            public string AddressData { get; set; }
            public int Address { get; set; }
        }

        public bool IsCommand(string item)
        {
            if (item.Contains("hexcode"))
            {
                var addressData = CodeDesigner.Parse.WithRegex(item, WordPattern);
                ILineCollection.Add(new Hexcode()
                {
                    LineNumber = LineNumber,
                    Address = Address,
                    AddressData = addressData
                });
                Address += 4;
                LineNumber++;
                return true;
            }
            return false;
        }

        public class Label : ILine
        {
            public int LineNumber { get; set; }
            public string Text { get; set; }
            public int Address { get; set; }
            public int AddressIncrement { get; set; } = 0;
        }

        public bool IsLabel(string item)
        {
            if (item.Contains(":") && Regex.IsMatch(item, LabelPattern, RegexOptions.IgnoreCase))
            {
                var text = CodeDesigner.Parse.WithRegex(item, LabelPattern);
                var label = new Label()
                {
                    LineNumber = LineNumber,
                    Address = Address,
                    Text = text
                };

                ILineCollection.Add(label);
                Labels.Add(label);
                Logs.Add(($"Line {LineNumber + 1}: Found Label [ {text} ] - 0x{Helper.ZeroPad(Convert.ToString(Address, 16), 8)}"));
                LineNumber++;
                return true;
            }
            return false;
        }

        public class TargetLabel : ILine
        {
            public int LineNumber { get; set; }
            public string Label { get; set; }
            public string Line { get; set; }
            public bool IsOperation { get; set; }
            public int AddressIncrement { get; set; } = 0;
            public int Address { get; set; }
            public string Text { get; set; }
        }

        public bool IsTargetLabel(string item)
        {
            if (item.Contains(":") && Regex.IsMatch(item, TargetPattern, RegexOptions.IgnoreCase))
            {
                var text = CodeDesigner.Parse.WithRegex(item, TargetPattern);
                ILineCollection.Add(new TargetLabel()
                {
                    LineNumber = LineNumber,
                    Label = CodeDesigner.Parse.WithRegex(item, TargetPattern),
                    Line = item,
                    IsOperation = Commands.Where(x => item.Contains(x)).Count() == 0 ? true : false,
                    Text = text,
                    Address = Address
                });
                Logs.Add(($"Line {LineNumber + 1}: Found Target Label [ {text} ] - 0x{Helper.ZeroPad(Convert.ToString(Address, 16), 8)}"));
                Address += 4;

                LineNumber++;
                return true;
            }
            return false;
        }
    }

}
