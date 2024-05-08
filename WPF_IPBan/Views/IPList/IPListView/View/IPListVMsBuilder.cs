using Logic_IPBanUtility.Logic.IPList;
using System.Collections.Generic;

namespace WPF_IPBanUtility.Views.IPList.IPListView.View;

public class IPListVMsBuilder
{
    private readonly IPBlockedListService _iPBlockedListService;

    public IPListVMsBuilder(IPBlockedListService iPBlockedListService)
    {
        _iPBlockedListService = iPBlockedListService;
    }

    public List<IPListViewModelBase> Build(IPListProperties properties)
    {
        return new()
          {
               new IPBlockedListViewModel(_iPBlockedListService, properties.BlockList),
               new WhiteListViewModel(properties.WhiteList),
               new BlackListViewModel(properties.BlackList)
          };
    }
}