namespace Logic_IPBanUtility;
public class IPBan
{
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
          var context = Path.Combine(iPBanFolderPath, "ipban.config");
          var logfile = Path.Combine(iPBanFolderPath, "logfile.txt");

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
}