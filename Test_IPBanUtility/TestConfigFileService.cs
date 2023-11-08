using Logic_IPBanUtility;

namespace Test_IPBanUtility;

[TestClass]
public class TestConfigFileService
{
     string directoryPath = "C:\\Program Files\\IPBan";
     private ConfigContextService contextManager;

     public TestConfigFileService()
     {
          contextManager = new ConfigContextService(directoryPath);
     }

     [TestMethod]
     public void Test1()
     {
          //contextManager.GetValue("BanTime");
          //contextManager.InsertValue("BanTime", "00:01:00:00");
     }
}
