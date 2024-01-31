using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Setting;

namespace Test_IPBanUtility.LogEvent.ManagerTest;

[TestClass]
public class LogEventManagerLogicTest
{
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
          LE_Manager = new(settingsBuilder.Settings!, new())
;     }
}
