using System;

namespace Kali.Common.Logger.Interfaces
{
    public interface ILoggerWrapper<T>
    {
        void LogError(string message);

        void LogError(Exception exception);

        void LogError(string message, Exception exception);
    }
}