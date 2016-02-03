using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CodeDesigner
{
    public partial class FormHistory : Form
    {
        public string Address { get; set; } = string.Empty;
        public List<string> ListBoxItems { get; set; }
        public string Path { get; set; } = @"Dump.lbl";

        public FormHistory()
        {
            InitializeComponent();
            textBox1.TextChanged += new EventHandler(textBox1_Changed);
            listBox1.MouseDoubleClick += new MouseEventHandler(SelectedItem);
            this.Text = "History";
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
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
            {
                listBox1.Items.AddRange(ListBoxItems.ToArray());
            }
        }
    }
}
