﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PollingService
{
    public class Poll
    {
        string source = @"C:\Root";
        string destination = @"C:\Destination";
        FileSystemWatcher watcher;
        PollingService pollingService;
        List<CopyThread> CopyThreads;
        List<string> files_to_be_copied;
        Mutex mutex;

        public Poll(PollingService pollingService, int numberOfThreads = 4)
        {
            mutex = new Mutex();

            files_to_be_copied = new List<string>();

            CreateCopyThreads(numberOfThreads);

            this.pollingService = pollingService;

            StartWatch();
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
                        //pollingService.Log("Copy thread used: " + copyThread.Id);
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
                Logger.Log("Onchanged event received for : " + e.FullPath + "\nAttempting copy...", mutex);
                string destination_file = destination + "\\" + Path.GetFileName(e.FullPath);
                CopyThread copyThread = GetCopyThread();

                try
                {
                    Logger.Log("Copy thread used : " + copyThread.Id, mutex);
                    Logger.Log("Copy started for : " + e.FullPath, mutex);
                    copyThread.copy(e.FullPath, destination_file,mutex);
                    files_to_be_copied.Remove(e.FullPath);
                }
                catch (Exception exception)
                {
                    copyThread.freeMe();
                    Logger.Log("!! File in use. Will be retried for : " + e.FullPath, mutex);
                }

            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            files_to_be_copied.Add(e.FullPath);
        }

        //private void Log(string text)
        //{
        //    mutex.WaitOne();
        //    File.AppendAllText(@"c:\Destination\log_file.txt", text + Environment.NewLine);
        //    mutex.ReleaseMutex();
        //}
    }
}