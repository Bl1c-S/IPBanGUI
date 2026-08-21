using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Models;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

public class WhiteListViewModel : KeyIPListViewModelBase
{
     public WhiteListViewModel(KeyValueManager keyManager, IPListViewProperties properties) :
          base(KeyNames.Whitelist, SetStatus(), keyManager, PageNames.WhiteList, properties)
     { }
     private static IPStatus SetStatus()
     {
          var icon = SymbolRegular.CheckmarkCircle20;
          var title = Status.InWhite;
          var message = ToolTips.WhiteListItem;
          return new(icon, title, message);
     }
}