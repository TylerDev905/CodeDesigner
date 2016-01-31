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
    public partial class FormSearch : Form
    {
        public List<Label> Collection { get; set; }
        public string Address { get; set; } = string.Empty;
        public List<string> ListBoxItems { get; set; }

        public FormSearch()
        {
            InitializeComponent();
            textBox1.TextChanged += new EventHandler(textBox1_Changed);
            listBox1.MouseDoubleClick += new MouseEventHandler(SelectedItem);
            this.Text = "Labels";
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            ListBoxItems = new List<string>(); 
            foreach (var item in Collection)
            {
                ListBoxItems.Add(Convert.ToString(item.Address, 16).PadLeft(8, '0') + " " + item.Text);
            }

            listBox1.Items.AddRange(ListBoxItems.ToArray());
            listBox1.Update();
        }

        private void SelectedItem(object sender, EventArgs e)
        {
            Address = listBox1.SelectedItem.ToString().Substring(0, 8);
            this.Close();
        }

        private void textBox1_Changed(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(ListBoxItems.Where(x => x.Contains(textBox1.Text)).ToArray());
            }
            else
                listBox1.Items.AddRange(ListBoxItems.ToArray());
        }
    }
}
