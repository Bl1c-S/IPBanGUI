namespace Test_IPBanUtility.LogEvent;
[TestClass]
public class LogEventBuilderTest
{
    string dateFormat = "yyyy-MM-dd HH:mm:ss.ffff";
    List<string> logEventsTxt = File.ReadAllLines("C:\\Users\\Bl1c\\Desktop\\logfile.txt").ToList();

    [TestMethod]
    public void Test1()
    {
        var LogEventB = new LogEventBuilder();

        foreach (var log in logEventsTxt)
        {
            var logEvent = LogEventB.GetLogEvent(log, 0);
        }
    }
}
