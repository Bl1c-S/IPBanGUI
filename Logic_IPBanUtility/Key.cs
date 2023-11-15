using System.Text.RegularExpressions;

namespace Logic_IPBanUtility;

public class Key
{
     private const string PATTERN = "value=\"(.*?)\"";

     public readonly int Index;
     public readonly string Name;
     public readonly string Comment;

     public string Context { get; private set; }
     public string Value { get; private set; }

     public Key(int index, string name, string context, string comment)
     {
          Index = index;
          Name = name;
          Context = context;
          Comment = comment;
          Value = GetValue();
     }
     public void InsertValue(string newValue)
     {
          Value = newValue;
          Context = Regex.Replace(Context, PATTERN, $"value=\"{newValue}\"");
     }
     private string GetValue()
     {
          Match match = Regex.Match(Context, PATTERN);
          if (!match.Success)
               return string.Empty;

          return match.Groups[1].Value;
     }
}