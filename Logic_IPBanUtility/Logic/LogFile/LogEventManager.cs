using Logic_IPBanUtility.Logic.LogFile.Models;
using Logic_IPBanUtility.Logic.LogFile.Services;
using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventManager
{
     public Action? LogEventsChanged;
     public List<LogEvent> LogEvents
     {
          get => _logEvents;
          private set { _logEvents = value; }
     }
     private List<LogEvent> _logEvents;

     public LogEventStatistics Statistics => _logEventBuilder.Statistics;
     private readonly FileManager _fileManager;
     private readonly LogEventBuilder _logEventBuilder = new();

     private readonly string _logFilePath;
     private int _lastLogFileStringCount;
     private int _lastLogEventId;

     public LogEventManager(Settings settings, FileManager fileManager)
     {
          _logFilePath = settings.IPBan.Logfile;
          _fileManager = fileManager;
          _logEvents = ReadAllLogEvents();
     }
     public List<LogEvent> ReadNewLogEvents()
     {
          var newLogs = _fileManager.ReadAllLinesFromIndexToEnd(_logFilePath, _lastLogFileStringCount);
          var newLogEvents = _logEventBuilder.GetLogEvents(newLogs, _lastLogEventId + 1);
          UpdateContext(newLogs.Count, newLogEvents.Count);
          _logEvents.AddRange(newLogEvents);
          LogEventsChanged?.Invoke();
          return newLogEvents;
     }
     public List<LogEvent> ReadAllLogEvents()
     {
          var logs = _fileManager.ReadAllLines(_logFilePath);
          var logEvents = _logEventBuilder.GetLogEvents(logs);
          UpdateContext(logs.Count, logEvents.Count);
          _logEvents = logEvents;
          LogEventsChanged?.Invoke();
          return logEvents;
     }

     private void UpdateContext(int logFileStringCount, int logEventId)
     {
          _lastLogFileStringCount += logFileStringCount;
          _lastLogEventId += logEventId;
     }
}
