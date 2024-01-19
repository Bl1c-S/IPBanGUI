using System.Runtime.InteropServices;

namespace Logic_IPBanUtility.Logic.LogFile;

public class LogMessageParser
{
     private readonly LogMessageInfoExtractor logInfoExtractor = new();
     private const string END_Attribute = ",";

     private const string START_IpAttributeSpace = "address ";
     private const string START_IpAttributeColon = "address: ";
     private const string START_IpAttributeFailure = "failure:";
     private const string Start_IpAttributeUpdated = "updated:";

     private const string START_NameAttributeColon = "name: ";
     private const string START_NameAttributeСomma = ", ";

     private const string START_DurationAttribute = "duration: ";
     private const string START_CountAttribute = "count: ";

     public string? Parse(string logMessage)
     {
          switch (logMessage)
          {
               case var s when s.StartsWith("Login succeeded"):
                    return LoginSucceeded(logMessage);
               case var s when s.StartsWith("Login failure"):
                    return LoginFailure(logMessage);
               case var s when s.StartsWith("Forgetting failed"):
                    return ForgetFailedLogin(logMessage);
               case var s when s.StartsWith("Banning ip"):
                    return BanningIP(logMessage);
               case var s when s.StartsWith("Un-banning"):
                    return UnBanningIP(logMessage);
               case var s when s.StartsWith("Firewall entries"):
                    return FirewallEntriesUpdated(logMessage);
               default : return null;
          }
     }

     private string LoginSucceeded(string logMessage)
     {
          var IpAddres = logInfoExtractor.ExtractStringFromStartAtributeToEndAttribute(logMessage, START_IpAttributeColon, END_Attribute);
          var user = logInfoExtractor.ExtractStringFromStartAtributeToEndAttribute(logMessage, START_NameAttributeColon, END_Attribute);

          if (user is null)
               return $"{Properties.Resources.LoginSucceeded} {IpAddres}, {Properties.Resources.IncorrectUsernameFormat}";
          else
               return $"{Properties.Resources.LoginSucceeded} {IpAddres}, {Properties.Resources.CorrectUsernameFormat} {user}";
     }

     private string LoginFailure(string logMessage)
     {
          var IpAddres = logInfoExtractor.ExtractStringFromStartAtributeToEndAttribute(logMessage, START_IpAttributeFailure, END_Attribute);
          var user = logInfoExtractor.ExtractStringFromStartAtributeToEndAttribute(logMessage, START_NameAttributeСomma, END_Attribute);

          if (user is null)
               return $"{Properties.Resources.LoginFailure}{IpAddres}, {Properties.Resources.IncorrectUsernameFormat}";
          else
               return $"{Properties.Resources.LoginFailure}{IpAddres}, {Properties.Resources.CorrectUsernameFormat} {user}";
     }

     private string ForgetFailedLogin(string logMessage)
     {
          var IpAddres = logInfoExtractor.ExtractStringFromStartAtributeToEndAttribute(logMessage, START_IpAttributeSpace, END_Attribute);
          return $"{Properties.Resources.ForgettingFailed} {IpAddres}";
     }

     private string BanningIP(string logMessage)
     {
          var IpAddres = logInfoExtractor.ExtractStringFromStartAtributeToEndAttribute(logMessage, START_IpAttributeColon, END_Attribute);
          var count = logInfoExtractor.ExtractStringFromStartAtributeToEndAttribute(logMessage, START_CountAttribute, END_Attribute);
          var duration = logInfoExtractor.ExtractStringFromStartAtributeToEndString(logMessage, START_DurationAttribute);

          return $"{Properties.Resources.BanningIP} {IpAddres}, {Properties.Resources.LogonTryCount} {count}, {Properties.Resources.BanTime} {duration}";
     }

     private string UnBanningIP(string logMessage)
     {
          var IpAddres = logInfoExtractor.ExtractStringFromStartAtributeToEndAttribute(logMessage, START_IpAttributeSpace, END_Attribute);
          return $"{Properties.Resources.UnBanningIP} {IpAddres}";
     }

     private string FirewallEntriesUpdated(string logMessage)
     {
          var IpAddreses = logInfoExtractor.ExtractStringFromStartAtributeToEndString(logMessage, Start_IpAttributeUpdated);

          if (IpAddreses is null)
               return $"{Properties.Resources.FirewallEntriesUpdated}";
          else
               return $"{Properties.Resources.FirewallEntriesUpdated}{IpAddreses}";
     }
}