using System.Text.RegularExpressions;

namespace Logic_IPBanUtility.Models;

public class Key
{
     public KeyIdenti KeyIdenti;

     private const string PATTERN = "value=\"(.*?)\"";

     public readonly int Index;
     public string Name => KeyIdenti.Name;
     public bool IsHidden => KeyIdenti.IsHidden;

     public readonly string Comment;

     public string Context { get; private set; }
     public string Value { get; private set; }

     public Key(int index, string context, string comment, KeyIdenti keyIdenti)
     {
          KeyIdenti = keyIdenti;
          Index = index;
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