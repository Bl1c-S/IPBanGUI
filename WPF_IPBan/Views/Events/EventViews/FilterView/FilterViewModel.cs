using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Logic.LogFile.Models;
using Logic_IPBanUtility.Logic.LogFile.Services;
using System.Collections.Generic;
using System.Linq;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

internal class FilterViewModel : ViewModelBase
{
     public string Title { get => PageNames.TypeFilterTitle; }
     public List<LogEvent> ObservebleLogEvent { get; private set; }
     private List<LogEvent> _filteredLogEvents;

     public delegate void LogEventHandler(List<LogEvent> logEvents);
     public event LogEventHandler? ObservableLogEventsChanged;

     private LogEventFilter _filter = new();
     private LogFileManager _manager;
     public FilterViewModel(LogFileManager manager)
     {
          _manager = manager;
          _filteredLogEvents = new(_manager.LogEvents);
          ObservebleLogEvent = new(_manager.LogEvents);
          FilterKeysBuild();
     }

     #region Statistics
     public LogEventStatistics Statistics { get => _manager.Statistics; }
     public string ShowedLogEventTitle { get => FilterKeys.ShowedLogEvents; }
     public int ShowedLogEventCount { get => ObservebleLogEvent.Count; }
     public string AllLogEvent { get => $"{FilterKeys.AllLogEvents}  {Statistics.AllLogEvent}"; }
     private void StaticticsChanged()
     {
          OnPropertyChanged(nameof(Statistics));
          OnPropertyChanged(nameof(ShowedLogEventCount));
          OnPropertyChanged(nameof(AllLogEvent));
     }
     #endregion

     #region FilterKey
     private Dictionary<LogEventType, FilterKey> _filterKeys = new();
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
          var filterKey = new FilterKey(name, filteType, ApplyFilter);
          _filterKeys.Add(filterKey.Type, filterKey);
     }

     public FilterKey LoginSucceeded { get => _filterKeys[LogEventType.LoginSucceeded]; }
     public FilterKey LoginFailure { get => _filterKeys[LogEventType.LoginFailure]; }
     public FilterKey ForgetFailedLogin { get => _filterKeys[LogEventType.ForgetFailedLogin]; }
     public FilterKey BanningIP { get => _filterKeys[LogEventType.BanningIP]; }
     public FilterKey UnBanningIP { get => _filterKeys[LogEventType.UnBanningIP]; }
     public FilterKey FirewallEntriesUpdated { get => _filterKeys[LogEventType.FirewallEntriesUpdated]; }
     #endregion

     #region Search
     public string? SearchedText;
     public void SearchLogEventByText()
     {
          if (SearchedText != null)
               ObservebleLogEvent = _filteredLogEvents.FindAll(x => x.Message.Contains(SearchedText));
          else
               ObservebleLogEvent = _filteredLogEvents;

          ObservableLogEventsChanged?.Invoke(ObservebleLogEvent);
          StaticticsChanged();
     }
     #endregion

     #region ApplyFilter
     private void ApplyFilter(FilterKey changedFilterKey)
     {
          if (changedFilterKey.IsEnable)
               AddFilteredByEventType(changedFilterKey.Type);
          else RemoveFilteredByEventType(changedFilterKey.Type);

          ObservableLogEventsChanged?.Invoke(ObservebleLogEvent);
          StaticticsChanged();
     }
     private void AddFilteredByEventType(LogEventType type)
     {
          var findedLogs = _filter.FindEventsByType(_manager.LogEvents, type).ToList();
          _filteredLogEvents.AddRange(findedLogs);
          ApplyLogEventsBySearch(findedLogs);
          SortObservebleLogEvents();
          StaticticsChanged();
     }
     private void RemoveFilteredByEventType(LogEventType type)
     {
          _filteredLogEvents = _filter.RemoveEventsByType(_filteredLogEvents, type);
          ObservebleLogEvent = _filter.RemoveEventsByType(ObservebleLogEvent, type);
     }
     #endregion

     #region NewLogs
     public void ReadNewLogs()
     {
          var newLogEvents = _manager.ReadNewLogEvents();
          if (newLogEvents.Count == 0) return;

          AddNewLogEventsByFiltersAndSearch(newLogEvents);
          SortObservebleLogEvents();
          StaticticsChanged();
          ObservableLogEventsChanged?.Invoke(ObservebleLogEvent);
     }
     private void AddNewLogEventsByFiltersAndSearch(List<LogEvent> newLogEvents)
     {
          foreach (var key in _filterKeys)
          {
               if (!key.Value.IsEnable) continue;

               var findedLogs = _filter.FindEventsByType(newLogEvents, key.Key).ToList();
               _filteredLogEvents.AddRange(findedLogs);
               ApplyLogEventsBySearch(findedLogs);
          }
     }
     #endregion

     #region Under services
     private void ApplyLogEventsBySearch(List<LogEvent> logEvents)
     {
          if (SearchedText is null)
               ObservebleLogEvent.AddRange(logEvents);
          else
          {
               var findedLogs = logEvents.FindAll(x => x.Message.Contains(SearchedText));
               ObservebleLogEvent.AddRange(findedLogs);
          }
     }
     private void SortObservebleLogEvents() => ObservebleLogEvent.Sort((x, y) => x.Id.CompareTo(y.Id));
     #endregion
}