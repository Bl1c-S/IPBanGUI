using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Models;

namespace Logic_IPBanUtility.Logic.IPList;

public class IpBlockedListService
{
     public Action ApplyRemove => _iPManager.ApplyRemove;
     public List<IPAddressEntity> IPs => _iPManager.IpAddress;
     public Action? IPsChanged { get => _iPManager.IpAddressChanged; set => _iPManager.IpAddressChanged = value; }

     private readonly IPAddressManager.IpAddressManager _iPManager;
     private readonly KeyValueManager _keyManager;

     public IpBlockedListService(KeyValueManager keyManager, Settings.Settings settings)
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