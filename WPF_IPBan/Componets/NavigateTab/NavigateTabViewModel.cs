﻿namespace WPF_IPBanUtility;

using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

internal class NavigateTabViewModel : ViewModelBase
{
     private readonly NavigationService _navigationService;

     public ObservableCollection<Button>? CurrentPageButtons => _navigationService.CurrentViewModel?.PageButtons;

     public NavigateTabViewModel(NavigationService navigationService)
     {
          _navigationService = navigationService;
          _navigationService.OnCurrentChanged += OnCurrentChanged;
     }

     private void OnCurrentChanged()
     {
          OnPropertyChanged(nameof(CurrentPageButtons));
     }
     public override void Dispose()
     {
          _navigationService.OnCurrentChanged -= OnCurrentChanged;
          base.Dispose();
     }
}
