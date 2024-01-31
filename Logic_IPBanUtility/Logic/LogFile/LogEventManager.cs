using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;
using NLog.Fluent;

namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventManager
{
     private readonly FileManager _fileManager;
     private readonly LogEventBuilder _logEventBuilder = new();
     private readonly string _logFilePath;
     private int _lastLogFileStringCount;
     private int _lastLogEventId;

     public LogEventManager(Settings settings, FileManager fileManager)
     {
          _logFilePath = settings.IPBan.Logfile;
          _fileManager = fileManager;
     }

     public List<LogEvent> ReadAllLogEvents()
     {
          var logs = _fileManager.ReadAllLines(_logFilePath);
          var logEvents = _logEventBuilder.GetLogEvents(logs);
          UpdateContext(logs.Count, logEvents.Count);
          return logEvents;
     }

     public List<LogEvent> ReadNewLogEvents()
     {
          var newLogs = _fileManager.ReadAllLinesFromIndexToEnd(_logFilePath, _lastLogFileStringCount);
          var logEvents = _logEventBuilder.GetLogEvents(newLogs, _lastLogEventId + 1);
          UpdateContext(newLogs.Count, logEvents.Count);
          return logEvents;
     }

     private void UpdateContext(int logFileStringCount, int logEventId)
     {
          _lastLogFileStringCount += logFileStringCount;
          _lastLogEventId += logEventId;
     }
}
