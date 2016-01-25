using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeDesigner
{
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

    public class Increment : ILine
    {
        public int LineNumber { get; set; }
        public string Line { get; set; }
        public List<string> AddressData { get; set; }
        public int AddressIncrement { get; set; } = 4;
        public int Address { get; set; }
        public bool HasLabel { get; set; }
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
}
