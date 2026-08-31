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
using Logic_IPBanUtility.Settings;
using WPF_IPBanUtility.Components.MessageBox;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility
{
     public partial class App
     {
          private readonly GeneralExceptionHandler _generalExceptionHandler = new(ApplicationStop);
          private readonly PreparatoryViewModel _preparatoryVm = new();
          private readonly SettingsBuilder _sb = new();

          protected override void OnStartup(StartupEventArgs e)
          {
               AppDomain.CurrentDomain.UnhandledException += _generalExceptionHandler.CurrentDomain_UnhandledException;
               Dispatcher.UnhandledException += _generalExceptionHandler.Dispatcher_UnhandledException;

               SafeMainProcess();
               base.OnStartup(e);
          }

          private void SafeMainProcess()
          {
               try
               {
                    MainProcess();
               }
               catch (Exception ex)
               {
                    ErrorHandle(ex);
               }
          }

          private void MainProcess()
          {
               var preparatoryWindow = CreatePreparatoryWindow();
               preparatoryWindow.Show();

               var mainWindow = CreateMainWindow();
               preparatoryWindow.Close();
               mainWindow.Show();
          }
          
          private PreparatoryWindow CreatePreparatoryWindow() =>
               new() { DataContext = _preparatoryVm };

          private MainWindow CreateMainWindow()
          {
               var settings = LoadSettings();

               var services = ConfigureServiceProvider(settings);
               var mainWindow = new MainWindow();
               var vm = services.GetRequiredService<MainWindowViewModel>();
               mainWindow.WindowClosing += vm.Window_Closing;
               mainWindow.DataContext = vm;
               return mainWindow;
          }

          private void SelectIpBanAndCreateDfSettings()
          {
               var path = _preparatoryVm.SelectFolder();
               if (path == null) return;

               var iPBan = IPBan.Create(path);
               iPBan.CheckExist();
               _sb.CreateDefaultSettings(iPBan);
          }

          private Settings LoadSettings()
          {
               try
               {
                    if (!_sb.LoadSettings())
                         SelectFolder();
               }
               catch (Exception ex)
               {
                    ErrorHandle(ex);
               }

               return _sb.Settings!;
          }

          private static IServiceProvider ConfigureServiceProvider(Settings settings)
          {
               var host = Host.CreateDefaultBuilder().ConfigureServices(services =>
               {
                    services.AddSingleton(settings);
                    services.AddSingleton<FileManager>();
                    services.AddSingleton<ConfigFileManager>();
                    services.AddSingleton<KeyValueManager>();
                    services.AddSingleton<LogEventManager>();
                    services.AddSingleton<WinServicesController>();
                    services.AddSingleton<UnBanService>();

                    services.AddSingleton<IpBlockedListService>();

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

          private void SelectFolder()
          {
               MessageBoxCrutch.LoadSettingsError(SelectIpBanAndCreateDfSettings,
                    ApplicationStop);
          }
          private static void ErrorHandle(Exception ex)
          {
               try
               {
                    MessageBoxCrutch.ErrorBox(ex.Message, ApplicationStop);
               }
               catch
               {
                    MessageBox.Show(ex.Message);
               }

               ApplicationStop();
          }
          private static void ApplicationStop()
          {
               Current.Shutdown();
               Environment.Exit(0);
          }
     }
}