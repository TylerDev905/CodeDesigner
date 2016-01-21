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

        public static string LabelPattern = @"([_.a-z0-9]{3,15}):";
        public static string TargetPattern = @":([_.a-z0-9]{3,15})";
        public static string WordPattern = "([a-f0-9]{8})";
        public static string HalfWordPattern = "([a-f0-9]{4})";
        public static string BytePatternPattern = "([a-f0-9]{2})";
        public static string RegisterPattern = @"([a-z0-9]{2,})";
        public static string BetweenCurleyBraces = @"\((.{1,})\)";
        public static string BetweenQuotes = "\"(.{1,})\"";
        public static string MultiLineStart = "/*";
        public static string MultiLineEnd = "*/";
        public static string SingleLine = "//";

        public MipsSource(string source)
        {
            Address = 0;
            LineNumber = 0;
            Source = source;
        }

        public string Parse()
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
                            if (!IsAddress(lines[LineNumber]))
                                if (!IsLabel(lines[LineNumber]))
                                    if (!IsHexcode(lines[LineNumber]))
                                        if (!IsSetreg(lines[LineNumber]))
                                            if (!IsCall(lines[LineNumber]))
                                                if (!IsPrint(lines[LineNumber]))
                                                    IsOperation(lines[LineNumber]);
                    }
                    else
                        return string.Empty;
            }


            return PatchLabels();
        }

        public string PatchLabels()
        {
            var result = string.Empty;

            for (var i = 0; i < ILineCollection.Count(); i++)
            {
                var iLine = ILineCollection[i];
                var iLineType = iLine.GetType();

                if (iLineType == typeof(Setreg))
                {
                    var setreg = (Setreg)iLine;
                    IsSetreg(setreg.Line, true, i);
                    result += CodeFormatMulti(setreg.Address, ILineCollection[i].AddressData);
                }

                if (iLineType == typeof(Hexcode))
                {
                    var hexcode = (Hexcode)iLine;
                    IsHexcode(hexcode.Line, true, i);
                    result += CodeFormatMulti(hexcode.Address, ILineCollection[i].AddressData);
                }

                if (iLineType == typeof(Call))
                {
                    var call = (Call)iLine;
                    IsCall(call.Line, true, i);
                    result += CodeFormatMulti(call.Address, ILineCollection[i].AddressData);
                }

                if (iLineType == typeof(Print))
                {
                    var print = (Print)iLine;
                    result += CodeFormatMulti(print.Address, ILineCollection[i].AddressData);
                }

                if (iLineType == typeof(Operation))
                {
                    var operation = (Operation)iLine;
                    IsOperation(operation.Line, true, i);
                    result += CodeFormatMulti(operation.Address, ILineCollection[i].AddressData);
                }
            }
            return result;
        }

        public string CodeFormatSingle(int address, string data) => $"{Convert.ToString(address, 16).PadLeft(8, '0')} {data}\r\n";

        public string CodeFormatMulti(int address, List<string> data)
        {
            var result = string.Empty;
            foreach (var item in data)
            {
                result += CodeFormatSingle(address, item);
                address += 4;
            }
            return result;
        }

        public interface ILine
        {
            int LineNumber { get; set; }
            int AddressIncrement { get; set; }
            int Address { get; set; }
            bool HasLabel { get; set; }
            List<string> AddressData { get; set; }
        }

        public class BlankLine : ILine
        {
            public int LineNumber { get; set; }
            public List<string> AddressData { get; set; }
            public int AddressIncrement { get; set; } = 0;
            public int Address { get; set; }
            public bool HasLabel { get; set; } = false;
        }

        public class Comment : ILine
        {
            public int LineNumber { get; set; }
            public int LinePosition { get; set; }
            public List<string> AddressData { get; set; }
            public int LineSpan { get; set; }
            public string Text { get; set; }
            public int AddressIncrement { get; set; } = 0;
            public int Address { get; set; }
            public bool HasLabel { get; set; } = false;
        }

        public class AddressUpdate : ILine
        {
            public int LineNumber { get; set; }
            public int NewAddress { get; set; }
            public List<string> AddressData { get; set; }
            public int AddressIncrement { get; set; } = 0;
            public int Address { get; set; }
            public bool HasLabel { get; set; } = false;
        }

        public class Label : ILine
        {
            public int LineNumber { get; set; }
            public string Text { get; set; }
            public int Address { get; set; }
            public List<string> AddressData { get; set; }
            public int AddressIncrement { get; set; } = 0;
            public bool HasLabel { get; set; } = false;
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

        public bool IsLabel(string item, bool isLabelAssemble = false)
        {
            if (item.Contains(":") && Regex.IsMatch(item, LabelPattern, RegexOptions.IgnoreCase))
            {
                var label = new Label()
                {
                    LineNumber = LineNumber,
                    Address = Address,
                    Text = CodeDesigner.Parse.WithRegex(item, LabelPattern)
                };

                ILineCollection.Add(label);
                Labels.Add(label);
                LineNumber++;
                return true;
            }
            return false;
        }

        public bool IsAddress(string item, bool isLabelAssemble = false)
        {
            if (item.Contains("address"))
            {
                if (Regex.IsMatch(item, WordPattern, RegexOptions.IgnoreCase))
                {
                    var newAddress = Convert.ToInt32(CodeDesigner.Parse.WithRegex(item, WordPattern), 16);
                    ILineCollection.Add(new AddressUpdate()
                    {
                        LineNumber = LineNumber,
                        Address = Address,
                        AddressIncrement = 0,
                        NewAddress = newAddress
                    });
                    Address = newAddress;
                }
                else
                    Logs.Add($"Line {LineNumber + 1}: Exception thrown - The address update cannot be parsed");
                LineNumber++;
                return true;
            }
            return false;
        }

        public class Operation : ILine
        {
            public int LineNumber { get; set; }
            public string Line { get; set; }
            public List<string> AddressData { get; set; }
            public int AddressIncrement { get; set; } = 4;
            public int Address { get; set; }
            public bool HasLabel { get; set; }
        }

        public class Setreg : ILine
        {
            public int LineNumber { get; set; }
            public string Line { get; set; }
            public int AddressIncrement { get; set; } = 8;
            public List<string> AddressData { get; set; }
            public int Address { get; set; }
            public bool HasLabel { get; set; }
        }

        public class Call : ILine
        {
            public int LineNumber { get; set; }
            public string Line { get; set; }
            public int AddressIncrement { get; set; }
            public List<string> AddressData { get; set; }
            public int Address { get; set; }
            public bool HasLabel { get; set; }
        }

        public class Hexcode : ILine
        {
            public int LineNumber { get; set; }
            public string Line { get; set; }
            public int AddressIncrement { get; set; } = 4;
            public List<string> AddressData { get; set; }
            public int Address { get; set; }
            public bool HasLabel { get; set; }
        }

        public class Print : ILine
        {
            public int LineNumber { get; set; }
            public int NewAddress { get; set; }
            public List<string> AddressData { get; set; }
            public int AddressIncrement { get; set; } = 0;
            public int Address { get; set; }
            public bool HasLabel { get; set; } = false;
        }

        public bool IsOperation(string item, bool isLabelAssemble = false, int? iLineIndex = null)
        {
            var data = string.Empty;
            var hasLabel = false;

            if (item.Contains(":"))
            {
                if (isLabelAssemble)
                {
                    var parsed = Regex.Match(item, TargetPattern);
                    var label = Labels.Single(x => x.Text == parsed.Groups[1].Value);

                    var instruction = Mips.Instructions.Single(x => x.Name.Equals(Helper.ParseInstructionName(item.ToUpper())));
                    var syntaxArgs = Helper.ParseArgs(instruction.Syntax);

                    if (syntaxArgs.Contains(Placeholders.Call))
                    {
                        item = item.Replace($":{label.Text}", $"${Helper.ZeroPad(Convert.ToString(label.Address, 16), 8)}");
                        data = Mips.Assemble(item);
                    }
                    else
                    {
                        var value = 0;
                        if (label.Address > ILineCollection[(int)iLineIndex].Address)
                            value = (label.Address - ILineCollection[(int)iLineIndex].Address);
                        else
                            value = ((ILineCollection[(int)iLineIndex].Address - label.Address) * -1) - 4;

                        var offset = Helper.ZeroPad(Convert.ToString(value, 16), 4);
                        item = item.Replace($":{label.Text}", $"${offset.Substring(offset.Count() - 4, 4)}");
                        data = Mips.Assemble(item);
                    }
                }
                hasLabel = true;
            }
            else
                data = Mips.Assemble(item);

            var operation = new Operation()
            {
                LineNumber = LineNumber,
                Address = Address,
                AddressData = new List<string> { data },
                HasLabel = hasLabel,
                Line = item
            };

            if (!isLabelAssemble)
                ILineCollection.Add(operation);
            else
                ILineCollection[(int)iLineIndex] = operation;

            LineNumber++;
            Address += 4;
            return true;
        }

        public bool IsHexcode(string item, bool isLabelAssemble = false, int? iLineIndex = null)
        {
            if (item.Contains("hexcode"))
            {
                var hex = string.Empty;
                var hasLabel = false;
                if (item.Contains("$"))
                {
                    var parsed = Regex.Match(item, WordPattern);
                    hex = parsed.Groups[1].Value;
                }

                if (item.Contains(":"))
                {
                    if (isLabelAssemble)
                    {
                        var parsed = Regex.Match(item, TargetPattern);
                        hex = Convert.ToString(Labels.Single(x => x.Text == parsed.Groups[1].Value).Address, 16);
                    }
                    hasLabel = true;
                }

                var hexcode = new Hexcode()
                {
                    LineNumber = LineNumber,
                    Address = Address,
                    AddressData = new List<string> { hex.PadLeft(8, '0') },
                    HasLabel = hasLabel,
                    Line = item
                };

                if (!isLabelAssemble)
                    ILineCollection.Add(hexcode);
                else
                    ILineCollection[(int)iLineIndex] = hexcode;

                LineNumber++;
                Address += 4;
                return true;
            }
            return false;
        }

        public bool IsSetreg(string item, bool isLabelAssemble = false, int? iLineIndex = null)
        {
            if (item.Contains("setreg"))
            {
                var hex = string.Empty;
                var register = string.Empty;
                var data = new List<string>();
                var hasLabel = false;

                if (item.Contains("$"))
                {
                    var parsed = Regex.Match(item, $"{RegisterPattern}\\, \\${WordPattern}");
                    register = parsed.Groups[1].Value;
                    hex = parsed.Groups[2].Value;
                }

                if (item.Contains(":"))
                {
                    if (isLabelAssemble)
                    {
                        var parsed = Regex.Match(item, $"{RegisterPattern}, {TargetPattern}");
                        register = parsed.Groups[1].Value;
                        hex = Convert.ToString(Labels.Single(x => x.Text == parsed.Groups[2].Value).Address, 16).PadLeft(8, '0');
                    }
                    hasLabel = true;
                }

                if (register != string.Empty)
                {
                    var hexUpper = hex.Substring(0, 4);
                    var hexLower = hex.Substring(4, 4);
                    data.Add(Mips.Assemble($"lui {register} ${hexUpper}"));
                    data.Add(Mips.Assemble($"ori {register}, {register}, ${hexLower}"));
                }

                var setreg = new Setreg()
                {
                    LineNumber = LineNumber,
                    Address = Address,
                    AddressData = data,
                    HasLabel = hasLabel,
                    Line = item
                };

                if (!isLabelAssemble)
                    ILineCollection.Add(setreg);
                else
                    ILineCollection[(int)iLineIndex] = setreg;

                LineNumber++;
                Address += 8;
                return true;
            }
            return false;
        }

        public bool IsCall(string item, bool isLabelAssemble = false, int? iLineIndex = null)
        {
            if (item.Contains("call"))
            {
                var hex = string.Empty;
                var args = new string[] { };
                var hasLabel = false;

                if (item.Contains("$"))
                {
                    var parsed = Regex.Match(item, WordPattern + BetweenCurleyBraces);
                    hex = parsed.Groups[1].Value;
                    args = parsed.Groups[2].Value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                }

                if (item.Contains(":") && isLabelAssemble)
                {
                    if (isLabelAssemble)
                    {
                        var parsed = Regex.Match(item, TargetPattern + BetweenCurleyBraces);
                        hex = Convert.ToString(Labels.Single(x => x.Text == parsed.Groups[1].Value).Address, 16).PadLeft(8, '0');
                        args = parsed.Groups[2].Value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    hasLabel = true;
                }

                var argCount = args.Count();
                var data = new List<string>();

                if (isLabelAssemble)
                {
                    var paramReg = new List<string> { "a0", "a1", "a2", "a3", "t0", "t1", "t2", "t3", "t4", "t5", "t6", "t7", "t8", "t9" };
                    for (var i = 0; i < argCount; i++)
                    {
                        if (i == (argCount - 1))
                            data.Add(Mips.Assemble($"jal ${hex}"));

                        var register = Mips.Registers.Single(x => x.Name == args[i]);
                        data.Add(Mips.Assemble($"daddu {paramReg[i]}, {register.Name}, zero"));

                    }
                }

                var addressIncrement = argCount * 4 + 4;
                var call = new Call()
                {
                    LineNumber = LineNumber,
                    Address = Address,
                    AddressData = data,
                    AddressIncrement = addressIncrement,
                    HasLabel = hasLabel,
                    Line = item
                };

                if (!isLabelAssemble)
                    ILineCollection.Add(call);
                else
                    ILineCollection[(int)iLineIndex] = call;

                LineNumber++;
                Address += addressIncrement;
                return true;
            }
            return false;
        }

        public bool IsPrint(string item, bool isLabelAssemble = false, int? iLineIndex = null)
        {
            if (item.Contains("print"))
            {
                var parsed = Regex.Match(item, BetweenQuotes);
                var bytes = Encoding.ASCII.GetBytes(parsed.Groups[1].Value);
                var data = new List<string>();

                var hex = string.Empty;
                var i = 1;
                foreach (char chr in parsed.Groups[1].Value)
                {
                    if (i == 5)
                    {
                        data.Add(hex);
                        hex = string.Empty;
                        i = 1;
                    }
                    hex = Convert.ToString(Convert.ToInt32(chr), 16) + hex;
                    i++;
                }
                if (i > 1 && i <= 5)
                    data.Add(hex.PadLeft(8, '0'));

                var addressIncrement = data.Count() * 4;
                ILineCollection.Add(new Print()
                {
                    LineNumber = LineNumber,
                    Address = Address,
                    AddressData = data,
                    AddressIncrement = data.Count() * 4
                });

                LineNumber++;
                Address += addressIncrement;
                return true;
            }
            return false;
        }

    }

}
