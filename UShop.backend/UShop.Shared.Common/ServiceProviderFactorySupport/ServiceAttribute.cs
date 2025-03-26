using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Shared.Common.ServiceProviderFactorySupport
{
    /// <summary>
    /// 特性，用于标注为由CI管理依赖注入的类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ServiceAttribute : Attribute
    {
        /// <summary>
        /// keyed, 用于注册时区分同一个接口多个实现类
        /// </summary>
        public string? Keyed { get; }
        /// <summary>
        /// 服务生命周期，默认为Scoped
        /// </summary>
        public ServiceLifeTime LifeTime { get; }

        /// <summary>
        /// 特性，用于标注为由CI管理依赖注入的类
        /// </summary>
        /// <param name="keyed">keyed, 用于注册时区分同一个接口多个实现类</param>
        /// <param name="lifeTime"></param>
        public ServiceAttribute(string? keyed = null, ServiceLifeTime lifeTime = ServiceLifeTime.Scoped)
        {
            Keyed = keyed;
            LifeTime = lifeTime;
        }
    }
}
