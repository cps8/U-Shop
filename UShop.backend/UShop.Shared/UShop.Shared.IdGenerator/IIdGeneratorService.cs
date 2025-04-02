using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Shared.IdGenerator
{
    public interface IIdGeneratorService
    {
        /// <summary>
        /// 生成雪花id
        /// </summary>
        /// <returns></returns>
        long GenerateId();
    }
}
