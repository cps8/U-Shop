using Microsoft.AspNetCore.Mvc;
using UShop.Shared.Logging;

namespace UShop.Services.User.Api.Controllers
{
    [ApiController]
    [Route("/user/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Test()
        {
            _logger.LogInformation("¼ÇÂ¼ÈÕÖ¾");
            return Ok(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        }
    }
}