using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeDesigner
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            var source = System.IO.File.ReadAllText(@"C:\Users\Tyler\Desktop\mips.txt");

            var mipsSource = new MipsSource(source);
            mipsSource.Parse();
            Console.Write(mipsSource.ToCode());

            var collection = new List<MipsSource.ISyntax>();

            foreach(var item in mipsSource.SyntaxItems)
            {
                var type = item.GetType();
                Console.WriteLine(type);
            }
            
        }
    }
}
