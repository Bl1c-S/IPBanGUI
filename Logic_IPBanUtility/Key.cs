using System.Text.RegularExpressions;

namespace Logic_IPBanUtility;

public class Key
{
     private const string PATTERN = "value=\"(.*?)\"";

     public int Index;
     public string Name;
     public string Context;
     public string Comment;
     public string Value => GetValue();

     public Key(int index, string name, string context, string comment)
     {
          Index = index;
          Name = name;
          Context = context;
          Comment = comment;
     }
     public void InsertValue(string newValue)
     {
          Context = Regex.Replace(Context, PATTERN, $"value=\"{newValue}\"");
     }
     private string GetValue()
     {
          Match match = Regex.Match(Context, PATTERN);
          if (!match.Success)
               throw new Exception("Value not found from context key: " + Name);

          return match.Groups[1].Value;
     }
}