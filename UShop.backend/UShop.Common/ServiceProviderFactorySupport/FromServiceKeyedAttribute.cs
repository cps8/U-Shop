using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Common.ServiceProviderFactorySupport
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class FromKeyedServiceAttribute : Attribute
    {
        public string Keyed { get; }

        public FromKeyedServiceAttribute(string keyed)
        {
            Keyed = keyed ?? throw new ArgumentNullException(nameof(keyed));
        }
    }
}
