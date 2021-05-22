using System.IO;
using System.Threading;
using NUnit.Framework;
using PollingService;


namespace PollingServiceTestNunit
{
    [TestFixture]
    class CopyThreadTest
    {
        private CopyThread copyThread;
        [SetUp]
        public void setup()
        {
            copyThread = new CopyThread(1);
        }

        [Test]
        public void thread_has_valid_id()
        {
            Assert.AreEqual(1, copyThread.getId());
        }

        [Test]
        public void thread_is_free_at_start()
        {
            Assert.AreEqual(true, copyThread.isFree());
        }

        [Test]
        public void thread_should_copy_file()
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
