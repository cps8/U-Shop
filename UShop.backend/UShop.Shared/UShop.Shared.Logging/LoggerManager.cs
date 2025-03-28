using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UShop.Shared.Logging
{
    public class LoggerManager: ILoggerManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void Info(string message) => logger.Info(message);
        public void Warn(string message) => logger.Warn(message);
        public void Debug(string message) => logger.Debug(message);
        public void Error(string message) => logger.Error(message);
        public void Error(string message, Exception ex)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                logger.Error(ex, $"{message} | Exception: {ex.Message}");
            }
            else
            {
                logger.Error(ex, $"Exception: {ex.Message}");
            }
        }
        public void Debug<T>(string message, T data) => logger.Debug($"{message} | Data: {JsonSerializer.Serialize(data)}");
        public void Info<T>(string message, T data) => logger.Info($"{message} | Data: {JsonSerializer.Serialize(data)}");
        public void Warn<T>(string message, T data) => logger.Warn($"{message} | Data: {JsonSerializer.Serialize(data)}");
        public void Error<T>(string message, T data) => logger.Error($"{message} | Data: {JsonSerializer.Serialize(data)}");
        public void BatchInfo<T>(string message, params T[] items)
        {
            if (items == null || items.Length == 0)
            {
                logger.Info($"{message} | No items.");
            }
            else
            {
                string jsonData = JsonSerializer.Serialize(items);
                logger.Info($"{message} | Batch Data: {jsonData}");
            }
        }
    }
}
