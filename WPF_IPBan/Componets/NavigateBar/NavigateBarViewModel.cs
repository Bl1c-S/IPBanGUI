﻿using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace WPF_IPBanUtility;

internal class NavigateBarViewModel : ViewModelBase
{
     private readonly NavigationService _navigationService;

     public NavigateBarViewModel(NavigationService navigationService)
     {
          _navigationService = navigationService;
          NavigateToKeyList = new RelayCommand(() => _navigationService.Navigate<KeyListViewModel>());
          NavigateToSettings = new RelayCommand(() => _navigationService.Navigate<SettingsViewModel>());
     }

     public ICommand NavigateToKeyList { get; }
     public ICommand NavigateToSettings { get; }
}
