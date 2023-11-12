namespace WPF_IPBanUtility;

internal class MainWindowViewModel : ViewModelBase
{
     private readonly NavigateBarViewModel _navigateBarViewModel;
     private readonly NavigationService _navigationService;

     public NavigateBarViewModel NavigateBarViewModel => _navigateBarViewModel;
     public ViewModelBase? CurrentViewModel => _navigationService.CurrentViewModel;

     public MainWindowViewModel(NavigationService navigationService)
     {
          _navigateBarViewModel = new(navigationService);
          _navigationService = navigationService;

          _navigationService.Navigate<KeyListViewModel>();
          _navigationService.OnCurrentChanged += OnNavigateChanged;
     }
     private void OnNavigateChanged()
     {
          OnPropertyChanged(nameof(CurrentViewModel));
     }

     public override void Dispose()
     {
          _navigationService.OnCurrentChanged -= OnNavigateChanged;
          base.Dispose();
     }
}
