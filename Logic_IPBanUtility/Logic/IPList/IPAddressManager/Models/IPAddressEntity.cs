namespace Logic_IPBanUtility.Logic.IPList;

public class IPAddressEntity
{
    public IPAddressEntity(string iPAddressText, DateTime? lastFailedLogin, int failedLoginCount, DateTime? banDate, DateTime? banEndDate, string? userName)
    {
        IPAddressText = iPAddressText;
        LastFailedLogin = lastFailedLogin;
        FailedLoginCount = failedLoginCount;
        BanDate = banDate;
        BanEndDate = banEndDate;
        UserName = userName;
    }
    public string IPAddressText { get; set; }
    public DateTime? LastFailedLogin { get; set; }
    public int FailedLoginCount { get; set; }
    public DateTime? BanDate { get; set; }
    public DateTime? BanEndDate { get; set; }
    public string? UserName { get; set; }
}