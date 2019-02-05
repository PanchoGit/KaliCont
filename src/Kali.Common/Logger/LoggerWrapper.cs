using System;
using log4net;
using Kali.Common.Logger.Interfaces;

namespace Kali.Common.Logger
{
    public class LoggerWrapper<T> : ILoggerWrapper<T>
    {
        private const string MessageDelimiter = " - ";

        private readonly ILog log;

        public LoggerWrapper()
        {
            log = LogManager.GetLogger(typeof(T));
        }

        public void LogError(string message)
        {
            log.Error(message);
        }

        public void LogError(Exception exception)
        {
            log.Error(exception.Message, exception);
        }

        public void LogError(string message, Exception exception)
        {
            log.Error(message + MessageDelimiter + exception.Message, exception);
        }
    }
}
