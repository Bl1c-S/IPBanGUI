using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Settings.Models;
using Newtonsoft.Json;

namespace Logic_IPBanUtility.Settings;

public class Settings
{
     public int Version;
     public Config Config { get; set; }
     public IPBan IpBan { get; set; }
     
     private readonly FileManager _fileManager = new();
     
     [JsonConstructor]
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