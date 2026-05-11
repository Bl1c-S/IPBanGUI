using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WPF_IPBanUtility.Services;

internal class NavigationService
{
     private readonly IServiceProvider _serviceProvider;
     private UserControl? _currentView;
     private PageViewModelBase? _currentViewModel;

     public event Action? OnCurrentChanged;
     public PageViewModelBase? CurrentViewModel => _currentViewModel;
     public UserControl? CurrentView => _currentView;
     public IReadOnlyList<PageRegistration> Pages => _pages;
     private readonly List<PageRegistration> _pages = new();
     public NavigationService(IServiceProvider serviceProvider)
     {
          _serviceProvider = serviceProvider;
     }
     public void Register(PageRegistration page) => _pages.Add(page);

     public void Navigate(Type viewModelType)
     {
          var page = _pages.Find(p => p.ViewModelType == viewModelType);
          if (page is null) return;
          NavigateToPage(page);
     }

     public void NavToFirst() => Navigate(_pages[0].ViewModelType);

     private void NavigateToPage(PageRegistration page)
     {
          if (_currentViewModel?.GetType() == page.ViewModelType) return;

          _currentViewModel?.ApplyChanges();

          var viewModel = _serviceProvider.GetService(page.ViewModelType) as PageViewModelBase;
          if (viewModel is null) return;

          if (_currentView != null) _currentView.DataContext = null;
          _currentViewModel?.Dispose();

          _currentViewModel = viewModel;
          _currentView = page.ViewFactory();
          _currentView.DataContext = _currentViewModel;
          OnCurrentChanged?.Invoke();
     }
     internal void Window_Closing()
     {
          if (_currentViewModel != null)
               _currentViewModel.ApplyChanges(new[] { ApplyOptions.Await });
     }
}