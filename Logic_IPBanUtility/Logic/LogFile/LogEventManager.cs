using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.LogFile
{
    public class LogEventManager
     {
          private readonly FileManager _fileManager;
          private readonly LogEventBuilder _logEventBuilder= new();
          private readonly string _logFilePath;
          
          public LogEventManager(Settings settings, FileManager fileManager)
          {
               _logFilePath = settings.IPBan.Logfile;
               _fileManager = fileManager;

               
          }

          private List<LogEvent> ReadAll()
          {
               var logs = _fileManager.ReadFileToList(_logFilePath);
               return _logEventBuilder.GetLogEvents(logs);
          }
          //TODO Check changed
     }
}
