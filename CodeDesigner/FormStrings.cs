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
        public List<ListViewItem> itemQ { get; set; } = new List<ListViewItem>();
        public List<ListViewItem> Items { get; set; }  = new List<ListViewItem>();

        public FormStrings()
        {
            InitializeComponent();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_progressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_CompletedWork);
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
            List<byte> buffer = new List<byte>();
            
            var FileData = (byte[])e.Argument;
            for (int i = 0; i < FileData.Length; i = i + 4)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (x == 0 && matchCount == 0)
                        addressInt = i;
                    if (FileData[i + x] == 0 && matchCount == 0)
                        nopCount = 1;
                    else if (FileData[i + x] != 0 && FileData[i + x] > 31 && FileData[i + x] < 127)
                    {
                        matchCount++;
                        buffer.Add(FileData[i + x]);
                    }
                    else if (FileData[i + x] == 0 && matchCount > successCount && nopCount.Equals(1))
                    {
                        Items.Add(new ListViewItem() { Text = Encoding.ASCII.GetString(buffer.ToArray()) });
                        matchCount = 0;
                        nopCount = 0;
                        addressInt = 0;
                        worker.ReportProgress((int)(((float)i / (float)33554432) * 100)); 
                        buffer.Clear();
                    }
                    else
                    {
                        matchCount = 0;
                        nopCount = 0;
                        addressInt = 0;
                        buffer.Clear();
                    }
                }
            }
            Items = Items.OrderBy(x => x.Text).ToList();

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
            listViewStringDump.Items.AddRange(Items.ToArray());
            toolStripProgressBar1.ProgressBar.Value = 100;
            tssLProgress.Text = "Completed";
        }

    }
}
