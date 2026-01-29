using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Tenant
{
    /// <summary>
    /// Subscription plan with included resources
    /// </summary>
    [Table("TenantSubscriptions")]
    public class TenantSubscription : BaseEntity
    {
        [Required]
        public Guid TenantId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string PlanName { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyFee { get; set; }
        
        public int IncludedTokens { get; set; }
        
        public int IncludedVoiceMinutes { get; set; }
        
        public int IncludedAPICalls { get; set; }
        
        [Required]
        public DateTime StartsAt { get; set; }
        
        public DateTime? EndsAt { get; set; }
        
        [Required]
        public SubscriptionStatus Status { get; set; }
        
        public bool AutoRenew { get; set; }
    }
}
