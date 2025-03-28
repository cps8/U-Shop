using Autofac;
using Microsoft.AspNetCore.Http;
using UShop.Shared.Common;
using UShop.Shared.Common.ServiceProviderFactorySupport;

namespace UShop.Service.User.Application
{
    public interface INotificationService
    {
        public string Send();
    }

    [Service(keyed: "Emal")]
    public class EmalNotificationService : INotificationService
    {
        public string Send()
        {
            return "Emal";
        }
    }
    [Service(keyed: "Sms")]
    public class SmsNotificationService : INotificationService
    {
        public string Send()
        {
            return "Sms";
        }
    }


    public interface IUserService
    {
        public string Get();
    }
    [Service]
    public class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
    {
        public string Get()
        {
            return JwtUtils.GetUserId(httpContextAccessor) ?? "";
        }
    }
}
