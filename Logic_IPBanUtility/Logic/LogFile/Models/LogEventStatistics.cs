namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventStatistics
{
     public Action? StatisticsChanged;
     private Dictionary<LogEventType, int> statistics;
     public LogEventStatistics()
     {
          statistics = Enum.GetValues(typeof(LogEventType))
        .Cast<LogEventType>()
        .ToDictionary(key => key, value => 0);
     }
     public int AllLogEvent => GetAll();
     public int LoginSucceeded => Get(LogEventType.LoginSucceeded);
     public int LoginFailure => Get(LogEventType.LoginFailure);
     public int ForgetFailedLogin => Get(LogEventType.ForgetFailedLogin);
     public int BanningIP => Get(LogEventType.BanningIP);
     public int UnBanningIP => Get(LogEventType.UnBanningIP);
     public int FirewallEntriesUpdated => Get(LogEventType.FirewallEntriesUpdated);

     public void AddEvents(LogEventType type, int count = 1)
     {
          statistics[type] += count;
          StatisticsChanged?.Invoke();
     }
     public void RemoveEvents(LogEventType type, int count = 1)
     {
          statistics[type] -= count;
          StatisticsChanged?.Invoke();
     }
     public void Clear()
     {
          foreach (var key in statistics.Keys.ToList())
               statistics[key] = 0;
          StatisticsChanged?.Invoke();
     }
     public int Get(LogEventType type) => statistics[type];
     private int GetAll()
     {
          int all = 0;
          foreach (var value in statistics.Values)
               all += value;
          return all;
     }
}
