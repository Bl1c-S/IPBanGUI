namespace Logic_IPBanUtility;

public class ConfigFileManager
{
     public List<string> Context = new();
     public List<Key> Keys = new();

     private readonly string _contextPath;
     private readonly string _keyNamesPath;

     public ConfigFileManager(Settings settings)
     {
          _contextPath = settings.ContextFilePath;
          _keyNamesPath = settings.KeyNamesFilePath;
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
          for (int i = index; ;)
          {
               var str = Context[--i];
               str = RemoveEmptyLine(str);
               str = RemoveDoubleSpaces(str);
               if (str.Contains("\n<!--"))
                    break;
               else if (str.Contains("<!--"))
               {
                    comment.Add(str);
                    break;
               }
               else
                    comment.Add(str);
          }
          comment.Reverse();
          var result = string.Join(' ', comment);
          return result;
     }
     private string RemoveDoubleSpaces(string input)
     {
          while (input.Contains("\t"))
               input = input.Replace("\t", "\n");
          return input;
     }
     private string RemoveEmptyLine(string input)
     {
          while (input.Contains("\n"))
               input = input.Replace("\n", "");
          return input;
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