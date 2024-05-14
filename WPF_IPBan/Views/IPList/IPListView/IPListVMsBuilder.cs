using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Logic.IPList;
using Logic_IPBanUtility.Models;
using System;
using System.Collections.Generic;

namespace WPF_IPBanUtility.Views.IPList;

public class IPListVMsBuilder : IDisposable
{
     private readonly IPBlockedListService _iPBlockedListService;
     private readonly KeyValueManager _keyManager;
     private IPListChangedActions? _listChangedActions;

     public IPListVMsBuilder(IPBlockedListService iPBlockedListService, KeyValueManager keyManager)
     {
          _iPBlockedListService = iPBlockedListService;
          _keyManager = keyManager;
     }

     public IPListVMsResult Build(IPListProperties properties)
     {
          var whiteList = new WhiteListViewModel(_keyManager, properties.WhiteList);
          var blackList = new BlackListViewModel(_keyManager, properties.BlackList);
          _listChangedActions = new IPListChangedActions(whiteList.ListChanged, blackList.ListChanged);
          _keyManager.KeyContextChanged += _listChangedActions.InvokeKey;

          var iPInputVM = new IPInputViewModel(_keyManager.AddIpToKey, _keyManager.KeyContextChanged);
          var ipBlock = new IPBlockedListViewModel(_iPBlockedListService, properties.BlockList, _keyManager.KeyContextChanged);
          var iPListVMs = new List<IPListViewModelBase>() { ipBlock, whiteList, blackList };
          return new(iPListVMs, iPInputVM);
     }

     public void Dispose()
     {
          if (_listChangedActions != null)
               _keyManager.KeyContextChanged -= _listChangedActions.InvokeKey;
     }

     public class IPListVMsResult
     {
          public List<IPListViewModelBase> IPListVMs;
          public IPInputViewModel IPInputVM;

          public IPListVMsResult(List<IPListViewModelBase> iPListVMs, IPInputViewModel iPInputVM)
          {
               IPListVMs = iPListVMs;
               IPInputVM = iPInputVM;
          }
     }

     public class IPListChangedActions
     {
          public Action? WhiteList;
          public Action? BlackList;

          public IPListChangedActions(Action? whiteList, Action? blackList)
          {
               WhiteList = whiteList;
               BlackList = blackList;
          }

          public void InvokeKey(KeyNames keyName)
          {
               if (keyName == KeyNames.Whitelist)
                    WhiteList?.Invoke();
               if (keyName == KeyNames.Blacklist)
                    BlackList?.Invoke();
          }
     }
}