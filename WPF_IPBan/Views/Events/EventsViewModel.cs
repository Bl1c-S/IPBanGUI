using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility;
using Logic_IPBanUtility.Logic.LogFile;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

internal class EventsViewModel : PageViewModelBase
{
     public LogEventListViewModel logEventListViewModel { get; }
     public FilterViewModel filterViewModel { get; }

     public EventsViewModel(LogEventManager logManager) : base(Properties.PageNames.Events)
     {
          filterViewModel = new FilterViewModel(logManager);
          logEventListViewModel = new LogEventListViewModel(logManager.LogEvents);
          filterViewModel.ObservableLogEventsSet += logEventListViewModel.ObservableLogEventsSet;
          filterViewModel.NewObservableLogEventsAdded += logEventListViewModel.NewObservableLogEventsAdded;

          IUpdateCommand = new RelayCommand(UpdateLogEvents);
          IFilterCommand = new RelayCommand(ChangeFilterVisibility);
          FilterVisibility = Visibility.Collapsed;

          CreatePageButtons();
     }

     public ICommand IUpdateCommand { get; }
     private void UpdateLogEvents()
     {
          filterViewModel.ReadNewLogs();
          logEventListViewModel.ObservableLogEventsSet(filterViewModel.LogEvents);
     }

     public ICommand IFilterCommand { get; }
     public Visibility FilterVisibility { get; private set; }
     private bool _isEnableVisibility = false;
     private void ChangeFilterVisibility()
     {
          _isEnableVisibility = !_isEnableVisibility;
          if (_isEnableVisibility)
               FilterVisibility = Visibility.Visible;
          else
               FilterVisibility = Visibility.Collapsed;
          OnPropertyChanged(nameof(FilterVisibility));
     }

     protected override void CreatePageButtons()
     {
          PageButtons.Add(new Button
          {
               Content = Properties.ButtonNames.Update,
               Command = IUpdateCommand,
               Icon = Wpf.Ui.Common.SymbolRegular.ArrowSync24
          });

          PageButtons.Add(new Button
          {
               Content = Properties.ButtonNames.Filter,
               Command = IFilterCommand,
               Icon = Wpf.Ui.Common.SymbolRegular.Filter24,
               Margin = new(4, 0, 0, 0)
          });
     }

     public override void Dispose()
     {
          filterViewModel.ObservableLogEventsSet -= logEventListViewModel.ObservableLogEventsSet;
          filterViewModel.NewObservableLogEventsAdded -= logEventListViewModel.NewObservableLogEventsAdded;
          base.Dispose();
     }
}
