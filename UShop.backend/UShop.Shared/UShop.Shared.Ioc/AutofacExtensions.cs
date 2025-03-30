using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using UShop.Shared.Ioc.ServiceProviderFactorySupport;

namespace UShop.Shared.Ioc
{
    public static class AutofacExtensions
    {
        public static IHostBuilder UseAutofac(this IHostBuilder host, Type controllerType, params Assembly[] assemblies)
        {
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            return host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                //containerBuilder.RegisterModule(new ServiceAutofacModel(Assembly.GetExecutingAssembly())); // 仅加载当前程序集
                containerBuilder.RegisterModule(new ServiceAutofacModel(assemblies)); // 指定程序集


                // 监听 Controller 创建时的生命周期，并解析 `FromKeyedService`
                containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .Where(t => controllerType.IsAssignableFrom(t)) // 仅注册 Controller
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
        }

    }
}
