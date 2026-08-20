using Logic_IPBanUtility.Setting;
using Logic_IPBanUtility.Settings;

namespace Test_IPBanUtility;

public class IpBanTestHelper
{
     private const string TestFolder = "IPBan";
     private const string TestDb = "ipban.sqlite";
     internal readonly string FolderPath;
     internal readonly IPBan IpBan;
     internal readonly Settings Settings;

     public IpBanTestHelper()
     {
          FolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TestFolder);
          Directory.CreateDirectory(FolderPath);
          IpBan = CreateEmptyIpBan();
          
          SettingsBuilder sb = new();
          sb.CreateDefaultSettings(IpBan);
          Settings = sb.Settings!;
     }

     private IPBan CreateEmptyIpBan()
     {
          CreateIpBanCfgFile();
          CreateEmptyFile();
          return IPBan.Create(FolderPath);
     }

     private void CreateEmptyFile()
     {
          var path = Path.Combine(FolderPath, TestDb);
          if (!File.Exists(path)) File.Create(path);
     }

     private void CreateIpBanCfgFile()
     {
          var path = Path.Combine(FolderPath, TestDb);
          if (!File.Exists(path))
          {
               File.WriteAllText(path, """
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