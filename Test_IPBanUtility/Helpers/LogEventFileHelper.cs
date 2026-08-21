using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Setting.Builders;
using Test_IPBanUtility.LogEvent;

namespace Test_IPBanUtility.Helpers;

public class LogEventFileHelper
{
     private readonly IpBanTestHelper _ipBanTestHelper = new();
     public LogEventManager CreateLogEventManager()
     {
          return new LogEventManager(_ipBanTestHelper.Settings);
     }

     public void CreateLogFileWithDate(int count, int startWith = 0)
     {
          FilesDelete();
          CustomLogFileFactory factory = new(_ipBanTestHelper.Folder, count, startWith);
          factory.CreateSimpleFileWithDate();
     }
     public void CreateCustomLogFileWithDate(int simpleContentCount, int customContentCount, string ip, string userName, int fileCount = 1)
     {
          FilesDelete();
          CustomLogFileFactory factory = new(_ipBanTestHelper.Folder, simpleContentCount, customContentCount, ip, userName, fileCount);
          factory.CreateCustomLogFileWithDate();
     }
     public void CreateCustomLogFileWithDate(int contentCount, int fileCount = 1)
     {
          FilesDelete();
          CustomLogFileFactory factory = new(_ipBanTestHelper.Folder,contentCount, 0, "1.1.1.1", "user", fileCount);
          factory.CreateCustomLogFileWithDate();
     }

     public void FilesDelete()
     {
          LogFilePathExtractor pathExtractor = new(_ipBanTestHelper.Folder);
          var paths = pathExtractor.GetDaysWithLogFilePath().Values;
          foreach (var path in paths)
               File.Delete(path);
     }
     public void CheckDaysCount(int days, int startWith, int file)
     {
          if (startWith == 0) Assert.AreEqual(file, days);
          else Assert.AreEqual(file + 1, days);
     }
}