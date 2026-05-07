using Logic_IPBanUtility.Logic.LogFile;
using NLog.Filters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

public class FilterViewModel : ViewModelBase
{
     public void SetToday(DateTime today) =>_manager.Today = today; //Для тестів
     public string Title { get => PageNames.TypeFilterTitle; }
     public Action CheckDaysWithLogsChanged => _manager.CheckDaysWithLogsChanged;
     public List<LogEvent> ObservebleLogEvents { get; private set; } = new();
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

          _manager.DaysWithLogChanged += UpdateSelectableDateRange;
          _manager.TodayChanged += TodayChanged;

          Statistics.StatisticsChanged += StaticticsChanged;
          UpdateSelectableDateRange();
          SetSelectedDate(DateTime.Today);
     }

     #region Statistics
     public LogEventStatistics Statistics { get; } = new();
     public string ShowedLogEventTitle { get => FilterKeys.ShowedLogEvents; }
     public int ShowedLogEventCount { get => ObservebleLogEvents.Count; }
     public string AllLogEvent { get => $"{FilterKeys.AllLogEvents}  {Statistics.AllLogEvent}"; }
     private void StaticticsChanged()
     {
          foreach (var filter in Filters)
          {
               filter.Count = Statistics.Get(filter.Type);
          }

          OnPropertyChanged(nameof(ShowedLogEventCount));
          OnPropertyChanged(nameof(AllLogEvent));
     }    
     #endregion

     #region Searching
     public string? SearchedText;
     public void SearchLogEvents()
     {
          ObservebleLogEvents = FindLogEventsBySearchedText(_filteredLogEvents);
          ObservableLogEventsChanged?.Invoke(ObservebleLogEvents);
          OnPropertyChanged(nameof(ShowedLogEventCount));
     }
     private List<LogEvent> FindLogEventsBySearchedText(List<LogEvent> logEvents)
     {
          if (SearchedText is null) return logEvents;
          return logEvents.FindAll(x => x.Message.Contains(SearchedText, StringComparison.OrdinalIgnoreCase));
     }
     #endregion

     #region DatePicker
     public DateTime SelectedDate { get; private set; }
     public DateTime SelectableDateRangeStart { get; private set; }
     public DateTime SelectableDateRangeEnd { get; private set; }
     public Action? DaysWithLogChanged { get => _manager.DaysWithLogChanged; set => _manager.DaysWithLogChanged = value; }
     public void SetSelectedDate(DateTime date)
     {
          Clear();
          _manager.CheckDaysWithLogsChanged();

          if (SelectableDateRangeStart > date)
               SelectedDate = SelectableDateRangeStart;
          else SelectedDate = date;

          var logs = _manager.GetLogEvents(SelectedDate);
          AddLogEventsByFiltersAndSearch(logs);
     }
     public void UpdateSelectableDateRange()
     {
          var DateWithLogsRange = _manager.CurrentDayWithLogs;
          if (DateWithLogsRange.Count > 1)
          {
               SelectableDateRangeStart = DateWithLogsRange.LastOrDefault();
               SelectableDateRangeEnd = DateWithLogsRange.FirstOrDefault();
          }
          else
          {
               SelectableDateRangeStart = DateTime.Today;
               SelectableDateRangeEnd = DateTime.Today;
          }
     }

     private void AddLogEventsByFiltersAndSearch(List<LogEvent> logEvents)
     {
          _allLogEvents.AddRange(logEvents);
          foreach (var key in Filters)
          {
               var findedLogs = FilterLogEventsByType(logEvents, key.Type);
               Statistics.AddEvents(key.Type, findedLogs.Count);
               if (key.IsEnable)
                    AddObservableLogEvents(findedLogs);
          }
     }
     #endregion

     #region FilterKeys
     public ObservableCollection<FilterKey> Filters { get; } = new();

     private void FilterKeysBuild()
     {
          AddToFilterKeys(FilterKeys.LoginSucceeded, true, LogEventType.LoginSucceeded);
          AddToFilterKeys(FilterKeys.LoginFailure, true, LogEventType.LoginFailure);
          AddToFilterKeys(FilterKeys.ForgetFailedLogin, false, LogEventType.ForgetFailedLogin);
          AddToFilterKeys(FilterKeys.BanningIP, true, LogEventType.BanningIP);
          AddToFilterKeys(FilterKeys.UnBanningIP, false, LogEventType.UnBanningIP);
          AddToFilterKeys(FilterKeys.FirewallEntriesUpdated, false, LogEventType.FirewallEntriesUpdated);
     }
     private void AddToFilterKeys(string name, bool isEnable, LogEventType filteType)
     {
          var filter = new FilterKey(name, isEnable, filteType);
          filter.PropertyChanged += OnFilterChanged;
          Filters.Add(filter);
     }
     private void OnFilterChanged(object? sender, PropertyChangedEventArgs e)
     {
          if (e.PropertyName == nameof(FilterKey.IsEnable))
          {
               if (sender is FilterKey filter)
               {
                    ApplyFilter(filter.IsEnable, filter.Type);
               }
          }
     }

     #endregion

     #region ApplyFilter
     private void ApplyFilter(bool state, LogEventType type)
     {
          if (state) ProcessLogEventsByType(_allLogEvents, type);
          else RemoveLogEventsByType(type);
     }
     private void RemoveLogEventsByType(LogEventType type)
     {
          _filteredLogEvents = _filter.RemoveLogEventsByType(_filteredLogEvents, type);
          ObservebleLogEvents = _filter.RemoveLogEventsByType(ObservebleLogEvents, type);
          SortObservebleLogEvents();
     }
     #endregion

     #region Update
     private void TodayChanged()
     {
          Clear();
          UpdateSelectableDateRange();
          var logs = _manager.GetLogEvents(SelectedDate);
          AddLogEventsByFiltersAndSearch(logs);
     }
     public void ReadNewLogs()
     {
          _manager.CheckDaysWithLogsChanged();
          var newLogEvents = _manager.GetLogEvents(SelectedDate, false);
          AddLogEventsByFiltersAndSearch(newLogEvents);
     }
     #endregion

     #region ProcessLogEvents
     private void ProcessLogEventsByType(List<LogEvent> logEvents, LogEventType type)
     {
          var findedLogs = FilterLogEventsByType(logEvents, type);
          AddObservableLogEvents(findedLogs);
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
          ObservebleLogEvents.AddRange(searchedLogEvents);
     }
     #endregion

     #region Under services
     private void SortObservebleLogEvents()
     {
          ObservebleLogEvents.Sort((x, y) => x.Id.CompareTo(y.Id));
          ObservableLogEventsChanged?.Invoke(ObservebleLogEvents);
          OnPropertyChanged(nameof(ShowedLogEventCount));
     }
     private void Clear()
     {
          _allLogEvents.Clear();
          _filteredLogEvents.Clear();
          ObservebleLogEvents.Clear();
          Statistics.Clear();
     }
     public override void Dispose()
     {
          _manager.DaysWithLogChanged -= UpdateSelectableDateRange;
          _manager.TodayChanged -= TodayChanged;

          foreach (var filter in Filters)
               filter.PropertyChanged -= OnFilterChanged;
          
          Statistics.StatisticsChanged -= StaticticsChanged;
          base.Dispose();
     }
     #endregion
}