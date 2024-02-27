using Logic_IPBanUtility.Logic.LogFile;
using System.Diagnostics;

namespace LogEventTest;
[TestClass]
public class LogEventBuilderPerfomansTest
{
     private List<string> logEventsTxt;

     public LogEventBuilderPerfomansTest()
     {
          var programFolder = AppDomain.CurrentDomain.BaseDirectory;
          var path = $"{programFolder}\\TestLogs\\logfile_10000.txt";
          logEventsTxt = File.ReadAllLines(path).ToList();
     }

     [TestMethod]
     public void GetLogEventsTest()
     {
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();

          var LogEventB = new LogEventBuilder();
          var logEvent = LogEventB.GetLogEvents(logEventsTxt);

          stopwatch.Stop();
          var time = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(time < 300);
     }
}
