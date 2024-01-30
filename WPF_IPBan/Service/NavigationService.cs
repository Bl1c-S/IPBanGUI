using Microsoft.Extensions.DependencyInjection;
using System;

namespace WPF_IPBanUtility;

internal class NavigationService
{
     private readonly IServiceProvider _serviceProvider;
     private PageViewModelBase? _currentViewModel;

     public event Action? OnCurrentChanged;
     public PageViewModelBase? CurrentViewModel => _currentViewModel;
     public NavigationService(IServiceProvider serviceProvider)
     {
          _serviceProvider = serviceProvider;
     }
     public void Navigate<T>() where T : PageViewModelBase
     {
          if (_currentViewModel != null && _currentViewModel is T)
               return;

          var viewModel = _serviceProvider.GetService<T>() as PageViewModelBase;


          if (viewModel is null)
               return;

          _currentViewModel?.Dispose();
          _currentViewModel = viewModel;
          OnCurrentChanged?.Invoke();
     }
}