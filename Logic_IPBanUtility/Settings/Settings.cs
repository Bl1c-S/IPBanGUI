using Logic_IPBanUtility.Services;

namespace Logic_IPBanUtility.Setting;

public class Settings
{
     private FileManager _fileManager = new();
     public Config Config;
     public IPBan IPBan;
          
     public Settings(Config config, IPBan iPBan)
     {
          Config = config;
          IPBan = iPBan;
     }

     public void SetIPBan(string folder)
     {
          IPBan = IPBan.Create(folder);
          Save();
     }

     public void Save()
     {
          _fileManager.SaveJson(Config.Settings, this);
     }
}