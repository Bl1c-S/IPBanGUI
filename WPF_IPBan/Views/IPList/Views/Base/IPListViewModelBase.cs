using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Common;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;
public class IPListViewModelBase : ViewModelBase
{
     public string Title { get; }
     public string ItemCountText { get => $"{OtherWords.Total} {ItemCount}"; }
     public int ItemCount { get; protected set; }
     public Visibility ListVisibility { get; private set; }
     public SymbolRegular ListIcon { get; private set; }
     public IPListViewModelBase(string title, int itemCount)
     {
          Title = title;
          ItemCount = itemCount;
          ListVisibility = Visibility.Visible;
          ListIcon = SymbolRegular.ChevronDown24;
          IHideCommand = new RelayCommand(Hide);
     }

     public virtual void Update() { }
     public virtual void Hide()
     {
          if (ListVisibility == Visibility.Visible)
          {
               ListVisibility = Visibility.Collapsed;
               ListIcon = SymbolRegular.ChevronUp24;
          }
          else
          {
               ListVisibility = Visibility.Visible;
               ListIcon = SymbolRegular.ChevronDown24;
          }
          OnPropertyChanged(nameof(ListVisibility));
          OnPropertyChanged(nameof(ListIcon));
     }
     public virtual void RemoveAll() { }

     public ICommand IHideCommand { get; }
}
