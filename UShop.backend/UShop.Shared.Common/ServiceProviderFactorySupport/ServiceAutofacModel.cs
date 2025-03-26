
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Shared.Common.ServiceProviderFactorySupport
{
    public class ServiceAutofacModel : Autofac.Module
    {
        private readonly Assembly[] _assemblies;

        // 构造函数，接收需要扫描的程序集数组
        public ServiceAutofacModel(params Assembly[] assemblies)
        {
            _assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
        }
        protected override void Load(ContainerBuilder builder)
        {

            // 扫描当前程序集中的所有类型
            var typesWithAttribute = _assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttributes(typeof(ServiceAttribute), true).Length != 0);

            // 遍历所有找到的类型。
            foreach (var type in typesWithAttribute)
            {
                // 获取该类型上的第一个 ServiceAttribute 特性实例。
                ServiceAttribute attribute = (ServiceAttribute)type.GetCustomAttributes(typeof(ServiceAttribute), true).First();

                // 获取该类型实现的所有接口。
                Type[] interfaces = type.GetInterfaces();

                // 遍历接口列表，为每个接口注册该实现类型。
                foreach (var @interface in interfaces)
                {
                    string? keyed = attribute.Keyed;
                    if (keyed is null)
                    {
                        switch (attribute.LifeTime)
                        {
                            case ServiceLifeTime.Scoped:
                                // 使用特性值作为 Key 进行注册
                                builder.RegisterType(type).As(@interface).InstancePerLifetimeScope();
                                break;
                            case ServiceLifeTime.Single:
                                // 使用特性值作为 Key 进行注册
                                builder.RegisterType(type).As(@interface).SingleInstance();
                                break;
                            case ServiceLifeTime.Transient:
                                // 使用特性值作为 Key 进行注册
                                builder.RegisterType(type).As(@interface).InstancePerDependency();
                                break;
                            default:
                                // 使用特性值作为 Key 进行注册
                                builder.RegisterType(type).As(@interface).InstancePerLifetimeScope();
                                break;
                        }
                    }
                    else
                    {
                        switch (attribute.LifeTime)
                        {
                            case ServiceLifeTime.Scoped:
                                // 使用特性值作为 Key 进行注册
                                builder.RegisterType(type).Keyed(keyed, @interface).InstancePerLifetimeScope();
                                break;
                            case ServiceLifeTime.Single:
                                // 使用特性值作为 Key 进行注册
                                builder.RegisterType(type).Keyed(keyed, @interface).SingleInstance();
                                break;
                            case ServiceLifeTime.Transient:
                                // 使用特性值作为 Key 进行注册
                                builder.RegisterType(type).Keyed(keyed, @interface).InstancePerDependency();
                                break;
                            default:
                                // 使用特性值作为 Key 进行注册
                                builder.RegisterType(type).Keyed(keyed, @interface).InstancePerLifetimeScope();
                                break;
                        }
                    }

                }
            }
            // 服务间依赖解析
            builder.Register(ctx =>
            {
                return new ResolvedParameter(
                    (pi, context) => pi.GetCustomAttribute<FromKeyedServiceAttribute>() != null,
                    (pi, context) =>
                    {
                        //var attr = pi.GetCustomAttribute<FromKeyedServiceAttribute>();
                        //return context.ResolveKeyed(attr.Keyed, pi.ParameterType);
                        var attr = pi.GetCustomAttribute<FromKeyedServiceAttribute>();
                        if (pi.ParameterType.IsGenericType &&
                            pi.ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                        {
                            var elementType = pi.ParameterType.GetGenericArguments()[0];
                            return ctx.ResolveKeyed(attr.Keyed, typeof(IEnumerable<>).MakeGenericType(elementType));
                        }
                        return ctx.ResolveKeyed(attr.Keyed, pi.ParameterType);
                    });
            }).As<Parameter>();
        }
    }
}
