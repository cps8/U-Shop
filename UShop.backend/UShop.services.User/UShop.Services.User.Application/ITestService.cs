using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Shared.Dto;

namespace UShop.Services.User.Application
{
    public interface ITestService
    {
        Task<ResultModel<Account>> AddAccount();
    }
}
