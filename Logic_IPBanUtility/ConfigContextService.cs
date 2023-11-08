using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Logic_IPBanUtility;
/// <summary>
/// Use ConfigFileManager method CreateConfigFileService()
/// </summary>
public class ConfigContextService
{
     private ConfigFileManager contextFileManager;
     private List<string> Context => contextFileManager.Context;
     private List<Key> _keys => contextFileManager.Keys;

     public ConfigContextService(string directoryPath)
     {
          contextFileManager = new(directoryPath);
     }
     ////public Key GetKey(string keyName)
     ////{
          
     ////}
     ////public void SaveKey(Key key) 
     ////{
     ////     Context.Insert(key.Index, key.Context);
     ////}
}