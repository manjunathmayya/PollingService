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
        private int Id;
        private string source;
        private string destination;

        public CopyThread(int id)
        {
            Id = id;
        }

       private void doCopy()
        {
            try
            {
                File.Copy(source, destination);
                Logger.Log("Thread : "+Id + " File :" + source + " Copied to : " + destination, Levels.SUCCES);
            }
            catch (Exception e)
            {
                Thread.Sleep(50);
                Logger.Log("ID :" + Id+ " : Retry copy: " + destination, Levels.ERROR);
                doCopy();
            }
            finally
            {
                freeMe();
            }
        }

       public int getId()
       {
           return Id;
       }

       public bool isFree()
        {
            return free;
        }

        public void freeMe()
        {
            free = true;
        }
        public void copy(string source, string destination)
        {
            free = false;

            this.source = source;
            this.destination = destination;

            Task task = new Task(doCopy);
            task.Start();
            //task.Wait();
        }
    }
}
