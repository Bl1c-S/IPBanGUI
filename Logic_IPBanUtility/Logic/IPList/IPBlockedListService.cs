using Logic_IPBanUtility.Logic.ConfigFile;
using Logic_IPBanUtility.Models;

namespace Logic_IPBanUtility.Logic.IPList;

public class IPBlockedListService
{
     public List<IPAddressEntity> IPs => _iPManager.IPAddress;
     public Action? IPsChanged { get => _iPManager.IPAddressChanged; set => _iPManager.IPAddressChanged = value; }

     private readonly IPAddressManager _iPManager;
     private readonly KeyValueManager _keyManager;

     public IPBlockedListService(KeyValueManager keyManager, IPAddressManager iPAddressManager)
     {
          _keyManager = keyManager;
          _iPManager = iPAddressManager;
     }
     public void Update() => _iPManager.Update();
     public void Add(IPAddressEntity ip) => _iPManager.Add(ip); //For tests
     public void Remove(IPAddressEntity ip) => _iPManager.Remove(ip);
     public void RemoveAll() => _iPManager.RemoveAll();

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