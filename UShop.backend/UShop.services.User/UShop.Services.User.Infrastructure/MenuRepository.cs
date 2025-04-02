using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;
using UShop.Services.User.Domain.Repositories;
using UShop.Shared.Ioc.ServiceProviderFactorySupport;

namespace UShop.Services.User.Infrastructure
{
    [Service]
    public class MenuRepository : IMenuRepository
    {
        private readonly IFreeSql _freeSql;
        public MenuRepository(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, Menu Menu)> Add(Menu menu)
        {
            List<Menu> menus = await _freeSql.Insert(menu).ExecuteInsertedAsync();
            return (menus.Count > 0, menus[0] ?? menu);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<Menu> Menus)> Add(IEnumerable<Menu> menus)
        {
            List<Menu> menusResult = await _freeSql.Insert(menus).ExecuteInsertedAsync();
            return (menusResult.Count > 0, menusResult.Count > 0 ? menusResult : menus.ToList());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, Menu Menu)> Delete(Menu menu)
        {
            menu.Delete();
            List<Menu> menus = await _freeSql.Update<Menu>(menu).ExecuteUpdatedAsync();
            return (menus.Count > 0, menus[0] ?? menu);
        }

        public Task<List<Menu>> GetAll()
        {
            return _freeSql.Select<Menu>().ToTreeListAsync();
        }

        public async Task<(bool IsSuccess, Menu Menu)> Update(Menu menu)
        {
            List<Menu> menus = await _freeSql.Update<Menu>(menu).ExecuteUpdatedAsync();
            return (menus.Count > 0, menus[0] ?? menu);
        }
    }
}
