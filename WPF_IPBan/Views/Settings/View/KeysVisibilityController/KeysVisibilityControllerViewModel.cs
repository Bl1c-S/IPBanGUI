using Logic_IPBanUtility;
using Logic_IPBanUtility.Models;
using System.Collections.Generic;
using WPF_IPBanUtility.Base;

namespace WPF_IPBanUtility;

public class KeysVisibilityControllerViewModel : SettingsComponentViewModelBase
{
     private ConfigFileManager _configFileManager;
     private List<KeyIdenti> _oldKeyIndentis { get; set; }
     public List<KeyIdenti> KeyIndentis { get; set; }

     public KeysVisibilityControllerViewModel(ConfigFileManager configFileManager) : base(Properties.PageNames.KeysVisibilityControllerViewTitle)
     {
          _configFileManager = configFileManager;
          KeyIndentis = configFileManager.ReadKeyIndentis();
          _oldKeyIndentis = KeyIndentis;
     }
     public override void Save()
     {
          if (_oldKeyIndentis.Equals(KeyIndentis))
          {
               _oldKeyIndentis = KeyIndentis;
               foreach (var key in KeyIndentis)
                    _configFileManager.WriteKeyIdentiChanged(key);
          }
     }
}
