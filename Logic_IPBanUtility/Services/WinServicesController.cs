using Logic_IPBanUtility.Interfaces.Services;
using Logic_IPBanUtility.Setting;
using System.Runtime.Versioning;
using System.ServiceProcess;

namespace Logic_IPBanUtility.Services
{
     public class WinServicesController : IWinServicesController
     {
          public Service IPBan { get; }

          public WinServicesController(Settings settings, IServiceManager serviceManager)
          {
               IPBan = new(settings.IPBan.ServiceName, serviceManager);
          }

          [SupportedOSPlatform("windows")]
          public class Service
          {
               private readonly IServiceManager _manager;
               public string Name { get; }

               private ServiceProcessStatus _status;
               public ServiceProcessStatus Status
               {
                    get => _status; private set
                    {
                         if (_status != value)
                         {
                              _status = value;
                              StatusChanged?.Invoke();
                         }
                    }
               }
               public Action? StatusChanged;

               public Service(string name, IServiceManager manager)
               {
                    Name = name;
                    _manager = manager;
                    _ = Update(); //fire and forget 
               }
               public Task Update() => Task.Run(() =>
               {
                    if (_manager.CheckExists(Name))
                    {
                         var systemStatus = _manager.GetStatus(Name);
                         Status = systemStatus == ServiceControllerStatus.Running
                                              ? ServiceProcessStatus.Running
                                              : ServiceProcessStatus.Stopped;
                    }
                    else
                         Status = ServiceProcessStatus.Stopped;
               });

               public async Task Start()
               {
                    Status = ServiceProcessStatus.Starting;
                    await Task.Run(() =>
                    {
                         if (_manager.GetStatus(Name) == ServiceControllerStatus.Stopped)
                              _manager.Start(Name);
                    });

                    await Update();
               }
               public async Task Stop()
               {
                    Status = ServiceProcessStatus.Stopping;
                    await Task.Run(() =>
                    {
                         if (_manager.GetStatus(Name) == ServiceControllerStatus.Running)
                              _manager.Stop(Name);
                    });

                    await Update();
               }

               public async Task Restart() 
               {
                    await Stop();
                    await Start();
               }
          }
          public enum ServiceProcessStatus
          {
               Running,
               Stopped,
               Starting,
               Stopping,
               UpdatingStatus
          }
     }
}
