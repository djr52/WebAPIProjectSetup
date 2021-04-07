using System;
using Contracts;
using NLog;

namespace LoggerService
{
    public class LoggerManager : ILoggingManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public LoggerManager()
        {

        }
        public void LogInfo(string message)
        {

        }
        public void LogWarn(string message)
        {

        }
        public void LogDebug(string message)
        {

        }
        public void LogError(string message)
        {

        }

    }
}
