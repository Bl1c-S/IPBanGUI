using Wpf.Ui.Controls;

namespace WPF_IPBanUtility.Components.NavigateBar;

internal class NavItem
     : ViewModelBase
{
     public NavItem(Button button)
     {
          Button = button;
          Title = button.Content?.ToString() ?? string.Empty;
     }

     public Button Button { get; }

     public string Title { get; }

     private string? _currentPageName;

     public string? CurrentPageName
     {
          get => _currentPageName;
          set => SetProperty(ref _currentPageName, value);
     }
}
