using System.Windows.Controls;

namespace WPF_IPBanUtility;

internal class MainWindowViewModel : ViewModelBase
{
     public NavigateBarViewModel NavigateBarViewModel => _navigateBarViewModel;
     public NavigateTabViewModel NavigateTabViewModel => _navigateTabViewModel;

     public UserControl? CurrentView => _navigationService.CurrentView;
     public PageViewModelBase? CurrentViewModel => _navigationService.CurrentViewModel;

     private readonly NavigateBarViewModel _navigateBarViewModel;
     private readonly NavigateTabViewModel _navigateTabViewModel;
     private readonly NavigationService _navigationService;


     public MainWindowViewModel(NavigationService navigationService)
     {
          _navigateBarViewModel = new(navigationService);
          _navigateTabViewModel = new(navigationService);
          _navigationService = navigationService;

          _navigationService.OnCurrentChanged += OnNavigateChanged;
          _navigationService.NavToManual();
     }

     public void Window_Closing() =>
          _navigationService.Window_Closing();

     private void OnNavigateChanged()
     {
          OnPropertyChanged(nameof(CurrentViewModel));
          OnPropertyChanged(nameof(CurrentView));
     }

     public override void Dispose()
     {
          _navigationService.OnCurrentChanged -= OnNavigateChanged;
          base.Dispose();
     }
}
