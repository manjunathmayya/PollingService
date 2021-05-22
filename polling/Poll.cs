using System;
using System.Collections.Generic;
using System.IO;

namespace PollingService
{
    public class Poll
    {
        private string source ;
        private string destination ;
        FileSystemWatcher watcher;
        List<string> files_to_be_copied;
        private ICopyThreadPool copyThreadPool;

        public void Start(ICopyThreadPool copyThreadPool,string source = @"C:\Root", string destination = @"C:\Destination")
        {
            if (watcher == null)
            {
                this.copyThreadPool = copyThreadPool;

                this.source = source;
                this.destination = destination;

                files_to_be_copied = new List<string>();
                StartWatch();
            }
            else
            {
                throw new Exception("Polling already started.");
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
                throw new Exception("Polling already stopped.");
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

        

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (files_to_be_copied.Contains(e.FullPath))
            {
                Logger.Log("Copy started for : " + e.FullPath, Levels.INFO);
                string destination_file = destination + "\\" + Path.GetFileName(e.FullPath);
                copyThreadPool.copy(e.FullPath, destination_file);
                files_to_be_copied.Remove(e.FullPath);
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            files_to_be_copied.Add(e.FullPath);
        }

        public int GetNumberOfFilesToBeCopied()
        {
            return files_to_be_copied.Count;
        }

    }
}
