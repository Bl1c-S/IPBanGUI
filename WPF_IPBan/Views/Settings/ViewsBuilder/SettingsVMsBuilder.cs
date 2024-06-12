﻿using Logic_IPBanUtility;
using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;
using System.Collections.Generic;

namespace WPF_IPBanUtility;

public class SettingsVMsBuilder
{
     private readonly Settings _settings;
     private readonly ConfigFileManager _configFileManager;
     private readonly WinServicesController _controller;

     public SettingsVMsBuilder(Settings settings, ConfigFileManager configFileManager, WinServicesController controller)
     {
          _settings = settings;
          _configFileManager = configFileManager;
          _controller = controller;
     }

     public List<ISettingsVMComponent> Build()
     {
          List<ISettingsVMComponent> VMs = new()
          {
               new KeysVisibilityControllerViewModel(_configFileManager),
               new WinServicesViewModel(_controller),
               new ClearLogsViewModel(_settings)
          };
          return VMs;
     }
}
