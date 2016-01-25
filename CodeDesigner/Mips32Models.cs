using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeDesigner
{
    public enum MemoryMap
    {
        EE = 1,
        COP0 = 2,
        COP1 = 3
    }

    public partial class Register
    {
        public int RegisterID { get; set; }
        public MemoryMap MemoryID { get; set; }
        public string Name { get; set; }
        public string Hex { get; set; }
        public string Bin { get; set; }
        public string Info { get; set; }
    }

    public partial class Instruction
    {
        public int InstructionID { get; set; }
        public MemoryMap MemoryID { get; set; }
        public string Name { get; set; }
        public string Syntax { get; set; }
        public string InstructionBin { get; set; }
        public string ArgumentBin { get; set; }
        public string ArgumentInfo { get; set; }
        public string Info { get; set; }
    }

    public static class Placeholders
    {
        public static string[] EERegister { get; } = new string[] { "rt", "rs", "rd", "base" };
        public static string COP0Register { get; } = "c0reg";
        public static string[] COP1Register { get; } = new string[] { "ft", "fs", "fd" };
        public static string Integer { get; } = "sa";
        public static string Code { get; } = "code";
        public static string[] Immediate { get; } = new string[] { "immediate", "offset(", "offset" };
        public static string Call { get; } = "target";
        public static string Branch { get; } = "offset";
    }
}
