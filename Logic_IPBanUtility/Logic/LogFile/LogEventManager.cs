using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventManager
{
     public List<DateTime> DateWithLogs => _logFileManagers.Keys.ToList();
     public Action? DaysWithLogChanged;

     private Dictionary<DateTime, LogFileManager> _logFileManagers = new();
     private Func<Dictionary<DateTime, string>> getDaysWithLogFilePath;

     public LogEventManager(Settings settings)
     {
          getDaysWithLogFilePath = settings.IPBan.GetDaysWithLogFilePath;
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
          UpdateDaysWithLogs();
          var logFileManager = _logFileManagers[date];
          var logs = logFileManager.ReadNewLogEvents(true);
          return logs;
     }
     public void UpdateDaysWithLogs()
     {
          var updateDateWithLogs = LoadDaysWithLogs();
          if (updateDateWithLogs.Count == _logFileManagers.Count) return;

          _logFileManagers = updateDateWithLogs;
          DaysWithLogChanged?.Invoke();
     }
     private Dictionary<DateTime, LogFileManager> LoadDaysWithLogs()
     {
          Dictionary<DateTime, LogFileManager> dateWithLogs = new();
          foreach (var day in getDaysWithLogFilePath())
               dateWithLogs.Add(day.Key, new(day.Value));
          return dateWithLogs;
     }
}