using System;

namespace AmazingShop.Shared.Core.Model
{
    public class TenantInfo
    {
        public Guid TenantId { get; set; }
        public string Domain { get; set; }
    }
}