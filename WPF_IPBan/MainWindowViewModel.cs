namespace WPF_IPBanUtility;

internal class MainWindowViewModel : ViewModelBase
{
     private readonly NavigateBarViewModel _navigateBarViewModel;
     private readonly NavigateTabViewModel _navigateTabViewModel;
     private readonly NavigationService _navigationService;

     public NavigateBarViewModel NavigateBarViewModel => _navigateBarViewModel;
     public NavigateTabViewModel NavigateTabViewModel => _navigateTabViewModel;

     public PageViewModelBase? CurrentViewModel => _navigationService.CurrentViewModel;

     public MainWindowViewModel(NavigationService navigationService)
     {
          _navigateBarViewModel = new(navigationService);
          _navigateTabViewModel = new(navigationService);
          _navigationService = navigationService;

          _navigationService.Navigate<EventsViewModel>();
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
