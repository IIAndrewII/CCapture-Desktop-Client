using NLog;
using System;

namespace Konecta.Tools.CCaptureClient.Infrastructure

{
    public static class LoggerHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void LogInfo(string message)
        {
            Logger.Info(message);
        }

        public static void LogError(string message, Exception ex = null)
        {
            if (ex != null)
                Logger.Error(ex, message);
            else
                Logger.Error(message);
        }

        public static void LogWarning(string message)
        {
            Logger.Warn(message);
        }

        public static void LogDebug(string message)
        {
            Logger.Debug(message);
        }
    }
}