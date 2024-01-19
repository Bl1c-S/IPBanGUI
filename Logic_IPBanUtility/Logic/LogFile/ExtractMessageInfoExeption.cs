namespace Logic_IPBanUtility.Logic.LogFile;

public class ExtractMessageInfoException : Exception
{
     public ExtractMessageInfoException(string logMessage)
         : base($"Error extracting information from log message: {logMessage}.")
     {
     }
}