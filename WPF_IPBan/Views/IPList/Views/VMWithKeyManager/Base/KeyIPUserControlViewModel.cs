using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;

public class KeyIPUserControlViewModel : IPUserControlViewModelBase
{
     public KeyIPUserControlViewModel(string ip, IPStatus status, Action<string> remove, Action<IPUserControlViewModelBase> dispose) : base(ip)
     {
          IRemoveCommand = new RelayCommand(() => ExecuteAction(remove, dispose));
          ICopyCommand = new RelayCommand(Copy);

          SetStatus(status);
          Buttons = CreateButtons();
     }
     private void SetStatus(IPStatus status)
     {
          Status = status;
          base.SetStatus();
     }
     private ObservableCollection<Button> CreateButtons()
     {
          Thickness margin = new(2);
          return new()
               {
                   new Button
                   {
                        Command = ICopyCommand,
                        Icon = SymbolRegular.Copy20,
                        ToolTip = ToolTips.Copy,
                        Margin = margin
                   },
                   new Button
                   {
                        Command = IRemoveCommand,
                        Icon = SymbolRegular.Delete20,
                        ToolTip = ToolTips.RemoveFromList,
                        Margin = margin
                   }
               };
     }

     public ICommand IRemoveCommand { get; }
     public ICommand ICopyCommand { get; }
}
