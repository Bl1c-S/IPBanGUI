using System.Text.RegularExpressions;

namespace Logic_IPBanUtility.Models;

public class Key
{
     private const string PATTERN = "value=\"(.*?)\"";

     public readonly int Index;
     public readonly string Name;
     public readonly string Comment;
     public bool IsChanged { get; private set; }

     public bool IsHidden { get; set; }
     public string Context { get; private set; }
     public string Value { get; private set; }

     public Key(int index, bool isHidden, string name, string context, string comment)
     {
          IsChanged = false;
          Index = index;
          IsHidden = isHidden;
          Name = name;
          Context = context;
          Comment = comment;
          Value = GetValue();
     }
     public void InsertValue(string newValue)
     {
          Value = newValue;
          Context = Regex.Replace(Context, PATTERN, $"value=\"{newValue}\"");
          IsChanged = true;
     }
     private string GetValue()
     {
          Match match = Regex.Match(Context, PATTERN);
          if (!match.Success)
               return string.Empty;

          return match.Groups[1].Value;
     }
}