using Logic_IPBanUtility.Models;
using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;
using System.Text.Json;

namespace Logic_IPBanUtility;

public class ConfigFileManager
{
     private FileManager _fileManager { get; }
     public List<string> Context = new();

     private readonly string _contextPath;
     private readonly string _keyIdentiPath;

     public ConfigFileManager(Settings settings, FileManager fileManager)
     {
          _fileManager = fileManager;
          _contextPath = settings.IPBan.Context;
          _keyIdentiPath = settings.Config.KeyIdenti;
          UpdateContex();
     }

     public void UpdateContex()
     {
          Context = _fileManager.ReadAllLines(_contextPath);
     }
     public Key GetKey(KeyNames keyName)
     {
          UpdateContex();
          KeyBuilder kb = new(Context);
          KeyIdenti keyIndenty = new(false, keyName.ToString());
          return kb.GetKey(keyIndenty);
     }

     public List<Key> CreateKeys()
     {
          var keyIdentis = ReadKeyIndentis();
          UpdateContex();
          var keys = new KeyBuilder(Context).GetKeys(keyIdentis);
          return keys;
     }
     public List<KeyIdenti> ReadKeyIndentis()
     {
          var keyIdentis = _fileManager.GetJson<List<KeyIdenti>>(_keyIdentiPath);
          return keyIdentis;
     }

     public void WriteKeyIdentiChanged(KeyIdenti keyIdenti)
     {
          var keyIdentis = ReadKeyIndentis();
          var oldKeyIdenti = keyIdentis.Find(x => x.Name == keyIdenti.Name);

          if (oldKeyIdenti == null)
               throw new Exception($"{Properties.Resources.ErrorWriteKeyIdentiChanged} {keyIdenti.Name}");
          if (oldKeyIdenti.IsHidden == keyIdenti.IsHidden)
               return;

          var index = keyIdentis.FindIndex(x => x.Name == keyIdenti.Name);
          keyIdentis[index] = keyIdenti;

          try
          {
               var jsonKeyIdenti = JsonSerializer.Serialize(keyIdentis);
               File.WriteAllText(_keyIdentiPath, jsonKeyIdenti);
          }
          catch (Exception ex)
          { throw new Exception($"{Properties.Resources.ErrorFileWrite} {_keyIdentiPath} \n {ex.Message}"); }
     }

     public void WriteKey(Key key)
     {
          Context[key.Index] = key.Context;
          File.WriteAllLines(_contextPath, Context);
     }
     public void WriteKeys(IEnumerable<Key> keys)
     {
          foreach (var key in keys)
               Context[key.Index] = key.Context;
          File.WriteAllLines(_contextPath, Context);
     }
}