using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Logic.LogFile;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

internal class EventsViewModel : PageViewModelBase
{
     public LogEventManager LogManager { get; }
     public ObservableCollection<LogEvent> LogEvents { get; set; }
     public bool LogEventIsEmpty => LogEvents.Count == 0;

     public List<ViewModelBase> VMs => _vMs;
     private List<ViewModelBase> _vMs;

     public EventsViewModel(LogEventManager logManager) : base(Properties.PageNames.Events)
     {
          LogManager = logManager;
          LogEvents = new(LogManager.ReadAllLogEvents());

          _vMs = new() { new LogEventListViewModel(LogEvents)};

          IUpdateCommand = new RelayCommand(UpdateLogEvents);
          CreatePageButtons();
     }

     public ICommand IUpdateCommand { get; }
     private void UpdateLogEvents()
     {
          var newLogs = LogManager.ReadNewLogEvents();
          if (newLogs.Count == 0)
               return;
          foreach (var logEvent in newLogs)
               LogEvents.Add(logEvent);
          OnPropertyChanged(nameof(LogEvents));
     }



     protected override void CreatePageButtons()
     {
          PageButtons.Add(new Button
          {
               Content = Properties.ButtonNames.Update,
               Command = IUpdateCommand,
               Icon = Wpf.Ui.Common.SymbolRegular.ArrowSync24
          });
     }
}
