using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;

namespace UShop.Services.User.Domain.Repositories
{
    /// <summary>
    /// 角色持久化
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// 根据id获取角色列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<Role>> GetRolesById(List<long> ids);
    }
}
