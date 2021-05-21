using System;
using System.Windows.Forms;

namespace PollingService
{
    public partial class PollingService : Form
    {
        private Poll poll;

        public PollingService()
        {
            InitializeComponent();
            poll = new Poll();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Logger.StartLogging(Levels.ALL);

            int threadCount = 4;
            if (int.TryParse(txtThreadCount.Text.Trim(), out threadCount))
            {
                if (poll.CanStart())
                    poll.Start(threadCount, txtSource.Text.Trim(), txtDestination.Text.Trim());
                else
                    MessageBox.Show("Polling already started. Stop and start to use new parameters");
            }
            else
            {
                MessageBox.Show("Please provide a valid integet thread count value");
            }
        }

        private void btnStopWatch_Click(object sender, EventArgs e)
        {
            if(poll.CanStop())
                poll.Stop();
            else
            {
                MessageBox.Show("Polling already stopped");
            }
        }       
    }

}
