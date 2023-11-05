using System.Text;

namespace Logic_IPBanUtility;
public class ConfigReader
{
     public readonly string OldNote;

     private readonly string[] _lines;
     private readonly char[] _endNoteAttribute;
     private readonly char? _startAttribute;

     private readonly int _startIndex;
     private readonly int _endIndex;

     public ConfigReader(string[] lines, int lineIndex)
     {
          _endNoteAttribute = new char[] { '+', '-', '[' };
          _startIndex = lineIndex;
          _lines = lines;

          var (oldNote, endIndex, startAttribute) = GetNote();
          OldNote = oldNote;
          _endIndex = endIndex;
          _startAttribute = startAttribute;
     }
     private bool IsHaveEndChar(string line)
     {
          if (line.Length > 0)
          {
               if (_endNoteAttribute.Contains(line[0]))
                    return true;
          }
          return false;
     }
     private (string, int, char?) GetNote()
     {
          if (_startIndex >= _lines.Length)
               throw new ArgumentOutOfRangeException();

          int endIndex = _startIndex + 1;
          string startLine = _lines[_startIndex];
          char? startAttrubute = null;
          StringBuilder sb = new(startLine);

          if (IsHaveEndChar(startLine))
               startAttrubute = startLine[0];

          for (; endIndex < _lines.Length; ++endIndex)
          {
               string line = _lines[endIndex];
               if (IsHaveEndChar(line))
                    break;

               sb.AppendLine($"\n{line}");
          }

          string ufNote = GetUnFormatNote(sb.ToString());
          return (ufNote, endIndex, startAttrubute);
     }

     private string GetUnFormatNote(string note)
     {
          if (note.StartsWith('\n'))
               return note;

          note = note.TrimEnd('\n', '\r');
          return note.TrimStart(_endNoteAttribute);
     }
     private List<string> GetFormatNote(string changedNote)
     {
          string startFormatNote = _startAttribute + changedNote;

          List<string> formattedChangedNote = new();

          foreach (string line in startFormatNote.Split("\n"))
               formattedChangedNote.Add(line.TrimEnd('\r'));

          return formattedChangedNote;
     }

     public string[] InsertInside(string changedNote)
     {
          List<string> allTextList = new(_lines);
          List<string> finalText = new(allTextList.GetRange(0, _startIndex));

          List<string> formattedChangedNote = GetFormatNote(changedNote);
          finalText.AddRange(formattedChangedNote);

          int countEndRange = _lines.Length - _endIndex;
          List<string> endOfTextRange = allTextList.GetRange(_endIndex, countEndRange);
          finalText.AddRange(endOfTextRange);

          return finalText.ToArray();
     }
}
