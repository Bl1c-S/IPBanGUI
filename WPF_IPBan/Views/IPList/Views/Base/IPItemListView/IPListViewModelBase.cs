using System;
using System.Collections.ObjectModel;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;
public class IPListViewModelBase : ViewModelBase
{
     public ObservableCollection<IPUserControlViewModelBase> VMs { get; protected set; } = new();

     public Action? VMChanged;
     public string Title { get; }
     public string ItemCountText { get => $"{OtherWords.Total} {ItemCount}"; }
     public int ItemCount { get; protected set; }

     public IPListViewProperties Properties { get; }

     public IPListViewModelBase(string title, IPListViewProperties properties)
     {
          Title = title;
          Properties = properties;
     }
     protected void DisposeItem(IPUserControlViewModelBase vm)
     {
          VMs.Remove(vm);
          vm.Dispose();
     }
     protected virtual void IPListChanged(bool currentVMChanged = false)
     {
          ItemCount = VMs.Count;
          OnPropertyChanged(nameof(VMs));
          OnPropertyChanged(nameof(ItemCountText));
          if (currentVMChanged) VMChanged?.Invoke();
     }
     public virtual void Update() { }
}
