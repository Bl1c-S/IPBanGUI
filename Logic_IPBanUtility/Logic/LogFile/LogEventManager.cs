using Logic_IPBanUtility.Logic.LogFile.Services;

namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventManager
{
     public readonly LogFileManager LogFileManager;
     private readonly string[] _logFilePaths;

     public LogEventManager(IPBan iPBan)
     {
          LogFileManager = new LogFileManager(iPBan.Logfile);
           _logFilePaths = iPBan.GetAllLogFilePaths();
     }
     public List<LogEvent> GetLogEvents(string filePath) => LogFileManager.ReadLogEvents(filePath);
   
     public Dictionary<DateTime, string> GetLogFilePathsByDay()
     {
          Dictionary<DateTime, string> logFilePathByDay = new();
          foreach (string logFilePath in _logFilePaths)
          {
               var d = File.GetCreationTime(logFilePath);
               var dateCreated = new DateTime(d.Year, d.Month, d.Day);
               logFilePathByDay.Add(dateCreated, logFilePath);
          }
          return logFilePathByDay;
     }
}
