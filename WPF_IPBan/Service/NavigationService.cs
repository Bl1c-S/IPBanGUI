using System;

namespace WPF_IPBanUtility;

internal class NavigationService
{
     private ViewModelBase? _currentViewModel;
     public event Action? OnCurrentChanged;

     public ViewModelBase? CurrentViewModel => _currentViewModel;

     public void Navigate(ViewModelBase viewModel)
     {
          if (viewModel != null)
               _currentViewModel?.Dispose();

          _currentViewModel = viewModel;

          OnCurrentChanged?.Invoke();
     }
}
