using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UShop.Shared.Common;

namespace UShop.Shared.Infrastructure
{
    /// <summary>
    /// 统一注册FreeSql扩展
    /// </summary>
    public static class FreeSqlServiceCollectionExtensions
    {
        /// <summary>
        /// 注册freesql
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbType">配置文件中配置的数据库类型</param>
        /// <param name="dbKey">配置文件中配置的数据库连接串</param>
        /// <param name="assemblies">持久化程序集</param>
        /// <exception cref="ArgumentException">dbType或dbKey不存在与配置文件中</exception>
        /// <exception cref="NotSupportedException">配置文件中的dbType与本扩展中规定的不相符</exception>
        public static void AddFreeSql(this IServiceCollection services, string dbType = "Database:Type", string dbKey = "Database:ConnectionString", params Assembly[] assemblies)
        {
            var connectionString = ConfigUtils.Instance.Get(dbKey);
            dbType = ConfigUtils.Instance.Get(dbType).ToLower();

            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(dbType))
            {
                throw new ArgumentException("Database connection configuration is missing.");
            }

            IFreeSql fsql = dbType switch
            {
                "postgresql" => new FreeSqlBuilder()
                    .UseConnectionString(FreeSql.DataType.PostgreSQL, connectionString)
                    .UseNameConvert(FreeSql.Internal.NameConvertType.PascalCaseToUnderscoreWithLower)
                    .UseGenerateCommandParameterWithLambda(true)
                    .UseAutoSyncStructure(false) // 自动同步实体结构到数据库，只有CRUD时才会生成表, 不建议开启，生产环境严禁开启
                    .Build(),

                "mysql" => new FreeSqlBuilder()
                    .UseConnectionString(FreeSql.DataType.MySql, connectionString)
                    .UseNameConvert(FreeSql.Internal.NameConvertType.PascalCaseToUnderscoreWithLower)
                    .UseGenerateCommandParameterWithLambda(true)
                    .UseAutoSyncStructure(false) // 自动同步实体结构到数据库，只有CRUD时才会生成表, 不建议开启，生产环境严禁开启
                    .Build(),

                _ => throw new NotSupportedException($"Unsupported database type: {dbType}")
            };

            fsql.Aop.CommandBefore += Aop_CommandBefore;

            if (assemblies.Length > 0)
            {
                services.AddFreeRepository(assemblies);
                services.AddSingleton<IFreeSql>(r=> r.GetService<UnitOfWorkManager>().Orm);
                services.AddScoped<UnitOfWorkManager>(r=> new UnitOfWorkManager(fsql)); // 仓储事务， 保证事务一致性
            }
            else
            {
                services.AddSingleton<IFreeSql>(fsql);
                services.AddScoped<UnitOfWorkManager>();
            }
        }

        private static void Aop_CommandBefore(object? sender, FreeSql.Aop.CommandBeforeEventArgs e)
        {
            
        }
    }
}
