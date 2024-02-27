using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Setting;

namespace LogEventTest;

[TestClass]
public class LogEventManagerTest
{
     private string _folder;
     Settings _settings;
     public LogEventManagerTest()
     {
          _folder = AppDomain.CurrentDomain.BaseDirectory + "\\TestLogs\\TestManager\\";
          var filePath0 = Path.Combine(_folder, "logfile.txt");
          CreateLogFileWithDate(filePath0, DateTime.Today);

          var iPBan = IPBan.Create(_folder);
          SettingsBuilder sb = new();
          sb.CreateDefaultSettings(iPBan);
          sb.LoadSettings();
          _settings = sb.Settings!;
     }

     #region GetLogFilePathsByDay
     [TestMethod]
     public void GetLogFilePathsByDay_Should_3Days()
     {
          var filePath0 = Path.Combine(_folder, "logfile.txt");
          var filePath1 = Path.Combine(_folder, "logfile.1.txt");
          var filePath2 = Path.Combine(_folder, "logfile.0.txt");

          CreateLogFileWithDate(filePath0, DateTime.Today);
          CreateLogFileWithDate(filePath1, DateTime.Today.AddDays(-1));
          CreateLogFileWithDate(filePath2, DateTime.Today.AddDays(-2));

          
          var logEventManager = new LogEventManager(_settings);
          //test obnoviti
     }
     #endregion

     #region SupportMethods
     private void CreateLogFileWithDate(string filePath, DateTime creationDate)
     {
          File.WriteAllText(filePath, "");
          File.SetCreationTime(filePath, creationDate);
     }
     #endregion
}
