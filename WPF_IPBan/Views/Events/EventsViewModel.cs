using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Logic.LogFile;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Button = Wpf.Ui.Controls.Button;

namespace WPF_IPBanUtility;

public class EventsViewModel : PageViewModelBase
{
     public readonly LogEventManager _logEventManager;
     public LogEventListViewModel LogEventListVM { get; }
     public FilterViewModel FilterVM { get; }

     public EventsViewModel(LogEventManager logEventManager) : base(Properties.PageNames.Events)
     {
          _logEventManager = logEventManager;
          FilterVM = new FilterViewModel(logEventManager);
          LogEventListVM = new LogEventListViewModel(FilterVM.ObservebleLogEvents);

          FilterVM.ObservableLogEventsChanged += LogEventListVM.ObservableLogEventsSet;
          FilterVM.DaysWithLogChanged += UpdateDate;

          IUpdateCommand = new RelayCommand(UpdateAll);
          IFilterCommand = new RelayCommand(ChangeFilterVisibility);
          ISearchCommand = new RelayCommand(FilterVM.SearchLogEvents);

          CreatePageButtons();
     }

     #region Update
     public ICommand IUpdateCommand { get; }
     private void UpdateAll()
     {
          FilterVM.CheckDaysWithLogsChanged();
          UpdateLogsEventsForSelectedDate();
          UpdateDate();
     }
     private void UpdateLogsEventsForSelectedDate() => FilterVM.ReadNewLogs();
     #endregion

     #region Filter
     public ICommand IFilterCommand { get; }
     public Visibility FilterVisibility { get; private set; }= Visibility.Collapsed;
     private bool _filterViewVisibility = false;
     private void ChangeFilterVisibility()
     {
          _filterViewVisibility = !_filterViewVisibility;
          if (_filterViewVisibility)
               FilterVisibility = Visibility.Visible;
          else
               FilterVisibility = Visibility.Collapsed;
          OnPropertyChanged(nameof(FilterVisibility));
     }
     #endregion

     #region Search
     public string? SearchText { get => FilterVM.SearchedText; set => FilterVM.SearchedText = value; }
     public ICommand ISearchCommand { get; }
     #endregion

     #region DatePicker
     public DateTime SelectedDate { get => _selectedDate;
          set
          {
               _selectedDate = value;
               OnPropertyChanged(nameof(SelectedDate));
               FilterVM.SetSelectedDate(value);
               OnPropertyChanged(nameof(SelectedDate));
          }
     }
     private DateTime _selectedDate => FilterVM.SelectedDate;
     public DateTime DateStart => FilterVM.SelectableDateRangeStart;
     public DateTime DateEnd => FilterVM.SelectableDateRangeEnd;

     private void UpdateDate()
     {
          OnPropertyChanged(nameof(SelectedDate));
          OnPropertyChanged(nameof(DateStart));
          OnPropertyChanged(nameof(DateEnd));
     }
     #endregion

     protected override void CreatePageButtons()
     {
          PageButtons.Add(new Button
          {
               Content = Properties.ButtonNames.Update,
               Command = IUpdateCommand,
               Icon = Wpf.Ui.Common.SymbolRegular.ArrowSync24
          });

          PageButtons.Add(new Button
          {
               Content = Properties.ButtonNames.Filter,
               Command = IFilterCommand,
               Icon = Wpf.Ui.Common.SymbolRegular.Filter24,
               Margin = new(4, 0, 0, 0)
          });
     }

     public override void Dispose()
     {
          FilterVM.ObservableLogEventsChanged -= LogEventListVM.ObservableLogEventsSet;
          base.Dispose();
     }
}
