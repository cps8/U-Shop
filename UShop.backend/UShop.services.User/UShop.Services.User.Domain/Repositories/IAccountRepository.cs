using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;

namespace UShop.Services.User.Domain.Repositories
{
    /// <summary>
    /// 账号持久化服务
    /// </summary>
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(long id);
        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Account?> GetByNameAsync(string name);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Account account);
    }
}
