namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEvent
{
    public LogEvent(int id, DateTime date, string message)
    {
        Id = id;
        Date = date;
        Message = message;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public string Message { get; }
}
