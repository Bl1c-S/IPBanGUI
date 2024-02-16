namespace Logic_IPBanUtility.Services;
/// <summary>
/// Readed
/// </summary>
public class StreamFileManager
{
     private long _lastPosition;
     public List<string> StreamReadAllNewLines(string filePath)
     {
          using (FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
          {
               using (StreamReader sr = new(fs))
               {
                    var newLines = new List<string>();
                    fs.Seek(_lastPosition, SeekOrigin.Begin);

                    while (true)
                    {
                         var line = sr.ReadLine();
                         if (line is null) break;
                         newLines.Add(line);
                    }
                    _lastPosition = fs.Position;
                    return newLines;
               }
          }
     }
}
