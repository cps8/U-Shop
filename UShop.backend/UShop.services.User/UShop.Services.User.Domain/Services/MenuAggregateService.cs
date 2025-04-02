using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Services.User.Domain.Repositories;

namespace UShop.Services.User.Domain.Services
{
    public class MenuAggregateService(IAccountRepository accountRepository) : IMenuAggregateService
    {
    }
}
