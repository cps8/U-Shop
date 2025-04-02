using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Services.User.Domain.Repositories;
using UShop.Shared.Infrastructure;
using UShop.Shared.Ioc.ServiceProviderFactorySupport;

namespace UShop.Services.User.Infrastructure
{
    /// <summary>
    /// 持久化测试
    /// </summary>
    [Service]
    public class TestRepository(IFreeSql db, IBaseRepository<Account> accountRepository, IBaseRepository<Role> roleRepository) : ITestRepository
    {
        [Transactional]
        public async Task<Account> AddAccount(Account account)
        {
            var repository = accountRepository.Orm.GetAggregateRootRepository<Account>();
            await roleRepository.InsertAsync(account.Roles);
            throw new Exception("测试异常");
            return await accountRepository.InsertAsync(account);
        }

        public Task<Role> AddRole(Role role)
        {
            var repository = db.GetAggregateRootRepository<Role>();
            db.Select<Role>(role).First();
            return repository.InsertAsync(role);
        }
    }
}
