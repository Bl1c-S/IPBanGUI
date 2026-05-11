using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WPF_IPBanUtility.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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


     //public void Navigate<T>(UserControl linkedView) where T : PageViewModelBase
     //{
     //     if (_currentViewModel != null && _currentViewModel is T) return;

     //     if (_currentViewModel != null) _currentViewModel.ApplyChanges();

     //     var viewModel = _serviceProvider.GetService<T>() as PageViewModelBase;
     //     if (viewModel is null) return;

     //     if (_currentView != null) _currentView.DataContext = null;

     //     _currentViewModel?.Dispose();
     //     _currentViewModel = viewModel;

     //     _currentView = linkedView;
     //     _currentView.DataContext = _currentViewModel;
     //     OnCurrentChanged?.Invoke();
     //}

     //public IPListView IPListView { get; private set; } = new();
     //public void NavToIpList() => Navigate<IPListViewModel>(IPListView);

     //public EventsView EventsView { get; private set; } = new();
     //public void NavToEvents() => Navigate<EventsViewModel>(EventsView);

     //public KeyListView KeyListView { get; private set; } = new();
     //public void NavToKeyList() => Navigate<KeyListViewModel>(KeyListView);

     //public ManualView ManualView { get; private set; } = new();
     //public void NavToManual() => Navigate<ManualViewModel>(ManualView);

     //public SettingsView SettingsView { get; private set; } = new();
     //public void NavToSettings() => Navigate<SettingsViewModel>(SettingsView);

     internal void Window_Closing()
     {
          if (_currentViewModel != null)
               _currentViewModel.ApplyChanges(new[] { ApplyOptions.Await });
     }
}