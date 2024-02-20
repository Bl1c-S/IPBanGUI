using Logic_IPBanUtility.Logic.LogFile;

namespace LogEventTest;

[TestClass]
public class LogMessageParserTest
{
    LogMessageParser logMessageParser = new();

    [TestMethod]
    public void LoginSucceeded_WhenUserEmpty()
    {
        string InputLog = "Login succeeded, address: 31.41.92.178, user name: , source: RDP";
        string expectedLog = "Успішний вхід за IP адресою: 31.41.92.178, Ім'я користувача неправильного формату";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.LoginSucceeded, resultLog.Type);
    }

    [TestMethod]
    public void LoginSucceeded_WhenUserCurrent()
    {
        string InputLog = "Login succeeded, address: 31.41.92.178, user name: TSAdmin, source: RDP";
        string expectedLog = "Успішний вхід за IP адресою: 31.41.92.178, Ім'я користувача: TSAdmin";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.LoginSucceeded, resultLog.Type);
    }

    [TestMethod]
    public void LoginFailure_WhenUserEmpty()
    {
        string InputLog = "Login failure: 22.3.200.31, , RDP, 6, 14";
        string expectedLog = "Невдала спроба входу за IP адресою: 22.3.200.31, Ім'я користувача неправильного формату";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.LoginFailure, resultLog.Type);
    }
    [TestMethod]
    public void LoginFailure_WhenUserCurrent()
    {
        string InputLog = "Login failure: 32.123.2.2, ADMINISTRATOR, RDP, 4, 4625";
        string expectedLog = "Невдала спроба входу за IP адресою: 32.123.2.2, Ім'я користувача: ADMINISTRATOR";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.LoginFailure, resultLog.Type);
    }

    [TestMethod]
    public void ForgetFailedLogin_WhenCurrent()
    {
        string InputLog = "Forgetting failed login ip address 87.236.176.76, time expired";
        string expectedLog = "Забуто невдалу спробу входу: 87.236.176.76";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.ForgetFailedLogin, resultLog.Type);
    }

    [TestMethod]
    public void BanningIP_WhenCurrent()
    {
        string InputLog = "Banning ip address: 185.16.39.70, user name: , config blacklisted: False, count: 3, extra info: , duration: 00:05:00";
        string expectedLog = "Заблоковано IP адресу: 185.16.39.70, Час блокування: 00:05:00";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.BanningIP, resultLog.Type);
    }

    [TestMethod]
    public void UnBanningIP_WhenCurrent()
    {
        string InputLog = "Un-banning ip address 193.34.213.119, ban expired";
        string expectedLog = "Розблоковано ІР адресу: 193.34.213.119";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.UnBanningIP, resultLog.Type);
    }

    [TestMethod]
    public void Updatingfirewall_When0IP()
    {
        var InputLog = "Firewall entries updated:";
        var expectedLog = "Оновлені записи брандмауера:";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.FirewallEntriesUpdated, resultLog.Type);
    }

    [TestMethod]
    public void Updatingfirewall_When1IP()
    {
        var InputLog = "Firewall entries updated: 193.34.213.119";
        var expectedLog = "Оновлені записи брандмауера: 193.34.213.119";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.FirewallEntriesUpdated, resultLog.Type);
    }

    [TestMethod]
    public void Updatingfirewall_When2IP()
    {
        var InputLog = "Firewall entries updated: 193.34.213.119, 195.3.221.194";
        var expectedLog = "Оновлені записи брандмауера: 193.34.213.119, 195.3.221.194";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.FirewallEntriesUpdated, resultLog.Type);
    }

    [TestMethod]
    public void Updatingfirewall_When4IP()
    {
        var InputLog = "Firewall entries updated: 193.34.213.119, 195.3.221.194, 1.4.5.6, 324.12.32.0";
        var expectedLog = "Оновлені записи брандмауера: 193.34.213.119, 195.3.221.194, 1.4.5.6, 324.12.32.0";

        var resultLog = logMessageParser.Parse(InputLog);

        Assert.IsNotNull(resultLog);
        Assert.AreEqual(expectedLog, resultLog.Message);
        Assert.AreEqual(LogEventType.FirewallEntriesUpdated, resultLog.Type);
    }
}