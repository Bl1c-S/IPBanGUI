using System;
using System.Windows;

namespace WPF_IPBanUtility.Components.MessageBox;

internal static class MessageBoxCrutch
{
     public static void ErrorBox(string message, Action right)
     {
          DialogMessageBox.TwoActionBox(
               () => Clipboard.SetText(message), 
               right, 
               message,
               Properties.Status.Error,
               Properties.ButtonNames.Copy, 
               Properties.ButtonNames.Close);
     }
     public static void TwoActionBoxAndLeftButtonNameSelect(Action left, Action right)
     {
          DialogMessageBox.TwoActionBox(left, 
               right, 
               Properties.Messages.LoadSettingsErrorText, 
               Properties.Messages.LoadSettingsErrorTitle, 
               Properties.ButtonNames.Select,
               Properties.ButtonNames.Close);
     }
}