using Logic_IPBanUtility.Logic.LogFile.Models;
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
     private List<LogEvent> _logEvents = new();

     public LogEventStatistics Statistics => _logEventBuilder.Statistics;
     private readonly StreamFileManager _fileManager = new();
     private readonly LogEventBuilder _logEventBuilder = new();

     private readonly string _logFilePath;
     private int _lastLogEventId;

     public LogEventManager(Settings settings)
     {
          _logFilePath = settings.IPBan.Logfile;
          _logEvents = ReadNewLogEvents();
     }
     public List<LogEvent> ReadNewLogEvents()
     {
          var newLogs = _fileManager.StreamReadAllNewLines(_logFilePath);
          var newLogEvents = _logEventBuilder.GetLogEvents(newLogs, _lastLogEventId + 1);
          _lastLogEventId += newLogEvents.Count;
          _logEvents.AddRange(newLogEvents);
          LogEventsChanged?.Invoke();
          return newLogEvents;
     }
}
