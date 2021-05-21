using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PollingService
{
    public class CopyThread
    {
        private bool free = true;
        public int Id;
        private string source;
        private string destination;
        private Mutex mutex;

        public CopyThread(int id)
        {
            Id = id;
        }

       private void doCopy()
        {
            try
            {
                File.Copy(source, destination);
                Logger.Log("File :" + source + " Copied to : " + destination, mutex, Levels.SUCCES);
            }
            catch (Exception e)
            {
                Thread.Sleep(50);
                Logger.Log("ID :" + Id+ " : Retry copy: " + destination, mutex, Levels.ERROR);
                doCopy();
            }
            finally
            {
                freeMe();
            }
        }

       public bool isFree()
        {
            return free;
        }

        public void freeMe()
        {
            free = true;
        }
        public void copy(string source, string destination, Mutex mutex)
        {

            this.mutex = mutex;
            free = false;

            this.source = source;
            this.destination = destination;

            Task task = new Task(doCopy);
            task.Start();
            //task.Wait();
        }
    }
}
