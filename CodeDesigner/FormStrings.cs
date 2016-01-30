using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeDesigner
{
    public partial class FormStrings : Form
    {
        private BackgroundWorker worker { get; set; } = new BackgroundWorker();
        public byte[] MemoryDump { get; set; }
        public List<string> Items { get; set; }  = new List<string>();
        public int Address { get; set; } 

        public FormStrings()
        {
            InitializeComponent();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_progressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_CompletedWork);
            tbSearch.TextChanged += new EventHandler(tbSearch_Changed);
            lbStringDumper.MouseDoubleClick += new MouseEventHandler(SelectedItem);
        }

        private void SelectedItem(object sender, EventArgs e)
        {
            Address = Convert.ToInt32(lbStringDumper.SelectedItem.ToString().Substring(0, 8), 16);
            worker.Dispose();
            Items.Clear();
            this.Close();
        }

        private void tbSearch_Changed(object sender, EventArgs e)
        {
            if (tbSearch.Text != string.Empty)
            {
                lbStringDumper.Items.Clear();
                lbStringDumper.Items.AddRange(Items.Where(x => x.Contains(tbSearch.Text)).ToArray());
            }
            else
                lbStringDumper.Items.AddRange(Items.ToArray());
        }

        private void FormStrings_Load(object sender, EventArgs e)
        {
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(MemoryDump);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int successCount = 3;
            int nopCount = 0;
            int matchCount = 0;
            int addressInt = 0;
            int numberCount = 0;
            int letterCount = 0;
            List<byte> buffer = new List<byte>();
            
            var FileData = (byte[])e.Argument;
            for (int i = 0; i < FileData.Length; i = i + 4)
            {
                var reset = false;

                for (int x = 0; x < 4; x++)
                {
                    if (x == 0 && matchCount == 0)
                    {
                        addressInt = i;
                    }
                    if (FileData[i + x] == 0 && matchCount == 0)
                    {
                        nopCount = 1;
                    }
                    else if (FileData[i + x] != 0 && FileData[i + x] > 31 && FileData[i + x] < 127)
                    {
                        matchCount++;
                        var character = FileData[i + x];
                        if (character > 47 && character < 58)
                        {
                            numberCount++;
                        }
                        if (character > 64 && character < 91)
                        {
                            letterCount++;
                        }
                        if (character > 96 && character < 123)
                        {
                            letterCount++;
                        }
                        buffer.Add(FileData[i + x]);
                    }
                    else if (FileData[i + x] == 0 && matchCount > successCount && nopCount.Equals(1))
                    {
                        reset = true;
                        var item = Encoding.ASCII.GetString(buffer.ToArray());
                        if (letterCount > 2)
                            Items.Add(Convert.ToString(addressInt, 16).PadLeft(8, '0') + " " + item);
                        
                        worker.ReportProgress((int)(((float)i / (float)33554432) * 100));
                        buffer.Clear();
                    }
                    else
                    {
                        reset = true;
                    }

                    if (reset)
                    {
                        matchCount = 0;
                        nopCount = 0;
                        addressInt = 0;
                        numberCount = 0;
                        letterCount = 0;
                        buffer.Clear();
                    }

                }
            }
            Items = Items.OrderBy(x => x).ToList();
        }

        private void worker_progressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.ProgressBar.Value = e.ProgressPercentage;
            toolStripProgressBar1.ProgressBar.Maximum = 100;
            toolStripProgressBar1.ProgressBar.Minimum = 0;
            tssLProgress.Text = string.Format("%{0}", e.ProgressPercentage);  
        }

        private void worker_CompletedWork(object sender, RunWorkerCompletedEventArgs e)
        {
            lbStringDumper.Items.AddRange(Items.ToArray());
            toolStripProgressBar1.ProgressBar.Value = 100;
            tssLProgress.Text = "Completed";
            toolStripProgressBar1.Visible = false;
            tssLProgress.Visible = false;
        }

    }
}
