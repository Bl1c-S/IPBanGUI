﻿using Logic_IPBanUtility.Models;
using Logic_IPBanUtility.Services;
using System.Text.Json;

namespace Logic_IPBanUtility.Setting;

public class SettingsBuilder
{
     public Settings? Settings;

     FileManager _fileManager = new();

     public void LoadSettings()
     {
          Config config = Config.Create();
          config.CheckExist();
          Settings = _fileManager.GetJson<Settings>(config.Settings);
          Settings.Config.CheckExist();
     }
     public void CreateDefaultSettings(IPBan iPBan)
     {
          Config config = Config.Create();
          _fileManager.CreateDefaultDirectory(config.ConfigFolder);
          CreateDefaultKeyIdenty(config.KeyIdenti);
          Settings settings = new(config, iPBan);
          settings.Save();
          config.CheckExist();
          Settings = settings;
     }

     #region KeyIdenty
     private void CreateDefaultKeyIdenty(string filePath)
     {
          var names = GetDefaultKeyNames();
          List<KeyIdenti> keyIdentis = new();

          foreach (var name in names)
               keyIdentis.Add(new(true, name));

          keyIdentis = keyIdentis.OrderBy(x => x.Name, StringComparer.CurrentCulture).ToList();
          string JsonKeyIdentiList = JsonSerializer.Serialize(keyIdentis);
          File.WriteAllText(filePath, JsonKeyIdentiList);
     }
     private string[] GetDefaultKeyNames()
     {
          string keyNames = "IPThreatApiKey\r\nFailedLoginAttemptsBeforeBan\r\nFirewallRulePrefix\r\nBanTime\r\nResetFailedLoginCountForUnbannedIPAddresses\r\nClearBannedIPAddressesOnRestart\r\nClearFailedLoginsOnSuccessfulLogin\r\nProcessInternalIPAddresses\r\nExpireTime\r\nCycleTime\r\nMinimumTimeBetweenFailedLoginAttempts\r\nMinimumTimeBetweenSuccessfulLoginAttempts\r\nFirewallRulePrefix\r\nWhitelist\r\nWhitelistRegex\r\nBlacklist\r\nBlacklistRegex\r\nTruncateUserNameChars\r\nUserNameWhitelist\r\nUserNameWhitelistRegex\r\nUserNameWhitelistMinimumEditDistance\r\nFailedLoginAttemptsBeforeBanUserNameWhitelist\r\nProcessToRunOnBan\r\nProcessToRunOnUnban\r\nFirewallRules\r\nUseDefaultBannedIPAddressHandler\r\nExternalIPAddressUrl\r\nFirewallUriRules\r\nGetUrlUpdate\r\nGetUrlStart\r\nGetUrlStop\r\nGetUrlConfig\r\nIPBanPro_UrlWebSocket";
          return keyNames.Split("\r\n");
     }
     #endregion
}