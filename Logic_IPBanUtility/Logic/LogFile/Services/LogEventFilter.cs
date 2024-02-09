namespace Logic_IPBanUtility.Logic.LogFile.Services;

public class LogEventFilter
{
     public List<LogEvent> RemoveEventsByType(List<LogEvent> logEvents, LogEventType eventType)
     {
          logEvents.RemoveAll(logEvent => logEvent.Type == eventType);
          return logEvents;
     }
     public IEnumerable<LogEvent> FindEventsByType(List<LogEvent> logEvents, LogEventType eventType)
     {
          var filteredLogEvents = logEvents.Where(le => le.Type == eventType);
          return filteredLogEvents;
     }
}
