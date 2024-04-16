using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventManager
{
     public Action? DaysWithLogChanged;
     public Action? TodayChanged;
     public DateTime? Today = null;
     public List<DateTime> CurrentDayWithLogs => _logFileManagers.Keys.ToList();

     private Dictionary<DateTime, LogFileManager> _logFileManagers = new();
     private Func<Dictionary<DateTime, string>> _getDaysWithLogFilePath;

     public LogEventManager(Settings settings)
     {
          _getDaysWithLogFilePath = settings.IPBan.GetDaysWithLogFilePath;
          CheckDaysWithLogsChanged();
     }
     public List<LogEvent> GetLogEvents(DateTime date, bool first = true)
     {
          if (CheckExistLogFileManager(date))
               return GetLogEventsByDay(date, first);
          return new();
     }

     private List<LogEvent> GetLogEventsByDay(DateTime date, bool first = true)
     {
          var manager = _logFileManagers[date];
          var logs = manager.ReadNewLogEvents(first);
          return logs;
     }

     public void CheckDaysWithLogsChanged()
     {
          LogFileManagersValidator validator = new(_getDaysWithLogFilePath(), _logFileManagers, Today);

          if (validator.CheckTodayChanged())
          {
               _logFileManagers = validator.LogFileManagers;
               TodayChanged?.Invoke();
          }
          else if (validator.ChackManagersChanged())
          {
               _logFileManagers = validator.LogFileManagers;
               DaysWithLogChanged?.Invoke();
          }
     }

     private bool CheckExistLogFileManager(DateTime date)
     {
          if (!_logFileManagers.Keys.Contains(date))
               return false;
          return true;
     }

     private class LogFileManagersValidator
     {
          public bool ChangesDetected;
          private DateTime _today;

          public Dictionary<DateTime, LogFileManager> LogFileManagers;
          private readonly Dictionary<DateTime, string> _newDaysWithLogs;

          public LogFileManagersValidator(Dictionary<DateTime, string> newDaysWithLogs, Dictionary<DateTime, LogFileManager> logFileManagers, DateTime? today)
          {
               _newDaysWithLogs = newDaysWithLogs;
               LogFileManagers = logFileManagers;
               ChangesDetected = false;
               _today = today ?? DateTime.Today;
          }
          public bool ChackManagersChanged()
          {
               var dayAdded = AddedNewDays();
               var dayRemoved = RemoveUnusedManagers();
               return dayAdded || dayRemoved;
          }
          private bool AddedNewDays()
          {
               bool changesDetected = false;
               foreach (var day in _newDaysWithLogs)
               {
                    var date = day.Key;
                    var path = day.Value;
                    if (!LogFileManagers.ContainsKey(date))
                         changesDetected = Add(date, path);
                    else if (LogFileManagers[date].LogFilePath != path)
                         changesDetected = Reset(date, path);
               }
               return changesDetected;
          }
          private bool RemoveUnusedManagers()
          {
               bool changesDetected = false;
               foreach (var manager in LogFileManagers.Keys)
                    if (!_newDaysWithLogs.ContainsKey(manager))
                    {
                         Remove(manager);
                         changesDetected = true;
                    }
               return changesDetected;
          }
          public bool CheckTodayChanged()
          {
               if (!LogFileManagers.ContainsKey(_today))
                    return ResetLogFileManagers();
               return false;
          }
          private bool ResetLogFileManagers()
          {
               LogFileManagers = _newDaysWithLogs.ToDictionary(kv => kv.Key, kv => new LogFileManager(kv.Value));
               return true;
          }
          private bool Reset(DateTime date, string path)
          {
               Remove(date);
               Add(date, path);
               return true;
          }
          private bool Add(DateTime date, string path)
          {
               LogFileManagers.Add(date, new(path));
               return true;
          }
          private bool Remove(DateTime date)
          {
               LogFileManagers.Remove(date);
               return true;
          }
     }
}