
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using UShop.Shared.Common;
using UShop.Shared.Common.ServiceProviderFactorySupport;
using UShop.Shared.Infrastructure;
using UShop.Shared.Logging;

namespace UShop.Services.User.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // 初始化单例ConfigUtils
        ConfigUtils.Initialize(builder.Configuration);

        var logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddControllers();
        builder.Services.AddControllers().AddControllersAsServices();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // 注册日志
        builder.Host.UseNLogLogging();
        builder.Services.AddLoggingServices();

        // 替换默认的 ServiceProviderFactory 为 Autofac。
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        // 注册 Autofac 模块或服务。
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            //containerBuilder.RegisterModule(new ServiceAutofacModel(Assembly.GetExecutingAssembly())); // 仅加载当前程序集
            containerBuilder.RegisterModule(new ServiceAutofacModel(Assembly.Load("UShop.Services.User.Application"))); // 仅appliction程序集


            // 监听 Controller 创建时的生命周期，并解析 `FromKeyedService`
            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t)) // 仅注册 Controller
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.GetCustomAttribute<FromKeyedServiceAttribute>() != null,
                    (pi, ctx) =>
                    {
                        var attr = pi.GetCustomAttribute<FromKeyedServiceAttribute>();
                        if (pi.ParameterType.IsGenericType &&
                            pi.ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                        {
                            var elementType = pi.ParameterType.GetGenericArguments()[0];
                            return ctx.ResolveKeyed(attr.Keyed, typeof(IEnumerable<>).MakeGenericType(elementType));
                        }
                        return ctx.ResolveKeyed(attr.Keyed, pi.ParameterType);
                    }
                ))
                .PropertiesAutowired(); // 确保所有依赖都能正确注入
        });

        // 注册jwt服务
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ////允许的服务器时间偏移量
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = ConfigUtils.Instance.Get("Token:Issuer"),
                    ValidAudiences = ConfigUtils.Instance.Get("Token:Audience").Split(","),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigUtils.Instance.Get("Token:SecretKey")))
                };
            });

        // 注册FreeSql
        builder.Services.AddFreeSql();

        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
