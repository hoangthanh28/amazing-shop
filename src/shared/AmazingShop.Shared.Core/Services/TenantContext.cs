using System;
using System.Collections.Generic;
using System.Linq;
using AmazingShop.Shared.Core.Model;
using AmazingShop.Shared.Core.Service.MultiTenancy.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AmazingShop.Shared.Core.Service.MultiTenancy
{
    public class TenantContext : ITenantContext
    {
        private Guid _tenantId;
        private string _domain;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TenantContext(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid TenantId => _tenantId;


        public ITenantContext SetTenantIdentifier(Guid tenantId)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var tenantString = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "tenants");
                if (tenantString != null)
                {
                    var tenants = JsonConvert.DeserializeObject<IEnumerable<TenantInfo>>(tenantString.Value);
                    var tenantInfo = tenants.FirstOrDefault(x => x.TenantId == tenantId);
                    if (tenantInfo == null)
                    {
                        throw new Exception("Can not parse the tenant in claim");
                    }
                    _tenantId = tenantInfo.TenantId;
                    _domain = tenantInfo.Domain;
                }
            }
            return this;
        }
        public string Domain => _domain;
    }
}