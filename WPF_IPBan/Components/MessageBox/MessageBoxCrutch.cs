using System;
using System.Windows;

namespace WPF_IPBanUtility.Components.MessageBox;

internal static class MessageBoxCrutch
{
     public static void ErrorBox(string message, Action right)
     {
          DialogMessageBox.TwoActionBoxAsync(
               () => Clipboard.SetText(message),
               right,
               message,
               Properties.Status.Error,
               Properties.ButtonNames.Copy,
               Properties.ButtonNames.Close).Wait();
     }

     public static void LoadSettingsError(Action primary, Action secondary)
     {
          DialogMessageBox.TwoActionBoxAsync(
               primary,
               secondary,
               Properties.Messages.LoadSettingsErrorText,
               Properties.Messages.LoadSettingsErrorTitle,
               Properties.ButtonNames.Select,
               Properties.ButtonNames.Close).Wait();
     }
}