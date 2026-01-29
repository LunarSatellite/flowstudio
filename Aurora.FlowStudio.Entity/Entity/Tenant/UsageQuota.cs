using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Tenant
{
    /// <summary>
    /// Spending limits and quotas
    /// Auto-pause service when limits exceeded
    /// </summary>
    [Table("UsageQuotas")]
    public class UsageQuota : TenantBaseEntity
    {
        public UsageType? Type { get; set; }
        
        [Required]
        public QuotaPeriod Period { get; set; }
        
        // Limits
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MaxDollarAmount { get; set; }
        
        public int? MaxQuantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? WarningThreshold { get; set; }
        
        // Actions
        [Required]
        public QuotaAction OnExceed { get; set; }
        
        public bool AutoReset { get; set; }
        
        // Current usage
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentAmount { get; set; }
        
        public int CurrentQuantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PercentUsed { get; set; }
        
        // Status
        public bool IsActive { get; set; }
        
        public bool IsExceeded { get; set; }
        
        public DateTime? ExceededAt { get; set; }
        
        public DateTime? LastResetAt { get; set; }
        
        public DateTime? NextResetAt { get; set; }
    }
}
