using Logic_IPBanUtility.Interfaces.Services;
using System.Runtime.Versioning;
using System.ServiceProcess;

namespace Logic_IPBanUtility.Services
{
     [SupportedOSPlatform("windows")]
     public class WindowsServiceManager : IServiceManager
     {

          public ServiceControllerStatus GetStatus(string name)
          {
               using var sc = new ServiceController(name);
               return sc.Status;
          }
          public void Start(string name)
          {
               using var sc = new ServiceController(name);
               sc.Start();
               sc.WaitForStatus(ServiceControllerStatus.Running);
          }
          public void Stop(string name)
          {
               using var sc = new ServiceController(name); 
               sc.Stop(); 
               sc.WaitForStatus(ServiceControllerStatus.Stopped);
          }
          public bool CheckExists(string name) => ServiceController.GetServices().Any(s => s.ServiceName == name);
     }
}
