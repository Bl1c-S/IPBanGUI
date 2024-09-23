using Logic_IPBanUtility;
using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Logic.IPList;
using Logic_IPBanUtility.Logic.IPList.Services;
using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using WPF_IPBanUtility.View.LoadWindow.MessangeBox;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility
{
     /// <summary>
     /// Interaction logic for App.xaml
     /// </summary>
     public partial class App : Application
     {
          private OtherExeptionHandler otherExeptionHandler = new();
          private LoadWindowModel? _loadVM;
          private SettingsBuilder _sb = new();

          protected override void OnStartup(StartupEventArgs e)
          {
               AppDomain.CurrentDomain.UnhandledException += otherExeptionHandler.CurrentDomain_UnhandledException;
               Dispatcher.UnhandledException += otherExeptionHandler.Dispatcher_UnhandledException;

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
               catch (Exception ex)
               {
                    try { MessangeBoxCrutch.ErrorBox(ex.Message); }
                    catch { MessageBox.Show(ex.Message); }
               }

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
                    services.AddSingleton<KeyValueManager>();
                    services.AddSingleton<LogEventManager>();
                    services.AddSingleton<WinServicesController>();
                    services.AddSingleton<UnBanService>();

                    services.AddSingleton<IPBlockedListService>();

                    services.AddSingleton<IPListProperties>();
                    services.AddTransient<IPListVMsBuilder>();
                    services.AddTransient<SettingsVMsBuilder>();

                    services.AddSingleton(s => new NavigationService(s));
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddTransient<ManualViewModel>();
                    services.AddTransient<IPListViewModel>();
                    services.AddTransient<KeyListViewModel>();
                    services.AddTransient<SettingsViewModel>();
                    services.AddTransient<EventsViewModel>();
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