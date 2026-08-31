using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Input;
using System.Windows;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

public class ViewModelBase : ObservableObject, IDisposable
{
     protected Button CreateButton(ICommand command, SymbolRegular icon, string toolTip, Thickness? margin = null)
     {
          return new()
          {
               Command = command,
               Icon = new SymbolIcon(icon),
               ToolTip = toolTip,
               Margin = margin ?? new(0)
          };
     }
     protected virtual Button CreateButtonWithTitle(ICommand command, SymbolRegular icon, string title, string toolTip = "", Thickness? margin = null)
     {
          return new Button
          {
               Content = title,
               Command = command,
               Icon = new SymbolIcon(icon),
               ToolTip = toolTip,
               Margin = margin ?? new(0),
          };
     }
     public virtual void Dispose()
     {
     }
}
