using System;
using System.IO;
using System.Threading;

namespace PollingService
{

    public enum Levels
    {
        INFO,
        DEBUG,
        ERROR,
        SUCCES,
        ALL
    }

    public class Logger
    {
        private static Levels selectedLevel;
        private static Mutex mutex;
        public static void StartLogging(Levels level)
        {
            mutex = new Mutex();
            selectedLevel = level;
        }

        public static void Log(string text, Levels level)
        {
            if (selectedLevel == Levels.ALL || selectedLevel == level)
            {
                mutex.WaitOne();
                File.AppendAllText(@"c:\Destination\log_file.txt", text + Environment.NewLine);
                mutex.ReleaseMutex();
            }
        }
    }
}
