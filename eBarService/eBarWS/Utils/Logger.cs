using eBarWS.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading;

namespace eBarWS.Utils
{
    public sealed class Logger : ILogger
    {
        private string filePath = null;
        //private static readonly object SyncLock = new object();
        private static readonly object Lock = new object();
        //private static volatile Logger _instance;

        private ConcurrentQueue<string> queueMessages = new ConcurrentQueue<string>();
        private bool isRunning = false;

        public Logger()
        {
            GetLogPath();
        }
        //public static Logger Instance
        //{
        //    get
        //    {
        //        if (_instance?.path == null || _instance.path.Equals(""))
        //        {
        //            lock (SyncLock)
        //            {
        //                if (_instance?.path == null || _instance.path.Equals(""))
        //                {
        //                    _instance = new Logger
        //                    {
        //                        path = GetLogPath()
        //                    };

        //                    if (_instance.path == null || _instance.path.Equals(""))
        //                        _instance = null;
        //                }
        //            }
        //        }
        //        return _instance;
        //    }
        //}

        private string GetLogPath()
        {
            var logDirectory = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "Logs");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            filePath = Path.Combine(logDirectory, "Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            return filePath;
        }

        public void Start()
        {
            lock (Lock)
            {
                if (!isRunning)
                {
                    BackgroundWorker bgWorker = new BackgroundWorker();
                    bgWorker.DoWork += WriteLogMessages_DoWork;
                    bgWorker.RunWorkerCompleted += WriteLogMessages_Completed;
                    bgWorker.RunWorkerAsync();
                }
            }
        }

        private void WriteLogMessages_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            isRunning = false;
        }

        private void WriteLogMessages_DoWork(object sender, DoWorkEventArgs e)
        {
            isRunning = true;
            while (!queueMessages.IsEmpty)
            {
                string message = string.Empty;
                queueMessages.TryDequeue(out message);
                if (!string.IsNullOrEmpty(message) && !IsFileLocked())
                {
                    try
                    {
                        WriteToFile(message);
                    }
                    catch (IOException)
                    {
                        queueMessages.Enqueue(message);
                        Thread.Sleep(50);
                    }
                }
            }
        }

        private void WriteToFile(string entry)
        {
            using (var streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(entry);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        public void Log(string type, string logPhrase)
        {
            lock (Lock)
            {
                string log = DateTime.Now.ToString("HH.mm.ss.FFF") + " [" + type + "] : " + logPhrase;
                queueMessages.Enqueue(log);
                if (!isRunning && !IsFileLocked())
                {
                    Start();
                }
            }
        }

        private bool IsFileLocked()
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            }
            catch (Exception)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
            return false;
        }
    }
}