using System.Text.RegularExpressions;

namespace Logic_IPBanUtility.Models;

public class Key
{
     public KeyIdenti KeyIdenti;

     private const string PATTERN = "value=\"(.*?)\"";

     public readonly int Index;
     public string Name => KeyIdenti.Name;
     public bool IsHidden => KeyIdenti.IsHidden;

     public string Description { get; private set; }

     public string Context { get; private set; }
     public string Value { get; private set; }

     public Key(int index, string context, string description, KeyIdenti keyIdenti)
     {
          KeyIdenti = keyIdenti;
          Index = index;
          Context = context;
          Description = description;
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