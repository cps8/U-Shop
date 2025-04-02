using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Services.User.Domain.Services;
using UShop.Shared.Dto;
using UShop.Shared.Ioc.ServiceProviderFactorySupport;

namespace UShop.Services.User.Application
{
    [Service]
    public class TestService : ITestService
    {
        private readonly ITestDomainService _testDomainService;
        public TestService(ITestDomainService testDomainService)
        {
            _testDomainService = testDomainService;
        }
        public async Task<ResultModel<Account>> AddAccount()
        {
            Account account = await _testDomainService.AddAccount();
            return ResultModel<Account>.Success(account);
        }
    }
}
