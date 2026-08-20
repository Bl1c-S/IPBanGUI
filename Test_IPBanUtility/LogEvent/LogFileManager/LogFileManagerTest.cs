using Test_IPBanUtility;

namespace LogEventTests;

[TestClass]
public class LogFileManagerTest
{
     private readonly string _currentTestLog = "2022-02-22 22:22:22.2222|22|22|Login succeeded, address: 2.2.2.2, user name: ";
     private readonly string _badTestLog = "2022-02-22 22:22:22.2222|22|22|";

     private readonly IpBanTestHelper _ipBanTestHelper;
     private readonly Logic_IPBanUtility.Logic.LogFile.Services.LogFileManager _logFileManager;

     public LogFileManagerTest()
     {
          _ipBanTestHelper = new();
          _logFileManager = new(_ipBanTestHelper.IpBan.LogfilePath);
     }
     #region TestReadAllLogEvents

     [TestMethod]
     public void ReadAllLogEvents_WhenCurrentLog1()
     {
          CreateTestFile_When1Current();
          var result = _logFileManager.ReadNewLogEvents();
          Assert.AreEqual(1, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName0));
          Assert.IsTrue(result[0].Id == 1);
     }
     [TestMethod]
     public void ReadAllLogEvents_WhenCurrentLog2()
     {
          CreateTestFile_When2Current();
          var result = _logFileManager.ReadNewLogEvents();
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
          var result = _logFileManager.ReadNewLogEvents();
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
          var result = _logFileManager.ReadNewLogEvents();
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
          var result = _logFileManager.ReadNewLogEvents();
          Assert.AreEqual(1, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName1));
          Assert.IsTrue(result[0].Id == 1);
     }
     [TestMethod]
     public void ReadAllLogEvents_WhenBadLog1()
     {
          CreateTestFile_When1Bad();
          var result = _logFileManager.ReadNewLogEvents();
          Assert.AreEqual(0, result.Count);
     }
     [TestMethod]
     public void ReadAllLogEvents_WhenEmptyLog()
     {
          CreateTestFile_WhenEmpty(); 
          var result = _logFileManager.ReadNewLogEvents();
          Assert.AreEqual(0, result.Count);
     }
     #endregion

     #region TestReadNewLogEvents

     [TestMethod]
     public void ReadNewLogEvents_WhenNotNew()
     {
          CreateTestFile_When1Current();
          _logFileManager.ReadNewLogEvents();
          CreateTestFile_When1Current();
          var result = _logFileManager.ReadNewLogEvents();
          Assert.AreEqual(0, result.Count);
     }

     [TestMethod]
     public void ReadNewLogEvents_WhenNew1()
     {
          CreateTestFile_When1Current();
          _logFileManager.ReadNewLogEvents();
          CreateTestFile_When2Current();
          var result = _logFileManager.ReadNewLogEvents();

          Assert.AreEqual(1, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName1));
          Assert.IsTrue(result[0].Id == 2);
     }

     [TestMethod]
     public void ReadNewLogEvents_WhenNew2()
     {
          CreateTestFile_When1Current();
          _logFileManager.ReadNewLogEvents();
          CreateTestFile_When3Current();
          var result = _logFileManager.ReadNewLogEvents();

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
          _logFileManager.ReadNewLogEvents();
          CreateTestFile_When3Current();
          var result = _logFileManager.ReadNewLogEvents();

          Assert.AreEqual(1, result.Count);
          Assert.IsTrue(result[0].Message.Contains(_exspectedName2));
          Assert.IsTrue(result[0].Id == 3);
     }

     [TestMethod]
     public void ReadNewLogEvents_When3NotNew()
     {
          CreateTestFile_When3Current();
          _logFileManager.ReadNewLogEvents();
          var result = _logFileManager.ReadNewLogEvents();

          Assert.AreEqual(0, result.Count);
     }

     [TestMethod]
     public void ReadNewLogEvents_WhenBad3NotNew()
     {
          CreateTestFile_When3Bad();
          _logFileManager.ReadNewLogEvents();
          var result = _logFileManager.ReadNewLogEvents();

          Assert.AreEqual(0, result.Count);
     }

     [TestMethod]
     public void ReadNewLogEvents_WhenBad3New1()
     {
          CreateTestFile_When1Current();
          _logFileManager.ReadNewLogEvents();
          CreateTestFile_When3Bad();
          var result = _logFileManager.ReadNewLogEvents();

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
          WriteLogs(testLogs);
     }

     private void CreateTestFile_When2Current()
     {
          string[] testLogs ={
               _currentTestLog + _logTestName0,
               _currentTestLog + _logTestName1,
          };
          WriteLogs(testLogs);
     }

     private void CreateTestFile_When3Current()
     {
          string[] testLogs ={
               _currentTestLog + _logTestName0,
               _currentTestLog +_logTestName1,
               _currentTestLog + _logTestName2,
          };
          WriteLogs(testLogs);
     }

     private void CreateTestFile_When3Bad()
     {
          string[] testLogs ={
               _currentTestLog + _logTestName0,
               _badTestLog + "23er",
               _currentTestLog + _logTestName1,
          };
          WriteLogs(testLogs);
     }

     private void CreateTestFile_When2Bad()
     {
          string[] testLogs ={
               _badTestLog + "23er",
               _currentTestLog +_logTestName1,
          };
          WriteLogs(testLogs);
     }

     private void CreateTestFile_When1Bad()
     {
          string[] testLogs ={
              _badTestLog + "23er",
          };
          WriteLogs(testLogs);
     }

     private void CreateTestFile_WhenEmpty()
     {
          string[] testLogs = { };
          WriteLogs(testLogs);
     }
     
     private void WriteLogs(string[] testLogs)
     {
          File.WriteAllLines(_ipBanTestHelper.IpBan.LogfilePath, testLogs);
     }
     #endregion
}
