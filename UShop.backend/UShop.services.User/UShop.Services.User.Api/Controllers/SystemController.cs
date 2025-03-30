using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UShop.Services.User.Application.System;
using UShop.Services.User.Application.System.Dto;

namespace UShop.Services.User.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemService _service;
        public SystemController(ISystemService service) {
            _service = service;
        }

        [HttpPost]
        public Task<bool> Login(LoginDto dto)
        { 
            return _service.Login(dto);
        }
    }
}
