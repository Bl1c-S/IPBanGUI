using System;
using System.Collections.ObjectModel;
using System.Windows;
using Wpf.Ui.Controls;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;

public class IPUserControlViewModelBase : ViewModelBase
{
     public Visibility BorderVisibility { get; set; } = Visibility.Visible;
     public ObservableCollection<Button> Buttons { get; protected set; } = new();
     public IPStatus? Status { get; protected set; }
     public string Title { get; protected set; }
     public string? Message { get; protected set; }

     public IPUserControlViewModelBase(string ip)
     {
          Title = ip;
     }
     protected virtual void ExecuteAction(Action<string> action, Action<IPUserControlViewModelBase> dispose)
     {
          try
          {
               action.Invoke(Title);
               dispose.Invoke(this);
          }
          catch (Exception ex)
          {
               var copyError = () => { Clipboard.SetText(ex.Message); };
               DialogMessageBox.ActionBox(copyError, Messages.Error, ex.Message, ButtonNames.Copy);
          }
     }
     protected virtual void SetStatus()
     {
          OnPropertyChanged(nameof(Status));
     }

     protected void Copy() => Clipboard.SetText(Title);
}
