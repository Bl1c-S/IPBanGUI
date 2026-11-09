using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Logic.IPList;
using Logic_IPBanUtility.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Wpf.Ui.Controls;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;

public class IPBlockedListViewModel : IPListViewModelBase
{
     private readonly IpBlockedListService _ipBlockedListService;
     private readonly Action<KeyNames>? _listChanged;
     public Action ApplyRemove => _ipBlockedListService.ApplyRemove;
     public IRelayCommand AddAllToBlackListCommand { get; }
     private readonly Button _moveAllToBlackListButton;

     public IPBlockedListViewModel(IpBlockedListService ipListService, IPListViewProperties properties,
          Action<KeyNames> iPListChanged) :
          base(PageNames.BlockList, properties)
     {
          _listChanged = iPListChanged;
          _ipBlockedListService = ipListService;
          AddAllToBlackListCommand = new RelayCommand(AddAllToBlacklist, () => HasItems);

          VMs = BuildVMs(_ipBlockedListService.IPs);
          IpListChanged();
          _ipBlockedListService.IPsChanged += IpBlockListChanged;

          _moveAllToBlackListButton = CreateListHeaderButton(new RelayCommand(OpenConfirmMenuMoveAllToBlackList),
               SymbolRegular.Prohibited20, "Подсказка");

          Buttons.Add(_moveAllToBlackListButton);
     }

     private void OpenConfirmMenuMoveAllToBlackList()
     {
          _moveAllToBlackListButton.ContextMenu ??= CreateContextMenuConfirmation(
               "текст предупреждения",
               new RelayCommand(AddAllToBlacklist),
               SymbolRegular.Prohibited20);

          OpenContextMenu(_moveAllToBlackListButton);
     }

     public void AddAllToBlacklist()
     {
          if (VMs.Count == 0)
               return;

          _ipBlockedListService.AddAllToBlacklist();
          Cleanup();
          IpListChanged(true);
          _listChanged?.Invoke(KeyNames.Blacklist);
     }

     private ObservableCollection<IPUserControlViewModelBase> BuildVMs(List<IPAddressEntity> ips)
     {
          var result = new List<IPUserControlViewModelBase>();
          if (ips.Count == 0)
               return new(result);

          var firstVm = CreateVm(ips[0]);
          firstVm.BorderVisibility = Visibility.Collapsed;
          result.Add(firstVm);

          for (var id = 1; id < ips.Count; id++)
          {
               var vm = CreateVm(ips[id]);
               result.Add(vm);
          }

          return new(result);
     }

     private IPBlockedViewModel CreateVm(IPAddressEntity iPAddressEntity) => new(
          iPAddressEntity,
          AddToWhiteList,
          AddToBlacklist,
          RemoveWithBlocklist,
          DisposeItem);

     private void AddToWhiteList(IPAddressEntity ip)
     {
          _ipBlockedListService.AddToWhiteList(ip);
          RemoveVm(ip);
          _listChanged?.Invoke(KeyNames.Whitelist);
     }

     private void AddToBlacklist(IPAddressEntity ip)
     {
          _ipBlockedListService.AddToBlacklist(ip);
          RemoveVm(ip);
          _listChanged?.Invoke(KeyNames.Blacklist);
     }

     private void RemoveWithBlocklist(IPAddressEntity ip)
     {
          _ipBlockedListService.Remove(ip);
          RemoveVm(ip);
     }

     private void RemoveVm(IPAddressEntity ip)
     {
          var vm = VMs.FirstOrDefault(vm => vm.Title == ip.IPAddressText);
          if (vm == null) return;

          DisposeItem(vm);
          IpListChanged(true);
     }

     private void IpBlockListChanged()
     {
          AddNewVMs();
          IpListChanged();
     }

     protected sealed override void IpListChanged(bool currentVmChanged = false)
     {
          OnPropertyChanged(nameof(VMs));
          AddAllToBlackListCommand.NotifyCanExecuteChanged();
          if (_moveAllToBlackListButton != null)
               _moveAllToBlackListButton.IsEnabled = HasItems;

          base.IpListChanged(currentVmChanged);
     }

     private void AddNewVMs()
     {
          foreach (var ip in _ipBlockedListService.IPs)
          {
               var vm = VMs.FirstOrDefault(vm => vm.Title == ip.IPAddressText);
               if (vm == null) VMs.Add(CreateVm(ip));
          }
     }

     private void Cleanup()
     {
          if (VMs.Count == 0)
               return;

          foreach (var item in VMs)
               item.Dispose();

          VMs.Clear();
     }

     public override void Dispose()
     {
          _ipBlockedListService.IPsChanged -= IpBlockListChanged;
          Cleanup();
          base.Dispose();
     }
}
