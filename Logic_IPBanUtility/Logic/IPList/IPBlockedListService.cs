using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Models;
using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.IPList;

public class IPBlockedListService
{
     public Action ApplyRemove => _iPManager.ApplyRemove;
     public List<IPAddressEntity> IPs => _iPManager.IPAddress;
     public Action? IPsChanged { get => _iPManager.IPAddressChanged; set => _iPManager.IPAddressChanged = value; }

     private readonly IPAddressManager _iPManager;
     private readonly KeyValueManager _keyManager;

     public IPBlockedListService(KeyValueManager keyManager, Settings settings)
     {
          _keyManager = keyManager;
          _iPManager = new(settings);
     }
     public void Update() => _iPManager.Update();
     public void Add(IPAddressEntity ip) => _iPManager.Add(ip); //For tests
     public void Remove(IPAddressEntity ip) => _iPManager.Remove(ip);
     public void RemoveAll() => _iPManager.RemoveAll(); //TODO Додати видалення всіх

     #region Keys
     public void AddToWhiteList(IPAddressEntity ip)
     {
          Remove(ip);
          _keyManager.AddIpToKey(KeyNames.Whitelist, ip.IPAddressText);
     }
     public void AddToBlacklist(IPAddressEntity ip)
     {
          Remove(ip);
          _keyManager.AddIpToKey(KeyNames.Blacklist, ip.IPAddressText);
     }
     #endregion
}