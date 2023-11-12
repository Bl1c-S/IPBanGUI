using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

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
     }

     public ViewModelBase? CurrentViewModel => _navigationService.CurrentViewModel;

     public ICommand NavigateToKeyList { get; }
     public ICommand NavigateToSettings { get; }

     private void OnCurrentChanged()
     {
          OnPropertyChanged(nameof(CurrentViewModel));
     }
     public override void Dispose()
     {
          _navigationService.OnCurrentChanged -= OnCurrentChanged;
          base.Dispose();
     }
}
