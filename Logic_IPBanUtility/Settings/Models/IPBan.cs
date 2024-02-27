using Logic_IPBanUtility.Setting.Builders;

namespace Logic_IPBanUtility;
public class IPBan
{
     private readonly LogFilePathExtractor _logsExtractor;
     private const string _NAME_IPBan = "ipban.config";
     public string Folder { get; set; }
     public string Context { get; set; }
     public string Logfile { get; set; }

     public IPBan(string folder, string context, string logfile)
     {
          Folder = folder;
          Context = context;
          Logfile = logfile;
          _logsExtractor = new(Folder);
     }

     public static IPBan Create(string iPBanFolderPath)
     {
          var context = Path.Combine(iPBanFolderPath, _NAME_IPBan);
          var logExtractor = new LogFilePathExtractor(iPBanFolderPath);
          var logfile = logExtractor.ToDatLogFilePath;

          var iPBan = new IPBan(iPBanFolderPath, context, logfile);
          iPBan.CheckExist();
          return iPBan;
     }

     public Dictionary<DateTime, string> GetDaysWithLogFilePath() => _logsExtractor.GetDaysWithLogFilePath();

     public bool CheckExist()
     {
          var m = "Не знайдено необхідний файл:";
          if (!Directory.Exists(Folder))
               throw new DirectoryNotFoundException("Не знайдено вказану теку:" + Folder);
          if (!File.Exists(Context))
               throw new FileNotFoundException(m + Context);
          if (!File.Exists(Logfile))
               throw new FileNotFoundException(m + Logfile);
          return true;
     }
}