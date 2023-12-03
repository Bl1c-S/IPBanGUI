using Logic_IPBanUtility.Setting;
using System.Collections.Generic;

namespace WPF_IPBanUtility;

internal class SettingsViewsBuilder
{
     public List<ISettingsViewComponent> SettingVMs = new();
     private Settings _settings;
     public SettingsViewsBuilder(Settings settings)
     {
          _settings = settings;
     }

     private void CreateSelectFolder()
     {
          SelectFolderViewModel sf = new(_settings);
     }
}
