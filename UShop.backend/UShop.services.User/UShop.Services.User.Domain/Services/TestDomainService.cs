using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Services.User.Domain.Repositories;
using UShop.Shared.IdGenerator;
using UShop.Shared.Ioc.ServiceProviderFactorySupport;

namespace UShop.Services.User.Domain.Services
{
    [Service]
    public class TestDomainService(IIdGeneratorService idGeneratorService, ITestRepository testRepository) : ITestDomainService
    {
        public Task<Account> AddAccount()
        {
            Account account = Account.Generator(idGeneratorService, "test", "password123");
            Role role = Role.Generator(idGeneratorService, "测试角色");
            account.Roles.Add(role);
            return testRepository.AddAccount(account);
        }
    }
}
