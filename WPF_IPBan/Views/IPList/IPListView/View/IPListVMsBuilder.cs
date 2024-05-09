using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Logic.IPList;
using System.Collections.Generic;

namespace WPF_IPBanUtility.Views.IPList.IPListView.View;

public class IPListVMsBuilder
{
     private readonly IPBlockedListService _iPBlockedListService;
     private readonly KeyValueManager _keyManager;

     public IPListVMsBuilder(IPBlockedListService iPBlockedListService, KeyValueManager keyManager)
     {
          _iPBlockedListService = iPBlockedListService;
          _keyManager = keyManager;
     }

     public List<IPListViewModelBase> Build(IPListProperties properties)
     {
          var whiteList = new WhiteListViewModel(_keyManager, properties.WhiteList);
          var blackList = new BlackListViewModel(_keyManager, properties.BlackList);
          var ipBlock = new IPBlockedListViewModel(_iPBlockedListService, properties.BlockList, whiteList.ListChanged, blackList.ListChanged);
          return new() { ipBlock, whiteList, blackList };
     }
}