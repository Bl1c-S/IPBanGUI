using System;
using System.Windows;
using System.Windows.Controls;

namespace WPF_IPBanUtility;

public static class DialogMessageBox
{
     public static void TwoActionBox(Action Leftaction, Action Rightaction, string message, string title, string actionLeftButtonName = "Ок", string closeRightButtonName = "Закрити")
     {
          var textContent = new TextBlock();
          textContent.Text = message;
          textContent.TextWrapping = TextWrapping.Wrap;

          var messageBox = new Wpf.Ui.Controls.MessageBox();
          messageBox.ButtonLeftName = actionLeftButtonName;
          messageBox.ButtonRightName = closeRightButtonName;
          messageBox.Title = title;
          messageBox.Content = textContent;

          var onOk = new RoutedEventHandler((_, _) =>
          {
               Leftaction?.Invoke();
               messageBox.Close();
          });

          var onClose = new RoutedEventHandler((_, _) =>
          {
               Rightaction?.Invoke();
               messageBox.Close();
          });

          messageBox.ButtonLeftClick += onOk;
          messageBox.ButtonRightClick += onClose;

          messageBox.ShowDialog();
     }
     public static void ActionBox(Action action, string message, string title, string actionLeftButtonName = "Ок", string closeRightButtonName = "Закрити")
     {
          var textContent = new TextBlock();
          textContent.Text = message;
          textContent.TextWrapping = TextWrapping.Wrap;

          var messageBox = new Wpf.Ui.Controls.MessageBox();
          messageBox.ButtonLeftName = actionLeftButtonName;
          messageBox.ButtonRightName = closeRightButtonName;
          messageBox.Title = title;
          messageBox.Content = textContent;

          var onOk = new RoutedEventHandler((_, _) =>
          {
               action?.Invoke();
               messageBox.Close();
          });

          var onClose = new RoutedEventHandler((_, _) =>
          {
               messageBox.Close();
          });

          messageBox.ButtonLeftClick += onOk;
          messageBox.ButtonRightClick += onClose;

          messageBox.ShowDialog();
     }

     public static void InfoBox(string title, string message, string LeftButtonName = "Ок", string closeRightButtonName = "Закрити")
     {
          var textContent = new TextBlock();
          textContent.Text = message;
          textContent.TextWrapping = TextWrapping.Wrap;

          var messageBox = new Wpf.Ui.Controls.MessageBox();
          messageBox.ButtonLeftName = LeftButtonName;
          messageBox.ButtonRightName = closeRightButtonName;
          messageBox.Title = title;
          messageBox.Content = textContent;

          var onOk = new RoutedEventHandler((_, _) => { messageBox.Close(); });
          var onClose = new RoutedEventHandler((_, _) => { messageBox.Close(); });

          messageBox.ButtonLeftClick += onOk;
          messageBox.ButtonRightClick += onClose;

          messageBox.ShowDialog();
     }
}