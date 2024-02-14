using Logic_IPBanUtility.Models;
using Logic_IPBanUtility.Properties;

namespace Logic_IPBanUtility;

public class KeyBuilder
{
     public List<string> Context;

     public KeyBuilder(List<string> context)
     {
          Context = context;
     }

     public List<Key> GetKeys(List<KeyIdenti> keyIdentis)
     {
          List<Key> newKeys = new();
          foreach (var keyIdenti in keyIdentis)
               newKeys.Add(GetKey(keyIdenti));
          return newKeys;
     }
     private Key GetKey(KeyIdenti keyIdenti)
     {
          var keyContext = GetKeyContext(keyIdenti.Name);
          var index = Context.IndexOf(keyContext);
          var comment = GetKeyComment(keyIdenti.Name);
          Key key = new(index, keyContext, comment, keyIdenti);
          return key;
     }
     private string GetKeyContext(string name)
     {
          var keyContext = Context.FirstOrDefault(x => x.Contains($"add key=\"{name}\""));
          if (keyContext is null)
               throw new KeyNotFoundException($"Не знайдено контекст для ключа: {name} " +
                    $"\n Перевірте наявність ключа в файлі конфігурації IPBan та в списку ключів KeyIdentis");
          return keyContext;
     }

     #region GetKeyComment
     private string GetKeyComment(string keyName)
     {
          var description = KeyDescription.ResourceManager.GetString(keyName) ?? string.Empty;
          return description;
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
     #endregion
}
