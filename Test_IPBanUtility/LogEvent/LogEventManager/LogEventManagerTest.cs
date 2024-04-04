using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Setting;
using Logic_IPBanUtility.Setting.Builders;
using System.Text;

namespace LogEventTest;

[TestClass]
public class LogEventManagerTest
{
     private string _folder;
     SettingsBuilder _sb = new();
     public LogEventManagerTest()
     {
          _folder = AppDomain.CurrentDomain.BaseDirectory + "\\TestLogs\\TestManager\\";
     }

     #region GetDateWithLogs

     [TestMethod]
     public void DateWithLogs_Should_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0;
          //Test
          FirstTestDateWithLogs(firstFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_1Days()
     {//Arguments for test
          int firstFileCount = 1;
          //Test
          FirstTestDateWithLogs(firstFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_2Days()
     {//Arguments for test
          int firstFileCount = 2;
          //Test
          FirstTestDateWithLogs(firstFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_10Days()
     {//Arguments for test
          int firstFileCount = 3;
          //Test
          FirstTestDateWithLogs(firstFileCount);
     }

     [TestMethod]
     public void DateWithLogs_Should_Second_0_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0, secondFileCount = 0;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Second_1_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0, secondFileCount = 1;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Second_2_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0, secondFileCount = 2;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Second_10_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0, secondFileCount = 10;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
     }

     [TestMethod]
     public void DateWithLogs_Should_Third_0_Second_0_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 0;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_1_Second_0_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 1;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_2_Second_0_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 2;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_3_Second_0_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 3;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }

     [TestMethod]
     public void DateWithLogs_Should_Third_1_Second_1_FirstRead_1Days()
     {//Arguments for test
          int firstFileCount = 1, secondFileCount = 1, thirdFileCount = 1;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_0_Second_3_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0, secondFileCount = 3, thirdFileCount = 0;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_3_Second_0_FirstRead_2Days()
     {//Arguments for test
          int firstFileCount = 2, secondFileCount = 0, thirdFileCount = 3;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_1_Second_10_FirstRead_0Days()
     {//Arguments for test
          int firstFileCount = 0, secondFileCount = 10, thirdFileCount = 1;
          //Test
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     #endregion

     #region TestMethods

     private LogEventManager FirstTestDateWithLogs(int fileCount)
     {
          CreateLogFileWithDate(fileCount);
          var logEventManager = CreateLogEventManager();
          var days = logEventManager.GetDateWithLogs();
          for (int id = 0; id < days.Count; id++)
          {
               var selectedDay = days[id];
               var eventsOfDay = logEventManager.GetAllLogEvents(selectedDay);
               Assert.AreEqual(id + 1, eventsOfDay.Count);
               Assert.IsTrue(selectedDay == DateTime.Now.AddDays(-id).Date);
          }
          return logEventManager;
     }
     private void NextTestDateWithLogs(LogEventManager logEventManager, int fileCount)
     {
          CreateLogFileWithDate(fileCount);
          var days = logEventManager.GetDateWithLogs();
          Assert.AreEqual(fileCount, days.Count);

          for (int id = 0; id < days.Count; id++)
          {
               var selectedDay = days[id];
               var eventsOfDay = logEventManager.GetAllLogEvents(selectedDay);
               Assert.AreEqual(id + 1, eventsOfDay.Count);
               Assert.IsTrue(selectedDay == DateTime.Now.AddDays(-id).Date);
          }
     }
     #endregion

     #region SupportMethods

     private LogEventManager CreateLogEventManager()
     {
          _sb.CreateDefaultSettings(IPBan.Create(_folder));
          return new LogEventManager(_sb.Settings!);
     }
     private string[] CreateLogFileWithDate(int count)
     {
          FileDelete();
          var logFiles = GenerateLogFileMeta(count);
          var logFileNames = logFiles.Keys.ToArray();
          var logFileDates = logFiles.Values.ToArray();

          for (int id = 0; id < logFiles.Count; id++)
               CreateLogFileWithDate(logFileNames[id], logFileDates[id], id + 1);

          return logFiles.Keys.ToArray();
     }
     private void FileDelete()
     {
          LogFilePathExtractor pathExtractor = new(_folder);
          var paths = pathExtractor.GetDaysWithLogFilePath().Values;
          foreach (var path in paths)
               File.Delete(path);
     }
     private Dictionary<string, DateTime> GenerateLogFileMeta(int count)
     {
          var logFiles = new Dictionary<string, DateTime>();

          if (count > 0)
               logFiles.Add("logfile.txt", DateTime.Now);

          count -= 2;
          for (int x = 0; x <= count; x++)
               logFiles.Add($"logfile.{count - x}.txt", DateTime.Now.AddDays(-(x + 1)));
          //else
          //{
          //     count += startWith;
          //     for (int x = startWith; x < count; x++)
          //          logFiles.Add($"logfile.{count - (x - 1)}.txt", DateTime.Now.AddDays(-x));
          //}

          return logFiles;
     }
     private void CreateLogFileWithDate(string fileName, DateTime creationDate, int contentCount)
     {
          var filePath = Path.Combine(_folder, fileName);
          File.WriteAllText(filePath, CreateContentCount(contentCount));
          File.SetCreationTime(filePath, creationDate);
     }
     private string CreateContentCount(int count)
     {
          StringBuilder sb = new();
          for (int i = 0; i < count; i++)
               sb.AppendLine("2024-01-26 08:40:32.5901|WARN|IPBan|Login succeeded, address: 27.7.9.65, user name: TOV, source: RDP");

          return sb.ToString();
     }
     #endregion
}