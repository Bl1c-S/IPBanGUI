namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventStatistics
{
     public Action? StatisticsChanged;
     public int AllLogEvent { get; private set; }
     public int LoginSucceeded { get; private set; }
     public int LoginFailure { get; private set; }
     public int ForgetFailedLogin { get; private set; }
     public int BanningIP { get; private set; }
     public int UnBanningIP { get; private set; }
     public int FirewallEntriesUpdated { get; private set; }

     public void AddEvents(LogEventType type, int count = 1)
     {
          AllLogEvent += count;
          switch (type)
          {
               case LogEventType.LoginSucceeded:
                    LoginSucceeded += count; break;
               case LogEventType.LoginFailure:
                    LoginFailure += count; break;
               case LogEventType.ForgetFailedLogin:
                    ForgetFailedLogin += count ; break;
               case LogEventType.BanningIP:
                    BanningIP += count ; break;
               case LogEventType.UnBanningIP:
                    UnBanningIP += count; break;
               case LogEventType.FirewallEntriesUpdated:
                    FirewallEntriesUpdated += count; break;
          }
          StatisticsChanged?.Invoke();
     }
     public void RemoveEvents(LogEventType type, int count = 1)
     {
          //AllLogEvent -= count; З списку всіх логів ми ніколи не видаляємо обєкти, тому й не потрібно змінювати число всіх.
          switch (type)
          {
               case LogEventType.LoginSucceeded:
                    LoginSucceeded -= count; break;
               case LogEventType.LoginFailure:
                    LoginFailure -= count; break;
               case LogEventType.ForgetFailedLogin:
                    ForgetFailedLogin -= count; break;
               case LogEventType.BanningIP:
                    BanningIP -= count; break;
               case LogEventType.UnBanningIP:
                    UnBanningIP -= count; break;
               case LogEventType.FirewallEntriesUpdated:
                    FirewallEntriesUpdated -= count; break;
          }
          StatisticsChanged?.Invoke();
     }
     public void Clear()
     {
          AllLogEvent = 0;
          LoginSucceeded = 0;
          LoginFailure = 0;
          ForgetFailedLogin = 0;
          BanningIP = 0;
          UnBanningIP = 0;
          FirewallEntriesUpdated = 0;
          StatisticsChanged?.Invoke();
     }
}
