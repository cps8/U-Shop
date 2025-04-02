using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Shared.Common;

namespace UShop.Services.User.Domain.Specifications
{
    public class LoginSpecification
    {
        private readonly string _pwd;
        private readonly Account _account;
        public LoginSpecification(string pwd, Account account)
        {
            _pwd = pwd;
            _account = account;
        }
        /// <summary>
        /// 校验密码是否正确
        /// </summary>
        /// <returns></returns>
        public bool VerifyPassword()
        {
            return PasswordHelper.VerifyPassword(_pwd, _account.Password);
        }

        /// <summary>
        /// 校验是否被禁用
        /// </summary>
        /// <returns></returns>
        public (bool value, string? reason, DateTime? time) IsDisable() {
            bool value = _account.IsDisabled == true;
            return (value, _account.DisabledReason, _account.DisabledTime);
        }
    }
}
