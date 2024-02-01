using Logic_IPBanUtility.Logic.LogFile;
using System.Collections.ObjectModel;

namespace Test_IPBanUtility.LogEventTest.LogEventFilter
{
     public class LogEventFilterTest
     {
          ObservableCollection<LogEvent> _logEvents = new();

          public LogEventFilterTest()
          {
               CreateLogEvents();
          }

          [TestMethod]
          public void Test1()
          {
          }


          private void CreateLogEvents()
          {
               _logEvents.Add(new LogEvent(1, DateTime.Now, "BanningIP", LogEventType.BanningIP));
               _logEvents.Add(new LogEvent(2, DateTime.Now, "FirewallEntriesUpdated", LogEventType.FirewallEntriesUpdated));
               _logEvents.Add(new LogEvent(3, DateTime.Now, "FirewallEntriesUpdated2", LogEventType.FirewallEntriesUpdated));
               _logEvents.Add(new LogEvent(4, DateTime.Now, "ForgetFailedLogin", LogEventType.ForgetFailedLogin));
               _logEvents.Add(new LogEvent(5, DateTime.Now, "UnBanningIP", LogEventType.UnBanningIP));
               _logEvents.Add(new LogEvent(6, new DateTime(999999), "UnBanningIP", LogEventType.UnBanningIP));
               _logEvents.Add(new LogEvent(7, DateTime.Now, "UnBanningIP", LogEventType.UnBanningIP));
               _logEvents.Add(new LogEvent(8, DateTime.Now, "UnBanningIP", LogEventType.UnBanningIP));
               _logEvents.Add(new LogEvent(9, DateTime.Now, "LoginSucceeded", LogEventType.LoginSucceeded));
          }
     }
}
