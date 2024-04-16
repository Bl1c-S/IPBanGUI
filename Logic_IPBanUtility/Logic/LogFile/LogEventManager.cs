using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventManager
{
     public Action? DaysWithLogChanged;
     public Action? TodayChanged;
     public DateTime? Today = null;
     public List<DateTime> CurrentDayWithLogs => _logFileManagers.Keys.ToList();

     private Dictionary<DateTime, LogFileManager> _logFileManagers;
     private Func<Dictionary<DateTime, string>> _getDaysWithLogFilePath;

     public LogEventManager(Settings settings)
     {
          _getDaysWithLogFilePath = settings.IPBan.GetDaysWithLogFilePath;
          _logFileManagers = LoadDaysWithLogs();
     }
     public List<LogEvent> GetNewLogEvents(DateTime date)
     {
          var logFileManager = _logFileManagers[date];
          var logs = logFileManager.ReadNewLogEvents();
          return logs;
     }
     public List<LogEvent> GetAllLogEvents(DateTime date)
     {
          GetDateWithLogs();
          var logFileManager = _logFileManagers[date];
          var logs = logFileManager.ReadNewLogEvents(true);
          return logs;
     }
     public List<DateTime> GetDateWithLogs()
     {
          var updateDateWithLogs = LoadDaysWithLogs();
          if (updateDateWithLogs.Count != _logFileManagers.Count)
          {
               _logFileManagers = updateDateWithLogs;
               DaysWithLogChanged?.Invoke();
          }
          return _logFileManagers.Keys.ToList();
     }
     private Dictionary<DateTime, LogFileManager> LoadDaysWithLogs()
     {
          Dictionary<DateTime, LogFileManager> dateWithLogs = new();
          foreach (var day in _getDaysWithLogFilePath())
               dateWithLogs.Add(day.Key, new(day.Value));
          return dateWithLogs;
     }
}