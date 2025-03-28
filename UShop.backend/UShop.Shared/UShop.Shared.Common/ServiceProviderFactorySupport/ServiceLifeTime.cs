using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Shared.Common.ServiceProviderFactorySupport
{
    /// <summary>
    /// 服务生命周期
    /// </summary>
    public enum ServiceLifeTime
    {
        /// <summary>
        /// 作用域
        /// </summary>
        Scoped = 0,
        /// <summary>
        /// 单例
        /// </summary>
        Single = 1,
        /// <summary>
        /// 瞬时
        /// </summary>
        Transient = 2
    }
}
