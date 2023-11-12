namespace WPF_IPBanUtility;

internal class KeyListViewModel : ViewModelBase
{
     private readonly NavigationService _navigationService;

     public KeyListViewModel(NavigationService navigationService)
     {
          _navigationService = navigationService;
     }
}
