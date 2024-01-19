using System.Diagnostics;

namespace Test_IPBanUtility.LogEvent;

[TestClass]
public class LogMessageParserPerfomanseTest
{
     LogMessageParserTest parserTest = new();
     int testCount = 200;
     int maxMS_TestTime = 5;

     [TestMethod]
     public void A_BuferTest()
     {
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();
          for (int i = 0; i < testCount; i++)
               parserTest.LoginSucceeded_WhenUserEmpty();
          stopwatch.Stop();
          var testTime = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(testTime < 100);
     }

     [TestMethod]
     public void LoginSucceeded_WhenUserEmpty()
     {
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();
          for (int i = 0; i < testCount; i++)
               parserTest.LoginSucceeded_WhenUserEmpty();
          stopwatch.Stop();
          var testTime = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(testTime < maxMS_TestTime);
     }
     [TestMethod]
     public void LoginSucceeded_WhenUserCurrent()
     {
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();
          for (int i = 0; i < testCount; i++)
               parserTest.LoginSucceeded_WhenUserCurrent();
          stopwatch.Stop();
          var testTime = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(testTime < maxMS_TestTime);
     }
     [TestMethod]
     public void BanningIP_WhenCurrent()
     {
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();
          for (int i = 0; i < testCount; i++)
               parserTest.BanningIP_WhenCurrent();
          stopwatch.Stop();
          var testTime = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(testTime < maxMS_TestTime);
     }
     [TestMethod]
     public void UnBanningIP_WhenCurrent()
     {
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();
          for (int i = 0; i < testCount; i++)
               parserTest.UnBanningIP_WhenCurrent();
          stopwatch.Stop();
          var testTime = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(testTime < maxMS_TestTime);
     }
     [TestMethod]
     public void ForgetFailedLogin_WhenCurrent()
     {
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();
          for (int i = 0; i < testCount; i++)
               parserTest.ForgetFailedLogin_WhenCurrent();
          stopwatch.Stop();
          var testTime = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(testTime < maxMS_TestTime);
     }
     [TestMethod]
     public void LoginFailure_WhenUserCurrent()
     {
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();
          for (int i = 0; i < testCount; i++)
               parserTest.LoginFailure_WhenUserCurrent();
          stopwatch.Stop();
          var testTime = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(testTime < maxMS_TestTime);
     }
     [TestMethod]
     public void LoginFailure_WhenUserEmpty()
     {
          Stopwatch stopwatch = new Stopwatch();
          for (int i = 0; i < testCount; i++)
               stopwatch.Start();
          parserTest.LoginFailure_WhenUserEmpty();
          stopwatch.Stop();
          var testTime = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(testTime < maxMS_TestTime);
     }
     [TestMethod]
     public void Updatingfirewall()
     {
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();
          for (int i = 0; i < testCount; i++)
               parserTest.Updatingfirewall_When2IP();
          stopwatch.Stop();
          var testTime = stopwatch.ElapsedMilliseconds;
          Assert.IsTrue(testTime < maxMS_TestTime);
     }
}
