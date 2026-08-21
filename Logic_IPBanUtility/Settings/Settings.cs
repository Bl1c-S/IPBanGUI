using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Settings.Models;

namespace Logic_IPBanUtility.Settings;

public class Settings
{
     public int Version;
     public Config Config
     {
          get { return field ?? throw new NullReferenceException(ToString()); }
          set;
     }
     public IPBan IpBan {
          get { return field ?? throw new NullReferenceException(ToString()); }
          set;
     }
     private readonly FileManager _fileManager = new();
     
     public Settings() 
     { 
          //Json Constructor
     }
     public Settings(Config config, IPBan iPBan)
     {
          Version = 100;
          Config = config;
          IpBan = iPBan;
     }

     public void Save()
     {
          _fileManager.SaveJson(Config.Settings, this);
     }
}