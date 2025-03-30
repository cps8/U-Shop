using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Services.User.Domain.Account
{
    public interface IAccountRepository
    {
        Task<Account?> Get(string name, string password);
    }
}
