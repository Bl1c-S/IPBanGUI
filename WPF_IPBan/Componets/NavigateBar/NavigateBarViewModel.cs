using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF_IPBanUtility;

internal class NavigateBarViewModel : ViewModelBase
{
     private readonly NavigationService _navigationService;

     public NavigateBarViewModel(NavigationService navigationService)
     {
          _navigationService = navigationService;
          _navigationService.OnCurrentChanged += OnCurrentChanged;
          NavigateToKeyList = new RelayCommand(() => _navigationService.Navigate<KeyListViewModel>());
          NavigateToSettings = new RelayCommand(() => _navigationService.Navigate<SettingsViewModel>());
          NavigateToEvents = new RelayCommand(() => _navigationService.Navigate<EventsViewModel>());
     }

     public PageViewModelBase? CurrentViewModel => _navigationService.CurrentViewModel;
     public string? CurrentPageName => CurrentViewModel?.PageName;

     public ICommand NavigateToKeyList { get; }
     public ICommand NavigateToSettings { get; }
     public ICommand NavigateToEvents { get; }

     private void OnCurrentChanged()
     {
          OnPropertyChanged(nameof(CurrentViewModel));
          OnPropertyChanged(nameof(CurrentPageName));
     }
     public override void Dispose()
     {
          _navigationService.OnCurrentChanged -= OnCurrentChanged;
          base.Dispose();
     }
}
