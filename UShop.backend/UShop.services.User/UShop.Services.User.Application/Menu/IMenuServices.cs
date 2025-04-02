using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Application.Menu.Vo;
using UShop.Services.User.Application.System.Vo;
using UShop.Shared.Dto;

namespace UShop.Services.User.Application.Menu
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public interface IMenuServices
    {
        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        Task<ResultModel<List<MenuManagerVo>>> GetAll();
    }
}
