using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using UShop.Common;
using UShop.Common.ServiceProviderFactorySupport;
using UShop.UserServer.Service;

namespace UShop.UserServer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, [FromKeyedService("Emal")] INotificationService notificationService, IUserService userService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _userService = userService;
        }

        [HttpGet]
        [Route("/api/test")]
        public ActionResult Test()
        {
            string result = _notificationService.Send();
            string user = _userService.Get(1);
            return Ok(new { user, result});
        }
    }
}
