using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;
public class IPListViewModelBase : ViewModelBase
{
     public ObservableCollection<IPUserControlViewModelBase> VMs { get; protected set; } = new();
     public string Title { get; }
     public string ItemCountText { get => $"{OtherWords.Total} {ItemCount}"; }
     public int ItemCount { get; protected set; }

     public IPListViewProperties Properties { get; }

     public IPListViewModelBase(string title, int itemCount, IPListViewProperties properties)
     {
          Title = title;
          ItemCount = itemCount;
          Properties = properties;
          IHideCommand = new RelayCommand(Properties.Hide);
     }
     protected virtual void IPListChanged() { }
     public virtual void Update() { }

     public ICommand IHideCommand { get; }
}
