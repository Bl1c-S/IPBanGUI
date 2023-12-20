using Newtonsoft.Json;

namespace Logic_IPBanUtility.Services;

public class FileManager
{
     public void CreateDefaultDirectory(string path)
     {
          if (Directory.Exists(path))
               Directory.Delete(path, true);
          Directory.CreateDirectory(path);
     }
     public void SaveText(string path, string content) => File.WriteAllText(path, content);
     public void SaveJson<T>(string path, T content)
     {
          try
          {
               string settingsJson = JsonConvert.SerializeObject(content);
               File.WriteAllText(path, settingsJson);
          }
          catch (Exception ex)
          {
               throw new Exception($"Помилка під час збереження файлу {path}. \n{ex.Message}");
          }
     }

     public IEnumerable<string> GetStrings(string filePath) => File.ReadAllLines(filePath);
     public T GetJson<T>(string path)
     {
          var json = File.ReadAllText(path);
          var result = JsonConvert.DeserializeObject<T>(json);
          if (result is null)
               throw new FileLoadException(path);
          return result;
     }
}
