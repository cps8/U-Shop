
using Autofac.Core;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using UShop.Shared.Common.ServiceProviderFactorySupport;
using UShop.Shared.Common;

namespace UShop.Service.User.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // ��ʼ������ConfigUtils
            ConfigUtils.Initialize(builder.Configuration);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddControllersAsServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // �滻Ĭ�ϵ� ServiceProviderFactory Ϊ Autofac��
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            // ע�� Autofac ģ������
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new ServiceAutofacModel(Assembly.GetExecutingAssembly())); // �����ص�ǰ����

                // ���� Controller ����ʱ���������ڣ������� `FromKeyedService`
                containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .Where(t => typeof(ControllerBase).IsAssignableFrom(t)) // ��ע�� Controller
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
                    .PropertiesAutowired(); // ȷ����������������ȷע��
            });

            // ע��jwt����
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ////�Ƿ���֤Token��Ч�ڣ�ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Ա�
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ////����ķ�����ʱ��ƫ����
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = ConfigUtils.Instance.Get("Token:Issuer"),
                        ValidAudiences = ConfigUtils.Instance.Get("Token:Audience").Split(","),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigUtils.Instance.Get("Token:SecretKey")))
                    };
                });

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
}
