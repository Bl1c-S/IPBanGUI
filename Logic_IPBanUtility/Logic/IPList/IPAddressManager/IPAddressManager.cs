using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.IPList
{
     public class IPAddressManager
     {
          public Action? IPAddressChanged;
          public List<IPAddressEntity> IPAddress;
          private readonly IPAddressDatabaseManager _dBManager;

          public IPAddressManager(Settings settings)
          {
               _dBManager = new(settings);
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
               IPAddress.Remove(iPAddress);
               _dBManager.Remove(iPAddress);
               IPAddressChanged?.Invoke();
          }
          public void RemoveAll()
          {
               _dBManager.RemoveAll();
               IPAddress.Clear();
               IPAddressChanged?.Invoke();
          }

          private bool IsNewIP(IPAddressEntity iPAddress)
          {
               return !IPAddress.Any(ip => ip.IPAddressText == iPAddress.IPAddressText);
          }
     }
}
