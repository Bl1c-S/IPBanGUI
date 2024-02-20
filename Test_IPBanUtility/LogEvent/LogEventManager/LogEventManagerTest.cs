using Logic_IPBanUtility.Logic.LogFile;

namespace LogEventTest;

[TestClass]
public class LogEventManagerTest
{
     private string folder;

     public LogEventManagerTest()
     {
          folder = AppDomain.CurrentDomain.BaseDirectory + "\\TestLogs\\";
     }

     #region GetLogFilePathsByDay
     [TestMethod]
     public void GetLogFilePathsByDay_Should_3Days()
     {
          CreateLogFileWithDate("logFile.txt", DateTime.Today);
          CreateLogFileWithDate("logFile.1.txt", DateTime.Today.AddDays(-1));
          CreateLogFileWithDate("logFile.0.txt", DateTime.Today.AddDays(-2));

          var iPBan = IPBan.Create(folder);
          var logEventManager = new LogEventManager(iPBan);
          var result = logEventManager.GetLogFilePathsByDay();

          Assert.AreEqual(3, result.Count);
          Assert.IsTrue(CheckContainsKey(result, DateTime.Today));
          Assert.IsTrue(CheckContainsKey(result, DateTime.Today.AddDays(-1)));
          Assert.IsTrue(CheckContainsKey(result, DateTime.Today.AddDays(-2)));
     }
     [TestMethod]
     public void GetLogFilePathsByDay_Should_1Days()
     {
          CreateLogFileWithDate("logFile.txt", DateTime.Today);

          var iPBan = IPBan.Create(folder);
          var logEventManager = new LogEventManager(iPBan);
          var result = logEventManager.GetLogFilePathsByDay();

          Assert.AreEqual(1, result.Count);
          Assert.IsTrue(CheckContainsKey(result, DateTime.Today));
     }
     #endregion

     #region SupportMethods
     private bool CheckContainsKey(Dictionary<DateTime, string> logFilePathsByDay, DateTime expectedKey)
     {
          if (!logFilePathsByDay.ContainsKey(expectedKey))
               return false;
          FileDelate(logFilePathsByDay[expectedKey]);
          return true;
     }
     private void CreateLogFileWithDate(string fileName, DateTime creationDate)
     {
          string filePath = Path.Combine(folder, fileName);
          File.WriteAllText(filePath, "");
          File.SetCreationTime(filePath, creationDate);
     }
     private void FileDelate(string fileName) => File.Delete(Path.Combine(folder, fileName));
     #endregion
}
