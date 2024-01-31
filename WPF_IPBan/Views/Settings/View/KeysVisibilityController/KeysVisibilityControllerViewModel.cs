using Logic_IPBanUtility;
using Logic_IPBanUtility.Models;
using System.Collections.Generic;
using WPF_IPBanUtility.Base;

namespace WPF_IPBanUtility;

internal class KeysVisibilityControllerViewModel : SettingsComponentViewModelBase
{
     private ConfigFileManager _configFileManager;
     public List<KeyIdenti> KeyIndentis { get; set; }

     public KeysVisibilityControllerViewModel(ConfigFileManager configFileManager)
     {
          Title = Properties.PageNames.KeysVisibilityControllerViewTitle;
          _configFileManager = configFileManager;
          KeyIndentis = configFileManager.ReadKeyIndentis();
     }
     public override void Save()
     {
          foreach (var key in KeyIndentis)
               _configFileManager.WriteKeyIdentiChanged(key);
     }
}
