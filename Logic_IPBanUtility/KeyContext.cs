using System.Text.RegularExpressions;

namespace Logic_IPBanUtility;

public class Key
{
     const string PATTERN = "value=\"(.*?)\"";
     public int Index;
     public string Name;
     public string Context;
     public string Value;
     public string Comment;

     public Key(int index, string name, string context, string comment)
     {
          Index = index;
          Name = name;
          Context = context;
          Comment = comment;
          Value = GetValue();
     }
     public string GetValue()
     {
          Match match = Regex.Match(Context, PATTERN);
          if (!match.Success)
               throw new Exception("Value not found from this context: " + Context);

          return match.Groups[1].Value;
     }
     public void InsertValue(string newValue)
     {
          Value = newValue;
          Context = Regex.Replace(Context, PATTERN, $"value=\"{newValue}\"");
     }
}