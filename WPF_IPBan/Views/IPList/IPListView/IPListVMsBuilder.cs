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
          _listChangedActions = new IPListChangedActions(whiteList.ListChanged, blackList.ListChanged, new(() => whiteList.IPs), new(() => blackList.IPs));
          _keyManager.KeyContextChanged += _listChangedActions.InvokeKey;

          var iPInputVM = new IPInputViewModel(_keyManager.AddIpToKey, _listChangedActions.GetIPList, _keyManager.KeyContextChanged);
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
          public Action? WhiteListChanged;
          public Action? BlackListChanged;

          private Func<string[]> _whiteIPs;
          private Func<string[]> _blackIPs;

          public IPListChangedActions(Action? whiteList, Action? blackList, Func<string[]> whiteIPs, Func<string[]> blackIPs)
          {
               WhiteListChanged = whiteList;
               BlackListChanged = blackList;
               _whiteIPs = whiteIPs;
               _blackIPs = blackIPs;
          }

          public void InvokeKey(KeyNames keyName)
          {
               if (keyName == KeyNames.Whitelist)
                    WhiteListChanged?.Invoke();
               if (keyName == KeyNames.Blacklist)
                    BlackListChanged?.Invoke();
          }
          public string[] GetIPList(KeyNames keyName)
          {
               if (keyName == KeyNames.Whitelist)
                    return _whiteIPs.Invoke();
               if (keyName == KeyNames.Blacklist)
                    return _blackIPs.Invoke();
               return new string[] { };
          }
     }
}