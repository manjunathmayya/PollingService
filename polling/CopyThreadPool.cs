using System.Collections.Generic;
using System.Threading;

namespace PollingService
{
    public interface ICopyThreadPool
    {
        void copy(string source, string destination);
    }


    public class CopyThreadPool: ICopyThreadPool
    {
        private List<CopyThread> CopyThreads;

        public CopyThreadPool(int threadCount)
        {
            CopyThreads = new List<CopyThread>();
            for (int threadId = 0; threadId < threadCount; threadId++)
            {
                CopyThreads.Add(new CopyThread(threadId));
            }
        }


        private CopyThread GetCopyThread()
        {
            while (true)
            {
                foreach (var copyThread in CopyThreads)
                {
                    if (copyThread.isFree())
                    {
                        return copyThread;
                    }
                }
                Thread.Sleep(20);
            }
        }

        public void copy(string source, string destination)
        {
            GetCopyThread().copy(source, destination);            
        }
        
        public int GetThreadCount()
        {
            return CopyThreads.Count;
        }
    }
}
