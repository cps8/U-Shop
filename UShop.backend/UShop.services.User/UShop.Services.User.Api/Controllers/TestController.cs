using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UShop.Services.User.Application;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Shared.Dto;

namespace UShop.Services.User.Api.Controllers
{
    [Route("user/[controller]/[action]")]
    [ApiController]
    public class TestController(ITestService testService) : ControllerBase
    {
        [HttpPost]
        public Task<ResultModel<Account>> AddAccount()
        {
            return testService.AddAccount();
        }
    }
}
