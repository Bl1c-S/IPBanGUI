﻿using Logic_IPBanUtility.Logic.LogFile;
using System;
using System.Collections.Generic;
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
          OnPropertyChanged(nameof(Statistics));
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
          AddToFilterKeys(FilterKeys.LoginSucceeded, true, LogEventType.LoginSucceeded);
          AddToFilterKeys(FilterKeys.LoginFailure, true, LogEventType.LoginFailure);
          AddToFilterKeys(FilterKeys.ForgetFailedLogin, false, LogEventType.ForgetFailedLogin);
          AddToFilterKeys(FilterKeys.BanningIP, true, LogEventType.BanningIP);
          AddToFilterKeys(FilterKeys.UnBanningIP, false, LogEventType.UnBanningIP);
          AddToFilterKeys(FilterKeys.FirewallEntriesUpdated, false, LogEventType.FirewallEntriesUpdated);
     }
     private void AddToFilterKeys(string name, bool isEnable, LogEventType filteType)
     {
          var filterKey = new FilterKey(name, isEnable, filteType, ApplyFilter);
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
          Statistics.StatisticsChanged -= StaticticsChanged;
          base.Dispose();
     }
     #endregion
}