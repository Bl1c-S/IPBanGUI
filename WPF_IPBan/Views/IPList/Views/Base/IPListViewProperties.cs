using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Windows;
using Wpf.Ui.Common;

namespace WPF_IPBanUtility.Views.IPList
{
    public class IPListViewProperties : ObservableObject
     {
          private bool isHide;
          public Visibility ListVisibility { get; private set; }
          public SymbolRegular ListIcon { get; private set; }

          public IPListViewProperties(bool ListVisibility)
          {
               isHide = ListVisibility;
               Hide();
          }
          public void Hide()
          {
               if (!isHide)
               {
                    ListVisibility = Visibility.Collapsed;
                    ListIcon = SymbolRegular.ChevronUp24;
               }
               else
               {
                    ListVisibility = Visibility.Visible;
                    ListIcon = SymbolRegular.ChevronDown24;
               }
               isHide = !isHide;
               OnPropertyChanged(nameof(ListVisibility));
               OnPropertyChanged(nameof(ListIcon));
          }
     }
}
