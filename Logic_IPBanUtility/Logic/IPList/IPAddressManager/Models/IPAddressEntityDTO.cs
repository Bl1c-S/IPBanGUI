using System.ComponentModel.DataAnnotations;

namespace Logic_IPBanUtility.Logic.IPList;

public class IPAddressEntityDTO
{
     [Key]
     public string IPAddressText { get; set; }
     public byte[] IPAddress { get; set; }
     public long? LastFailedLogin { get; set; }
     public int FailedLoginCount { get; set; }
     public long? BanDate { get; set; }
     public long? BanEndDate { get; set; }
     public int State { get; set; } = 0;
     public string UserName { get; set; }
     public string Source { get; set; } = "RDP";

     public IPAddressEntityDTO(byte[] iPAddress, string iPAddressText, long? lastFailedLogin, int failedLoginCount, long? banDate, long? banEndDate, string? userName)
     {
          IPAddressText = iPAddressText;
          IPAddress = iPAddress;
          LastFailedLogin = lastFailedLogin;
          FailedLoginCount = failedLoginCount;
          BanDate = banDate;
          BanEndDate = banEndDate;
          UserName = userName ?? string.Empty;
     }

     public IPAddressEntity ToEntity()
     {
          var lastFailedLogin = ConvertUnixDateToNormal(LastFailedLogin);
          var banDate = ConvertUnixDateToNormal(BanDate);
          var banEndDate = ConvertUnixDateToNormal(BanEndDate);
          return new(IPAddressText, lastFailedLogin, FailedLoginCount, banDate, banEndDate, UserName);
     }
     public static IPAddressEntityDTO ToDTO(IPAddressEntity entity)
     {
          return new(
                System.Net.IPAddress.Parse(entity.IPAddressText).GetAddressBytes(),
               entity.IPAddressText,
               ConvertNormalDateToUnix(entity.LastFailedLogin),
               entity.FailedLoginCount,
               ConvertNormalDateToUnix(entity.BanDate),
               ConvertNormalDateToUnix(entity.BanEndDate),
               entity.UserName);
     }

     private static DateTime? ConvertUnixDateToNormal(long? msTime)
     {
          if (msTime == null || msTime == 0) return null;

          var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(msTime.Value);
          return dateTimeOffset.DateTime;
     }

     private static long ConvertNormalDateToUnix(DateTime? dateTime)
     {
          if (dateTime == null) return 0;

          var dateTimeOffset = new DateTimeOffset(dateTime.Value);
          return dateTimeOffset.ToUnixTimeSeconds() * 10000000 + 621355968000000000;
     }
}