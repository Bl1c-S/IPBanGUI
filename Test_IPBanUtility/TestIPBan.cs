namespace Test_IPBanUtility;

public class TestIPBan
{
     private readonly string _folder;
     public TestIPBan(string customFolder)
     {
          _folder = AppDomain.CurrentDomain.BaseDirectory + customFolder;
     }

     public IPBan CreateEmptyIPBan()
     {
          CreateEmptyFiles("ipban.config");
          CreateEmptyFiles("ipban.sqlite");
          return IPBan.Create(_folder);
     }
     private void CreateEmptyFiles(string name)
     {
          var path = Path.Combine(_folder, name);
          if (!File.Exists(path)) File.Create(path);
     }
}
