using Logic_IPBanUtility;
using Logic_IPBanUtility.Setting;
using System.Collections.Generic;

namespace WPF_IPBanUtility;

public class SettingsVMsBuilder
{
     private Settings _settings;
     private ConfigFileManager _configFileManager;

     public SettingsVMsBuilder(Settings settings, ConfigFileManager configFileManager)
     {
          _settings = settings;
          _configFileManager = configFileManager;
     }

     public List<ISettingsVMComponent> Build()
     {
          List<ISettingsVMComponent> VMs = new()
          {
               new SelectFolderViewModel(_settings),
               new KeysVisibilityControllerViewModel(_configFileManager),
               new ClearLogsViewModel(_settings)
          };
          return VMs;
     }
}
