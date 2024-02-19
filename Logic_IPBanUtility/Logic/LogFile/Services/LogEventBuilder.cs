using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Logic.LogFile.Models;

namespace Logic_IPBanUtility;

public class LogEventBuilder
{
     private const string DATEFORMAT = "yyyy-MM-dd HH:mm:ss.ffff";
     public LogMessageParser logMessageParser = new();
     public LogEventStatistics Statistics = new();

     public List<LogEvent> GetLogEvents(List<string> logs, int previousId = 1)
     {
          var logEvents = new List<LogEvent>();
          foreach (var log in logs)
          {
               var logEvent = CreateLogEvent(log, previousId);
               if (logEvent is null)
                    continue;
               logEvents.Add(logEvent);
               Statistics.AddOneEvent(logEvent.Type);
               ++previousId;
          }
          return logEvents;
     }

     private LogEvent? CreateLogEvent(string log, int id)
     {
          var logParts = log.Split('|'); //Стандартна розмітка лога дільть його на 4 частини.
          if (logParts.Length != 4 ) return null;

          (var logDate, var logMessage) = LogSplit(logParts);

          var dto = logMessageParser.Parse(logMessage);
          if (dto is null) return null;

          var date = DateParse(logDate);

          var logEvent = new LogEvent(id, date, dto.Message, dto.Type);
          return logEvent;
     }

     private (string, string) LogSplit(string[] logParts)
     {
          var logDate = logParts[0];
          var logMessage = logParts[3];
          return (logDate, logMessage);
     }

     private DateTime DateParse(string logDate)
     {
          var result = DateTime.ParseExact(logDate, DATEFORMAT, null);
          return result;
     }
}
