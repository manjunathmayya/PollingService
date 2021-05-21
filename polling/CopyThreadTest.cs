using System.IO;
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

        [Test]
        public void thread_should_copy_file_correctly()
        {
            string source = @"E:\FO_MD1NWGHC\Desktop\ACSOS62 - VA11\acsos.ini";
            string destination = @"C:\Destination\acsos.ini";
            if (File.Exists(destination))
            {
                File.Delete(destination);
            }
            copyThread.copy(source, destination);
            while (!copyThread.isFree())
            {
                Thread.Sleep(5);
            }
            
            Assert.AreEqual(true, File.Exists(destination));
        }
    }
}
