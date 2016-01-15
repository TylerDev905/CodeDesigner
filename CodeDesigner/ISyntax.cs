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

    public class Operation : IMemorySyntax
    {
        public int LineNumber { get; set; }
        public int MemoryAddress { get; set; }
        public int ByteLength { get; set; } = 4;
        public string Data { get; set; }
    }

    public class Command : IMemorySyntax
    {
        public int LineNumber { get; set; }
        public int MemoryAddress { get; set; }
        public int ByteLength { get; set; }
        public List<string> Data { get; set; }
    }

    public class Comment : ISyntax
    {
        public int LineNumber { get; set; }
        public int LineLength { get; set; }
        public string Text { get; set; }
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
        public int ItemIndex { get; set; }
        public List<ISyntax> SyntaxItems { get; set; } = new List<ISyntax>();
        public string Source { get; set; } = string.Empty;
        public Mips32 Mips { get; set; } = new Mips32();
        public List<string>Errors { get; set; } = new List<string>();

        public MipsSource(string source)
        {
            Address = 0;
            ItemIndex = 0;
            Source = source;
        }

        public MipsSource(int address, int itemIndex, string source)
        {
            Address = address;
            ItemIndex = itemIndex;
            Source = source;
        }
        
        public void Parse()
        {
            var source = Source;
            foreach (var item in new string[] { "\n", "\t", "\n\r" })
                source = source.Replace(item, "#");

            var syntaxItems = source.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ItemIndex = 1;
            foreach (var syntaxItem in syntaxItems)
            {
                if(!IsLabel(syntaxItem))
                    if(!IsTargetLabel(syntaxItem))
                        IsOperation(syntaxItem);
                ItemIndex++;
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
  
        public bool IsComment()
        {
            var multiLine = new string[]{ "/*", "*/" };
            var singleLine = "//";

            return false;
        }

        public void IsOperation(string item)
        {
            try {
                SyntaxItems.Add(new Operation()
                {
                    LineNumber = ItemIndex,
                    MemoryAddress = Address,
                    Data = Mips.Assemble(item)
                });

                Address += 4;
            }
            catch (ArgumentException e)
            {
                var types = new string[] { "Branch", "Code", "Register", "Integer", "Immediate", "Call" };
                var found = false;
                foreach (var type in types)
                    if (e.StackTrace.Contains($"Format{type}"))
                    {
                        Errors.Add($"Line {ItemIndex}: Exception thrown - Operation argument of type {type}");
                        found = true;
                    }
                if(!found)
                    Errors.Add($"Line {ItemIndex}: Exception thrown - Operation incorrect format");
            }
        }

        public void IsCommand(string item)
        {
            
        }

        public bool IsLabel(string item)
        {
            if (item.Contains(":") && Regex.IsMatch(item, @"\:([a-z0-9]{3,15})", RegexOptions.IgnoreCase))
            {
                SyntaxItems.Add(new Label()
                {
                    LineNumber = ItemIndex,
                    MemoryAddress = Address,
                    Text = CodeDesigner.Parse.WithRegex(item, @"\:([_a-z0-9]{3,15})")
                });
                return true;
            }
            return false;
        }

        public bool IsTargetLabel(string item)
        {
            if (item.Contains(":") && Regex.IsMatch(item, @"([a-z0-9]{3,15})\:", RegexOptions.IgnoreCase))
            {
                SyntaxItems.Add(new Label()
                {
                    LineNumber = ItemIndex,
                    MemoryAddress = Address,
                    Text = CodeDesigner.Parse.WithRegex(item, @"([_a-z0-9]{3,15})\:")
                });
                return true;
            }
            return false;
        }
    }

}
