using Moq;
using System.ServiceProcess;
using Logic_IPBanUtility;
using Logic_IPBanUtility.Interfaces.Services;
using Logic_IPBanUtility.Models;
using Logic_IPBanUtility.Setting;
using WPF_IPBanUtility;
using static Logic_IPBanUtility.Services.WinServicesController;

namespace Test_WPFIPBan.KeyList
{
     [TestClass]
     public class KeyListViewModelTests
     {
          private Mock<IFileManager> _fileManagerMock;
          private Mock<IWinServicesController> _servicesControllerMock;
          private Mock<IServiceManager> _serviceManagerMock;

          private Settings _settings;
          private List<KeyIdenti> fakeList;

          [TestInitialize]
          public void Setup()
          {
               _fileManagerMock = new Mock<IFileManager>();
               _servicesControllerMock = new Mock<IWinServicesController>();
               _serviceManagerMock = new Mock<IServiceManager>();

               fakeList = new List<KeyIdenti>
               {
                     new KeyIdenti(true, "Blacklist"),
                     new KeyIdenti(false, "Whitelist")
               };

               var fakeContext = new List<string>
               {
                   "<add key=\"Blacklist\" value=\"1.1.1.1\"/>",
                   "<add key=\"Whitelist\" value=\"2.2.2.2\"/>"
               };

               _fileManagerMock.Setup(f => f.GetJson<List<KeyIdenti>>(It.IsAny<string>()))
                   .Returns(() => fakeList.Select(x => new KeyIdenti(x.IsHidden, x.Name)).ToList()); //return always new List

               _fileManagerMock.Setup(f => f.ReadAllLines(It.IsAny<string>()))
                   .Returns(fakeContext);


               _fileManagerMock.Setup(f => f.ReadAllLines(It.IsAny<string>()))
                   .Returns(fakeContext);

               //moq paths
               var baseDir = AppDomain.CurrentDomain.BaseDirectory;
               var config = new Config(baseDir, Path.Combine(baseDir, "setting.json"), Path.Combine(baseDir, "keys.json"));
               var ipban = new IPBan(baseDir, Path.Combine(baseDir, "fake_log.log"));

               _settings = new Settings(config, ipban);


               //moq controller
               _serviceManagerMock.Setup(m => m.CheckExists(It.IsAny<string>())).Returns(true);
               _serviceManagerMock.Setup(m => m.GetStatus(It.IsAny<string>())).Returns(ServiceControllerStatus.Running);

               var realService = new Service("IPBAN", _serviceManagerMock.Object);
               _servicesControllerMock.Setup(s => s.IPBan).Returns(realService);
               var currentStatus = ServiceControllerStatus.Running;

               _serviceManagerMock.Setup(m => m.GetStatus(It.IsAny<string>()))
                   .Returns(() => currentStatus);

               _serviceManagerMock.Setup(m => m.Stop(It.IsAny<string>()))
                   .Callback(() => currentStatus = ServiceControllerStatus.Stopped);

               _serviceManagerMock.Setup(m => m.Start(It.IsAny<string>()))
                   .Callback(() => currentStatus = ServiceControllerStatus.Running);

          }

          [TestMethod]
          public void Constructor_ShouldCreateViewModels_OnlyForHiddenKeys()
          {
               RunInSTA(() =>
               {
                    var cfgManager = new ConfigFileManager(_settings, _fileManagerMock.Object);
                    var viewModel = new KeyListViewModel(cfgManager, _servicesControllerMock.Object);

                    Assert.AreEqual(1, viewModel.KeyViewModels.Count);
                    Assert.AreEqual("Blacklist", viewModel.KeyViewModels.First().Key.KeyIdenti.Name);
               });
          }
          [TestMethod]
          public void HideKey_ShouldRemoveViewModel_AndRewriteJson()
          {
               RunInSTA(() =>
               {
                    fakeList.First().IsHidden = true;

                    var cfgManager = new ConfigFileManager(_settings, _fileManagerMock.Object);
                    var viewModel = new KeyListViewModel(cfgManager, _servicesControllerMock.Object);

                    var data = cfgManager.ReadKeyIndentis();
                    data.First(x => x.Name == "Blacklist").IsHidden = true;

                    var targetVM = viewModel.KeyViewModels.First();
                    viewModel.HideKey(targetVM);

                    _fileManagerMock.Verify(f => f.SaveJson(It.IsAny<string>(), It.IsAny<List<KeyIdenti>>()), Times.Once);
               });
          }

          [TestMethod]
          public void SaveAllCommand_WhenExecuted_ShouldWriteToConfig()
          {
               RunInSTA(() =>
               {
                    var cfgManager = new ConfigFileManager(_settings, _fileManagerMock.Object);
                    var viewModel = new KeyListViewModel(cfgManager, _servicesControllerMock.Object);
                    var keyVM = viewModel.KeyViewModels.First(); // Blacklist

                    keyVM.Value = "1.1.1.1, 192.168.1.1";

                    viewModel.ISaveAllCommand.Execute(null);

                    _fileManagerMock.Verify(f => f.WriteAllLines(
                         _settings.IPBan.Context,
                         It.Is<IEnumerable<string>>(lines => lines.Contains("<add key=\"Blacklist\" value=\"1.1.1.1, 192.168.1.1\"/>"))
                    ), Times.Once);
               });
          }

          [TestMethod]
          public void Dispose_WhenPageHaveChanges_ShouldRestartIPBanService()
          {
               RunInSTA(() =>
               {
                    var cfgManager = new ConfigFileManager(_settings, _fileManagerMock.Object);
                    var viewModel = new KeyListViewModel(cfgManager, _servicesControllerMock.Object);
                    viewModel.PageHaveChanges = true;

                    viewModel.Dispose();

                    _serviceManagerMock.Verify(m => m.Stop("IPBAN"), Times.Once);
                    _serviceManagerMock.Verify(m => m.Start("IPBAN"), Times.Once);
               });
          }

          private void RunInSTA(Action action) //Костиль :)
          {
               Exception? threadEx = null;
               Thread thread = new Thread(() =>
               {
                    try
                    {
                         // Имитируем контекст диспетчера WPF, если кнопкам он понадобится
                         System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() => { });
                         action();
                    }
                    catch (Exception ex)
                    {
                         threadEx = ex;
                    }
               });

               thread.SetApartmentState(ApartmentState.STA); // Жестко задаем STA-поток
               thread.Start();
               thread.Join(); // Ждем завершения потока

               if (threadEx != null)
               {
                    // Если внутри теста что-то упало (например, Assert не сошелся), прокидываем ошибку наружу
                    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(threadEx).Throw();
               }
          }
     }
}