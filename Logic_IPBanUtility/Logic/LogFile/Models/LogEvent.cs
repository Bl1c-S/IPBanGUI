namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEvent
{
    public LogEvent(int id, TimeSpan time, string message, LogEventType type)
    {
        Id = id;
        Time = time.ToString(@"hh\:mm\:ss");
        Message = message;
        Type = type;
    }

    public int Id { get; }
    public string Time { get; }
    public string Message { get; }
    public LogEventType Type { get; }
}
