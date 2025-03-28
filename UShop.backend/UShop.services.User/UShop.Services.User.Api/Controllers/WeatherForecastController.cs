using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UShop.Service.User.Application;
using UShop.Shared.Common;
using UShop.Shared.Common.ServiceProviderFactorySupport;

namespace UShop.Services.User.Api.Controllers
{
    [ApiController]
    [Route("/user/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly INotificationService _service;
        private readonly IUserService _userService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, [FromKeyedService("Sms")] INotificationService service, IUserService userService)
        {
            _logger = logger;
            _service = service;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Test ()
        {
            return Ok(_service.Send());    
        }
        [HttpGet]
        public IActionResult Token()
        {
            return Ok(JwtUtils.GenerateJwtToken("123"));
        }

        [HttpGet]
        [Authorize]
        public IActionResult UserId()
        {
            return Ok(_userService.Get());
        }
    }
}
