using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Setting;

namespace Test_IPBanUtility.LogEvent.ManagerTest;

[TestClass]
public class LogEventManagerLogicTest
{
     SettingsBuilder settingsBuilder = new();
     LogEventManager LE_Manager;

     public LogEventManagerLogicTest()
     {
          var ipb = IPBan.Create("C:\\Program Files\\IPBan");
          settingsBuilder.CreateDefaultSettings(ipb);
          settingsBuilder.LoadSettings();
          LE_Manager = new(settingsBuilder.Settings!, new())
;     }

     [TestMethod]
     public void Test1()
     {
          var logEvents = LE_Manager.ReadAll();
     }
}
