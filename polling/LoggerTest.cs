using System.IO;
using System.Threading;
using NUnit.Framework;
using PollingService;


namespace PollingServiceTest
{


    [TestFixture]
    class LoggerTest
    {
        private string log_file = @"C:\Destination\log_file.txt";
        private string debug_trace = "Adding debug trace";
        private string info_trace = "Adding info trace";
        private string error_trace = "Adding error trace";
        private string success_trace = "Adding success trace";

        [SetUp]
        public void DeleteLogFile()
        {
            if (File.Exists(log_file))
            {
                File.Delete(log_file);
            }
        }

        [Test]
        public void when_debug_enabled_logger_should_log_only_debug_logs()
        {
            Logger.StartLogging(Levels.DEBUG);
            AddLogs();

            Assert.AreEqual(true, TraceExistsInFile(debug_trace));
            Assert.AreEqual(false, TraceExistsInFile(info_trace));
        }

        private void AddLogs()
        {
            Logger.Log(debug_trace, Levels.DEBUG);
            Logger.Log(info_trace, Levels.INFO);
            Logger.Log(error_trace, Levels.ERROR);
            Logger.Log(success_trace, Levels.SUCCES);
        }

        [Test]
        public void when_info_enabled_logger_should_log_only_info_logs()
        {
            Logger.StartLogging(Levels.INFO);
            AddLogs();

            Assert.AreEqual(true, TraceExistsInFile(info_trace));
            Assert.AreEqual(false, TraceExistsInFile(debug_trace));
        }

        [Test]
        public void when_all_logs_enabled_logger_should_log_all_logs()
        {
            Logger.StartLogging(Levels.ALL);
            AddLogs();

            Assert.AreEqual(true, TraceExistsInFile(info_trace));
            Assert.AreEqual(true, TraceExistsInFile(debug_trace));
            Assert.AreEqual(true, TraceExistsInFile(error_trace));
            Assert.AreEqual(true, TraceExistsInFile(success_trace));
        }

        private bool TraceExistsInFile(string trace)
        {
            if (File.Exists(log_file))
            {
                if (File.ReadAllText(log_file).Contains(trace))
                    return true;
                else
                    return false;
            }
            
            return false;
        }

       
    }
}
