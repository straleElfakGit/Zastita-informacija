using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public enum LogType { Info, Warning, Error }

    public class Logger
    {
        private static Logger _instance;
        private static readonly object _lock = new object();
        private readonly object _fileLock = new object();
        private ILogFilePathStrategy _fileStrategy;

        public event Action<string> OnLogAdded;

        private Logger()
        {
            _fileStrategy = new DailyLogStrategy();
        }

        public static Logger Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new Logger();
                    return _instance;
                }
            }
        }

        public void SetStrategy(ILogFilePathStrategy strategy) => _fileStrategy = strategy;

        public void Log(string message, LogType type)
        {
            string logEntryForFile = _fileStrategy.FormatMessage(message, type);
            string logEntryForUI = $"[{DateTime.Now:HH:mm:ss}] {message}";

            WriteToFile(logEntryForFile);
            OnLogAdded?.Invoke(logEntryForUI);
        }

        private void WriteToFile(string entry)
        {
            lock (_fileLock)
            {
                try
                {
                    string path = _fileStrategy.GetLogPath();
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    File.AppendAllLines(path, new[] { entry });
                }
                catch { }
            }
        }
    }
}
