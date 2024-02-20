namespace Logic_IPBanUtility;
public class IPBan
{
     private const string _NAME_LogFile = "logFile."; //Використовується для пошуку всіх файлів
     private const string _NAME_IPBan = "ipban.config";
     public string Folder { get; set; }
     public string Context { get; set; }
     public string Logfile { get; set; }

     public IPBan(string folder, string context, string logfile)
     {
          Folder = folder;
          Context = context;
          Logfile = logfile;
     }

     public static IPBan Create(string iPBanFolderPath)
     {
          var context = Path.Combine(iPBanFolderPath, _NAME_IPBan);
          var logfile = Path.Combine(iPBanFolderPath, _NAME_LogFile + "txt");

          var iPBan = new IPBan(iPBanFolderPath, context, logfile);
          iPBan.CheckExist();
          return iPBan;
     }

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

     public string[] GetAllLogFilePaths()
     {
          var allFiles = Directory.GetFiles(Folder);
          var logFiles = new List<string>();

          foreach (var file in allFiles)
               if (file.Contains(_NAME_LogFile))
                    logFiles.Add(file);

          return logFiles.ToArray();
     }
}