using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Shared.Common;

namespace UShop.Services.User.Application.Menu.Vo
{
    /// <summary>
    /// 菜单管理VO
    /// </summary>
    /// <param name="menu"></param>
    public class MenuManagerVo(Domain.AggregateRoots.Menu menu)
    {
        public string Id => menu.Id.ToAesEncrypt();
        public string Path => menu.Path;
        public string Name => menu.Name;
        public string Component => menu.Component;
        public string Redirect => menu.Redirect;
        public MenuManagerVo? Parent => menu.Parent != null ? new MenuManagerVo(menu) : null;
        public object Meta => new
        {
            Icon = menu.Icon,
            menu.Title,
            IsLink = menu.Link,
            menu.IsHide,
            menu.IsFull,
            menu.IsAffix,
            menu.IsKeepAlive
        };
        public List<MenuManagerVo>? Children => menu.Children?.Select(m => new MenuManagerVo(m)).ToList();
    }
}
