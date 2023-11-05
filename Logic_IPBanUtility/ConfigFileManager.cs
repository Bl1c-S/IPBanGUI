using NLog;
using System.Text;

namespace Logic_IPBanUtility;

public class ConfigFileManager
{
     private readonly string _contextPath;
     private readonly string keyNamesPath;
     private List<string> _context;

     public ConfigFileManager(string directoryPath)
     {
          DirectoryCheck(directoryPath);
          _contextPath = Path.Combine(directoryPath, "ipban.config");
          keyNamesPath = Path.Combine(directoryPath, "keynames.txt");
          _context = ReadFile(_contextPath);
     }
     public ConfigContextManager CreateConfigContextManager()
     {
          var names = ReadFile(keyNamesPath);
          names = GetCurrentKeyNames(names);
          var keys = GetKeys(names);
          return new(this, keys, _context);
     }
     public List<string> UpdateCfgContext() => ReadFile(_contextPath);
     private List<Key> GetKeys(IEnumerable<string> keyNames)
     {
          List<Key> keys = new();
          foreach (var keyName in keyNames)
               keys.Add(GetKey(keyName));
          return keys;
     }
     private Key GetKey(string keyName)
     {
          (string context, int index) = GetKeyContextAndIndex(keyName);
          string comment = GetKeyComment(index);
          Key key = new(index, keyName, context, comment);
          return key;
     }
     private string GetKeyComment(int index)
     {
          List<string> comment = new();
          for (int i = index; ; i--)
          {
               var str = _context[i];
               comment.Add(str);
               if (str.Contains("<!--"))
                    break;
          }
          comment.Reverse();
          StringBuilder sb = new();
          foreach (string str in comment)
               sb.AppendLine(str);

          var result = sb.ToString();
          return result;
     }
     private (string, int) GetKeyContextAndIndex(string key)
     {
          var keyContext = _context.FirstOrDefault(x => x.Contains($"add key=\"{key}\""));
          if (keyContext is null)
               throw new KeyNotFoundException(key);

          int index = _context.IndexOf(keyContext);
          return (keyContext, index);
     }

     private void DirectoryCheck(string directoryPath)
     {
          if (!Directory.Exists(directoryPath))
               throw new DirectoryNotFoundException();
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