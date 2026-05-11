using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_IPBanUtility.Componets.NavigateBar;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Services;

namespace WPF_IPBanUtility;

internal class NavigateBarViewModel : ViewModelBase
{

     private readonly NavigationService _navigationService;
     public ObservableCollection<NavButtonModel> NavButtons { get; } = new();
     public string? CurrentPageName => _navigationService.CurrentViewModel?.PageName;
     public string IPMes { get => Messages.IPMes; }
     public NavigateBarViewModel(NavigationService navigationService)
     {
          _navigationService = navigationService;

          foreach (var page in navigationService.Pages)
          {
               NavButtons.Add(new NavButtonModel(
                    page.Name,
                    page.Icon,
                    new RelayCommand(() => navigationService.Navigate(page.ViewModelType)),
                    () => CurrentPageName,
                    page.ToolTip
               ));
          }

          _navigationService.OnCurrentChanged += OnCurrentChanged;
     }

     private void OnCurrentChanged()
     {
          OnPropertyChanged(nameof(CurrentPageName));
          foreach (var btn in NavButtons)
               btn.RefreshIsActive();
     }
     public PageViewModelBase? CurrentViewModel => _navigationService.CurrentViewModel;


     //public ICommand NavigateManual { get; }
     //public ICommand NavigateToKeyList { get; }
     //public ICommand NavigateToIPList { get; }
     //public ICommand NavigateToSettings { get; }
     //public ICommand NavigateToEvents { get; }

     //private void OnCurrentChanged()
     //{
     //     OnPropertyChanged(nameof(CurrentViewModel));
     //     OnPropertyChanged(nameof(CurrentPageName));
     //}
     public override void Dispose()
     {
          _navigationService.OnCurrentChanged -= OnCurrentChanged;
          base.Dispose();
     }

}
