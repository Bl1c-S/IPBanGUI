
namespace Logic_IPBanUtility.Interfaces.Services
{
     public interface IFileManager
     {
          List<string> ReadAllLines(string path);
          List<string> ReadAllLinesFromIndexToEnd(string filePath, int startLineIndex);
          void WriteAllLines(string path, IEnumerable<string> contents);
          void WriteAllText(string path, string contents);
          T GetJson<T>(string path);
          void SaveJson<T>(string path, T content);
          void CreateDefaultDirectory(string path);
          void DeleteFiles(IEnumerable<string> paths);
     }
}
