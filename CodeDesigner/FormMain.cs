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

            var source = "\t\taddiu sp, sp, $0080\n"
                + ":Winning12"
                + "\tsw a0, $0000(t0)\n"
                + "\tlw t0, $0000(a0)\n";

            var mipsSource = new MipsSource(source);
            mipsSource.Parse();

            var item1 = new Operation();
            var item2 = new Command();
            var item3 = new Comment();
            var item4 = new Label();

            var collection = new List<ISyntax>();

            collection.Add(item1);
            collection.Add(item2);
            collection.Add(item3);
            collection.Add(item4);

            foreach(var item in collection)
            {
                var type = item.GetType();
                Console.WriteLine(type);

                if (typeof(Label) == type)
                {
                    Console.WriteLine("Label found");
                }
            }

        }
    }
}
