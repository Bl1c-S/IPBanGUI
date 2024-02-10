namespace Logic_IPBanUtility.Logic.LogFile.Models;

public class LogEventStatistics
{
     public int AllLogEvent { get; private set; }
     public int LoginSucceeded { get; private set; }
     public int LoginFailure { get; private set; }
     public int ForgetFailedLogin { get; private set; }
     public int BanningIP { get; private set; }
     public int UnBanningIP { get; private set; }
     public int FirewallEntriesUpdated { get; private set; }

     public void AddOneEvent(LogEventType type)
     {
          ++AllLogEvent;
          switch (type)
          {
               case LogEventType.LoginSucceeded:
                    ++LoginSucceeded; break;
               case LogEventType.LoginFailure:
                    ++LoginFailure; break;
               case LogEventType.ForgetFailedLogin:
                    ++ForgetFailedLogin; break;
               case LogEventType.BanningIP:
                    ++BanningIP; break;
               case LogEventType.UnBanningIP:
                    ++UnBanningIP; break;
               case LogEventType.FirewallEntriesUpdated:
                    ++FirewallEntriesUpdated; break;
          }
     }
     public void RemoveOneEvent(LogEventType type)
     {
          --AllLogEvent;
          switch (type)
          {
               case LogEventType.LoginSucceeded:
                    --LoginSucceeded; break;
               case LogEventType.LoginFailure:
                    --LoginFailure; break;
               case LogEventType.ForgetFailedLogin:
                    --ForgetFailedLogin; break;
               case LogEventType.BanningIP:
                    --BanningIP; break;
               case LogEventType.UnBanningIP:
                    --UnBanningIP; break;
               case LogEventType.FirewallEntriesUpdated:
                    --FirewallEntriesUpdated; break;
          }
     }
}
