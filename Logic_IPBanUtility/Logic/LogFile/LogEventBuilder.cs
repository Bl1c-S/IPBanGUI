using Logic_IPBanUtility.Logic.LogFile;

namespace Logic_IPBanUtility;

public class LogEventBuilder
{
     private const string DATEFORMAT = "yyyy-MM-dd HH:mm:ss.ffff";
     LogMessageParser logMessageParser = new();

     public List<LogEvent> GetLogEvents(List<string> logs, int previousId = 0)
     {
          var logEvents = new List<LogEvent>();
          foreach (var log in logs)
          {
               ++previousId;
               var logEvent = CreateLogEvent(log, previousId);
               if (logEvent != null)
                    logEvents.Add(logEvent);
          }
          return logEvents;
     }

     private LogEvent? CreateLogEvent(string log, int id)
     {
          (var logDate, var logMessage) = LogSplit(log);

          var dto = logMessageParser.Parse(logMessage);
          if (dto is null) return null;

          var date = DateParse(logDate);

          var logEvent = new LogEvent(id, date, dto.Message, dto.Type);
          return logEvent;
     }

     private (string, string) LogSplit(string log)
     {
          var logParts = log.Split('|');
          var logDate = logParts[0];
          var logMessage = logParts[3];
          return (logDate, logMessage);
     }

     private DateTime DateParse(string logDate)
     {
          var result = DateTime.ParseExact(logDate, DATEFORMAT, null);
          result.GetDateTimeFormats('g');
          return result;
     }
}
