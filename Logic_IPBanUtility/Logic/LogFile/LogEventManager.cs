﻿using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.LogFile;

public class LogEventManager
{
     private readonly FileManager _fileManager;
     private readonly LogEventBuilder _logEventBuilder = new();
     private readonly string _logFilePath;
     private readonly int _lastLogFileStringCount;

     public LogEventManager(Settings settings, FileManager fileManager)
     {
          _logFilePath = settings.IPBan.Logfile;
          _fileManager = fileManager;
     }

     public List<LogEvent> ReadAllLogEvents()
     {
          var logs = _fileManager.ReadAllLines(_logFilePath);
          return _logEventBuilder.GetLogEvents(logs);
     }

     public List<LogEvent>? ReadNewLogEvents()
     {
          var newLogs = _fileManager.ReadAllLinesFromIndexToEnd(_logFilePath, _lastLogFileStringCount);
          if (newLogs is null)
               return null;
          return _logEventBuilder.GetLogEvents(newLogs, _lastLogFileStringCount);
     }
}
