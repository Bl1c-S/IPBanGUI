using Logic_IPBanUtility.Logic.IPList.Services;
using Logic_IPBanUtility.Setting;

namespace Test_IPBanUtility.IPManager;

[TestClass]
public class UnBanServiceTest
{
     private readonly UnBanService _unBanService;
     public UnBanServiceTest()
     {
          TestIPBan _testIPBan = new("TestIP\\");
          SettingsBuilder sb = new();
          sb.CreateDefaultSettings(_testIPBan.CreateEmptyIPBan());
          _unBanService = new(sb.Settings!);
     }

     [TestMethod]
     public void Test()
     {

     }
}