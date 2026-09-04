using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MbResult = Wpf.Ui.Controls.MessageBoxResult;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace WPF_IPBanUtility;

public static class DialogMessageBox
{
     public static async Task TwoActionBoxAsync(
          Action primaryAction,
          Action secondaryAction,
          string message,
          string title,
          string primaryActionName,
          string secondaryActionName)
     {
          var textContent = new TextBlock
          {
               Text = message,
               TextWrapping = TextWrapping.Wrap
          };

          var messageBox = new MessageBox
          {
               PrimaryButtonText = primaryActionName,
               CloseButtonText = secondaryActionName,
               Title = title,
               Content = textContent
          };

          var result = await messageBox.ShowDialogAsync();
          if (result == MbResult.Primary)
               primaryAction?.Invoke();
          else
               secondaryAction?.Invoke();
     }

     public static async Task ActionBoxAsync(
          Action action,
          string message,
          string title,
          string primaryActionName,
          string secondaryActionName)
     {
          var textContent = new TextBlock
          {
               Text = message,
               TextWrapping = TextWrapping.Wrap
          };

          var messageBox = new MessageBox()
          {
               PrimaryButtonText = primaryActionName,
               CloseButtonText = secondaryActionName,
               Title = title,
               Content = textContent
          };

          var result = await messageBox.ShowDialogAsync();
          if (result == MbResult.Primary)
               action.Invoke();
     }
}