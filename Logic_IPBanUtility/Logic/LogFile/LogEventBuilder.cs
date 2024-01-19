using Logic_IPBanUtility.Logic.LogFile;

namespace Logic_IPBanUtility;

public class LogEventBuilder
{
     private const string DATEFORMAT = "yyyy-MM-dd HH:mm:ss.ffff";
     public LogEvent GetLogEvent(string log, int previousId)
     {
          var logParts = log.Split('|');

          var logDate = logParts[0];
          var logMessage = logParts[3];

          var date = DateParse(logDate);
          var message = MessageParse(logMessage);

          var id = previousId + 1;

          var logEvent = new LogEvent(id, date, message);
          return logEvent;
     }
     private DateTime DateParse(string logDate)
     {
          var dateTime = logDate.Trim();
          var result = DateTime.ParseExact(dateTime, DATEFORMAT, null);
          return result;
     }

     private string MessageParse(string logMessage)
     {
          return "t";
     }
}
