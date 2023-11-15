using Logic_IPBanUtility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
               var mainWindow = new MainWindow();
               try
               {
                    var Services = CreateServiceProvager();
                    mainWindow.DataContext = Services.GetRequiredService<MainWindowViewModel>();
                    mainWindow.Show();
               }
               catch (Exception ex)
               {
                    var messageBox = new InfoMessageBox(ex.Message, "Помилка", "Ок", "Закрити");
                    messageBox.OpenMassangeBox(null);
                    Task.Delay(5000).Wait();
               }

               base.OnStartup(e);
          }

          private IServiceProvider CreateServiceProvager()
          {
               IHost host = Host.CreateDefaultBuilder().ConfigureServices(servises =>
               {
                    servises.AddSingleton<Settings>();
                    servises.AddSingleton<ConfigFileManager>();

                    servises.AddSingleton(s => new NavigationService(s));
                    servises.AddSingleton<MainWindowViewModel>();
                    servises.AddTransient<SettingsViewModel>();
                    servises.AddTransient<KeyListViewModel>();
               }).Build();
               host.Start();

               return host.Services;
          }
     }
}