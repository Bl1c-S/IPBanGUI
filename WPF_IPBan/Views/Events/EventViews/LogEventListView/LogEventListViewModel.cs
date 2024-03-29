﻿using Logic_IPBanUtility.Logic.LogFile;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF_IPBanUtility;
public class LogEventListViewModel : ViewModelBase
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
}