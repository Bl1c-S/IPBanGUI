using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Models;
using Wpf.Ui.Common;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;

public class WhiteListViewModel : KeyIPListViewModelBase
{
     public WhiteListViewModel(KeyValueManager keyManager, IPListViewProperties properties) :
          base(KeyNames.Whitelist, SetStatus, keyManager, PageNames.WhiteList, properties)
     { }
     private static readonly IPStatus SetStatus = new(
          SymbolRegular.CheckmarkCircle20,
          Status.InWhite,
          ToolTips.WhiteListItem              
     );
}