using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_IPBanUtility
{
     internal class MainWindowViewModel : ViewModelBase
     {
          private readonly NavigateBarViewModel _navigateBarViewModel;
          private readonly NavigationService _navigationService;

          public ViewModelBase? CurrentViewModel => _navigationService.CurrentViewModel;
          public NavigateBarViewModel NavigateBarViewModel => _navigateBarViewModel;

          public MainWindowViewModel(NavigationService navigationService)
          {
               _navigateBarViewModel = new(navigationService);
               _navigationService = navigationService;

               _navigationService.Navigate(new SettingsViewModel());
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
}
