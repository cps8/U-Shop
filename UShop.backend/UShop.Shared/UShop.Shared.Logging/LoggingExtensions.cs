using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace UShop.Shared.Logging
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddLoggingServices(this IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog();  // 使用 NLog
            });

            return services;
        }
    }
}
