using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_IPBanUtility
{
     internal class OtherExeptionHandler
     {
          public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
          {
               HandleException(e.ExceptionObject as Exception);
          }

          public void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
          {
               HandleException(e.Exception);
               e.Handled = true;
          }

          private void HandleException(Exception? ex)
          {
               if (ex == null) return;

               var message = "Не оброблена помилка в додатку";
               MessageBox.Show($"{message}: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
          }
     }
}
