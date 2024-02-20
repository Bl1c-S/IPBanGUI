using Logic_IPBanUtility.Logic.LogFile;
using Logic_IPBanUtility.Logic.LogFile.Services;

namespace LogEventTest;

[TestClass]
public class LogEventFilterTest
{
     private readonly LogEventFilter _filter;
     public LogEventFilterTest()
     {
          _filter = new LogEventFilter();
     }
     [TestMethod]
     public void FindEventsByType_When4Item()
     {
          var logs = TestLogEvents();
          var result = _filter.FindEventsByType(logs, LogEventType.BanningIP).ToList();
          Assert.AreEqual(4, result.Count);
     }
     [TestMethod]
     public void FindEventsByType_When2Item()
     {
          var logs = TestLogEvents();
          var result = _filter.FindEventsByType(logs, LogEventType.ForgetFailedLogin).ToList();
          Assert.AreEqual(2, result.Count);
     }
     [TestMethod]
     public void FindEventsByType_When1Item()
     {
          var logs = TestLogEvents();
          var result = _filter.FindEventsByType(logs, LogEventType.UnBanningIP).ToList();
          Assert.AreEqual(1, result.Count);
     }
     [TestMethod]
     public void FindEventsByType_When0Item()
     {
          var logs = TestLogEvents();
          var result = _filter.FindEventsByType(logs, LogEventType.FirewallEntriesUpdated).ToList();
          Assert.AreEqual(0, result.Count);
     }
     public List<LogEvent> TestLogEvents()
     {
          List<LogEvent> logs = new() {
               new LogEvent(1, DateTime.Now, "1", LogEventType.BanningIP),
               new LogEvent(1, DateTime.Now, "1", LogEventType.UnBanningIP),
               new LogEvent(1, DateTime.Now, "1", LogEventType.BanningIP),
               new LogEvent(1, DateTime.Now, "1", LogEventType.BanningIP),
               new LogEvent(1, DateTime.Now, "1", LogEventType.BanningIP),
               new LogEvent(1, DateTime.Now, "1", LogEventType.ForgetFailedLogin),
               new LogEvent(1, DateTime.Now, "1", LogEventType.ForgetFailedLogin)
          };
          return logs;
     }
}
