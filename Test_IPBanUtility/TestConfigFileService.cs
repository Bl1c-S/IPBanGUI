using Logic_IPBanUtility;

namespace Test_IPBanUtility;

[TestClass]
public class TestConfigFileService
{
     string directoryPath = "C:\\Program Files\\IPBan";
     private ConfigFileManager cfgManager;

     public TestConfigFileService()
     {
          cfgManager = new(directoryPath);
     }

     [TestMethod]
     public void Test1()
     {
          //contextManager.GetValue("BanTime");
          //contextManager.InsertValue("BanTime", "00:01:00:00");
     }
}
