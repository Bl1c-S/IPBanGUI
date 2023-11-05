using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Logic_IPBanUtility;
/// <summary>
/// Use ConfigFileManager method CreateConfigFileService()
/// </summary>
public class ConfigContextManager
{
     public List<string> Context;
     private List<Key> _keys;
     private ConfigFileManager contextFileManager;

     public ConfigContextManager(ConfigFileManager contextFileManager, List<Key> keys, List<string> context)
     {
          _keys = keys;
          Context = context;
          this.contextFileManager = contextFileManager;
     }
     public Key GetKey(string keyName)
     {
          (string context, int index) = GetKeyContextAndIndex(keyName);
          string comment = GetKeyComment(index);
          Key key = new(index, keyName, context, comment);
          return key;
     }
     public void SaveKey(Key key) 
     {
          Context.Insert(key.Index, key.Context);
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
          StringBuilder sb = new();
          foreach (string str in comment)
               sb.AppendLine(str);

          var result = sb.ToString();
          return result;
     }
     private (string, int) GetKeyContextAndIndex(string key)
     {
          var keyContext = Context.FirstOrDefault(x => x.Contains($"add key=\"{key}\""));
          if (keyContext is null)
               throw new KeyNotFoundException(key);

          int index = Context.IndexOf(keyContext);
          return (keyContext, index);
     }
}