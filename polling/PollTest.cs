using System.IO;
using System.Threading;
using NUnit.Framework;
using PollingService;


namespace PollingServiceTest
{

    public class CopyThreadPoolMock : ICopyThreadPool
    {
        public void copy(string source, string destination)
        {
            
        }
    }


    [TestFixture]
    class PollTest
    {
        Poll poll;

        [SetUp]
        public void setup()
        {
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
        }      
    }
}
