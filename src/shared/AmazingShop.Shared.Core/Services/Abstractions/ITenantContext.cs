using System;

namespace AmazingShop.Shared.Core.Service.MultiTenancy.Abstraction
{
    public interface ITenantContext
    {
        ITenantContext SetTenantIdentifier(Guid tenantId);
        Guid TenantId { get; }
        string Domain { get; }
    }
}
