using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;

public class KeyIPListViewModelBase : IPListViewModelBase
{
     public Action? ListChanged;
     private readonly IPStatus _status;
     private KeyNames _keyName;
     public KeyValueManager _keyManager { get; }
     private string[] _oldIPs = new string[] {};

     public KeyIPListViewModelBase(KeyNames key, IPStatus status, KeyValueManager keyManager, string title, IPListViewProperties properties) :
          base(title, properties)
     {
          ListChanged += IPListChanged;
          _status = status;
          _keyName = key;
          _keyManager = keyManager;
          IPListChanged();
     }

     public ObservableCollection<KeyIPUserControlViewModel> BuildVMs(string[] ips)
     {
          {
               var result = new List<KeyIPUserControlViewModel>();
               if (ips.Length == 0)
                    return new(result);

               var firstVm = CreateVM(ips[0]);
               firstVm.BorderVisibility = Visibility.Collapsed;
               result.Add(firstVm);

               for (var id = 1; id < ips.Length; id++)
               {
                    var vm = CreateVM(ips[id]);
                    result.Add(vm);
               }
               return new(result);
          }
     }
     public KeyIPUserControlViewModel CreateVM(string ip)
     {
          return new(ip,
               _status,
               RemoveItem,
               DisposeItem);
     }
     private void RemoveItem(string ip)
     {
          _keyManager.RemoveIpFromKey(_keyName, ip);
          ListChanged?.Invoke();
     }
     public override void Update()
     {
          var newIps = GetIps();
          var changeFunc = () =>
          {
               _oldIPs = newIps;
               IPListChanged();
               return;
          };

          if (_oldIPs.Length != newIps.Length)
               changeFunc.Invoke();
          for (int i = 0; i < _oldIPs.Length; i++)
          {
               if (!_oldIPs[i].Equals(newIps[i], StringComparison.Ordinal))
                    changeFunc.Invoke();
          }
     }
     protected override void IPListChanged()
     {
          _oldIPs = GetIps();
          VMs = new(BuildVMs(_oldIPs));
          base.IPListChanged();
     }
     private string[] GetIps() => _keyManager.GetIpListWithKey(_keyName);

     public override void Dispose()
     {
          ListChanged -= IPListChanged;
          base.Dispose();
     }
}
