using System.Text;

namespace Test_IPBanUtility.LogEvent
{
     internal class CustomLogFileFactory
     {
          private readonly string _folder;
          private readonly int _createFileCount;
          private readonly int _startCreateWith;

          private readonly int _simpleContentCount;
          private readonly int _customContentCount;

          private readonly string? _ipAddress;
          private readonly string? _userName;

          public CustomLogFileFactory(string folder,int createFileCount, int startCreateWith = 0)
          {
               _folder = folder;
               _createFileCount = createFileCount;
               _startCreateWith = startCreateWith;

               _simpleContentCount = createFileCount;
               _customContentCount = 0;
          }

          public CustomLogFileFactory(string folder, int simpleContentCount, int customContentCount, string ipAddress,
               string userName, int createFileCount, int fileCount = 1)
          {
               _simpleContentCount = simpleContentCount;
               _customContentCount = customContentCount;
               _ipAddress = ipAddress;
               _userName = userName;
               _createFileCount = createFileCount;
               _folder = folder;
               _createFileCount = fileCount;
          }

          public void CreateSimpleFileWithDate()
          {
               var logFilesMeta = GenerateSimpleLogFileMeta();
               var logFileNames = logFilesMeta.Keys.ToArray();
               var logFileDates = logFilesMeta.Values.ToArray();

               for (int id = 0; id < logFilesMeta.Count; id++)
                    CreateLogFileWithDate(logFileNames[id], logFileDates[id], id + 1);
          }

          public void CreateCustomLogFileWithDate()
          {
               var logFilesMeta = GenerateSimpleLogFileMeta();

               foreach (var logMeta in logFilesMeta)
                    CreateLogFileWithDate(logMeta.Key, logMeta.Value);
          }

          private void CreateLogFileWithDate(string fileName, DateTime creationDate)
          {
               var filePath = Path.Combine(_folder, fileName);
               File.WriteAllText(filePath, CreateContentCount());
               File.SetCreationTime(filePath, creationDate);
          }

          private void CreateLogFileWithDate(string fileName, DateTime creationDate, int contentCount)
          {
               var filePath = Path.Combine(_folder, fileName);
               File.WriteAllText(filePath, CreateContentCount(contentCount));
               File.SetCreationTime(filePath, creationDate);
          }

          private Dictionary<string, DateTime> GenerateSimpleLogFileMeta()
          {
               var logFiles = new Dictionary<string, DateTime>();
               var count = _createFileCount;
               if (count > 0)
                    logFiles.Add("logfile.txt", DateTime.Now);

               if (_startCreateWith == 0)
               {
                    count -= 2;
                    for (int x = 0; x <= count; x++)
                         logFiles.Add($"logfile.{count - x}.txt", DateTime.Now.AddDays(-(x + 1)));
               }
               else if (_createFileCount > 1)
               {
                    for (int day = 1, file = count + _startCreateWith; day <= count; day++)
                         logFiles.Add($"logfile.{file - day}.txt", DateTime.Now.AddDays(-day));
               }

               return logFiles;
          }

          private string CreateContentCount(int count)
          {
               StringBuilder sb = new();
               for (int i = 0; i < count; i++)
                    sb.AppendLine(
                         "2024-01-26 08:40:32.5901|WARN|IPBan|Login succeeded, address: 27.7.9.65, user name: TOV, source: RDP");

               return sb.ToString();
          }

          private string CreateContentCount()
          {
               StringBuilder sb = new();
               for (int i = 0; i < _customContentCount; i++)
                    sb.AppendLine(
                         $"2024-01-26 08:40:32.5901|WARN|IPBan|Login succeeded, address: {_ipAddress}, user name: {_userName}, source: RDP");
               for (int i = 0; i < _simpleContentCount; i++)
                    sb.AppendLine(
                         $"2024-01-26 08:40:32.5901|WARN|IPBan|Banning ip address: 1.11.11.1, user name: , config blacklisted: False, count: 3, extra info: , duration: 00:05:00");

               return sb.ToString();
          }
     }
}