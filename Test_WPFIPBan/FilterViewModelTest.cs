using Logic_IPBanUtility.Setting;
using Logic_IPBanUtility;
using WPF_IPBanUtility;
using Logic_IPBanUtility.Logic.LogFile;

namespace EventVMsTest
{
     [TestClass]
     public class FilterViewModelTest
     {
          private readonly string _folder;
          private Settings _settings;

          public FilterViewModelTest()
          {
               _folder = AppDomain.CurrentDomain.BaseDirectory + "\\TestLogs\\";
               _settings = CreateSettings(_folder);
               lgFile = Path.Combine(_folder, "logfile.txt");
               lgFile0 = Path.Combine(_folder, "logfile.0.txt");
               lgFile1 = Path.Combine(_folder, "logfile.1.txt");
               lgFile2 = Path.Combine(_folder, "logfile.2.txt");
               lgFile3 = Path.Combine(_folder, "logfile.3.txt");
               lgFile4 = Path.Combine(_folder, "logfile.4.txt");
               lgFile5 = Path.Combine(_folder, "logfile.5.txt");
          }

          #region ReadFirstDaysLogs
          [TestMethod]
          public void ReadTodayLogs1()
          {
               CreateTestFileWhen1Days();

               var filterVM = CreateFilterVM();
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 1;

               TestFileClear();
               Assert.AreEqual(expected, result);
          }
          [TestMethod]
          public void ReadTodayLogs2()
          {
               CreateTestFileWhen2Days();

               var filterVM = CreateFilterVM();
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 2;

               TestFileClear();
               Assert.AreEqual(expected, result);
          }
          [TestMethod]
          public void ReadTodayLogs3()
          {
               CreateTestFileWhen3Days();

               var filterVM = CreateFilterVM();
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 1;

               TestFileClear();
               Assert.AreEqual(expected, result);
          }
          #endregion

          #region ReadSelectedDate
          [TestMethod]
          public void ReadSecondDayLogs1()
          {
               CreateTestFileWhen2Days();

               var filterVM = CreateFilterVM();
               filterVM.SetSelectedDate(DateTime.Today.AddDays(-1));
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 1;

               TestFileClear();
               Assert.AreEqual(expected, result);
          }
          [TestMethod]
          public void ReadSecondDayLogs2()
          {
               CreateTestFileWhen3Days();

               var filterVM = CreateFilterVM();
               filterVM.SetSelectedDate(DateTime.Today.AddDays(-2));
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 1;

               TestFileClear();
               Assert.AreEqual(expected, result);
          }
          #endregion

          #region ReSelectDate
          [TestMethod]
          public void ReSelectDay1()
          {
               CreateTestFileWhen3Days();

               var filterVM = CreateFilterVM();
               filterVM.SetSelectedDate(DateTime.Today.AddDays(-2));
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 1;
               Assert.AreEqual(expected, result);

               filterVM.SetSelectedDate(DateTime.Today);
               result = filterVM.ObservebleLogEvent.Count;
               Assert.AreEqual(expected, result);
               TestFileClear();
          }
          [TestMethod]
          public void ReSelectDay2()
          {
               CreateTestFileWhen2Days();

               var filterVM = CreateFilterVM();
               filterVM.SetSelectedDate(DateTime.Today);
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 2;
               Assert.AreEqual(expected, result);

               filterVM.SetSelectedDate(DateTime.Today.AddDays(-1));
               expected = 1;
               result = filterVM.ObservebleLogEvent.Count;
               Assert.AreEqual(expected, result);
               TestFileClear();
          }
          #endregion

          #region SearchText
          [TestMethod]
          public void SearchTextEmpty()
          {
               CreateTestFileWhen2Days();
               var filterVM = CreateFilterVM();
               filterVM.SearchedText = string.Empty;
               filterVM.SearchLogEvents();
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 2;

               TestFileClear();
               Assert.AreEqual(expected, result);
          }
          [TestMethod]
          public void SearchTextFind1()
          {
               CreateTestFileWhen2Days();
               var filterVM = CreateFilterVM();
               filterVM.SearchedText = "207";
               filterVM.SearchLogEvents();
               var result = filterVM.ObservebleLogEvent[0];

               var resultCount = filterVM.ObservebleLogEvent.Count;
               var expectedCount = 1;

               TestFileClear();
               Assert.IsTrue(result.Message.Contains(filterVM.SearchedText));
               Assert.AreEqual(expectedCount, resultCount);
          }
          [TestMethod]
          public void SearchTextFind2()
          {
               CreateTestFileWhen2Days();
               var filterVM = CreateFilterVM();
               filterVM.SearchedText = "80";
               filterVM.SearchLogEvents();
               var result = filterVM.ObservebleLogEvent[0];

               var resultCount = filterVM.ObservebleLogEvent.Count;
               var expectedCount = 2;

               TestFileClear();
               Assert.IsTrue(result.Message.Contains(filterVM.SearchedText));
               Assert.AreEqual(expectedCount, resultCount);
          }
          #endregion

          #region SearchTextChangeDate
          [TestMethod]
          public void SearchTextChangeDate1()
          {
               CreateTestFileWhen3Days();
               var filterVM = CreateFilterVM();
               filterVM.SearchedText = "211";
               filterVM.SearchLogEvents();

               var result = filterVM.ObservebleLogEvent[0];
               var resultCount = filterVM.ObservebleLogEvent.Count;
               var expectedCount = 1;

               Assert.IsTrue(result.Message.Contains(filterVM.SearchedText));
               Assert.AreEqual(expectedCount, resultCount);

               filterVM.SetSelectedDate(DateTime.Today.AddDays(-1));

               resultCount = filterVM.ObservebleLogEvent.Count;
               expectedCount = 0;

               Assert.AreEqual(expectedCount, resultCount);
               TestFileClear();
          }
          [TestMethod]
          public void SearchTextChangeDate2()
          {
               CreateTestFileWhen3Days();
               var filterVM = CreateFilterVM();
               filterVM.SetSelectedDate(DateTime.Today.AddDays(-1));
               filterVM.SearchedText = "207";
               filterVM.SearchLogEvents();

               var result = filterVM.ObservebleLogEvent[0];
               var resultCount = filterVM.ObservebleLogEvent.Count;
               var expectedCount = 1;

               Assert.IsTrue(result.Message.Contains(filterVM.SearchedText));
               Assert.AreEqual(expectedCount, resultCount);

               filterVM.SetSelectedDate(DateTime.Today);
               resultCount = filterVM.ObservebleLogEvent.Count;
               expectedCount = 0;

               Assert.AreEqual(expectedCount, resultCount);
               TestFileClear();
          }
          [TestMethod]
          public void SearchTextChangeDate3()
          {
               CreateTestFileWhen3Days();
               var filterVM = CreateFilterVM();
               filterVM.SetSelectedDate(DateTime.Today.AddDays(-1));
               filterVM.SearchedText = "207";
               filterVM.SearchLogEvents();

               var result = filterVM.ObservebleLogEvent[0];
               var resultCount = filterVM.ObservebleLogEvent.Count;
               var expectedCount = 1;

               Assert.IsTrue(result.Message.Contains(filterVM.SearchedText));
               Assert.AreEqual(expectedCount, resultCount);

               filterVM.SetSelectedDate(DateTime.Today);
               resultCount = filterVM.ObservebleLogEvent.Count;
               expectedCount = 0;

               Assert.AreEqual(expectedCount, resultCount);

               filterVM.SetSelectedDate(DateTime.Today.AddDays(-1));
               result = filterVM.ObservebleLogEvent[0];
               resultCount = filterVM.ObservebleLogEvent.Count;
               expectedCount = 1;

               Assert.IsTrue(result.Message.Contains(filterVM.SearchedText));
               Assert.AreEqual(expectedCount, resultCount);

               TestFileClear();
          }
          #endregion

          #region ReadNewLogs
          [TestMethod]
          public void ReadNewLogs_WhenEmpty()
          {
               var content = string.Empty;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);
               var filterVM = CreateFilterVM();
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 0;
               Assert.AreEqual(expected, result);

               filterVM.ReadNewLogs();
               result = filterVM.ObservebleLogEvent.Count;
               Assert.AreEqual(expected, result);
               TestFileClear();
          }
          [TestMethod]
          public void ReadNewLogs_When1NewLog_1()
          {
               var content = string.Empty;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);
               var filterVM = CreateFilterVM();
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 0;
               Assert.AreEqual(expected, result);

               content = testlogs1;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);
               filterVM.ReadNewLogs();
               result = filterVM.ObservebleLogEvent.Count;
               expected = 1;
               Assert.AreEqual(expected, result);
               TestFileClear();
          }
          [TestMethod]
          public void ReadNewLogs_When1NewLog_2()
          {
               var content = testlogs1;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);
               var filterVM = CreateFilterVM();
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 1;
               Assert.AreEqual(expected, result);

               content = testlogs2;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);
               filterVM.ReadNewLogs();
               result = filterVM.ObservebleLogEvent.Count;
               expected = 2;
               Assert.AreEqual(expected, result);
               TestFileClear();
          }
          [TestMethod]
          public void ReadNewLogs_When2NewLog()
          {
               var content = string.Empty;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);
               var filterVM = CreateFilterVM();
               var result = filterVM.ObservebleLogEvent.Count;
               var expected = 0;
               Assert.AreEqual(expected, result);

               content = testlogs2;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);
               filterVM.ReadNewLogs();
               result = filterVM.ObservebleLogEvent.Count;
               expected = 2;
               Assert.AreEqual(expected, result);
               TestFileClear();
          }
          #endregion

          #region Statistics

          [TestMethod]
          public void Statistics_WhenOneFilterDisable_WhenReadNewEmtpy()
          {
               var content = testlogs3;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);
               var filterVM = CreateFilterVM();
               Assert.AreEqual(3, filterVM.Statistics.AllLogEvent);
               Assert.AreEqual(3, filterVM.ShowedLogEventCount);
               Assert.AreEqual(2 ,filterVM.Statistics.LoginSucceeded);
               Assert.AreEqual(1 ,filterVM.Statistics.LoginFailure);

               filterVM.LoginFailure.IsEnable = false;
               Assert.AreEqual(3, filterVM.Statistics.AllLogEvent);
               Assert.AreEqual(2, filterVM.ShowedLogEventCount);
               Assert.AreEqual(2, filterVM.Statistics.LoginSucceeded);
               Assert.AreEqual(1, filterVM.Statistics.LoginFailure);

               filterVM.ReadNewLogs();
               Assert.AreEqual(3, filterVM.Statistics.AllLogEvent);
               Assert.AreEqual(2, filterVM.ShowedLogEventCount);
               Assert.AreEqual(2, filterVM.Statistics.LoginSucceeded);
               Assert.AreEqual(1, filterVM.Statistics.LoginFailure);
               TestFileClear();
          }
          [TestMethod]
          public void Statistics_WhenReadNew1Log()
          {
               var content = testlogs1;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);
               var filterVM = CreateFilterVM();
               Assert.AreEqual(1, filterVM.Statistics.AllLogEvent);
               Assert.AreEqual(1, filterVM.ShowedLogEventCount);
               Assert.AreEqual(1, filterVM.Statistics.LoginFailure);

               content = testlogs2;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);

               filterVM.ReadNewLogs();
               Assert.AreEqual(2, filterVM.Statistics.AllLogEvent);
               Assert.AreEqual(2, filterVM.ShowedLogEventCount);
               Assert.AreEqual(2, filterVM.Statistics.LoginFailure);
               TestFileClear();
          }
          [TestMethod]
          public void Statistics_DateChange_WhenReadNew1Log()
          {
               var content = testlogs1;
               CreateLogFileWithDate(lgFile, DateTime.Today, content);
               var filterVM = CreateFilterVM();
               Assert.AreEqual(1, filterVM.Statistics.AllLogEvent);
               Assert.AreEqual(1, filterVM.ShowedLogEventCount);
               Assert.AreEqual(1, filterVM.Statistics.LoginFailure);

               content = testlogs3;
               CreateLogFileWithDate(lgFile0, DateTime.Today.AddDays(-1), content);
               filterVM.SetSelectedDate(DateTime.Today.AddDays(-1));

               Assert.AreEqual(3, filterVM.Statistics.AllLogEvent);
               Assert.AreEqual(3, filterVM.ShowedLogEventCount);
               Assert.AreEqual(2, filterVM.Statistics.LoginSucceeded);
               Assert.AreEqual(1, filterVM.Statistics.LoginFailure);
               TestFileClear();
          }
          #endregion

          #region Suport
          private void CreateTestFileWhen1Days()
          {
               CreateLogFileWithDate(lgFile, DateTime.Today, testlogs1);
          }
          private void CreateTestFileWhen2Days()
          {
               CreateLogFileWithDate(lgFile, DateTime.Today, testlogs2);
               CreateLogFileWithDate(lgFile1, DateTime.Today.AddDays(-1), testlogs1);
          }
          private void CreateTestFileWhen3Days()
          {
               CreateLogFileWithDate(lgFile, DateTime.Today, testlogs1);
               CreateLogFileWithDate(lgFile1, DateTime.Today.AddDays(-1), testlogs2);
               CreateLogFileWithDate(lgFile0, DateTime.Today.AddDays(-2), testlogs1);
          }

          private string lgFile, lgFile0, lgFile1, lgFile2, lgFile3, lgFile4, lgFile5;
          private string testlogs1 = "2024-02-15 00:24:19.0287|WARN|IPBan|Login failure: 80.66.88.211, , RDP, 4, 14";
          private string testlogs2 = "2024-02-15 00:24:03.9379|WARN|IPBan|Login failure: 11.80.222.20, , RDP, 2, 14\r\n2024-02-15 00:45:05.8387|WARN|IPBan|Login failure: 80.66.88.207, , RDP, 2, 14";
          private string testlogs3 = "2024-01-29 08:11:08.0237|WARN|IPBan|Login succeeded, address: 217.77.219.65, user name: STR_INSTR_3 Kassa, source: RDP\r\n2024-01-29 08:23:09.1471|WARN|IPBan|Login succeeded, address: 217.77.219.65, user name: STR_INSTR_1, source: RDP\r\n2024-02-15 00:45:05.8387|WARN|IPBan|Login failure: 80.66.88.207, , RDP, 2, 14";


          private FilterViewModel CreateFilterVM()
          {
               var logEventManager = new LogEventManager(_settings);
               FilterViewModel filterViewModel = new(logEventManager);
               return filterViewModel;
          }
          private Settings CreateSettings(string folder)
          {
               var filePath0 = Path.Combine(folder, "logfile.txt");
               CreateLogFileWithDate(filePath0, DateTime.Today);

               var iPBan = IPBan.Create(folder);
               SettingsBuilder sb = new();
               sb.CreateDefaultSettings(iPBan);
               sb.LoadSettings();
               return sb.Settings!;
          }

          private void CreateLogFileWithDate(string filePath, DateTime creationDate, string content = "empty")
          {
               File.WriteAllText(filePath, content);
               File.SetCreationTime(filePath, creationDate);
          }
          private void TestFileClear()
          {
               File.Delete(lgFile);
               File.Delete(lgFile0);
               File.Delete(lgFile1);
               File.Delete(lgFile2);
               File.Delete(lgFile3);
               File.Delete(lgFile4);
               File.Delete(lgFile5);
          }
          #endregion
     }
}