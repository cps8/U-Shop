using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.Repositories;
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
        public async Task<Domain.AggregateRoots.Account?> GetByName(string name)
        {
            return await _db.Select<Domain.AggregateRoots.Account>().Where(a=> a.Name == name).FirstAsync();
        }

        public async Task<bool> Update(Domain.AggregateRoots.Account account)
        {
            int row = await _db.Update<Domain.AggregateRoots.Account>(account).ExecuteAffrowsAsync();
            return row > 0;
        }
    }
}
