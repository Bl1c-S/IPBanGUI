namespace Test_IPBanUtility.LogEventTest.ManagerTest;

[TestClass]
public class LogEventManagerPerfomansTest
{
     int testCount = 1000;
     private readonly string path_10;
    private readonly string path_10000;

     public LogEventManagerPerfomansTest()
     {
          var programFolder = AppDomain.CurrentDomain.BaseDirectory;
          path_10 = $"{programFolder}\\TestLogs\\logfile_10.txt";
          path_10000 = $"{programFolder}\\TestLogs\\logfile_10000.txt";
     }

    [TestMethod]
    public void ReadLines_When10Lines() => ReadLinesTest(path_10);
    [TestMethod]
    public void ReadLines_When10000Lines() => ReadLinesTest(path_10000);

    [TestMethod]
    public void ReadAllLines_When10Lines() => ReadAllLinesTest(path_10);
    [TestMethod]
    public void ReadAllLines_When10000Lines() => ReadAllLinesTest(path_10000);

    [TestMethod]
    public void StreamReader_When10Lines() => StreamReaderTest(path_10);
    [TestMethod]
    public void StreamReader_When10000Lines() => StreamReaderTest(path_10000);

    public void ReadLinesTest(string path)
    {
        List<string> lines = new List<string>();
        for (int i = 0; i < testCount; i++)
            lines = ReadLines(path).ToList();
    }
    public void StreamReaderTest(string path)
    {
        List<string> lines = new List<string>();
        for (int i = 0; i < testCount; i++)
            lines = StreamReader(path);
    }

    public void ReadAllLinesTest(string path)
    {
        List<string> lines = new List<string>();

        for (int i = 0; i < testCount; i++)
            lines = File.ReadAllLines(path).ToList();
    }




    public IEnumerable<string> ReadLines(string filePath)
    {
        return File.ReadLines(filePath).ToList();
    }

    public List<string> StreamReader(string filePath, int n = 999999999)
    {
        List<string> lines = new List<string>();
        using (StreamReader reader = new StreamReader(filePath))
        {
            for (int i = 0; i < n; i++)
            {
                var line = reader.ReadLine();
                if (line == null)
                    break;

                lines.Add(line);
            }
        }
        return lines;
    }
}
