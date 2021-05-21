using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PollingService
{
    public class Poll
    {
        private string source ;
        private string destination ;
        private int numberOfThreads;
        FileSystemWatcher watcher;
        List<CopyThread> CopyThreads;
        List<string> files_to_be_copied;

        public void Start(int numberOfThreads, string source = @"C:\Root", string destination = @"C:\Destination")
        {
            if (watcher == null)
            {
                this.numberOfThreads = numberOfThreads;
                this.source = source;
                this.destination = destination;

                files_to_be_copied = new List<string>();
                CreateCopyThreads(numberOfThreads);
                StartWatch();
            }
            else
            {
                MessageBox.Show("Polling already started. Stop and start to use new parameters");
            }
        }

        public bool CanStart()
        {
            return watcher == null;
        }

        public bool CanStop()
        {
            return watcher != null;
        }

        public void Stop()
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;

                watcher.Created -= OnCreated;
                watcher.Changed -= OnChanged;
                watcher.Dispose();
                watcher = null;
            }
            else
            {
                MessageBox.Show("Polling already stopped");
            }
        }


        private void StartWatch()
        {
            watcher = new FileSystemWatcher(source);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                   | NotifyFilters.CreationTime
                                   | NotifyFilters.DirectoryName
                                   | NotifyFilters.FileName
                                   | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.Security
                                   | NotifyFilters.Size;

            watcher.Created += OnCreated;
            watcher.Changed += OnChanged;

            //watcher.Filter = "*.txt";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }


        private void CreateCopyThreads(int count)
        {
            CopyThreads = new List<CopyThread>();
            for (int threadId = 0; threadId < count; threadId++)
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

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (files_to_be_copied.Contains(e.FullPath))
            {
                Logger.Log("Onchanged event received for : " + e.FullPath + "\nAttempting copy...", Levels.INFO);
                string destination_file = destination + "\\" + Path.GetFileName(e.FullPath);
                CopyThread copyThread = GetCopyThread();

                try
                {
                    Logger.Log("Copy thread used : " + copyThread.getId(), Levels.INFO);
                    Logger.Log("Copy started for : " + e.FullPath, Levels.INFO);
                    copyThread.copy(e.FullPath, destination_file);
                    files_to_be_copied.Remove(e.FullPath);
                }
                catch (Exception exception)
                {
                    copyThread.freeMe();
                    Logger.Log("!! File in use. Will be retried for : " + e.FullPath, Levels.ERROR);
                }

            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            files_to_be_copied.Add(e.FullPath);
        }

    }
}
