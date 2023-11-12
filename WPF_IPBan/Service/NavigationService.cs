using Microsoft.Extensions.DependencyInjection;
using System;

namespace WPF_IPBanUtility;

internal class NavigationService
{
     private readonly IServiceProvider _serviceProvider;
     private ViewModelBase? _currentViewModel;

     public event Action? OnCurrentChanged;
     public ViewModelBase? CurrentViewModel => _currentViewModel;
     public NavigationService(IServiceProvider serviceProvider)
     {
          _serviceProvider = serviceProvider;
     }
     public void Navigate<T>() where T : ViewModelBase
     {
          var viewModel = _serviceProvider.GetService<T>() as ViewModelBase;

          if (viewModel is null)
               return;

          _currentViewModel?.Dispose();
          _currentViewModel = viewModel;
          OnCurrentChanged?.Invoke();
     }
}