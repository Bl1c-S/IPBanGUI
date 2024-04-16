using WPF_IPBanUtility;
using Test_IPBanUtility.LogEvent;

namespace EventVMsTest
{
     [TestClass]
     public class FilterViewModelTest
     {
          TestLogEventFileService _testFileManager = new();

          #region SelectDate

          [TestMethod]
          public void SelectDay_When0Days()
          {
               int fileCount = 0;
               SelectDayTest_WhenFileCount(fileCount);
          }
          [TestMethod]
          public void SelectDay_When1Days()
          {
               int fileCount = 1;
               SelectDayTest_WhenFileCount(fileCount);
          }
          [TestMethod]
          public void SelectDay_When2Days()
          {
               int fileCount = 2;
               SelectDayTest_WhenFileCount(fileCount);
          }
          [TestMethod]
          public void SelectDay_When3Days()
          {
               int fileCount = 3;
               SelectDayTest_WhenFileCount(fileCount);
          }

          #region Factory
          public void SelectDayTest_WhenFileCount(int fileCount)
          {
               for (int selectDay = -3; selectDay > 5; selectDay++)
                    SelectDayTestFactory(fileCount, selectDay);
          }
          public FilterViewModel SelectDayTestFactory(int fileCount, int selectDay)
          {
               _testFileManager.CreateLogFileWithDate(fileCount);

               var filterVM = CreateFilterVM();
               TestDaySelected(filterVM, selectDay);
               return filterVM;
          }
          public void ReSelectDayTestFactory(FilterViewModel filterVM, int reSelectDay)
          {
               TestDaySelected(filterVM, reSelectDay);
          }
          public void ReSelectDayAndChangeFileCountTestFactory(FilterViewModel filterVM, int reSelectDay, int changeFileCount)
          {
               _testFileManager.FileDelete();
               _testFileManager.CreateLogFileWithDate(changeFileCount);
               TestDaySelected(filterVM, reSelectDay);
          }

          private void TestDaySelected(FilterViewModel filterVM, int selectedDay)
          {
               var selectedDateTime = CreateDate(selectedDay);
               filterVM.SetSelectedDate(selectedDateTime);

               if (filterVM.SelectableDateRangeStart <= filterVM.SelectedDate && filterVM.SelectableDateRangeEnd >= filterVM.SelectedDate)
                    Assert.IsTrue(1 == filterVM.ObservebleLogEvents.Count || 0 == filterVM.ObservebleLogEvents.Count);
               else
                    Assert.AreEqual(filterVM.SelectedDate, filterVM.SelectableDateRangeStart);
          }
          #endregion
          #endregion

          #region SearchText
          [TestMethod]
          public void SearchText_WhenEmpty1()
          {
               int simple = 2, custom = 1;
               int expected = simple + custom;
               string searchedText = string.Empty, ip = "2.1.2.1", user = "exp";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SearchTest(searchedInfo);
               _testFileManager.FileDelete();
          }
          [TestMethod]
          public void SearchText_WhenEmpty2()
          {
               int simple = 0, custom = 4;
               int expected = simple + custom;
               string searchedText = string.Empty, ip = "2.1.2.1", user = "exp";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SearchTest(searchedInfo);
               _testFileManager.FileDelete();
          }
          [TestMethod]
          public void SearchText_WhenEmpty3()
          {
               int simple = 3, custom = 0;
               int expected = simple + custom;
               string searchedText = string.Empty, ip = "2.1.2.1", user = "exp";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SearchTest(searchedInfo);
               _testFileManager.FileDelete();
          }

          [TestMethod]
          public void SearchText_WhenSimple1()
          {
               int simple = 3, custom = 1;
               int expected = custom;
               string searchedText = "Ex", ip = "2.1.2.1", user = "exp";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SearchTest(searchedInfo);
               _testFileManager.FileDelete();
          }
          [TestMethod]
          public void SearchText_WhenSimple2()
          {
               int simple = 1, custom = 5;
               int expected = custom;
               string searchedText = "exp", ip = "2.1.2.1", user = "exp";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SearchTest(searchedInfo);
               _testFileManager.FileDelete();
          }
          [TestMethod]
          public void SearchText_WhenSimple3()
          {
               int simple = 0, custom = 1;
               int expected = custom;
               string searchedText = "eX2", ip = "2.1.2.1", user = "ex2p";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SearchTest(searchedInfo);
               _testFileManager.FileDelete();
          }
          [TestMethod]
          public void SearchText_WhenSimple4()
          {
               int simple = 1, custom = 0;
               int expected = custom;
               string searchedText = "2", ip = "2.1.2.1", user = "ex";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SearchTest(searchedInfo);
               _testFileManager.FileDelete();
          }
          [TestMethod]
          public void SearchText_WhenSimple5()
          {
               int simple = 2, custom = 4;
               int expected = custom;
               string searchedText = "2", ip = "2.1.2.1", user = "ex";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SearchTest(searchedInfo);
               _testFileManager.FileDelete();
          }

          #region Factory
          private FilterViewModel SearchTest(SearchedLogFileInfo info, int fileCount = 1)
          {
               _testFileManager.CreateCustomLogFileWithDate(info.SimpleContent, info.CustomContent,
                    info.CustomIP, info.CustomUser, fileCount);

               var filterVM = CreateFilterVM(info.SearchedText);
               filterVM.SearchLogEvents();
               info.ValidateSearched(filterVM);
               return filterVM;
          }
          private class SearchedLogFileInfo
          {
               public int SimpleContent;
               public int CustomContent;
               public int ExpectedCountShowed;

               public string SearchedText;
               public string CustomIP;
               public string CustomUser;

               public SearchedLogFileInfo(int simpleContent, int customContent, int expectedCountShowed, string searchedText, string customIP, string customUser)
               {
                    SimpleContent = simpleContent;
                    CustomContent = customContent;
                    ExpectedCountShowed = expectedCountShowed;
                    SearchedText = searchedText;
                    CustomIP = customIP;
                    CustomUser = customUser;
               }

               public void ValidateSearched(FilterViewModel filterVM)
               {
                    ValidateStatistics(filterVM);
                    foreach (var item in filterVM.ObservebleLogEvents)
                         Assert.IsTrue(item.Message.Contains(SearchedText, StringComparison.OrdinalIgnoreCase));
               }
               private void ValidateStatistics(FilterViewModel filterVM)
               {
                    Assert.AreEqual(SimpleContent + CustomContent, filterVM.Statistics.AllLogEvent);
                    Assert.AreEqual(ExpectedCountShowed, filterVM.ShowedLogEventCount);
                    Assert.AreEqual(CustomContent, filterVM.Statistics.LoginSucceeded);
                    Assert.AreEqual(0, filterVM.Statistics.LoginFailure);
               }
          }
          #endregion
          #endregion

          #region SearchTextChangeDate
          [TestMethod]
          public void SearchTextChangeDate1()
          {
               int simple = 2, custom = 4; int expected = custom;
               string searchedText = "Ex", ip = "2.1.2.1", user = "ex";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               var filterVM = SearchTest(searchedInfo);

               simple = 1; custom = 2; expected = custom;
               searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SetDateSearchTest(searchedInfo, filterVM);
          }
          [TestMethod]
          public void SearchTextChangeDate2()
          {
               int simple = 1, custom = 0; int expected = custom;
               string searchedText = "1.2", ip = "2.1.2.1", user = "ex";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               var filterVM = SearchTest(searchedInfo);

               simple = 4; custom = 1; expected = custom;
               searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SetDateSearchTest(searchedInfo, filterVM);
          }
          [TestMethod]
          public void SearchTextChangeDate3()
          {
               int simple = 3, custom = 3; int expected = custom;
               string searchedText = "X", ip = "2.1.2.1", user = "ex";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               var filterVM = SearchTest(searchedInfo);

               simple = 0; custom = 10; expected = custom;
               searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SetDateSearchTest(searchedInfo, filterVM);
          }
          [TestMethod]
          public void SearchTextChangeDate4()
          {
               int simple = 2, custom = 0; int expected = 0;
               string searchedText = "hj", ip = "2.1.2.1", user = "ex";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               var filterVM = SearchTest(searchedInfo);

               simple = 0; custom = 1; expected = 0;
               searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SetDateSearchTest(searchedInfo, filterVM);
          }
          [TestMethod]
          public void SearchTextChangeDate5()
          {
               int simple = 2, custom = 0; int expected = simple;
               string searchedText = string.Empty, ip = "2.1.2.1", user = "ex";

               SearchedLogFileInfo searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               var filterVM = SearchTest(searchedInfo);

               simple = 5; custom = 0; expected = simple;
               searchedInfo = new(simple, custom, expected, searchedText, ip, user);
               SetDateSearchTest(searchedInfo, filterVM);
          }

          #region Factory
          private FilterViewModel SetDateSearchTest(SearchedLogFileInfo info, FilterViewModel filterVM)
          {
               _testFileManager.CreateCustomLogFileWithDate(info.SimpleContent, info.CustomContent,
                    info.CustomIP, info.CustomUser, 2);

               filterVM.SearchedText = info.SearchedText;
               filterVM.SetSelectedDate(CreateDate(-1));
               info.ValidateSearched(filterVM);
               return filterVM;
          }
          #endregion
          #endregion

          #region ReadNewLogs
          [TestMethod]
          public void ReadNewLogs_WhenFirstEmpty0()
          {
               var filterVM = FirstReadTest(0);
               SecondReadTest(filterVM, 0);
          }
          [TestMethod]
          public void ReadNewLogs_WhenFirstEmpty1()
          {
               var filterVM = FirstReadTest(0);
               SecondReadTest(filterVM, 1);
          }
          [TestMethod]
          public void ReadNewLogs_WhenFirstEmpty2()
          {
               var filterVM = FirstReadTest(0);
               SecondReadTest(filterVM, 5);
          }
          [TestMethod]
          public void ReadNewLogs_WhenOnlyFirst()
          {
               var filterVM = FirstReadTest(10);
               SecondReadTest(filterVM, 10);
          }
          [TestMethod]
          public void ReadNewLogs_WhenAddedLogs1()
          {
               var filterVM = FirstReadTest(10);
               SecondReadTest(filterVM, 11);
          }
          [TestMethod]
          public void ReadNewLogs_WhenAddedLogs2()
          {
               var filterVM = FirstReadTest(10);
               SecondReadTest(filterVM, 14);
          }
          [TestMethod]
          public void ReadNewLogs_When2AddedLogs1()
          {
               var filterVM = FirstReadTest(10);
               SecondReadTest(filterVM, 14);
               SecondReadTest(filterVM, 16);
          }
          [TestMethod]
          public void ReadNewLogs_When2AddedLogs2()
          {
               var filterVM = FirstReadTest(10);
               SecondReadTest(filterVM, 14);
               SecondReadTest(filterVM, 14);
          }
          private FilterViewModel FirstReadTest(int contentCount)
          {
               _testFileManager.CreateCustomLogFileWithDate(contentCount);
               var filterVM = CreateFilterVM();
               Assert.AreEqual(contentCount, filterVM.ShowedLogEventCount);
               return filterVM;
          }
          private void SecondReadTest(FilterViewModel filterVM, int contentCount)
          {
               _testFileManager.CreateCustomLogFileWithDate(contentCount);
               filterVM.ReadNewLogs();
               Assert.AreEqual(contentCount, filterVM.ShowedLogEventCount);
          }
          #endregion

          #region ReadNewLogsChangeToday
          [TestMethod]
          public void ReadNewLogs_WhenChangeToday_WhenFirst0_Second0()
          {
               var filterVM = FirstReadTest(0);
               var today = DateTime.Today.AddDays(1);
               filterVM.SetToday(today);
               SecondReadTest(filterVM, 0);
          }
          [TestMethod]
          public void ReadNewLogs_WhenChangeToday_WhenFirst0_Second3()
          {
               var filterVM = FirstReadTest(0);
               var today = DateTime.Today.AddDays(1);
               filterVM.SetToday(today);
               SecondReadTest(filterVM, 3);
          }
          [TestMethod]
          public void ReadNewLogs_WhenChangeToday_WhenFirst1_Second0()
          {
               var filterVM = FirstReadTest(1);
               var today = DateTime.Today.AddDays(1);
               filterVM.SetToday(today);
               SecondReadTest(filterVM, 0);
          }
          [TestMethod]
          public void ReadNewLogs_WhenChangeToday_WhenFirst1_Second1()
          {
               var filterVM = FirstReadTest(1);
               var today = DateTime.Today.AddDays(1);
               filterVM.SetToday(today);
               SecondReadTest(filterVM, 1);
          }
          [TestMethod]
          public void ReadNewLogs_WhenChangeToday_WhenFirst1_Second6()
          {
               var filterVM = FirstReadTest(1);
               var today = DateTime.Today.AddDays(1);
               filterVM.SetToday(today);
               SecondReadTest(filterVM, 6);
          }
          [TestMethod]
          public void ReadNewLogs_WhenChangeToday_WhenFirst3_Second0()
          {
               var filterVM = FirstReadTest(3);
               var today = DateTime.Today.AddDays(1);
               filterVM.SetToday(today);
               SecondReadTest(filterVM, 0);
          }
          [TestMethod]
          public void ReadNewLogs_WhenChangeToday_WhenFirst3_Second2()
          {
               var filterVM = FirstReadTest(3);
               var today = DateTime.Today.AddDays(1);
               filterVM.SetToday(today);
               SecondReadTest(filterVM, 2);
          }
          [TestMethod]
          public void ReadNewLogs_WhenChangeToday_WhenFirst6_Second7()
          {
               var filterVM = FirstReadTest(6);
               var today = DateTime.Today.AddDays(1);
               filterVM.SetToday(today);
               SecondReadTest(filterVM, 7);
          }

          #endregion
          #region Suport
          private FilterViewModel CreateFilterVM(string searchedText = "")
          {
               var logEventManager = _testFileManager.CreateLogEventManager();
               return new(logEventManager) { SearchedText = searchedText };
          }
          private DateTime CreateDate(int day)
          {
               return DateTime.Now.AddDays(day).Date;
          }
          #endregion
     }
}