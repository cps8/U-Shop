using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace UShop.Shared.Logging
{
    public static class LoggingExtensions
    {
        public static IHostBuilder UseNLogLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseNLog(); // 绑定 NLog 到 Host
        }
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
