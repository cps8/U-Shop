using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Shared.Common;

namespace UShop.Services.User.Application.System.Vo
{
    /// <summary>
    /// 系统展示菜单VO
    /// </summary>
    /// <param name="menu"></param>
    public class MenuVo(Domain.AggregateRoots.Menu menu)
    {
        public string Path => menu.Path;
        public string Name => menu.Name;
        public string Component => menu.Component;
        public string Redirect => menu.Redirect;
        public object Meta => new
        {
            menu.Icon,
            menu.Title,
            IsLink = menu.Link,
            menu.IsHide,
            menu.IsFull,
            menu.IsAffix,
            menu.IsKeepAlive
        };
        public List<MenuVo>? Children => menu.Children?.Select(m => new MenuVo(m)).ToList();
    }
}
