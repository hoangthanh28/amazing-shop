using System;

namespace Function.Service.Model
{
    public class ShardingModel
    {
        public string ServerName { get; set; }
        public string ServiceName { get; set; }
        public string EnvironmentName { get; set; }
        public string DatabaseName { get; set; }
        public Guid TenantId { get; set; }
        public Guid SubTenantId { get; set; }

    }
}