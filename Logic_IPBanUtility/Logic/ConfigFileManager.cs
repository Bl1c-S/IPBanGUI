using Logic_IPBanUtility.Builders;
using Logic_IPBanUtility.Models;
using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;
using System.Collections.Generic;
using System.Text.Json;

namespace Logic_IPBanUtility;

public class ConfigFileManager
{
     private FileManager _fileManager { get; }
     public List<string> Context = new();
     public List<Key> Keys = new();
     public List<KeyIdenti> keyIdentis = new();

     private readonly string _contextPath;
     private readonly string _keyIdentiPath;

     public ConfigFileManager(Settings settings, FileManager fileManager)
     {
          _fileManager = fileManager;
          _contextPath = settings.IPBan!.Context;
          _keyIdentiPath = settings.Config.KeyIdenti;

          Context = _fileManager.GetStrings(_contextPath).ToList();
          keyIdentis = _fileManager.GetJson<List<KeyIdenti>>(_keyIdentiPath);

          Keys = new KeyBuilder(Context).GetKeys(keyIdentis);
     }
     public void WriteKeyIdentiChanged(Key key)
     {
          var index = keyIdentis.FindIndex(x => x.Name == key.Name);
          if (index == -1)
               throw new Exception($"Під час запису {key.Name}, в списку завантажених keyIdentis, не знайдено Відповідного елемента");
          key.KeyIdenti.IsHidden = !key.KeyIdenti.IsHidden;

          try
          {
               var jsonKeyIdenti = JsonSerializer.Serialize(keyIdentis);
               File.WriteAllText(_keyIdentiPath, jsonKeyIdenti);
          }
          catch (Exception ex) { throw new Exception($"Під час запису файлу за шляхом: {_keyIdentiPath} виникла помилка:  {ex.Message}"); }
     }

     public void WriteKey(Key key)
     {
          Context[key.Index] = key.Context;
          File.WriteAllLines(_contextPath, Context);
     }
}