using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace eBarDatabase
{
    public sealed class DatabaseLogger : IDatabaseLogger
    {
        private string _filePath = null;
        //private static readonly object SyncLock = new object();
        private static readonly object Lock = new object();
        //private static volatile Logger _instance;

        private readonly ConcurrentQueue<string> _queueMessages = new ConcurrentQueue<string>();
        private bool _isRunning = false;

        public DatabaseLogger()
        {
            GetLogPath();
        }
        
        private string GetLogPath()
        {
            var appDomain = AppDomain.CurrentDomain;
            var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;

            var logDirectory = Path.Combine(basePath, "Logs");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            _filePath = Path.Combine(logDirectory, "Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath);
            }
            return _filePath;
        }

        public void Start()
        {
            lock (Lock)
            {
                if (!_isRunning)
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
            _isRunning = false;
        }

        private void WriteLogMessages_DoWork(object sender, DoWorkEventArgs e)
        {
            _isRunning = true;
            while (!_queueMessages.IsEmpty)
            {
                string message = string.Empty;
                _queueMessages.TryDequeue(out message);
                if (!string.IsNullOrEmpty(message) && !IsFileLocked())
                {
                    try
                    {
                        WriteToFile(message);
                    }
                    catch (IOException)
                    {
                        _queueMessages.Enqueue(message);
                        Thread.Sleep(50);
                    }
                }
            }
        }

        private void WriteToFile(string entry)
        {
            using (var streamWriter = new StreamWriter(_filePath, true))
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
                _queueMessages.Enqueue(log);
                if (!_isRunning && !IsFileLocked())
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
                stream = new FileStream(_filePath, FileMode.Open, FileAccess.ReadWrite);
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