using System;
using System.Windows.Input;
using Wpf.Ui.Common;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility.Componets.NavigateBar
{
     internal class NavButtonModel : ViewModelBase
     {
          public string Name { get; }
          public SymbolRegular Icon { get; }
          public ICommand Command { get; }
          public string ToolTip { get; }
          public string ActiveName => IsActive ? Name : string.Empty;
          public string ActiveBorder => IsActive ? Collors.Active : Collors.InActive;
          public bool IsActive => _getCurrentPage() == Name;

          private readonly Func<string?> _getCurrentPage;

          public NavButtonModel(string name, SymbolRegular icon, ICommand command, Func<string?> getCurrentPage, string toolTip)
          {
               Name = name;
               Icon = icon;
               Command = command;
               _getCurrentPage = getCurrentPage;
               ToolTip = toolTip;
          }
          public void RefreshIsActive()
          {
               OnPropertyChanged(nameof(IsActive));
               OnPropertyChanged(nameof(ActiveName));
               OnPropertyChanged(nameof(ActiveBorder));
          }
     }
}
