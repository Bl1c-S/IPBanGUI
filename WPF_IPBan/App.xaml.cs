using Logic_IPBanUtility;
using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;
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
          private LoadWindowModel? _loadVM;
          private Settings? _settings;


          protected override void OnStartup(StartupEventArgs e)
          {
               try
               {
                    var loadWindow = new LoadWindow();
                    _loadVM = new LoadWindowModel();
                    loadWindow.DataContext = _loadVM;
                    loadWindow.Show();

                    _settings = LoadSettings(loadWindow);
                    if (_settings is null)
                         ApplicationStop();

                    if (_settings!.IPBan is null || !_settings.IPBan.CheckExist())
                         DialogMessageBox.TwoActionBox(OnSelectFolder, ApplicationStop, "Не вказана тека IPBan", "Попередження", "Вибрати", "Закрити");

                    var mainWindow = new MainWindow();
                    var Services = CreateServiceProvager(_settings);
                    mainWindow.DataContext = Services.GetRequiredService<MainWindowViewModel>();
                    loadWindow.Close();
                    mainWindow.Show();
               }
               catch (Exception ex) { DialogMessageBox.InfoBox("Помилка", ex.Message); }

               base.OnStartup(e);
          }

          private void OnSelectFolder()
          {
               var path = _loadVM?.SelectFolder();
               if(path != null)
                    _settings?.SetIPBan(path);
          }

          private Settings? LoadSettings(LoadWindow loadWindow)
          {
               var sb = new SettingsBuilder();
               try { sb.LoadSettings(); }
               catch (Exception ex) { DialogMessageBox.ActionBox(sb.CreateDefaultSettings, ex.Message, "Помилка завантаження налаштувань", "Скинути налаштування", "Закрити"); }

               return sb.Settings;
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