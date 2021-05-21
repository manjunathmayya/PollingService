using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PollingService
{
    public partial class PollingService : Form
    {
        private Print print;
        public PollingService()
        {
            InitializeComponent();

            Poll poll = new Poll(this);
            //print = new Print(this, 4);
        }

        delegate void SetTextCallback(string text);
        public void Log(string text)
        {
            if (this.txtOutput.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Log);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtOutput.AppendText("\n" + text);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            print.start();
        }
    }

}
