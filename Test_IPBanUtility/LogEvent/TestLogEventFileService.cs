using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Setting;
using Logic_IPBanUtility.Setting.Builders;
using NLog.LayoutRenderers;
using System.Text;

namespace Test_IPBanUtility.LogEvent;

public class TestLogEventFileService
{
     private readonly string _folder = AppDomain.CurrentDomain.BaseDirectory + "\\TestLogs\\";
     private readonly SettingsBuilder _sb = new();

     public LogEventManager CreateLogEventManager()
     {
          CreateEmptyFilesForIPBan();
          _sb.CreateDefaultSettings(IPBan.Create(_folder));
          return new LogEventManager(_sb.Settings!);
     }
     private IPBan CreateEmptyFilesForIPBan()
     {
          CreateEmptyFiles("ipban.config");
          CreateEmptyFiles("ipban.sqlite");
          return IPBan.Create(_folder);
     }
     private void CreateEmptyFiles(string name)
     {
          var path = Path.Combine(_folder, name);
          if (!File.Exists(path)) File.Create(path);
     }

     public void CreateLogFileWithDate(int count, int startWith = 0)
     {
          FileDelete();
          CustomLogFileFactory factory = new(count, startWith);
          factory.CreateSimpleFileWithDate();
     }
     public void CreateCustomLogFileWithDate(int simpleContentCount, int customContentCount, string ip, string userName, int fileCount = 1)
     {
          FileDelete();
          CustomLogFileFactory factory = new(simpleContentCount, customContentCount, ip, userName, fileCount);
          factory.CreateCustomLogFileWithDate();
     }
     public void CreateCustomLogFileWithDate(int contentCount, int fileCount = 1)
     {
          FileDelete();
          CustomLogFileFactory factory = new(contentCount, 0, "1.1.1.1", "user", fileCount);
          factory.CreateCustomLogFileWithDate();
     }

     public void FileDelete()
     {
          LogFilePathExtractor pathExtractor = new(_folder);
          var paths = pathExtractor.GetDaysWithLogFilePath().Values;
          foreach (var path in paths)
               File.Delete(path);
     }
     public void CheckDaysCount(int days, int startWith, int file)
     {
          if (startWith == 0) Assert.AreEqual(file, days);
          else Assert.AreEqual(file + 1, days);
     }

     private class CustomLogFileFactory
     {
          private readonly string _folder = AppDomain.CurrentDomain.BaseDirectory + "\\TestLogs\\";
          public int CreateFileCount;
          public int StartCreateWith;

          public int SimpleContentCount;
          public int CustomContentCount;

          private string? _ipAdress;
          private string? _userName;

          public CustomLogFileFactory(int createFileCount, int startCreateWith = 0)
          {
               CreateFileCount = createFileCount;
               StartCreateWith = startCreateWith;

               SimpleContentCount = createFileCount;
               CustomContentCount = 0;
          }
          public CustomLogFileFactory(int simpleContentCount, int customContentCount, string ipAdress, string userName, int fileCount = 1)
          {
               SimpleContentCount = simpleContentCount;
               CustomContentCount = customContentCount;
               _ipAdress = ipAdress;
               _userName = userName;
               CreateFileCount = fileCount;
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
               var count = CreateFileCount;
               if (count > 0)
                    logFiles.Add("logfile.txt", DateTime.Now);

               if (StartCreateWith == 0)
               {
                    count -= 2;
                    for (int x = 0; x <= count; x++)
                         logFiles.Add($"logfile.{count - x}.txt", DateTime.Now.AddDays(-(x + 1)));
               }
               else if (CreateFileCount > 1)
               {
                    for (int day = 1, file = count + StartCreateWith; day <= count; day++)
                         logFiles.Add($"logfile.{file - day}.txt", DateTime.Now.AddDays(-day));
               }
               return logFiles;
          }

          private string CreateContentCount(int count)
          {
               StringBuilder sb = new();
               for (int i = 0; i < count; i++)
                    sb.AppendLine("2024-01-26 08:40:32.5901|WARN|IPBan|Login succeeded, address: 27.7.9.65, user name: TOV, source: RDP");

               return sb.ToString();
          }
          private string CreateContentCount()
          {
               StringBuilder sb = new();
               for (int i = 0; i < CustomContentCount; i++)
                    sb.AppendLine($"2024-01-26 08:40:32.5901|WARN|IPBan|Login succeeded, address: {_ipAdress}, user name: {_userName}, source: RDP");
               for (int i = 0; i < SimpleContentCount; i++)
                    sb.AppendLine($"2024-01-26 08:40:32.5901|WARN|IPBan|Banning ip address: 1.11.11.1, user name: , config blacklisted: False, count: 3, extra info: , duration: 00:05:00");

               return sb.ToString();
          }
     }
}