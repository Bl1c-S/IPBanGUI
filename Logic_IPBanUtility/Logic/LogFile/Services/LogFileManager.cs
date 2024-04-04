using Logic_IPBanUtility.Services;

namespace Logic_IPBanUtility.Logic.LogFile;

public class LogFileManager
{
     public Action? LogEventsChanged;

     private readonly StreamFileManager _streamFileManager = new();
     private readonly LogEventBuilder _logEventBuilder = new();

     private readonly string _logFilePath;
     private int _lastLogEventId;

     public LogFileManager(string logFilePath)
     {
          _logFilePath = logFilePath;
     }
     public List<LogEvent> ReadNewLogEvents(bool readFirst = false)
     {
          var firstLogID = readFirst ? 0 : _lastLogEventId;
          var newLogs = _streamFileManager.StreamReadAllNewLines(_logFilePath, readFirst);
          var newLogEvents = _logEventBuilder.GetLogEvents(newLogs, firstLogID + 1);

          _lastLogEventId += newLogEvents.Count;
          LogEventsChanged?.Invoke();
          return newLogEvents;
     }

     public void DeleteLogFile() => File.Delete(_logFilePath);
}
