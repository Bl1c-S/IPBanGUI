using System;
using System.Windows.Controls;
using Wpf.Ui.Common;

namespace WPF_IPBanUtility.Services
{
     internal class PageRegistration
     {
          public string Name { get; }
          public string ToolTip { get; }
          public SymbolRegular Icon { get; }
          public Type ViewModelType { get; }
          public Func<UserControl> ViewFactory { get; }

          public PageRegistration(string name,
               SymbolRegular icon,
               Type viewModelType,
               Func<UserControl> viewFactory,
               string? toolTip = null)
          {
               Name = name;
               Icon = icon;
               ViewModelType = viewModelType;
               ViewFactory = viewFactory;
               ToolTip = toolTip ?? name;
          }
     }
}
