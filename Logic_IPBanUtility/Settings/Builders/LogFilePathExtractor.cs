namespace Logic_IPBanUtility.Setting.Builders;

public class LogFilePathExtractor
{
     public readonly string ToDatLogFilePath;

     private readonly string _logsFolder;
     private const string _LOGFILENAME = "logfile.";

     public LogFilePathExtractor(string logsFolder)
     {
          _logsFolder = logsFolder;
          ToDatLogFilePath = Path.Combine(logsFolder, _LOGFILENAME + "txt");
     }
     public Dictionary<DateTime, string> GetDaysWithLogFilePath()
     {
          var allLogFilePaths = GetAllLogFilePaths(_logsFolder);
          allLogFilePaths.Reverse();

          var logFilePathByDay = new Dictionary<DateTime, string>();
          int daysOffset = 0;
          foreach (var path in allLogFilePaths)
          {
               var date = DateTime.Today.AddDays(daysOffset--);
               logFilePathByDay.Add(date, path);
          }
          return logFilePathByDay;
     }

     private List<string> GetAllLogFilePaths(string logsFolder)
     {
          var allFiles = Directory.GetFiles(logsFolder);
          var logFiles = new List<string>();

          foreach (var file in allFiles)
               if (file.Contains(_LOGFILENAME))
                    logFiles.Add(file);

          return logFiles;
     }
}
