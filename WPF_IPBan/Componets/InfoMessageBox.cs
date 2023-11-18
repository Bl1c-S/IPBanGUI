using System;
using System.ComponentModel;
using System.Windows;

namespace WPF_IPBanUtility;

public class InfoMessageBox
{
     Wpf.Ui.Controls.MessageBox _messageBox;
     public InfoMessageBox(string message, string title, string actionLeftButtonName, string closeRightButtonName)
     {
          _messageBox = new Wpf.Ui.Controls.MessageBox();
          _messageBox.Title = title;
          _messageBox.Content = message;
          _messageBox.ButtonLeftName = actionLeftButtonName;
          _messageBox.ButtonRightName = closeRightButtonName;
     }
     public void OpenMassangeBox(Action? action)
     {
          RoutedEventHandler? onLeft = null;
          if (action != null)
          {
               onLeft = ActionInvokeAndCloseEvent(action);
               _messageBox.ButtonLeftClick += onLeft;
          }
          var onRight = CloseEvent();
          _messageBox.ButtonRightClick += onRight;

          _messageBox.Closing += CleanUp(onLeft, onRight);
          _messageBox.ShowDialog();
     }

     private CancelEventHandler CleanUp(RoutedEventHandler? onLeft, RoutedEventHandler onRight)
     {
          return (_, args) =>
          {
               if (onLeft != null)
                    _messageBox.ButtonLeftClick -= onLeft;

               _messageBox.ButtonRightClick -= onRight;
          };
     }

     private RoutedEventHandler CloseEvent() => (_, _) => _messageBox.Close();

     private RoutedEventHandler ActionInvokeAndCloseEvent(Action action) => (_, _) =>
     {
          action.Invoke();
          _messageBox.Close();
     };
}