using Logic_IPBanUtility.Logic.LogFile;
using Test_IPBanUtility.LogEvent;

namespace LogEventTest;

[TestClass]
public class LogEventManagerTest
{
     TestLogEventFileService testFileManager = new();

     #region GetDateWithLogs

     #region FirstRead
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_0Days()
     {
          int firstFileCount = 0;
          FirstTestDateWithLogs(firstFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_1Days()
     {
          int firstFileCount = 1;
          FirstTestDateWithLogs(firstFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_2Days()
     {
          int firstFileCount = 2;
          FirstTestDateWithLogs(firstFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_FirstRead_10Days()
     {
          int firstFileCount = 3;
          FirstTestDateWithLogs(firstFileCount);
          testFileManager.FileDelete();
     }
     #endregion

     #region SecondRead
     [TestMethod]
     public void DateWithLogs_Should_Second_0_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 0;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_Second_1_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 1;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_Second_2_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 2;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_Second_10_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 10;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          testFileManager.FileDelete();
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
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_1_Second_0_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 1;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_2_Second_0_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 2;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_3_Second_0_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 0, thirdFileCount = 3;
         
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
          testFileManager.FileDelete();
     }

     [TestMethod]
     public void DateWithLogs_Should_Third_1_Second_1_FirstRead_1Days()
     {
          int firstFileCount = 1, secondFileCount = 1, thirdFileCount = 1;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_0_Second_3_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 3, thirdFileCount = 0;
         
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_3_Second_0_FirstRead_2Days()
     {
          int firstFileCount = 2, secondFileCount = 0, thirdFileCount = 3;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_Third_1_Second_10_FirstRead_0Days()
     {
          int firstFileCount = 0, secondFileCount = 10, thirdFileCount = 1;
          
          var manager = FirstTestDateWithLogs(firstFileCount);
          NextTestDateWithLogs(manager, secondFileCount);
          NextTestDateWithLogs(manager, thirdFileCount);
          testFileManager.FileDelete();
     }
     #endregion

     #region StartWith
     [TestMethod]
     public void DateWithLogs_Should_0_StartWith_5()
     {
          int firstFileCount = 0, startWithCount = 5;
          StartWithTestDateWithLogs(firstFileCount, startWithCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_1_StartWith_3()
     {
          int firstFileCount = 1, startWithCount = 3;
          StartWithTestDateWithLogs(firstFileCount, startWithCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_2_StartWith_1()
     {
          int firstFileCount = 2, startWithCount = 1;
          StartWithTestDateWithLogs(firstFileCount, startWithCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_10_StartWith_10()
     {
          int firstFileCount = 1, startWithCount = 3;
          StartWithTestDateWithLogs(firstFileCount, startWithCount);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_Should_5_StartWith_2()
     {
          int firstFileCount = 1, startWithCount = 3;
          StartWithTestDateWithLogs(firstFileCount, startWithCount);
          testFileManager.FileDelete();
     }

     #endregion

     #region StartWithSecond
     [TestMethod]
     public void DateWithLogs_First_0_Second_0_StartWith_0()
     {
          int first = 0, second = 0, startWith = 0;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_First_1_Second_0_StartWith_3()
     {
          int first = 1, second = 0, startWith = 3;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_First_1_Second_1_StartWith_1()
     {
          int first = 1, second = 1, startWith = 1;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_First_3_Second_2_StartWith_5()
     {
          int first = 3, second = 2, startWith = 5;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_First_0_Second_4_StartWith_1()
     {
          int first = 0, second = 4, startWith = 1;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_First_4_Second_1_StartWith_0()
     {
          int first = 4, second = 1, startWith = 0;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_First_1_Second_2_StartWith_8()
     {
          int first = 1, second = 2, startWith = 8;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second, startWith);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_First_0_Second_5_StartWith_10()
     {
          int first = 0, second = 5, startWith = 10;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second, startWith);
          testFileManager.FileDelete();
     }
     [TestMethod]
     public void DateWithLogs_First_2_Second_5_StartWith_0()
     {
          int first = 0, second = 5, startWith = 10;
          var manager = StartWithTestDateWithLogs(first, startWith);
          NextTestDateWithLogs(manager, second, startWith);
          testFileManager.FileDelete();
     }
     #endregion

     #endregion

     #region TestMethods

     private LogEventManager FirstTestDateWithLogs(int fileCount)
     {
          testFileManager.CreateLogFileWithDate(fileCount);
          var logEventManager = testFileManager.CreateLogEventManager();
          var days = logEventManager.CurrentDayWithLogs;
          for (int id = 0; id < days.Count; id++)
          {
               var selectedDay = days[id];
               var eventsOfDay = logEventManager.GetLogEvents(selectedDay);
               Assert.AreEqual(id + 1, eventsOfDay.Count);
               Assert.IsTrue(selectedDay == DateTime.Now.AddDays(-id).Date);
          }
          return logEventManager;
     }
     private void NextTestDateWithLogs(LogEventManager logEventManager, int fileCount, int startWith  = 0)
     {
          testFileManager.CreateLogFileWithDate(fileCount, startWith);
          logEventManager.CheckDaysWithLogsChanged();
          var days = logEventManager.CurrentDayWithLogs;
          testFileManager.CheckDaysCount(days.Count, startWith, fileCount);

          for (int id = 0; id < days.Count; id++)
          {
               var selectedDay = days[id];
               var eventsOfDay = logEventManager.GetLogEvents(selectedDay);
               Assert.AreEqual(id + 1, eventsOfDay.Count);
               Assert.IsTrue(selectedDay == DateTime.Now.AddDays(-id).Date);
          }
     }
     private LogEventManager StartWithTestDateWithLogs(int fileCount, int startWith)
     {
          testFileManager.CreateLogFileWithDate(fileCount, startWith);
          var logEventManager = testFileManager.CreateLogEventManager();
          var days = logEventManager.CurrentDayWithLogs;
          for (int id = 0; id < days.Count; id++)
          {
               var selectedDay = days[id];
               var eventsOfDay = logEventManager.GetLogEvents(selectedDay);
               Assert.AreEqual(id + 1, eventsOfDay.Count);
               Assert.IsTrue(selectedDay == DateTime.Now.AddDays(-id).Date);
          }
          return logEventManager;
     }
     #endregion

     
}