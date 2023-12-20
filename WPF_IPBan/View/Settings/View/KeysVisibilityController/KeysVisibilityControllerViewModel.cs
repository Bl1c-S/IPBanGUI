using Logic_IPBanUtility;
using Logic_IPBanUtility.Models;
using System.Collections.Generic;

namespace WPF_IPBanUtility;

internal class KeysVisibilityControllerViewModel : ISettingsVMComponent
{
     private ConfigFileManager _configFileManager;
     public List<KeyIdenti> KeyIndentis { get; set; }

     public KeysVisibilityControllerViewModel(ConfigFileManager configFileManager)
     {
          _configFileManager = configFileManager;
          KeyIndentis = configFileManager.ReadKeyIndentis();
     }
     public void Save()
     {
          foreach (var key in KeyIndentis)
               _configFileManager.WriteKeyIdentiChanged(key);
     }
}
