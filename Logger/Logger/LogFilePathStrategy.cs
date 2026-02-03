using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILogFilePathStrategy
    {
        string GetLogPath();
        string FormatMessage(string message, LogType type);
    }

    public class DailyLogStrategy : ILogFilePathStrategy
    {
        public string GetLogPath() =>
            Path.Combine("Logs", "Daily", $"Log_{DateTime.Now:yyyy-MM-dd}.txt");
        public string FormatMessage(string message, LogType type) =>
            $"[{DateTime.Now:HH:mm:ss}] [{type}] {message}";
    }

    public class WeeklyLogStrategy : ILogFilePathStrategy
    {
        public string GetLogPath()
        {
            CultureInfo myCI = new CultureInfo("sr-RS"); 
            Calendar myCal = myCI.Calendar;

            int weekNumber = myCal.GetWeekOfYear(DateTime.Now, myCI.DateTimeFormat.CalendarWeekRule, myCI.DateTimeFormat.FirstDayOfWeek);
            string year = DateTime.Now.Year.ToString();

            return Path.Combine("Logs", "Weekly", $"Log_{year}_W{weekNumber}.txt");
        }

        public string FormatMessage(string message, LogType type) =>
            $"[{DateTime.Now:ddd HH:mm:ss}] [{type}] {message}";
    }
    public class MonthlyLogStrategy : ILogFilePathStrategy
    {
        public string GetLogPath() =>
            Path.Combine("Logs", "Monthly", $"Log_{DateTime.Now:yyyy_MM}.txt");
        public string FormatMessage(string message, LogType type) =>
            $"[{DateTime.Now:dd.MM. HH:mm:ss}] [{type}] {message}";
    }

    public class GeneralLogStrategy : ILogFilePathStrategy
    {
        public string GetLogPath() =>
            Path.Combine("Logs", "General", "Application.log");

        public string FormatMessage(string message, LogType type) =>
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{type}] {message}";
    }

}
