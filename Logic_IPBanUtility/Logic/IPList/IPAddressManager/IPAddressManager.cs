using Logic_IPBanUtility.Logic.IPList.Services;
using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.IPList
{
     public class IPAddressManager
     {
          public Action? IPAddressChanged;
          public List<IPAddressEntity> IPAddress;
          private readonly IPAddressDatabaseManager _dBManager;
          private readonly UnBanService _unBanService;

          public IPAddressManager(Settings settings)
          {
               _dBManager = new(settings);
               _unBanService = new(settings);
               IPAddress = _dBManager.GetAll();
          }

          public void Add(IPAddressEntity iPAddress)
          {
               Update();
               if (!IsNewIP(iPAddress))
                    throw new ArgumentException($"Адресу {iPAddress.IPAddressText} вже заблоковано");
               else
               {
                    _dBManager.Add(iPAddress);
                    IPAddress.Add(iPAddress);
                    IPAddressChanged?.Invoke();
               }
          }

          public void Update()
          {
               if (!IPsEqual()) IPAddressChanged?.Invoke();
          }
          private bool IPsEqual()
          {
               var newIPAddressList = _dBManager.GetAll();
               if (newIPAddressList.Count != IPAddress.Count) return false;

               foreach (var newIp in newIPAddressList)
               {
                    var ip = IPAddress.FirstOrDefault(ip => ip.IPAddressText == newIp.IPAddressText);
                    if (ip == null) return false;
               }

               IPAddress = newIPAddressList;
               return true;
          }
          private bool Equal(IPAddressEntity oldIP, IPAddressEntity newIP)
          {
               return oldIP.IPAddressText == newIP.IPAddressText;
          }

          public void Remove(IPAddressEntity iPAddress)
          {
               var ip = IPAddress.Find(x => x.IPAddressText == iPAddress.IPAddressText)!;

               if (ip != null) AddToUnBan(ip);
          }

          public void RemoveAll()
          {
               foreach (var ip in IPAddress)
                    _unBanService.Add(ip.IPAddressText);

               _dBManager.RemoveAll();
               IPAddress.Clear();
          }

          public void ApplyRemove()
          {
               _unBanService.CreateFile();
          }

          private void AddToUnBan(IPAddressEntity ip)
          {
               IPAddress.Remove(ip);
               _dBManager.Remove(ip);
               _unBanService.Add(ip.IPAddressText);
          }

          private bool IsNewIP(IPAddressEntity iPAddress)
          {
               return !IPAddress.Any(ip => ip.IPAddressText == iPAddress.IPAddressText);
          }
     }
}
