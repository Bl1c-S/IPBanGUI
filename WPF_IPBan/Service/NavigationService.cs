using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;

namespace WPF_IPBanUtility;

internal class NavigationService
{
     private readonly IServiceProvider _serviceProvider;
     private UserControl? _currentView;
     private PageViewModelBase? _currentViewModel;

     public event Action? OnCurrentChanged;
     public PageViewModelBase? CurrentViewModel => _currentViewModel;
     public UserControl? CurrentView => _currentView;

     public NavigationService(IServiceProvider serviceProvider)
     {
          _serviceProvider = serviceProvider;
     }

     public void Navigate<T>(UserControl linkedView) where T : PageViewModelBase
     {
          if (_currentViewModel != null && _currentViewModel is T) return;

          if (_currentViewModel != null) _currentViewModel.ApplyChanges();

          var viewModel = _serviceProvider.GetService<T>() as PageViewModelBase;
          if (viewModel is null) return;

          if (_currentView != null) _currentView.DataContext = null;

          _currentViewModel?.Dispose();
          _currentViewModel = viewModel;

          _currentView = linkedView;
          _currentView.DataContext = _currentViewModel;
          OnCurrentChanged?.Invoke();
     }

     public IPListView IPListView { get; private set; } = new();
     public void NavToIpList() => Navigate<IPListViewModel>(IPListView);

     public EventsView EventsView { get; private set; } = new();
     public void NavToEvents() => Navigate<EventsViewModel>(EventsView);

     public KeyListView KeyListView { get; private set; } = new();
     public void NavToKeyList() => Navigate<KeyListViewModel>(KeyListView);

     public ManualView ManualView { get; private set; } = new();
     public void NavToManual() => Navigate<ManualViewModel>(ManualView);

     public SettingsView SettingsView { get; private set; } = new();
     public void NavToSettings() => Navigate<SettingsViewModel>(SettingsView);

     internal void Window_Closing()
     {
          if (_currentViewModel != null)
               _currentViewModel.ApplyChanges(new[] { ApplyOptions.Await });
     }
}