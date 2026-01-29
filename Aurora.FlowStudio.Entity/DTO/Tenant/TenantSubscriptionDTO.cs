using System;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Tenant
{
    public class TenantSubscriptionDTO : BaseDTO
    {
        public Guid TenantId { get; set; }
        public string PlanName { get; set; }
        public decimal MonthlyFee { get; set; }
        public int IncludedTokens { get; set; }
        public int IncludedVoiceMinutes { get; set; }
        public int IncludedAPICalls { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
        public SubscriptionStatus Status { get; set; }
        public bool AutoRenew { get; set; }
    }
}
