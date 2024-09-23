using Logic_IPBanUtility.Logic.IPList;
using Logic_IPBanUtility.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;
public class IPBlockedListViewModel : IPListViewModelBase
{
     public Action ApplyRemove => _iPBlokedListService.ApplyRemove;
     private readonly IPBlockedListService _iPBlokedListService;
     private Action<KeyNames>? ListChanged;

     public IPBlockedListViewModel(IPBlockedListService iPListService, IPListViewProperties properties, Action<KeyNames> iPListChanged) :
          base(PageNames.BlockList, properties)
     {
          ListChanged = iPListChanged;
          _iPBlokedListService = iPListService;
          IPBlockListChanged();
          _iPBlokedListService.IPsChanged += IPBlockListChanged;
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
          ListChanged?.Invoke(KeyNames.Whitelist);
     }
     private void AddToBlacklist(IPAddressEntity ip)
     {
          _iPBlokedListService.AddToBlacklist(ip);
          ListChanged?.Invoke(KeyNames.Blacklist);
     }

     private void IPBlockListChanged()
     {
          Cleanup();
          VMs = BuildVMs(_iPBlokedListService.IPs);
          base.IPListChanged();
     }

     private void Cleanup()
     {
          if (VMs.Count > 0)
          {
               foreach (var item in VMs)
                    item.Dispose();

               VMs.Clear();
          }
     }

     public override void Update()
     {
          _iPBlokedListService.Update();
     }
     public override void Dispose()
     {
          _iPBlokedListService.IPsChanged -= IPBlockListChanged;
          Cleanup();
          base.Dispose();
     }
}