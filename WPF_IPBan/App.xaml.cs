using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_IPBanUtility
{
     /// <summary>
     /// Interaction logic for App.xaml
     /// </summary>
     public partial class App : Application
     {
          protected override void OnStartup(StartupEventArgs e)
          {
               var navigationService = new NavigationService();
               var mainWindowViewModel = new MainWindowViewModel(navigationService);
               var mainWindow = new MainWindow();

               mainWindow.DataContext = mainWindowViewModel;
               mainWindow.Show();

               base.OnStartup(e);
          }
     }
}
