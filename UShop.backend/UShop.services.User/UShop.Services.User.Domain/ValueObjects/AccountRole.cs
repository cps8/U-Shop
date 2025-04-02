using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Services.User.Domain.Base;

namespace UShop.Services.User.Domain.ValueObjects
{
    /// <summary>
    /// 账号角色关联表
    /// </summary>
    internal class AccountRole: IValueObject
    {
        [Column(IsPrimary = true)]
        public long AccountId { get; private set; }

        [Column(IsPrimary = true)]
        public long RoleId { get; private set; }

        [Navigate(nameof(AccountId))]
        public Account Account { get; private set; }

        [Navigate(nameof(RoleId))]
        public Role Role { get; private set; }

        public override string ToString()
        {
            return $"{AccountId}, {RoleId}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is not AccountRole)
                return false;
            var ar = (AccountRole)obj;
            return AccountId == ar.AccountId && RoleId == ar.RoleId;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
