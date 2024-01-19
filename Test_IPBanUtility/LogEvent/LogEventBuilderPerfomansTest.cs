using System.Diagnostics;

namespace Test_IPBanUtility;
[TestClass]
public class LogEventBuilderPerfomansTest
{
     List<string> logEventsTxt = File.ReadAllLines("C:\\Users\\Bl1c\\Desktop\\logfile.txt").ToList();

     [TestMethod]
     public void PerfomansTest10()
     {
          int testCount = 10;
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();

          var LogEventB = new LogEventBuilder();
          for (int i = 0; i < testCount; i++)
               foreach (var log in logEventsTxt)
               {
                    var logEvent = LogEventB.GetLogEvent(log, 0);
               }

          stopwatch.Stop();
          var time = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(time < 2000);
     }

     [TestMethod]
     public void PerfomansTest50()
     {
          int testCount = 50;
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();

          var LogEventB = new LogEventBuilder();
          for (int i = 0; i < testCount; i++)
               foreach (var log in logEventsTxt)
               {
                    var logEvent = LogEventB.GetLogEvent(log, 0);
               }

          stopwatch.Stop();
          var time = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(time < 10000);
     }
}
