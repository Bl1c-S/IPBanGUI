using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
               var Services = CreateServiceProvager();

               var mainWindow = new MainWindow();
               mainWindow.DataContext = Services.GetRequiredService<MainWindowViewModel>();
               mainWindow.Show();

               base.OnStartup(e);
          }

          private IServiceProvider CreateServiceProvager()
          {
               IHost host = Host.CreateDefaultBuilder().ConfigureServices(servises =>
               {
                    servises.AddSingleton(s => new NavigationService(s));
                    servises.AddSingleton<MainWindowViewModel>();
                    servises.AddTransient<SettingsViewModel>();
                    servises.AddTransient<KeyListViewModel>();
                    servises.AddTransient<KeyViewModel>();
               }).Build();
               host.Start();

               return host.Services;
          }
     }
}