namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventFilter
{
     public List<LogEvent> RemoveLogEventsByType(List<LogEvent> logEvents, LogEventType type)
     {
          logEvents.RemoveAll(log => log.Type == type);
          return logEvents;
     }
     public List<LogEvent> FindEventsByType(List<LogEvent> logEvents, LogEventType type)
     {
          var filteredLogEvents = logEvents.Where(log => log.Type == type);
          return filteredLogEvents.ToList();
     }
}
