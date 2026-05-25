using Logic_IPBanUtility;
using Logic_IPBanUtility.Interfaces.Services;
using Logic_IPBanUtility.Setting;
using System.Collections.Generic;
using WPF_IPBanUtility.Base;

namespace WPF_IPBanUtility;

public class SettingsVMsBuilder
{
     private readonly Settings _settings;
     private readonly ConfigFileManager _configFileManager;
     private readonly IWinServicesController _controller;

     public SettingsVMsBuilder(Settings settings, ConfigFileManager configFileManager, IWinServicesController controller)
     {
          _settings = settings;
          _configFileManager = configFileManager;
          _controller = controller;
     }

     public List<SettingsComponentViewModelBase> Build()
     {
          List<SettingsComponentViewModelBase> VMs = new()
          {
               new KeysVisibilityControllerViewModel(_configFileManager),
               new WinServicesViewModel(_controller),
               new ClearLogsViewModel(_settings)
          };
          return VMs;
     }
}
