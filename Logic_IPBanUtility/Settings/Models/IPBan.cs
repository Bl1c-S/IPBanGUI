using Logic_IPBanUtility.Setting.Builders;

namespace Logic_IPBanUtility;
public class IPBan
{
     private readonly LogFilePathExtractor _logsExtractor;
     private const string _NAME_IPBan = "ipban.config";
     private const string _NAME_SQLite_DB = "ipban.sqlite";
     public string Folder { get; set; }
     public string Context { get; set; }
     public string Logfile { get; set; }
     public string Sqlite_db { get; set; }
     public string ServiceName = "IPBAN";
     public IPBan(string folder, string logfile)
     {
          Folder = folder;
          Context = Path.Combine(folder, _NAME_IPBan);
          Sqlite_db = Path.Combine(folder, _NAME_SQLite_DB);
          Logfile = logfile;
          _logsExtractor = new(Folder);
     }

     public static IPBan Create(string iPBanFolderPath)
     {
          var logExtractor = new LogFilePathExtractor(iPBanFolderPath);
          var logfile = logExtractor.ToDayLogFilePath;

          var iPBan = new IPBan(iPBanFolderPath, logfile);
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
          if (!File.Exists(Sqlite_db))
               throw new FileNotFoundException(m + Sqlite_db);
          return true;
     }
}