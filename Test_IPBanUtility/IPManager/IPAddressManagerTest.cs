using Logic_IPBanUtility.Logic.IPList;
using Logic_IPBanUtility.Setting;

namespace Test_IPBanUtility.IPManager;

[TestClass]
public class IPAddressManagerTest
{
     private readonly IPAddressManagerTestFactory _factory;

     public IPAddressManagerTest()
     {
          TestIPBan _testIPBan = new("TestIP\\");
          SettingsBuilder sb = new();
          sb.CreateDefaultSettings(_testIPBan.CreateEmptyIPBan());
          _factory = new(sb.Settings!);
     }

     #region Add
     [TestMethod]
     public void Add_When1IP()
     {
          int expected = 1;
          var result = AddSimpleTest(expected);
          Assert.AreEqual(expected, result);
     }
     [TestMethod]
     public void Add_When2IP()
     {
          var expected = 2;
          var result = AddSimpleTest(expected);
          Assert.AreEqual(expected, result);
     }
     [TestMethod]
     public void Add_When3IP()
     {
          var expected = 3;
          var result = AddSimpleTest(expected);
          Assert.AreEqual(expected, result);
     }
     [TestMethod]
     public void Add_WhenIPExist()
     {
          var expected = 1;
          var result = 0;
          try
          {
               result = AddSimpleTest(expected);
               result = AddSimpleTest(expected, ClearOptions.None);
          }
          catch (ArgumentException)
          {
               Assert.AreEqual(expected, result);
          }
     }
     #region TestFactory
     private int AddSimpleTest(int count, ClearOptions clearOptions = ClearOptions.Before)
     {
          var manager = _factory.CreateManager();
          if (clearOptions == ClearOptions.Before) 
               manager.RemoveAll();

          var ips = _factory.CreateIP(count);
          foreach (var ip in ips)
               manager.Add(ip);

          manager.Update();
          return manager.IPAddress.Count;
     }
     #endregion
     #endregion

     #region Remove
     [TestMethod]
     public void Remove_Should0_WhenAdd1_Remove1()
     {
          int add = 1, remove = 1;
          int expectedLeft = add - remove;

          AddSimpleTest(add);
          var resultLeft = RemoveSimpleTest(remove);
          Assert.AreEqual(expectedLeft, resultLeft);
     }
     [TestMethod]
     public void Remove_Should0_WhenAdd3_Remove3()
     {
          int add = 3, remove = 3;
          int expectedLeft = add - remove;

          AddSimpleTest(add);
          var resultLeft = RemoveSimpleTest(remove);
          Assert.AreEqual(expectedLeft, resultLeft);
     }

     [TestMethod]
     public void Remove_Should1_WhenAdd2_Remove1()
     {
          int add = 2, remove = 1;
          int expectedLeft = add - remove;

          AddSimpleTest(add);
          var resultLeft = RemoveSimpleTest(remove);
          Assert.AreEqual(expectedLeft, resultLeft);
     }
     [TestMethod]
     public void Remove_Should1_WhenAdd5_Remove4()
     {
          int add = 5, remove = 4;
          int expectedLeft = add - remove;

          AddSimpleTest(add);
          var resultLeft = RemoveSimpleTest(remove);
          Assert.AreEqual(expectedLeft, resultLeft);
     }

     [TestMethod]
     public void Remove_Should2_WhenAdd3_Remove1()
     {
          int add = 3, remove = 1;
          int expectedLeft = add - remove;

          AddSimpleTest(add);
          var resultLeft = RemoveSimpleTest(remove);
          Assert.AreEqual(expectedLeft, resultLeft);
     }
     [TestMethod]
     public void Remove_Should2_WhenAdd5_Remove3()
     {
          int add = 5, remove = 3;
          int expectedLeft = add - remove;

          AddSimpleTest(add);
          var resultLeft = RemoveSimpleTest(remove);
          Assert.AreEqual(expectedLeft, resultLeft);
     }
     #region TestFactory
     private int RemoveSimpleTest(int count)
     {
          var manager = _factory.CreateManager();

          for (int id = 0; id < count; id++)
               manager.Remove(manager.IPAddress[id]);

          manager.Update();
          return manager.IPAddress.Count;
     }
     #endregion

     #endregion

     private class IPAddressManagerTestFactory
     {
          private readonly Settings _settings;

          public IPAddressManagerTestFactory(Settings settings)
          {
               _settings = settings;
          }

          public IPAddressManager CreateManager()
          {
               return new IPAddressManager(_settings);
          }
          public List<IPAddressEntity> CreateIP(int count)
          {
               List<IPAddressEntity> iPAddresses = new();
               for (int i = 0; i < count; i++)
                    iPAddresses.Add(new($"77.255.3.{i}", DateTime.Now, 0, DateTime.Now, DateTime.Now.AddYears(1), null));

               return iPAddresses;
          }
     }

     private enum ClearOptions
     {
          None,
          Before
     }
}