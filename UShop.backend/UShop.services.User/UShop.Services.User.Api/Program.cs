
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Reflection;
using System.Text;
using UShop.Shared.Common;
using UShop.Shared.Infrastructure;
using UShop.Shared.Ioc;
using UShop.Shared.Logging;

namespace UShop.Services.User.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var logger = LogManager.GetCurrentClassLogger();
        try
        {
            logger.Info(PrintLogo());
            var builder = WebApplication.CreateBuilder(args);
            // 初始化单例ConfigUtils
            ConfigUtils.Initialize(builder.Configuration);

            var logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
                logger.Info("created log path {0}", logDirectory);
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
            builder.Host.UseAutofac(typeof(ControllerBase), Assembly.Load("UShop.Services.User.Application"), Assembly.Load("UShop.Services.User.Infrastructure"));
            logger.Info("Ioc done");

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
            logger.Info("jwt done");

            // 注册FreeSql
            builder.Services.AddFreeSql();
            logger.Info("orm done");

            builder.Services.AddHttpContextAccessor();
            logger.Info("Services configured successfully.");

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
        catch (Exception ex)
        {
            logger.Error("An error occurred during application startup.", ex);
        }
    }

    #region 打印logo
    private static string PrintLogo()
    {
        return """

            /***
            *           _____                            _____                    _____                   _______                   _____          
            *          /\    \                          /\    \                  /\    \                 /::\    \                 /\    \         
            *         /::\____\                        /::\    \                /::\____\               /::::\    \               /::\    \        
            *        /:::/    /                       /::::\    \              /:::/    /              /::::::\    \             /::::\    \       
            *       /:::/    /                       /::::::\    \            /:::/    /              /::::::::\    \           /::::::\    \      
            *      /:::/    /                       /:::/\:::\    \          /:::/    /              /:::/~~\:::\    \         /:::/\:::\    \     
            *     /:::/    /                       /:::/__\:::\    \        /:::/____/              /:::/    \:::\    \       /:::/__\:::\    \    
            *    /:::/    /                        \:::\   \:::\    \      /::::\    \             /:::/    / \:::\    \     /::::\   \:::\    \   
            *   /:::/    /      _____            ___\:::\   \:::\    \    /::::::\    \   _____   /:::/____/   \:::\____\   /::::::\   \:::\    \  
            *  /:::/____/      /\    \          /\   \:::\   \:::\    \  /:::/\:::\    \ /\    \ |:::|    |     |:::|    | /:::/\:::\   \:::\____\ 
            * |:::|    /      /::\____\        /::\   \:::\   \:::\____\/:::/  \:::\    /::\____\|:::|____|     |:::|    |/:::/  \:::\   \:::|    |
            * |:::|____\     /:::/    /        \:::\   \:::\   \::/    /\::/    \:::\  /:::/    / \:::\    \   /:::/    / \::/    \:::\  /:::|____|
            *  \:::\    \   /:::/    /          \:::\   \:::\   \/____/  \/____/ \:::\/:::/    /   \:::\    \ /:::/    /   \/_____/\:::\/:::/    / 
            *   \:::\    \ /:::/    /            \:::\   \:::\    \               \::::::/    /     \:::\    /:::/    /             \::::::/    /  
            *    \:::\    /:::/    /              \:::\   \:::\____\               \::::/    /       \:::\__/:::/    /               \::::/    /   
            *     \:::\__/:::/    /                \:::\  /:::/    /               /:::/    /         \::::::::/    /                 \::/____/    
            *      \::::::::/    /                  \:::\/:::/    /               /:::/    /           \::::::/    /                   ~~          
            *       \::::::/    /                    \::::::/    /               /:::/    /             \::::/    /                                
            *        \::::/    /                      \::::/    /               /:::/    /               \::/____/                                 
            *         \::/____/                        \::/    /                \::/    /                 ~~                                       
            *          ~~                               \/____/                  \/____/                                                           
            *                                                                                                                                      
            */
            """;
    }
    #endregion
}
