using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Shared.Ioc.ServiceProviderFactorySupport;

namespace UShop.Services.User.Infrastructure
{
    /// <summary>
    /// 角色持久化
    /// </summary>
    /// <param name="db"></param>
    [Service]
    public class RoleRepository(IFreeSql db)
    {
        /// <summary>
        /// 根据id获取角色列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<Role>> GetRolesById(List<long> ids)
        {
            return db.Select<Role>(ids).ToListAsync();
        }
    }
}
