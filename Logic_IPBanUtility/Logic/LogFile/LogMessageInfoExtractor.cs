namespace Logic_IPBanUtility.Logic.LogFile;

public class LogMessageInfoExtractor
{
     public string? ExtractStringFromStartAtributeToEndString(string logMessage, string startAttribute)
     {
          var startIndex = logMessage.IndexOf(startAttribute) + startAttribute.Length;

          if (startIndex == logMessage.Length)
               return null;

          string result = logMessage.Substring(startIndex);
          return result;
     }

     public string? ExtractStringFromStartAtributeToEndAttribute(string logMessage, string startAttribute, string endAttribute)
     {
          int startIndex = logMessage.IndexOf(startAttribute) + startAttribute.Length;
          int endIndex = logMessage.IndexOf(endAttribute, startIndex);

          if (endIndex == -1 || startIndex == endIndex)
               return null;

           string result = logMessage.Substring(startIndex, endIndex - startIndex);
          return result;
     }
}