using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeDesigner
{
    public static class Parse
    {
        public static List<string> BetweenMulti(string source, string startPad, string endPad)
        {
            var results = new List<string>();
            foreach (string item in source.Split(new string[] { endPad }, StringSplitOptions.None))
                if (item.Contains(startPad))
                    results.Add(item.Substring(item.IndexOf(startPad) + startPad.Length, item.Length - (item.IndexOf(startPad) + startPad.Length)));

            return results;
        }

        public static string BetweenSingle(string source, string startPad, string endPad)
        {
            foreach (string item in source.Split(new string[] { endPad }, StringSplitOptions.None))
                if (item.Contains(startPad))
                    return item.Substring(item.IndexOf(startPad) + startPad.Length, item.Length - (item.IndexOf(startPad) + startPad.Length));
            return null;
        }

        public static string WithRegex(string matchString, string pattern) => Regex.Match(matchString, pattern, RegexOptions.IgnoreCase).Groups[1].Value;
    }
}
