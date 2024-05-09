using Logic_IPBanUtility.Logic.IPList;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;
using WPF_IPBanUtility.Views.IPList.Views.IPBlockedList;

namespace WPF_IPBanUtility;
public class IPBlockedListViewModel : IPListViewModelBase
{
     private readonly IPBlockedListService _iPBlokedListService;
     private Action? WhiteListChanged;
     private Action? BlackListChanged;

     public IPBlockedListViewModel(IPBlockedListService iPListService, IPListViewProperties properties, Action? whiteListChanged, Action? blackListChanged) : 
          base(PageNames.BlockList, properties)
     {
          WhiteListChanged = whiteListChanged;
          BlackListChanged = blackListChanged;
          _iPBlokedListService = iPListService;
          IPListChanged();
          _iPBlokedListService.IPsChanged += IPListChanged;
     }

     private ObservableCollection<IPUserControlViewModelBase> BuildVMs(List<IPAddressEntity> ips)
     {
          var result = new List<IPUserControlViewModelBase>();
          if (ips.Count == 0)
               return new(result);

          var firstVm = CreateVM(ips[0]);
          firstVm.BorderVisibility = Visibility.Collapsed;
          result.Add(firstVm);

          for (var id = 1; id < ips.Count; id++)
          {
               var vm = CreateVM(ips[id]);
               result.Add(vm);
          }
          return new(result);
     }
     private IPBlockedViewModel CreateVM(IPAddressEntity iPAddressEntity)
     {
          return new(iPAddressEntity,
               AddToWhiteList,
               AddToBlacklist,
               _iPBlokedListService.Remove,
               DisposeItem);
     }
     private void AddToWhiteList(IPAddressEntity ip)
     {
          _iPBlokedListService.AddToWhiteList(ip);
          WhiteListChanged?.Invoke();
     }
     private void AddToBlacklist(IPAddressEntity ip)
     {
          _iPBlokedListService.AddToBlacklist(ip);
          BlackListChanged?.Invoke();
     }

     protected override void IPListChanged()
     {
          VMs = BuildVMs(_iPBlokedListService.IPs);
          base.IPListChanged();
     }
     public override void Update()
     {
          _iPBlokedListService.Update();
     }
     public override void Dispose()
     {
          _iPBlokedListService.IPsChanged -= IPListChanged;
          base.Dispose();
     }
}