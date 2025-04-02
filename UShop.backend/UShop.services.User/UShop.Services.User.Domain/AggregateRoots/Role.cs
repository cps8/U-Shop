using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.Base;
using UShop.Services.User.Domain.Entities;
using UShop.Services.User.Domain.ValueObjects;
using UShop.Shared.IdGenerator;

namespace UShop.Services.User.Domain.AggregateRoots
{
    public class Role: IAggregateRoot, IDeleted, IDisabled, ICreated, ILastUpdated
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsPrimary = true)]
        public long Id { get; private set; }
        /// <summary>
        /// 逻辑删除标识
        /// </summary>
        public bool IsDeleted { get; private set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; private set; } = "";
        /// <summary>
        /// 角色描述
        /// </summary>
        public string? Description { get; private set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; private set; }
        /// <summary>
        /// 禁用时间
        /// </summary>
        public DateTime? DisabledTime { get; private set; }
        /// <summary>
        /// 禁用原因
        /// </summary>
        public string? DisabledReason { get; private set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; private set;}
        /// <summary>
        /// 最后修改人
        /// </summary>
        public long LastUpdatedBy { get; private set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdatedAt { get; private set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Navigate(ManyToMany = typeof(AccountRole))]
        public List<Account> Accounts { get; private set; } = new List<Account>();

        /// <summary>
        /// 权限列表
        /// </summary>
        [Navigate(ManyToMany = typeof(RolePermission))]
        public List<Permission> Permissions { get; private set; } = new List<Permission>();

        /// <summary>
        /// 菜单列表，依赖权限列表
        /// </summary>
        [Column(IsIgnore = true)]
        public List<Menu> Menus => Permissions.Select(p => p.Menu).Distinct().ToList();

        private Role() { }
        private Role(long id, string name, string? description = null)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// 生成一个角色实例
        /// </summary>
        /// <param name="idGeneratorServicee">id初始化服务</param>
        /// <param name="name">角色名</param>
        /// <param name="description">角色描述</param>
        /// <returns>返回一个实例化的角色对象</returns>
        public static Role Generator(IIdGeneratorService idGeneratorServicee, string name, string? description = null)
        {
            return new Role(idGeneratorServicee.GenerateId(), name, description);
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
        /// <param name="reason"></param>
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

        public void BindPermissions(List<Permission> permissions)
        { 
            
        }
    }
}
