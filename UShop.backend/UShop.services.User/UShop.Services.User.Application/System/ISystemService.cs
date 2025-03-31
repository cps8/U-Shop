using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Application.System.Dto;
using UShop.Services.User.Application.System.Vo;
using UShop.Shared.Dto;

namespace UShop.Services.User.Application.System
{
    public interface ISystemService
    {
        Task<ResultModel<LoginVo>> Login(LoginDto dto);
    }
}
