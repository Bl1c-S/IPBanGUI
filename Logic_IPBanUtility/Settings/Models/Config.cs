namespace Logic_IPBanUtility;

public class Config
{
     public string ConfigFolder { get; set; }
     public string Settings { get; set; }
     public string KeyIdenti { get; set; }

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