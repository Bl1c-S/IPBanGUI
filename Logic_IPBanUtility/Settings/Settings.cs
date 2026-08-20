using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Settings.Models;

namespace Logic_IPBanUtility.Settings;

public class Settings
{
     public int Version;
     public Config Config;
     public IPBan IPBan;
          
     private readonly FileManager _fileManager = new();
     
     public Settings() 
     { 
          //Json Constructor
     }
     public Settings(Config config, IPBan iPBan)
     {
          Version = 100;
          Config = config;
          IPBan = iPBan;
     }

     public void Save()
     {
          _fileManager.SaveJson(Config.Settings, this);
     }
}