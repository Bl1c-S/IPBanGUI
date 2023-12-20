using NLog;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Logic_IPBanUtility.Services;

public class IPBanLogger
{
     //TODO Внедрити логер

    private Logger _logger;
    public IPBanLogger()
    {
        //targetType = obj.GetType().ToString(); //TODO додати типізацію помилок 
        _logger = LogManager.GetCurrentClassLogger();
    }

    #region Parent methods
    public void Info(string message) => _logger.Info(message);
    public void Error(string message) => _logger.Error(message);
    public void Warn(string message) => _logger.Warn(message);
    public void Trace(string message) => _logger.Trace(message);
    #endregion

    #region Key
    public string KeyNotFound(string key)
    {
        var message = $"No faund key: <{key}> in file <ipban.config> but you can edit the key name in this file <keynames.txt>";
        _logger.Error(message);
        return message;
    }
    public string KeyValueNotFound(string key)
    {
        var message = $"No faund key value: <{key}> in file <ipban.config> but you can edit the key name in this file <keynames.txt>";
        _logger.Error(message);
        return message;
    }
    #endregion

    #region File
    public string FileNotFound(string filePath)
    {
        var message = $"Not found required file <{filePath}>.";
        _logger.Error(message);
        return message;
    }
    public string FileFound(string filePath)
    {
        var message = $"Successfully found required file <{filePath}>.";
        _logger.Info(message);
        return message;
    }
    public string FileReadProblems(string filePath)
    {
        var message = $"Empty required file <{filePath}> or other problems with reading.";
        _logger.Error(message);
        return message;
    }
    public string FileReaded(string filePath)
    {
        var message = $"Successfully readed file contents to the <{filePath}>.";
        _logger.Info(message);
        return message;
    }
    #endregion

    #region Directory
    public string DirectoryNotFound(string directoryPath)
    {
        var message = $"Not found specified directory <{directoryPath}>.";
        _logger.Error(message);
        return message;
    }
    public string DirectoryFound(string directoryPath)
    {
        var message = $"Successfully found required directory <{directoryPath}>.";
        _logger.Info(message);
        return message;
    }
    #endregion
}
