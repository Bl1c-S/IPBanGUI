using Logic_IPBanUtility.Logic.LogFile;
using System.Collections.Generic;

namespace WPF_IPBanUtility;

internal class LogEventsViewModel : ViewModelBase
{
     List<LogEvent> LogEvents;
     
     public LogEventsViewModel(List<LogEvent> logEvents)
     {
          LogEvents = logEvents;
     }
}
