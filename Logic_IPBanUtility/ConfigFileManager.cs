using System.Text;

namespace Logic_IPBanUtility;

public class ConfigFileManager
{
     public List<string> Context = new();
     public List<Key> Keys = new();

     private readonly string _contextPath;
     private readonly string _keyNamesPath;

     public ConfigFileManager(string directoryPath)
     {
          _contextPath = Path.Combine(directoryPath, "ipban.config");
          _keyNamesPath = Path.Combine(directoryPath, "keynames.txt");
          GetContext();
          GetKeys();
     }
     public void WriteContextChanged(IEnumerable<Key> keys)
     {
          foreach (var key in keys)
               Context.Insert(key.Index, key.Context);
          File.WriteAllLines(_contextPath, Context);
     }
     public void GetContext() => Context = ReadFile(_contextPath);
     public void GetKeys()
     {
          var names = ReadFile(_keyNamesPath);
          List<Key> newKeys = new();
          foreach (var name in names)
               newKeys.Add(GetKey(name));
          Keys = newKeys;
     }
     private Key GetKey(string name)
     {
          var keyContext = GetKeyContext(name);
          var index = GetKeyIndex(keyContext);
          var comment = GetKeyComment(index);
          Key key = new(index, name, keyContext, comment);
          return key;
     }
     private string GetKeyContext(string name)
     {
          var keyContext = Context.FirstOrDefault(x => x.Contains($"add key=\"{name}\""));
          if (keyContext is null)
               throw new KeyNotFoundException(name);
          return keyContext;
     }
     private int GetKeyIndex(string keyContext)
     {
          int index = Context.IndexOf(keyContext);
          return index;
     }
     private string GetKeyComment(int index)
     {
          List<string> comment = new();
          for (int i = index; ; i--)
          {
               var str = Context[i];
               comment.Add(str);
               if (str.Contains("<!--"))
                    break;
          }
          comment.Reverse();
          //TODO test ToString
          var result = comment.ToString()!;
          return result;
     }
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