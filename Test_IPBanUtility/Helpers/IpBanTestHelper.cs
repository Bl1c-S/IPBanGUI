using Logic_IPBanUtility.Setting;
using Logic_IPBanUtility.Settings;

namespace Test_IPBanUtility;

public class IpBanTestHelper
{
     internal readonly Settings Settings;
     internal IPBan IpBan => Settings.IpBan;
     internal string Folder => IpBan.Folder;
     private string TestCfg => IpBan.Context;

     public IpBanTestHelper()
     {
          var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IPBan");
          Directory.CreateDirectory(folder);
          var ipBan = IPBan.Create(folder);

          SettingsBuilder sb = new();
          sb.CreateDefaultSettings(ipBan);
          Settings = sb.Settings;

          CreateIpBanCfgFile();
     }

     private void CreateIpBanCfgFile()
     {
          if (!File.Exists(TestCfg))
          {
               File.WriteAllText(TestCfg, """
                                          <add key="BanTime" value=""/>
                                          <add key="Blacklist" value=""/>
                                          <add key="BlacklistRegex" value=""/>
                                          <add key="ClearBannedIPAddressesOnRestart" value=""/>
                                          <add key="ClearFailedLoginsOnSuccessfulLogin" value=""/>
                                          <add key="CycleTime" value=""/>
                                          <add key="ExpireTime" value=""/>
                                          <add key="FailedLoginAttemptsBeforeBan" value=""/>
                                          <add key="FailedLoginAttemptsBeforeBanUserNameWhitelist" value=""/>
                                          <add key="FirewallRulePrefix" value=""/>
                                          <add key="FirewallRules" value=""/>
                                          <add key="MinimumTimeBetweenFailedLoginAttempts" value=""/>
                                          <add key="MinimumTimeBetweenSuccessfulLoginAttempts" value=""/>
                                          <add key="ProcessInternalIPAddresses" value=""/>
                                          <add key="ResetFailedLoginCountForUnbannedIPAddresses" value=""/>
                                          <add key="UserNameWhitelist" value=""/>
                                          <add key="UserNameWhitelistMinimumEditDistance" value=""/>
                                          <add key="UserNameWhitelistRegex" value=""/>
                                          <add key="Whitelist" value=""/>
                                          <add key="WhitelistRegex" value=""/>
                                          """);
          }
     }
}