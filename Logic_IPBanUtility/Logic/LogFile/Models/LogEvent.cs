namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEvent
{
    public LogEvent(int id, DateTime date, string message, LogEventType type)
    {
        Id = id;
        Date = date;
        Message = message;
        Type = type;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public string Message { get; }
    public LogEventType Type { get; }
}
