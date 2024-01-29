using Logic_IPBanUtility.Logic.LogFile;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF_IPBanUtility;

internal class EventsViewModel : PageViewModel
{
     public LogEventManager LogManager { get; }
     public ObservableCollection<LogEvent> LogEvents { get; set; }
     public bool LogEventIsEmpty => LogEvents.Count == 0;

     public List<ViewModelBase> VMs => _vMs;
     private List<ViewModelBase> _vMs;

     public EventsViewModel(LogEventManager logManager) : base(Properties.PageName.Events)
     {
          LogManager = logManager;
          LogEvents = new(LogManager.ReadAllLogEvents());

          _vMs = new() {
               new LogEventListViewModel(LogEvents)
          };
     }

}
