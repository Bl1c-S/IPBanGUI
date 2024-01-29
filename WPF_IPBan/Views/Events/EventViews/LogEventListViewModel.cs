using Logic_IPBanUtility.Logic.LogFile;
using System.Collections.ObjectModel;

namespace WPF_IPBanUtility;
internal class LogEventListViewModel : ViewModelBase
{
     public ObservableCollection<LogEvent> LogEvents { get; set; }

     public LogEventListViewModel(ObservableCollection<LogEvent> logEvents)
     {
          LogEvents = logEvents;
     }
}