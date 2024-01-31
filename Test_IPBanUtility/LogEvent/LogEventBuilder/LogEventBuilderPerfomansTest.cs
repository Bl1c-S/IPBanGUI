using System.Diagnostics;

namespace Test_IPBanUtility.LogEvent.Builder;
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
