using System;
using System.Windows.Forms;

namespace PollingService
{
    public partial class PollingService : Form
    {
        public PollingService()
        {
            InitializeComponent();
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            Logger.setLevel(Levels.ALL);

            int threadCount = 4;
            if (int.TryParse(txtThreadCount.Text.Trim(), out threadCount))
            {
                Poll poll = new Poll(threadCount);
            }
            else
            {
                MessageBox.Show("Please provide a valid integet thread count value");
            }
        }
    }

}
