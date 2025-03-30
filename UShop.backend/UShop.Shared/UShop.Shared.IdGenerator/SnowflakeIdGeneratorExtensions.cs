using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Shared.IdGenerator
{
    public static class CExtensions
    {
        /// <summary>
        /// 添加SnowflakeIdGenerator依赖
        /// </summary>
        /// <param name="services"></param>
        /// <param name="worderId"></param>
        /// <param name="datacenterId"></param>
        public static void AddSnowflakeIdGenerator(this IServiceCollection services, int worderId = 1, int datacenterId = 1)
        {
            services.AddSingleton<ISnowflakeIdGeneratorService>(new SnowflakeIdGeneratorService(worderId, datacenterId));
        }
    }
}
