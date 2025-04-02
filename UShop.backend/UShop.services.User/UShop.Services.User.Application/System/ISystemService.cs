using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Application.System.Dto;
using UShop.Services.User.Application.System.Vo;
using UShop.Shared.Dto;

namespace UShop.Services.User.Application.System
{
    /// <summary>
    /// 系统业务服务
    /// </summary>
    public interface ISystemService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResultModel<LoginVo>> Login(LoginDto dto);
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        Task<ResultModel<List<MenuVo>>> GetMenu();
    }
}
