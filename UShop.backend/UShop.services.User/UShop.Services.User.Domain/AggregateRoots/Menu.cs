using FreeSql.DataAnnotations;
using UShop.Services.User.Domain.Base;
using UShop.Services.User.Domain.Entities;
using UShop.Services.User.Domain.Specifications;

namespace UShop.Services.User.Domain.AggregateRoots
{

    /// <summary>
    /// 菜单- 聚合根
    /// </summary>
    public class Menu:IAggregateRoot,IDeleted, IDisabled, ICreated, ILastUpdated
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsPrimary = true)]
        public long Id { get; private set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public long ParentId { get; private set; }
        /// <summary>
        /// 逻辑删除标识
        /// </summary>
        public bool IsDeleted { get; private set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; } = "";
        /// <summary>
        /// 页面标识 - 建议与前端script标签的name一致
        /// </summary>
        public string Name { get; private set; } = "";
        /// <summary>
        /// 访问路径
        /// </summary>
        public string Path { get; private set; } = "";
        /// <summary>
        /// 重定向地址
        /// </summary>
        public string Redirect { get; private set; } = "";
        /// <summary>
        /// 组件名
        /// </summary>
        public string Component { get; private set; } = "";
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; private set; } = "";
        /// <summary>
        /// 外链连接
        /// </summary>
        public string Link { get; private set; } = "";
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHide { get; private set; }
        /// <summary>
        /// 是否填充
        /// </summary>
        public bool IsFull { get; private set; }
        /// <summary>
        /// 是否固定
        /// </summary>
        public bool IsAffix { get; private set; }
        /// <summary>
        /// 是否缓存 - 当Name与前端script标签的name一致时有效
        /// </summary>
        public bool IsKeepAlive { get; private set; } = true;
        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; private set; }
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
        public DateTime CreatedAt { get; private set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public long LastUpdatedBy { get; private set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdatedAt { get; private set; }

        /// <summary>
        /// 权限列表 - OneToMany
        /// </summary>
        [Navigate(nameof(Permission.MenuId))]
        public List<Permission> Permissions { get; private set; } = new List<Permission>();

        /// <summary>
        /// 父级菜单
        /// </summary>
        [Navigate(nameof(ParentId))]
        public Menu? Parent { get; private set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        [Navigate(nameof(ParentId))]
        public List<Menu>? Children { get; private set; }

        private Menu() { }

        private Menu(string title, string path)
        {
            Title = title;
            Path = path;
        }

        /// <summary>
        /// 生成一个实例
        /// </summary>
        /// <param name="title"></param>
        /// <param name="path"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static Menu Generator(string title, string path, string component = "", string redirect = "")
        {
            MenuSpecification.CanGenerator(title, path);
            Menu menu = new Menu(title, path);
            menu.Component = component;
            menu.Redirect = redirect;
            return menu;
        }

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
            DisabledReason = null;
            DisabledTime = null;
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="name">权限名</param>
        /// <param name="code">权限标识</param>
        /// <param name="description">权限说明</param>
        public void AddPermission(string name, string code, string? description = null)
        {
            Permissions.Add(Permission.Generator(Id, name, code, description));
        }

        /// <summary>
        /// 批量禁用权限
        /// </summary>
        /// <param name="reason"></param>
        public void ChangePermissionDisabled(string reason)
        {
            if (Permissions.Count == 0)
                return;
            Permissions.ForEach(p => p.ChangeDisabled(reason));
        }
        /// <summary>
        /// 禁用指定权限
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="code"></param>
        public void ChangePermissionDisabled(string reason, string code)
        {
            if (Permissions.Count == 0)
                return;
            var permission = Permissions.First(p => p.Code == code);
            permission.ChangeDisabled(reason);
        }
        /// <summary>
        /// 删除指定权限
        /// </summary>
        /// <param name="code"></param>
        public void DeletePermission(string code)
        {
            if (Permissions.Count == 0)
                return;
            var permission = Permissions.First(p => p.Code == code);
            permission.Delete();
        }
        /// <summary>
        /// 清空权限
        /// </summary>
        public void ClearPermission()
        {
            if (Permissions.Count == 0)
                return;
            Permissions.ForEach(p => p.Delete());
        }
    }
}
