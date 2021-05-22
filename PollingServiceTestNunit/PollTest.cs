using System.IO;
using System.Threading;
using NUnit.Framework;
using PollingService;


namespace PollingServiceTestNunit
{

    public class CopyThreadPoolMock : ICopyThreadPool
    {
        public void copy(string source, string destination)
        {
            Thread.Sleep(2); //Adding delay to simulate that the copy action is taking some time.
        }
    }

    [TestFixture]
    class PollTest
    {
        Poll poll;

        [SetUp]
        public void setup()
        {
            Logger.StartLogging(Levels.ALL);
            poll = new Poll();
        }

        [Test]
        public void poll_created_can_start()
        {
            Assert.AreEqual(true, poll.CanStart());
        }

        [Test]
        public void poll_created_cant_stop_without_start()
        {
            Assert.AreEqual(false, poll.CanStop());
        }


        [Test]
        public void poll_once_started_cant_start_again_immediately()
        {
            poll.Start(new CopyThreadPoolMock());
            Assert.AreEqual(false, poll.CanStart());
        }

        [Test]
        public void poll_once_started_can_be_stopped()
        {
            poll.Start(new CopyThreadPoolMock());
            Assert.AreEqual(true, poll.CanStop());
            poll.Stop();
        }

        [Test]
        public void copy_triggered_on_new_file_in_source_path()
        {
            poll.Start(new CopyThreadPoolMock());

            string source = @"E:\FO_MD1NWGHC\Desktop\ACSOS62 - VA11\acsos.ini";
            string destination = @"C:\Root\acsos.ini";

            if (File.Exists(destination))
            {
                File.Delete(destination);
            }

            File.Copy(source, destination);
            Assert.AreEqual(1, poll.GetNumberOfFilesToBeCopied());

            while (poll.GetNumberOfFilesToBeCopied() != 0)
                Thread.Sleep(5);

            Assert.AreEqual(0, poll.GetNumberOfFilesToBeCopied());
        }
    }
}
