using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Logic.IPList;
using Logic_IPBanUtility.Models;
using Test_IPBanUtility;
using WPF_IPBanUtility;

namespace IpVMsTest;

[STATestClass]
public class IPListViewModelTest
{
     private IpBanTestHelper _helper = null!;
     private ConfigFileManager _cfgManager = null!;
     private KeyValueManager _keyManager = null!;

     [TestInitialize]
     public void Setup()
     {
          EnsureApplication();
          _helper = new IpBanTestHelper();
          _cfgManager = new ConfigFileManager(_helper.Settings, new());
          _keyManager = new KeyValueManager(_cfgManager);
     }

     private static void EnsureApplication()
     {
          if (System.Windows.Application.Current == null)
          {
               var app = new App();
               app.InitializeComponent();
          }
     }

     #region Helpers
     private WhiteListViewModel CreateWhiteListVM(params string[] ips)
     {
          _keyManager.RemoveAllIpsFromKey(KeyNames.Whitelist);
          foreach (var ip in ips)
               _keyManager.AddIpToKey(KeyNames.Whitelist, ip);

          return new WhiteListViewModel(_keyManager, new(true));
     }

     private BlackListViewModel CreateBlackListVM(params string[] ips)
     {
          _keyManager.RemoveAllIpsFromKey(KeyNames.Blacklist);
          foreach (var ip in ips)
               _keyManager.AddIpToKey(KeyNames.Blacklist, ip);

          return new BlackListViewModel(_keyManager, new(true));
     }

     private IpBlockedListService CreateBlockedService()
     {
          var context = new IPAddressesDbContext(_helper.IpBan.Sqlite, createIfMissing: true);
          context.Database.EnsureCreated();

          var blockedService = new IpBlockedListService(_keyManager, _helper.Settings);
          blockedService.RemoveAll();
          return blockedService;
     }
     #endregion

     #region WhiteListViewModel
     [TestMethod]
     public void WhiteListViewModel_ClearAll_ClearsItemsAndUpdatesProperties()
     {
          var whiteListVM = CreateWhiteListVM("1.1.1.1");

          Assert.AreEqual(1, whiteListVM.ItemCount);
          Assert.IsTrue(whiteListVM.HasItems);
          Assert.IsTrue(whiteListVM.IClearAllCommand.CanExecute(null));

          whiteListVM.IClearAllCommand.Execute(null);

          Assert.AreEqual(0, whiteListVM.ItemCount);
          Assert.IsFalse(whiteListVM.HasItems);
          Assert.IsFalse(whiteListVM.IClearAllCommand.CanExecute(null));
     }

     [TestMethod]
     public void WhiteListViewModel_ClearAll_WhenEmpty_DoesNothing()
     {
          var whiteListVM = CreateWhiteListVM();

          Assert.AreEqual(0, whiteListVM.ItemCount);
          Assert.IsFalse(whiteListVM.HasItems);
          Assert.IsFalse(whiteListVM.IClearAllCommand.CanExecute(null));

          whiteListVM.ClearAll();

          Assert.AreEqual(0, whiteListVM.ItemCount);
          Assert.IsFalse(whiteListVM.HasItems);
     }
     #endregion

     #region BlackListViewModel
     [TestMethod]
     public void BlackListViewModel_ClearAll_ClearsItemsAndUpdatesProperties()
     {
          var blackListVM = CreateBlackListVM("2.2.2.2");

          Assert.AreEqual(1, blackListVM.ItemCount);
          Assert.IsTrue(blackListVM.HasItems);
          Assert.IsTrue(blackListVM.IClearAllCommand.CanExecute(null));

          blackListVM.IClearAllCommand.Execute(null);

          Assert.AreEqual(0, blackListVM.ItemCount);
          Assert.IsFalse(blackListVM.HasItems);
          Assert.IsFalse(blackListVM.IClearAllCommand.CanExecute(null));
     }

     [TestMethod]
     public void BlackListViewModel_ClearAll_WhenEmpty_DoesNothing()
     {
          var blackListVM = CreateBlackListVM();

          Assert.AreEqual(0, blackListVM.ItemCount);
          Assert.IsFalse(blackListVM.HasItems);
          Assert.IsFalse(blackListVM.IClearAllCommand.CanExecute(null));

          blackListVM.ClearAll();

          Assert.AreEqual(0, blackListVM.ItemCount);
          Assert.IsFalse(blackListVM.HasItems);
     }
     #endregion

     #region IPBlockedListViewModel
     [TestMethod]
     public void IPBlockedListViewModel_ClearAll_ClearsItemsAndUpdatesProperties()
     {
          var blockedService = CreateBlockedService();
          blockedService.Add(new IPAddressEntity("3.3.3.3", null, 0, null, null, null));

          var blockedVM = new IPBlockedListViewModel(blockedService, new(true), _ => { });

          Assert.AreEqual(1, blockedVM.ItemCount);
          Assert.IsTrue(blockedVM.HasItems);
          Assert.IsTrue(blockedVM.IClearAllCommand.CanExecute(null));

          blockedVM.IClearAllCommand.Execute(null);

          Assert.AreEqual(0, blockedVM.ItemCount);
          Assert.IsFalse(blockedVM.HasItems);
          Assert.IsFalse(blockedVM.IClearAllCommand.CanExecute(null));
     }

     [TestMethod]
     public void IPBlockedListViewModel_ClearAll_WhenEmpty_DoesNothing()
     {
          var blockedService = CreateBlockedService();
          var blockedVM = new IPBlockedListViewModel(blockedService, new(true), _ => { });

          Assert.AreEqual(0, blockedVM.ItemCount);
          Assert.IsFalse(blockedVM.HasItems);
          Assert.IsFalse(blockedVM.IClearAllCommand.CanExecute(null));

          blockedVM.ClearAll();

          Assert.AreEqual(0, blockedVM.ItemCount);
          Assert.IsFalse(blockedVM.HasItems);
     }
     #endregion
}
