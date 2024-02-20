using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Logic.LogFile.Services;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

internal class EventsViewModel : PageViewModelBase
{
     public LogEventListViewModel LogEventListVM { get; }
     public FilterViewModel FilterVM { get; }

     public EventsViewModel(LogFileManager logManager) : base(Properties.PageNames.Events)
     {
          FilterVM = new FilterViewModel(logManager);
          LogEventListVM = new LogEventListViewModel(logManager.AllLogEvents);

          FilterVM.ObservableLogEventsChanged += LogEventListVM.ObservableLogEventsSet;

          IUpdateCommand = new RelayCommand(UpdateLogEvents);
          IFilterCommand = new RelayCommand(ChangeFilterVisibility);
          ISearchCommand = new RelayCommand(FilterVM.SearchLogEventByText);

          CreatePageButtons();
     }

     #region Update
     public ICommand IUpdateCommand { get; }
     private void UpdateLogEvents()
     {
          FilterVM.ReadNewLogs();
     }
     #endregion

     #region Filter
     public ICommand IFilterCommand { get; }
     public Visibility FilterVisibility { get; private set; }= Visibility.Collapsed;
     private bool _isEnableVisibility = false;
     private void ChangeFilterVisibility()
     {
          _isEnableVisibility = !_isEnableVisibility;
          if (_isEnableVisibility)
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
          base.Dispose();
     }
}
