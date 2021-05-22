using System.IO;
using System.Threading;
using NUnit.Framework;
using PollingService;


namespace PollingServiceTest
{
    [TestFixture]
    class CopyThreadPoolTest
    {

        [Test]
        public void thread_pool_creates_required_number_of_threads()
        {
            CopyThreadPool copyThreadPool = new CopyThreadPool(1);
            Assert.AreEqual(1, copyThreadPool.GetThreadCount());

            CopyThreadPool copyThreadPoolWithTenThreads = new CopyThreadPool(10);
            Assert.AreEqual(10, copyThreadPoolWithTenThreads.GetThreadCount());
        }


        [Test]
        public void thread_pool_should_copy_file()
        {
            string source = @"E:\FO_MD1NWGHC\Desktop\ACSOS62 - VA11\acsos.ini";
            string destination = @"C:\Destination\acsos.ini";

            if (File.Exists(destination))
            {
                File.Delete(destination);
            }

            CopyThreadPool copyThreadPool = new CopyThreadPool(1);
            copyThreadPool.copy(source, destination);

            while (!File.Exists(destination)) //Wait till file is copied
            {
                Thread.Sleep(5);
            }

            Assert.AreEqual(true, File.Exists(destination));
        }
    }
}
