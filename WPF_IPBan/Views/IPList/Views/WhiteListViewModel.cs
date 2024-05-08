using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;

public class WhiteListViewModel : IPListViewModelBase
{
    public WhiteListViewModel(IPListViewProperties properties) : base(PageNames.WhiteList, 0, properties)
    {
    }
}
