using Autofac.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UShop.Services.User.Application.System;
using UShop.Services.User.Application.System.Dto;
using UShop.Services.User.Application.System.Vo;
using UShop.Shared.Dto;

namespace UShop.Services.User.Api.Controllers
{
    [Route("user/[controller]/[action]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemService _service;
        public SystemController(ISystemService service) {
            _service = service;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/user/login")]
        public Task<ResultModel<LoginVo>> Login(LoginDto dto)
        { 
            return _service.Login(dto);
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns>树型菜单列表</returns>
        [HttpGet]
        [Authorize]
        public Task<ResultModel<List<MenuVo>>> GetMenu()
        {
            return _service.GetMenu();
        }
    }
}
