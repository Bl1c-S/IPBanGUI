namespace Logic_IPBanUtility.Settings.Models;

public class Config
{
     public string ConfigFolder {
          get { return field ?? throw new NullReferenceException(ToString()); }
          set;
     }
     public string Settings {
          get { return field ?? throw new NullReferenceException(ToString()); }
          set;
     }
     public string KeyIdenti {
          get { return field ?? throw new NullReferenceException(ToString()); }
          set;
     }

     public Config()
     {
          //Json Constructor
     }
     public Config(string configFolder, string settings, string keyIdenti)
     {
          ConfigFolder = configFolder;
          Settings = settings;
          KeyIdenti = keyIdenti;
     }

     public static Config Create()
     {
          var programFolder = AppDomain.CurrentDomain.BaseDirectory;
          var configFolder = Path.Combine(programFolder, "Config");
          var settings = Path.Combine(configFolder, "setting.json");
          var keyIdenti = Path.Combine(configFolder, "keys.json");

          var config = new Config(configFolder, settings, keyIdenti);
          return config;
     }
     public bool TryCheckExist()
     {
          if (!Directory.Exists(ConfigFolder))
               return false;
          if (!File.Exists(Settings))
               return false;
          return File.Exists(KeyIdenti);
     }
     public void CheckExist()
     {
          if (!Directory.Exists(ConfigFolder))
               throw new DirectoryNotFoundException(Properties.Resources.DirectoryNotFoundException + ConfigFolder);
          if (!File.Exists(Settings))
               throw new FileNotFoundException(Properties.Resources.FileNotFoundException + Settings);
          if (!File.Exists(KeyIdenti))
               throw new FileNotFoundException(Properties.Resources.FileNotFoundException + KeyIdenti);
     }
}