using Logic_IPBanUtility.Logic.LogFile;
using System.Diagnostics;

namespace Test_IPBanUtility.LogEvent;
[TestClass]
public class LogEventBuilderPerfomansTest
{
     List<string> logEventsTxt = File.ReadAllLines("C:\\Users\\Bl1c\\Desktop\\logfile.txt").ToList();

     [TestMethod]
     public void GetLogEventsTest10()
     {
          int testCount = 10;
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();

          var LogEventB = new LogEventBuilder();
          for (int i = 0; i < testCount; i++)
          {
               var logEvent = LogEventB.GetLogEvents(logEventsTxt);
          }

          stopwatch.Stop();
          var time = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(time < 2000);
     }

     [TestMethod]
     public void GetLogEventsTest50()
     {
          int testCount = 50;
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();

          var LogEventB = new LogEventBuilder();
          for (int i = 0; i < testCount; i++) 
               LogEventB.GetLogEvents(logEventsTxt);

          stopwatch.Stop();
          var time = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(time < 10000);
     }
}
