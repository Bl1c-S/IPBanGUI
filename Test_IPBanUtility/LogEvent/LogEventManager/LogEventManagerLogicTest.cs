using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Setting;
using static System.Net.Mime.MediaTypeNames;

namespace Test_IPBanUtility.LogEventTest.ManagerTest;

[TestClass]
public class LogEventManagerLogicTest
{
     string _currentTestLog = "2022-02-22 22:22:22.2222|22|22|Login succeeded, address: 2.2.2.2, user name: ";
     string _badTestLog = "2022-02-22 22:22:22.2222|22|22|";

     SettingsBuilder settingsBuilder = new();
     LogEventManager LE_Manager;
     string _logFilePath;

     public LogEventManagerLogicTest()
     {
          var ipb = IPBan.Create("C:\\Program Files\\IPBan");
          var programFolder = AppDomain.CurrentDomain.BaseDirectory;
          _logFilePath = $"{programFolder}\\TestLogs\\LogEventManagerTest.txt";
          ipb.Logfile = _logFilePath;
          settingsBuilder.CreateDefaultSettings(ipb);
          settingsBuilder.LoadSettings();
          LE_Manager = new(settingsBuilder.Settings!, new());
     }

     #region TestReadAllLogEvents

     [TestMethod]
     public void ReadAllLogEvents_WhenCurrentLog1()
     {
          CreateTestFile_When1Current();
          var result = LE_Manager.ReadAllLogEvents();
          Assert.AreEqual(1, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName0));
          Assert.IsTrue(result[0].Id == 1);
     }
     [TestMethod]
     public void ReadAllLogEvents_WhenCurrentLog2()
     {
          CreateTestFile_When2Current();
          var result = LE_Manager.ReadAllLogEvents();
          Assert.AreEqual(2, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName0));
          Assert.IsTrue(result[0].Id == 1);
          Assert.IsTrue(result[1].Message.Contains(_exspectedName1));
          Assert.IsTrue(result[1].Id == 2);
     }
     [TestMethod]
     public void ReadAllLogEvents_WhenCurrentLog3()
     {
          CreateTestFile_When3Current();
          var result = LE_Manager.ReadAllLogEvents();
          Assert.AreEqual(3, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName0));
          Assert.IsTrue(result[0].Id == 1);
          Assert.IsTrue(result[1].Message.Contains(_exspectedName1));
          Assert.IsTrue(result[1].Id == 2);
          Assert.IsTrue(result[2].Message.Contains(_exspectedName2));
          Assert.IsTrue(result[2].Id == 3);

     }
     [TestMethod]
     public void ReadAllLogEvents_WhenBadLog3()
     {
          CreateTestFile_When3Bad();
          var result = LE_Manager.ReadAllLogEvents();
          Assert.AreEqual(2, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName0));
          Assert.IsTrue(result[0].Id == 1);
          Assert.IsTrue(result[1].Message.Contains(_exspectedName1));
          Assert.IsTrue(result[1].Id == 2);
     }
     [TestMethod]
     public void ReadAllLogEvents_WhenBadLog2()
     {
          CreateTestFile_When2Bad();
          var result = LE_Manager.ReadAllLogEvents();
          Assert.AreEqual(1, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName1));
          Assert.IsTrue(result[0].Id == 1);
     }
     [TestMethod]
     public void ReadAllLogEvents_WhenBadLog1()
     {
          CreateTestFile_When1Bad();
          var result = LE_Manager.ReadAllLogEvents();
          Assert.AreEqual(0, result.Count);
     }
     [TestMethod]
     public void ReadAllLogEvents_WhenEmptyLog()
     {
          CreateTestFile_WhenEmpty();
          var result = LE_Manager.ReadAllLogEvents();
          Assert.AreEqual(0, result.Count);
     }
     #endregion

     #region TestReadAllLogEvents

     [TestMethod]
     public void ReadNewLogEvents_WhenNotNew()
     {
          CreateTestFile_When1Current();
          LE_Manager.ReadAllLogEvents();
          var result = LE_Manager.ReadNewLogEvents();

          Assert.AreEqual(0, result.Count);
     }

     [TestMethod]
     public void ReadNewLogEvents_WhenNew1()
     {
          CreateTestFile_When1Current();
          LE_Manager.ReadAllLogEvents();
          CreateTestFile_When2Current();
          var result = LE_Manager.ReadNewLogEvents();

          Assert.AreEqual(1, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName1));
          Assert.IsTrue(result[0].Id == 2);
     }

     [TestMethod]
     public void ReadNewLogEvents_WhenNew2()
     {
          CreateTestFile_When1Current();
          LE_Manager.ReadAllLogEvents();
          CreateTestFile_When3Current();
          var result = LE_Manager.ReadNewLogEvents();

          Assert.AreEqual(2, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName1));
          Assert.IsTrue(result[0].Id == 2);
          Assert.IsTrue(result[1].Message.Contains(_exspectedName2));
          Assert.IsTrue(result[1].Id == 3);
     }

     [TestMethod]
     public void ReadNewLogEvents_When2New1()
     {
          CreateTestFile_When2Current();
          LE_Manager.ReadAllLogEvents();
          CreateTestFile_When3Current();
          var result = LE_Manager.ReadNewLogEvents();

          Assert.AreEqual(1, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName2));
          Assert.IsTrue(result[0].Id == 3);
     }

     [TestMethod]
     public void ReadNewLogEvents_When3NotNew()
     {
          CreateTestFile_When3Current();
          LE_Manager.ReadAllLogEvents();
          var result = LE_Manager.ReadNewLogEvents();

          Assert.AreEqual(0, result.Count);
     }

     [TestMethod]
     public void ReadNewLogEvents_WhenBad3NotNew()
     {
          CreateTestFile_When3Bad();
          LE_Manager.ReadAllLogEvents();
          var result = LE_Manager.ReadNewLogEvents();

          Assert.AreEqual(0, result.Count);
     }

     [TestMethod]
     public void ReadNewLogEvents_WhenBad3New1()
     {
          CreateTestFile_When1Current();
          LE_Manager.ReadAllLogEvents();
          CreateTestFile_When3Bad();
          var result = LE_Manager.ReadNewLogEvents();

          Assert.AreEqual(1, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName1));
          Assert.IsTrue(result[0].Id == 2);
     }

     #endregion

     #region PrivateSupportTestMethods

     string _exspectedName0 = "Test0";
     string _exspectedName1 = "Test1";
     string _exspectedName2 = "Test2";

     string _logTestName0 = "Test0,";
     string _logTestName1 = "Test1 ,";
     string _logTestName2 = "Test2 , 31";

     private void CreateTestFile_When1Current()
     {
          string[] testLogs ={
               _currentTestLog + _logTestName0,
          };
          File.WriteAllLines(_logFilePath, testLogs);
     }
     private void CreateTestFile_When2Current()
     {
          string[] testLogs ={
               _currentTestLog + _logTestName0,
               _currentTestLog + _logTestName1,
          };
          File.WriteAllLines(_logFilePath, testLogs);
     }
     private void CreateTestFile_When3Current()
     {
          string[] testLogs ={
               _currentTestLog + _logTestName0,
               _currentTestLog +_logTestName1,
               _currentTestLog + _logTestName2,
          };
          File.WriteAllLines(_logFilePath, testLogs);
     }

     private void CreateTestFile_When3Bad()
     {
          string[] testLogs ={
               _currentTestLog + _logTestName0,
               _badTestLog + "23er",
               _currentTestLog + _logTestName1,
          };
          File.WriteAllLines(_logFilePath, testLogs);
     }
     private void CreateTestFile_When2Bad()
     {
          string[] testLogs ={
               _badTestLog + "23er",
               _currentTestLog +_logTestName1,
          };
          File.WriteAllLines(_logFilePath, testLogs);
     }
     private void CreateTestFile_When1Bad()
     {
          string[] testLogs ={
              _badTestLog + "23er",
          };
          File.WriteAllLines(_logFilePath, testLogs);
     }
     private void CreateTestFile_WhenEmpty()
     {
          string[] testLogs = { };
          File.WriteAllLines(_logFilePath, testLogs);
     }
     #endregion
}
