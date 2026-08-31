using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WPF_IPBanUtility.Components.NavigateBar;
using WPF_IPBanUtility.Properties;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

internal class NavigateBarViewModel : ViewModelBase
{
     private readonly NavigationService _navigationService;
     public ObservableCollection<NavItem> NavButtons { get; set; } = new();
     public NavigateBarViewModel(NavigationService navigationService)
     {
          _navigationService = navigationService;
          _navigationService.OnCurrentChanged += OnCurrentChanged;

          NavButtons.Add(new(CreateButtonWithTitle(new RelayCommand(_navigationService.NavToManual), SymbolRegular.BookInformation24, PageNames.Main, "", new(4, 0, 0, 0))));
          NavButtons.Add(new(CreateButtonWithTitle(new RelayCommand(_navigationService.NavToEvents), SymbolRegular.ChartMultiple24, PageNames.Events, "", new(4, 0, 0, 0))));
          NavButtons.Add(new(CreateButtonWithTitle(new RelayCommand(_navigationService.NavToIpList), SymbolRegular.ShieldTask24, PageNames.IP, "", new(4, 0, 0, 0))));
          NavButtons.Add(new(CreateButtonWithTitle(new RelayCommand(_navigationService.NavToKeyList), SymbolRegular.Key24, PageNames.KeyList, "", new(4, 0, 0, 0))));
          NavButtons.Add(new(CreateButtonWithTitle(new RelayCommand(_navigationService.NavToSettings), SymbolRegular.Settings48, PageNames.Settings, "", new(4, 0, 0, 0))));
          UpdateNavItems();
     }

     public PageViewModelBase? CurrentViewModel => _navigationService.CurrentViewModel;

     public string? CurrentPageName => CurrentViewModel?.PageName;

     protected sealed override Button CreateButtonWithTitle(ICommand command, SymbolRegular icon, string title, string toolTip = "", Thickness? margin = null)
     {
         var button = base.CreateButtonWithTitle(command, icon, title, toolTip, margin);
         return button;
     }
     
     private void OnCurrentChanged()
     {
          OnPropertyChanged(nameof(CurrentViewModel));
          OnPropertyChanged(nameof(CurrentPageName));
          UpdateNavItems();
     }

     private void UpdateNavItems()
     {
          foreach (var navItem in NavButtons)
          {
               navItem.CurrentPageName = CurrentPageName;
          }
     }
     public override void Dispose()
     {
          _navigationService.OnCurrentChanged -= OnCurrentChanged;
          base.Dispose();
     }
}
