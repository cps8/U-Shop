using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.AggregateRoots;

namespace UShop.Services.User.Domain.Specifications
{
    public class MenuSpecification
    {
        /// <summary>
        /// 校验能否创建菜单
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CanGenerator(string name, string path)
        {
            return !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(path);
        }
    }
}
