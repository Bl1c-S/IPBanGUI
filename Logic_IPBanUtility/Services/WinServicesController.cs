using Logic_IPBanUtility.Setting;
using System.Runtime.Versioning;
using System.ServiceProcess;

namespace Logic_IPBanUtility.Services
{
     public class WinServicesController
     {
          public Service IPBan;

          public WinServicesController(Settings settings)
          {
               IPBan = new(settings.IPBan.ServiceName);
          }

          [SupportedOSPlatform("windows")]
          public class Service
          {
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

               public Service(string name)
               {
                    Name = name;
                    Update();
               }

               public Task Update() => Task.Run(() =>
               {
                    using (var serviceController = new ServiceController(Name))
                    {
                         if (CheckIfServiceExists(Name))
                         {
                              Status = ServiceProcessStatus.UpdatingStatus;
                              var status = serviceController.Status;
                              if (status == ServiceControllerStatus.Running)
                              {
                                   Status = ServiceProcessStatus.Running;
                                   return;
                              }
                         }
                         Status = ServiceProcessStatus.Stopped;
                    }
               });

               public Task Start() => Task.Run(() =>
               {
                    Status = ServiceProcessStatus.Starting;
                    using (var serviceController = new ServiceController(Name))
                    {
                         if (ServiceControllerStatus.Stopped == serviceController.Status)
                         {
                              serviceController.Start();
                              serviceController.WaitForStatus(ServiceControllerStatus.Running);
                         }
                    }
                    Update();
               });
               public Task Stop() => Task.Run(() =>
               {
                    Status = ServiceProcessStatus.Stoping;
                    using (var serviceController = new ServiceController(Name))
                    {
                         if (ServiceControllerStatus.Running == serviceController.Status)
                         {
                              serviceController.Stop();
                              serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                         }
                    }
                    Update();
               });
               public Task Restart() => Task.Run(async () =>
               {
                    using (var serviceController = new ServiceController(Name))
                    {
                         await Stop();
                         await Start();
                    }
               });
               public bool CheckIfServiceExists(string serviceName)
               {
                    var services = ServiceController.GetServices();
                    return services.Any(s => s.ServiceName == serviceName);
               }
          }
          public enum ServiceProcessStatus
          {
               Running,
               Stopped,
               Starting,
               Stoping,
               UpdatingStatus
          }
     }
}
