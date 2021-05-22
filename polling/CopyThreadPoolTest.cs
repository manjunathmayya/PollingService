using System.IO;
using System.Threading;
using NUnit.Framework;
using PollingService;


namespace PollingServiceTest
{
    [TestFixture]
    class CopyThreadPoolTest
    {

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(100)]
        public void thread_pool_creates_required_number_of_threads(int numberOfThreads)
        {
            CopyThreadPool copyThreadPool = new CopyThreadPool(numberOfThreads);
            Assert.AreEqual(numberOfThreads, copyThreadPool.GetThreadCount());
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
