using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Models;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

public class BlackListViewModel(KeyValueManager keyManager, IPListViewProperties properties)
     : KeyIPListViewModelBase(KeyNames.Blacklist, SetStatus(), keyManager, properties)
{
     private static IPStatus SetStatus()
     {
          var icon = SymbolRegular.Prohibited20;
          var title = Status.Ban;
          var message = ToolTips.BlackListItem;
          return new(icon, title, message);
     }
}