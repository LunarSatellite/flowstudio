using System;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.Tenant
{
    public class PricingHistoryDTO : BaseDTO
    {
        public Guid? TenantId { get; set; }
        public string ServiceType { get; set; }
        public string Provider { get; set; }
        public string Model { get; set; }
        
        public decimal OldBaseCost { get; set; }
        public decimal OldMarkup { get; set; }
        public decimal OldClientPrice { get; set; }
        
        public decimal NewBaseCost { get; set; }
        public decimal NewMarkup { get; set; }
        public decimal NewClientPrice { get; set; }
        
        public decimal ChangePercent { get; set; }
        public string ChangeReason { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public Guid ChangedBy { get; set; }
        
        public bool ClientNotified { get; set; }
        public DateTime? NotifiedAt { get; set; }
    }
}
