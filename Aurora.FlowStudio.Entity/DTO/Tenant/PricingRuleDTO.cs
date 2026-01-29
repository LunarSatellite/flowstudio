using System;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.Tenant
{
    public class PricingRuleDTO : TenantBaseDTO
    {
        public string ServiceType { get; set; }
        public string Provider { get; set; }
        public string Model { get; set; }
        public decimal BaseCostPer1K { get; set; }
        public decimal MarkupPercent { get; set; }
        public decimal FinalPricePer1K { get; set; }
        public bool IsActive { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}
