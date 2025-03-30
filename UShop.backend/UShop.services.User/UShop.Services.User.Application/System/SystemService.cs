using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Application.System.Dto;
using UShop.Services.User.Domain.Account;
using UShop.Shared.Ioc.ServiceProviderFactorySupport;

namespace UShop.Services.User.Application.System
{
    /// <summary>
    /// 系统服务
    /// </summary>
    [Service]
    public class SystemService : ISystemService
    {
        /// <summary>
        /// 账号领域服务
        /// </summary>
        private readonly IAccountRepository _accountRepository;
        public SystemService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> Login(LoginDto dto)
        {
            Account? account = await _accountRepository.Get(dto.Name, dto.Password);
            return account != null;
        }
    }
}
