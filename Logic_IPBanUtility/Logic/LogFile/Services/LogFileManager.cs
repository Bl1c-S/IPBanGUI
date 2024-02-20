using Logic_IPBanUtility.Logic.LogFile.Models;
using Logic_IPBanUtility.Services;

namespace Logic_IPBanUtility.Logic.LogFile.Services;

public class LogFileManager
{
    public Action? LogEventsChanged;
    public List<LogEvent> AllLogEvents => _logEvents;
    private List<LogEvent> _logEvents = new();

    public LogEventStatistics Statistics => _logEventBuilder.Statistics;

    private readonly StreamFileManager _fileManager = new();
    private readonly LogEventBuilder _logEventBuilder = new();

    private readonly string _logFilePath;
    private int _lastLogEventId;

    public LogFileManager(string logFilePath)
    {
        _logFilePath = logFilePath;
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
     public List<LogEvent> ReadLogEvents(string logFilePath)
     {
          var logs = _fileManager.StreamReadAllNewLines(logFilePath);
          var logEvents = _logEventBuilder.GetLogEvents(logs);
          return logEvents;
     }
}
