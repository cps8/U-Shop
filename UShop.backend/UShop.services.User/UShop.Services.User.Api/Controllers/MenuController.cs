using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UShop.Services.User.Application.Menu;
using UShop.Services.User.Application.Menu.Vo;
using UShop.Services.User.Application.System.Vo;
using UShop.Shared.Dto;

namespace UShop.Services.User.Api.Controllers
{
    [Route("user/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuServices _services;
        public MenuController(IMenuServices services)
        {
            _services = services;
        }
        [HttpGet("All")]
        [Authorize]
        public Task<ResultModel<List<MenuManagerVo>>> GetAll()
        {
            return _services.GetAll();
        }
    }
}
