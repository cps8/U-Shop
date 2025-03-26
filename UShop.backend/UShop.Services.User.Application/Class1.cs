using Autofac;
using UShop.Shared.Common.ServiceProviderFactorySupport;

namespace UShop.UserService.Service
{
    public interface INotificationService
    {
        public string Send();
    }

    [Service(keyed:"Emal")]
    public class EmalNotificationService: INotificationService
    {
        public string Send()
        {
            return "Emal";
        }
    }
    [Service(keyed:"Sms")]
    public class SmsNotificationService : INotificationService
    {
        public string Send()
        {
            return "Sms";
        }
    }


    public interface IUserService
    {
        public string Get(int userId);
    }
    [Service]
    public class UserService : IUserService
    {
        public string Get(int userId)
        {
            return userId.ToString();
        }
    }
}
