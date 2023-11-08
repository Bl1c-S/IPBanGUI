using System.Text.RegularExpressions;

namespace Logic_IPBanUtility;

public class Key
{
     private const string PATTERN = "value=\"(.*?)\"";
     private string _context;

     public int Index;
     public string Name;
     public string Value;
     public string Comment;

     public Key(int index, string name, string context, string comment)
     {
          Index = index;
          Name = name;
          _context = context;
          Comment = comment;
          Value = GetValue();
     }
     public string GetValue()
     {
          Match match = Regex.Match(_context, PATTERN);
          if (!match.Success)
               throw new Exception("Value not found from this context: " + _context);

          return match.Groups[1].Value;
     }
     public void InsertValue(string newValue)
     {
          Value = newValue;
          _context = Regex.Replace(_context, PATTERN, $"value=\"{newValue}\"");
     }
}