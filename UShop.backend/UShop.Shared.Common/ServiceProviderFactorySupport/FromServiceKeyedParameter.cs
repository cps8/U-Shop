using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Common.ServiceProviderFactorySupport
{
    public class KeyedServiceParameter : Parameter
    {
        private readonly string _keyed;

        public KeyedServiceParameter(string keyed)
        {
            _keyed = keyed ?? throw new ArgumentNullException(nameof(keyed));
        }

        public override bool CanSupplyValue(ParameterInfo pi, IComponentContext context, out Func<object> valueProvider)
        {
            // 检查是否有 KeyedServiceAttribute 特性
            var attribute = pi.GetCustomAttribute<FromKeyedServiceAttribute>();
            if (attribute != null && attribute.Keyed == _keyed)
            {
                valueProvider = () => context.ResolveKeyed(attribute.Keyed, pi.ParameterType);
                return true;
            }

            valueProvider = null;
            return false;
        }
    }
}
