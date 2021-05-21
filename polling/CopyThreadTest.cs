using System.Threading;
using NUnit.Framework;
using PollingService;


namespace PollingServiceTest
{
    [TestFixture]
    class CopyThreadTest
    {
        private CopyThread copyThread;
        private Mutex mutex;
        [SetUp]
        public void setup()
        {
            copyThread = new CopyThread(1);
        }

        [Test]
        public void thread_should_have_valid_thread_id()
        {
            Assert.AreEqual(1, copyThread.getId());
        }

        [Test]
        public void thread_should_be_free_at_start()
        {
            Assert.AreEqual(true, copyThread.isFree());
        }

        public void thread_should_copy_files_correctly()
        {
            //Assert.AreEqual(true, copyThread.copy());
        }
    }
}
