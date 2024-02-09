using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Logic.LogFile.Models;
using Logic_IPBanUtility.Logic.LogFile.Services;
using System;
using System.Collections.Generic;
using System.Numerics;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

internal class FilterViewModel : ViewModelBase
{
     public string Title { get => PageNames.TypeFilterTitle; }

     public delegate void LogEventHandler(List<LogEvent> logEvents);
     public event LogEventHandler? ObservableLogEventsSet;
     public event LogEventHandler? NewObservableLogEventsAdded;

     private delegate void FilterChangedDelegate(FilterKey filterKey);
     private LogEventFilter _filter = new();
     private LogEventManager _manager;
     private Dictionary<LogEventType, FilterKey> _filterKeys = new();

     public List<LogEvent> LogEvents { get; private set; }

     public FilterViewModel(LogEventManager manager)
     {
          _manager = manager;
          LogEvents = new(_manager.LogEvents);
          FilterKeysBuild();
     }

     private void FilterKeysBuild()
     {
          AddToFilterKeys(FilterKeys.LoginSucceeded, LogEventType.LoginSucceeded);
          AddToFilterKeys(FilterKeys.LoginFailure, LogEventType.LoginFailure);
          AddToFilterKeys(FilterKeys.ForgetFailedLogin, LogEventType.ForgetFailedLogin);
          AddToFilterKeys(FilterKeys.BanningIP, LogEventType.BanningIP);
          AddToFilterKeys(FilterKeys.UnBanningIP, LogEventType.UnBanningIP);
          AddToFilterKeys(FilterKeys.FirewallEntriesUpdated, LogEventType.FirewallEntriesUpdated);
     }
     private void AddToFilterKeys(string name, LogEventType filteType)
     {
          var filterKey = new FilterKey(name, filteType, ApplyFilterToLogEventsList);
          _filterKeys.Add(filterKey.Type, filterKey);
     }

     public FilterKey LoginSucceeded { get => _filterKeys[LogEventType.LoginSucceeded]; }
     public FilterKey LoginFailure { get => _filterKeys[LogEventType.LoginFailure]; }
     public FilterKey ForgetFailedLogin { get => _filterKeys[LogEventType.ForgetFailedLogin]; }
     public FilterKey BanningIP { get => _filterKeys[LogEventType.BanningIP]; }
     public FilterKey UnBanningIP { get => _filterKeys[LogEventType.UnBanningIP]; }
     public FilterKey FirewallEntriesUpdated { get => _filterKeys[LogEventType.FirewallEntriesUpdated]; }

     private void ApplyFilterToLogEventsList(FilterKey changedFilterKey)
     {
          if (changedFilterKey.IsEnable)
          {
               var findedLogs = _filter.FindEventsByType(_manager.LogEvents, changedFilterKey.Type);
               LogEvents.AddRange(findedLogs);
          }
          else
               LogEvents = _filter.RemoveEventsByType(LogEvents, changedFilterKey.Type);
          ObservableLogEventsSet?.Invoke(LogEvents);
     }
     public void ReadNewLogs()
     {
          var newLogEvents = _manager.ReadNewLogEvents();

          foreach (var key in _filterKeys)
          {
               if (!key.Value.IsEnable)
                    continue;

               var findedLogs = _filter.FindEventsByType(newLogEvents, key.Key);
               LogEvents.AddRange(findedLogs);
          }
          NewObservableLogEventsAdded?.Invoke(LogEvents);
     }
}