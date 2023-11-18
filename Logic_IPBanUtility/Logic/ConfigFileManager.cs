using Logic_IPBanUtility.Builders;
using Logic_IPBanUtility.Models;
using System.Text.Json;

namespace Logic_IPBanUtility;

public class ConfigFileManager
{
     public List<string> Context = new();
     public List<Key> Keys = new();
     public List<KeyIdenti> keyIdentis = new();
     private KeyBuilder _keyBuilder;

     private readonly string _contextPath;
     private readonly string _keyIdentiPath;

     public ConfigFileManager(Settings settings)
     {
          _contextPath = settings.ContextFilePath;
          _keyIdentiPath = settings.KeyIdentiFilePath;

          Context = GetContext();
          _keyBuilder = new(Context);

          keyIdentis = ReadKeyIdenti();
          Keys = _keyBuilder.GetKeys(keyIdentis);
     }
     private List<KeyIdenti> ReadKeyIdenti()
     {
          if (!File.Exists(_keyIdentiPath))
               throw new FileNotFoundException();

          var fileData = File.ReadAllText(_keyIdentiPath);
          var KeyIdentis = JsonSerializer.Deserialize<List<KeyIdenti>>(fileData);
          if (KeyIdentis == null)
               throw new FileLoadException($"Помилка завантаження: KeyIdenti з файлу {_keyIdentiPath} " +
                    $"\nПеревірте цілісність файлу, та спробуйте завантажити цей файл з папки becap");
          return KeyIdentis;
     }
     public void WriteKeyIdentiChanged(KeyIdenti keyIdenti)
     {
          var item = keyIdentis.Find(x => x.Name == keyIdenti.Name);
          if (item == null)
               throw new Exception($"Під час запису {keyIdenti.Name}, в списку завантажених keyIdentis, не знайдено Відповідного елемента");
          keyIdentis.Remove(item);
          keyIdentis.Add(keyIdenti);
          keyIdentis.Sort();
          try
          {
          var serializebleKeyIdenti = JsonSerializer.Serialize(keyIdentis);
          File.WriteAllText(_keyIdentiPath, serializebleKeyIdenti);
          }
          catch (Exception ex)
          {
               throw new Exception($"Під час запису файлу за шляхом: {_keyIdentiPath} виникла помилка:  {ex.Message}");
          }
     }

     public void WriteContextChanged(IEnumerable<Key> keys)
     {
          foreach (var key in keys)
          {
               if (key.IsChanged)
                    Context.Insert(key.Index, key.Context);
          }
          File.WriteAllLines(_contextPath, Context);
     }
     public void WriteContextChanged(Key key)
     {
          if (key.IsChanged)
          {
               Context.Insert(key.Index, key.Context);
               File.WriteAllLines(_contextPath, Context);
          }
     }

     public List<string> GetContext() => ReadFile(_contextPath);
     private List<string> ReadFile(string filePath)
     {
          if (!File.Exists(filePath))
               throw new FileNotFoundException();

          var fileData = File.ReadAllLines(filePath).ToList();
          if (fileData.Count == 0)
               throw new FileLoadException();

          return fileData;
     }
}