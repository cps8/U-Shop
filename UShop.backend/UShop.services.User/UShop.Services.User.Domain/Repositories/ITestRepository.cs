using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;

namespace UShop.Services.User.Domain.Repositories
{
    /// <summary>
    /// 持久化测试
    /// </summary>
    public interface ITestRepository
    {
        Task<Account> AddAccount(Account account);

        Task<Role> AddRole(Role role);
    }
}
