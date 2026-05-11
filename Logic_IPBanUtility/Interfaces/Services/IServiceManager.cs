using System.ServiceProcess;

namespace Logic_IPBanUtility.Interfaces.Services
{
     public interface IServiceManager
     {
          ServiceControllerStatus GetStatus(string name);
          void Start(string name);
          void Stop(string name);
          bool CheckExists(string name);
     }
}
