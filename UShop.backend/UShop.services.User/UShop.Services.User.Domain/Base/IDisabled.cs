using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Services.User.Domain.Base
{
    public interface IDisabled
    {
        /// <summary>
        /// 是否被禁用
        /// </summary>
        public bool IsDisabled { get; }
        public DateTime? DisabledTime { get; }
        public string? DisabledReason { get; }

        /// <summary>
        /// 禁用
        /// </summary>
        public void ChangeDisabled(string reason);
        /// <summary>
        /// 启用
        /// </summary>
        public void ChangeEnabled();
    }
}
