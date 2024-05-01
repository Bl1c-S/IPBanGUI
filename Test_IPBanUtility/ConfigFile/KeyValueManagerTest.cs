using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Models;
using Logic_IPBanUtility.Setting;
using Test_IPBanUtility;

namespace ConfigFile;

[TestClass]
public class KeyValueManagerTest
{
     private readonly string _ip = "127.0.0.1";
     private readonly KeyValueManager _keyManager;
     private readonly ConfigFileManager _cfgManager;

     public KeyValueManagerTest()
     {
          TestIPBan _testIPBan = new("TestCfg\\");
          SettingsBuilder sb = new();
          sb.CreateDefaultSettings(_testIPBan.CreateEmptyIPBan());
          _cfgManager = new ConfigFileManager(sb.Settings!, new());
          _keyManager = new(_cfgManager);
     }

     #region AddIpToKey
     [TestMethod]
     public void AddIpToKey_WhenEmpty()
     {
          var testingIPs = "";
          var expectedIPs = $"{_ip}";

          AddTest(testingIPs, expectedIPs);
     }

     [TestMethod]
     public void AddIpToKey_When1Exist()
     {
          var testingIPs = "1.1.1.1";
          var expectedIPs = $"{testingIPs}, {_ip}";

          AddTest(testingIPs, expectedIPs);
     }
     [TestMethod]
     public void AddIpToKey_When2Exist()
     {
          var testingIPs = "1.1.1.1, 32.21.32.1";
          var expectedIPs = $"{testingIPs}, {_ip}";

          AddTest(testingIPs, expectedIPs);
     }
     #endregion

     #region RemoveIpFromKey
     [TestMethod]
     public void RemoveIpFromKey_WhenIP_Empty()
     {
          var testingIPs = "";
          var expectedIPs = "";

          RemoveTest(testingIPs, expectedIPs);
     }
     [TestMethod]
     public void RemoveIpFromKey_WhenIP_Solo()
     {
          var testingIPs = _ip;
          var expectedIPs = "";

          RemoveTest(testingIPs, expectedIPs);
     }
     [TestMethod]
     public void RemoveIpFromKey_WhenIP_inStart()
     {
          var testIPs = "1.1.1.1";
          var testingIPs = $"{_ip}, {testIPs}";
          var expectedIPs = $"{testIPs}";

          RemoveTest(testingIPs, expectedIPs);
     }
     [TestMethod]
     public void RemoveIpFromKey_WhenIP_inLongStart()
     {
          var testIPs = "1.1.1.1";
          var testingIPs = $"{_ip}, {testIPs}, {testIPs}, {testIPs}";
          var expectedIPs = $"{testIPs}, {testIPs}, {testIPs}";

          RemoveTest(testingIPs, expectedIPs);
     }
     [TestMethod]
     public void RemoveIpFromKey_WhenIP_inEnd()
     {
          var testIPs = "1.1.1.1";
          var testingIPs = $"{testIPs}, {_ip}";
          var expectedIPs = $"{testIPs}";

          RemoveTest(testingIPs, expectedIPs);
     }
     [TestMethod]
     public void RemoveIpFromKey_WhenIP_inLondEnd()
     {
          var testIPs = "1.1.1.1";
          var testingIPs = $"{testIPs}, {testIPs}, {testIPs}, {_ip}";
          var expectedIPs = $"{testIPs}, {testIPs}, {testIPs}";

          RemoveTest(testingIPs, expectedIPs);
     }
     [TestMethod]
     public void RemoveIpFromKey_WhenIP_inMiddle()
     {
          var testIPs = "1.1.1.1";
          var testingIPs = $"{testIPs}, {_ip}, {testIPs}";
          var expectedIPs = $"{testIPs}, {testIPs}";

          RemoveTest(testingIPs, expectedIPs);
     }
     #endregion

     #region TestFactory
     private void AddTest(string testingIPs, string expectedIPs, KeyNames name = KeyNames.Whitelist)
     {
          SetInitialKeyValues(name, testingIPs);
          _keyManager.AddIpToKey(name, _ip);
          AssertKeyValues(name, expectedIPs);
     }
     private void RemoveTest(string testingIPs, string expectedIPs, KeyNames name = KeyNames.Whitelist)
     {
          SetInitialKeyValues(name, testingIPs);
          _keyManager.RemoveIpFromKey(name, _ip);
          AssertKeyValues(name, expectedIPs);
     }

     private void SetInitialKeyValues(KeyNames keyName, string initialValues)
     {
          var key = _cfgManager.GetKey(keyName);
          key.SetValue(initialValues);
          _cfgManager.WriteKey(key);
     }

     // Метод для перевірки результату
     private void AssertKeyValues(KeyNames keyName, string expectedValue)
     {
          var key = _cfgManager.GetKey(keyName);
          Assert.IsTrue(key.Value == expectedValue);
     }
     #endregion
}
