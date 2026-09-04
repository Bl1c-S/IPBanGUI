using System;

namespace WPF_IPBanUtility
{
     /// <summary>
     /// Interaction logic for MainWindow.xaml
     /// </summary>
     public partial class MainWindow
     {
          public Action? WindowClosing;
          public MainWindow()
          {
               InitializeComponent();
          }

          private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
          {
               WindowClosing?.Invoke();
               e.Cancel = false;
          }
     }
}
