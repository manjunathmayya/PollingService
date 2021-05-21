using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PollingService
{
    class Print
    {
        private Mutex mtx;
        List<PrintThread> PrintThreads;
        PollingService pollingService;

        public Print(PollingService pollingService, int numberOfThreads = 4)
        {
            this.pollingService = pollingService;
            CreateCopyThreads(numberOfThreads);
        }


        private void CreateCopyThreads(int count)
        {
            PrintThreads = new List<PrintThread>();
            for (int threadId = 0; threadId < count; threadId++)
            {
                PrintThreads.Add(new PrintThread(threadId, pollingService));
            }
        }


        private PrintThread GetPrintThread()
        {
            while (true)
            {
                foreach (var printThread in PrintThreads)
                {
                    if (printThread.isFree())
                    {
                        Log("Print thread used: " + printThread.Id.ToString());
                        return printThread;
                    }
                }
                Thread.Sleep(20);
            }
        }

        public void start()
        {
            mtx = new Mutex();
                //PrintThread printThread = GetPrintThread();
                GetPrintThread().print(0,100000, mtx);
                GetPrintThread().print(100000, 200000, mtx);
                GetPrintThread().print(200000, 300000, mtx);
                GetPrintThread().print(300000, 400000, mtx);
        }

        private void Log(string text)
        {
            mtx.WaitOne();
            File.AppendAllText(@"c:\Destination\file.txt", text + Environment.NewLine);
            mtx.ReleaseMutex();
        }
    }


    public class PrintThread
    {
        private Mutex mutex;
        private bool free = true;
        public int Id;
        private int startCount;
        private int endCount;
        PollingService pollingService;

        public PrintThread(int id, PollingService pollingService)
        {
            Id = id;
            this.pollingService = pollingService;
        }

        private void doPrint()
        {
            for (int count = startCount; count < endCount; count++)
            {
                //pollingService.Log(count.ToString());
                Log(count.ToString());
            }
        }

        private void Log(string text)
        {
            mutex.WaitOne();
            File.AppendAllText(@"c:\Destination\file.txt", text + Environment.NewLine);
            mutex.ReleaseMutex();
        }

        public bool isFree()
        {
            return free;
        }

        public void freeMe()
        {
            free = true;
        }
        public void print(int startCount, int endCount, Mutex mtx)
        {
            mutex = mtx;
            free = false;

            this.startCount = startCount;
            this.endCount = endCount;

            Task task = new Task(doPrint);
            task.Start();
            //task.Wait();

            freeMe();
        }
    }
}
