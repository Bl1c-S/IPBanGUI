using Logic_IPBanUtility;
using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
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

          protected override void OnStartup(StartupEventArgs e)
          {
               try
               {
                    _loadVM = new LoadWindowModel();
                    var loadWindow = new LoadWindow() { DataContext = _loadVM };
                    loadWindow.Show();

                    var settings = LoadSettings();

                    var Services = CreateServiceProvager(settings);
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

          private Settings LoadSettings()
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
               IHost host = Host.CreateDefaultBuilder().ConfigureServices(services =>
               {
                    services.AddSingleton(settings);
                    services.AddSingleton<FileManager>();
                    services.AddSingleton<ConfigFileManager>();

                    services.AddSingleton<SettingsVMsBuilder>();
                    services.AddTransient<SettingsViewModel>();

                    services.AddSingleton(s => new NavigationService(s));
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddTransient<KeyListViewModel>();
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