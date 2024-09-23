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
               var newIPAddressList = _dBManager.GetAll();
               if (!IPAddress.SequenceEqual(newIPAddressList))
               {
                    IPAddress = newIPAddressList;
                    IPAddressChanged?.Invoke();
               }
          }
          public void Remove(IPAddressEntity iPAddress)
          {
               Update();
               var ip = IPAddress.Find(x => x.IPAddressText == iPAddress.IPAddressText)!;

               if (ip != null) AddToUnBan(ip);
          }

          private void AddToUnBan(IPAddressEntity ip)
          {
               IPAddress.Remove(ip);
               _dBManager.Remove(ip);
               IPAddressChanged?.Invoke();
               _unBanService.Add(ip.IPAddressText);
          }

          public void RemoveAll()
          {
               foreach (var ip in IPAddress)
                    _unBanService.Add(ip.IPAddressText);

               _dBManager.RemoveAll();
               IPAddress.Clear();
               IPAddressChanged?.Invoke();
          }

          public void ApplyRemove()
          {
               _unBanService.CreateFile();
          }

          private bool IsNewIP(IPAddressEntity iPAddress)
          {
               return !IPAddress.Any(ip => ip.IPAddressText == iPAddress.IPAddressText);
          }
     }
}
