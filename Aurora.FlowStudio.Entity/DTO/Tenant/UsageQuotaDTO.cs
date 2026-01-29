using System;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Tenant
{
    public class UsageQuotaDTO : TenantBaseDTO
    {
        public UsageType? Type { get; set; }
        public QuotaPeriod Period { get; set; }
        
        public decimal? MaxDollarAmount { get; set; }
        public int? MaxQuantity { get; set; }
        public decimal? WarningThreshold { get; set; }
        
        public QuotaAction OnExceed { get; set; }
        public bool AutoReset { get; set; }
        
        public decimal CurrentAmount { get; set; }
        public int CurrentQuantity { get; set; }
        public decimal PercentUsed { get; set; }
        
        public bool IsActive { get; set; }
        public bool IsExceeded { get; set; }
        public DateTime? ExceededAt { get; set; }
        public DateTime? LastResetAt { get; set; }
        public DateTime? NextResetAt { get; set; }
    }
}
