using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Models;
using Wpf.Ui.Common;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;

public class BlackListViewModel : KeyIPListViewModelBase
{
     public BlackListViewModel(KeyValueManager keyManager, IPListViewProperties properties) :
           base(KeyNames.Blacklist, SetStatus, keyManager, PageNames.BlackList, properties)
     { }
     private static readonly IPStatus SetStatus = new(
          SymbolRegular.Prohibited20,
          Status.Ban,
          ToolTips.BlackListItem          
     );
}