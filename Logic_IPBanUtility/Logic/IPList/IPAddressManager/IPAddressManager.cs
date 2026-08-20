using Logic_IPBanUtility.Logic.IPList.IPAddressManager.Services;
using Logic_IPBanUtility.Logic.IPList.Services;

namespace Logic_IPBanUtility.Logic.IPList.IPAddressManager
{
     public class IpAddressManager
     {
          public Action? IpAddressChanged;
          public List<IPAddressEntity> IpAddress;
          private readonly IpAddressDatabaseManager _dBManager;
          private readonly UnBanService _unBanService;

          public IpAddressManager(Settings.Settings settings)
          {
               _dBManager = new(settings);
               _unBanService = new(settings);
               IpAddress = _dBManager.GetAll();
          }

          public void Add(IPAddressEntity iPAddress)
          {
               Update();
               if (!IsNewIP(iPAddress))
                    throw new ArgumentException($"Адресу {iPAddress.IPAddressText} вже заблоковано");
               else
               {
                    _dBManager.Add(iPAddress);
                    IpAddress.Add(iPAddress);
                    IpAddressChanged?.Invoke();
               }
          }

          public void Update()
          {
               if (!IPsEqual()) IpAddressChanged?.Invoke();
          }
          private bool IPsEqual()
          {
               var newIPAddressList = _dBManager.GetAll();
               if (newIPAddressList.Count != IpAddress.Count) return false;

               foreach (var newIp in newIPAddressList)
               {
                    var ip = IpAddress.FirstOrDefault(ip => ip.IPAddressText == newIp.IPAddressText);
                    if (ip == null) return false;
               }

               IpAddress = newIPAddressList;
               return true;
          }
          private bool Equal(IPAddressEntity oldIP, IPAddressEntity newIP)
          {
               return oldIP.IPAddressText == newIP.IPAddressText;
          }

          public void Remove(IPAddressEntity iPAddress)
          {
               var ip = IpAddress.Find(x => x.IPAddressText == iPAddress.IPAddressText)!;

               if (ip != null) AddToUnBan(ip);
          }

          public void RemoveAll()
          {
               foreach (var ip in IpAddress)
                    _unBanService.Add(ip.IPAddressText);

               _dBManager.RemoveAll();
               IpAddress.Clear();
          }

          public void ApplyRemove()
          {
               _unBanService.CreateFile();
          }

          private void AddToUnBan(IPAddressEntity ip)
          {
               IpAddress.Remove(ip);
               _dBManager.Remove(ip);
               _unBanService.Add(ip.IPAddressText);
          }

          private bool IsNewIP(IPAddressEntity iPAddress)
          {
               return !IpAddress.Any(ip => ip.IPAddressText == iPAddress.IPAddressText);
          }
     }
}
