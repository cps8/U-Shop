using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using UShop.Shared.Common;

namespace UShop.Shared.Infrastructure
{
    /// <summary>
    /// 统一注册FreeSql扩展
    /// </summary>
    public static class FreeSqlServiceCollectionExtensions
    {
        public static void AddFreeSql(this IServiceCollection services, string dbType = "Database:Type", string dbKey = "Database:ConnectionString")
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
                    .UseGenerateCommandParameterWithLambda(true)
                    .UseAutoSyncStructure(false) // 自动同步实体结构到数据库，只有CRUD时才会生成表, 不建议开启，生产环境严禁开启
                    .Build(),

                "mysql" => new FreeSqlBuilder()
                    .UseConnectionString(FreeSql.DataType.MySql, connectionString)
                    .UseGenerateCommandParameterWithLambda(true)
                    .UseAutoSyncStructure(false) // 自动同步实体结构到数据库，只有CRUD时才会生成表, 不建议开启，生产环境严禁开启
                    .Build(),

                _ => throw new NotSupportedException($"Unsupported database type: {dbType}")
            };

            services.AddSingleton<IFreeSql>(fsql);
            //services.AddFreeRepository()
            services.AddScoped<UnitOfWorkManager>(); // 保证事务一致性
        }
    }
}
