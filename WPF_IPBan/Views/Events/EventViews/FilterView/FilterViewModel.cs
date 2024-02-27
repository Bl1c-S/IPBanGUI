using Logic_IPBanUtility.Logic.LogFile;
using System;
using System.Collections.Generic;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

public class FilterViewModel : ViewModelBase
{
     public string Title { get => PageNames.TypeFilterTitle; }
     public List<LogEvent> ObservebleLogEvent { get; private set; } = new();
     private List<LogEvent> _filteredLogEvents = new();
     private List<LogEvent> _allLogEvents = new();

     public delegate void LogEventHandler(List<LogEvent> logEvents);
     public event LogEventHandler? ObservableLogEventsChanged;

     private LogEventFilter _filter = new();
     private LogEventManager _manager;
     public FilterViewModel(LogEventManager manager)
     {
          _manager = manager;
          FilterKeysBuild();

          Statistics.StatisticsChanged += StaticticsChanged;
          SetSelectedDate(DateTime.Today);
     }

     #region Statistics
     public LogEventStatistics Statistics { get; } = new();
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

     #region Searching
     public string? SearchedText;
     public void SearchLogEvents()
     {
          ObservebleLogEvent = FindLogEventsBySearchedText(_filteredLogEvents);
          ObservableLogEventsChanged?.Invoke(ObservebleLogEvent);
          OnPropertyChanged(nameof(ShowedLogEventCount));
     }
     private List<LogEvent> FindLogEventsBySearchedText(List<LogEvent> logEvents)
     {
          if (SearchedText is null) return logEvents;
          return logEvents.FindAll(x => x.Message.Contains(SearchedText));
     }
     #endregion

     #region DatePicker
     public Action? DaysWithLogChanged { get => _manager.DaysWithLogChanged; set => _manager.DaysWithLogChanged = value; }
     public void SetSelectedDate(DateTime date)
     {
          Clear();
          _selectedDate = date;
          var logs = _manager.GetAllLogEvents(_selectedDate);
          SetAllLogEventsByFiltersAndSearch(logs);
     }

     private DateTime _selectedDate = DateTime.Today;
     private void SetAllLogEventsByFiltersAndSearch(List<LogEvent> logEvents)
     {
          _allLogEvents = logEvents;
          foreach (var key in _filterKeys)
          {
               var findedLogs = FilterLogEventsByType(logEvents, key.Key);
               Statistics.AddEvents(key.Key, findedLogs.Count);
               if (key.Value.IsEnable)
                    AddObservableLogEvents(findedLogs);
          }
     }
     #endregion

     #region FilterKeys
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
          _filterKeys.Add(filteType, filterKey);
     }

     public FilterKey LoginSucceeded { get => _filterKeys[LogEventType.LoginSucceeded]; }
     public FilterKey LoginFailure { get => _filterKeys[LogEventType.LoginFailure]; }
     public FilterKey ForgetFailedLogin { get => _filterKeys[LogEventType.ForgetFailedLogin]; }
     public FilterKey BanningIP { get => _filterKeys[LogEventType.BanningIP]; }
     public FilterKey UnBanningIP { get => _filterKeys[LogEventType.UnBanningIP]; }
     public FilterKey FirewallEntriesUpdated { get => _filterKeys[LogEventType.FirewallEntriesUpdated]; }
     #endregion

     #region ApplyFilter
     private void ApplyFilter(bool state, LogEventType type)
     {
          if (state) AddLogEventByType(type);
          else RemoveLogEventsByType(type);
     }
     private int AddLogEventByType(LogEventType type)
     {
          return ProcessLogEventsByType(_allLogEvents, type);
     }
     private void RemoveLogEventsByType(LogEventType type)
     {
          _filteredLogEvents = _filter.RemoveLogEventsByType(_filteredLogEvents, type);
          ObservebleLogEvent = _filter.RemoveLogEventsByType(ObservebleLogEvent, type);
          SortObservebleLogEvents();
     }
     #endregion

     #region NewLogsReading
     public void ReadNewLogs()
     {
          var newLogEvents = _manager.GetNewLogEvents(_selectedDate);
          if (newLogEvents.Count != 0)
               AddNewLogEventsByFiltersAndSearch(newLogEvents);
     }
     private void AddNewLogEventsByFiltersAndSearch(List<LogEvent> newLogEvents)
     {
          foreach (var key in _filterKeys)
               if (key.Value.IsEnable)
               {
                    var processCount = ProcessLogEventsByType(newLogEvents, key.Key);
                    Statistics.AddEvents(key.Key, processCount);
               }
     }
     #endregion

     #region ProcessLogEvents
     private int ProcessLogEventsByType(List<LogEvent> logEvents, LogEventType type)
     {
          var findedLogs = FilterLogEventsByType(logEvents, type);
          AddObservableLogEvents(findedLogs);
          return findedLogs.Count;
     }
     private List<LogEvent> FilterLogEventsByType(List<LogEvent> newLogEvents, LogEventType type)
     {
          var findedLogs = _filter.FindEventsByType(newLogEvents, type);
          _filteredLogEvents.AddRange(findedLogs);
          return findedLogs;
     }
     private void AddObservableLogEvents(List<LogEvent> logEvents)
     {
          ApplyObservableLogEventsBySearch(logEvents);
          SortObservebleLogEvents();
     }
     private void ApplyObservableLogEventsBySearch(List<LogEvent> logEvents)
     {
          var searchedLogEvents = FindLogEventsBySearchedText(logEvents);
          ObservebleLogEvent.AddRange(searchedLogEvents);
     }
     #endregion

     #region Under services
     private void SortObservebleLogEvents()
     {
          ObservebleLogEvent.Sort((x, y) => x.Id.CompareTo(y.Id));
          ObservableLogEventsChanged?.Invoke(ObservebleLogEvent);
          OnPropertyChanged(nameof(ShowedLogEventCount));
     }
     private void Clear()
     {
          _filteredLogEvents.Clear();
          ObservebleLogEvent.Clear();
          Statistics.Clear();
     }
     public override void Dispose()
     {
          Statistics.StatisticsChanged -= StaticticsChanged;
          base.Dispose();
     }
     #endregion
}