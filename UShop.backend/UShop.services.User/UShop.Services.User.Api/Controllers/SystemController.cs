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

        [HttpPost]
        [Route("/user/login")]
        public Task<ResultModel<LoginVo>> Login(LoginDto dto)
        { 
            return _service.Login(dto);
        }
    }
}
