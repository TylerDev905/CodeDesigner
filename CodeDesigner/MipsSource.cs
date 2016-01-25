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

        public List<Error> Logs { get; set; } = new List<Error>();
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

        public class Error
        {
            public int LineNumber { get; set; }
            public string Message { get; set; }
            public string Address { get; set; }
        }

        public MipsSource(string source)
        {
            Source = source;
        }

        public string Parse()
        {
            ILineCollection = new List<ILine>();
            LineNumber = 0;
            LineCount = 0;
            Labels = new List<Label>();
            AssembledCode = string.Empty;
            Address = 0;
            Logs = new List<Error>();

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
                            if (!IsIncrement(lines[LineNumber]))
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
            LineNumber = 0;
            return PatchLabels();
        }

        public void AddError(string message)
        {
            Logs.Add(new Error()
            {
                LineNumber = LineNumber + 1,
                Message = message,
                Address = Convert.ToString(Address, 16).PadLeft(8, '0')
            });
        }

        public string PatchLabels()
        {
            var result = string.Empty;
            for (var i = 0; i < ILineCollection.Count(); i++)
            {
                var iLine = ILineCollection[i];
                var iLineType = iLine.GetType();
                LineNumber = iLine.LineNumber;

                if (iLineType == typeof(Setreg) && iLine.HasLabel)
                {
                    var setreg = (Setreg)iLine;
                    IsSetreg(setreg.Line, true, i);
                }

                if (iLineType == typeof(Hexcode) && iLine.HasLabel)
                {
                    var hexcode = (Hexcode)iLine;
                    IsHexcode(hexcode.Line, true, i);
                }

                if (iLineType == typeof(Call) && iLine.HasLabel)
                {
                    var call = (Call)iLine;
                    IsCall(call.Line, true, i);
                }

                if (iLineType == typeof(Operation) && iLine.HasLabel)
                {
                    var operation = (Operation)iLine;
                    IsOperation(operation.Line, true, i);
                }

                if (iLineType == typeof(Increment))
                {
                    var increment = (Increment)iLine;
                }

                if (iLineType == typeof(Print))
                {
                    var print = (Print)iLine;
                }

                if (ILineCollection[i].AddressData != null)
                {
                    result += CodeFormatMulti(iLine.Address, ILineCollection[i].AddressData);
                    Address += iLine.AddressIncrement;
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
                    AddError($"Line {LineNumber + 1}: Exception thrown - The multi line comment is missing its closing indicator");
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
                    LineNumber++;
                    return true;
                }
                else
                {
                    AddError($"Line {LineNumber + 1}: Exception thrown - The address update cannot be parsed");
                }
                LineNumber++;
                return true;
            }
            return false;
        }

        public bool IsOperation(string item, bool isLabelAssemble = false, int? iLineIndex = null)
        {
            var data = string.Empty;
            var hasLabel = false;
            if (item.Contains(":"))
            {
                if (isLabelAssemble)
                {
                    try
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
                    catch
                    {
                        AddError($"Line {LineNumber + 1}: Exception thrown - Operation argument of type label is not defined");
                    }
                }
                hasLabel = true;
            }
            else
            {
                try
                {
                    data = Mips.Assemble(item);
                }
                catch (Exception e)
                {
                    var found = false;
                    var message = string.Empty;
                    foreach (var type in MipsArgTypes)
                    {
                        if (e.StackTrace.Contains($"Format{type}"))
                        {
                            message = $"Line {LineNumber + 1}: Exception thrown - Operation argument of type {type} is incorrectly typed";
                            found = true;
                        }
                    }
                    if (!found)
                        message = $"Line {LineNumber + 1}: Exception thrown - The Operation cannot be parsed";

                    AddError(message);
                }
            }
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
                    if (parsed.Groups.Count == 1)
                    {
                        AddError($"Line {LineNumber + 1}: Exception thrown - Hexcode argument of type hex cannot be parsed");
                    }
                    hex = parsed.Groups[1].Value;
                }

                if (item.Contains(":"))
                {
                    if (isLabelAssemble)
                    {
                        try
                        {
                            var parsed = Regex.Match(item, TargetPattern);
                            hex = Convert.ToString(Labels.Single(x => x.Text == parsed.Groups[1].Value).Address, 16);
                        }
                        catch
                        {
                            AddError($"Line {LineNumber + 1}: Exception thrown - Hexcode argument of type label is not defined");
                        }
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
                    register = CodeDesigner.Parse.WithRegex(item, $"setreg {RegisterPattern}");
                    hex = CodeDesigner.Parse.WithRegex(item, WordPattern);
                    if(register == string.Empty)
                        AddError($"Line {LineNumber + 1}: Exception thrown - Setreg argument of type register cannot be parsed");
                    if (hex ==  string.Empty)
                        AddError($"Line {LineNumber + 1}: Exception thrown - Setreg argument of type hex cannot be parsed");
                }

                if (item.Contains(":"))
                {
                    if (isLabelAssemble)
                    {
                        register = CodeDesigner.Parse.WithRegex(item, $"setreg {RegisterPattern}");
                        hex = CodeDesigner.Parse.WithRegex(item, TargetPattern);
                        try {
                            hex = Convert.ToString(Labels.Single(x => x.Text == hex).Address, 16).PadLeft(8, '0');
                        }
                        catch
                        {
                            AddError($"Line {LineNumber + 1}: Exception thrown - Setreg argument of type label cannot be parsed or does not exist");
                        }
                        if (register == string.Empty)
                            AddError($"Line {LineNumber + 1}: Exception thrown - Setreg argument of type register cannot be parsed");
                    }
                    hasLabel = true;
                }

                if (register != string.Empty)
                {
                    try
                    {
                        var hexUpper = hex.Substring(0, 4);
                        var hexLower = hex.Substring(4, 4);
                        data.Add(Mips.Assemble($"lui {register} ${hexUpper}"));
                        data.Add(Mips.Assemble($"ori {register}, {register}, ${hexLower}"));
                    }
                    catch { }
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
                    hex = CodeDesigner.Parse.WithRegex(item, WordPattern);
                    args = CodeDesigner.Parse.WithRegex(item, BetweenCurleyBraces).Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (hex == string.Empty)
                        AddError($"Line {LineNumber + 1}: Exception thrown - Call argument of type hex cannot be parsed");
                }

                if (item.Contains(":"))
                {
                    if (isLabelAssemble)
                    {
                        try
                        {
                            hex = CodeDesigner.Parse.WithRegex(item, TargetPattern);
                            hex = Convert.ToString(Labels.Single(x => x.Text == hex).Address, 16).PadLeft(8, '0');
                            args = CodeDesigner.Parse.WithRegex(item, BetweenCurleyBraces).Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        catch
                        {
                            AddError($"Line {LineNumber + 1}: Exception thrown - Call argument of type label cannot be parsed");
                        }
                    }
                    hasLabel = true;
                }

                var argCount = args.Count();
                var data = new List<string>();
                var paramReg = new List<string> { "a0", "a1", "a2", "a3", "t0", "t1", "t2", "t3", "t4", "t5", "t6", "t7", "t8", "t9" };
                for (var i = 0; i < argCount; i++)
                {
                    if (i == (argCount - 1))
                        data.Add(Mips.Assemble($"jal ${hex}"));
                    try
                    {
                        var register = Mips.Registers.Single(x => x.Name == args[i]);
                        data.Add(Mips.Assemble($"daddu {paramReg[i]}, {register.Name}, zero"));
                    }
                    catch
                    {
                        AddError($"Line {LineNumber + 1}: Exception thrown - Call argument {i + 1} cannot be parsed");
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
                if (parsed.Groups.Count == 1)
                {
                    AddError($"Line {LineNumber + 1}: Exception thrown - Print is missing a quote or contains an empty string");
                }
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

        public bool IsIncrement(string item, bool isLabelAssemble = false, int? iLineIndex = null)
        {
            var data = string.Empty;
            var register = string.Empty;

            try
            {
                if (item.Contains("++"))
                {

                    register = CodeDesigner.Parse.WithRegex(item, "([a-z0-9]{2,})");
                    data = Mips.Assemble($"addiu {register}, {register}, $0001");
                }

                if (item.Contains("--"))
                {
                    register = CodeDesigner.Parse.WithRegex(item, "([a-z0-9]{2,})");
                    data = Mips.Assemble($"addiu {register}, {register}, $FFFF");
                }
            }
            catch { }

            if (data != string.Empty)
            {
                ILineCollection.Add(new Increment()
                {
                    LineNumber = LineNumber,
                    Address = Address,
                    AddressData = new List<string> { data },
                    HasLabel = false,
                    Line = item
                });
                LineNumber++;
                Address += 4;
                return true;
            }
            return false;
        }
    }

}
