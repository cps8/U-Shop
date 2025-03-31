using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Services.User.Domain.Base
{
    /// <summary>
    /// 记录最后修改人
    /// </summary>
    internal interface ILastUpdated
    {
        /// <summary>
        /// 最后修改人id
        /// </summary>
        [Column(CanUpdate = false)]
        public long LastUpdatedBy { get; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Column(CanUpdate = false)]
        public DateTime LastUpdatedAt { get; }
    }
}
