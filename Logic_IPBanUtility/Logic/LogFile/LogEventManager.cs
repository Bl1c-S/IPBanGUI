using Logic_IPBanUtility.Logic.LogFile.Services;
using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventManager
{
     public List<LogEvent> LogEvents => _logEvents;

     private List<LogEvent> _logEvents;

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
          return newLogEvents;
     }
     public List<LogEvent> ReadAllLogEvents()
     {
          var logs = _fileManager.ReadAllLines(_logFilePath);
          var logEvents = _logEventBuilder.GetLogEvents(logs);
          UpdateContext(logs.Count, logEvents.Count);
          _logEvents = logEvents;
          return logEvents;
     }

     private void UpdateContext(int logFileStringCount, int logEventId)
     {
          _lastLogFileStringCount += logFileStringCount;
          _lastLogEventId += logEventId;
     }
}
