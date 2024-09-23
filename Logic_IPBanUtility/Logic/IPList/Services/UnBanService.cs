using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.IPList.Services
{
     /// <summary>
     /// We have to create a .txt file with the IPs we will not unban
     /// </summary>
     public class UnBanService
     {
          private readonly string filePath;
          private readonly List<string> unBanList = new();

          public UnBanService(Settings settings)
          {
               filePath = Path.Combine(settings.IPBan.Folder, "unban.txt");
          }

          public void CreateFile() => File.WriteAllLines(filePath, unBanList);

          public void Add(string unBanIP) => unBanList.Add(unBanIP);
     }
}