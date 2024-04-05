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

     #region FirstRead
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_0Days()
     {
          int firstFileCount = 0;
          FirstTestDateWithLogs(firstFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_1Days()
     {
          int firstFileCount = 1;
          FirstTestDateWithLogs(firstFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_2Days()
     {
          int firstFileCount = 2;
          FirstTestDateWithLogs(firstFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_10Days()
     {
          int firstFileCount = 3;
          FirstTestDateWithLogs(firstFileCount);
     }
     #endregion

     #region SecondRead
     [TestMethod]
     public void DateWithLogs_Should_Second_0_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 0;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Second_1_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 1;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Second_2_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 2;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Second_10_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 10;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
     }
     #endregion

     #region Third
     [TestMethod]
     public void DateWithLogs_Should_Third_0_Second_0_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 0;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_1_Second_0_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 1;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_2_Second_0_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 2;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_3_Second_0_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 3;
         
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }

     [TestMethod]
     public void DateWithLogs_Should_Third_1_Second_1_FirstRead_1Days()
     {
          int firstFileCount = 1, secondFileCount = 1, thirdFileCount = 1;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_0_Second_3_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 3, thirdFileCount = 0;
         
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_3_Second_0_FirstRead_2Days()
     {
          int firstFileCount = 2, secondFileCount = 0, thirdFileCount = 3;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_1_Second_10_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 10, thirdFileCount = 1;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
     }
     #endregion

     #region StartWith
     [TestMethod]
     public void DateWithLogs_Should_0_StartWith_5()
     {
          int firstFileCount = 0, startWithCount = 5;
          StartWithTestDateWithLogs(firstFileCount, startWithCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_1_StartWith_3()
     {
          int firstFileCount = 1, startWithCount = 3;
          StartWithTestDateWithLogs(firstFileCount, startWithCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_2_StartWith_1()
     {
          int firstFileCount = 2, startWithCount = 1;
          StartWithTestDateWithLogs(firstFileCount, startWithCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_10_StartWith_10()
     {
          int firstFileCount = 1, startWithCount = 3;
          StartWithTestDateWithLogs(firstFileCount, startWithCount);
     }
     [TestMethod]
     public void DateWithLogs_Should_5_StartWith_2()
     {
          int firstFileCount = 1, startWithCount = 3;
          StartWithTestDateWithLogs(firstFileCount, startWithCount);
     }

     #endregion

     #region StartWithSecond
     [TestMethod]
     public void DateWithLogs_First_0_Second_0_StartWith_0()
     {
          int first = 0, second = 0, startWith = 0;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
     }
     [TestMethod]
     public void DateWithLogs_First_1_Second_0_StartWith_3()
     {
          int first = 1, second = 0, startWith = 3;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
     }
     [TestMethod]
     public void DateWithLogs_First_1_Second_1_StartWith_1()
     {
          int first = 1, second = 1, startWith = 1;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
     }
     [TestMethod]
     public void DateWithLogs_First_3_Second_2_StartWith_5()
     {
          int first = 3, second = 2, startWith = 5;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
     }
     [TestMethod]
     public void DateWithLogs_First_0_Second_4_StartWith_1()
     {
          int first = 0, second = 4, startWith = 1;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
     }
     [TestMethod]
     public void DateWithLogs_First_4_Second_1_StartWith_0()
     {
          int first = 4, second = 1, startWith = 0;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
     }
     [TestMethod]
     public void DateWithLogs_First_1_Second_2_StartWith_8()
     {
          int first = 1, second = 2, startWith = 8;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second, startWith);
     }
     [TestMethod]
     public void DateWithLogs_First_0_Second_5_StartWith_10()
     {
          int first = 0, second = 5, startWith = 10;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second, startWith);
     }
     [TestMethod]
     public void DateWithLogs_First_2_Second_5_StartWith_0()
     {
          int first = 0, second = 5, startWith = 10;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second, startWith);
     }
     #endregion

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
     private void NextTestDateWithLogs(LogEventManager logEventManager, int fileCount, int startWith  = 0)
     {
          CreateLogFileWithDate(fileCount, startWith);
          var days = logEventManager.GetDateWithLogs();
          CheckDaysCoint(days.Count, startWith, fileCount);

          for (int id = 0; id < days.Count; id++)
          {
               var selectedDay = days[id];
               var eventsOfDay = logEventManager.GetAllLogEvents(selectedDay);
               Assert.AreEqual(id + 1, eventsOfDay.Count);
               Assert.IsTrue(selectedDay == DateTime.Now.AddDays(-id).Date);
          }
     }

     private LogEventManager StartWithTestDateWithLogs(int fileCount, int startWith)
     {
          CreateLogFileWithDate(fileCount, startWith);
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

     #endregion

     #region SupportMethods
     private LogEventManager CreateLogEventManager()
     {
          _sb.CreateDefaultSettings(IPBan.Create(_folder));
          return new LogEventManager(_sb.Settings!);
     }

     private string[] CreateLogFileWithDate(int count, int startWith = 0)
     {
          FileDelete();
          var logFiles = GenerateLogFileMeta(count, startWith);
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

     private Dictionary<string, DateTime> GenerateLogFileMeta(int count, int startWith = 0)
     {
          var logFiles = new Dictionary<string, DateTime>();

          if (count > 0)
               logFiles.Add("logfile.txt", DateTime.Now);

          if (startWith == 0)
          {
               count -= 2;
               for (int x = 0; x <= count; x++)
                    logFiles.Add($"logfile.{count - x}.txt", DateTime.Now.AddDays(-(x + 1)));
          }
          else
          {
               for (int day = 1, file = count + startWith; day <= count; day++)
                    logFiles.Add($"logfile.{file - day}.txt", DateTime.Now.AddDays(-day));
          }

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

     private void CheckDaysCoint(int days, int startWith, int file)
     {
          if (startWith == 0) Assert.AreEqual(file, days);
          else Assert.AreEqual(file + 1, days);
     }
     #endregion
}