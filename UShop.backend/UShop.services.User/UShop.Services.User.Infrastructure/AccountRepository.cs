
using Org.BouncyCastle.Asn1.X509;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Services.User.Domain.Repositories;
using UShop.Shared.Ioc.ServiceProviderFactorySupport;

namespace UShop.Services.User.Infrastructure
{
    [Service]
    public class AccountRepository(IFreeSql db) : IAccountRepository
    {
        public Task<Account?> GetByIdAsync(long id)
        {
            return db.Select<Account?>(id).FirstAsync();
        }
        public async Task<Account?> GetByNameAsync(string name)
        {
            var repository = db.GetAggregateRootRepository<Account>();
            //return await db.Select<Domain.AggregateRoots.Account>().Where(a=> a.Name == name).IncludeMany(a=> a.Roles).FirstAsync();
            return await repository.Select.Where(a => a.Name == name).FirstAsync();
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            int row = await db.Update<Account>(account).ExecuteAffrowsAsync();
            return row > 0;
        }
    }
}
