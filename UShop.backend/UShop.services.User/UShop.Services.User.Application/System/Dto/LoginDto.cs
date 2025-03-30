using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Services.User.Application.System.Dto
{
    /// <summary>
    /// 登录dto
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "";
    }
}
