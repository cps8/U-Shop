using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;

namespace UShop.Services.User.Domain.Repositories
{
    public interface IMenuRepository
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<Menu>> GetAll();
        
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="menu"></param>
        /// <returns>IsSuccess: 执行结果<br/>Menu: 执行成功时返回插入后的结果，否则返回入参</returns>
        Task<(bool IsSuccess, Menu Menu)> Add(Menu menu);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="menus"></param>
        /// <returns>IsSuccess: 执行结果<br/>Menus: 执行成功时返回插入后的结果，否则返回入参</returns>
        Task<(bool IsSuccess, List<Menu> Menus)> Add(IEnumerable<Menu> menus);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="menu"></param>
        /// <returns>IsSuccess: 执行结果<br/>Menu: 执行成功时返回修改后的结果，否则返回入参</returns>
        Task<(bool IsSuccess, Menu Menu)> Update(Menu menu);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menu"></param>
        /// <returns>IsSuccess: 执行结果<br/>Menu: 执行成功时返回逻辑删除后的结果，否则返回入参</returns>
        Task<(bool IsSuccess, Menu Menu)> Delete(Menu menu);
    }
}
