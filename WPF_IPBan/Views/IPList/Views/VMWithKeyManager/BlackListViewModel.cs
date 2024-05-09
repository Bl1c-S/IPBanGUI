using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Models;
using Wpf.Ui.Common;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;

public class BlackListViewModel : KeyIPListViewModelBase
{
     public BlackListViewModel(KeyValueManager keyManager, IPListViewProperties properties) :
           base(KeyNames.Blacklist, SetStatus(), keyManager, PageNames.BlackList, properties)
     { }
     private static IPStatus SetStatus()
     {
          var icon = SymbolRegular.Prohibited20;
          var title = Status.Ban;
          var message = ToolTips.BlackListItem;
          return new(icon, title, message);
     }
}