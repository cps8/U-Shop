using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Services.User.Domain.Base
{
    /// <summary>
    /// 记录创建人
    /// </summary>
    internal interface ICreated
    {
        /// <summary>
        /// 创建人id
        /// </summary>
        [Column(CanUpdate = false)]
        public long CreatedBy { get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(CanUpdate = false)]
        public DateTime CreatedAt { get; }
    }
}
