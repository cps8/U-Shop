using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.Account;
using UShop.Shared.Ioc.ServiceProviderFactorySupport;

namespace UShop.Services.User.Infrastructure.Account
{
    [Service]
    public class AccountRepository : IAccountRepository
    {
        private readonly IFreeSql _db;
        public AccountRepository(IFreeSql db)
        {
            _db = db;
        }
        public async Task<Domain.Account.Account?> Get(string name, string password)
        {
            return await _db.Select<Domain.Account.Account>().Where(a=> a.Name == name && a.Password == password).FirstAsync();
        }
    }
}
