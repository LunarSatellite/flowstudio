using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.Tenant
{
    public class APIKeyDTO : TenantBaseDTO
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public List<string> AllowedIPs { get; set; }
        public List<string> AllowedOrigins { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public bool IsActive { get; set; }
        public int RequestCount { get; set; }
    }
}
