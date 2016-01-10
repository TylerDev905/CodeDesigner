using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner
{
    public static class Helper
    {
        public static string BinToHex(string binary, int padSize = 8) => Convert.ToString(Convert.ToInt32(binary.ToString(), 2), 16).PadLeft(padSize, '0');

        public static string HexToBin(string hex, int padSize = 32) => Convert.ToString(Convert.ToInt32(hex, 16), 2).PadLeft(padSize, '0');

        public static string InsertBin(int index, int size, string bin, string insertBin) => bin.Remove(index, size).Insert(index, insertBin);

        public static string ZeroPad(string paddingString, int size) => paddingString.PadLeft(size, '0');

        public static List<string[]> SplitRules(Instruction instruction)
        {
            List<string[]> results = new List<string[]>();

            foreach (string rule in instruction.ArgumentInfo.Split(new string[] { ";" }, StringSplitOptions.None))
                results.Add(rule.Split(new string[] { ":" }, StringSplitOptions.None));

            return results;
        }
    }
}
