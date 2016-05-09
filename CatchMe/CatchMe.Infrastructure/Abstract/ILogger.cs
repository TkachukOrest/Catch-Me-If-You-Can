using System;

namespace CatchMe.Infrastructure.Abstract
{
    public interface ILogger
    {
        void LogError(Exception exception);
        void LogError(string message, params object[] param);
        void LogInfo(string message, params object[] param);
        void LogDebug(string message, params object[] param);
    }
}
