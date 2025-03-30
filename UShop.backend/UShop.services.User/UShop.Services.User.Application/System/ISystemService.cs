using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Application.System.Dto;

namespace UShop.Services.User.Application.System
{
    public interface ISystemService
    {
        Task<bool> Login(LoginDto dto);
    }
}
