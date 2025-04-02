using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Application.System.Dto;
using UShop.Services.User.Application.System.Vo;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Services.User.Domain.Repositories;
using UShop.Services.User.Domain.Specifications;
using UShop.Services.User.Infrastructure;
using UShop.Shared.Common;
using UShop.Shared.Dto;
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
        /// 账号持久化服务
        /// </summary>
        private readonly IAccountRepository _accountRepository;
        /// <summary>
        /// 菜单持久化服务
        /// </summary>
        private readonly IMenuRepository _menuRepository;
        public SystemService(IAccountRepository accountRepository, IMenuRepository menuRepository)
        {
            _accountRepository = accountRepository;
            _menuRepository = menuRepository;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResultModel<LoginVo>> Login(LoginDto dto)
        {   
            Account? account = await _accountRepository.GetByNameAsync(dto.Name);
            if(account == null)
                return ResultModel<LoginVo>.Failed(1000, "登录失败, 用户名或密码错误");
            LoginSpecification specification = new LoginSpecification(dto.Password, account);
            if (!specification.VerifyPassword())
                return ResultModel<LoginVo>.Failed(1000, "登录失败, 用户名或密码错误");
            var isDisable = specification.IsDisable();
            if (isDisable.value)
                return ResultModel<LoginVo>.Failed(1001, $"登录失败，账号被禁用，禁用原因：{isDisable.reason}");
            string token = JwtUtils.GenerateJwtToken(account!.Id.ToAesEncrypt());

            return ResultModel<LoginVo>.Success(new LoginVo { Token = token,}, "登录成功");
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public async Task<ResultModel<List<MenuVo>>> GetMenu()
        {
            var menus = await _menuRepository.GetAll();

            return ResultModel<List<MenuVo>>.Success(menus.Select(m => new MenuVo(m)).ToList());
        }
    }
}
