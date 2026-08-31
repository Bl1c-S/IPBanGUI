using System;
using System.Windows;
using System.Windows.Threading;

namespace WPF_IPBanUtility
{
     internal class GeneralExceptionHandler(Action applicationStop)
     {
          private bool _isHandlingException;

          public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
          {
               HandleException(e.ExceptionObject as Exception);
          }

          public void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
          {
               e.Handled = true;
               HandleException(e.Exception);
          }

          private void HandleException(Exception? ex)
          {
               if (ex == null) return;

               if (_isHandlingException) return;

               try
               {
                    _isHandlingException = true;

                    var message = "Не оброблена помилка в додатку";
                    MessageBox.Show($"{message}:\n\n{ex.Message}\n\nTrace:\n{ex.StackTrace}", 
                         "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
               }
               finally
               {
                    _isHandlingException = false;
                    applicationStop.Invoke();
               }
          }
     }
}