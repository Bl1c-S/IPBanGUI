using Logic_IPBanUtility.Logic.IPList;
using Logic_IPBanUtility.Logic.IPList.IPAddressManager;
using Logic_IPBanUtility.Setting;
using Logic_IPBanUtility.Settings;

namespace Test_IPBanUtility.Factories
{
     internal class IpAddressManagerTestFactory
     {
          private readonly Settings _settings;

          public IpAddressManagerTestFactory(Settings settings)
          {
               _settings = settings;
          }

          public IpAddressManager CreateManager()
          {
               return new IpAddressManager(_settings);
          }
          public List<IPAddressEntity> CreateIP(int count)
          {
               List<IPAddressEntity> iPAddresses = new();
               for (int i = 0; i < count; i++)
                    iPAddresses.Add(new($"77.255.3.{i}", DateTime.Now, 0, DateTime.Now, DateTime.Now.AddYears(1), null));

               return iPAddresses;
          }
     }
}