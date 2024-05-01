using Logic_IPBanUtility.Models;

namespace Logic_IPBanUtility.Logic.ConfigFile;

public class KeyValueManager
{
     private readonly ConfigFileManager _cfgManager;

     public KeyValueManager(ConfigFileManager cfgManager)
     {
          _cfgManager = cfgManager;
     }

     public void AddIpToKey(KeyNames keyName, string ip)
     {
          var key = _cfgManager.GetKey(keyName);
          var keyChanged = false;
          if (string.IsNullOrWhiteSpace(key.Value))
               keyChanged = key.SetValue(ip);
          else if (!key.Value.Contains(ip))
               keyChanged = key.SetValue($"{key.Value}, {ip}");
          if (keyChanged)
               _cfgManager.WriteKey(key);
     }
     public void RemoveIpFromKey(KeyNames keyName, string ip)
     {
          var key = _cfgManager.GetKey(keyName);
          if (key.Value.Contains(ip))
          {
               var newKeyValue = RemoveIpFromList(key.Value, ip);
               key.SetValue(newKeyValue);
               _cfgManager.WriteKey(key);
          }
     }
     private string RemoveIpFromList(string Ips, string ipToRemove)
     {
          var old_Ips = Ips.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
          var updated_Ips = old_Ips.Where(ip => ip != ipToRemove);
          var updated_ips_stringFormat = string.Join(", ", updated_Ips);
          return updated_ips_stringFormat;
     }
}
