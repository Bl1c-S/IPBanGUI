namespace Logic_IPBanUtility.Logic.LogFile
{
    public class LogTypeAndMessageDTO
    {
          public LogTypeAndMessageDTO(LogEventType logEventType, string message)
          {
               Type = logEventType;
               Message = message;
          }

          public LogEventType Type { get; set; }
          public string Message { get; set; }
     }
}
