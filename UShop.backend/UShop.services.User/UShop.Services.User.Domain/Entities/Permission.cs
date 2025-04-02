using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Services.User.Domain.Base;

namespace UShop.Services.User.Domain.Entities
{
    public class Permission:IEntity, IDeleted, IDisabled, ICreated, ILastUpdated
    {
        [Column(IsPrimary = true)]
        public long Id { get; private set; }
        public bool IsDeleted { get; private set; }
        public long MenuId { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string? Description { get; private set; }
        public bool IsDisabled { get; private set; }
        public DateTime? DisabledTime { get; private set; }
        public string? DisabledReason { get; private set; }
        public long CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long LastUpdatedBy { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }

        /// <summary>
        /// 所属菜单 - ManyToOne
        /// </summary>
        [Navigate(nameof(MenuId))]
        public Menu Menu { get; private set; }

        private Permission() { }

        private Permission(long menuId, string name, string code, string? description = null)
        {
            MenuId = menuId;
            Name = name;
            Code = code;
            Description = description;
        }

        public static Permission Generator(long menuId, string name, string code, string? description = null)
        {
            return new Permission(menuId, name, code, description);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            IsDeleted = true;
        }
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="reason">禁用原因</param>
        public void ChangeDisabled(string reason)
        {
            IsDisabled = true;
            DisabledTime = DateTime.Now;
            DisabledReason = reason;
        }
        /// <summary>
        /// 启用
        /// </summary>
        public void ChangeEnabled()
        {
            IsDisabled = false;
            DisabledTime = null;
            DisabledReason = null;
        }
    }
}
