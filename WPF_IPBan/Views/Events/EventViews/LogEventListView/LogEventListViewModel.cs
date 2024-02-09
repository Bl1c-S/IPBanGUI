using Logic_IPBanUtility.Logic.LogFile;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF_IPBanUtility;
internal class LogEventListViewModel : ViewModelBase
{
     public ObservableCollection<LogEvent> LogEvents { get; set; }

     public LogEventListViewModel(List<LogEvent> logEvents)
     {
          LogEvents = new(logEvents);
     }

     public void ObservableLogEventsSet(List<LogEvent> logEvents)
     {
          LogEvents = new(logEvents);
          OnPropertyChanged(nameof(LogEvents));
     }

     public void NewObservableLogEventsAdded(List<LogEvent> logEvents)
     {
          foreach (var logEvent in logEvents)
               LogEvents.Add(logEvent);
          OnPropertyChanged(nameof(LogEvents));
     }
}