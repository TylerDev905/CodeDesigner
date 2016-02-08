using FastColoredTextBoxNS;
using System.Drawing;

namespace CodeDesigner
{
    public static class Theme
    {

        public static FontStyle Font { get; set; } = FontStyle.Regular;

        public static Brush CommentBrush { get; set; } = Brushes.Green;
        public static Brush HexBrush { get; set; } = Brushes.OldLace;
        public static Brush StringBrush { get; set; } = Brushes.IndianRed;
        public static Brush LabelBrush { get; set; } = Brushes.DodgerBlue;
        public static Brush RegisterBrush1 { get; set; } = Brushes.Goldenrod;
        public static Brush RegisterBrush2 { get; set; } = Brushes.YellowGreen;
        public static Brush RegisterBrush3 { get; set; } = Brushes.CornflowerBlue;
        public static Brush RegisterBrush4 { get; set; } = Brushes.OrangeRed;
        public static Brush RegisterBrush5 { get; set; } = Brushes.LightGray;
        public static Brush RegisterBrush6 { get; set; } = Brushes.MediumPurple;
        public static Brush ExceptionBrush { get; set; } = Brushes.OrangeRed;

        public static Style CommentStyle { get; set; } = new TextStyle(CommentBrush, null, Font);
        public static Style HexStyle { get; set; } = new TextStyle(HexBrush, null, Font);
        public static Style StringStyle { get; set; } = new TextStyle(StringBrush, null, Font);
        public static Style LabelStyle { get; set; } = new TextStyle(LabelBrush, null, Font);
        public static Style RegisterStyle1 { get; set; } = new TextStyle(RegisterBrush1, null, Font);
        public static Style RegisterStyle2 { get; set; } = new TextStyle(RegisterBrush2, null, Font);
        public static Style RegisterStyle3 { get; set; } = new TextStyle(RegisterBrush3, null, Font);
        public static Style RegisterStyle4 { get; set; } = new TextStyle(RegisterBrush4, null, Font);
        public static Style RegisterStyle5 { get; set; } = new TextStyle(RegisterBrush5, null, Font);
        public static Style RegisterStyle6 { get; set; } = new TextStyle(RegisterBrush6, null, Font);
        public static Style ExceptionStyle { get; set; } = new TextStyle(ExceptionBrush, null, Font);
        
        public static string LabelPattern = @"([_.\[\]a-z0-9]{3,}):";
        public static string TargetPattern = @":([_.\[\]a-z0-9]{3,})";

        public static string WordPattern = "([a-f0-9]{8})";
        public static string HalfWordPattern = @"([a-f0-9]{4})";
        public static string NothingAheadPattern = @"(?=[ \(]{1})";

        public static string RegisterPattern1 = "s0|s1|s2|s3|s4|s5|s6|s7|sp";
        public static string RegisterPattern2 = "ra|v0|v1";
        public static string RegisterPattern3 = "a0|a1|a2|a3";
        public static string RegisterPattern4 = "t0|t1|t2|t3|t4|t5|t6|t7|t8|t9";
        public static string RegisterPattern5 = "k0|k1|at";
        public static string RegisterPattern6 = "gp|fp|zero";
  
        public static string BytePatternPattern = "([a-f0-9]{2})";
        public static string RegisterPattern = @"([a-z0-9]{2,})";
        public static string CurleyBracesPattern = @"\((.{1,})\)";
        public static string QuotesPattern = "\"(.{1,})\"";
        public static string ExceptionPattern = "Line [0-9]{1,}: Exception thrown - [a-z0-9 ,!.]{1,}";

        public static string BMSTypes = "long|double|integer|float|string|char";
        public static string BMSStatements = "goto|for|if|else|else if|while|end";
        public static string BMSSpecial2 = "get|set|offset";
        public static string BMSSpecial1 = "math";
        public static string BMSHex = "0x[0-9a-f]{1,}";

    }
}
