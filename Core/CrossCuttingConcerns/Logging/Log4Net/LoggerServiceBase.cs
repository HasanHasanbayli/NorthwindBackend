﻿using System.Reflection;
using System.Xml;
using log4net;
using log4net.Repository;

namespace Core.CrossCuttingConcerns.Logging.Log4Net;

public class LoggerServiceBase
{
    private ILog _log;

    public LoggerServiceBase(string name)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(File.OpenRead("log4net.config"));

        ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
            typeof(log4net.Repository.Hierarchy.Hierarchy));

        log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);

        _log = LogManager.GetLogger(loggerRepository.Name, name);
    }

    private bool IsInfoEnabled => _log.IsInfoEnabled;
    private bool IsDebugEnabled => _log.IsDebugEnabled;
    private bool IsWarningEnabled => _log.IsWarnEnabled;
    private bool IsErrorEnabled => _log.IsErrorEnabled;
    private bool IsFatalEnabled => _log.IsFatalEnabled;

    public void Info(object logMessage)
    {
        if (IsInfoEnabled)
        {
            _log.Info(logMessage);
        }
    }
    
    public void Debug(object logMessage)
    {
        if (IsDebugEnabled)
        {
            _log.Debug(logMessage);
        }
    }
    
    public void Warning(object logMessage)
    {
        if (IsWarningEnabled)
        {
            _log.Warn(logMessage);
        }
    }
    
    public void Error(object logMessage)
    {
        if (IsErrorEnabled)
        {
            _log.Error(logMessage);
        }
    }
    
    public void Fatal(object logMessage)
    {
        if (IsFatalEnabled)
        {
            _log.Fatal(logMessage);
        }
    }
}