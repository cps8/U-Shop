using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Services.User.Domain.Base;
using UShop.Services.User.Domain.Entities;

namespace UShop.Services.User.Domain.ValueObjects
{
    /// <summary>
    /// 角色-权限 关联表
    /// </summary>
    internal class RolePermission:IValueObject
    {
        [Column(IsPrimary = true)]
        public long RoleId { get; private set; }
        [Column(IsPrimary = true)]
        public long PermissionId { get; private set; }

        [Navigate(nameof(RoleId))]
        public Role Role { get; private set; }
        [Navigate(nameof(PermissionId))]
        public Permission Permission { get; private set; }

        public override string ToString()
        {
            return $"{RoleId}, {PermissionId}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is not RolePermission)
                return false;
            var ar = (RolePermission)obj;
            return PermissionId == ar.PermissionId && RoleId == ar.RoleId;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
