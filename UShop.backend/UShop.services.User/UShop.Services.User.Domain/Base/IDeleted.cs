using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Services.User.Domain.Base
{
    /// <summary>
    /// 逻辑删除
    /// </summary>
    internal interface IDeleted
    {
        public bool IsDeleted { get; }
        public void Delete();
    }
}
