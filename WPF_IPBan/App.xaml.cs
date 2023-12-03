using Logic_IPBanUtility;
using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using WPF_IPBanUtility.View.LoadWindow.MessangeBox;

namespace WPF_IPBanUtility
{
     /// <summary>
     /// Interaction logic for App.xaml
     /// </summary>
     public partial class App : Application
     {
          private LoadWindowModel? _loadVM;
          private SettingsBuilder _sb = new();
          private Settings? _settings;


          protected override void OnStartup(StartupEventArgs e)
          {
               try
               {
                    var loadWindow = new LoadWindow() { DataContext = new LoadWindowModel() };
                    loadWindow.Show();

                    _settings = LoadSettings(loadWindow);

                    var Services = CreateServiceProvager(_settings);
                    var mainWindow = new MainWindow() { DataContext = Services.GetRequiredService<MainWindowViewModel>() };

                    loadWindow.Close();
                    mainWindow.Show();
               }
               catch (Exception ex) { MessangeBoxCrutch.ErrorBox(ex.Message); }

               base.OnStartup(e);
          }

          private void SelectIPBanAndCreateDfSettings()
          {
               var path = _loadVM?.SelectFolder();
               if (path == null) return;

               var iPBan = IPBan.Create(path);
               _sb.CreateDefaultSettings(iPBan);
          }

          private Settings LoadSettings(LoadWindow loadWindow)
          {
               try { _sb.LoadSettings(); }
               catch (Exception ex)
               {
                    MessangeBoxCrutch.ErrorBox(ex.Message);
                    MessangeBoxCrutch.TwoActionBoxAndLeftButtonNameSelect(SelectIPBanAndCreateDfSettings, ApplicationStop);
               }
               return _sb.Settings!;
          }

          private IServiceProvider CreateServiceProvager(Settings settings)
          {
               IHost host = Host.CreateDefaultBuilder().ConfigureServices(servises =>
               {
                    servises.AddSingleton(settings);
                    servises.AddSingleton<FileManager>();
                    servises.AddSingleton<ConfigFileManager>();

                    servises.AddSingleton(s => new NavigationService(s));
                    servises.AddSingleton<MainWindowViewModel>();
                    servises.AddTransient<SettingsViewModel>();
                    servises.AddTransient<KeyListViewModel>();
               }).Build();
               host.Start();

               return host.Services;
          }

          private void ApplicationStop()
          {
               Current.Shutdown();
               Environment.Exit(0);
          }
     }
}