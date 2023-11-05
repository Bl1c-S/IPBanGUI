using Logic_IPBanUtility;

namespace Test_IPBanUtility;

[TestClass]
public class TestConfigFileService
{
     string directoryPath = "C:\\Program Files\\IPBan";
     private ConfigContextManager cfgService;

     public TestConfigFileService()
     {
          var cfgManager = new ConfigFileManager(directoryPath);
          cfgService = cfgManager.CreateConfigContextManager();
     }

     [TestMethod]
     public void Test1()
     {
          cfgService.GetValue("BanTime");
          cfgService.InsertValue("BanTime", "00:01:00:00");
     }
}
