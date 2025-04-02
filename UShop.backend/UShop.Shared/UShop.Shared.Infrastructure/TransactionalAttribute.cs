using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Shared.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class TransactionalAttribute : Attribute
    {
        /// <summary>
        /// 事务传播方式
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Required;

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        public TransactionalAttribute() { }

        public TransactionalAttribute(Propagation propagation, IsolationLevel isolationLevel)
        {
            Propagation = propagation;
            IsolationLevel = isolationLevel;
        }
    }
}
