using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;

public class BlackListViewModel : IPListViewModelBase
{
    public BlackListViewModel(IPListViewProperties properties) : base(PageNames.BlackList, 0, properties)
    {
    }
}
