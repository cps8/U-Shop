using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UShop.Services.User.Domain.Base;

namespace UShop.Services.User.Domain.AggregateRoots
{
    public class Menu:IAggregateRoot,IDeleted, ICreated, ILastUpdated
    {
        public long Id { get; private set; }
        public long ParentId { get; private set; }
        public bool IsDeleted { get; private set; }
        public string Title { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
        public string Component { get; private set; }
        public string Icon { get; private set; } = "";
        public string Link { get; private set; } = "";
        public bool IsHide { get; private set; }
        public bool IsFull { get; private set; }
        public bool IsAffix { get; private set; }
        public bool IsKeepAlive { get; private set; } = true;
        public int Index { get; private set; }
        public bool Disabled { get; private set; }
        public DateTime? DisabledTime { get; private set; }
        public string? DisableReason { get; private set; }
    }
}
