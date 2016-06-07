using System;
using log4net;

namespace CatchMe.Infrastructure.Abstract
{
    public class Logger : ILogger
    {
        private readonly ILog _logger;

        public Logger(string appenderName)
        {
            _logger = LogManager.GetLogger(appenderName);            
        }

        public void LogError(Exception exception)
        {
            _logger.Error(exception);
        }

        public void LogError(string message, params object[] param)
        {
            _logger.ErrorFormat(message, param);
        }

        public void LogDebug(string message, params object[] param)
        {
            _logger.DebugFormat(message, param);
        }

        public void LogInfo(string message, params object[] param)
        {
            _logger.InfoFormat(message, param);
        }
    }
}
